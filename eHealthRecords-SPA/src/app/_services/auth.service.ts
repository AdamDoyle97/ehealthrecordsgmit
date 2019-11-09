import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {map} from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseURL = 'http://localhost:5000/api/auth/';
  jwtHelper = new JwtHelperService();
  decodeToken: any;

constructor(private http: HttpClient) { }

login(model: any) {
  return this.http.post(this.baseURL + 'login', model)
    .pipe(
      map((response: any) => {
        const user = response;
        if (user) {
          localStorage.setItem('token', user);
          this.decodeToken = this.jwtHelper.decodeToken(user.token);
          console.log(this.decodeToken);
        }
      })
    );
}

register(model: any) {
  return this.http.post(this.baseURL + 'register', model);
}

loggedIn() {
  const token = localStorage.getItem('token');
  return this.jwtHelper.isTokenExpired(token);
}

}
