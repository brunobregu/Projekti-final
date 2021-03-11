using Academy.DataLayer;
using Academy.DomainModels;
using Academy.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Repository
{
    public class NewsRepository : INewsRepository
    {
        ProjectDbContext db;
        public NewsRepository()
        {
            this.db = new ProjectDbContext();
        }



        public List<News> GetAllPublishedNews(int id)
        {
            return db.News.Where(x => x.Status == "Published" && x.CategoryId == id).OrderByDescending(x=>x.CreatedOn).ToList();
        }



        public bool CreateNews(News n)
        {
            //nuk lejohet qe te kete lajm me emer te njejte ne nivel tabele
            var countSameTitleNews = db.News.Where(x => x.Title == n.Title && x.Status != "Delete").Count();

            if (countSameTitleNews > 0)
                return false;
            db.News.Add(n);
            db.SaveChanges();
            return true;
        }



        public List<News> GetAllDraftNews(int id)
        {
            return db.News.Where(x => x.Status == "Draft" && x.CategoryId == id).ToList();
        }



        public News GetNewsById(int id)
        {
            return db.News.FirstOrDefault(x => x.Id == id);
        }



        public bool UpdateNews(News n)
        {
            //nuk lejohet qe te kete lajme me titull te njejte ne nivel tabele
            var countSameTitleNews = db.News.Where(x => x.Title == n.Title && x.Id != n.Id).Count();
            if (countSameTitleNews > 0)
                return false;
            var currentNews = db.News.FirstOrDefault(x => x.Id == n.Id);
            currentNews.Title = n.Title;
            currentNews.Subtitle = n.Subtitle;
            currentNews.Description = n.Description;
            currentNews.Status = n.Status;
            currentNews.CreatedOn = DateTime.Now;
            //nqs nuk eshte ngarkuar nje file i ri ateher emri i file-it nuk do te ndryshoje
            if (!string.IsNullOrEmpty(n.Filename))
                currentNews.Filename = n.Filename;
            currentNews.CategoryId = n.CategoryId;
            db.SaveChanges();
            return true;
        }
    }
}
