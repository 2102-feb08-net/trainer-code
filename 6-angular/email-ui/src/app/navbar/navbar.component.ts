import { Component, OnInit } from '@angular/core';
import { OktaAuthService } from '@okta/okta-angular';
import { UserClaims } from '@okta/okta-auth-js';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  isAuthenticated = false;
  user: UserClaims | null = null;

  get profileText(): string {
    return this.user?.preferred_username ?? this.user?.email ?? 'Profile';
  }

  constructor(private oktaAuth: OktaAuthService) {}

  ngOnInit(): void {
    // this.oktaAuth.isAuthenticated().then((isAuthenticated) => {
    //   this.isAuthenticated = isAuthenticated;
    // });

    this.oktaAuth.$authenticationState.subscribe((isAuthenticated) => {
      this.isAuthenticated = isAuthenticated;
      if (isAuthenticated) {
        this.oktaAuth.getUser().then((user) => {
          this.user = user;
        });
      }
    });
  }

  login(): void {
    this.oktaAuth.signInWithRedirect();
  }

  logout(): void {
    this.oktaAuth.signOut();
  }
}
