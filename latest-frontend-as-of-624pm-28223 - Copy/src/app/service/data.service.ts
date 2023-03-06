import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, firstValueFrom, Subject } from 'rxjs';
import { environment } from '../environment/Environment';
import { Data } from '../Model/model';

@Injectable({
  providedIn: 'root',
})
export class DataService {
  constructor(private http: HttpClient) {}

  register(x: any) {
    return this.http.post(environment.authUrl + '/register', x);
  }
  authenticateUser(x: any) {
    return this.http.post(environment.authUrl + `/login`, x);
  }

  public isloggedIn = new BehaviorSubject(false);

  getUsers() {
    return this.http.get(environment.authUrl);
  }

  updateUser(user: string, role: string, block: string) {
    return this.http.put(
      environment.authUrl +
        `/userrights?userName=${user}&role=${role}&isBlocked=${block}`,
      '',
      {
        headers: {
          Authorization: `Bearer ${localStorage.getItem('token')}`,
        },
      }
    );
  }
  //to fetch user token from local storage
  isUserAuthenticated() {
    return this.http.get(environment.authUrl + '/isAuthenicated', {
      headers: {
        Authorization: `Bearer ${localStorage.getItem('token')}`,
      },
    });
  }

  notifyUsers(x: any) {
    //seen from postman, onesignal API requires passing of key value pair w key:'Authorization'
    //and value : 'Basic + restAPI token'
    return this.http.post(environment.notiURL, x, {
      headers: {
        Authorization: 'Basic ODZiYzI3NWQtN2E0OC00YjJkLWIyZWMtMDU0ZjBkNDMzMjM5',
      },
    });
  }

  lenLong(source: any) {
    return this.http.get(environment.mapURL + `/Convert?postalCode=${source}`);

    //https://localhost:7197/api/OneMap/Convert?postalCode=117372
  }

  getRoutes(sla: string, slo: string, ela: string, elo: string, mode: string) {
    return this.http.get(
      environment.mapURL +
        `/Route?startLang=${sla}&startLong=${slo}&endLang=${ela}&endLong=${elo}&mode=${mode}`
    );
    //https://localhost:7197/api/OneMap/Route?startLang=1.298879&startLong=103.8464&endLang=1.276042&endLong=103.8001&mode=TRANSIT
  }

  specificUser(username: string) {
    return this.http.get(environment.authUrl + `/${username}`);
  }

  addFav(x: any) {
    return this.http.post(environment.favURL, x);
  }

  public getUserName = new Subject();

  getFav(username: any) {
    return this.http.get(environment.favURL + `?username=${username}`);
    //https://localhost:7269/api/Favourites?username=kai%40gmail.com
  }

  delFav(id: any) {
    return this.http.delete(environment.favURL + `/${id}`);
  }

  getTaxiTime(sla: any, slo: any, elo: any, ela: any) {
    return this.http.get(
      environment.taxiURL +
        `/TimeDistance?startLang=${sla}&startLong=${slo}&endLang=${ela}&endLong=${elo}`
    );
  }

  getMeteredFare(sla: any, slo: any, elo: any, ela: any) {
    return this.http.get(
      environment.taxiURL +
        `/Meter?startLang=${sla}&startLong=${slo}&endLang=${ela}&endLong=${elo}`
    );
  }

  getCdgFare(sla: any, slo: any, elo: any, ela: any) {
    return this.http.get(
      environment.taxiURL +
        `/CDGFare?startLang=${sla}&startLong=${slo}&endLang=${ela}&endLong=${elo}`
    );
  }

  getarrivelah(busstopcode: string) {
    return this.http.get(environment.arrivelahURL, {
      headers: {
        origin: 'X-Requested-With',
      },
      params: {
        id: busstopcode,
      },
    });
  }

  getBusStopName(busstopcode: any) {
    return this.http.get(`${environment.busStopNameURL}/${busstopcode}`);
  }

  resetPassword(email: any) {
    return this.http.post(
      environment.authUrl + `/passwordreset?email=${email}`,
      email
    );
    // https://localhost:7094/api/User/passwordreset?email=string
  }

  updatePassword(x: any) {
    return this.http.put(environment.authUrl + '/password', x);
  }

  addBusStop(x: any) {
    return this.http.post(environment.busStopURL, x);
  }

  getBusStops(x: any) {
    return this.http.get(environment.busStopURL + `/${x}`);
  }
  //https://localhost:7269/api/BusBookmark/kh%40gmail.com

  delBusStop(id: any) {
    return this.http.delete(environment.busStopURL + `/${id}`);
  }
  //https://localhost:7269/api/BusBookmark/1

  uploadPicture(username: string, formData: FormData) {
    return this.http.post(
      environment.authUrl + `/profilepic/${username}`,
      formData,
      { reportProgress: true, observe: 'events' }
    );
  }

  getPicture(username: string) {
    return this.http.get(environment.picURL + `/profilepic/${username}`, {
      responseType: 'blob',
    }); //image will be sent back as raw data, need to set responseType as a blob in get request settings
    //will get image as a blob -> need to convert to base64 encoded source done at profile component
  }

  updateInfo(user: Data) {
    return this.http.put(environment.authUrl + '/userinfo', user);
  }

  passPicture = new Subject<any>();
  passUser = new Subject<string>();


welcomeEmail(x:any){
  return this.http.post(environment.emailURL, x);
}

}
