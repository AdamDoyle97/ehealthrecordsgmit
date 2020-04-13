import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class StorageService {
  constructor() {}
   get(key: string): string {
     return localStorage.getItem(key);
   }
   add(key: string, value: string) {
     localStorage.setItem(key, value);
   }
}
