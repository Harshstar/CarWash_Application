import { Component } from '@angular/core';
import { AccountService } from './account.service';
import { RouterLink } from '@angular/router';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Modal } from 'bootstrap';

@Component({
  selector: 'app-account',
  imports: [RouterLink,ReactiveFormsModule , CommonModule],
  templateUrl: './account.component.html',
  styleUrl: './account.component.css'
})
export class AccountComponent {
  user: any = {};
  address: any = {};
  private modal!: Modal;
  isUpdateMode: boolean = false;

  constructor(private accountService: AccountService) {}
  
  addressForm = new FormGroup({
    doorNumber: new FormControl('', [Validators.required]),
    street: new FormControl('', [Validators.required]),
    area: new FormControl('', [Validators.required]),
    landmark: new FormControl('', [Validators.required]),
    city: new FormControl('', [Validators.required]),
    state: new FormControl('', [Validators.required]),
    pincode: new FormControl('', [
      Validators.required,
      Validators.pattern('^[0-9]{6}$') 
    ])
  });


  logout(){
    this.accountService.logout();
  }

  ngOnInit() : void {
    this.fetchUsers();
    this.fetchAddress();
  };

  navigating(){
    this.accountService.navigating();
  }

  fetchUsers(): void {
    this.accountService.getUsersById().subscribe({
      next: (data) =>{
        console.log("User fetched",data);
        this.user = data;
      },
      error: (err) => {
        console.error('Error ', err);
      }
    })
  }



  fetchAddress() : void {
    this.accountService.getAddressByUserId().subscribe({
      next : (data) =>{
        console.log('Address fetched',data),
        this.address = data;
      },
      error: (err) => { 
        console.log('Error', err);
      }
    })
  }


  openAddAddressDialog(): void {
      this.isUpdateMode = false;
      const modalElement = document.getElementById('addAddressModal');
      if (modalElement) {
        this.addressForm.reset();
        this.modal = new Modal(modalElement);
        this.modal.show();
      }
    }


  saveAddress() {
    if (this.address.invalid) return;

    const addressData = this.addressForm.value;
    console.log('getting address from the form',addressData);
    if(this.isUpdateMode === true)
    {
      console.log('updated value is getting trigger',addressData);
      this.accountService.updateAddress(addressData).subscribe(() =>{
        this.fetchAddress();
        this.addressForm.reset();
        this.modal.hide();
      })
    }
    else{
    this.accountService.addAddress(addressData).subscribe(() => {
      this.fetchAddress();
      this.addressForm.reset();
      this.modal.hide();
    });
  }
  }


  openUpdateAddressDialog(){
    this.isUpdateMode = true;
    // Pre-fill the form with existing address details
    this.addressForm.patchValue(this.address);
    const modalElement = document.getElementById('addAddressModal');
      if (modalElement) {
        this.addressForm.reset();
        this.modal = new Modal(modalElement);
        this.modal.show();
      }
  }
}
