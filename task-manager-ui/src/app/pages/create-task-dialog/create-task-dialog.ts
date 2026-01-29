import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';


import { User } from '../../models/user.model';

@Component({
  selector: 'app-create-task-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatDialogModule
  ],
  templateUrl: './create-task-dialog.html',
  styleUrl: './create-task-dialog.css',
})
export class CreateTaskDialogComponent {

  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<CreateTaskDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public users: User[]
  ) {
    this.form = this.fb.group({
      title: ['', Validators.required],
      description: [''],
      userId: [null, Validators.required],
      priority: ['']
    });
  }

  submit() {
    if (this.form.invalid) return;

    const value = this.form.value;

    const payload = {
      title: value.title,
      description: value.description,
      userId: value.userId,
      additionalData: {
        priority: value.priority
      }
    };

    this.dialogRef.close(payload);
  }

  cancel() {
    this.dialogRef.close();
  }
}
