using Kendo.Mvc.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebApplication.Models
{
    public class DbInitializer
    {
        public static async Task InitializeDatabaseAsync(IServiceProvider serviceProvider, bool createUsers = true)
        {
            using (var serviceScope = serviceProvider.CreateScope())
            {
                var scopeServiceProvider = serviceScope.ServiceProvider;
                var db = scopeServiceProvider.GetService<BloggingContext>();

                if (await db.Database.EnsureCreatedAsync())
                {
                    await DbInitializer.InitializeAsync(db);
                }
            }
        }

        public static async Task<bool> InitializeAsync(BloggingContext context)
        {
            if (context.Blogs.Any())
            {
                return await Task.FromResult(true);
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
                return await Task.FromResult(false);   // DB has been seeded
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

            return await Task.FromResult<bool>(true);
        }

        // https://github.com/aspnet/MusicStore/blob/dev/samples/MusicStore/Models/SampleData.cs
        // TODO [EF] This may be replaced by a first class mechanism in EF
        private static async Task AddOrUpdateAsync<TEntity>(
            IServiceProvider serviceProvider,
            Func<TEntity, object> propertyToMatch, IEnumerable<TEntity> entities)
            where TEntity : class
        {
            // Query in a separate context so that we can attach existing entities as modified
            List<TEntity> existingData;
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<BloggingContext>();
                existingData = db.Set<TEntity>().ToList();
            }

            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<BloggingContext>();
                foreach (var item in entities)
                {
                    db.Entry(item).State = existingData.Any(g => propertyToMatch(g).Equals(propertyToMatch(item)))
                        ? EntityState.Modified
                        : EntityState.Added;
                }

                await db.SaveChangesAsync();
            }
        }
    }
}
