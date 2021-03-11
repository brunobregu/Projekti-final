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
    public class CategoryRepository : ICategoryRepository
    {
        ProjectDbContext db;
        public CategoryRepository()
        {
            this.db = new ProjectDbContext();
        }


        public List<Category> GetAllCategories()
        {
            return db.Categories.OrderBy(x=>x.Name).ToList();
        }


        public bool CreateCategory(Category c)
        {
            //nuk lejohet qe te kete kategori me emer te njejte ne nivel tabele
            var countSameTitleCategory = db.Categories.Where(x => x.Name == c.Name).Count();

            if (countSameTitleCategory > 0)
                return false;
            db.Categories.Add(c);
            db.SaveChanges();
            return true;
        }



        public Category GetCategoryById(int id)
        {
            return db.Categories.FirstOrDefault(x => x.Id == id);

        }



        public bool UpdateCategory(Category c)
        {
            //nuk lejohet qe te kete kategori me emer te njejte ne nivel tabele
            var countSameTitleCategories = db.Categories.Where(x => x.Name == c.Name && x.Id != c.Id).Count();
            if (countSameTitleCategories > 0)
                return false;
            var currentCategory = db.Categories.FirstOrDefault(x => x.Id == c.Id);
            currentCategory.Name = c.Name;

            db.SaveChanges();
            return true;
        }



        public void DeleteCategory(int categoryId)
        {
            var currentCategory = db.Categories.FirstOrDefault(x => x.Id == categoryId);

            db.Categories.Remove(currentCategory);
            db.SaveChanges();
        }
    }
}
