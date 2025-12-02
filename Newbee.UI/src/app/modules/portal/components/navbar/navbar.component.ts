import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../../core/services/auth.service';

@Component({
  selector: 'app-ecom-navbar',
  standalone: false,
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit {
  cartCount = 3;
  isLoggedIn : boolean ;
  constructor(
    public authService: AuthService,
  ) {
    this.isLoggedIn = false;
  }
  ngOnInit(): void {
    this.authService.isLoggedIn$.subscribe(result=>{
      this.isLoggedIn = result;
    });
  }
  logout(){
    this.authService.logout();
  }
}
