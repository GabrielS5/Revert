import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RecordsOverviewComponent } from './components/records-overview/records-overview.component';
import { AccountService } from './services/account.service';
import { RecordsService } from './services/records.service';
import { RecordsAddOrUpdateComponent } from './components/records-add-or-update/records-add-or-update.component';
import { RecordsDetailsComponent } from './components/records-details/records-details.component';

const routes: Routes = [{ path: '', component: RecordsOverviewComponent },
{ path: 'create', component: RecordsAddOrUpdateComponent },
{ path: ':id', component: RecordsDetailsComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: [AccountService, RecordsService]
})
export class RecordsRoutingModule { }
