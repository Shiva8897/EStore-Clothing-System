using EStore.Domain.Entities;
using EStore.Domain.EntityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> GetCategoryAsync(int id);
        Task<Category> GetCategoryAsync(string name);
        Task<List<Category>> GetAllCategoriesAsync();
        Task<Category> CreateCategoryAsync(CategoryReq category);
        Task<Category> UpdateCategoryAsync(CategoryReq category);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
