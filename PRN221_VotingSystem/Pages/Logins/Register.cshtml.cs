using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_VotingSystem.DAL;
using PRN221_VotingSystem.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace PRN221_VotingSystem.Pages
{
    public class Register : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Please fill out this field")]
        public string Username { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [BindProperty]
        [Compare("Password", ErrorMessage = "Password does not match")]
        public string RePassword { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please fill out this field")]
        public string FirstName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please fill out this field")]
        public string LastName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please fill out this field")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please fill out this field")]
        public string Job { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please fill out this field")]
        public string Address { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please fill out this field")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please fill out this field")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please fill out this field")]
        public string Sex { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (DateOfBirth > DateTime.Now)
            {
                ModelState.AddModelError("DateOfBirth", "Invalid Date of Birth.");
                return Page();
            }

            var listEmail = AccountDAO.Instance.GetAll().Select(account => account.Email).ToList();

            if (listEmail.Contains(Email))
            {
                ModelState.AddModelError("Email", "Email existed");
                return Page();
            }

            var listUserName = AccountDAO.Instance.GetAll().Select(account => account.Username).ToList();

            if (listUserName.Contains(Username))
            {
                ModelState.AddModelError("Username", "Username existed");
                return Page();
            }

            if (Password != RePassword)
            {
                ModelState.AddModelError("RePassword", "Password does not match");
                return Page();
            }

            var newUser = new Account()
            {
                FirstName = this.FirstName, 
                LastName = this.LastName, 
                Email = this.Email,
                Username = this.Username,
                Password = this.Password,
                Address = this.Address,
                Dob = DateOfBirth,
                Job = this.Job,
                Gender = this.Sex,
                Phone = this.PhoneNumber,
                IsMod = 0,
                IsAdmin = 0,
                IsActive = 1,
                Salt = "abc"
            };

            HttpContext.Session.SetString("registerUser", JsonSerializer.Serialize(newUser));
            return RedirectToPage("/Logins/OTPRegister", new { email = Email });
        }
    }
}
