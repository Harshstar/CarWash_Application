import { Component, OnInit } from '@angular/core';
import { CustomerService } from './customer.service'; 
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, Validators, Form } from '@angular/forms';
import { Modal } from 'bootstrap';
import { ReactiveFormsModule } from '@angular/forms';
import { AuthService } from '../auth/auth.service';
import { RouterLink } from '@angular/router';
import { loadStripe, Stripe } from '@stripe/stripe-js';

@Component({
  selector: 'app-customer',
  imports: [CommonModule,ReactiveFormsModule , RouterLink],
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.css']
})
export class CustomerComponent implements OnInit {
  // Replace with actual customer ID (can come from authentication)

  carDetails: any[] = [];
  washerss: any[] =[];
  packages: any[] = [];
  promoCodes: any[] = [];
  selectedWasher : any;
  selectedPackageDetails: any = null;
  selectedPromoCode: any = null;
  selectedCarDetails: any = null;
  addressDetails: any = null;
  stripe: Stripe | null = null;
  isPromoCodeAvailable: boolean = false;
  eligiblePromoCodes: any[] = [];
  latestWashRequest: any = null;
  errorMessage: string = "";
  washRequests: any[] = [];
  washerName : string = "";
  status : string = "";
  customerName : string = "";

  form = new FormGroup({
    company: new FormControl('' , [Validators.required]),
    model: new FormControl('', [Validators.required]),
    number: new FormControl('', [Validators.required])
  });

  packageForm = new FormGroup({
    selectedPackage: new FormControl('', [Validators.required]),
    selectedPromoCode :new FormControl(''),
    selectedCar : new FormControl('', [Validators.required]),
    deliveryDate : new FormControl('', [Validators.required]),
    washType: new FormControl('', [Validators.required]),  
    notes: new FormControl('', [Validators.required])
  });

  private modal!: Modal;

  constructor(private customerService: CustomerService) {}

  ngOnInit(): void {
    this.fetchCarDetails();
    this.fetchPackages();
    this.fetchPromoCodes();
    this.fetchAddress();
    this.fetchLatestWashRequest();
    this.loadWashRequests();
    this.fetchcustomername();
  }

  logout()
  {
    this.customerService.logout();
  }

  
  fetchcustomername(){
    const customerid = this.customerService.getUserId();
    if(customerid){
    this.customerService.getUserById(customerid).subscribe({
      next : (data) =>{
        this.customerName = data.name;
      }
    })
  }
  }

  openModal(): void {
    const modalElement = document.getElementById('addCarModal');
    if (modalElement) {
      this.modal = new Modal(modalElement);
      this.modal.show();
    }
  }

  

  fetchAddress(): void {
    this.customerService.gettAddressByUserId().subscribe({
      next: (data) =>{
        console.log('address data',data);
        this.addressDetails = data;
        if(this.addressDetails && this.addressDetails.city)
        {
          this.fetchWashersByAddress(this.addressDetails.city);
        }
      },
      error: (err) =>{
        console.error('Error fetching address details',err);
      }
    });
  }


  fetchWashersByAddress(city : string) : void {
    this.customerService.getAddressByCity(city).subscribe({
      next : (data) => {
        console.log('data received from fetch address',data);
        this.washerss = Array.isArray(data) ? data : [data];
        console.log(this.washerss);
        this.washerss = this.washerss.filter(x=>x.user && x.user.role.toLowerCase() === 'washer');
        console.log('this.washerss',this.washerss);
        if(this.washerss)
          console.log('fetched filtered address',this.washerss);
        else
          console.log('no address found');
      },
      error: (err) => {
        console.error('Error fetching address by city details', err);
      }
    })
  }



  


  fetchPackages(): void {
    this.customerService.getAllPackages().subscribe({
      next: (data) => {  
        console.log(data);
        this.packages = data;
      },
      error: (err) => console.error('Error fetching packages', err)
    });
  }
 


  fetchPromoCodes() {
    this.customerService.getAllPromoCodes().subscribe(data => {
      this.promoCodes = data;
    });
  }
  

