import { Component, OnInit } from '@angular/core';
import { Record } from '../../models/Record';
import { RecordsService } from '../../services/records.service';
import { Router } from '../../../../../node_modules/@angular/router';

@Component({
  selector: 'app-records-process-result',
  templateUrl: './records-process-result.component.html',
  styleUrls: ['./records-process-result.component.css']
})
export class RecordsProcessResultComponent implements OnInit {
  diagnosis: string;
  lastRecord: Record;
  constructor(private api: RecordsService, private router: Router) {
    this.diagnosis = JSON.parse(localStorage.getItem('diagnosis'));
    this.lastRecord = JSON.parse(localStorage.getItem('lastRecord'));
  }

  ngOnInit() {
  }

  onYes() {
    this.lastRecord.diagnosis = this.diagnosis;
    this.api.post(this.lastRecord).subscribe(r => {
      this.router.navigate(['/records']);
    });
  }

  onNo() {
    this.router.navigate(['/records']);
  }
}
