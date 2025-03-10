import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Form, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { jwtDecode } from "jwt-decode";
import { map, Observable } from 'rxjs';
 
@Injectable({
  providedIn: 'root'
})
export class AuthService {
 
  private httpClient = inject(HttpClient);
  private router = inject(Router);
  private baseUrl = "http://localhost:5007";
 
  loginUser(email: string | null, password : string | null){
    return this.httpClient.post<LoginResponse>(this.baseUrl+ "/api/Auth/Login", {
      "username" : email,
      "password" : password,
    }).pipe(
      map((response:LoginResponse) => {
 
        console.log(response);
       
       
        if(response && response.jwtToken){
          localStorage.setItem("authToken", response.jwtToken);
          
        }
          this.getUserRole();
          const userRole = this.getUserRole();
          console.log(userRole);
         
 
          if(userRole == "Customer")
          {
            // console.log("Navigating to /customer");
            this.router.navigate(["/customer"]);
          }
          else if(userRole == "Washer")
          {
            console.log("Navigating to /washer");
            this.router.navigate(["/washer"]);
          }
          else if(userRole == "Admin")
          {
            console.log("Navigating to /admin");
            this.router.navigate(["/admin"]);
          }
      })
 
 
    )
  }
 
 
  signupUser(email : string | null, password : string | null  , role: string | null, name : string | null , mobile : string | null){
      return this.httpClient.post(this.baseUrl + "/api/Auth/Register", {
          "username": email,
          "password": password,
          "roles": [
            role
          ],
          "name": name,
          "mobileNumber": mobile
        },{responseType : "text"}
      ).pipe(
        map((response) => {
          console.log(response);
          this.router.navigate(["/auth/login"]);
        })
      )
  }
 
  isAuthenticated() : boolean{
    const token = localStorage.getItem("authToken");
    if(token != null){
      return true;
    }
   
    return false;
  }
 
 
  getUserRole() : string | null{
    const token = localStorage.getItem("authToken");
   
    if(token != null){
      const decodedToken : any = jwtDecode(token);
      console.log(decodedToken);
     
      return decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
    }
    return null;
  }


  sendOtp(email:OTP):Observable<any>{
    return this.httpClient.post<any>(`http://localhost:5007/api/Auth/forgot-password`,email);
 }

 resetPassword(newPass:ResetPass):Observable<any>{
   return this.httpClient.post<any>(`http://localhost:5007/api/Auth/reset-password`,newPass);
 }





  getUserId(): string | null {
    const token = localStorage.getItem("authToken");
    if (token) {
        const decodedToken: any = jwtDecode(token);
        console.log("🔍 Decoded Token:", decodedToken); // Debugging

        // Extract customerId from token
        const customerId = decodedToken["customerId"];
        console.log("🆔 Extracted customerId:", customerId); //  Debugging
        return customerId || null;
    }
    return null;
}

  
 
 
  logout() {
    localStorage.removeItem("authToken");
    this.router.navigate(["/home"]);
  }
 
}
 
interface LoginResponse {
  jwtToken: string;
}

export interface OTP{
  email:string
}
 
export interface ResetPass{
  email:string,
  newPassword:string,
  otp:string
}



//   sadsadsaddassdsda