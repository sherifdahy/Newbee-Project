import { Component, OnInit } from '@angular/core';
import { RoleResponse } from '../../../../core/models/role/responses/role-response';
import { RoleService } from '../../../../core/services/role.service';
import { NotificationService } from '../../../../core/services/notification.service';
import { MatDialog } from '@angular/material/dialog';
import { RoleDialogComponent } from '../../components/role-dialog/role-dialog.component';

@Component({
  selector: 'app-roles',
  standalone: false,
  templateUrl: './roles.component.html',
  styleUrls: ['./roles.component.css']
})
export class RolesComponent implements OnInit {

  showDeleted: boolean;
  roles!: RoleResponse[];
  constructor(private roleService: RoleService,private dialog:MatDialog , private notificationService: NotificationService) {
    this.roles = [];
    this.showDeleted = false;
  }

  ngOnInit() {
    this.loadRoles();
  }

  loadRoles() {
    this.roleService.getAll(this.showDeleted).subscribe({
      next: (responseRoles) => {
        this.roles = responseRoles;
      },
      error: (error) => {
        this.notificationService.showError(error);
      }
    })
  }
  handleChangeDeletedFilter() {
    this.loadRoles();
  }

  openDialog(id : number) {
    const dialogRef = this.dialog.open(RoleDialogComponent, {
      width: '600px',
      data: id
        ? { mode: 'edit', id }
        : { mode: 'create' }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        // تعمل refresh أو تحدث الجدول مثلاً
      }
    });
  }

  toggleState(id: number) {
    this.roleService.toggleState(id).subscribe({
      next: () => {
        this.loadRoles();
        this.notificationService.showSuccess('تمت العمليه بنجاح');
      },
      error: (errors) => {
        this.notificationService.showError(errors);
      }
    });
  }
}
