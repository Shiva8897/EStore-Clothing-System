using EStore.Domain.EntityDtos;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;


namespace EStore.Application.Services
{
    public class AddProductDtoModelBinder: IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var request = bindingContext.ActionContext.HttpContext.Request;
            var formData = await request.ReadFormAsync();
            var json = await new StreamReader((Stream)formData.Files["jsonData"]).ReadToEndAsync();
            var addProductDto = JsonConvert.DeserializeObject<AddProductDto>(json);

            bindingContext.Result = ModelBindingResult.Success(addProductDto);
            await Task.CompletedTask;
        }
    }
}
