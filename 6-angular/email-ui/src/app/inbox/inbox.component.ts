import { Component, OnInit } from '@angular/core';
import { EmailApiService } from '../email-api.service';
import Message from '../message';
import { OktaAuthService } from '@okta/okta-angular';
import { UserClaims } from '@okta/okta-auth-js';

@Component({
  selector: 'app-inbox',
  templateUrl: './inbox.component.html',
  styleUrls: ['./inbox.component.css'],
})
export class InboxComponent implements OnInit {
  user: UserClaims | null = null;
  messages: Message[] | null = null;
  error: string | null = null;

  // reusable logic/data that is tied to a particular view on the page / part of the DOM
  //   belongs in a component.
  // reusable logic/data that is not tied to one particular part of the page
  //   belongs in an injectable/service, so that mulitple components (or other services)
  //  could have it injected.

  constructor(
    private oktaAuth: OktaAuthService,
    private apiService: EmailApiService
  ) {
    // put setup code that does not need the DOM to be ready and linked to the object here
  }

  ngOnInit(): void {
    this.oktaAuth.getUser().then((user) => {
      this.user = user;
      return user;
    }).then((user) => {
      if (user.email === undefined) {
        this.error = 'cannot identify user email';
      } else {
        this.apiService.getMessages(user.email).subscribe(
          (messages) => {
            // subscribe is (to simplify) like Promise.then
            this.messages = messages;

            // this (or rather in the service) would be a good place for some validation at runtime
            // to ensure the data coming from the backend matches expectations
          },
          (error) => {
            this.error = error.message;
          }
        );
      }
    });
    // put setup code that DOES need the DOM to be ready and linked to the object here
    // angular provides several "lifecycle hooks", ngOnInit is one
  }
}
