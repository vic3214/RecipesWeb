import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NerCreateEditComponent } from './ner-create-edit.component';

describe('NerCreateEditComponent', () => {
  let component: NerCreateEditComponent;
  let fixture: ComponentFixture<NerCreateEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NerCreateEditComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NerCreateEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
