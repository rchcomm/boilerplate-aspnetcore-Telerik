using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebApplication.Models
{
    public class DbInitializer
    {
        public static void Initialize(BloggingContext context)
        {
            var isEnsulreCreated = context.Database.EnsureCreated();
            if (isEnsulreCreated)
            {

            }

            if (context.Blogs.Any())
            {
                return;
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    context.Blogs.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
                }
                var count = context.SaveChanges();
            }

            if (context.OrderView.Any())
            {
                return;   // DB has been seeded
            }
            else
            {
                var result = Enumerable.Range(0, 250).Select(i => new OrderViewModel
                {
                    Freight = i * 10,
                    OrderDate = new DateTime(2016, 9, 15).AddDays(i % 7),
                    ShipName = "ShipName " + i,
                    ShipCity = "ShipCity " + i
                });

                result.Each(item => context.OrderView.Add(item));

                context.SaveChanges();
            }

            //foreach (var userId in new[] { "ing", "feelanet01", "feelanet02", "feelanet03", "feelanet04" })
            //{
            //    int limit1 = new Random(DateTime.Now.Millisecond).Next(100);
            //    for (int i = 1; i <= limit1; i++)
            //    {
            //        var httpFolderInfo = new HttpFolderInfo() { ParentId = 0, FileCount = 0, IsFlag = false, Name = "Folder #" + i, Order = i, Size = 0, UserId = userId };
            //        context.HttpFolderInfos.Add(httpFolderInfo);
            //        int limit2 = new Random(DateTime.Now.Millisecond + 1000).Next(100);
            //        for (int j = 1; j <= limit2; j++)
            //        {
            //            httpFolderInfo.Files.Add(new HttpFileInfo() { FolderInfo = httpFolderInfo, Name = httpFolderInfo.Name + "_FileName #" + j });
            //        }

            //        httpFolderInfo.FileCount = httpFolderInfo.Files.Count();
            //    }
            //}

            context.SaveChanges();
        }
    }
}
