using Academy.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.RepositoryInterface
{
    public interface ICommentRepository
    {
        void CreateComment(Comment c);


        List<Comment> GetAllComments(int id);
    }
}
