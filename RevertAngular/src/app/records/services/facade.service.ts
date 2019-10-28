import { Injectable, Injector } from '@angular/core';
import { AccountService } from './account.service';
import { RecordsService } from './records.service';
import { Record } from '../models/Record';

@Injectable({
  providedIn: 'root'
})
export class FacadeService {
  constructor(private injector: Injector) {}

  private _accountService: AccountService;
  public get accountService(): AccountService {
    if (!this._accountService) {
      this._accountService = this.injector.get(AccountService);
    }
    return this._accountService;
  }

  getAllUsers() {
    return this.accountService.getAllUsers();
  }
  logIn(credentials) {
    return this.accountService.logIn(credentials);
  }
  logout() {
    return this.accountService.logout();
  }

  private _recordsService: RecordsService;
  public get recordsService(): RecordsService {
    if (!this._recordsService) {
      this._recordsService = this.injector.get(RecordsService);
    }
    return this._recordsService;
  }

  get(): Record[] {
    return this.recordsService.get();
  }
  post(record) {
    return this.recordsService.post(record);
  }
  update(record) {
    return this.recordsService.update(record);
  }
  delete(recordId) {
    return this.recordsService.delete(recordId);
  }
}
