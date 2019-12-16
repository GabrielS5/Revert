import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RecordsOverviewComponent } from './components/records-overview/records-overview.component';
import { AccountService } from './services/account.service';
import { RecordsService } from './services/records.service';
import { RecordsAddOrUpdateComponent } from './components/records-add-or-update/records-add-or-update.component';
import { RecordsDetailsComponent } from './components/records-details/records-details.component';
import { RecordsProcessComponent } from './components/records-process/records-process.component';
import { ProcessingService } from './services/processing.service';
import { RecordsProcessResultComponent } from './components/records-process-result/records-process-result.component';

const routes: Routes = [{ path: '', component: RecordsOverviewComponent },
{ path: 'create', component: RecordsAddOrUpdateComponent },
{ path: 'process', component: RecordsProcessComponent },
{ path: 'process/result', component: RecordsProcessResultComponent },
{ path: ':id', component: RecordsDetailsComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: [AccountService, RecordsService, ProcessingService]
})
export class RecordsRoutingModule { }
