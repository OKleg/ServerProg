using Microsoft.EntityFrameworkCore;
using RazorAppLab6.Models;

namespace RazorAppLab6.Data
{
    public static class ModelBuilderExtensions
    {
        public static ModelBuilder Seed(this ModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<Testimonial>().HasData(
            new Testimonial
            {
                Id = 1,
                Name = "James Fernando ",
                Img_url = "uploads/testi_01.png",
                Ocupation= "Manager of Racer",
                Title= "Wonderful Support!",
                Description = "They have got my project on time with the competition with a sed highly skilled, and experienced & professional team."
            },
            new Testimonial
            {
                Id = 2,
                Name = "Jacques Philips ",
                Img_url = "uploads/testi_02.png",
                Ocupation = "- Designer",
                Title = " Awesome Services!",
                Description = "Explain to you how all this mistaken idea of denouncing pleasure and praising pain was born and I will give you completed."
            },
            new Testimonial
            {
                Id = 3,
                Name = "Venanda Mercy ",
                Img_url = "uploads/testi_03.png",
                Ocupation = "- Newyork City",
                Title = " Great & Talented Team!",
                Description = "The master-builder of human happines no one rejects, dislikes avoids pleasure itself, because it is very pursue pleasure. "
            },
            new Testimonial
            {
                Id = 4,
                Name = "James Fernando ",
                Img_url = "uploads/testi_01.png",
                Ocupation = "- Manager of Racer",
                Title = " Wonderful Support!",
                Description = "They have got my project on time with the competition with a sed highly skilled, and experienced & professional team."
            },
            new Testimonial
            {
                Id = 5,
                Name = "Venanda Mercy ",
                Img_url = "uploads / testi_03.png",
                Ocupation = "- Newyork City",
                Title = " Great & Talented Team!",
                Description = "The master-builder of human happines no one rejects, dislikes avoids pleasure itself, because it is very pursue pleasure. "
            },
            new Testimonial
            {
                Id = 6,
                Name = "Jacques Philips",
                Img_url = "uploads/testi_02.png",
                Ocupation = "- Designer",
                Title = " Awesome Services!",
                Description = "Explain to you how all this mistaken idea of denouncing pleasure and praising pain was born and I will give you completed."
            }
           );*/
            return modelBuilder;
        }
    }
}
