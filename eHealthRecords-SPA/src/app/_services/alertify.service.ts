import { Injectable } from '@angular/core';
import * as alertify from 'alertifyjs';
// declare let alertify: any;

@Injectable({
  providedIn: 'root'
})
export class AlertifyService {
  constructor() {}

  confirm(message: string, okCallback: () => any) {
    alertify.confirm(message, (e: any) => {
      if (e) {
        okCallback();
      } else {}
    });
   }

   // success message prompt
   success(message: string) {
     alertify.success(message);
   }
// erro message prompt
   error(message: string) {
    alertify.error(message);
  }
// warning message prompt
  warning(message: string) {
    alertify.warning(message);
  }
// normal message prompt (logout)
  message(message: string) {
    alertify.message(message);
  }
}
