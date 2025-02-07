using EStore.Application.IRepositories;
using EStore.Domain.Entities;
using EStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Infrastructure.Repositories
{
     public class CategoryRepository:ICategoryRepository
     {
        private EStoreDbContext _context;
        public CategoryRepository(EStoreDbContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
             var existingCategory=await _context.Categories.FirstOrDefaultAsync(c => c.CategoryName == category.CategoryName);
            if (existingCategory != null)
            {
                return null;
            }
            _context.Categories.Add(category);         
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var categoryToRemove = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(categoryToRemove);
            return await _context.SaveChangesAsync()>0;
        }
        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            var existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == category.CategoryId);
            if (existingCategory == null)
            {
                return null;
            }
            existingCategory.CategoryName = category.CategoryName;
            existingCategory.IsActive = true;
            existingCategory.CreatedDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return category;
        }
        public async Task<List<Category>> GetAllCategoriesAsync()
        {

            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
        }

        public async Task<Category> GetCategoryAsync(string name)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.CategoryName == name);
        }
    
    }
}
