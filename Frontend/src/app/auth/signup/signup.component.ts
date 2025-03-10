import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, Validators, ValidationErrors, ValidatorFn,AbstractControl   } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { AuthService } from '../auth.service';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';

const passwordsMatchValidator: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {
  const form = control as FormGroup; // Cast to FormGroup
  const password = form.get('password')?.value;
  const confirmPassword = form.get('confirmPassword')?.value;
  return password === confirmPassword ? null : { mismatch: true };
};

@Component({
  selector: 'app-signup',
  imports: [ReactiveFormsModule,RouterLink,CommonModule],
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.css'
})
export class SignupComponent {
  private authService= inject(AuthService);

   form=new FormGroup({
     email: new FormControl('', [Validators.required, Validators.email]),
     password: new FormControl('', [Validators.required, Validators.minLength(6)]),
     confirmPassword: new FormControl('', [Validators.required]),
     role: new FormControl('', [Validators.required]),
     name: new FormControl('', [Validators.required, Validators.minLength(3)]),
     mobile: new FormControl('', [Validators.required, Validators.pattern('^[0-9]{10}$')])
   },{ validators: passwordsMatchValidator });


   

    

   onSubmit(){
   console.log(this.form.controls.email.value);
   
    this.authService.signupUser(this.form.controls.email.value,this.form.controls.password.value,this.form.controls.role.value,this.form.controls.name.value,this.form.controls.mobile.value).subscribe();
   }

   onclick(){
    // const role = localStorage.getItem
   }
}
