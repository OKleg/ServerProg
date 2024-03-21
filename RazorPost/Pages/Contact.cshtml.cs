using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;
using CsvHelper.Configuration.Attributes;
using CsvHelper;
using System.IO;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace RazorPages.Pages
{
    [Bind]
    public class Contact
    {
        [BindProperty(Name="first_name",SupportsGet =true)]
        [Required]
        public string first_name { get; set; }
        [BindProperty(Name = "last_name")]
        [Required]
        public  string last_name { get; set; }
        [BindProperty(Name = "email")]
        public  string email { get; set; }
        [BindProperty(Name = "phone")]
        public  string phone { get; set; }
        [BindProperty(Name = "select_price")]
        public  string select_price { get; set; }
        [BindProperty(Name = "select_servi")]
        public  string select_service { get; set; }
        [BindProperty(Name = "subject")]
        public  string subject { get; set; }
        [BindProperty(Name = "comments")]
        public  string comments { get; set; }
        [BindProperty(Name = "verify")]
        public  string verify { get; set; }

    }


    [IgnoreAntiforgeryToken]
    public class ContactModel : PageModel
    {
        public Contact ContactData { get;  set; } = default(Contact);

        public string MessageWorning { get; private set; } = "";
        public string MessageSuccess { get; private set; } = "";

        bool isEmail(string email)
        {
            return (Regex.IsMatch(email, @"/^[-_.[:alnum:]]+@((([[:alnum:]]|[[:alnum:]][[:alnum:]-]*[[:alnum:]])\.)+(ad|ae|aero|af|ag|ai|al|am|an|ao|aq|ar|arpa|as|at|au|aw|az|ba|bb|bd|be|bf|bg|bh|bi|biz|bj|bm|bn|bo|br|bs|bt|bv|bw|by|bz|ca|cc|cd|cf|cg|ch|ci|ck|cl|cm|cn|co|com|coop|cr|cs|cu|cv|cx|cy|cz|de|dj|dk|dm|do|dz|ec|edu|ee|eg|eh|er|es|et|eu|fi|fj|fk|fm|fo|fr|ga|gb|gd|ge|gf|gh|gi|gl|gm|gn|gov|gp|gq|gr|gs|gt|gu|gw|gy|hk|hm|hn|hr|ht|hu|id|ie|il|in|info|int|io|iq|ir|is|it|jm|jo|jp|ke|kg|kh|ki|km|kn|kp|kr|kw|ky|kz|la|lb|lc|li|lk|lr|ls|lt|lu|lv|ly|ma|mc|md|me|mg|mh|mil|mk|ml|mm|mn|mo|mp|mq|mr|ms|mt|mu|museum|mv|mw|mx|my|mz|na|name|nc|ne|net|nf|ng|ni|nl|no|np|nr|nt|nu|nz|om|org|pa|pe|pf|pg|ph|pk|pl|pm|pn|pr|pro|ps|pt|pw|py|qa|re|ro|ru|rw|sa|sb|sc|sd|se|sg|sh|si|sj|sk|sl|sm|sn|so|sr|st|su|sv|sy|sz|tc|td|tf|tg|th|tj|tk|tm|tn|to|tp|tr|tt|tv|tw|tz|ua|ug|uk|um|us|uy|uz|va|vc|ve|vg|vi|vn|vu|wf|ws|ye|yt|yu|za|zm|zw)$|(([0-9][0-9]?|[0-1][0-9][0-9]|[2][0-4][0-9]|[2][5][0-5])\.){3}([0-9][0-9]?|[0-1][0-9][0-9]|[2][0-4][0-9]|[2][5][0-5]))$/i"));
        }
        public void OnGet()
        {
            MessageWorning = "";
        }
        [HttpPost]
        public IActionResult OnPost(
         string first_name,
         string last_name,
         string email,
         string phone,
         string select_price,
         string select_service,
         string subject,
         string comments,
         string verify
            )
        {
            
        
            MessageWorning = "";
            if (first_name == null || first_name.Trim(' ') == "")
            {
                MessageWorning+= "<div class=\"error_message\">\"Attention! You must enter your name\"</div>";

            }
            else if (email == null || email.Trim(' ') == "")
            {
                MessageWorning+= "<div class=\"error_message\">\"Attention! Please enter a valid email address.\"</div>";
            }
            else if (!isEmail(email))
            {
                MessageWorning+= "<div class=\"error_message\">\"Attention! You have enter an invalid e-mail address, try again.\"</div>";
            }

            if (comments == null || comments.Trim(' ') == " ")
            {
                MessageWorning+= "<div class=\"error_message\">\"Attention! Please enter your message.\"</div>";
            }
           
            using var fs = new FileStream("file.csv", FileMode.Append);
            using (var writer = new StreamWriter(fs))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                if (fs.Length == 0)
                {
                    csv.WriteHeader<Contact>();
                    csv.NextRecord();
                }
                //
                    csv.WriteRecord(ContactData);
                    csv.NextRecord();
                //
            }
            
            return Content(MessageWorning);
        }
    }
}
