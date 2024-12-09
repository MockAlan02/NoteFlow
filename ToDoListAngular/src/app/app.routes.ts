import { Routes } from '@angular/router';
import { ShowTasksComponent } from './pages/show-tasks/show-tasks.component';
import { CreateTaskComponent } from './pages/create-task/create-task.component';

export const routes: Routes = [
  { path: '', component: ShowTasksComponent },
  { path: 'task/update/:id', component: CreateTaskComponent },
  { path: 'task/create', component: CreateTaskComponent },
];
