import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { AuthService } from '../../../../core/services/auth.service';
import { ToastService } from '../../../../shared/services/toast.service';

@Component({
  selector: 'app-otp',
  standalone: false,
  templateUrl: './otp.html',
  styleUrl: './otp.css'
})
export class Otp implements OnInit{


  otpFrom:FormGroup;
  email:string="";
  constructor(private toast:ToastService,private fb:FormBuilder,private activeRouter:ActivatedRoute,private router:Router,private auth:AuthService){
    this.otpFrom=fb.group({
      code:['',Validators.required]
    })
  }
  ngOnInit(): void {

    this.activeRouter.paramMap.subscribe(params=>{
      this.email=params.get("email")!;
    })
  }


submit() {
  this.auth.confirmEmail(this.email, this.code?.value).subscribe(
    () => {
      this.toast.success("Correct Code");
      this.router.navigate(["/auth/login"]);
    },
    (error: any) => {
      this.toast.error(error);
    }
  );
}
  get code() { return this.otpFrom.get('code'); }

}
