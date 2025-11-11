import { Component, OnInit } from '@angular/core';
import { ActiveCodeResponse } from '../../../../core/models/active-code/response/active-code-response';
import { NotificationService } from '../../../../core/services/notification.service';
import { ActiveCodeService } from '../../../../core/services/active-code.service';
import { MatDialog } from '@angular/material/dialog';
import { ActiveCodeDialogComponent } from '../../components/active-code-dialog/active-code-dialog.component';

@Component({
  selector: 'app-active-codes',
  standalone: false,
  templateUrl: './active-codes.component.html',
  styleUrls: ['./active-codes.component.css']
})
export class ActiveCodesComponent implements OnInit {

  activatedCodes!: ActiveCodeResponse[];
  constructor(private notificationsService: NotificationService, private dialog: MatDialog, private activeCodeService: ActiveCodeService) { }

  ngOnInit() {
    this.loadActivatedCodes();
  }

  loadActivatedCodes() {
    this.activeCodeService.getAll().subscribe({
      next: (response) => {
        this.activatedCodes = response;
      },
      error: (errors) => {
        this.notificationsService.showError(errors);
      }
    })
  }
  handleOpenDialog(id: number) {

    const dialogRef = this.dialog.open(ActiveCodeDialogComponent, {
      width: '600px',
      data: id
        ? { editMode: true, id }
        : { editMode: false }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadActivatedCodes();
      }
    });
  }

  handleDeleteCode(id: number) {

  }
}

