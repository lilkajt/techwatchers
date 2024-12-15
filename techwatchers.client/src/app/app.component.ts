import { HttpClient } from '@angular/common/http';
import { AfterViewInit, Component, ElementRef, OnInit } from '@angular/core';
interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit, AfterViewInit {
  public forecasts: WeatherForecast[] = [];

  constructor(private http: HttpClient, private elementRef: ElementRef) {}

  ngOnInit() {
    this.getForecasts();
  }

  ngAfterViewInit() {
    this.elementRef.nativeElement.ownerDocument
        .body.style.backgroundColor = 'var(--background-color)';
}

  getForecasts() {
    this.http.get<WeatherForecast[]>('/weatherforecast').subscribe(
      (result) => {
        this.forecasts = result;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  title = 'techwatchers.client';
}
