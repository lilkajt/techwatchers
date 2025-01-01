import { Component } from '@angular/core';
import { LoginService } from '../services/login/login.service';
@Component({
  selector: 'app-login',
  standalone: false,
  
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginData = {username: '', password: ''};
  message: string = '';
  isError: boolean = false;
  constructor( private loginService: LoginService) { }

  onSubmit() {
    console.log('Form submitted:', this.loginData);
    
    if (this.removeWhitespacesAndValidate(this.loginData.username) || this.removeWhitespacesAndValidate(this.loginData.password)) {
      return;
    }

    this.loginService.login(this.loginData).subscribe({
      next: (response) => {
        this.isError = false;
        this.message = 'Login udany!';
        // this.router.navigate(['/dashboard']);
      },
      error: (error) => {
        console.error('Login failed:', error);
        this.isError = true;
        if (error.status === 401) {
          this.message = 'Niepoprawna nazwa użytkownika lub hasło!';
        } else {
          this.message = 'Coś poszło nie tak. Spróbuj ponownie!';
        }
      }
    });
  }

  logout() {
    this.loginService.logout().subscribe({
      next: () => {
        console.log('Logout successful');
        this.isError = false;
        this.message = 'Wylogowanie udane!';
        // this.router.navigate(['/login']);
      },
      error: (error) => {
        console.error('Logout failed:', error);
        this.isError = true;
        this.message = 'Wylogowanie nieudane. Spróbuj ponownie!';
      }
    });
  }

  removeWhitespacesAndValidate(data: string): boolean {
    const trimmedData = data.replace(/\s+/g, '');
    if (trimmedData === '') {
      this.isError = true;
      this.message = 'Wszystkie pola są wymagane!';
      return true;
    }
    return false;
  }
}
