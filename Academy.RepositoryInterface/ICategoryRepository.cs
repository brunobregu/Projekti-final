using Academy.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.RepositoryInterface
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategories();


        bool CreateCategory(Category c);



        Category GetCategoryById(int id);


        bool UpdateCategory(Category c);


        void DeleteCategory(int categoryId);
    }
}
