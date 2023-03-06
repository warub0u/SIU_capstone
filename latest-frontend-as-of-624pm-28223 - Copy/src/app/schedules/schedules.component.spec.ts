import { ComponentFixture, TestBed } from '@angular/core/testing';
import { DataService } from '../service/data.service';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, NgControl, ReactiveFormsModule } from '@angular/forms';
import { SchedulesComponent } from './schedules.component';
import { of } from 'rxjs';
import { MatSnackBarModule } from '@angular/material/snack-bar';

describe('SchedulesComponent', () => {
  let component: SchedulesComponent;
  let fixture: ComponentFixture<SchedulesComponent>;
  let service: DataService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SchedulesComponent],
      imports: [
        HttpClientModule,
        FormsModule,
        BrowserAnimationsModule,
        ReactiveFormsModule,
        MatSnackBarModule
      ],
      providers: [DataService, HttpClient, NgControl],
    }).compileComponents();

    fixture = TestBed.createComponent(SchedulesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should have the correct title', () => {
    const title = fixture.nativeElement.querySelector('h5').textContent;
    expect(title).toEqual('Bookmarked Stops');
  });

  it('should have a form for entering a bus stop code', () => {
    const form = fixture.nativeElement.querySelector('form');
    expect(form).toBeTruthy();
  });

  // it('should set busStopCode to "65009" by default in ngOnInit', () => {
  //   component.ngOnInit();
  //   expect(component.busStopCode).toEqual('65009');
  // });

  // it('should call OnSubmit method when getStop is called', () => {
  //   const onSubmitSpy = spyOn(component, 'OnSubmit');
  //   const busStopCode = '65009';
  //   component.getStop(busStopCode);
  //   expect(component.busStopCode).toEqual(busStopCode);
  //   expect(onSubmitSpy).toHaveBeenCalled();
  // });
});
