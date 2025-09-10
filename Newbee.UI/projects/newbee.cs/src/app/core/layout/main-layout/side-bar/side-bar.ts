import { Component } from '@angular/core';
import { LocalStorgeService } from '../../../services/local-storge/local-storge.service';
import { AuthService } from '../../../services/auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-side-bar',
  standalone: false,
  templateUrl: './side-bar.html',
  styleUrl: './side-bar.css',
})
export class SideBar {
  constructor(
    private localStorage: LocalStorgeService,
    private authService: AuthService,
    private router: Router
  ) {}
  logout() {
    this.authService.logout();
    this.router.navigate(['/auth']);
  }
}
