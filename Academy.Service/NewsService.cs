using Academy.DomainModels;
using Academy.RepositoryInterface;
using Academy.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Service
{
    public class NewsService : INewsService
    {
        readonly INewsRepository repository;
        public NewsService(INewsRepository rep)
        {
            this.repository = rep;
        }


        public List<News> GetAllPublishedNews(int id)
        {
            return repository.GetAllPublishedNews(id);
        }



        public bool CreateNews(News n)
        {
            return repository.CreateNews(n);
        }



        public List<News> GetAllDraftNews(int id)
        {
            return repository.GetAllDraftNews(id);
        }



        public News GetNewsById(int id)
        {
            return repository.GetNewsById(id);
        }


        public bool UpdateNews(News n)
        {
            return repository.UpdateNews(n);
        }
    }
}
