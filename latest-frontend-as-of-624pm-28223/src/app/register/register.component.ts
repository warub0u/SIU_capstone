import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { DataService } from '../service/data.service';
import { timer } from 'rxjs';
import { welcomeUser } from '../Model/model';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent {
  constructor(
    private fb: FormBuilder,
    private service: DataService,
    private router: Router,
    private snackBar: MatSnackBar
  ) {}

  registerForm = this.fb.group({
    DOB: new FormControl('', Validators.required),
    IsPasswordReset: new FormControl(false),
    Email: new FormControl('', [Validators.email, Validators.required]),
    Password: new FormControl('', [
      Validators.required,
      Validators.minLength(6),
    ]),
    Role: new FormControl('User'),
    IsBlocked: new FormControl(false),
    UserName: new FormControl('', Validators.required),
    lastName: new FormControl('', Validators.required),
    firstName: new FormControl('', Validators.required),
    mobileNo: new FormControl('', [
      Validators.required,
      Validators.minLength(8),
      Validators.maxLength(8),
    ]),
    gender: new FormControl('other', Validators.required),
    postalCode: new FormControl('', [
      Validators.required,
      Validators.minLength(6),
      Validators.maxLength(8),
    ]),
  });
  get postalCode() {
    return this.registerForm.get('postalCode');
  }
  get mobileNo() {
    return this.registerForm.get('mobileNo');
  }
  get gender() {
    return this.registerForm.get('gender');
  }
  get firstName() {
    return this.registerForm.get('firstName');
  }
  get lastName() {
    return this.registerForm.get('lastName');
  }
  get Email() {
    return this.registerForm.get('Email');
  }
  get Password() {
    return this.registerForm.get('Password');
  }
  get UserName() {
    return this.registerForm.get('UserName');
  }

  get DOB() {
    return this.registerForm.get('DOB');
  }
  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      horizontalPosition: 'left',
      verticalPosition: 'top',

      duration: 3000,
    });
  }

  newUser: welcomeUser = {};
  onSubmit() {
    if (this.registerForm.valid) {
      this.service.register(this.registerForm.value).subscribe(
        (data) => {
          this.router.navigate(['/']);
          this.newUser.userName = this.registerForm.controls['UserName'].value;
          this.newUser.firstName =
            this.registerForm.controls['firstName'].value;
          this.newUser.email = this.registerForm.controls['Email'].value;
          this.newUser.lastName = this.registerForm.controls['lastName'].value;
          this.service.welcomeEmail(this.newUser).subscribe((data) => {
            this.snackBar.open('Check your registered email inbox', 'close', {
              horizontalPosition: 'center',
              verticalPosition: 'top',
              duration: 3000,
            });
          });
        },
        (err: HttpErrorResponse) => {
          this.snackBar.open('An error has occured', 'close', {
            horizontalPosition: 'center',
            verticalPosition: 'top',
            duration: 3000,
          });
        }
      );
    } else {
      this.snackBar.open(
        'Registeration unsuccessful. Please check form fields.',
        'close',
        {
          horizontalPosition: 'center',
          verticalPosition: 'top',
          duration: 3000,
        }
      );
    }
  }
}
