import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroupDirective,
  NgForm,
  Validators,
} from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Data } from '../Model/model';
import { DataService } from '../service/data.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  constructor(
    private fb: FormBuilder,
    public router: Router,
    private service: DataService,
    private snackBar: MatSnackBar
  ) {}

  loginForm = this.fb.group({
    UserName: new FormControl('', Validators.required),
    Password: new FormControl('', Validators.required),
  });
  
  get UserName() {
    return this.loginForm.get('UserName');
  }

  get Password() {
    return this.loginForm.get('Password');
  }
  u: any = '';

  public userinfo: Data = {
    userName: '',
    email: '',
    isPasswordReset: false,
    dob: '',
    password: '',
    role: '',
    isBlocked: '',
    firstName: '',
    lastName: '',
    gender: '',
    postalCode: '',
    mobileNo: '',
  };


  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      
    });
  }

  loginSubmit() {
    if (this.loginForm.valid) {
      this.service.authenticateUser(this.loginForm.value).subscribe(
        (data: any) => {
          this.service
            .specificUser(this.loginForm.controls['UserName'].value || '')
            .subscribe((data: any) => {
              this.userinfo = data;
              localStorage.setItem('block', this.userinfo.isBlocked);
              localStorage.setItem('role', this.userinfo.role);
            });

          if (localStorage.getItem('block') == 'true') {
            this.snackBar.open('You have been blocked', 'close', {
              horizontalPosition: 'center',
              verticalPosition: 'top',
              duration: 3000,
            });
          } else {
            localStorage.setItem('token', data.token);

            this.service.isloggedIn.next(true);
            localStorage.setItem(
              'username',
              this.loginForm.controls['UserName'].value || ''
            );
            this.service.passUser.next(localStorage.getItem('username') || '');
            this.router.navigate([
              `/profile/${localStorage.getItem('username')}`,
            ]);
          }
        },
        (err: HttpErrorResponse) => {
          this.snackBar.open(err.error, 'close', {
            horizontalPosition: 'center',
            verticalPosition: 'top',
            duration: 3000,
          });
        }
      );
    } else {
      this.snackBar.open('Login invalid, please try again', 'close', {
        horizontalPosition: 'center',
        verticalPosition: 'top',
        duration: 3000,
      });;
    }
  }
}
