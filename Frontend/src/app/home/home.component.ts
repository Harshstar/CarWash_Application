import { Component, inject } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../auth/auth.service';

@Component({
  selector: 'app-home',
  imports: [],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent{
  
  private authService = inject(AuthService);
  private router = inject(Router);
  handleLogin() {
    if (this.authService.isAuthenticated()) {
      const userRole = this.authService.getUserRole();

      if (userRole === 'Customer') {
        this.router.navigate(['/customer']);
      } else if (userRole === 'Washer') {
        this.router.navigate(['/washer']);
      } else if (userRole === 'Admin') {
        this.router.navigate(['/admin']);
      }
    } else {
      this.router.navigate(['/auth/login']);
    }
  }
}
