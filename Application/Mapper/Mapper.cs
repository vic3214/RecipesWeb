using Application.DTO;
using Domain.Entities;

namespace Application.Mapper
{

        public static class EntityToDTO
        {
            public static RecipeDTO RecipeToDTO(Recipe recipe)
            {
                return new RecipeDTO
                {
                    Id = recipe.Id,
                    Title = recipe.Title,
                    Link = recipe.Link,
                    Site = recipe.Site,
                    Source = recipe.Source
                };
            }
        }
    
}
