import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Tasks } from '../../models/tasks';
import { MatMenuModule } from '@angular/material/menu';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { TasksFilter } from '../../models/tasksFilter.interface';
import { TasksService } from '../../services/tasks.service';

@Component({
  selector: 'app-filter',
  standalone: true,
  imports: [MatMenuModule, MatButtonModule, MatIconModule],
  templateUrl: './filter.component.html',
  styleUrl: './filter.component.css',
})
export class FilterComponent {
  tasksFilter: TasksFilter = {
    Name: '',
  };
  @Output() filteredTasks2 = new EventEmitter<Tasks[]>();

  constructor(private taskService: TasksService) {}

  applyFilter() {
    const response = this.taskService.filterTask(this.tasksFilter) ?? [];
    response.subscribe((tasks) => {
      this.filteredTasks2.emit(tasks);
    });
  }

  setFilterName(name?: string) {
    this.tasksFilter.Name = name ?? '';
    this.applyFilter();
  }

  setFilterStatus(status?: string | undefined) {
    if (status === 'all') {
      this.tasksFilter.Status = undefined; // O el valor que represente "Todos"
    } else if (status === 'completed') {
      this.tasksFilter.Status = true;
    } else if (status === 'pending') {
      this.tasksFilter.Status = false;
    }
    this.applyFilter();
  }
}
