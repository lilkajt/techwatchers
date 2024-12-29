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
  constructor( private loginService: LoginService) { }

  onSubmit(){
    console.log('Form submitted:', this.loginData);
    this.loginService.login(this.loginData).subscribe(
      (response) => {
        console.log(response); //successfull login
      },
      (error) => {
        console.log(error); // error
        //handle login failure
      }
    );
  }
}
