import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RecordsOverviewComponent } from './components/records-overview/records-overview.component';
import { RecordsRoutingModule } from './records-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { RecordsAddOrUpdateComponent } from './components/records-add-or-update/records-add-or-update.component';
import { RecordsDetailsComponent } from './components/records-details/records-details.component';
import { RecordsProcessComponent } from './components/records-process/records-process.component';
import { RecordsProcessResultComponent } from './components/records-process-result/records-process-result.component';

@NgModule({
  imports: [
    CommonModule,
    RecordsRoutingModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  declarations: [
    RecordsOverviewComponent,
    RecordsAddOrUpdateComponent,
    RecordsDetailsComponent,
    RecordsProcessComponent,
    RecordsProcessResultComponent]
})
export class RecordsModule { }
