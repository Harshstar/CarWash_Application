import { Routes } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { SignupComponent } from './auth/signup/signup.component';
import { AuthComponent } from './auth/auth.component';
import { CustomerComponent } from './customer/customer.component';
import { WasherComponent } from './washer/washer.component';
import { AdminComponent } from './admin/admin.component';
import { HomeComponent } from './home/home.component';
import { AccountComponent } from './account/account.component';
import { AuthGuard } from './auth/guard/auth.guard';
import { ForgotComponent } from './auth/forgot-password/forgot-password.component';

export const routes: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    
    { 
        path: 'auth',
        component: AuthComponent, 
        children: [
            { path: 'login', component: LoginComponent },
            { path: 'signup', component: SignupComponent },
            { path: 'forgot-password', component: ForgotComponent }
        ]
    },

    { path: 'home', component: HomeComponent },

    { 
        path: 'customer', 
        component: CustomerComponent, 
        canActivate: [AuthGuard], 
        data: { roles: 'Customer' }
    },

    { 
        path: 'washer', 
        component: WasherComponent, 
        canActivate: [AuthGuard], 
        data: { roles: 'Washer' }
    },

    { 
        path: 'admin', 
        component: AdminComponent, 
        canActivate: [AuthGuard], 
        data: { roles: 'Admin' }
    },

    { 
        path: 'account', 
        component: AccountComponent, 
        canActivate: [AuthGuard] 
    },


    // { path: 'unauthorized', component: UnauthorizedComponent }, 

    { path: '**', redirectTo: 'home' } // Redirect unknown routes
];
