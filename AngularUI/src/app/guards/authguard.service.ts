import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';
import { JwtManagerService } from '../services/JwtManagerService/jwtManager.service';

@Injectable({
  providedIn: 'root',
})
export class AuthguardService implements CanActivate {
  constructor(private tokenService: JwtManagerService) {}
  canActivate() {
    if (this.tokenService.IsAuthenticated()) {
      return true;
    }
    return false;
  }
}
