import { Component, OnInit } from '@angular/core';
import { ProfileService } from '../services/profile/profile.service';
import { AppService } from '../services/app/app.service';

@Component({
  selector: 'app-profile',
  standalone: false,
  
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit{
  profileData = { currentPassword: '', newPassword: '', repeatNewPassword: '' };
  message: string = '';
  isError: boolean = false;
  userName: string = 'User';

  constructor(private profileService: ProfileService, private appService: AppService) { }

  ngOnInit(): void {
    this.loadUsername()
  }

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

  loadUsername(): void {
    this.appService.checkUser().subscribe(
      response => {
        this.userName = response.user.username;
      },
      error => {
        console.error('Error while getting username:', error);
      }
    )
  }
}