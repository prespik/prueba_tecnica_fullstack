import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TaskItem } from '../models/task.model';

@Injectable({ providedIn: 'root' })

export class TaskService {

  private baseUrl = 'https://localhost:7197/api/tasks'; // AJUSTA PUERTO

  constructor(private http: HttpClient) {}

  getTasks(page: number, pageSize: number, status: number | null) {
    let params: any = {
      page,
      pageSize
    };

    if (status !== null) {
      params.status = status;
    }

    return this.http.get<any>(this.baseUrl, { params });
  }


  createTask(task: any) {
    return this.http.post(this.baseUrl, task);
  }

  changeStatus(id: number, newStatus: number) {
    return this.http.put(
      `${this.baseUrl}/${id}/status`,
      { newStatus }
    );
  }
}
