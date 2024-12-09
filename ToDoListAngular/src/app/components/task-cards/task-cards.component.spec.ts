import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskCardsComponent } from './task-cards.component';

describe('TaskCardsComponent', () => {
  let component: TaskCardsComponent;
  let fixture: ComponentFixture<TaskCardsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TaskCardsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TaskCardsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
