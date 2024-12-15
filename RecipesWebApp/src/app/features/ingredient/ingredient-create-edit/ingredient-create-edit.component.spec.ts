import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IngredientCreateEditComponent } from './ingredient-create-edit.component';

describe('IngredientCreateEditComponent', () => {
  let component: IngredientCreateEditComponent;
  let fixture: ComponentFixture<IngredientCreateEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [IngredientCreateEditComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IngredientCreateEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
