using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using Sport.Domain.Entities;
using Sport.Service.Abstract;
using Sport.WebUI.Models;
using Sport.WebUI.Models.Security;
using Sport.WebUI.TwoFactorService;

namespace Sport.WebUI.Controllers
{
    public class SecurityController : Controller
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private readonly SessionHelper _sessionHelper;
        private readonly IUserService _userService;
        private readonly IOptions<TwoFactorOptions> _config;


        public SecurityController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            SessionHelper sessionHelper,
            IUserService userService,
            IOptions<TwoFactorOptions> config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _sessionHelper = sessionHelper;
            _userService = userService;
            _config = config;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            string usernmae = _sessionHelper.GetSessionUsername();
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }
            var user = await _userManager.FindByNameAsync(loginViewModel.UserName);
            if (user != null)
            {
                //if (!await _userManager.IsEmailConfirmedAsync(user))
                //{
                //    ModelState.AddModelError(string.Empty, "Confirm Your Email please");
                //    return View(loginViewModel);
                //}
            }
            var result = await _signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, false, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Calculator", "Home");
            }
            ModelState.AddModelError(string.Empty, "Login Failed");
            return View(loginViewModel);
        }



        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Calculator", "Home");
        }



        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        #region RegisterYeni
        //[HttpPost]
        //public async Task<ActionResult> Register(RegisterViewModel registerViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        AppUser user = new AppUser
        //        { 
        //        UserName=registerViewModel.UserName,
        //        Email=registerViewModel.Email
        //        };
        //        IdentityResult result = _userManager.CreateAsync(user, registerViewModel.Password).Result;
        //        if(result.Succeeded)
        //        {
        //            if (!_roleManager.RoleExistsAsync("Admin").Result)
        //            { 
        //                AppRole role = new AppRole
        //                { Name="Admin"};

        //                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
        //                if(!roleResult.Succeeded)
        //                {
        //                    ModelState.AddModelError("", "We can't add the role !");
        //                    return View(registerViewModel);
        //                }
        //            }
        //            _userManager.AddToRoleAsync(user, "Admin").Wait();
        //            return RedirectToAction("Login", "Security");
        //        }
        //    }
        //    return View(registerViewModel);
        //}
        #endregion
        //[HttpPost]
        //public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        //{

        //    if(!ModelState.IsValid)
        //    {
        //        return View(registerViewModel);
        //    }
        //    var user = new AppUser
        //    {
        //        UserName = registerViewModel.UserName,
        //        Email = registerViewModel.Email,
        //        Age = registerViewModel.Age
        //    };
        //    var result = await _userManager.CreateAsync(user, registerViewModel.Password);
        //    if (result.Succeeded)
        //    {
        //        var confirmationCode = _userManager.GenerateEmailConfirmationTokenAsync(user);
        //        var callBackUrl = Url.Action("ConfirmEmail", "Security", new { userId = user.Id, code = confirmationCode.Result });
        //        //Send Email
        //        return RedirectToAction("Calculator", "Home");
        //    }
        //    return View(registerViewModel);
        //}

        [HttpPost]
        public async Task<JsonResult> RegisterPost(RegisterViewModel jsonData)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(jsonData);
            //}

            var user = new AppUser
            {
                UserName = jsonData.UserName,
                Email = jsonData.Email,
                Age = jsonData.Age
            };
            var result = await _userManager.CreateAsync(user, jsonData.Password);
            if (result.Succeeded)
            {
                //var confirmationCode = _userManager.GenerateEmailConfirmationTokenAsync(user);
                //var callBackUrl = Url.Action("ConfirmEmail", "Security", new { userId = user.Id, code = confirmationCode.Result });

                //var url = "http://localhost:59851" + callBackUrl;
                //var appSetting = _config.Value;
                //var client = new SendGridClient(appSetting.SendGrid_ApiKey);
                //var from = new EmailAddress("mehmetuslu37@gmail.com", "uslumehmet");
                //var subject = "Uyelik onayi";
                //var to = new EmailAddress(user.Email, user.NormalizedUserName);
                //var plainTextContent = "FIT OL AILESINE KATILDIGINIZ ICIN MUTLUYUZ";
                //var htmlContent = "<strong>Uyeliginiz onaylanmasi icin </strong>" + "<a href=" + url + "> buraya</a> tiklayiniz.";
                //var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                //var response = await client.SendEmailAsync(msg);

                var thereIs = await _userManager.IsInRoleAsync(user, "User");
                if (thereIs == false)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                }
                return Json(new { status = 1, message = "Kayıt Başarılı...!", redirect = "/Home/Calculator" });
            }
            return Json(new { status = 0, message = "Kayıt Başarısız...!", redirect = "/Security/Register" });
        }


        #region SendMail
        public async void BuildEmailTemplate(string regID, string callBackUrl)
        {
            string username = _sessionHelper.GetSessionUsername();
            var appUser = await _userManager.FindByIdAsync(regID);
            var url = "http://localhost:59851" + callBackUrl;
            string body = url;
            body = body.ToString();
            BuildEmailTemplate("Your Account Is Successfully Created", body, appUser.Email);
        }

        public void BuildEmailTemplate(string subjectText, string bodyText, string sendTo)
        {
            string from, to, bcc, cc, subject, body;
            from = "fatihsozuer0@gmail.com";
            to = sendTo.Trim();
            bcc = "";
            cc = "";
            subject = subjectText;
            StringBuilder sb = new StringBuilder();
            sb.Append(bodyText);
            body = sb.ToString();
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from);
            mail.To.Add(new MailAddress(to));
            if (!string.IsNullOrEmpty(bcc))
            {
                mail.Bcc.Add(new MailAddress(bcc));
            }
            if (!string.IsNullOrEmpty(cc))
            {
                mail.CC.Add(new MailAddress(cc));
            }
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            SendEmail(mail);
        }

        public void SendEmail(MailMessage mail)
        {
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.sendgrid.net";
            client.Port = 465;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential("fatihsozuer0@gmail.com", "9s3832hslhitaf611");
            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region eski
        //public async Task<JsonResult> RegisterPost(RegisterViewModel jsonData)
        //{
        //    //if (!ModelState.IsValid)
        //    //{
        //    //    return View(jsonData);
        //    //}

        //    var user = new AppUser
        //    {
        //        UserName = jsonData.UserName,
        //        Email = jsonData.Email,
        //        Age = jsonData.Age

        //    };
        //    var result = await _userManager.CreateAsync(user, jsonData.Password);

        //    if (result.Succeeded)
        //    {
        //        var confirmationCode = _userManager.GenerateEmailConfirmationTokenAsync(user);
        //        //var callBackUrl = Url.Action("ConfirmEmail", "Security", new { userId = user.Id, code = confirmationCode.Result });
        //        var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = confirmationCode }, protocol: HttpContext.Request.Scheme);
        //        //Send Email
        //        //await _userManager.IsEmailConfirmedAsync(user);
        //        //await _userManager.SetEmailAsync(user, user.Email);
        //        // await _signInManager.SignInAsync(user, isPersistent: false);
        //        //string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
        //        try
        //        {
        //            var client = new SendGridClient(_twoFactorOptions.SenGrid_Apikey);
        //            var from = new EmailAddress("mehmetuslu37@gmail.com");
        //            var subject = "Kayıt Onayı";
        //            var to = new EmailAddress(user.Email);
        //            var htmlContent = $"<h2>Kayıt onayınız için gerekli doğrulama kodu aşağıdadır.</h2><h3>Kodunuz:{callbackUrl}</h3>";
        //            var msg = MailHelper.CreateSingleEmail(from, to, subject, null, htmlContent);
        //            var response = await client.SendEmailAsync(msg);
        //            Console.WriteLine(response);
        //            //Examplesss examplesss = new Examplesss();


        //            return Json(new { status = 1, message = "Kayıt Başarılı...!", redirect = "/Home/Calculator" });
        //        }
        //        catch (Exception e)
        //        {
        //            Console.WriteLine(e.Message);
        //            throw;
        //        }
        //    }
        //    return Json(new { status = 0, message = "Kayıt Başarısız...!", redirect = "/Security/Register" });

        //}

        #endregion

        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction("Calculator", "Home");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException("Unable to fin the user");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                return View("ConfirmEmail");
            }
            return RedirectToAction("Calculator", "Home");
        }



        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return View();
            }
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return View();
            }

            var confirmationCode = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.Action("ResetPassword", "Security", new { userId = user.Id, code = confirmationCode });

            //send callback Url with email

            return RedirectToAction("ForgotPasswordEmailSend");
        }

        public IActionResult ForgotPasswordEmailSend()
        {
            return View();
        }

        public async Task<IActionResult> ResetPassword()
        {
            //if (userId == null || code == null)
            //{
            //    throw new ApplicationException("User Id or Code must be supplied for password reset");
            //}
            string username = _sessionHelper.GetSessionUsername();
            AppUser appUser = await _userService.UserByName(username);
            var code = await _userManager.GeneratePasswordResetTokenAsync(appUser);
            var model = new ResetPasswordViewModel { Code = code };
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> ResetPasswordPost(ResetPasswordViewModel jsondata)
        {

            var user = await _userManager.FindByEmailAsync(jsondata.Email);
            if (user == null)
            {
                throw new ApplicationException("User not found");
            }

            var result = await _userManager.ResetPasswordAsync(user, jsondata.Code, jsondata.Password);
            if (result.Succeeded)
            {
                return Json(new { status = 1, message = "Şifre yenileme işlemi  başarılı...!", redirect = "/Security/Login" });
            }

            return Json(new { status = 0, message = "Şifre yenileme işlemi  başarısız tekrar deneyin...!", redirect = "/Security/ResetPassword" });
        }

        public IActionResult ResetPasswordConfirm()
        {
            return View();
        }


    }

}
