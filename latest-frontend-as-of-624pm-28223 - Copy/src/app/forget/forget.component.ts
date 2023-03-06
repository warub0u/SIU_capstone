import { Component } from '@angular/core';
import { DataService } from '../service/data.service';
import { FormControl } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-forget',
  templateUrl: './forget.component.html',
  styleUrls: ['./forget.component.css'],
})
export class ForgetComponent {
  constructor(private service: DataService, private snackBar: MatSnackBar) {}
  public Email = new FormControl();

  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      
    });
  }

  forgetPassword() {
    return this.service.resetPassword(this.Email.value).subscribe(
      (data) => {
        this.snackBar.open('Check your inbox for an email!', 'close',{
          horizontalPosition: 'center',
          verticalPosition: 'top',
          duration: 3000,
        });
      },
      (err: HttpErrorResponse) => {
        this.snackBar.open('Email not found', 'close',{
          horizontalPosition: 'center',
          verticalPosition: 'top',
          duration: 3000,
        });
      }
    );
  }
}
