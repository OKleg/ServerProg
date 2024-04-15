using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Net.Http.Json;
using System.IO;
using RazorAppLab6.Data;
using RazorAppLab6.Models;
using Microsoft.EntityFrameworkCore;

namespace RazorPages.Pages;

    public class LCards
    {
        public List<Card> Cards { get; set; }
    }
    public class Card
    {
        public string Cat { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string ImgSrc { get; set; }
    }
    public interface IPortfolioContentService
    {
        public List<Card> ReadJson(string path); 
    }
    public class PortfolioContentService: IPortfolioContentService
    {
        public List<Card> ReadJson(string path) 
        {
            string jsonString = File.ReadAllText(path);
            var card = JsonSerializer.Deserialize<LCards>(jsonString)!;
            return  card.Cards;
           
        }
    }
public class PortfolioModel : PageModel
{
    public List<Card> cards;
    public readonly IPortfolioContentService contentService;
    private readonly string contentSrc = "wwwroot/portfolio.json";
    private readonly TestimonialContext context;
    public List<Testimonial> Testimonials { get; set; } = new();
    public PortfolioModel(IPortfolioContentService _contentService)
    {
        contentService = _contentService;
        cards = contentService.ReadJson(contentSrc);
        this.context = context;

    }

    public async Task OnGetAsync() =>
        Testimonials = await context.Testimonials.ToListAsync();
}

