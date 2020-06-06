using OlxProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OlxProject.Controllers
{
    public class AccountController : Controller
    {
        // Return Login View
        public ActionResult Login()
        {
            if (!string.IsNullOrEmpty(User.Identity.Name))
            {
                if (User.IsInRole("admin"))
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return RedirectToAction("Index", "Home");

                }
            }
            else
            {
                return View("Login");
            }


          


        }
        //public ActionResult Login()
        //{

        //        return View();



        //}
        //For Login Action Method
        [HttpPost]
        public ActionResult Login(Models.Membership model)
        {
            using (var db = new OlxDbEntities())
            {

                bool IsTrue = db.Users.Any(x => x.Email == model.Email);
                if (IsTrue)
                {
                    bool IsValid = db.Users.Any(x => x.Email == model.Email && x.Password == model.Password && x.EmailVerification==true);

                    if (IsValid)
                    {

                        var result = (from user in db.Users
                                      join role in db.UserRoles on user.Id equals role.UserId
                                      where user.Email == model.Email
                                      select role.Role).ToArray();


                        if (result.Contains("admin"))
                        {
                            FormsAuthentication.SetAuthCookie(model.Email, false);
                            return RedirectToAction("Index", "admin");

                        }
                        else
                        {
                            FormsAuthentication.SetAuthCookie(model.Email, false);
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid information plz Try again");

                        return View();
                    }

                }
                else
                {
                    ModelState.AddModelError("", "Account Does not exist");

                    return View();
                }

            }


        
  }
        //Return Signup View
        public ActionResult SignUp()
        {
            //return View();
            return View();
        }

        //For Sign Up User
        [HttpPost]
        public ActionResult SignUp(UserViewModel model)
        {
            //if(model.File==null)
            //{
            //    ModelState.AddModelError("", "Must upload File");

            //    return View();
            //}


            User user = new User();


            //string path = Path.Combine(Server.MapPath("~/Data/User"), Path.GetFileName(model.File.FileName));
            //model.File.SaveAs(path);
            user.Email = model.Email;
            user.Password = model.Password;
            user.Contact = model.Contact;
            //user.Image = path;
            user.Info = model.Info;
            user.ActivationCode= Guid.NewGuid().ToString();
            user.EmailVerification = false;
            UserRole userRole = new UserRole();
            userRole.Role = "user";
            userRole.UserId = model.Id;

            using (var context = new OlxDbEntities())
            {

                bool IsTrue = context.Users.Any(x => x.Email == model.Email);
                if (!IsTrue)
                {
                    context.Users.Add(user);
                    context.UserRoles.Add(userRole);

                    context.SaveChanges();

                    SendEmailToUser(user.Email, user.ActivationCode);

                    var message = "Registration Completed.  Please check you Email...." + user.Email;

                    ViewBag.Message = message;

                    return View("Registration");

                }
                else
                {
                    ModelState.AddModelError("", "Email Already Exist");

                    return View();
                }
            }

        }
        //for Sending Email to user
        public void SendEmailToUser(string emailId, string activationCode)
        {
            var GenarateUserVerificationLink = "/Account/UserVerification/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, GenarateUserVerificationLink);

            var fromMail = new MailAddress("junaid.ahmedse0@gmail.com"); // set your email  
            var fromEmailpassword = "Jcollection@2890"; // Set your password   
            var toEmail = new MailAddress(emailId);

            var smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(fromMail.Address, fromEmailpassword);

            var Message = new MailMessage(fromMail, toEmail);
            Message.Subject = "Registration Completed-Demo";
            Message.Body = "<br/> Your registration completed succesfully." +
                           "<br/> please click on the below link for account verification" +
                           "<br/><br/><a href=" + link + ">" + link + "</a>";
            Message.IsBodyHtml = true;
            smtp.Send(Message);
        }
        //For user Verification Code
        public ActionResult UserVerification(string id)
        {
            bool Status = false;
            using (var db=new OlxDbEntities())
            {
                db.Configuration.ValidateOnSaveEnabled = false; // Ignor to password confirmation   
                var IsVerify = db.Users.Where(u => u.ActivationCode == new Guid(id).ToString()).FirstOrDefault();

                if (IsVerify != null)
                {
                    IsVerify.EmailVerification = true;
                    db.SaveChanges();
                    ViewBag.Message = "Email Verification completed";
                    Status = true;
                }
                else
                {
                    ViewBag.Message = "Invalid Request...Email not verify";
                    ViewBag.Status = false;
                }

                return View();
            }
           
        }

        //For Logout Action
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}