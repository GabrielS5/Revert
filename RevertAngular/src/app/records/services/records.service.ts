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
    //return this.http.get(this.baseUrl);

    let records = [
      {patient: "Ion Popescu", date: new Date(), diagnosis: "Hepatita"} as Record,
      {patient: "Vasile Popescu", date: new Date(), diagnosis: "Raceala"} as Record,
      {patient: "Ioana Ionescu", date: new Date(), diagnosis: "Lupus"} as Record,
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
