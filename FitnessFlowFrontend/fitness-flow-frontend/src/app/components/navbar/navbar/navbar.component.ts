import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../../services/auth/auth.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './navbar.component.html',
  styles: ``
})
export class NavbarComponent {
  constructor(private router: Router, private authService: AuthService) {}

  logout() {
    this.authService.logout();
    this.router.navigate(['/']);
  }

  navigateTo(page: string) {
    if (this.router.url !== `/${page}`) {
      this.router.navigate([`/${page}`]);
    }
  }

  isActive(page: string): boolean {
    return this.router.url === `/${page}`
  }

}
