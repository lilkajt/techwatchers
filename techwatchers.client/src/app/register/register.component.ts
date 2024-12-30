import { Component } from '@angular/core';
import { RegisterService } from '../services/register/register.service';

@Component({
  selector: 'app-register',
  standalone: false,
  
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  registerData = {email: '', username: '', password: '', repeatPassword: '', acceptRules: false};
  message: string = '';
  isError: boolean = false;
  constructor( private registerService: RegisterService) { }

  onSubmit(){

    //podglad danych w konsoli
    console.log('Form submitted:', this.registerData);

    if ( this.removeWhitespacesAndValidate(this.registerData.username) || this.removeWhitespacesAndValidate(this.registerData.email) || this.removeWhitespacesAndValidate(this.registerData.password) || this.removeWhitespacesAndValidate(this.registerData.repeatPassword)) {
      return;
    }

    if (this.registerData.password !== this.registerData.repeatPassword) {
      this.isError = true;
      this.message = 'Hasła nie są identyczne.';
      return;
    }

    this.registerService.register(this.registerData).subscribe(
      (response) => {
        this.isError = false;
        this.message = 'Rejestracja udana!';
        console.log(response);
        // this.router.navigate(['/dashboard']);
      },
      (error) => {
        // dodac custom error code w backend zeby wiedziec czy ten uzytkownik juz istnieje
        // StatusCode(408, "User already exists") -> backend
        console.log(error);
        this.isError = true;
        this.message = error.status === 400 ? 'Niepoprawne dane!' : 'Coś poszło nie tak. Spróbuj ponownie!';
      }
    );
  }

  // funkcja usuwajaca biale znaki
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
