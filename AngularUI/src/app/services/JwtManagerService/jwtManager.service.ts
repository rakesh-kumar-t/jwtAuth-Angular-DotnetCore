import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { TokenModel } from 'src/app/models/TokenModel';

@Injectable({
  providedIn: 'root',
})
export class JwtManagerService {
  token: string;
  decodedToken: TokenModel;

  constructor(private router: Router, private jwtHelper: JwtHelperService) {}

  SetToken(token: string) {
    this.token = token;
    this.decodedToken = this.jwtHelper.decodeToken(this.token);
    return localStorage.setItem('userToken', token);
  }
  GetToken() {
    return this.jwtHelper.tokenGetter();
    //return localStorage.getItem('userToken');
  }

  IsAuthenticated() {
    return this.token && !this.jwtHelper.isTokenExpired(this.token);
  }

  DecodeToken(): TokenModel {
    if (this.IsAuthenticated()) {
      return this.decodedToken;
    } else {
      this.decodedToken = null;
      return null;
    }
  }

  Logout() {
    this.decodedToken = null;
    localStorage.removeItem('userToken');
    localStorage.clear();
    // this.router.navigate(['logout']);
  }
}
