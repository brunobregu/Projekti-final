using Academy.DomainModels;
using Academy.Extensions;
using Academy.ServiceInterface;
using Academy.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Academy.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        public CategoryController(ICategoryService categorysrv)
        {
            categoryService = categorysrv;
        }


        [AllowAnonymous]
        public ActionResult Menu()
        {
            var allcategory = categoryService.GetAllCategories();
            ViewBag.Category = allcategory;
            return PartialView("_Menu");
        }


        public ActionResult Create()
        {
            return View();
        }


        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddCategory model)
        {
            var userId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                Category newCategory = new Category()
                {
                    Name = model.Name,
                    UserId = userId
                };
                if (categoryService.CreateCategory(newCategory))
                {
                    this.AddNotification("Kategoria u shtua me sukses .", NotificationType.SUCCESS);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    this.AddNotification("Nje kategori me te njejtin emer egziston. Ju lutem zgjidhni nje kategori tjeter .", NotificationType.WARNING);
                    return View(model);
                }
            }
            return View(model);
        }



        public ActionResult Edit(int id)
        {
            var category = categoryService.GetCategoryById(id);
            if(category == null)
            {
                return HttpNotFound();
            }
            AddCategory editCategory = new AddCategory()
            {
                Name = category.Name,
                Id = id
            };
            return View(editCategory);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AddCategory model)
        {
            var userId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                Category category = new Category()
                {
                    Id = model.Id,
                    Name = model.Name,
                    UserId = userId
                };
                var result = categoryService.UpdateCategory(category);
                if (!result)
                {
                    this.AddNotification("Nje kategori me te njejtin emer egziston. Vendosni nje emer te ri per kategorine .", NotificationType.WARNING);
                    return View(model);
                }
                else
                {
                    this.AddNotification("Kategoria u modifikua me sukses .", NotificationType.SUCCESS);
                    return RedirectToAction("Index", "News", new { id = model.Id});
                }
            }
            return View(model);

        }



        public ActionResult DeleteConfirm(int id)
        {
            categoryService.DeleteCategory(id);
            this.AddNotification("Kategoria u fshi me sukses .", NotificationType.SUCCESS);
            return RedirectToAction("Index", "Home");
        }
    }
}