import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ChangeDetectorRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatCard } from '@angular/material/card';
import { MatDialogModule } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { CreateTaskDialogComponent } from '../create-task-dialog/create-task-dialog';

import { TaskService } from '../../services/task';
import { UserService } from '../../services/user';
import { TaskItem } from '../../models/task.model';
import { User } from '../../models/user.model';
import { TaskStatus } from '../../models/task-status.enum';


@Component({
  selector: 'app-tasks',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatSelectModule,
    MatButtonModule,
    MatFormFieldModule,
    MatDialogModule,
    MatCard,
    MatIconModule,
    MatTooltipModule
  ],
  templateUrl: './tasks.html',
  styleUrl: './tasks.css',
})
export class TasksComponent implements OnInit {

  tasks: TaskItem[] = [];
  users: User[] = [];
  selectedStatus: number | null = null;
  TaskStatus = TaskStatus;
  

  constructor(
    private taskService: TaskService,
    private userService: UserService,
    private cdr: ChangeDetectorRef,
    private dialog: MatDialog
  ) {}

  ngOnInit() {
    this.loadTasks();
    this.loadUsers();
  }

loadTasks() {
  this.taskService.getTasks(1, 10, this.selectedStatus)
    .subscribe(res => {
      this.tasks = res.items;
      this.cdr.detectChanges();
    });
}

  loadUsers() {
    this.userService.getUsers()
      .subscribe(res => this.users = res);
  }

  changeStatus(task: TaskItem, newStatus: number) {
    this.taskService.changeStatus(task.id, newStatus).subscribe({
      next: () => this.loadTasks(),
      error: err => {
        alert(err.error?.error || 'Cambio de estado no permitido');
      }
    });
  }

  openCreateDialog() {
  const dialogRef = this.dialog.open(CreateTaskDialogComponent, {
    data: this.users
  });

  dialogRef.afterClosed().subscribe(result => {
    if (!result) return;

    this.taskService.createTask(result).subscribe({
      next: () => this.loadTasks(),
      error: err => alert(err.error?.error || 'Error al crear tarea')
    });
  });
}


}
