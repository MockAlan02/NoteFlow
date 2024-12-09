import { CommonModule } from '@angular/common';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';

import { Tasks } from '../../models/tasks';
import { TasksService } from '../../services/tasks.service';
import { ActivatedRoute, Router } from '@angular/router';
import { error } from 'console';
import { title } from 'process';

@Component({
  selector: 'app-create-task',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './create-task.component.html',
  styleUrl: './create-task.component.css',
})
export class CreateTaskComponent implements OnInit {
  userForm!: FormGroup;
  task?: Tasks;
  @Output() onAdd = new EventEmitter<Tasks>();
  constructor(
    private fb: FormBuilder,
    private taskService: TasksService,
    private router: Router,
    private activeRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    // Create form structure
    this.userForm = this.fb.group({
      title: [this.task?.title, Validators.required],
      description: [this.task?.description, Validators.required],
    });

    //validate url
    const id = this.activeRoute.snapshot.paramMap.get('id');
    if (id) {
      this.taskService.getTasksById(id).subscribe(
        (task) => {
          this.userForm.patchValue({
            title: task.title,
            description: task.description,
          });
          this.task = task;
        },
        (error) => {
          console.log(error.message);
        }
      );
    }
  }

  //submit Form function
  submitForm(): void {
    //form data is valid..
    if (!this.userForm.valid) {
      console.log('Form is invalid');
      return;
    }
    // ternary condition to select taskOperation
    const newTask: Tasks = this.userForm.value;
    const taskOperation = this.task?.id
      ? this.taskService.updateTasks(
          { ...newTask, id: this.task.id },
          this.task.id
        )
      : this.taskService.addTask(newTask);

    taskOperation.subscribe({
      next: (task) => {
        if (!task) {
          console.log('Invalid Task');
        } else {
          console.log('Actualizacion exitosa', task);
          this.router.navigate(['/']);
        }
      },
      error: (err) => {
        console.log('Error message: ', err.message);
      },
    });
  }
}
