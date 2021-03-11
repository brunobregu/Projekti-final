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
    public class CommentRepository : ICommentRepository
    {
        ProjectDbContext db;
        public CommentRepository()
        {
            this.db = new ProjectDbContext();
        }



        public void CreateComment(Comment c)
        {
            db.Comments.Add(c);
            db.SaveChanges();
        }



        public List<Comment> GetAllComments(int id)
        {
            return db.Comments.Where(x => x.NewsId == id).ToList();
        }
    }
}
