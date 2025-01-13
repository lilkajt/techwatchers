import { Component } from '@angular/core';
import { RegisterService } from '../services/register/register.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-register',
  standalone: false,
  
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  registerData = {email: '', username: '', password: '', repeatPassword: '', Statute: false};
  message: string = '';
  isError: boolean = false;
  constructor( private registerService: RegisterService) { }

  onSubmit(registerForm: NgForm){

    //podglad danych w konsoli
    console.log('Form submitted:', this.registerData);

    if ( this.removeWhitespacesAndValidate(this.registerData.username) || this.removeWhitespacesAndValidate(this.registerData.email) || this.removeWhitespacesAndValidate(this.registerData.password) || this.removeWhitespacesAndValidate(this.registerData.repeatPassword)) {
      return;
    }
    if (!this.registerData.Statute) {
      this.isError = true;
      this.message = 'Musisz zaakceptować regulamin.';
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
        registerForm.resetForm();
        this.isError = false;
        this.message = response.message;
        // this.router.navigate(['/dashboard']);
      },
      (error) => {
        console.log(error);
        this.isError = true;
        this.message = error.error.message;
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
