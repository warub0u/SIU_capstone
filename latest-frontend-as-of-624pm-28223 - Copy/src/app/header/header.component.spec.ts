import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Router } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { DataService } from '../service/data.service';
import { HeaderComponent } from './header.component';

import { HttpClientModule } from '@angular/common/http';
import {HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { By } from '@angular/platform-browser';
import { MatSidenav, MatSidenavModule } from '@angular/material/sidenav';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSnackBarModule } from '@angular/material/snack-bar';

describe('HeaderComponent', () => {
  let component: HeaderComponent;
  let fixture: ComponentFixture<HeaderComponent>;
  let service: DataService;
  let httpTestingController: HttpTestingController;
  let router: Router;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HeaderComponent, MatSidenav ],
      imports:[
        RouterTestingModule,
        HttpClientModule,
        HttpClientTestingModule,
        MatSidenavModule,
        BrowserAnimationsModule,
        MatSidenavModule,
        MatSnackBarModule
      ],
      providers:[DataService]
    })
    .compileComponents();
    fixture = TestBed.createComponent(HeaderComponent);
    component = fixture.componentInstance;
    service = TestBed.inject(DataService);
    httpTestingController = TestBed.inject(HttpTestingController);
    router = TestBed.inject(Router);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should display mov and Ease in separate spans in the header', () => {
    const headerElement = fixture.debugElement.query(By.css('.header')).nativeElement;
    const spanElements = headerElement.querySelectorAll('span');
    expect(spanElements.length).toBe(3);
    expect(spanElements[0].textContent.trim()).toBe('mov');
    expect(spanElements[1].textContent.trim()).toBe('Ease');
  });

  
  it('should contain a toggle button for the side navigation', () => {
    const button = fixture.debugElement.query(By.css('.header button')).nativeElement;
    expect(button).toBeTruthy();
  });
  
  it('should display a logo image in the header', () => {
    const image = fixture.debugElement.query(By.css('.header img')).nativeElement;
    expect(image).toBeTruthy();
  });

  it('should have the sidenav closed initially', () => {
    const sidenav = fixture.debugElement.query(By.directive(MatSidenav)).componentInstance;
    expect(sidenav.opened).toBe(false);
  });



  
});