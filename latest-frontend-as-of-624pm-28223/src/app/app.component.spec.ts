import { BrowserModule } from '@angular/platform-browser';
import { TestBed, ComponentFixture } from '@angular/core/testing';
import { OneSignal } from 'onesignal-ngx';
import { RouterTestingModule } from '@angular/router/testing';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ProfileComponent } from './profile/profile.component';
import { AdminComponent } from './admin/admin.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HeaderComponent } from './header/header.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatRadioModule } from '@angular/material/radio';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatMenuModule } from '@angular/material/menu';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatTable, MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatListModule } from '@angular/material/list';
import { ExampleDialogComponent } from './example-dialog/example-dialog.component';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatTabsModule } from '@angular/material/tabs';
import { DialogComponent } from './dialog/dialog.component';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { SchedulesComponent } from './schedules/schedules.component';
import { ForgetComponent } from './forget/forget.component';

import { LeafletModule } from '@asymmetrik/ngx-leaflet';
import { UploadComponent } from './upload/upload.component';
import { FavoritesComponent } from './favorites/favorites.component';
import { Router } from '@angular/router';
import { of } from 'rxjs';
import { HttpClientModule } from '@angular/common/http';
import { MatSidenav, MatSidenavContainer } from '@angular/material/sidenav';
import {
  HttpClientTestingModule,
  HttpTestingController,
} from '@angular/common/http/testing';

describe('AppComponent', () => {
  let component: AppComponent;
  let httpTestingController: HttpTestingController;
  let fixture: ComponentFixture<AppComponent>;
  let oneSignalSpy: jasmine.SpyObj<OneSignal>;
  let router: Router;

  beforeEach(async () => {
    oneSignalSpy = jasmine.createSpyObj('OneSignal', ['init']);
    oneSignalSpy.init.and.returnValue(Promise.resolve());

    await TestBed.configureTestingModule({
      declarations: [
        AppComponent,
        HomeComponent,
        LoginComponent,
        RegisterComponent,
        ProfileComponent,
        AdminComponent,
        HeaderComponent,
        ExampleDialogComponent,
        DialogComponent,
        SchedulesComponent,
        ForgetComponent,
        UploadComponent,
        FavoritesComponent,
      ],
      imports: [
        BrowserModule,
        RouterTestingModule,
        HttpClientModule,
        HttpClientTestingModule,
        BrowserAnimationsModule,
        AppRoutingModule,
        FormsModule,
        ReactiveFormsModule,
        MatButtonModule,
        MatCardModule,
        MatToolbarModule,
        MatInputModule,
        MatFormFieldModule,
        MatRadioModule,
        MatMenuModule,
        MatSidenavModule,
        MatTableModule,
        MatPaginatorModule,
        MatDialogModule,
        MatDatepickerModule,
        MatNativeDateModule,
        MatProgressSpinnerModule,
        MatListModule,
        MatExpansionModule,
        MatTabsModule,
        MatButtonToggleModule,

        LeafletModule,
      ],
      providers: [{ provide: OneSignal, useValue: oneSignalSpy }],
    }).compileComponents();
    httpTestingController = TestBed.inject(HttpTestingController);
    router = TestBed.inject(Router);
    fixture = TestBed.createComponent(AppComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the app-component', () => {
    expect(component).toBeTruthy();
  });

  it('should have the title set to "TravelApp"', () => {
    expect(component.title).toEqual('TravelApp');
  });

  it('should render the app-header component', () => {
    const headerElement = fixture.nativeElement.querySelector('app-header');
    expect(headerElement).toBeTruthy();
  });

  it('should render the router-outlet component', () => {
    const routerOutletElement =
      fixture.nativeElement.querySelector('router-outlet');
    expect(routerOutletElement).toBeTruthy();
  });

  it('should initialize OneSignal', () => {
    const oneSignal = TestBed.inject(OneSignal);
    expect(oneSignal.init).toHaveBeenCalled();
  });
});
