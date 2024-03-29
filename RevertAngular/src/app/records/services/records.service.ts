import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Record } from '../models/Record';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RecordsService {
  private baseUrl: string = environment.baseUrl + 'records';

  constructor(private http: HttpClient) {}

  get(): Observable<Record[]> {
    return this.http.get<Record[]>(this.baseUrl);
  }

  getById(id: string): Observable<Record> {
    return this.http.get<Record>(`${this.baseUrl}/${id}`);
  }

  hasUpdated(record: Record): Observable<boolean> {
    return this.http.get<boolean>(`${this.baseUrl}/updated?date=${record.creationDate}`);
  }

  post(record: Record) {
    return this.http.post(this.baseUrl, record);
  }

  update(record) {
    return this.http.put(this.baseUrl, { record });
  }

  delete(recordId) {
    return this.http.delete(this.baseUrl + '/' + recordId);
  }
}
