import { Component } from '@angular/core';
import { OneSignal } from 'onesignal-ngx';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'TravelApp';

  constructor(private oneSignal: OneSignal) {
    this.oneSignal
      .init({
        appId: 'aa35db12-63fa-45f8-8f93-781e4d419a62',
      })
      .then(() => {
        console.log('OneSignal Connected');
      });
  }
}
