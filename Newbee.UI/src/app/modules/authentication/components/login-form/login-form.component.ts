import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NotificationService } from '../../../../core/services/notification.service';
import { AuthService } from '../../../../core/services/auth.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Role } from '../../../../core/enums/role.enum';

@Component({
  selector: 'app-login-form',
  standalone : false,
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent implements OnInit {

  loginForm!: FormGroup;
  constructor(private router: Router, private notificationService: NotificationService, private authService: AuthService, fb: FormBuilder) {
    this.loginForm = fb.group({
      email: fb.control('', Validators.required),
      password: fb.control('', Validators.required)
    })
  }
  ngOnInit() {
  }
  get email() {
    return this.loginForm.get('email');
  }

  get password() {
    return this.loginForm.get('password');
  }

  handleSubmitClick() {
    if (!this.loginForm.valid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    this.authService.login(this.email?.value, this.password?.value)?.subscribe({
      next: () => {
        if(this.authService.currentUser?.roles.includes(Role[Role.Admin]))
        {
          this.router.navigate(['admin']);
        }
        else{
          this.router.navigate(['portal']);
        }

      },
      error: (errors: any) => {
        this.notificationService.showError(errors);
      }
    })
  }

}
