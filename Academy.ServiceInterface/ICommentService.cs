using Academy.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.ServiceInterface
{
    public interface ICommentService
    {
        void CreateComment(Comment c);


        List<Comment> GetAllComments(int id);
    }
}
