import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { map, Observable } from 'rxjs';
import { AccountService } from '../_Services/account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private accountsService : AccountService){}
  
  canActivate(): Observable<boolean> {
    return this.accountsService.currentUser$.pipe(
      map( user =>{
          if(user) return true;
          else{
          console.error("you shall not passed");
            return false;
          }
        }
      )
    )
  }
  
}
