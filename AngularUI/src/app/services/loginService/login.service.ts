import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { loginModel } from 'src/app/models/LoginModel';
import { environment } from 'src/environments/environment';
import { GenericService } from '../generic.service';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  constructor(private httpClient: HttpClient) {}

  verifyUser(user: loginModel): Observable<any> {
    return this.httpClient.post(`${environment.baseUrl}/User`, user);
  }
}
