import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { addStop, busObj } from '../Model/model';
import { DataService } from '../service/data.service';

@Component({
  selector: 'app-schedules',
  templateUrl: './schedules.component.html',
  styleUrls: ['./schedules.component.css'],
})
export class SchedulesComponent {
  constructor(private service: DataService, private snackBar: MatSnackBar) {}

  dataArr: Array<any> = [];
  busArr: Array<busObj> = [];
  busobj: busObj = {};
  busStopName: any = '';

  ngOnInit() {
    this.OnSubmit();
    this.getBusStops();
  }

  public busStop = new FormControl();
  busStopCode: string = '65009';
  OnSubmit() {
    if (this.busStop.value != null) {
      this.busStopCode = this.busStop.value;
    }

    this.service.getarrivelah(this.busStopCode).subscribe((data: any) => {
      this.busArr = [];
      for (var element of data.services) {
        let busobj: busObj = {};
        busobj.busNo = element.no;

        if (element.next != null) {
          busobj.firstTime =
            element.next.time.substring(11, 16) || 'Not available';
          busobj.firstDur =
            Math.ceil(Number(element.next.duration_ms) / 60000) || 0;
        }

        if (element.next2 != null) {
          busobj.secondTime =
            element.next2.time.substring(11, 16) || 'Not available';
          busobj.secondDur =
            Math.ceil(Number(element.next2.duration_ms) / 60000) || 0;
        }

        if (element.next3 != null) {
          busobj.thirdTime =
            element.next3.time.substring(11, 16) || 'Not available';
          busobj.thirdDur =
            Math.ceil(Number(element.next3.duration_ms) / 60000) || 0;
        }

        this.busArr.push(busobj);
        }
    });

    this.service.getBusStopName(this.busStopCode).subscribe((data: any) => {
      this.busStopName = data[2];
    });
  }

  retrieveStop(busCode: any) {}

  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {});
  }

  addStops: addStop = {};
  submitStop() {
    this.addStops.userName = localStorage.getItem('username');
    this.addStops.busCode = this.busStopCode;
    this.addStops.busStopName = this.busStopName;
    this.service.addBusStop(this.addStops).subscribe(
      (data: any) => {
        this.getBusStops();
        this.snackBar.open('Stop has been bookmarked!', 'close', {
          horizontalPosition: 'center',
          verticalPosition: 'top',
          duration: 3000,
        });
      },
      (err: HttpErrorResponse) => {
        this.snackBar.open('You have bookmarked this before.', 'close', {
          horizontalPosition: 'center',
          verticalPosition: 'top',
          duration: 3000,
        });
      }
    );
  }

  busStopArr: Array<addStop> = [];
  getBusStops() {
    this.service
      .getBusStops(localStorage.getItem('username'))
      .subscribe((data: any) => {
        this.busStopArr = data;
      });
  }

  delStop(id: any) {
    this.service.delBusStop(id).subscribe((data: any) => {
      this.busStopArr = this.busStopArr.filter((x) => x.id != id);
    });
  }

  getStop(x: any) {
    this.busStopCode = x;
    this.OnSubmit();
  }
}
