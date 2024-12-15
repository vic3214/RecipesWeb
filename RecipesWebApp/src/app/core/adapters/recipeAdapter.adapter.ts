import { Recipe } from '../models/recipe.model';
import { PaginatedResult } from '@app/core/models/paginated-result.model';

/**
 * Adaptador que toma un PaginatedResult de recetas y normaliza sus propiedades, como los enlaces.
 * @param recipesResponse Un objeto de tipo PaginatedResult<Recipe[]> a adaptar.
 * @returns Un nuevo objeto PaginatedResult<Recipe[]> con los enlaces normalizados.
 */
export const RecipeAdapter = (
  recipesResponse: PaginatedResult<Recipe>,
): PaginatedResult<Recipe> => {
  return {
    ...recipesResponse,
    data: recipesResponse.data.map((recipe) => ({
      ...recipe,
      link: recipe.link.startsWith('http')
        ? recipe.link
        : 'https://' + recipe.link,
    })),
  };
};
