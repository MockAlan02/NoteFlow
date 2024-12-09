import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Tasks } from '../../models/tasks';
import { MatMenuModule } from '@angular/material/menu';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { TasksService } from '../../services/tasks.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-task-cards',
  standalone: true,
  imports: [MatMenuModule, MatButtonModule, MatIconModule, CommonModule],
  templateUrl: './task-cards.component.html',
  styleUrl: './task-cards.component.css',
})
export class TaskCardsComponent {
  /**
   *
   */
  @Output() onDelete = new EventEmitter<string>();
  @Output() onComplete = new EventEmitter<string>();
  isExpanded: boolean = false;
  @Input() task?: Tasks;
  constructor(private router: Router) {}

  deleteTask() {
    if (this.task?.id) {
      this.onDelete.emit(this.task.id);
    }
  }

  updateLink() {
    this.router.navigate([`/task/update`, this.task?.id]);
  }

  completeTask() {
    if (this.task?.id) {
      this.onComplete.emit(this.task.id);
    }
  }

  toggleExpanded() {
    this.isExpanded = !this.isExpanded;
  }
}
