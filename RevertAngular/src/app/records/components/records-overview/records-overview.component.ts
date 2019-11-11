import { Component, OnInit } from '@angular/core';
import { RecordsService } from '../../services/records.service';
import { Record } from '../../models/Record';

@Component({
  selector: 'app-records-overview',
  templateUrl: './records-overview.component.html',
  styleUrls: ['./records-overview.component.css']
})
export class RecordsOverviewComponent implements OnInit {
  records: Record[];

  constructor(private recordsService: RecordsService) {}

  ngOnInit() {
    this.recordsService.get().subscribe(response => {
      this.records = response;
    });
  }
}
