import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '../auth/auth.service'; // Import AuthService

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  private http = inject(HttpClient);
  private authService = inject(AuthService); // Inject AuthService
  private apiUrl = 'http://localhost:5007/api/Car/'; // Update with your actual API

  private userUrl = 'http://localhost:5007/api/User/';
  private packageUrl = 'http://localhost:5007/api/Package/';
  private promoUrl = 'http://localhost:5007/api/PromoCode/';
  private addressUrl = 'http://localhost:5007/api/Address/';

  private washrequestUrl = 'http://localhost:5007/api/WashReq/';
  private paymentUrl = 'http://localhost:5007/api/Payment/';

  constructor() {}

  logout()
  {
    return this.authService.logout();
  }

  getUserId()
  {
    return this.authService.getUserId();
  }

  

      getAddressByCity(city: string) : Observable<any>{
        console.log(city);
        return this.http.get<any>(`${this.addressUrl}Get-Address-By-City/${city}`);
      }

      getUserById(userId: string): Observable<any>{
        return this.http.get<any>(`${this.userUrl}Get-User-By-Id/${userId}`);
      }


      getAllPackages(): Observable<any>{
        return this.http.get<any>(`${this.packageUrl}GetAllPackage`);
      }
      
      getAllPromoCodes(): Observable<any>{
        return this.http.get<any>(`${this.promoUrl}GetAllPromoCode`);
      }

      gettAddressByUserId(): Observable<any>{
        const userId = this.authService.getUserId(); // Get customerId from token
        if (!userId) {
            console.error(" No Customer ID found! Cannot fetch car details.");
            return new Observable(); // Return empty observable if customer ID is missing
        }

        console.log(`Fetching address for customer ID: ${userId}`); //  Debugging
        return this.http.get<any>(`${this.addressUrl}Get-Address-By-userId/${userId}`);
      }
      


      getCarDetails(): Observable<any> {
        const customerId = this.authService.getUserId(); // Get customerId from token
        if (!customerId) {
            console.error(" No Customer ID found! Cannot fetch car details.");
            return new Observable(); // Return empty observable if customer ID is missing
        }

        console.log(`Fetching cars for customer ID: ${customerId}`); //  Debugging

        return this.http.get<any>(`${this.apiUrl}Get-Car-By-CustId/${customerId}`);
        }


      getLatestWashRequest(): Observable<any> {

        const customerId = this.authService.getUserId();
        return this.http.get<any>(`${this.washrequestUrl}Get-LatestWashRequest-By-CustId/${customerId}`);
      }
    
    
      getCustomerWashRequests(): Observable<any> {

        const customerId = this.authService.getUserId();
        return this.http.get<any>(`${this.washrequestUrl}GetCustomerWashRequests/${customerId}`);
      }


      addCar(carData: any): Observable<any> {   
        const customerId = this.authService.getUserId();  // Retrieve the customer ID from wherever it's stored (e.g., auth service)
        const carDataWithCustId = { 
              ...carData, 
              custId: customerId 
        };
        
        console.log(carDataWithCustId);
        return this.http.post<any>(`${this.apiUrl}add-car`, carDataWithCustId);
      }

      deleteCar(carId: number): Observable<any> {
        console.log(carId);
        return this.http.delete(`${this.apiUrl}delete-car/${carId}`);
      }
      

      isWasherAvailable(washerId: string, pickupDate: string) {
        return this.http.get<boolean>(`${this.washrequestUrl}IsWasherAvailable/${washerId}/${pickupDate}`);
      }

    

      requestWash(washdata:any) : Observable<any> {
        console.log('waseddrdrhdata',washdata);
        return this.http.post<any>(`${this.washrequestUrl}add-washrequest`,washdata);
      }
      
      createCheckoutSession(paymentData: any): Observable<any> {
        console.log('checkout data',paymentData);
        return this.http.post<any>(`${this.paymentUrl}create-checkout-session`, paymentData);
      }


      addPayment(paymentData: any): Observable<any> {
        console.log('Paymentdata',paymentData);
        // localStorage.setItem('paymentData',paymentData);
        return this.http.post<any>(`${this.paymentUrl}add-payment`, paymentData);
      }
}

