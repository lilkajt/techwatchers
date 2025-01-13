import { Component, inject, signal } from '@angular/core';
import Toast from 'bootstrap/js/dist/toast';
import { FormBuilder, FormGroup, Validators  } from '@angular/forms';
import { HeaderService, PostCreateDTO } from '../services/header/header.service';
import { Category } from '../models/category.model';
import { Router } from '@angular/router';
import { AppService } from '../services/app/app.service';

@Component({
  selector: 'app-logged-header',
  standalone: false,
  
  templateUrl: './logged-header.component.html',
  styleUrl: './logged-header.component.css'
})
export class LoggedHeaderComponent {

  private router = inject(Router);
  private appService = inject(AppService);
  
  isError: boolean = false;
  message: string = '';
  postForm: FormGroup;
  userId: number = -1;
  categories = signal<Category[]>([]);

  constructor(private fb:FormBuilder, private headerService: HeaderService){
    this.postForm = this.fb.group({
      categoryId: ['', Validators.required],
      title: ['', Validators.required],
      description: ['', Validators.required]
    })
  }
  
  ngOnInit(): void {
    this.loadCategories();
    this.checkUserSession();
  }

  checkUserSession() {
    this.appService.checkUser().subscribe(
      response => {
        this.userId = response.user.id;
      },
      error => {
        console.error('Error checking user session:', error);
      }
    );
  }

  loadCategories(): void {
    this.headerService.getCategories().subscribe((data: Category[]) => {
      this.categories.set(data);
    });
  }

  submitPost(): void {
    if (this.postForm.valid) {
      const formData: PostCreateDTO = {
        ...this.postForm.value,
        userId: this.userId //adding userid to form
      };
      console.log('Post submitted:', formData);
      this.headerService.createPost(formData).subscribe(
        (response) => {
          console.log('Post created successfully: ', response);
          this.resetForm();
          const closeModalButton = document.getElementById('closeModalButton') as HTMLElement;
          if (closeModalButton) {
          closeModalButton.click();
          }
          this.showToast(response.message);
          window.location.reload();
        },
        (error) =>{
          console.log(error);
          this.message = error.error;
          this.isError = true;
        }
      )
    } else {  
      this.message = "Wypełnione dane są nieprawidłowe!";
      this.isError = true;
    }
  }
  resetForm(): void {
    this.postForm.patchValue({
      title: '',
      description: ''
    });
    this.message = '';
    this.isError = false;
  }
  
  showToast(message: string): void {
    const toastElementLg = document.getElementById('successToast');
    if (toastElementLg ) {
      const toastBodyLg = toastElementLg.querySelector('.toast-body.fs-6');
      const toastBodySm = toastElementLg.querySelector('.toast-body.fs-5');
      if (toastBodyLg && toastBodySm) {
        toastBodyLg.textContent = message;
        toastBodySm.textContent = message;
      }
      const toastLg = new Toast(toastElementLg);
      toastLg.show();
    }
  }

  logout(): void {
    console.log('dupa');
    this.headerService.logout();
    this.router.navigate(['/home']).then(() => {
      window.location.href = '/home';
    });
  }
}
