import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewsLetterContainerComponent } from './news-letter-container.component';

describe('NewsLetterContainerComponent', () => {
  let component: NewsLetterContainerComponent;
  let fixture: ComponentFixture<NewsLetterContainerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NewsLetterContainerComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NewsLetterContainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
