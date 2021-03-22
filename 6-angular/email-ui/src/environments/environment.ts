// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

import { OktaConfig } from '@okta/okta-angular';

const oktaConfig: OktaConfig = {
  clientId: '0oa2mo0y1cehXX75q4x7',
  issuer: 'https://dev-723797.okta.com/oauth2/default',
  redirectUri: `${location.origin}/login/callback`,
  scopes: ['openid', 'profile', 'email', 'groups'],
  pkce: true,
};

export const environment = {
  production: false,
  emailApiBaseUrl: 'https://localhost:44317',
  okta: oktaConfig,
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
