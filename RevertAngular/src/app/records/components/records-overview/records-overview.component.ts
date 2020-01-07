import { Component, OnInit, OnDestroy } from '@angular/core';
import { RecordsService } from '../../services/records.service';
import { Record } from '../../models/Record';
import { PollingService } from '../../services/pollingService';
import { Subscription } from '../../../../../node_modules/rxjs';

@Component({
  selector: 'app-records-overview',
  templateUrl: './records-overview.component.html',
  styleUrls: ['./records-overview.component.css']
})
export class RecordsOverviewComponent implements OnInit, OnDestroy {
  records: Record[];
  pollingService: PollingService;
  subscription: Subscription;

  constructor(private recordsService: RecordsService) {
    this.pollingService = new PollingService();
   }

  ngOnInit() {
    this.recordsService.get().subscribe(response => {
      this.records = response;
      this.createPollingSubscription();
    });
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
    this.pollingService.destroy();
  }

  private createPollingSubscription() {
    this.subscription = this.pollingService.create<Record>(
      3,
      this.records[0],
      this.recordsService.hasUpdated.bind(this.recordsService),
      this.recordsService.get.bind(this.recordsService)).subscribe(result => {
      this.records = result;
      });
  }
}
