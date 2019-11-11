import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Record } from '../models/Record';

@Injectable({
  providedIn: 'root'
})
export class RecordsService {
  private baseUrl: string;

  constructor(private http: HttpClient) {}

  get(): Record[] {
    const records = [
    ];

    return records;
  }

  post(record) {
    return this.http.post(this.baseUrl, { record });
  }

  update(record) {
    return this.http.put(this.baseUrl, { record });
  }

  delete(recordId) {
    return this.http.delete(this.baseUrl + '/' + recordId);
  }
}
