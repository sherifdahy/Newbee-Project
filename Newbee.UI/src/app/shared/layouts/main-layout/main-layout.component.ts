import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { filter } from 'rxjs/operators';
import { AuthService } from '../../../core/services/auth.service';
import { Permissions } from '../../../core/enums/permissions.enum';
@Component({
  selector: 'app-main-layout',
  standalone: false,
  templateUrl: './main-layout.component.html',
  styleUrls: ['./main-layout.component.css']
})
export class MainLayoutComponent implements OnInit {
  sidebarOpen = true;
  addressNavOpen = false;
  Permissions = Permissions;
  constructor(
    public authService: AuthService,
    private router: Router
  ) {
  }

  ngOnInit(): void {
    // Open the address nav group automatically when route matches
    this.updateAddressGroup(this.router.url);
    this.router.events.pipe(filter(e => e instanceof NavigationEnd)).subscribe((ev: any) => {
      this.updateAddressGroup(ev.urlAfterRedirects || ev.url);
    });
  }

  toggleSidebar(): void {
    this.sidebarOpen = !this.sidebarOpen;
  }

  toggleNavGroup(): void {
    this.addressNavOpen = !this.addressNavOpen;
  }
  hasPermissions(permission: Permissions): boolean | undefined {
    return this.authService.currentUser?.permissions.includes(permission)
  }
  private updateAddressGroup(url: string): void {
    // If current url contains address-management, ensure the group is open
    this.addressNavOpen = /address-management\//.test(url);
  }

}
