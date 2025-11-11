import { Component, Inject, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { NotificationService } from '../../../../core/services/notification.service';
import { RoleService } from '../../../../core/services/role.service';
import { PermissionService } from '../../../../core/services/permission.service';
import { RoleRequest } from '../../../../core/models/role/requests/role-request';
import { DialogRef } from '@angular/cdk/dialog';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { RoleDetailResponse } from '../../../../core/models/role/responses/role-detail-response';

@Component({
  selector: 'app-role-dialog',
  standalone : false,
  templateUrl: './role-dialog.component.html',
  styleUrls: ['./role-dialog.component.css']
})
export class RoleDialogComponent implements OnInit {

  permissionsData: string[] = [];
  form!: FormGroup;
  isEditMode = false;

  constructor(
    private fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialog: DialogRef<RoleDialogComponent>,
    private notificationService: NotificationService,
    private roleService: RoleService,
    private permissionService: PermissionService
  ) {
    this.form = this.fb.group({
      name: ['', Validators.required],
      permissions: this.fb.array([],[ Validators.required])
    });
  }

  get permissions(): FormArray {
    return this.form.get('permissions') as FormArray;
  }


  ngOnInit() {
    this.isEditMode = this.data?.mode === 'edit';
    this.loadPermissions();

    if (this.isEditMode && this.data?.id) {
      this.loadRoleDetail(this.data.id);
    }
  }

  loadRoleDetail(id: number) {
    this.roleService.get(id).subscribe({
      next: (response: RoleDetailResponse) => {
        this.form.patchValue({
          name: response.name
        });

        this.permissions.clear();

        response.permissions.forEach(p =>
          this.permissions.push(new FormControl(p, Validators.required))
        );
      },
      error: (error) => {
        this.notificationService.showError(error);
      }
    });
  }

  loadPermissions() {
    this.permissionService.getAll().subscribe({
      next: (response) => (this.permissionsData = response),
      error: (error) => this.notificationService.showError(error)
    });
  }

  handleAddNewPermission() {
    this.permissions.push(this.fb.control('', Validators.required));
  }

  handleRemovePermission(index: number) {
    this.permissions.removeAt(index);
  }

  handleSubmitForm() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }


    const roleRequest = this.form.value as RoleRequest;

    if (this.isEditMode) {
      this.roleService.update(this.data.id, roleRequest).subscribe({
        next: () => {
          this.notificationService.showSuccess('تم تعديل الدور بنجاح');
          this.dialog.close();
        },
        error: (error) => this.notificationService.showError(error)
      });
    } else {
      // 🔵 إنشاء
      this.roleService.create(roleRequest).subscribe({
        next: () => {
          this.notificationService.showSuccess('تم إنشاء الدور بنجاح');
          this.dialog.close();
        },
        error: (error) => this.notificationService.showError(error)
      });
    }
  }
}
