import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RecordsOverviewComponent } from './components/records-overview/records-overview.component';
import { RecordsRoutingModule } from './records-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { RecordsAddOrUpdateComponent } from './components/records-add-or-update/records-add-or-update.component';

@NgModule({
  imports: [
    CommonModule,
    RecordsRoutingModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  declarations: [RecordsOverviewComponent, RecordsAddOrUpdateComponent]
})
export class RecordsModule { }
