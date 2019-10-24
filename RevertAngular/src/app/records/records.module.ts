import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RecordsOverviewComponent } from './components/records-overview/records-overview.component';
import { RecordsRoutingModule } from './records-routing.module';

@NgModule({
  imports: [
    CommonModule,
    RecordsRoutingModule
  ],
  declarations: [RecordsOverviewComponent]
})
export class RecordsModule { }
