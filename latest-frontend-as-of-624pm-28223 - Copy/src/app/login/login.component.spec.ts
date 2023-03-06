import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Router } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './login.component';
import { DataService } from '../service/data.service';
import { HttpClientModule } from '@angular/common/http';
import {
  HttpClientTestingModule,
  HttpTestingController,
} from '@angular/common/http/testing';
import { MatSnackBarModule } from '@angular/material/snack-bar';

import { of } from 'rxjs';

describe('LoginComponent', () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;
  let service: DataService;
  let httpTestingController: HttpTestingController;
  let router: Router;
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [LoginComponent],
      imports: [
        FormsModule,
        HttpClientModule,
        ReactiveFormsModule,
        RouterTestingModule,
        HttpClientTestingModule,
        MatSnackBarModule
      ],
      providers: [DataService],
    }).compileComponents();
    fixture = TestBed.createComponent(LoginComponent);
    component = fixture.componentInstance;
    service = TestBed.inject(DataService);
    httpTestingController = TestBed.inject(HttpTestingController);
    router = TestBed.inject(Router);
    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should set the form as valid when username and password are provided', () => {
    const loginForm = component.loginForm;
    loginForm.controls['UserName'].setValue('testuser');
    loginForm.controls['Password'].setValue('testpassword');
    expect(loginForm.valid).toBeTruthy();
  });

  it('should set the form as invalid when username is not provided', () => {
    const loginForm = component.loginForm;
    loginForm.controls['Password'].setValue('testpassword');
    expect(loginForm.valid).toBeFalsy();
    expect(loginForm.controls['UserName'].valid).toBeFalsy();
  });

  it('should set the form as invalid when password is not provided', () => {
    const loginForm = component.loginForm;
    loginForm.controls['UserName'].setValue('testuser');
    expect(loginForm.valid).toBeFalsy();
    expect(loginForm.controls['Password'].valid).toBeFalsy();
  });

  it('should call loginSubmit() method when submit button is clicked', () => {
    spyOn(component, 'loginSubmit');
    const button = fixture.debugElement.nativeElement.querySelector('button');
    button.click();
    expect(component.loginSubmit).toHaveBeenCalled();
  });

  it('should navigate to home page on successful authentication', () => {
    spyOn(service, 'authenticateUser').and.returnValue(
      of({ token: 'testtoken' })
    );
    spyOn(component.router, 'navigate');
    component.loginForm.controls['UserName'].setValue('testuser');
    component.loginForm.controls['Password'].setValue('testpassword');
    component.loginSubmit();
    expect(service.authenticateUser).toHaveBeenCalled();
    expect(component.router.navigate).toHaveBeenCalledWith([
      '/profile/testuser',
    ]);
  });
});
