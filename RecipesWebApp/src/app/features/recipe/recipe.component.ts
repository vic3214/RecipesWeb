import {
  ChangeDetectionStrategy,
  Component,
  inject,
  OnInit,
  signal,
  ViewChild,
  WritableSignal,
} from '@angular/core';
import { Recipe } from '@app/core/models/recipe.model';
import { RecipeService } from '@app/features/recipe/service/recipe.service';
import { Table, TableModule } from 'primeng/table';
import { TagModule } from 'primeng/tag';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { InputTextModule } from 'primeng/inputtext';
import { MultiSelectModule } from 'primeng/multiselect';
import { SelectModule } from 'primeng/select';
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { ToolbarModule } from 'primeng/toolbar';
import { debounceTime, Subject } from 'rxjs';
import { PaginatedResult } from '@app/core/models/paginated-result.model';

@Component({
  selector: 'app-recipe',
  imports: [
    TableModule,
    SelectModule,
    ToolbarModule,
    MultiSelectModule,
    InputTextModule,
    TagModule,
    IconFieldModule,
    InputIconModule,
    CommonModule,
    ButtonModule,
  ],
  templateUrl: './recipe.component.html',
  styleUrl: './recipe.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RecipeComponent implements OnInit {
  @ViewChild('dt1') d1Table!: Table;
  protected recipes: WritableSignal<Recipe[]> = signal([]);
  protected initialRecipes: Recipe[] = [];
  protected selectedRecipes!: Recipe[] | null;
  private recipeService: RecipeService = inject(RecipeService);
  private debouncer: Subject<string> = new Subject<string>();

  ngOnInit() {
    this.getInitialRecipes();

    this.debouncer.pipe(debounceTime(500)).subscribe((value) => {
      this.recipeService
        .getRecipesBySearch(value)
        .pipe(debounceTime(500))
        .subscribe((recipes: PaginatedResult<Recipe>) => {
          this.recipes.set(recipes.data);
        });
    });
  }

  protected addRecipe() {}

  protected deleteSelectedProducts() {
    console.log(this.selectedRecipes);
  }

  protected selectProduct(recipe: Recipe) {
    console.log(recipe);
  }

  protected filterTable(event: Event) {
    const inputValue = (event.target as HTMLInputElement).value;
    if (inputValue.length > 3) {
      this.debouncer.next(inputValue);
    }

    if (inputValue === '') {
      this.recipes.set(this.initialRecipes);
    }
  }

  private getInitialRecipes() {
    this.recipeService
      .getAllRecipes()
      .subscribe((recipes: PaginatedResult<Recipe>) => {
        this.initialRecipes = recipes.data;
        this.recipes.set(this.initialRecipes);
      });
  }
}
