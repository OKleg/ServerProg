using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace RazorAppLab6.Models
{
    [Bind]
    public class Contact
    {
        [BindProperty(Name = "first_name", SupportsGet = true)]
        [Required]
        public string first_name { get; set; }
        [BindProperty(Name = "last_name")]
        [Required]
        public string last_name { get; set; }
        [BindProperty(Name = "email")]
        public string email { get; set; }
        [BindProperty(Name = "phone")]
        public string phone { get; set; }
        [BindProperty(Name = "select_price")]
        public string select_price { get; set; }
        [BindProperty(Name = "select_servi")]
        public string select_service { get; set; }
        [BindProperty(Name = "subject")]
        public string subject { get; set; }
        [BindProperty(Name = "comments")]
        public string comments { get; set; }
        [BindProperty(Name = "verify")]
        public string verify { get; set; }

    }
}
