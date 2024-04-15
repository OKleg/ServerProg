using Microsoft.EntityFrameworkCore;
using RazorAppLab6.Models;
using System.Diagnostics;

namespace RazorAppLab6.Data;

public class TestimonialContext : DbContext
{
    public TestimonialContext(DbContextOptions<TestimonialContext> options) : base(options) { }

    public DbSet<Testimonial> Testimonials { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var opt = optionsBuilder.UseNpgsql(@"Host=localhost;Username=postgres;Password=root;Database=splab6");
        Console.WriteLine("/-/-/ OnConfiguring Log BEGIN /-/-/");
        opt.LogTo(Console.WriteLine);
        Console.WriteLine("/-/-/ OnConfiguring Log END   /-/-/");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Console.WriteLine("/// OnModelCreating Log BEGIN ///");
        //modelBuilder.ApplyConfiguration(new TestimonialConfiguration()).Seed();

        modelBuilder.Entity<Testimonial>().ToTable("testimonials");
        //modelBuilder.Entity<Contact>().ToTable("contacts");

        Console.WriteLine("/// OnModelCreating Log END   ///");

    }
}
