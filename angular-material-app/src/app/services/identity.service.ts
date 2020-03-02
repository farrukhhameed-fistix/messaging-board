import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class IdentityService {

  private userName: string;

  constructor() { }

  setCurrentUser(name: string){
    this.userName = name;
  }

  getCurrentUser(): string{
    return this.userName;
  }  
}
