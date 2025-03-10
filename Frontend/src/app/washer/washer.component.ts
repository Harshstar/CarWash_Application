import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { WasherService } from './washer.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-washer',
  imports: [CommonModule,RouterLink],
  templateUrl: './washer.component.html',
  styleUrl: './washer.component.css'
})
export class WasherComponent {
  washrequests: any[] =[];                                 

  constructor(private washerService: WasherService) {}
  ngOnInit(): void {
    this.fetchwashreqDetails();
  }

  logout()
  {
    this.washerService.logout();
  }


  trackById(index: number, request: any): string {
    return request.id; 
}


  fetchwashreqDetails(): void {
    this.washerService.getWashReqDetails().subscribe({
      next: (data) => {
        console.log("sdsdd", data);
        console.log(data[0]);
        if (Array.isArray(data)) 
        {
          this.washrequests = data; // If it's already an array, directly assign
        } 
        else if (data && data.washRequests) 
        {
          this.washrequests = data.washRequests; // Extract the array if wrapped inside a key
        } 
        else 
        {
          console.error(' Expected an array or washRequests key, but got:', data);
          this.washrequests = []; // Fallback to an empty array
        }
      },
      error: (err) => {
        console.error('Error fetching wash request details', err);
      }
    });
  }




}
