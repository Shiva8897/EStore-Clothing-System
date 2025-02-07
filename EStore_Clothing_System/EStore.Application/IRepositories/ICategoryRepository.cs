using EStore.Domain.Entities;
using EStore.Domain.EntityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.IRepositories
{
    public interface ICategoryRepository
    {
        Task<Category> GetCategoryAsync(int id);
        Task<Category> GetCategoryAsync(string name);
        Task<List<Category>> GetAllCategoriesAsync();
        Task<Category> CreateCategoryAsync(Category category);
        Task<Category> UpdateCategoryAsync(Category category);
        Task<bool> DeleteCategoryAsync(int id);

    }
}
