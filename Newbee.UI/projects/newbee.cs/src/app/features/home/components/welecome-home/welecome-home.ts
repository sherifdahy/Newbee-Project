import { Component } from '@angular/core';
import { LocalStorgeService } from '../../../../core/services/local-storge/local-storge.service';
import { IUserVm } from '../../../../core/view-models/stores/user-vm';
import { StorageKeys } from '../../../../core/statics/storage-keys';

@Component({
  selector: 'app-welecome-home',
  standalone: false,
  templateUrl: './welecome-home.html',
  styleUrl: './welecome-home.css',
})
export class WelcomeHome {
  public user: IUserVm | null;
  constructor(private localStorage: LocalStorgeService) {
    this.user = this.getUser();
  }

  getUser(): IUserVm | null {
    return this.localStorage.getItem<IUserVm>(StorageKeys.USER);
  }
}
