import { Component, signal } from '@angular/core';
import Toast from 'bootstrap/js/dist/toast';
import { FormBuilder, FormGroup, Validators  } from '@angular/forms';
import { HeaderService, PostCreateDTO } from '../services/header/header.service';
import { Category } from '../models/category.model';

@Component({
  selector: 'app-logged-header',
  standalone: false,
  
  templateUrl: './logged-header.component.html',
  styleUrl: './logged-header.component.css'
})
export class LoggedHeaderComponent {
  isError: boolean = false;
  message: string = '';
  postForm: FormGroup;
  userId: number = 1; //normaly download from session user id 
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
}
