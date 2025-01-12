import { Component, inject } from '@angular/core';
import { LoginService } from '../services/login/login.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-login',
  standalone: false,
  
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  private router = inject(Router);

  public loginData = {username: '', password: ''};
  public message: string = '';
  public isError: boolean = false;
  constructor( private loginService: LoginService) { }

  onSubmit() {
    console.log('Form submitted:', this.loginData);
    
    if (this.removeWhitespacesAndValidate(this.loginData.username) || this.removeWhitespacesAndValidate(this.loginData.password)) {
      return;
    }

    this.loginService.login(this.loginData).subscribe(
      (response) => {
        console.log(response);
        this.isError = false;
        this.message = response.message;
        this.router.navigate(['/home']).then(() => {
          window.location.href = '/home';
        });
      },
      (error) => {
        console.error('Login failed:', error);
        this.isError = true;
        this.message = error.error.message;
      }
    );
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
