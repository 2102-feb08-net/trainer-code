import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';

import { EmailApiService } from './email-api.service';
import Message from './message';

describe('EmailApiService', () => {
  it('should be created', () => {
    const fakeClient = {} as HttpClient;
    const service = new EmailApiService(fakeClient);
    expect(service).toBeTruthy();
  });

  it('getMessages should get messages', () => {
    const observable: Observable<Message[]> = of([]);

    const spyClient = jasmine.createSpyObj('HttpClient', ['get']);
    spyClient.get.and.returnValue(observable);

    const service = new EmailApiService(spyClient);
    const result = service.getMessages();

    expect(spyClient.get).toHaveBeenCalledWith(
      `${environment.emailApiBaseUrl}/api/mailbox/nick.escalona@revature.com`
    );
    expect(result).toBe(observable);
  });
});
