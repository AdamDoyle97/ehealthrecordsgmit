import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(
    private authService: AuthService,
    private router: Router,
    private alertify: AlertifyService
    ) {}
// if user is logged can navigate through webpage
  canActivate(): boolean {
    if (this.authService.loggedIn()) {
      return true;
    }
// alert message to notify user to log-in
    this.alertify.error('Unauthorization, please log-in');
    this.router.navigate(['/home']);
    return false;
  }
}
