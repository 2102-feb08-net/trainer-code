import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import Message from './message';

@Injectable({
  providedIn: 'root', // singleton for whole app (includes the tests)
})
export class EmailApiService {
  private readonly baseUrl = environment.emailApiBaseUrl;

  constructor(private http: HttpClient) {}

  getMessages(address: string): Observable<Message[]> {
    return this.http.get<Message[]>(`${this.baseUrl}/api/mailbox/${address}`);
  }
}
