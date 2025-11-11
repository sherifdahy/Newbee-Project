import { Component, Input, OnInit } from '@angular/core';
import { GovernorateResponse } from '../../../../../core/models/governorate/responses/governorate-response';
import { GovernorateService } from '../../../../../core/services/governorate.service';
import { NotificationService } from '../../../../../core/services/notification.service';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { GovernorateDialogComponent } from '../../../components/governorate-dialog/governorate-dialog.component';
import { DialogRef } from '@angular/cdk/dialog';

@Component({
  selector: 'app-governorates',
  standalone: false,
  templateUrl: './governorates.component.html',
  styleUrls: ['./governorates.component.css']
})
export class GovernoratesComponent implements OnInit {

  governorates!: GovernorateResponse[];

  constructor(
    private governorateService: GovernorateService,
    private notificationService: NotificationService,
    private dialog: MatDialog,
  ) {
    this.governorates = [];
  }

  ngOnInit() {
    this.loadGovernorates();
  }
  handleOpenDialog(id: number) {

    const dialogRef = this.dialog.open(GovernorateDialogComponent, {
      width: '600px',
      data: id
        ? { editMode: true, id }
        : { editMode: false }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadGovernorates();
      }
    });
  }
  handleDeleteGovernorate(id : number){

  }
  loadGovernorates() {
    this.governorateService.getAll().subscribe({
      next: (response: GovernorateResponse[]) => this.governorates = response,
      error: (err: any) => this.notificationService.showError(err)
    })
  }
}
