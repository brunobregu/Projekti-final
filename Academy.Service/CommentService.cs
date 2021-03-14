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
    public class CommentService : ICommentService
    {
        readonly ICommentRepository repository;
        public CommentService(ICommentRepository rep)
        {
            this.repository = rep;
        }


        public void CreateComment(Comment c)
        {
            repository.CreateComment(c);
        }



        public List<Comment> GetAllComments(int id)
        {
            return repository.GetAllComments(id);
        }
    }
}
