import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../../../core/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  standalone : false,
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  @Output() toggleSidebar: EventEmitter<void> = new EventEmitter();

  constructor(
    private router : Router,
    private authService : AuthService) { }

  ngOnInit() {
  }

  emitToggle(): void {
    this.toggleSidebar.emit();
  }

  handleLogoutClick(){
    this.authService.logout();
    this.router.navigateByUrl('');
  }
}
