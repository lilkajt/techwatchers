import { HttpClient } from '@angular/common/http';
import { AfterViewInit, Component, ElementRef, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit, AfterViewInit {

  constructor(private http: HttpClient, private elementRef: ElementRef) {}

  ngOnInit() {
  }

  ngAfterViewInit() {
    this.elementRef.nativeElement.ownerDocument
        .body.style.backgroundColor = 'var(--background-color)';
}

  title = 'techwatchers.client';
}
