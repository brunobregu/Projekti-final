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
    public class CategoryService : ICategoryService
    {
        ICategoryRepository repository;
        public CategoryService(ICategoryRepository rep)
        {
            this.repository = rep;
        }



        public List<Category> GetAllCategories()
        {
            return repository.GetAllCategories();
        }



        public bool CreateCategory(Category c)
        {
            return repository.CreateCategory(c);
        }



        public Category GetCategoryById(int id)
        {
            return repository.GetCategoryById(id);
        }


        public bool UpdateCategory(Category c)
        {
            return repository.UpdateCategory(c);
        }


        public void DeleteCategory(int categoryId)
        {
            repository.DeleteCategory(categoryId);
        }
    }
}