  // Fetch car details from the service
  fetchCarDetails(): void {
    this.customerService.getCarDetails().subscribe({
      next: (data) => {
        console.log(data);
        this.carDetails = data; // Set the car details data
      },
      error: (err) => {
        console.error('Error fetching car details', err);
      }
    });
  }

  loadWashRequests(): void {
    this.customerService.getCustomerWashRequests().subscribe({
      next: (data) => {
        console.log('customer wash requests',data);
        this.washRequests = data; // Set the details data

        this.washRequests.forEach((request) => {

          request.status = new Date(request.deliveryDate) > new Date() ? 'Pending' : 'Completed';


          if (request.washerId) {
            this.customerService.getUserById(request.washerId).subscribe({
              next: (washerData) => {
                request.washerName = washerData.name; // Attach washer name
                
              },
              error: (err) => {
                console.error(`Error fetching washer details for ID ${request.washerId}:`, err);
              }
            });
          }
        });
      },
      error: (err) => {
        console.error('Error fetching wash requests:', err);
      }
    });
  }

    

  onAddCar(): void {
    if (this.form.invalid) return;


    const carData = this.form.value;
    console.log("Sending car data:", carData); 



    this.customerService.addCar(this.form.value).subscribe({
      next: (newCar) => {


        console.log("Car added successfully:", newCar);
        this.fetchCarDetails();

        this.carDetails.push(newCar); // Update UI dynamically
        this.form.reset();
        this.modal.hide(); // Close 
      },
      error: (err) => {
        console.error('Error adding car', err);
      }
    });
  }


  deleteCar(carId: number): void {
    if (confirm('Are you sure you want to delete this car?')) {
      this.customerService.deleteCar(carId).subscribe({
        next: () => {
          console.log('Car deleted successfully');
          alert('Car deleted successfully');
          this.carDetails = this.carDetails.filter(car => car.id !== carId); // Remove deleted car from UI
        },
        error: (err) => {
          this.errorMessage = err.error.message || "Car cannot be deleted as it has an active wash request.";
          alert("Warning: Car cannot be deleted because it has an active wash request!");
          console.error('Error deleting car', err);
        }
      });
    }
  }


  openPackageModal(washer: any): void {
    if (!this.addressDetails) {
      const warningModalElement = document.getElementById('addressWarningModal');
      if (warningModalElement) {
        const warningModal = new Modal(warningModalElement);
        warningModal.show();
      }
      return;
    }
    this.selectedWasher = washer;
    localStorage.setItem('washer id',this.selectedWasher);
    this.packageForm.reset();
    this.selectedPackageDetails = null; // Reset previous selection
    this.selectedPromoCode = null; // Reset previous selection
    this.selectedCarDetails = null;

    const modalElement = document.getElementById('selectPackageModal');
    if (modalElement) {
      this.modal = new Modal(modalElement);
      this.modal.show();
    }
  }


  updateCarDetails(): void {
    const selectedCarId = this.packageForm.value.selectedCar;
    this.selectedCarDetails = this.carDetails.find(car => car.id === selectedCarId);
  }


  updatePackageDetails(): void {
    const selectedPackageId = this.packageForm.value.selectedPackage;
    this.selectedPackageDetails = this.packages.find(pack => pack.id === selectedPackageId);
    
    if (this.selectedPackageDetails) {
      this.eligiblePromoCodes = this.promoCodes.filter(promo => this.selectedPackageDetails.packPrice >=promo.minVal);
      this.isPromoCodeAvailable = this.eligiblePromoCodes.length > 0;
    } else {
      this.isPromoCodeAvailable = false;
    }
  }

