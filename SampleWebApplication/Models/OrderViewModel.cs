using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebApplication.Models
{
    [Table("OrderView")]
    public class OrderViewModel
    {
        [Key]
		public int OrderId
		{
			get;
			set;
		}

		public decimal? Freight
		{
			get;
			set;
		}

		[Required]
		public DateTime? OrderDate
		{
			get;
			set;
		}

		public string ShipCity
		{
			get;
			set;
		}

		public string ShipName
		{
			get;
			set;
		}
	}
}
