import { Component, signal } from '@angular/core';
import { HeaderService } from '../services/header/header.service';
import { Category } from '../models/category.model';

@Component({
  selector: 'app-header',
  standalone: false,
  
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  categories = signal<Category[]>([]);
  constructor( private headerService: HeaderService) {}

  ngOnInit(): void {
    this.loadCategories();
  }

  loadCategories(): void {
      this.headerService.getCategories().subscribe((data: Category[]) => {
        this.categories.set(data);
      });
  }
}
