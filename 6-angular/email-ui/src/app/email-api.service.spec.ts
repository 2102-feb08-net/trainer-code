import { HttpClient } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';
import { Observable, of } from 'rxjs';

import { EmailApiService } from './email-api.service';
import Message from './message';

describe('EmailApiService', () => {
  // let service: EmailApiService;

  // beforeEach(() => {
  //   TestBed.configureTestingModule({});
  //   service = TestBed.inject(EmailApiService);
  //   service = new EmailApiService();
  // });

  it('should be created', () => {
    const fakeClient = {} as HttpClient;
    const service = new EmailApiService(fakeClient);
    expect(service).toBeTruthy();
  });

  it('getMessages should get messages', () => {
    const observable: Observable<Message[]> = of([]);

    // let passedUrl: string = '';
    // const fakeClient = {
    //   get(url: string) {
    //     passedUrl = url;
    //     return observable;
    //   }
    // } as HttpClient;

    const spyClient = jasmine.createSpyObj('HttpClient', ['get']);
    spyClient.get.and.returnValue(observable);

    const service = new EmailApiService(spyClient);
    const result = service.getMessages();

    expect(spyClient.get).toHaveBeenCalledWith(
      'https://localhost:5001/api/inbox'
    );
    expect(result).toBe(observable);
  });
});
