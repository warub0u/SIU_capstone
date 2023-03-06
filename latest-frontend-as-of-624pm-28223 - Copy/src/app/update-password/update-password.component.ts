import { Component } from '@angular/core';
import { DataService } from '../service/data.service';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { password } from '../Model/model';
import { HttpErrorResponse } from '@angular/common/http';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-update-password',
  templateUrl: './update-password.component.html',
  styleUrls: ['./update-password.component.css'],
})
export class UpdatePasswordComponent {
  constructor(
    public service: DataService,
    private route: ActivatedRoute,
    public fb: FormBuilder,
    public route2: Router,
    public snackBar: MatSnackBar
  ) {}

  isLoading: boolean = false;

  passwordForm = this.fb.group({
    new: new FormControl('', [Validators.required, Validators.minLength(6)]),
    current: new FormControl('', [
      Validators.required,
      Validators.minLength(6),
    ]),
    confirm: new FormControl('', [
      Validators.required,
      Validators.minLength(6),
    ]),
  });

  get new() {
    return this.passwordForm.get('new');
  }

  get current() {
    return this.passwordForm.get('current');
  }

  get confirm() {
    return this.passwordForm.get('confirm');
  }

  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {});
  }

  pass: password = {};
  updatePass() {
    if (
      this.passwordForm.valid &&
      this.passwordForm.controls['new'].value ==
        this.passwordForm.controls['confirm'].value
    ) {
      this.pass.userName = localStorage.getItem('username');
      this.pass.old_pw = this.passwordForm.controls['current'].value;
      this.pass.new_pw = this.passwordForm.controls['new'].value;
      this.service.updatePassword(this.pass).subscribe((data) => {
        this.snackBar.open('Password updated!', 'close', {
          horizontalPosition: 'center',
          verticalPosition: 'top',
          duration: 3000,
        });
        localStorage.clear();
        this.route2.navigateByUrl('/');
        this.service.isloggedIn.next(false);
      }),
        (err: HttpErrorResponse) => {
          this.snackBar.open(err.error, 'close', {
            horizontalPosition: 'center',
            verticalPosition: 'top',
            duration: 3000,
          });
        };
    } else {
      this.snackBar.open(
        'Update unsuccessful, please check form fields.',
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
