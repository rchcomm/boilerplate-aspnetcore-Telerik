using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Kendo.Mvc.UI;
using SampleWebApplication.Models;
using Kendo.Mvc.Extensions;
using Microsoft.EntityFrameworkCore;
using Dapper;

namespace SampleWebApplication.Controllers
{
	[Produces("application/json")]
	[Route("api/[controller]")]
	public class GridController : Controller
	{
        private static bool entityFrameworkExecuteMode = true;

		private readonly IUnitOfWork unitOfWork;
		private readonly IRepository<OrderViewModel> orderViewRepository;
        private readonly MyConnection Connection;



        public GridController(IUnitOfWork unitOfWork, MyConnection connection)
		{
			this.unitOfWork = unitOfWork;
			this.orderViewRepository = unitOfWork.GetRepository<OrderViewModel>();
            this.Connection = connection;
        }

        [HttpGet()]
        public async Task<DataSourceResult> Get([DataSourceRequest]DataSourceRequest request)
		{
			return await this.orderViewRepository.GetAll().ToDataSourceResultAsync(request);
		}

        [HttpPost()]
        public async Task<OrderViewModel> Post([FromBody] OrderViewModel orderView)
        {
            if (ModelState.IsValid)
            {
#if EntiyFrameworkUsing
                await this.orderViewRepository.InsertAsync(orderView);
                await this.unitOfWork.SaveChangesAsync();
#else
                using (this.Connection.Instance)
                {
                    this.Connection.Instance.Open();
                    try
                    {
                        string sQuery = "INSERT INTO OrderView (ShipCity, ShipName, OrderDate, Freight) VALUES(@ShipCity, @ShipName, @OrderDate, @Freight)";
                        await this.Connection.Instance.ExecuteAsync(sQuery, orderView);
                    }
                    catch (Exception ex)
                    {
                        var message = ex.Message;
                    }
                }
#endif
            }


            return orderView;
        }

        [HttpPut()]
        public async Task<OrderViewModel> Put([FromBody] OrderViewModel orderView)
        {
            OrderViewModel order = null;
            if (ModelState.IsValid)
            {
#if EntiyFrameworkUsing
                order = await this.orderViewRepository.GetFirstOrDefaultAsync(predicate: item => item.OrderId == orderView.OrderId);
                order.Freight = orderView.Freight;
                order.OrderDate = orderView.OrderDate;
                order.ShipCity = orderView.ShipCity;
                order.ShipName = orderView.ShipName;

                this.orderViewRepository.Update(order);
                await this.unitOfWork.SaveChangesAsync();
#else
                using (this.Connection.Instance)
                {
                    this.Connection.Instance.Open();
                    string sQuery = "UPDATE OrderView SET ShipCity = @ShipCity, ShipName = @ShipName, OrderDate = @OrderDate, Freight = @Freight Where OrderId = @OrderId";
                    await this.Connection.Instance.ExecuteAsync(sQuery, orderView);
                    order = orderView;
                }
#endif
            }

            return order;
        }

        [HttpDelete()]
        public async Task<OrderViewModel> Delete([FromBody] OrderViewModel orderView)
        {
#if EntiyFrameworkUsing
            this.orderViewRepository.Delete(orderView);
            await this.unitOfWork.SaveChangesAsync();
#else
            using (this.Connection.Instance)
            {
                this.Connection.Instance.Open();
                string sQuery = "DELETE FROM OrderView Where OrderId = @OrderId";
                await this.Connection.Instance.ExecuteAsync(sQuery, orderView);
            }
#endif
            return await Task.FromResult(orderView);
        }
    }
}
