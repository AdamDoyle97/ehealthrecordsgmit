import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {}; // will store username and password

  constructor(public authService: AuthService , private alertify: AlertifyService,
              private router: Router) { }

  ngOnInit() {
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
    this.alertify.message('logged out');
    this.router.navigate(['/home']);
  }

}
