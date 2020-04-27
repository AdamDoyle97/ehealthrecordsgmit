import { Injectable } from '@angular/core';
import { StorageService } from './storage.service';
import { UserRoles } from '../_enums/userRoles';

@Injectable({
  providedIn: 'root'
})
export class PermissionService {
  constructor(private storageService: StorageService) { }
  private USER_ROLE_CONST = 'USER_ROLE_ID';
  get(): UserRoles {
    const roleId = this.storageService.get(this.USER_ROLE_CONST);
    switch (roleId) {
      case '1':
        return UserRoles.Admin;
      case '2':
        return UserRoles.Doctor;
      case '3':
        return UserRoles.Patient;
      default:
        break;
    }
  }
  hold(roleId: number) {
    this.storageService.add(this.USER_ROLE_CONST, roleId.toString());
  }


}
