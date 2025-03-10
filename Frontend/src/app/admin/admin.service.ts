import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '../auth/auth.service'; // Import AuthService

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  private http = inject(HttpClient);
  private authService = inject(AuthService); // Inject AuthService
  private apiUrl = 'http://localhost:5007/api/User/'; // Update with your actual API

  constructor() {}

  logout()
  {
    return this.authService.logout();
  }   
  getUsers(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}GetAllUser`);
  }


  deleteUser(userId: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}delete-user/${userId}`);
  }

}