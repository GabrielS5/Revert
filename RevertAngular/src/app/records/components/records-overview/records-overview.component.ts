import { Component, OnInit } from '@angular/core';
import { FacadeService } from '../../services/facade.service';

@Component({
  selector: 'app-records-overview',
  templateUrl: './records-overview.component.html',
  styleUrls: ['./records-overview.component.css']
})
export class RecordsOverviewComponent implements OnInit {

  constructor(private facadeService: FacadeService) { }

  ngOnInit() {
    console.log(this.facadeService.get());
  }

}
