import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';
import { PermissionService } from '../_services/permission.service';
import { UserRoles } from '../_enums/userRoles';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {}; // will store username and password
  photoUrl: string;
  userId: number;
  constructor(public authService: AuthService , private alertify: AlertifyService,
              private router: Router, private permissionService: PermissionService) { }

  ngOnInit() {
    this.authService.currentPhotoUrl.subscribe(photoUrl => this.photoUrl = photoUrl);
    this.userId = this.authService.decodedToken.nameid;
  }
// login method the shows successful login

  login() {
    this.authService.login(this.model).subscribe(next => {
    this.alertify.success('logged in successfully');
    }, error => {
      console.log(error);
      this.alertify.error(error);
    }, () => {
      this.router.navigate(['/members']);
    });
  }

// if token is empty it will return false
  loggedIn() {
    const token = localStorage.getItem('token');
    return !!token;
    // return this.authService.loggedIn();
  }

// removes token so user logs out
  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.authService.decodedToken = null;
    this.authService.currentUser = null;
    this.alertify.message('logged out');
    this.router.navigate(['/home']);
  }

  isAdmin(): boolean {
    return this.permissionService.get() === UserRoles.Admin;
  }
}
