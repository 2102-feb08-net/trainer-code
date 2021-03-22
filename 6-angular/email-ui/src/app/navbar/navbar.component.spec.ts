import { ComponentFixture, TestBed } from '@angular/core/testing';
import { OktaAuthService } from '@okta/okta-angular';
import { NEVER, Observable } from 'rxjs';

import { NavbarComponent } from './navbar.component';

describe('NavbarComponent', () => {
  let component: NavbarComponent;
  let fixture: ComponentFixture<NavbarComponent>;

  beforeEach(async () => {
    const authSpy = jasmine.createSpyObj(
      'OktaAuthService',
      ['signInWithRedirect', 'signOut'],
      ['$authenticationState']
    );
    (Object.getOwnPropertyDescriptor(authSpy, '$authenticationState')
      ?.get as jasmine.Spy<() => Observable<boolean>>).and.returnValue(NEVER);

    await TestBed.configureTestingModule({
      declarations: [NavbarComponent],
      providers: [{ provide: OktaAuthService, useValue: authSpy }],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NavbarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
