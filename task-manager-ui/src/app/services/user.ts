import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../models/user.model';

@Injectable({ providedIn: 'root' })

export class UserService {

  private baseUrl = 'https://localhost:7197/api/users';

  constructor(private http: HttpClient) {}

  getUsers() {
    return this.http.get<User[]>(this.baseUrl);
  }
}
