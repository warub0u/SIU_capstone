import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FavoritesComponent } from './favorites.component';
import { DOCUMENT } from '@angular/common';
import { Component, Inject } from '@angular/core';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { ActivatedRoute, Data, Router } from '@angular/router';
import { ExampleDialogComponent } from '../example-dialog/example-dialog.component';
import { Fav2 } from '../Model/model';
import { DataService } from '../service/data.service';
import { HttpClientModule, HttpErrorResponse } from '@angular/common/http';

import { By, DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { MatTabsModule } from '@angular/material/tabs';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { RouterTestingModule } from '@angular/router/testing';
import {
  HttpClientTestingModule,
  HttpTestingController,
} from '@angular/common/http/testing';
import { DebugElement } from '@angular/core';
import { of, throwError } from 'rxjs';
import { MatSnackBarModule } from '@angular/material/snack-bar';

describe('FavoritesComponent', () => {
  let component: FavoritesComponent;
  let fixture: ComponentFixture<FavoritesComponent>;
  let service: DataService;
  let httpTestingController: HttpTestingController;
  let router: Router;
  let debugElement: DebugElement;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [FavoritesComponent],
      imports: [
        HttpClientModule,
        MatDialogModule,
        MatTabsModule,
        MatInputModule,
        MatCardModule,
        RouterTestingModule,
        HttpClientTestingModule,
        MatSnackBarModule,
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(FavoritesComponent);
    component = fixture.componentInstance;
    service = TestBed.inject(DataService);
    httpTestingController = TestBed.inject(HttpTestingController);
    router = TestBed.inject(Router);

    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should have three tabs', () => {
    const tabElements = fixture.nativeElement.querySelectorAll('.nav-link');
    expect(tabElements.length).toBe(3);
  });

  it('should remove a favorite from the list when delete is called', () => {
    component.favList = [
      {
        id: 1,
        userName: 'AbbyMeh',
        source: 'sengkang',
        sourceLang: '1.39168782518022',
        sourceLong: '103.896129776175',
        destination: 'woodlands',
        destinationLang: '1.43357568988365',
        destinationLong: '103.804329417963',
        mode: 'BUS',
        mapUrl: 'https://localhost:7197/api/OneMap/Convert?postalCode=117372',
      },
    ];
    component.delete(1);
    spyOn(service, 'delFav').and.returnValue(of(1));
    component.delete(1);
    expect(service.delFav).toHaveBeenCalled();
    expect(component.favList.length).toBe(0);
  });

  it('should call the getFav method when click is called', () => {
    spyOn(service, 'getFav').and.returnValue(of([]));
    component.click();
    expect(service.getFav).toHaveBeenCalled();
  });

  it('should set favList property correctly when getFav() is called', () => {
    const mockData = [
      {
        id: 1,
        userName: 'AbbyMeh',
        source: 'sengkang',
        sourceLang: '1.39168782518022',
        sourceLong: '103.896129776175',
        destination: 'woodlands',
        destinationLang: '1.43357568988365',
        destinationLong: '103.804329417963',
        mode: 'BUS',
        mapUrl: 'https://localhost:7197/api/OneMap/Convert?postalCode=117372',
      },
    ];
    spyOn(service, 'getFav').and.returnValue(of(mockData));
    component.click();
    expect(component.favList).toEqual(mockData);
  });
});
