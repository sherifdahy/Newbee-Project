import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ManagerService } from '../../../../core/services/manager.service';
import { ManagerResponse } from '../../../../core/models/manager/responses/manager-response';
import { MatDialog } from '@angular/material/dialog';
import { ManagerDialogComponent } from '../manager-dialog/manager-dialog.component';
import { NotificationService } from '../../../../core/services/notification.service';

@Component({
  selector: 'app-managers-grid',
  standalone : false,
  templateUrl: './managers-grid.component.html',
  styleUrls: ['./managers-grid.component.css']
})
export class ManagersGridComponent implements OnInit {

  managers! : ManagerResponse[];

  @Output() openManagerDialogListner = new EventEmitter<number>();

  @Input('companyId') companyId! : number;

  constructor(
    private managerService: ManagerService,
    private notificationService : NotificationService,
    private dialog: MatDialog,
  )
  {
    this.managers = [];
  }

  ngOnInit() {
    this.loadManagers(this.companyId);
  }
  removeManager(i: number) {
    this.managerService.delete(i).subscribe({
      next:()=>{
        this.loadManagers(this.companyId);
      },
      error:(errors)=>{
        this.notificationService.showError(errors);
      }
    })
  }

  loadManagers(companyId: number) {
    this.managerService.getAll(companyId).subscribe({
      next: (response: ManagerResponse[]) => {
        this.managers = response;
      },
      error: (errors) => {
        this.notificationService.showError(errors)
      }
    });
  }
}
