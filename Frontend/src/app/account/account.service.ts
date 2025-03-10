import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '../auth/auth.service'; // Import AuthService
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})

export class AccountService {
    private http = inject(HttpClient);
    private authService = inject(AuthService);
    private router = inject(Router);
    private apiUrl = 'http://localhost:5007/api/User/'; // Update with your actual API
    private addressUrl = 'http://localhost:5007/api/Address/';

    constructor() {}

    logout()
    {
        return this.authService.logout();
    }
    navigating()
    {
        const userRole = this.authService.getUserRole();
        if(userRole === "Customer")
            this.router.navigate(["/customer"]);
        if(userRole === "Washer")
            this.router.navigate(["/washer"]);
        if(userRole === "Admin")
            this.router.navigate(["/admin"]);
    }
    getUsersById(): Observable<any> {
        const userId = this.authService.getUserId(); // Get userId from token
        if (!userId) {
            console.error(" No user ID found! Cannot fetch user details.");
            return new Observable(); // Return empty observable if user ID is missing
        }
    
        console.log(` Fetching user details for user ID: ${userId}`); //  Debugging
    
        return this.http.get<any>(`${this.apiUrl}Get-User-By-Id/${userId}`);
        }
    
    getAddressByUserId(): Observable<any> {
        const userId = this.authService.getUserId();
        if (!userId) {
            console.error(" No user ID found! Cannot fetch user details.");
            return new Observable(); // Return empty observable if user ID is missing
        }
        console.log(userId);
        return this.http.get<any>(`${this.addressUrl}Get-Address-By-userId/${userId}`);
    }

    addAddress(addressData: any): Observable<any> {   
        const userId = this.authService.getUserId();  // Retrieve the customer ID from wherever it's stored (e.g., auth service)
        const addressDataWithUserId = { 
              ...addressData, 
              userId: userId 
        };
        
        console.log('before passing the address into api....address with user id',addressDataWithUserId);
        return this.http.post<any>(`${this.addressUrl}add-address`, addressDataWithUserId,  );
     }
     
     
     updateAddress(addressData : any) : Observable<any> {
        const userId = this.authService.getUserId();
        const addressDataWithUserId = { 
            ...addressData, 
            userId: userId 
      };
      console.log('addressData',addressDataWithUserId);
        return this.http.put<any>(`${this.addressUrl}update-addressByUserId/${userId}`,addressDataWithUserId);
     }
}


//