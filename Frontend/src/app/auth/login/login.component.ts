import { Component, inject } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { AuthService } from '../auth.service';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import {MatSnackBar, MatSnackBarModule} from '@angular/material/snack-bar';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule,RouterModule,CommonModule,MatSnackBarModule ],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  private authService = inject(AuthService);
  private snackBar = inject(MatSnackBar);
  
  ngOnInit() : void {
    // this.showToast();
  }
  form = new FormGroup({
   email : new FormControl('', [Validators.required, Validators.email]),
   password : new FormControl('', [
    Validators.required, 
    Validators.minLength(6)
  ])

  });

  showToast(message: string, type: 'success' | 'error') {
    this.snackBar.open(message, 'Close', {
      duration: 3000,
      horizontalPosition: 'left',
      verticalPosition: 'bottom',
      panelClass: type === 'success' ? 'toast-success' : 'toast-error'
    });
  }



  onSubmit(){


    // console.log(this.form.controls.email.value);
       this.authService.loginUser(this.form.controls.email.value, this.form.controls.password.value)
       .subscribe({
        next : (data) =>{
          this.showToast('Welcome to car wash','success');
        },
        error : (err)=>{
          alert('Please enter valid credentials');
        }
       });
  }
}