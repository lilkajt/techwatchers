import { Component } from '@angular/core';
import { ProfileService } from '../services/profile/profile.service';

@Component({
  selector: 'app-profile',
  standalone: false,
  
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent {
  profileData = { userId: 23, currentPassword: '', newPassword: '', repeatNewPassword: '' }; //get userId from session
  message: string = '';
  isError: boolean = false;
  userName: string = 'User'; // Replace with actual user name from session

  constructor(private profileService: ProfileService) { }

  onSubmit() {
    console.log('Form submitted:', this.profileData);

    if (this.removeWhitespacesAndValidate(this.profileData.currentPassword) || 
        this.removeWhitespacesAndValidate(this.profileData.newPassword) || 
        this.removeWhitespacesAndValidate(this.profileData.repeatNewPassword)) {
      return;
    }

    if (this.profileData.newPassword !== this.profileData.repeatNewPassword) {
      this.isError = true;
      this.message = 'Hasła nie są identyczne.';
      return;
    }

    this.profileService.updatePassword(this.profileData).subscribe(
      (response) => {
        console.log(response);
        this.isError = false;
        this.message = response.message;
      },
      (error) => {
        console.log(error);
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