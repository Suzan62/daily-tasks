// @Component({
//   selector: 'app-login',
//   standalone: true,
//   imports: [],
//   templateUrl: './login.component.html',
//   styleUrl: './login.component.css',
// })
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Injectable } from '@angular/core';
@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  Email = '';
  Password = '';

  constructor(private authService: AuthService, private router: Router) {}

  login() {
  if (!this.Email || !this.Password) {
    alert('Please enter both email and password.');
    return;
  }

  this.authService
    .login({ email: this.Email, password: this.Password })
    .subscribe(
      (res) => {
        console.log('Login Response:', res);
        if (res?.role) {
          this.handleNavigation(res.role);
        } else {
          alert('Login successful, but role missing.');
        }
      },
      (err) => {
        console.error('Login error:', err);
        alert('Login failed.');
      }
    );
}

  private handleNavigation(role: string) {
  console.log(`Navigating to: ${role.toLowerCase()}`);

  switch (role.toLowerCase()) {
    case 'admin':
      console.log('Redirecting to Admin Dashboard...');
      this.router.navigate(['/dashboard']);
      break;
    case 'user':
      console.log('Redirecting to User Home...');
      this.router.navigate(['/home']);
      break;
    default:
      alert('Unknown role received.');
  }
}

}
