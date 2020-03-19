import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
// import { HttpClient } from 'selenium-webdriver/http';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../_models/user';
import { PaginationResult } from '../_models/pagination';
import { map } from 'rxjs/operators';
import { Message } from '../_models/message';

// no need for this as i've included whitelisted domains and blacklistedroutes
const httpOptions = {
  headers: new HttpHeaders({
    Authorization: 'Bearer ' + localStorage.getItem('token')
  })
};

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiUrl;

constructor(private http: HttpClient) { }

getUsers(page?, itemsPerPage?, userParams?, watchListParam?): Observable<PaginationResult<User[]>> {
  const paginationResult: PaginationResult<User[]> = new PaginationResult<User[]>();

  let params = new HttpParams();

  if (page != null && itemsPerPage != null) {
    params = params.append('pageNumber', page);
    params = params.append('pageSize', itemsPerPage);
  }

  if (userParams != null) {
    params = params.append('minAge', userParams.minAge);
    params = params.append('maxAge', userParams.maxAge);
    params = params.append('gender', userParams.gender);
    params = params.append('orderBy', userParams.orderBy);
  }

  if (watchListParam === 'DoctorWatch') {
    params = params.append('DoctorWatch', 'true');
  }

  if (watchListParam === 'PatientWatch') {
    params = params.append('PatientWatch', 'true');
  }

  return this.http.get<User[]>(this.baseUrl + 'users', { observe: 'response', params}).pipe(
    map(response => {
      paginationResult.result = response.body;
      if (response.headers.get('Pagination') != null) {
        paginationResult.pagination = JSON.parse(response.headers.get('Pagination'));
      }
      return paginationResult;
    })
  );
}

getUser(id): Observable<User> {
  return this.http.get<User>(this.baseUrl + 'users/' + id);
}

updateUser(id: number, user: User) {
  return this.http.put(this.baseUrl + 'users/' + id, user);
}

setMainPhoto(userId: number, id: number) {
  return this.http.post(this.baseUrl + 'users/' + userId + '/photos/' + id + '/setMain', {} );
}

deletePhoto(userId: number, id: number) {
  return this.http.delete(this.baseUrl + 'users/' + userId + '/photos/' + id);
}

sendWatch(id: number, recipientId: number) {
  return this.http.post(this.baseUrl + 'users/' + id + '/watch/' + recipientId, {});
}

getMessages(id: number, page?, itemsPerPage?, messageContainer?) {
  const paginationResult: PaginationResult<Message[]> = new PaginationResult<Message[]>();

  let params = new HttpParams();

  params = params.append('MessageContainer', messageContainer);

  if (page != null && itemsPerPage != null) {
    params = params.append('pageNumber', page);
    params = params.append('pageSize', itemsPerPage);
  }

  return this.http.get<Message[]>(this.baseUrl + 'users/' + id + '/messages', {observe: 'response', params})
    .pipe(
      map(response => {
      paginationResult.result = response.body;
      if ( response.headers.get('Pagination') !== null) {
          paginationResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }
      return paginationResult;
      })
    );
}

getMessageThread(id: number, recipientId: number) {
  return this.http.get<Message[]>(this.baseUrl + 'users/' + id + '/messages/thread/' + recipientId);
}

sendMessage(id: number, message: Message) {
  return this.http.post(this.baseUrl + 'users/' + id + '/messages', message);
}

deleteMessage(id: number, userId: number) {
  return this.http.post(this.baseUrl + 'users/' + userId + '/messages/' + id, {});
}

markAsRead(userId: number, messageId: number) {
  return this.http.post(this.baseUrl + 'users/' + userId + '/messages/' + messageId + '/read', {}).subscribe();
}
}
