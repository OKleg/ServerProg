using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorAppLab6.Data;
using RazorAppLab6.Models;

namespace RazorPages.Pages;

    public class ServicesModel : PageModel
    {
    private readonly TestimonialContext context;

    public ServicesModel(TestimonialContext context) => this.context = context;

    public List<Testimonial> Testimonials { get; set; } = new();
    public async Task OnGetAsync() =>
        Testimonials = await context.Testimonials.ToListAsync();
}

