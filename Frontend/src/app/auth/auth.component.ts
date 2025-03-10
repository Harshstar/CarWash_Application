import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { LoginComponent } from "./login/login.component";
import { RouterModule } from '@angular/router';


@Component({
  selector: 'app-auth',
  imports: [RouterOutlet, RouterModule],
  templateUrl: './auth.component.html',
  styleUrl: './auth.component.css'
})
export class AuthComponent {

}


