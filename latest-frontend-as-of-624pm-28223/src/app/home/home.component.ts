import { Component, Inject, OnInit, Renderer2 } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { mergeMap } from 'rxjs';
import { DataService } from '../service/data.service';
import { Fav, Route, taxiObj } from '../Model/model';
import { MatDialog } from '@angular/material/dialog';
import { ExampleDialogComponent } from '../example-dialog/example-dialog.component';
import { DialogComponent } from '../dialog/dialog.component';
import { MatSnackBar } from '@angular/material/snack-bar';

import * as L from 'leaflet';
import 'leaflet-providers';
import 'leaflet-routing-machine';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  constructor(
    private service: DataService,
    private fb: FormBuilder,
    private dialog: MatDialog,
    private renderer2: Renderer2,
    private snackBar: MatSnackBar
  ) {}

  events: string[] = [];
  opened: boolean = false;

  directionForm = this.fb.group({
    Source: new FormControl(''),
    Destination: new FormControl(''),
  });

  //polylines
  public map!: L.Map;
  public centroid: L.LatLngExpression = [1.29027, 103.851959];

  ngOnInit(): void {
   
    //polylines map
    this.map = L.map('mapdiv', {
      center: this.centroid,
      zoom: 8,
    });

    const basemap = L.tileLayer(
      'https://maps-{s}.onemap.sg/v3/Default/{z}/{x}/{y}.png',
      {
        detectRetina: true,
        maxZoom: 18,
        minZoom: 12,
      }
    );

    basemap.addTo(this.map);
    this.map.setMaxBounds([
      [1.45, 103.55],
      [1.2, 104.2],
    ]);
  }

  endpoint: any;
  startLang: string = '';
  startLong: string = '';
  endLang: string = '';
  endLong: string = '';
  date: string = '';
  time: string = '';
  datetime?: string;
  mode: string = '';
  empty: Array<any> = [];
  empty2: Array<any> = [];
  isLoading: boolean = false;
  TaxiObj: taxiObj = {};
  click2() {
    this.isLoading = true;
    const timeoutId = setTimeout(() => {
      this.isLoading = false;
    }, 10000);
    if (this.directionForm.valid) {
      this.endpoint = this.directionForm.controls['Destination'].value;

      this.service
        .lenLong(this.directionForm.controls['Source'].value)
        //pipe is used to chain rxjs methods ( which are the mergeMap) not observables
        .pipe(
          //mergeMap is a rxjs operator that acts on observables
          mergeMap((data: any) => {
            this.empty = data.results;
            this.startLang = this.empty[0].latitude;
            this.startLong = this.empty[0].longitude;
            return this.service.lenLong(
              this.directionForm.controls['Destination'].value
              //in mergeMap always need to return the next observable that it is going to act on
            );
          }), // , mergeMap(()) =>  --> if you're linking another observable
          mergeMap((data: any) => {
            this.empty2 = data.results;
            this.endLang = this.empty2[0].latitude;
            this.endLong = this.empty2[0].longitude;
            return this.service.getTaxiTime(
              this.startLang,
              this.startLong,
              this.endLong,
              this.endLang
            );
          }),
          mergeMap((data: any) => {
            this.TaxiObj = data;
            return this.service.getMeteredFare(
              this.startLang,
              this.startLong,
              this.endLong,
              this.endLang
            );
          }),
          mergeMap((data: any) => {
            this.TaxiObj.meter = data;
            return this.service.getCdgFare(
              this.startLang,
              this.startLong,
              this.endLong,
              this.endLang
            );
          }) //for the LAST of the chain, subscribe is done OUTSIDE pipe.
        )
        .subscribe((data: any) => {
          this.TaxiObj.appFare = data;
          this.TaxiObj.source = this.directionForm.controls['Source'].value;
          this.TaxiObj.destination =
            this.directionForm.controls['Destination'].value;
          this.routes();
          this.plotMarker();
          this.isLoading = false;
          clearTimeout(timeoutId);
        }); //this way -> observables will be called in sequence
    }
  }

  public mrtArr: Array<Route> = [];
  public busArr: Array<Route> = [];
  public transitArr: Array<Route> = [];

  Duration: any = 0;
  stops: Array<any> = [];
  fare: any = 0;
  Durationy: any = 0;
  stopy: Array<any> = [];
  farey: any = 0;
  Durationz: any = 0;
  stopz: Array<any> = [];
  farez: any = 0;
  routes() {
    this.service
      .getRoutes(
        this.startLang,
        this.startLong,
        this.endLang,
        this.endLong,
        'TRANSIT'
      )
      .subscribe((data: any) => {
        this.transitArr = data;
        this.Duration = 0; 
        this.transitArr.forEach((x) => {
          x.legDurationNum = Math.ceil(Number(x.legDuration) / 60);
          this.Duration += x.legDurationNum;
          this.stops.push(x.intermediateStops);
          this.fare = x.fare;
        });
      });

    this.service
      .getRoutes(
        this.startLang,
        this.startLong,
        this.endLang,
        this.endLong,
        'BUS'
      )
      .subscribe((data: any) => {
        this.busArr = data;
        this.Durationy = 0; 
        this.busArr.forEach((y) => {
          y.legDurationNum = Math.ceil(Number(y.legDuration) / 60);
          this.Durationy += y.legDurationNum;
          this.stopy.push(y.intermediateStops);
          this.farey = y.fare;
        });
      });

    this.service
      .getRoutes(
        this.startLang,
        this.startLong,
        this.endLang,
        this.endLong,
        'RAIL'
      )
      .subscribe((data: any) => {
        this.mrtArr = data;
        this.Durationz = 0;
        this.mrtArr.forEach((z) => {
          z.legDurationNum = Math.ceil(Number(z.legDuration) / 60);
          this.Durationz += z.legDurationNum;
          this.stopz.push(z.intermediateStops);
          this.farez = z.fare;
        });
        this.plotRoute('MRT'); 
      });
  }

  openDialog(myArr: any[]): void {
    let dialogRef = this.dialog.open(ExampleDialogComponent, {
      width: '550px',
      data: { myArr: myArr, endpoint: this.endpoint },
    });
  }

  openDialog2(): void {
    let dialogRef = this.dialog.open(DialogComponent, {
      width: '550px',
      data: { taxiobj: this.TaxiObj },
    });
  }

  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {});
  }

  fav: Fav = {};
  submit(x: string) {
    this.isLoading = true;
    const timeoutId = setTimeout(() => {
      this.isLoading = false;
    }, 10000);
    this.fav.Source = this.directionForm.controls['Source'].value;
    this.fav.Destination = this.directionForm.controls['Destination'].value;
    this.fav.SourceLong = this.startLong;
    this.fav.SourceLang = this.startLang;
    this.fav.DestinationLang = this.endLang;
    this.fav.DestinationLong = this.endLong;
    this.fav.Mode = x;
    this.fav.UserName = localStorage.getItem('username');
    this.fav.MapUrl = `https://www.onemap.gov.sg/amm/amm.html?mapStyle=Night&zoomLevel=10&marker=latLng:${this.startLang},${this.startLong}!colour:blue&marker=latLng:${this.endLang},${this.endLong}!colour:red!rType:${this.mode}!rDest:${this.startLang},${this.startLong}`;
    this.service.addFav(this.fav).subscribe(
      (data) => {
        this.isLoading = false;
        clearTimeout(timeoutId);
        this.snackBar.open('Added to favorites!', 'close', {
          horizontalPosition: 'center',
          verticalPosition: 'top',
          duration: 3000,
        });
      },
      (err: HttpErrorResponse) => {
        this.snackBar.open(
          'Unable to add to favorites. Did you add this before?',
          'close',
          {
            horizontalPosition: 'center',
            verticalPosition: 'top',
            duration: 3000,
          }
        );
      }
    );
  }


  // FOR PLOTTING MT, BUS, TRANSIT
  public mrt_leggeog_arr: Array<string> = [];
  public bus_leggeog_arr: Array<string> = [];
  public transit_leggeog_arr: Array<any> = [];
  public mrt_mode_arr: Array<any> = [];
  public bus_mode_arr: Array<any> = [];
  public transit_mode_arr: Array<any> = [];
  public polylineLayers: Array<any> = [];

  plotRoute(s: string): void {
    // remove all plotted polylines prior
    for (var i = 0; i < this.polylineLayers.length; i++) {
      this.map.removeLayer(this.polylineLayers[i]);
    }

    // initialise the required public arrays as emptied arrays
    this.mrt_leggeog_arr = [];
    this.bus_leggeog_arr = [];
    this.transit_leggeog_arr = [];
    this.mrt_mode_arr = [];
    this.bus_mode_arr = [];
    this.transit_mode_arr = [];
    this.polylineLayers = [];

    // FOR MRT, TRANSIT AND BUS
    if (s == 'MRT' || s == 'TRANSIT' || s == 'BUS') {
      // declare the arr variables and assign
      if (s == 'MRT') {
        var transport_arr = this.mrtArr;
      } else if (s == 'TRANSIT') {
        var transport_arr = this.transitArr;
      } else {
        var transport_arr = this.busArr;
      }
      var leggeog_arr: Array<string> = [];
      var mode_arr: Array<string> = [];

      transport_arr.forEach((x) => {
        if (s == 'MRT') {
          this.mrt_leggeog_arr.push(x.legGeo);
          leggeog_arr = this.mrt_leggeog_arr;
          this.mrt_mode_arr.push(x.methodOfTransport);
          mode_arr = this.mrt_mode_arr;
        } else if (s == 'TRANSIT') {
          this.transit_leggeog_arr.push(x.legGeo);
          leggeog_arr = this.transit_leggeog_arr;
          this.transit_mode_arr.push(x.methodOfTransport);
          mode_arr = this.transit_mode_arr;
        } else {
          this.bus_leggeog_arr.push(x.legGeo);
          leggeog_arr = this.bus_leggeog_arr;
          this.bus_mode_arr.push(x.methodOfTransport);
          mode_arr = this.bus_mode_arr;
        }
      });

      for (let i = 0; i < leggeog_arr.length; i++) {
        var poly = require('polyline-encoded');
        var polyline: any;

        // plot geometryOfRoute with diff colors based on MODE
        if (mode_arr[i] == 'SUBWAY') {
          polyline = L.polyline(poly.decode(leggeog_arr[i], 5), {
            color: 'rgb(255,0,0)',
            weight: 6, 
            smoothFactor: 1,
          });
        } else if (mode_arr[i] == 'WALK') {
          polyline = L.polyline(poly.decode(leggeog_arr[i], 5), {
            color: 'rgb(105,105,105)',
            weight: 6, 
            smoothFactor: 1,
          });
        } else if (mode_arr[i] == 'BUS') {
          polyline = L.polyline(poly.decode(leggeog_arr[i], 5), {
            color: 'rgb(0,0,255)',
            weight: 6, 
            smoothFactor: 1,
          });
        } else {
        }

        // add polyline layer to map
        polyline.addTo(this.map);
        this.polylineLayers.push(polyline);

        // Zoom the map to fit the bounds of the polyline
        this.map.fitBounds(polyline.getBounds());
      }
    }
    // ELSE IF TAXI
    else {
      var poly = require('polyline-encoded');
      //geometryOfRoute
      var polyline: any = L.polyline(poly.decode(this.TaxiObj.routeGeo, 5), {
        color: 'rgb(0,255,0)',
        weight: 6, 
        smoothFactor: 1,
      });

      polyline.addTo(this.map);
      this.polylineLayers.push(polyline);

      // Zoom the map to fit the bounds of the polyline
      this.map.fitBounds(polyline.getBounds());
    }
  }

  public markerLayers: Array<any> = [];

  plotMarker() {
    //remove prev markers
    for (var i = 0; i < this.markerLayers.length; i++) {
      this.map.removeLayer(this.markerLayers[i]);
    }

    var startlang = parseFloat(this.startLang);
    var startlong = parseFloat(this.startLong);
    var endlang = parseFloat(this.endLang);
    var endlong = parseFloat(this.endLong);
    var markerS = new L.Marker([startlang, startlong]);
    var markerD = new L.Marker([endlang, endlong]);
    markerS.addTo(this.map);
    this.markerLayers.push(markerS);
    markerD.addTo(this.map);
    this.markerLayers.push(markerD);
  }
}
