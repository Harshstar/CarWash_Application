import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '../auth/auth.service'; // Import AuthService

@Injectable({
  providedIn: 'root'
})
export class WasherService {
  private http = inject(HttpClient);
  private authService = inject(AuthService); // Inject AuthService
  private apiUrl = 'http://localhost:5007/api/WashReq/'; // Update with your actual API

  constructor() {}

  logout()
  {
    return this.authService.logout();
  }
  
  getWashReqDetails(): Observable<any> {
    const washerId = this.authService.getUserId(); // Get customerId from token
    if (!washerId) {
        console.error(" No washer ID found! Cannot fetch washrequest details.");
        return new Observable(); // Return empty observable if customer ID is missing
    }

    console.log(`🚀 Fetching wash request for washer ID: ${washerId}`); //  Debugging

    return this.http.get<any>(`${this.apiUrl}Get-WashRequest-By-washerId/${washerId}`);
    }


    

    
}

