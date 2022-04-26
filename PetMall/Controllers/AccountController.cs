using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetMall.Models;
using PetMall.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PetMall.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser user = new AppUser
            {
                FullName = register.FullName,
                UserName = register.UserName,
                Email = register.Email
            };

            if (!register.Terms)
            {
                ModelState.AddModelError("Terms", "Please check this field to scam you");
                return View();
            }

            IdentityResult result = await _userManager.CreateAsync(user, register.Password);

            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View();
            }
            await _signInManager.SignInAsync(user, isPersistent: false);
            await _userManager.AddToRoleAsync(user, "Member");

            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            string link = Url.Action(nameof(VerifyEmail), "Account", new { email = user.Email,token }, Request.Scheme, Request.Host.ToString());

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("vusal.bagirov.1501@gmail.com", "PetMall company");
            mail.To.Add(new MailAddress(user.Email));
            mail.Subject = "Verify Email";
            string body = string.Empty;
            using(StreamReader reader = new StreamReader("wwwroot/assets/template/verifyemail.html"))
            {
                body = reader.ReadToEnd();
            }

            string about = $"Welcome {user.FullName} to our company, please click the link in below to verify your account";

            body = body.Replace("{{link}}", link);
            mail.Body = body.Replace("{{About}}", about);
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("vusal.bagirov.1501@gmail.com", "samurai warriors 2");
            smtp.Send(mail);
            TempData["Verify"] = true;

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> VerifyEmail(string email, string token)
        {
            AppUser user = await _userManager.FindByEmailAsync(email);

            if(user == null)
            {
                return BadRequest();
            }

            await _userManager.ConfirmEmailAsync(user, token);
            await _signInManager.SignInAsync(user, true);
            TempData["Verified"] = true;

            return RedirectToAction("index", "home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser user = await _userManager.FindByNameAsync(login.UserName);

            if(user == null)
            {
                ModelState.AddModelError("", "UserName or password is incorrect");
                return View();
            }

            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, login.Remember, true);

            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", "Get 5 dəqiqədən sonra gələrsən");
                    return View();
                }

                ModelState.AddModelError("", "UserName or password is incorrect");
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(AccountVM account)
        {
            AppUser user = await _userManager.FindByEmailAsync(account.AppUser.Email);
            
            if(user == null)
            {
                return BadRequest();
            }




            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            string link = Url.Action(nameof(ResetPassword), "Account", new { email = user.Email,token }, Request.Scheme, Request.Host.ToString());

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("vusal.bagirov.1501@gmail.com", "Vusal");
            mail.To.Add(new MailAddress(user.Email));
            mail.Subject = "Reset Password";
            mail.Body = $"<a href='{link}'>Please click here to reset your password</a>";
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("vusal.bagirov.1501@gmail.com", "samurai warriors 2");
            smtp.Send(mail);

            return RedirectToAction("index", "home");
        }

        public async Task<IActionResult> ResetPassword(string email, string token)
        {
            AppUser user = await _userManager.FindByEmailAsync(email);

            if(user == null)
            {
                return BadRequest();
            }

            AccountVM model = new AccountVM
            {
                AppUser = user,
                Token = token
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(AccountVM account)
        {
            AppUser user = await _userManager.FindByEmailAsync(account.AppUser.Email);

            if (user == null)
            {
                return BadRequest();
            }

            AccountVM model = new AccountVM
            {
                AppUser = user,
                Token = account.Token
            };

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            IdentityResult result = await _userManager.ResetPasswordAsync(user, account.Token, account.Password);

            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }

            return RedirectToAction("index", "home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("index", "home");
        }

        [Authorize]
        public async Task<IActionResult> Edit()
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            UserEditVM editUser = new UserEditVM
            {
                UserName = user.UserName,
                FullName = user.FullName,
                Email = user.Email
            };

            return View(editUser);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserEditVM userEdit)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            UserEditVM eUser = new UserEditVM
            {
                UserName = user.UserName,
                FullName = user.FullName,
                Email = user.Email
            };

            if(user.UserName != userEdit.UserName && await _userManager.FindByNameAsync(userEdit.UserName) != null)
            {
                ModelState.AddModelError("", $"{userEdit.UserName} has already taken");
                return View(eUser);
            }

            if (string.IsNullOrWhiteSpace(userEdit.CurrentPassword))
            {
                user.UserName = userEdit.UserName;
                user.FullName = userEdit.FullName;
                user.Email = userEdit.Email;
                await _userManager.UpdateAsync(user);
            }
            else
            {
                user.UserName = userEdit.UserName;
                user.FullName = userEdit.FullName;
                user.Email = userEdit.Email;
                IdentityResult result = await _userManager.ChangePasswordAsync(user, userEdit.CurrentPassword, userEdit.Password);

                if (!result.Succeeded)
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View(eUser);
                }
            }

            if (string.IsNullOrWhiteSpace(userEdit.Password))
            {
                user.UserName = userEdit.UserName;
                user.FullName = userEdit.FullName;
                user.Email = userEdit.Email;
                await _userManager.UpdateAsync(user);
            }
            else
            {
                user.UserName = userEdit.UserName;
                user.FullName = userEdit.FullName;
                user.Email = userEdit.Email;
                IdentityResult result = await _userManager.ChangePasswordAsync(user, userEdit.CurrentPassword, userEdit.Password);

                if (!result.Succeeded)
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View(eUser);
                }
            }

            if (string.IsNullOrWhiteSpace(userEdit.ConfirmPassword))
            {
                user.UserName = userEdit.UserName;
                user.FullName = userEdit.FullName;
                user.Email = userEdit.Email;
                await _userManager.UpdateAsync(user);
            }
            else
            {
                user.UserName = userEdit.UserName;
                user.FullName = userEdit.FullName;
                user.Email = userEdit.Email;
                IdentityResult result = await _userManager.ChangePasswordAsync(user, userEdit.CurrentPassword, userEdit.Password);

                if (!result.Succeeded)
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View(eUser);
                }
            }

            await _signInManager.PasswordSignInAsync(user, userEdit.Password, true, true);

            return RedirectToAction("index", "home");
        }

        public IActionResult Show()
        {
            return Content(User.Identity.IsAuthenticated.ToString());
        }
    }
}