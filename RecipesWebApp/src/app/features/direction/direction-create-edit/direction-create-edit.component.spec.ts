import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DirectionCreateEditComponent } from './direction-create-edit.component';

describe('DirectionCreateEditComponent', () => {
  let component: DirectionCreateEditComponent;
  let fixture: ComponentFixture<DirectionCreateEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DirectionCreateEditComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DirectionCreateEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
