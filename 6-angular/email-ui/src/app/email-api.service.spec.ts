import { TestBed } from '@angular/core/testing';

import { EmailApiService } from './email-api.service';

describe('EmailApiService', () => {
  let service: EmailApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EmailApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
