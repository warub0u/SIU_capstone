import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { DataService } from '../service/data.service';
import { Data } from '../Model/model';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent {
  events: string[] = [];
  opened: boolean = false;

  constructor(private service: DataService, private route: Router) {}
  username: any = '';
  notLoggedIn() {
    this.username = localStorage.getItem('username');
    return localStorage.getItem('username') == null;
  }

  onLogOut() {
    localStorage.clear();
    this.route.navigateByUrl('/');
    this.service.isloggedIn.next(false);
    this.user = '';
  }

  user: any = '';
  path: any = {};
  response: any = {};
  ngOnInit() {
    
      this.service.getPicture(localStorage.getItem('username') || '').subscribe(
        (data) => {
          this.createImageFromBlob(data);
          this.service.passPicture.next(this.response);
        },
        (error) => {
          this.response = {};
          //if we get an error, this.response = null. Will display default picture provided
        }
      );
    
    //converting blob from getProfilePic into the base64-encoded source
   

    const storedPath = localStorage.getItem('path');
    if (storedPath) {
      this.path = storedPath;
    }

    // Try to get the stored user value from local storage
    const storedUser = localStorage.getItem('user');
    if (storedUser) {
      this.user = storedUser;
    }

    // Subscribe to the passPicture observable and store the emitted value in local storage
    this.service.passPicture.subscribe((data: any) => {
      this.path = data;
      localStorage.setItem('path', data);
    });

    // Subscribe to the passUser observable and store the emitted value in local storage
    this.service.passUser.subscribe((data: any) => {
      this.user = data;
      localStorage.setItem('user', data);
    });
  }

  createImageFromBlob(data: Blob) {
    //FileReader reads blob data and
    let reader = new FileReader();
    //this method of FileReader set where image is saved ->  at response.dbPath
    reader.addEventListener('load', () => {
      this.response.dbPath = reader.result;
    });
    //if able to get data from the get request, this method will convert the blob into encoded source
    if (data) {
      reader.readAsDataURL(data);
    }
  }
}
