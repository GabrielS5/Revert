import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RecordsOverviewComponent } from './components/records-overview/records-overview.component';
import { RecordsRoutingModule } from './records-routing.module';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  imports: [
    CommonModule,
    RecordsRoutingModule,
    HttpClientModule
  ],
  declarations: [RecordsOverviewComponent]
})
export class RecordsModule { }
