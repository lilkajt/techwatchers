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
        console.log(response);
        this.isError = false;
        this.message = 'Rejestracja udana!';
        // this.router.navigate(['/dashboard']);
      },
      (error) => {
        console.log(error);
        this.isError = true;
        switch (error.status) {
          case 441:
            this.message = 'Nazwa użytkownika lub email jest juz w użytku.';
            break;
          case 442:
            this.message = "Nazwa użytkownika musi zawierać co najmniej 3 znaki i składać się z liter, cyfr i podkreślników.";
            break;
          case 443:
            this.message = "Niepoprawny adres email.";
            break;
          case 444:
            this.message = "Hasło musi zawierać co najmniej 1 cyfrę, 1 dużą literę, 1 znak specjalny i mieć co najmniej 8 znaków.";
            break;
          case 445:
            this.message = "Musisz zaakceptować regulamin.";
            break;
          case 446:
            this.message = "Hasła nie są takie same.";
            break;
          default:
            this.message = 'Coś poszło nie tak. Spróbuj ponownie!';
        }
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
