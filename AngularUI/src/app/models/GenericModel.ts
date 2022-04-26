import { Observable } from 'rxjs';

export interface GenericModel<T, ID> {
  add(t: T): Observable<any>;
  update(t: T): Observable<T>;
  getById(id: ID): Observable<T>;
  getAll(): Observable<T[]>;
  delete(id: ID): Observable<any>;
}
