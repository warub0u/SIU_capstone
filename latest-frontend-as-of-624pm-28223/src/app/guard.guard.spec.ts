import { TestBed } from '@angular/core/testing';
import { DataService } from './service/data.service';
import { HttpClientModule } from '@angular/common/http';
import { GuardGuard } from './guard.guard';

describe('GuardGuard', () => {
  let guard: GuardGuard;

  beforeEach(async() => {
    await TestBed.configureTestingModule({
      imports:[HttpClientModule],
      providers: [DataService],
    });
    guard = TestBed.inject(GuardGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
