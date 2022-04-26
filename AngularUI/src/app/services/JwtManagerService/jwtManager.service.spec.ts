import { TestBed } from '@angular/core/testing';

import { JwtManagerService } from './jwtManager.service';

describe('JwtManagerService', () => {
  let service: JwtManagerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(JwtManagerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
