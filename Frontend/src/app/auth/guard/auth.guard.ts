import { inject, Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, GuardResult, MaybeAsync, Router, RouterStateSnapshot } from "@angular/router";
import { jwtDecode } from "jwt-decode";
 
@Injectable({
  providedIn : "root"
})
export class AuthGuard implements CanActivate{
  private router = inject(Router);
 
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
   
    const token = localStorage.getItem("authToken");
    if(!token){
 
      this.router.navigate(["/auth/login"]);
      return false;
    }
 
    try{
      const decodedToken : any= jwtDecode(token);
      const userRole = decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
     
      const allowedRoles = route.data["roles"] as string[]; // route based authorization
 
      if(allowedRoles && !allowedRoles.includes(userRole)){
        this.router.navigate(["/auth/login"]);
        return false;
      }
      
      return true;
    }catch(error){
        localStorage.removeItem("authToken");
        this.router.navigate(["/auth/login"]);
        return false;
    }
  }
}