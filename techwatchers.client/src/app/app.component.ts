import { HttpClient } from '@angular/common/http';
import { AfterViewInit, Component, ElementRef, inject, OnChanges, OnInit } from '@angular/core';
import { AppService } from './services/app/app.service';
import { Subscription } from 'rxjs';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit, AfterViewInit {

  isLoggedIn = false;
  private subscription: Subscription = new Subscription();

  constructor(private appService: AppService, private elementRef: ElementRef) {}

  ngOnInit() {
    this.subscription = this.appService.isLoggedIn$.subscribe(isLoggedIn => {
      this.isLoggedIn = isLoggedIn;
    });

    this.checkUserSession();
  }

  ngAfterViewInit() {
    this.elementRef.nativeElement.ownerDocument
        .body.style.backgroundColor = 'var(--background-color)';
  }

  ngOnDestroy() {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  checkUserSession() {
    this.appService.checkUser().subscribe(
      response => {
        const isLoggedIn = response.isLoggedIn;
        this.appService.updateUserStatus(isLoggedIn);
      },
      error => {
        console.error('Error checking user session:', error);
      }
    );
  }

  title = 'techwatchers.client';
}
