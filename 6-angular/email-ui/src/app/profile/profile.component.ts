import { Component, OnInit } from '@angular/core';
import { OktaAuthService } from '@okta/okta-angular';
import { UserClaims } from '@okta/okta-auth-js';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css'],
})
export class ProfileComponent implements OnInit {
  user: UserClaims | null = null;

  constructor(private oktaAuth: OktaAuthService) {}

  ngOnInit(): void {
    this.oktaAuth.getUser().then((user) => {
      this.user = user;
    });
  }
}
