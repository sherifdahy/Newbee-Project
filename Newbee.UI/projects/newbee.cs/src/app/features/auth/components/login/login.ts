import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ILoginVm } from '../../../../core/view-models/login-vm';
import { AuthService } from '../../../../core/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {
  userLoginForm:FormGroup
  constructor(private toast:ToastrService,private fb:FormBuilder,private auth:AuthService,private router:Router){
    this.userLoginForm=fb.group({
      email:['',[Validators.required]],
      password:['',[Validators.required]],
    })
  }



  submit(){
    let user:ILoginVm= this.userLoginForm.value as ILoginVm;
    this.auth.login(user).subscribe(
      ()=>{this.toast.success("login success");this.router.navigate(["/home/welcome"])},
      (error)=>{this.toast.error(error);}
    )
  }

  get email(){
    return this.userLoginForm.get('email');
  }

  get password(){
    return this.userLoginForm.get('password');
  }

}
