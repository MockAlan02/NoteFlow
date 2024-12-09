import { Injectable } from '@angular/core';
import { Tasks } from '../models/tasks';
import { HttpClient } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { TasksFilter } from '../models/tasksFilter.interface';

@Injectable({
  providedIn: 'root',
})
export class TasksService {
  private tasksUrl = 'https://localhost:7003/api/tasks';
  constructor(private http: HttpClient) {}

  getTasks(): Observable<Tasks[]> {
    return this.http.get<Tasks[]>(this.tasksUrl);
  }

  public getTasksById(id: string): Observable<Tasks> {
    return this.http
      .get<Tasks>(`${this.tasksUrl}/${id}`)
      .pipe(catchError(this.handleError));
  }

  public addTask(data: Tasks): Observable<Tasks> {
    return this.http
      .post<Tasks>(`${this.tasksUrl}`, data)
      .pipe(catchError(this.handleError));
  }

  public deleteTaks(id: string): Observable<void> {
    return this.http
      .delete<void>(`${this.tasksUrl}/${id}`)
      .pipe(catchError(this.handleError));
  }

  public updateTasks(data: Tasks, id: string): Observable<Tasks> {
    return this.http
      .put<Tasks>(`${this.tasksUrl}/${id}`, data)
      .pipe(catchError(this.handleError));
  }

  public completeTask(id: string): Observable<Tasks> {
    return this.http
      .put<Tasks>(`${this.tasksUrl}/${id}/Complete`, id)
      .pipe(catchError(this.handleError));
  }

  public filterTask(filter?: TasksFilter): Observable<Tasks[]> {
    let taskUrlQuery = `${this.tasksUrl}/filter?`;
    if (filter?.Name.trim()) {
      taskUrlQuery += `Name=${filter.Name}&`;
    }
    if (filter?.Status != undefined) {
      taskUrlQuery += `Status=${filter.Status}&`;
    }

    // Remove the trailing '&' or '?' if no filters were added
    taskUrlQuery = taskUrlQuery.slice(0, -1);

    return this.http
      .get<Tasks[]>(taskUrlQuery)
      .pipe(catchError(this.handleError));
  }

  // Centralized error handling
  private handleError(error: any): Observable<never> {
    console.error('An error occurred:', error);
    return throwError(() => new Error(error.message || 'Server error'));
  }
}
