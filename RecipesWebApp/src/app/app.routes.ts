import { Routes } from '@angular/router';
import {RecipeComponent} from '@app/features/recipe/recipe.component';
import {DirectionComponent} from '@app/features/direction/direction.component';
import {NerComponent} from '@app/features/ner/ner.component';
import {IngredientComponent} from '@app/features/ingredient/ingredient.component';
import {NotFoundComponent} from '@app/shared/components/not-found/not-found.component';
import {HomeComponent} from '@app/shared/components/home/home.component';

export const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'recipes',
    component: RecipeComponent,
    children:[
      {
        path:'create',
        loadComponent: () => import('./features/recipe/recipe-create-edit/recipe-create-edit.component').then(c=>c.RecipeCreateEditComponent)
      },
      {
        path: 'edit/:id',
        loadComponent: () => import('./features/recipe/recipe-create-edit/recipe-create-edit.component').then(c=>c.RecipeCreateEditComponent)
      }
    ]
  },
  { path: 'directions',
    component: DirectionComponent,
    children: [
      {
        path: 'create',
        loadComponent: () => import('./features/direction/direction-create-edit/direction-create-edit.component').then(c=>c.DirectionCreateEditComponent)
      },
      {
        path: 'edit/:id',
        loadComponent: () => import('./features/direction/direction-create-edit/direction-create-edit.component').then(c=>c.DirectionCreateEditComponent)
      }
    ]
  },
  { path: 'ners',
    component: NerComponent,
    children: [
      {
        path: 'create',
        loadComponent: () => import('./features/ner/ner-create-edit/ner-create-edit.component').then(c=>c.NerCreateEditComponent)
      },
      {
        path: 'edit/:id',
        loadComponent: () => import('./features/ner/ner-create-edit/ner-create-edit.component').then(c=>c.NerCreateEditComponent)
      }
    ]},
  { path: 'ingredients',
    component: IngredientComponent,
    children: [
      {
        path: 'create',
        loadComponent: () => import('./features/ingredient/ingredient-create-edit/ingredient-create-edit.component').then(c=>c.IngredientCreateEditComponent)
      },
      {
        path: 'edit/:id',
        loadComponent: () => import('./features/ingredient/ingredient-create-edit/ingredient-create-edit.component').then(c=>c.IngredientCreateEditComponent)
      }
    ] },
  { path: '', redirectTo:'/home', pathMatch:'full' },
  { path: '**', component: NotFoundComponent },
];
