using RazorPages.Pages;
using RazorAppLab6.Data;
using Microsoft.EntityFrameworkCore;
using RazorAppLab6.Models;

namespace RazorPages
{
    public class Program
    {
        public static void PopulateDB()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var options = new DbContextOptionsBuilder<TestimonialContext>()
                .UseNpgsql(config.GetConnectionString("Default"))
                .Options;

            using (var context = new TestimonialContext(options))
            {
                context.Database.EnsureCreated();

                /*context.Testimonials.AddRange(
                new Testimonial[]{
                    new Testimonial
                    {
                        //Id = 1,
                        Name = "James Fernando ",
                        Img_url = "uploads/testi_01.png",
                        Ocupation = "Manager of Racer",
                        Title = "Wonderful Support!",
                        Description = "They have got my project on time with the competition with a sed highly skilled, and experienced & professional team."
                    },
                    new Testimonial
                    {
                        //Id = 2,
                        Name = "Jacques Philips ",
                        Img_url = "uploads/testi_02.png",
                        Ocupation = "- Designer",
                        Title = " Awesome Services!",
                        Description = "Explain to you how all this mistaken idea of denouncing pleasure and praising pain was born and I will give you completed."
                    },
                    new Testimonial
                    {
                        //Id = 3,
                        Name = "Venanda Mercy ",
                        Img_url = "uploads/testi_03.png",
                        Ocupation = "- Newyork City",
                        Title = " Great & Talented Team!",
                        Description = "The master-builder of human happines no one rejects, dislikes avoids pleasure itself, because it is very pursue pleasure. "
                    },
                    new Testimonial
                    {
                        //Id = 4,
                        Name = "James Fernando ",
                        Img_url = "uploads/testi_01.png",
                        Ocupation = "- Manager of Racer",
                        Title = " Wonderful Support!",
                        Description = "They have got my project on time with the competition with a sed highly skilled, and experienced & professional team."
                    },
                    new Testimonial
                    {
                        //Id = 5,
                        Name = "Venanda Mercy ",
                        Img_url = "uploads / testi_03.png",
                        Ocupation = "- Newyork City",
                        Title = " Great & Talented Team!",
                        Description = "The master-builder of human happines no one rejects, dislikes avoids pleasure itself, because it is very pursue pleasure. "
                    },
                    new Testimonial
                    {
                       // Id = 6,
                        Name = "Jacques Philips",
                        Img_url = "uploads/testi_02.png",
                        Ocupation = "- Designer",
                        Title = " Awesome Services!",
                        Description = "Explain to you how all this mistaken idea of denouncing pleasure and praising pain was born and I will give you completed."
                    }
                 }
                ); */
           

                
                //context.Database.Migrate();

                /*var cnt = new Testimonial("James Fernando",
                    "uploads/testi_01.png",
                    "Manager of Racer",
                    "Wonderful Support!",
                    "They have got my project on time with the competition with a sed highly skilled, and experienced & professional team."
                    );*/

                //context.Òestimonials.Add(cnt);
                context.SaveChanges();
                foreach (var s in context.Testimonials)
                {
                    Console.WriteLine($"Id: {s.Id}\n" +
                        $" {s.Name} {s.Ocupation} " +
                        $"{s.Title} " +
                        $"{s.Img_url} " +
                        $"{s.Description}");
                }
            }
        }

        public static void Main(string[] args)
        {
            PopulateDB();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddNpgsql<TestimonialContext>(
                builder.Configuration.GetConnectionString("Default")
                );
            builder.Services.AddScoped<IPortfolioContentService, PortfolioContentService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}