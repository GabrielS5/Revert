import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RecordsOverviewComponent } from './components/records-overview/records-overview.component';
import { FacadeService } from './services/facade.service';
import { AccountService } from './services/account.service';
import { RecordsService } from './services/records.service';

const routes: Routes = [{ path: '', component: RecordsOverviewComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: [FacadeService, AccountService, RecordsService]
})
export class RecordsRoutingModule {}
