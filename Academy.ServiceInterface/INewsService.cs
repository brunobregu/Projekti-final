using Academy.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.ServiceInterface
{
    public interface INewsService
    {
        List<News> GetAllPublishedNews(int id);


        bool CreateNews(News n);


        List<News> GetAllDraftNews(int id);


        News GetNewsById(int id);


        bool UpdateNews(News n);
    }
}
