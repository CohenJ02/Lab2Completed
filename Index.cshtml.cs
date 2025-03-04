using Lab1.Pages.DataClasses;
using Lab1.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;

namespace Lab1.Pages.GrantWork
{
    public class IndexModel : PageModel
    {
        public List<Grants> GrantInfo { get; set; }
        [BindProperty]
        public string email { get; set; }
        [BindProperty]
        public string password { get; set; }

        public IndexModel()
        {
            GrantInfo = new List<Grants>();
        }
        public void OnGet()
        {
            SqlDataReader productReader = DBClass.ProductReader();
            while (productReader.Read())
            {
                GrantInfo.Add(new Grants
                {
                    Title = productReader["Title"].ToString(),
                    Category = productReader["Category"].ToString(),
                    FundingAgency = productReader["FundingAgency"].ToString(),
                    Status = productReader["Status"].ToString()
                });
            }
            DBClass.Lab1DBConnection.Close();
        }

        public IActionResult OnPost()
        {
            if (DBClass.StoredProcedureLogin(email, password))
            {
                HttpContext.Session.SetString("Email", email);
                ViewData["Email"] = email;
                DBClass.Lab1DBConnection.Close();
                return Page();
            }
            else
            {
                DBClass.Lab1DBConnection.Close();
                return RedirectToPage("/Index");
            }
        }
    }
}
