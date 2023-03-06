import { HttpClient, HttpHandler } from '@angular/common/http';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { ActivatedRoute, convertToParamMap, Router } from '@angular/router';
import { DataService } from '../service/data.service';
import { UploadComponent } from "../upload/upload.component";
import { ProfileComponent } from './profile.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';

describe('ProfileComponent', () => {
  let component: ProfileComponent;
  let fixture: ComponentFixture<ProfileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProfileComponent, UploadComponent ],
      imports:[MatDialogModule, ReactiveFormsModule, MatSnackBarModule],
      providers:[HttpClient, DataService, HttpHandler, MatDialog, FormBuilder, Router, {provide: ActivatedRoute, useValue: {snapshot: {paramMap: convertToParamMap({username: 'abc'})}}}]
    })
    .compileComponents();
  });
  
  beforeEach(() => {
    fixture = TestBed.createComponent(ProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});






