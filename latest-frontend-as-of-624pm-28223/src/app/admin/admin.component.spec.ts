import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AdminComponent } from './admin.component';
import { DataService } from '../service/data.service';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { By } from '@angular/platform-browser';
import { MatSnackBarModule } from '@angular/material/snack-bar';

describe('AdminComponent', () => {
  let component: AdminComponent;
  let fixture: ComponentFixture<AdminComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AdminComponent],
      imports: [
        HttpClientModule,
        RouterTestingModule,
        HttpClientTestingModule,
        FormsModule,
        MatSnackBarModule
      ],
      providers: [DataService],
    }).compileComponents();

    fixture = TestBed.createComponent(AdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should have a notifications input field', () => {
    const inputElement = fixture.debugElement.query(By.css('input'));
    expect(inputElement).toBeTruthy();
  });

  it('should display user details in a table', () => {
    const tableElement = fixture.debugElement.query(By.css('table'));
    expect(tableElement).toBeTruthy();
    const tableRows = tableElement.nativeElement.querySelectorAll('tbody tr');
    expect(tableRows.length).toEqual(component.dataArr.length);
  });

  it('should display user details correctly in the table', () => {
    const tableElement = fixture.debugElement.query(By.css('table'));
    const tableRows = tableElement.nativeElement.querySelectorAll('tbody tr');
    for (let i = 0; i < component.dataArr.length; i++) {
      const row = tableRows[i];
      const rowData = component.dataArr[i];
      const cells = row.querySelectorAll('td');
      expect(cells[0].textContent).toContain(rowData.userName);
      expect(cells[1].textContent).toContain(rowData.dob);
      expect(cells[2].textContent).toContain(rowData.email);
      expect(cells[3].textContent).toContain(rowData.mobileNo);
    }
  });

  it('should have a notifications button', () => {
    const buttonElement = fixture.debugElement.query(By.css('button'));
    expect(buttonElement).toBeTruthy();
  });

  it('should initialize with an empty table', () => {
    const tableRows = fixture.debugElement.queryAll(By.css('tr'));
    expect(tableRows.length).toBe(1); // table header row should be the only row
  });
});
