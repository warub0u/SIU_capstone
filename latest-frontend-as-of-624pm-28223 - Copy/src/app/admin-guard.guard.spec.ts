import { TestBed } from '@angular/core/testing';
import { DataService } from './service/data.service';
import { HttpClientModule } from '@angular/common/http';
import { AdminGuardGuard } from './admin-guard.guard';
import { MatSnackBarModule } from '@angular/material/snack-bar';

describe('AdminGuardGuard', () => {
  let guard: AdminGuardGuard;

  beforeEach(async() => {
    await TestBed.configureTestingModule({
      imports: [HttpClientModule, MatSnackBarModule],
      providers: [DataService],
    });
    guard = TestBed.inject(AdminGuardGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
