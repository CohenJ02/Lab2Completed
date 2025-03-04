using System.ComponentModel.DataAnnotations;
using Microsoft.Data.SqlClient;
using Lab1.Pages.DataClasses;
using Lab1.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab1.Pages.User
{
    public class CreateUserModel : PageModel
    {
        public List<Users> EmployeeInfo { get; set; }
        public List<Faculty> FacultyInfo { get; set; }
        [BindProperty]
        public Users NewUsers { get; set; }
        public Faculty NewFaculty { get; set; }
        public string FacultyType { get; set; }
        public string SuccessMessage { get; set; }
        public CreateUserModel() {
            EmployeeInfo = new List<Users>();
            FacultyInfo = new List<Faculty>();
                }
        public void OnGet()
        {
            SqlDataReader productReader = DBClass.UserReader();
            while(productReader.Read())
            {
                EmployeeInfo.Add(new Users
                {
                    Name = productReader["Name"].ToString(),
                    Email = productReader["Email"].ToString(),
                    Password = productReader["Password"].ToString(),
                    postion = productReader["position"].ToString(),
                    Role = productReader["Role"].ToString()
                });

            }
            DBClass.Lab1DBConnection.Close();
        }

        public IActionResult OnPost()
        {
            DBClass.InsertUser(NewUsers, NewFaculty);
            DBClass.Lab1DBConnection.Close();

            TempData["SuccessMessage"] = "User has been created successfully!";
            return RedirectToPage("Dashboard");
        }
    }
}