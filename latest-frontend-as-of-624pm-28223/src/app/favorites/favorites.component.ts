import { DOCUMENT } from '@angular/common';
import { Component, Inject } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Data, Router } from '@angular/router';
import { ExampleDialogComponent } from '../example-dialog/example-dialog.component';
import { Fav2 } from '../Model/model';
import { DataService } from '../service/data.service';
import { HttpErrorResponse } from '@angular/common/http';

import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-favorites',
  templateUrl: './favorites.component.html',
  styleUrls: ['./favorites.component.css'],
})
export class FavoritesComponent {
  urlSafe: SafeResourceUrl | undefined;

  
  constructor(
    private service: DataService,
    private route: ActivatedRoute,
    private dialog: MatDialog,
    @Inject(DOCUMENT) private document: Document,
    private route2: Router,

    public sanitizer: DomSanitizer
  ) {}

  username: any = '';
  user: Array<Data> = [];
  favList: Array<Fav2> = [];

  ngOnInit() {
    this.service
      .getFav(localStorage.getItem('username'))
      .subscribe((data: any) => {
        this.favList = data;
      });
  }

  click() {
    this.service
      .getFav(localStorage.getItem('username'))
      .subscribe((data: any) => {
        this.favList = data;
      });
  }

  delete(id: any) {
    this.service
      .delFav(id)
      .subscribe(
        (data: any) => (this.favList = this.favList.filter((x) => x.id != id))
      );
  }
  routeArr: Array<any> = [];
  duration: any = 0;
  fare: any = 0;
  stops: Array<any> = [];

  getRoute(y: Fav2) {
    this.loadRoute(y);
    this.openDialog(this.routeArr);
  }

  loadRoute(y: Fav2) {
    this.isLoading = true;
    this.service
      .getRoutes(
        y.sourceLang,
        y.sourceLong,
        y.destinationLang,
        y.destinationLong,
        y.mode
      )
      .subscribe((data: any) => {
        this.routeArr = data;

        this.routeArr.forEach((x) => {
          x.legDurationNum = Math.ceil(Number(x.legDuration) / 60);
          this.duration += x.legDurationNum;
          this.stops.push(x.intermediateStops);
          this.fare = x.fare;
        });
        this.isLoading = false;
      });
  }

  openDialog(myArr: any[]): void {
    let dialogRef = this.dialog.open(ExampleDialogComponent, {
      width: '550px',
      data: { myArr: myArr },
    });
  }

  isLoading: boolean = false;
}