  fetchLatestWashRequest(): void {
    this.customerService.getLatestWashRequest().subscribe({
      next: (data) => {
        console.log(data);
        this.latestWashRequest = data; // Set the car details data
      
      },
      error: (err) => {
        console.error('Error fetching car details', err);
      }
    });
  }
  
  
  async requestPackage() {
    if (!this.selectedCarDetails || !this.selectedPackageDetails || !this.selectedWasher || !this.addressDetails) {
      console.error("Missing required fields for the request.");
      return;
    }
    
    //  Submit Wash Request
  
    const washRequestData = {
      custId: this.customerService.getUserId(),
      carId: this.selectedCarDetails.id,
      washerId: localStorage.getItem('washer id') || '',
      packageId: this.selectedPackageDetails.id,
      addressId: this.addressDetails.id,
      orderedDate: new Date().toISOString(),
      deliveryDate: this.packageForm.value.deliveryDate,
      pickupDate: this.packageForm.value.deliveryDate,
      notes: this.packageForm.value.notes,
      washType: this.packageForm.value.washType
    };
    
    
    
    
    const pickupDate = this.packageForm.value.deliveryDate;
    
    const formattedPickupDate = new Date(pickupDate as string).toISOString(); 
    
    try {
      const updatedwasherid = localStorage.getItem('washer id') || '';
      const isAvailable = await this.customerService.isWasherAvailable(updatedwasherid, formattedPickupDate).toPromise();
      
      if (!isAvailable) 
      {
        alert("Washer is not available on the selected date. Please choose a different date or washer.");
        return;
      }
      
      const washResponse = await this.customerService.requestWash(washRequestData).toPromise();
      console.log("Wash request submitted successfully:", washResponse);
  
      const washRequestId = this.latestWashRequest.id; // Get the correct ID from the response
      console.log('Wash Request ID:', washRequestId);
      // localStorage.setItem('washreqId', washRequestId);

      const promoId = this.packageForm.value.selectedPromoCode;
      let discountAmount = 0;
      let discount = 0;
      const selectedPromo = this.promoCodes.find(promo => promo.id === promoId);
      discount = selectedPromo.discount; 
      discountAmount = (discount/100) * this.selectedPackageDetails.packPrice;
      let finalAmount = this.selectedPackageDetails.packPrice - discountAmount;

      const finalPaymentData = {
        amount: finalAmount,
        method: 'card',
        paymentTime: new Date(),
        custId: this.customerService.getUserId(),
        washerId: localStorage.getItem('washer id') || '',
        promoId: this.packageForm.value.selectedPromoCode || null,
        washRequestId: washRequestId
      };

      
      
      // localStorage.setItem('Payment Data', JSON.stringify(finalPaymentData));
  
      this.customerService.addPayment(finalPaymentData).subscribe({
        next: (paymentResponse) => {
          console.log("Payment recorded successfully:", paymentResponse);
          // localStorage.setItem("PaymentResponse", JSON.stringify(paymentResponse));
        },
        error: (err) => {
          console.error("Error saving payment:", err);
        }
      });
  
      // Prepare Payment Data for Stripe 
      const paymentData = {
        amount: finalAmount * 100, // Convert to cents
        currency: 'usd',
      };
  
      console.log('Amount:', this.selectedPackageDetails.packPrice);
  
      // 3: Create Stripe Checkout Session
      const response = await this.customerService.createCheckoutSession(paymentData).toPromise();
      console.log('Checkout Session Created:', response);
  
      // 4: Load Stripe
      this.stripe = await loadStripe('pk_test_51QqpV5HBRkLVgKYmabvTlF2lLtmObLAnVSpL8GTJOlSm3s3ANgyUqy2mfmCZFKwEh2BKV1PXbWX5hOlA6p8UWCzG00dI7GZsEP');
  
      if (!this.stripe) {
        console.error("Failed to initialize Stripe.");
        return;
      }
      
      // 5: Redirect to Stripe Checkout
      const checkoutResult = await this.stripe.redirectToCheckout({
        sessionId: response.sessionId // Use sessionId, NOT clientSecret
      });
        
      if (checkoutResult.error) {
        console.error("Payment failed:", checkoutResult.error);
        return;
      }
  
    
      
  
    } catch (error) {
      console.error("Error processing request:", error);
    }
  }

}
 



// 