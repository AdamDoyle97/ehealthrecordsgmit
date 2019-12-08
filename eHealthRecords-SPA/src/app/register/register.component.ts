import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  model: any = {};
  constructor(private authService: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {
  }
 // registration successful
  register() {
    this.authService.register(this.model).subscribe(() => {
      console.log('successful');
      this.alertify.success('register successful');
    }, error => {
      console.log(error);
      this.alertify.error(error);
    });

  }
// cancel registertation
  cancel() {
    this.cancelRegister.emit(false);
  }

}
