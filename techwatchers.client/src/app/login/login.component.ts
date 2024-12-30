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
    
    this.loginService.login(this.loginData).subscribe(
      (response) => {
        this.isError = false;
        this.message = 'Login successful!';
        console.log(response);
        // this.router.navigate(['/dashboard']);
      },
      (error) => {
        console.log(error);
        this.isError = true;
        if (error.status === 401) {
          this.message = 'Invalid username or password!';
        } else {
          this.message = 'An unexpected error occurred!';
        }
      }
    );
  }
}
