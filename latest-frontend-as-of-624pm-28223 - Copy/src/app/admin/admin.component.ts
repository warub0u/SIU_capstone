import { Component } from '@angular/core';
import { Data } from '../Model/model';
import { DataService } from '../service/data.service';
import { Signal } from '../Model/model';
import { HttpErrorResponse } from '@angular/common/http';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css'],
  
}) //implements AfterViewInit
export class AdminComponent {
  constructor(private service: DataService, private snackBar: MatSnackBar) {}

  
  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {});
  }

  dataArr: Array<Data> = [];
  getUsers() {
    this.service.getUsers().subscribe((data: any) => {
      this.dataArr = data;
    });
  }

  ngOnInit() {
    this.getUsers();
  }

  onSubmit(userName: string, role: string, isBlocked: string) {
    this.service.updateUser(userName, role, isBlocked).subscribe((data) =>
      this.snackBar.open('Changes saved', 'close', {
        horizontalPosition: 'center',
        verticalPosition: 'top',
        duration: 3000,
      })
    );
  }
  notification: string = '';
  onClick(x: any) {
    {
      var y = new Signal();
      y.contents = { en: x };
      this.service.notifyUsers(y).subscribe((data) => {
      this.snackBar.open('Notifications sent !', 'close', {
        horizontalPosition: 'center',
        verticalPosition: 'top',
        duration: 3000,
      });
    },
    (err: HttpErrorResponse) => {
      this.snackBar.open('Error occurred. Please enter a message.', 'close', {
        horizontalPosition: 'center',
        verticalPosition: 'top',
        duration: 3000,
      });
    })}
  }
}
