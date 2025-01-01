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

  onSubmit(){
    console.log('Form submitted:', this.loginData);
    
    if ( this.removeWhitespacesAndValidate(this.loginData.username) || this.removeWhitespacesAndValidate(this.loginData.password)) {
      return;
    }

    this.loginService.login(this.loginData).subscribe(
      (response) => {
        this.isError = false;
        this.message = 'Login udany!';
        console.log(response);
        // this.router.navigate(['/dashboard']);
      },
      (error) => {
        console.log(error);
        this.isError = true;
        if (error.status === 401) {
          this.message = 'Niepoprawna nazwa użytkownika lub hasło!';
        } else {
          this.message = 'Coś poszło nie tak. Spróbuj ponownie!';
        }
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
