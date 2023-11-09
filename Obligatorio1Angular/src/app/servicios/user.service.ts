import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';
import { IUserService, Session } from '../interfaces/user-service';

enum UserEndpoints{
  LOGIN='http://localhost:5222/api/sessions'
}

@Injectable({
  providedIn: 'root'
})
export class UserService implements IUserService {


  constructor(private _httpService:HttpClient) { }

  public login():Observable<Session>{
    return this._httpService.post<Session>(UserEndpoints.LOGIN,{

      
    })
  }
}
