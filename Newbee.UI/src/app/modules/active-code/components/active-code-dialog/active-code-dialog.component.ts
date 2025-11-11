import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ActiveCodeService } from '../../../../core/services/active-code.service';
import { NotificationService } from '../../../../core/services/notification.service';
import { ActiveCodeRequest } from '../../../../core/models/active-code/request/active-code-request';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatDialogRef } from '@angular/material/dialog';
import { ActiveCodeResponse } from '../../../../core/models/active-code/response/active-code-response';

@Component({
  selector: 'app-active-code-dialog',
  standalone: false,
  templateUrl: './active-code-dialog.component.html',
  styleUrls: ['./active-code-dialog.component.css']
})
export class ActiveCodeDialogComponent implements OnInit {
  editMode: boolean;
  form!: FormGroup;
  constructor(@Inject(MAT_DIALOG_DATA) public data: any,
    private dialog: MatDialogRef<ActiveCodeDialogComponent>,
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private activeCodeService: ActiveCodeService,
    private notificationService: NotificationService) {
    this.editMode = this.data.editMode;
    this.form = fb.group({
      code: fb.control('', [Validators.required])
    });
  }

  ngOnInit() {
    if (this.editMode) {
      this.handleLoadCurrentActiveCode();
    }
  }


  handleLoadCurrentActiveCode() {
    this.activeCodeService.get(this.data.id).subscribe({
      next: (response: ActiveCodeResponse) => {
        this.form.setValue({
          code: response.code
        })
      },
      error: (errors) => {
        this.notificationService.showError(errors)
      }
    })
  }

  handleSubmitForm() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    let request = this.form.value as ActiveCodeRequest;

    if (this.editMode) {
      this.activeCodeService.update(this.data.id, request).subscribe({
        next: () => {
          this.notificationService.showSuccess('done');
          this.dialog.close(true);
        },
        error: (errors) => {
          this.notificationService.showError(errors);
        }
      })
    }
    else {
      this.activeCodeService.create(request).subscribe({
        next: () => {
          this.notificationService.showSuccess('done');
          this.dialog.close(true);
        },
        error: (errors) => {
          this.notificationService.showError(errors);
        }
      })
    }

  }
}
