using Academy.DomainModels;
using Academy.Extensions;
using Academy.ServiceInterface;
using Academy.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Academy.Controllers
{
    public class NewsController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly INewsService newsService;
        private readonly ICommentService commentService; 

        public NewsController(ICategoryService categorySrv, INewsService newsSrv, ICommentService commentSrv)
        {
            categoryService = categorySrv;
            newsService = newsSrv;
            commentService = commentSrv;
        }



        private string ValidateFile(HttpPostedFileBase file)
        {
            string fileExtension = Path.GetExtension(file.FileName).ToLower();
            string[] allowedPhotoFiles = { ".png", ".jpeg", ".jpg" };

            if ((file.ContentLength > 0 && file.ContentLength < 30000000) &&
              allowedPhotoFiles.Contains(fileExtension))
            {
                return string.Empty;
            }
            return "File i zgjedhur duhet te jete foto.";

        }




        private void RuajFile(HttpPostedFileBase file)
        {
            var fileName = Path.GetFileName(file.FileName);
            string path;
            if (file.ContentLength > 0)
            {

                path = Path.Combine(Server.MapPath(NewsPath.NewsPhotoPath), fileName);

                file.SaveAs(path);
            }

        }





        public ActionResult Index(int id)
        {
            var category = categoryService.GetCategoryById(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = id;
            var news = newsService.GetAllPublishedNews(id);
            return View(news);
        }


        [Authorize]
        public ActionResult Create(int id)
        {
            List<SelectListItem> listItems = new List<SelectListItem>
            {
                new SelectListItem { Text = "Draft", Value = "Draft" },
                new SelectListItem { Text = "Published", Value = "Published" }
            };
            ViewBag.Status = new SelectList(listItems, "Text", "Value");
            var category = categoryService.GetCategoryById(id);
            if (category == null)
                return HttpNotFound();
            AddNews news = new AddNews()
            {
                CategoryId = category.Id
            };
            return View(news);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase file, AddNews model)
        {
            var userId = User.Identity.GetUserId();
            List<SelectListItem> listItems = new List<SelectListItem>
            {
                new SelectListItem { Text = "Draft", Value = "Draft" },
                new SelectListItem { Text = "Published", Value = "Published" }
            };
            if (file != null)
            {
                string error = ValidateFile(file);
                if (!string.IsNullOrEmpty(error))
                {
                    this.AddNotification(error, NotificationType.WARNING);
                    ViewBag.Status = new SelectList(listItems, "Text", "Value");
                    return View(model);
                }
            }
            else
            {
                this.AddNotification("Zgjidhni nje foto .", NotificationType.ERROR);
                ViewBag.Status = new SelectList(listItems, "Text", "Value");
                return View(model);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    RuajFile(file);
                }
                catch
                {
                    this.AddNotification("Nuk u ruajt file, provo perseri me vone .", NotificationType.WARNING);
                }
                News newNews = new News()
                {
                    Title = model.Title,
                    Subtitle = model.Subtitle,
                    Description = model.Description,
                    Status = model.Status,
                    Filename = Path.GetFileName(file.FileName),
                    CreatedOn = DateTime.Now,
                    CategoryId = model.CategoryId,
                    UserId = userId
                };
                if (newsService.CreateNews(newNews))
                {
                    this.AddNotification("Lajmi u shtua me sukses .", NotificationType.SUCCESS);
                    return RedirectToAction("Index", new { id = model.CategoryId });
                }
                else
                {
                    this.AddNotification("Nje lajm me te njejtin titull tashme egziston. Ju lutem zgjidhni nje titull te ri per lajmin .", NotificationType.INFO);
                    ViewBag.Status = new SelectList(listItems, "Text", "Value");
                    return View(model);
                }
            }
            ViewBag.Status = new SelectList(listItems, "Text", "Value");
            return View(model);
        }


        [Authorize]
        public ActionResult DraftNews(int id)
        {
            ViewBag.User = User.Identity.GetUserId();
            var category = categoryService.GetCategoryById(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            TempData["CategoryId"] = id;
            TempData.Keep();
            var allNews = newsService.GetAllDraftNews(id);
            return View(allNews);
        }


        [Authorize]
        public ActionResult Edit(int id)
        {
            TempData.Keep();
            var news = newsService.GetNewsById(id);
            if(news == null)
            {
                return HttpNotFound();
            }
            var categoryId = Convert.ToInt32(TempData["CategoryId"]);
            AddNews editNews = new AddNews()
            {
                Id = id,
                Title = news.Title,
                Subtitle = news.Subtitle,
                Description = news.Description,
                CategoryId = categoryId
            };
            List<SelectListItem> listItems = new List<SelectListItem>
            {
                new SelectListItem { Text = "Draft", Value = "Draft" },
                new SelectListItem { Text = "Published", Value = "Published" },
                new SelectListItem { Text = "Delete", Value = "Delete" }
            };
            ViewBag.Status = new SelectList(listItems, "Text", "Value");
            return View(editNews);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase file, AddNews model)
        {
            var userId = User.Identity.GetUserId();
            if (file != null)
            {
                string error = ValidateFile(file);
                if (!string.IsNullOrEmpty(error))
                {
                    this.AddNotification(error, NotificationType.SUCCESS);
                }
            }
            //ne kete rast nuk eshte perzgjedhur nje file dhe si material do te mbetet ai ekzistues
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    try
                    {
                        RuajFile(file);
                    }
                    catch (Exception)
                    {
                        this.AddNotification("Nuk u ruajt file ne disk, provo perseri.", NotificationType.SUCCESS);
                    }
                }
                News news = new News()
                {
                    Id = model.Id,
                    Title = model.Title,
                    Subtitle = model.Subtitle,
                    Description = model.Description,
                    Filename = model.Filename,
                    CategoryId = model.CategoryId,
                    UserId = userId,
                    Status = model.Status,
                    CreatedOn = DateTime.Now
                };
                bool result = newsService.UpdateNews(news);
                if (!result)
                    this.AddNotification("Nje lajm me te njetin titull tashme ekziston. Ju lutem zgjidhni nje titull tjeter per lajmin tuaj.", NotificationType.WARNING);
                else
                {
                    this.AddNotification("Lajmi u editua me sukses.", NotificationType.SUCCESS);
                    return RedirectToAction("Index", new { id = model.CategoryId });
                }
            }
            List<SelectListItem> listItems = new List<SelectListItem>
            {
                new SelectListItem { Text = "Draft", Value = "Draft" },
                new SelectListItem { Text = "Published", Value = "Published" },
                new SelectListItem { Text = "Delete", Value = "Delete" }
            };
            ViewBag.Status = new SelectList(listItems, "Text", "Value");
            return View(model);
        }


        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var userId = User.Identity.GetUserId();
            ViewBag.User = userId;
            var news = newsService.GetNewsById(id);
            var allComments = commentService.GetAllComments(id);
            TempData["CategoryId"] = news.CategoryId;
            TempData.Keep();
            if(news == null)
            {
                return HttpNotFound();
            }
            AddNews allNews = new AddNews()
            {
                Id=news.Id,
                Title = news.Title,
                Subtitle = news.Subtitle,
                Filename = news.Filename,
                Description = news.Description,
                UserId = news.UserId
            };
            AddComment comment = new AddComment()
            {
                NewsId = news.Id,
                CreatedOn = DateTime.Now,
            };
            ViewBag.Comments = allComments;
            ViewBag.News = allNews;
            return View(comment);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostComment(AddComment model)
        {
            if (ModelState.IsValid)
            {
                Comment comment = new Comment()
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    Description = model.Description,
                    NewsId = model.NewsId,
                    CreatedOn = DateTime.Now
                };
                commentService.CreateComment(comment);
                this.AddNotification("Komenti juaj u postua .", NotificationType.SUCCESS);
                return RedirectToAction("Details", new { id = model.NewsId });
            }
            this.AddNotification("Fushat jane te detyrueshme .", NotificationType.ERROR);
            return RedirectToAction("Details", new { id = model.NewsId });
            
        }


        
    }
}