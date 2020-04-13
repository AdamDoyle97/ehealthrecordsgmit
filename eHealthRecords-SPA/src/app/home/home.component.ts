import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;

  constructor(private http: HttpClient) { }

  ngOnInit() {
  }

  // register method
  registerToggle() {
    this.registerMode = true;
  }

  // cancel method
  cancelRegisterMode(registerMode: boolean) {
    this.registerMode = registerMode;
  }
}
