import { Component, inject } from '@angular/core';
import { AuthService, OTP, ResetPass } from '../auth.service';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
 
@Component({
  selector: 'app-forgot',
  imports: [ReactiveFormsModule,RouterLink,CommonModule],
  templateUrl: './forgot-password.component.html',
  styleUrl: './forgot-password.component.css'
})
export class ForgotComponent {
   private authService = inject(AuthService);
   private router=inject(Router);
   showPass=false;
 
     otpForm = new FormGroup({
      email : new FormControl('', [Validators.required, Validators.email]),
      //password : new FormControl('', [Validators.required])
     });
 
     resetForm = new FormGroup({
      email : new FormControl('', [Validators.required, Validators.email]),
      password : new FormControl('', [Validators.required]),
      otp: new FormControl('', [Validators.required])
     })
   
   
     onSendOtp(){
          if(this.otpForm.invalid){
            this.otpForm.markAllAsTouched();
          }
          const otp: OTP = {
            email: this.otpForm.controls.email.value || ''
          }
 
         this.authService.sendOtp(otp).subscribe({
            next : (response)=>{
              this.showPass=true;
              console.log(response);
            },
            error: (err)=>{
              console.log(err);
              alert('Please enter registered Email');
            }
          })
   
          this.resetForm.patchValue({
            email:otp.email
          })
          this.resetForm.get('email')?.disable();
 
     }
 
     onResend(){
      this.showPass=false;
     }
 
     onReset(){
      if(this.otpForm.invalid){
        this.otpForm.markAllAsTouched();
      }
      const reset : ResetPass = {
         email:this.resetForm.controls.email.value || '',
         newPassword:this.resetForm.controls.password.value || '',
         otp:this.resetForm.controls.otp.value || ''
      }
     
      this.authService.resetPassword(reset).subscribe({
        next : (response)=>{
          alert('Password reset successfully');
          this.router.navigate(["/auth/login"]);
        },
        error: (err)=>{
          console.log(err);
          alert('Something went wrong please try again');
        }
      })
 
     }
 
     reRoute(){
      this.router.navigate(["/auth/login"]);
     }
}

