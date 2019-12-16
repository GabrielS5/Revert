import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Record } from '../models/Record';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProcessingService {
  private baseUrl: string = environment.baseUrl + 'processing';

  constructor(private http: HttpClient) {}

  process(record: Record): any {
    return this.http.post(this.baseUrl, record);
  }
}
