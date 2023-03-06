import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { HomeComponent } from './home.component';
import { DataService } from '../service/data.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatTabsModule } from '@angular/material/tabs';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatSnackBarModule } from '@angular/material/snack-bar';

describe('HomeComponent', () => {
  let component: HomeComponent;
  let fixture: ComponentFixture<HomeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [HomeComponent],
      imports: [
        HttpClientModule,
        BrowserAnimationsModule,
        MatDialogModule,
        MatTabsModule,
        MatInputModule,
        MatCardModule,
        ReactiveFormsModule,
        FormsModule,
        MatSnackBarModule,
      ],
      providers: [DataService, HttpClient, MatDialog],
      teardown: {destroyAfterEach: false}
    }).compileComponents();

    fixture = TestBed.createComponent(HomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should set isLoading to true while fetching data', () => {
    component.directionForm.controls['Source'].setValue('640856');
    component.directionForm.controls['Destination'].setValue(
      '730605'
    );
    component.click2();
    expect(component.isLoading).toBeTrue();
  });

  it('should update Durationz and farez when form is submitted', () => {
    component.directionForm.controls['Source'].setValue('640856');
    component.directionForm.controls['Destination'].setValue(
      '730605'
    );
    component.click2();
    expect(component.Durationz).toBeDefined();
    expect(component.farez).toBeDefined();
  });

  it('should have a map-container', () => {
    const mapContainer = fixture.nativeElement.querySelector('.map-container');
    expect(mapContainer).toBeTruthy();
  });

  it('should have a search-container', () => {
    const searchContainer =
      fixture.nativeElement.querySelector('.search-container');
    expect(searchContainer).toBeTruthy();
  });
});
