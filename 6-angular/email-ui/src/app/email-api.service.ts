import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import Message from './message';

@Injectable({
  providedIn: 'root', // singleton for whole app (includes the tests)
})
export class EmailApiService {
  private readonly baseUrl = environment.emailApiBaseUrl;

  constructor(private http: HttpClient) {}

  getMessages(): Observable<Message[]> {
    return this.http.get<Message[]>(`${this.baseUrl}/api/mail`);
  }
}
