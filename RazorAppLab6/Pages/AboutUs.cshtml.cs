using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RazorAppLab6.Data;
using RazorAppLab6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPages.Pages;
    public class AboutUsModel : PageModel
    {
        private readonly ILogger<AboutUsModel> _logger;
    private readonly TestimonialContext context;
    public List<Testimonial> Testimonials { get; set; } = new();

    public AboutUsModel(ILogger<AboutUsModel> logger)
        {
            _logger = logger;
            this.context = context;
    }
    public async Task OnGetAsync() =>
        Testimonials = await context.Testimonials.ToListAsync();
}

