import { ComponentFixture, TestBed } from '@angular/core/testing';
import { UpdatePasswordComponent } from './update-password.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { DataService } from '../service/data.service';
import { Observable, of, throwError } from 'rxjs';
import { HttpClientModule, HttpErrorResponse } from '@angular/common/http';
import { MatFormFieldModule } from '@angular/material/form-field';

import {
  HttpClientTestingModule,
  HttpTestingController,
} from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

describe('UpdatePasswordComponent', () => {
  let component: UpdatePasswordComponent;
  let fixture: ComponentFixture<UpdatePasswordComponent>;
  let service: DataService;
  let httpTestingController: HttpTestingController;
  let router: Router;
  let snackBar: MatSnackBar;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UpdatePasswordComponent],
      imports: [
        ReactiveFormsModule,
        MatFormFieldModule,
        MatInputModule,
        MatButtonModule,
        BrowserAnimationsModule,
        HttpClientModule,
        RouterTestingModule,
        FormsModule,
        MatSnackBarModule,
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(UpdatePasswordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should show snackbar message when form is invalid', () => {
    component.passwordForm.setValue({
      new: '',
      current: '',
      confirm: '',
    });
    spyOn(component.snackBar, 'open');
    component.updatePass();
    expect(component.snackBar.open).toHaveBeenCalled();
  });

  it('should display new password and confirm password do not match error message when confirm password value is different from new password value', () => {
    const newInput = fixture.debugElement.nativeElement.querySelector(
      'input[formControlName="new"]'
    );
    const confirmInput = fixture.debugElement.nativeElement.querySelector(
      'input[formControlName="confirm"]'
    );
    newInput.value = 'newPassword123';
    newInput.dispatchEvent(new Event('input'));
    confirmInput.value = 'differentPassword123';
    confirmInput.dispatchEvent(new Event('input'));
    fixture.detectChanges();
    const errorElement =
      fixture.debugElement.nativeElement.querySelector('.text-danger');
    expect(errorElement.textContent.trim()).toBe(
      'New password and confirm password do not match.'
    );
  });

  it('should create password form with required fields', () => {
    expect(component.passwordForm.contains('new')).toBeTruthy();
    expect(component.passwordForm.contains('current')).toBeTruthy();
    expect(component.passwordForm.contains('confirm')).toBeTruthy();
  });

  it('should be invalid when "confirm" field is empty', () => {
    component.new && component.new.setValue('newPassword123');
    component.current && component.current.setValue('oldPassword123');
    component.confirm && component.confirm.setValue('');
    expect(component.passwordForm.valid).toBeFalsy();
  });

  it('should be invalid when "current" field is empty', () => {
    component.new && component.new.setValue('newPassword123');
    component.current && component.current.setValue('');
    component.confirm && component.confirm.setValue('oldPassword123');
    expect(component.passwordForm.valid).toBeFalsy();
  });

});
