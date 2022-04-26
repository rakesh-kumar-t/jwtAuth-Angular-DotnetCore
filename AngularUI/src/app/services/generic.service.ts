import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GenericModel } from 'src/app/models/GenericModel';

@Injectable({
  providedIn: 'root',
})
export class GenericService<T, ID> implements GenericModel<T, ID> {
  constructor(private http: HttpClient, private baseUrl: string) {}
  add(t: T): Observable<any> {
    return this.http.post(this.baseUrl, t);
  }
  update(t: T): Observable<T> {
    return this.http.put<T>(this.baseUrl, t);
  }
  getById(id: ID): Observable<T> {
    return this.http.get<T>(`${this.baseUrl}/${id}`);
  }
  getAll(): Observable<T[]> {
    return this.http.get<T[]>(this.baseUrl);
  }
  delete(id: ID): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${id}`);
  }
}
