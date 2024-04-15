using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorAppLab6.Data;
using RazorAppLab6.Models;
using System.ComponentModel.DataAnnotations;

namespace RazorPages.Pages;

public class TestimonialsModel : PageModel
{
    [BindProperty]
    public Testimonial form { get; set; }
    private readonly TestimonialContext context;
    public string MessageWorning { get; private set; } = "";
    public string MessageSuccess { get; private set; }
    public TestimonialsModel(TestimonialContext context) => this.context = context;

    public List<Testimonial> Testimonials { get; set; } = new();
    public async Task OnGetAsync()
    {
        //form = await context.Testimonials.FindAsync(Id);
        Testimonials = await context.Testimonials.ToListAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        Console.WriteLine("/-/-/ TESTIMONIAL OnPostAsync /-/-/");
        
        MessageSuccess =
        "<fieldset>"
            + "<div id='success_page'>"
            + "<h1>Email Sent Successfully.</h1>"
            + $"<p>Thank you <strong>{form.Name}</strong>,"
            + "Testimonial Published"
            + "</div>"
            + "</fieldset>";;

        MessageWorning = "";
        if (form.Name == null || form.Name.Trim(' ') == "")
        {
            MessageWorning += "<div class=\"error_message\">\"Attention! You must enter your name\"</div>";
        }
        else if (form.Ocupation == null || form.Ocupation.Trim(' ') == "")
        {
            MessageWorning += "<div class=\"error_message\">\"Attention! Please enter a valid Ocupation.\"</div>";
        }
        else if (form.Title == null || form.Title.Trim(' ') == "")
        {
            MessageWorning += "<div class=\"error_message\">\"Attention! Please enter a valid Title.\"</div>";
        }
        else if (form.Description == null || form.Description.Trim(' ') == "")
        {
            MessageWorning += "<div class=\"error_message\">\"Attention! Please enter a valid Description.\"</div>";
        }
        if (MessageWorning != "")
        {
            return Page();
            //return Content(MessageWorning);
        }
        else
        {
            if (form != null)
                context.Testimonials.Add(form);
            context.SaveChanges();/*
            Console.WriteLine(context.Testimonials.);*/
        }
        Console.WriteLine(form);
        Testimonials = await context.Testimonials.ToListAsync();
        return Page();
        //return Content(MessageSuccess);
    }
}

    

