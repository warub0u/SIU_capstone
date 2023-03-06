import { AnyCatcher } from 'rxjs/internal/AnyCatcher';

export interface Data {
  userName: string;
  email: string;
  isPasswordReset: any;
  dob: string;
  password: string;
  role: string;
  isBlocked: any;
  firstName: string;
  lastName: string;
  gender: string;
  postalCode: string;
  mobileNo: string;
}

export class Signal {
  app_id: string = 'aa35db12-63fa-45f8-8f93-781e4d419a62';
  included_segments: Array<any> = ['Subscribed Users'];
  data: any = { foo: 'bar' };
  contents?: any;
}
//Get(string startLang, string startLong, string endLang, string endLong, string date, string time, string mode)

export class Route {
  //duration: 1100, methodOfTransport: 'WALK', legSource: 'Origin', legDestination: 'path', legDuration: '115'

  duration?: number;
  methodOfTransport?: string;
  legSource?: string;
  legDestination?: string;
  legDuration?: string;
  legDurationNum?: number;
  intermediateStops?: any;
  fare?: any;
  startTime?: any;
  endTime?: any;
  routeType?: any;
  legGeo?: any;
}

export class Fav {
  Source?: any;
  Destination?: any;
  SourceLong?: any;
  SourceLang?: any;
  DestinationLang?: any;
  DestinationLong?: any;
  Mode?: any;
  UserName?: any;
  MapUrl?: string;
}

export class Fav2 {
  source?: any;
  destination?: any;
  sourceLong?: any;
  sourceLang?: any;
  destinationLang?: any;
  destinationLong?: any;
  mode?: any;
  userName?: any;
  id?: any;
  mapUrl?: any;
}

export class taxiObj {
  totalTime?: any;
  totalDistance?: any;
  startPoint?: any;
  meter?: any;
  appFare?: any;
  source?: any;
  destination?: any;
  routeGeo?: any; // KAI HENG CODES
}

export class busObj {
  busNo?: any;
  firstTime?: any;
  firstDur?: any;
  secondTime?: any;
  secondDur?: any;
  thirdTime?: any;
  thirdDur?: any;
  busCode?: any;
}

export class password {
  userName?: any;
  old_pw?: any;
  new_pw?: any;
}

export class addStop {
  userName?: any;
  busCode?: any;
  busStopName?: any;
  id?: any;
}

export class welcomeUser{
  userName?:any;
  firstName?: any;
  lastName?: any;
  email?: any;
}
