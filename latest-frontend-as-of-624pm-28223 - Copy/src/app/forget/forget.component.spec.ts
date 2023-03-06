import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ForgetComponent } from './forget.component';
import { DataService } from '../service/data.service';
import { HttpErrorResponse } from '@angular/common/http';
import { MatSnackBarModule, MatSnackBar } from '@angular/material/snack-bar';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('ForgetComponent', () => {
  let component: ForgetComponent;
  let fixture: ComponentFixture<ForgetComponent>;
  let service: DataService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ForgetComponent],
      imports: [
        ReactiveFormsModule,
        MatFormFieldModule,
        MatInputModule,
        MatButtonModule,
        MatSnackBarModule,
        HttpClientTestingModule,
        BrowserAnimationsModule,
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(ForgetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  // it('should have a submit button that is functional', () => {
  //   spyOn(component, 'forgetPassword').and.callThrough();

  //   const submitButton =
  //     fixture.debugElement.nativeElement.querySelector('button');
  //   submitButton.click();

  //   fixture.detectChanges();
  //   fixture.whenStable().then(() => {
  //     expect(component.forgetPassword).toHaveBeenCalled();
  //   });
  // });
  // it('should open a snackBar with the correct message when email is submitted successfully', () => {
  //   spyOn(component, 'openSnackBar').and.callThrough();
  //   component.Email.setValue('john@example.com');
  //   fixture.detectChanges();
  //   const submitButton =
  //     fixture.debugElement.nativeElement.querySelector('button');
  //   submitButton.click();
  //   fixture.detectChanges();
  //   expect(component.openSnackBar).toHaveBeenCalled();
  // });

  // it('should open a snackBar with the correct message when email is not found', () => {
  //   spyOn(component, 'openSnackBar').and.callThrough();
  //   component.Email.setValue('wrong@example.com');
  //   fixture.detectChanges();
  //   const submitButton =
  //     fixture.debugElement.nativeElement.querySelector('button');
  //   submitButton.click();
  //   fixture.detectChanges();
  //   expect(component.openSnackBar).toHaveBeenCalled();
  // });
});
