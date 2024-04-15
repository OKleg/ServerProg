using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace RazorAppLab6.Models;
/// <summary>
/// name, img_url, ocupation, title, description
/// </summary>
[Bind]
public class Testimonial
{
    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }
    [BindProperty, Required, Display(Name = "Name")]
    public string Name { get; set; }
    [BindProperty, Required, Display(Name = "Img_url")]
    public string Img_url { get; set; }
    [BindProperty, Required, Display(Name = "Ocupation")]
    public string Ocupation { get; set; }
    [BindProperty, Required, Display(Name = "Title")]
    public string Title { get; set; }
    [BindProperty, Required, Display(Name = "Description")]
    public string Description { get; set; }
}

