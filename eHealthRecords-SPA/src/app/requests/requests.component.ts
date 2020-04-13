import { Component, OnInit } from '@angular/core';
import { UserService } from '../_services/user.service';
import { User } from '../_models/user';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-requests',
  templateUrl: './requests.component.html',
  styleUrls: ['./requests.component.css']
})
export class RequestsComponent implements OnInit {
  constructor(private userService: UserService, private alertify: AlertifyService) { }
  userList: User[];
  ngOnInit() {
   this.getAllUserRequests();
  }
getAllUserRequests() {
  this.userService.getUsersRequest()
  .subscribe(data => {this.userList = data; });
}
giveRole(Id: number, role: number) {
  const user = this.userList.find(x => x.id === Id);
  user.roleId = role;
  this.userService.updateRoleId(user)
   .subscribe(() => {
     const index =  this.userList.indexOf(user);
     this.userList.splice(index, 1);
     this.alertify.success('Role Successfully Updated');
   });
}
}
