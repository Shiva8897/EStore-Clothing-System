using AutoMapper;
using EStore.Application.Interfaces;
using EStore.Application.IRepositories;
using EStore.Domain.Entities;
using EStore.Domain.EntityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Category> CreateCategoryAsync(CategoryReq category)
        {
            if(category == null)
            {
                throw new ArgumentNullException();
            }
            var categoryDto = _mapper.Map<Category>(category);
           return await _categoryRepository.CreateCategoryAsync(categoryDto);    
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException("Invalid product ID", nameof(id));
            }
           return await _categoryRepository.DeleteCategoryAsync(id);
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllCategoriesAsync();
        }

        public async Task<Category> GetCategoryAsync(int id)
        {                    
            return await _categoryRepository.GetCategoryAsync(id);
        }

        public async Task<Category> GetCategoryAsync(string name)
        {        
            return await _categoryRepository.GetCategoryAsync(name);
        }

        public async Task<Category> UpdateCategoryAsync(CategoryReq category)
        {
            if (category == null)
            {
                throw new ArgumentNullException();
            }
            var categoryDto = _mapper.Map<Category>(category);
            return await _categoryRepository.UpdateCategoryAsync(categoryDto);
        }
       
        }
}
