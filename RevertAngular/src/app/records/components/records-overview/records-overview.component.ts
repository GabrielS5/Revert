import { Component, OnInit } from '@angular/core';
import { RecordsService } from '../../services/records.service';

@Component({
  selector: 'app-records-overview',
  templateUrl: './records-overview.component.html',
  styleUrls: ['./records-overview.component.css']
})
export class RecordsOverviewComponent implements OnInit {

  constructor(private recordsService: RecordsService) { }

  ngOnInit() {
    console.log(this.recordsService.get());
  }

}
