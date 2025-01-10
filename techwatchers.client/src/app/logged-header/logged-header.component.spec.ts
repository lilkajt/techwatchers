import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoggedHeaderComponent } from './logged-header.component';

describe('LoggedHeaderComponent', () => {
  let component: LoggedHeaderComponent;
  let fixture: ComponentFixture<LoggedHeaderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [LoggedHeaderComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LoggedHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
