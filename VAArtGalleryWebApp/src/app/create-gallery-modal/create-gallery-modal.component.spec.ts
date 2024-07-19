import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateGalleryModalComponent } from './create-gallery-modal.component';

describe('CreateGalleryModalComponent', () => {
  let component: CreateGalleryModalComponent;
  let fixture: ComponentFixture<CreateGalleryModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CreateGalleryModalComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CreateGalleryModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
