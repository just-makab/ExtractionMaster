using Microsoft.AspNetCore.Mvc;
using EM_WebApp.Models;
using Microsoft.EntityFrameworkCore;
using EM_WebApp.ViewModels;
using EM_WebApp.Utilities;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Text.RegularExpressions;
using EM_WebApp.Constants;
using EM_WebApp.Utilities.Notification;

namespace EM_WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly EMDbContext _context;
        private readonly IInputSanitizer _inputSanitizer;
        private readonly INotificationService _notificationService;

        public AccountController(EMDbContext context, IInputSanitizer inputSanitizer, INotificationService notificationService)
        {
            _context = context;
            _inputSanitizer = inputSanitizer;
            _notificationService = notificationService;

        }

        // Helper method to validate password strength using regex
        private bool ValidatePassword(string password)
        {
            string pattern = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^\w\s]).{8,}$";
            return Regex.IsMatch(password, pattern);
        }

        // Helper method to validate email format using regex
        private bool ValidateEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        //Helper method ensure users adds country code to allow whatsApp feature to work
        private bool ValidatePhoneNumber(string phoneNumber)
        {
            // Regex to ensure phone number starts with + followed by 1-3 digits (country code) and 6-14 digits
            string pattern = @"^\+\d{1,3}\d{6,14}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }


        // Helper method to check if email already exists in either Customers or Users table
        private async Task<bool> EmailExistsAsync(string email)
        {
            var existingCustomer = await _context.Customers.FirstOrDefaultAsync(u => u.Email == email);
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return existingCustomer != null || existingUser != null;
        }

        // GET: Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: Register
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Sanitize input fields
                model.Email = _inputSanitizer.SanitizeInput(model.Email.ToLower());
                model.FirstName = _inputSanitizer.SanitizeInput(model.FirstName);
                model.LastName = _inputSanitizer.SanitizeInput(model.LastName);
                model.PhoneNumber = _inputSanitizer.SanitizeInput(model.PhoneNumber);
                model.Password = _inputSanitizer.SanitizeInput(model.Password);

                // Validate email format
                if (!ValidateEmail(model.Email))
                {
                    ModelState.AddModelError("Email", "Invalid email format.");
                    return View(model);
                }

                // Check if the email already exists
                if (await EmailExistsAsync(model.Email))
                {
                    ModelState.AddModelError("Email", "Email already in use.");
                    return View(model);
                }

                // Validate phone number format
                if (!ValidatePhoneNumber(model.PhoneNumber))
                {
                    ModelState.AddModelError("PhoneNumber", "Phone number must include a country code (e.g., +1XXXXXXXXXX).");
                    return View(model);
                }

                // Validate password strength
                if (!ValidatePassword(model.Password))
                {
                    ModelState.AddModelError("Password", "Password must be at least 8 characters long and contain an uppercase letter, a number, and a special character.");
                    return View(model);
                }

                // Hash the password using BCrypt
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

                // Create a new customer
                var newCustomer = new Customer
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    PasswordHash = hashedPassword,
                    Role = "Customer"
                };

                _context.Customers.Add(newCustomer);
                await _context.SaveChangesAsync();

                try
                {
                    var welcomeEmailBody = string.Format(NotificationMessages.WelcomeEmail, newCustomer.FirstName);
                    await _notificationService.SendEmailAsync(newCustomer.Email, "Welcome to Extraction Master", welcomeEmailBody);

                    await _notificationService.SendWhatsAppMessageAsync(newCustomer.PhoneNumber, "Thank you for registering with Extraction Master. If you need assistance, feel free to contact us.");
                }
                catch (Exception ex)
                {
                    // Log the error (in real-world, you might want to handle this more gracefully)
                    ModelState.AddModelError("", "Failed to send notification. Please try again later.");
                    return View(model);
                }

                // After successful registration, redirect to Login
                return RedirectToAction("Login");
            }

            return View(model);
        }

        // GET: Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Sanitize input fields
                model.Email = _inputSanitizer.SanitizeInput(model.Email.ToLower());
                model.Password = _inputSanitizer.SanitizeInput(model.Password);

                // Check if the email exists in Users (Admin) table
                var adminUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Role != null);

                if (adminUser != null)
                {
                    // Verify the password
                    bool isPasswordValid = BCrypt.Net.BCrypt.Verify(model.Password, adminUser.PasswordHash);
                    if (!isPasswordValid)
                    {
                        ModelState.AddModelError("", "Invalid login attempt.");
                        return View(model);
                    }

                    // Set claims for admin
                    var adminClaims = new[]
                    {
                        new Claim(ClaimTypes.Name, adminUser.Email),
                        new Claim(ClaimTypes.NameIdentifier, adminUser.UserId.ToString()),
                        new Claim("Role", adminUser.Role) // Admin role
                    };

                    var adminIdentity = new ClaimsIdentity(adminClaims, "login");
                    var adminPrincipal = new ClaimsPrincipal(adminIdentity);

                    // Sign in the admin
                    await HttpContext.SignInAsync(adminPrincipal);

                    return RedirectToAction("AdminDashboard", "Admin");
                }

                // Check if the email exists in Customers (Customer) table
                var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Email == model.Email);

                if (customer != null)
                {
                    // Verify the password
                    bool isPasswordValid = BCrypt.Net.BCrypt.Verify(model.Password, customer.PasswordHash);
                    if (!isPasswordValid)
                    {
                        ModelState.AddModelError("", "Invalid login attempt.");
                        return View(model);
                    }

                    // Set claims for customer
                    var customerClaims = new[]
                    {
                        new Claim(ClaimTypes.Name, customer.Email),
                        new Claim(ClaimTypes.NameIdentifier, customer.CustomerId.ToString()),
                        new Claim("PhoneNumber", customer.PhoneNumber),
                        new Claim("Role", customer.Role ?? "Customer") // Default role is "Customer"
                    };

                    var customerIdentity = new ClaimsIdentity(customerClaims, "login");
                    var customerPrincipal = new ClaimsPrincipal(customerIdentity);

                    // Sign in customer
                    await HttpContext.SignInAsync(customerPrincipal);

                    return RedirectToAction("CustomerDashboard", "Customer");
                }

                // If email is not found in either table
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }

            return View(model);
        }

        // POST: Logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
