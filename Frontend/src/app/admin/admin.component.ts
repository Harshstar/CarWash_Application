import { Component } from '@angular/core';
import { AdminService } from './admin.service';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-admin',
  imports: [ReactiveFormsModule, CommonModule, RouterLink],
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.css'
})
export class AdminComponent {
   users: any[] =[];
  constructor(private adminService: AdminService) {}

  ngOnInit() : void {
    this.fetchUsers();
  };
  
  logout()
  {
    this.adminService.logout();
  }

  fetchUsers(): void {
    this.adminService.getUsers().subscribe({
      next: (data) =>{
        console.log("User fetched",data);
        this.users = data;
      },
      error: (err) => {
        console.error('Error ', err);
      }
    })
  }


  deleteUser(userId: string): void {
    if (confirm('Are you sure you want to delete this user?')) {
      this.adminService.deleteUser(userId).subscribe({
        next: () => {
          console.log(`User ${userId} deleted successfully`);
          this.users = this.users.filter(user => user.id !== userId);
          // this.users.splice(index, 1); // Remove user from UI
        },
        error: (err) => {
          console.error('Error deleting user', err);
        }
      });
    }
  }


}