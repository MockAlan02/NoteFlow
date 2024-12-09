import { Component, OnInit } from '@angular/core';
import { TaskCardsComponent } from '../../components/task-cards/task-cards.component';
import { Tasks } from '../../models/tasks';
import { TasksService } from '../../services/tasks.service';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FilterComponent } from '../../components/filter/filter.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-show-tasks',
  standalone: true,
  imports: [
    TaskCardsComponent,
    HttpClientModule,
    CommonModule,
    FilterComponent,
  ],
  templateUrl: './show-tasks.component.html',
  styleUrl: './show-tasks.component.css',
  providers: [TasksService],
})
export class ShowTasksComponent implements OnInit {
  tasks: Tasks[] = [];
  constructor(private taskService: TasksService, private router: Router) {}
  ngOnInit(): void {
    this.taskService.getTasks().subscribe((tasks) => {
      this.tasks = tasks;
    });
  }

  //set filtered tasks
  onFilteredTask(filtered: Tasks[]) {
    this.tasks = filtered;
  }

  // Función para eliminar tarea
  deleteTask(taskId: string) {
    this.taskService.deleteTaks(taskId).subscribe({
      next: () => {
        if (this.tasks) {
          this.tasks = this.tasks.filter((task) => task.id !== taskId);
          console.log('Task deleted successfully');
        } else {
          console.warn('Tasks array is undefined or null');
        }
      },
      error: (err) => {
        console.error('Error deleting task:', err);
      },
    });
  }

  // Función para completar tarea
  completeTask(taskId: string) {
    this.taskService.completeTask(taskId).subscribe({
      next: (task) => {
        if (this.tasks) {
          const index = this.tasks.findIndex((t) => t.id === taskId);
          if (index !== -1) {
            this.tasks[index].isCompleted = true; // Actualizar la tarea con los nuevos datos
          }
          console.log('Task completed successfully');
        }
      },
      error: (err) => {
        console.error('Error completing task:', err);
      },
    });
  }
  addTask() {
    this.router.navigate(['task/create']);
  }
}
