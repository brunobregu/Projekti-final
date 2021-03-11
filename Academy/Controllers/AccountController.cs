using Academy.DataLayer;
using Academy.DomainModels;
using Academy.Extensions;
using Academy.ServiceInterface;
using Academy.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Academy.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AccountController : Controller
    {
        private readonly IUserService userService;
        public AccountController(IUserService srv)
        {
            this.userService = srv;
        }


        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }


        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM lvm)
        {
            if (ModelState.IsValid)
            {
                var appDbContext = new ProjectDbContext();
                var userStore = new ApplicationUserStore(appDbContext);
                var userManager = new ApplicationUserManager(userStore);

                var user = userManager.Find(lvm.Username, lvm.Password);
                if (user != null)
                {
                    if(user.Enable == true)
                    {
                        //login
                        var authenticationManager = HttpContext.GetOwinContext().Authentication;
                        var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                        authenticationManager.SignIn(new AuthenticationProperties(), userIdentity);
                        return Redirect("/Home/Index");
                    }
                    else
                    {
                        this.AddNotification("Perdoruesi nuk eshte aktiv. Kontaktoni administratorin", NotificationType.ERROR);
                    }
                    
                }
                else
                {
                    this.AddNotification("Passwordi ose username nuk eshte i sakte.", NotificationType.ERROR);
                }
            }
            return View(lvm);

        }


        [Authorize]
        public ActionResult Logout()
        {
            var authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var passwordHash = Crypto.HashPassword(model.Password);
                ApplicationUser user = new ApplicationUser()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.Username,
                    Email = model.Email,
                    PasswordHash = passwordHash
                };
                if (userService.CreateUser(user, "Journalist"))
                {
                    this.AddNotification("Perdoruesi u shtua me sukses .", NotificationType.SUCCESS);

                    return RedirectToAction("AllEnableUser");
                }
                else
                {
                    this.AddNotification("Nje perdorues me te njejtin username ose email egziston.", NotificationType.WARNING);
                    return View(model);
                }
            }
            return View(model);
        }



        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var user = userService.GetUserById(id);
            EditUserVM editUserVM = new EditUserVM()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.UserName
            };
            return View(editUserVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditUserVM model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName = model.Username,
                    Id = model.Id
                };
                if (userService.UpdateUser(user))
                {
                    this.AddNotification("Modifikimi u realizua me sukses .", NotificationType.SUCCESS);
                    return RedirectToAction("AllEnableUser");
                }
                else
                {
                    this.AddNotification("Nje perdorues me te njejtin username ose email egziston.", NotificationType.INFO);
                    return View(model);
                }

            }
            return View(model);
        }



        public ActionResult DisableUser(string id)
        {
            userService.DisableUser(id);
            return RedirectToAction("AllEnableUser");
        }



        public ActionResult EnableUser(string id)
        {
            userService.EnableUser(id);
            return RedirectToAction("AllDisableUser");
        }


        public ActionResult AllEnableUser()
        {
            var userId = User.Identity.GetUserId();
            List<ApplicationUser> users = userService.GetAllEnableUsers(userId);
           
            return View(users);
        }



        public ActionResult AllDisableUser()
        {
            var userId = User.Identity.GetUserId();
            List<ApplicationUser> users = userService.GetAllDisableUsers(userId);

            return View(users);
        }
    }
}