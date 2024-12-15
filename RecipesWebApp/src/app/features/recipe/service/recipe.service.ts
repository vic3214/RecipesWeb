import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Recipe } from '@app/core/models/recipe.model';
import { environment } from '../../../../environments/environment';
import { Routes } from '@app/core/enums/routes.enum';
import { map, Observable } from 'rxjs';
import { RecipeAdapter } from '@app/core/adapters/recipeAdapter.adapter';
import { PaginatedResult } from '@app/core/models/paginated-result.model';

@Injectable({
  providedIn: 'root',
})
export class RecipeService {
  private _http: HttpClient = inject(HttpClient);

  getAllRecipes(
    page: number = 1,
    pageSize: number = 10,
  ): Observable<PaginatedResult<Recipe>> {
    const params = new HttpParams().set('page', page).set('pageSize', pageSize);

    return this._http
      .get<PaginatedResult<Recipe>>(environment.api + Routes.Recipes, {
        params,
      })
      .pipe(map((recipes) => RecipeAdapter(recipes)));
  }

  getRecipesBySearch(searchText: string): Observable<PaginatedResult<Recipe>> {
    const params = new HttpParams().set('searchText', searchText);

    return this._http
      .get<PaginatedResult<Recipe>>(environment.api + Routes.RecipesBySearch, {
        params,
      })
      .pipe(map((recipes) => RecipeAdapter(recipes)));
  }

  addRecipe(recipe: Omit<Recipe, 'id'>): Observable<Recipe> {
    return this._http.post<Recipe>(
      environment.api + Routes.RecipesCreate,
      recipe,
    );
  }

  updateRecipe(recipe: Recipe): Observable<Recipe> {
    return this._http.put<Recipe>(environment.api + Routes.RecipesEdit, recipe);
  }

  removeRecipe(id: number): Observable<void> {
    return this._http.delete<void>(
      `${environment.api + Routes.RecipesEdit}/${id}`,
    );
  }
}
