namespace ProjectDAW.Migrations
{
    using ProjectDAW.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Models.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;

            ContextKey = "DefaultMigration";
        }

        protected override void Seed(Models.AppDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            for(int i = 0; i < 10; i++)
            {
                var category = new TblCategory
                {
                    Name = $"DAW{i}",
                    Description = "A place to discuss DAW project",
                    CategoryId = i
                };


                context.TblCategory.AddOrUpdate(category);
                context.SaveChanges();

                var post = new TblPost
                {
                    CategoryId = category.CategoryId,
                    Title = $"project {i} progress update",
                    Content = $"Started The project {i}",
                    CreatedDate = DateTime.Now,
                    PostId = i
                };

                context.TblPost.AddOrUpdate(post);
                context.SaveChanges();
            }

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
