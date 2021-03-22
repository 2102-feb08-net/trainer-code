import { OktaConfig } from '@okta/okta-angular';

const oktaConfig: OktaConfig = {
  clientId: '0oa2mo0y1cehXX75q4x7',
  issuer: 'https://dev-723797.okta.com/oauth2/default',
  redirectUri: `${location.origin}/implicit/callback`,
  scopes: ['openid', 'profile', 'email', 'groups'],
  pkce: true,
};

export const environment = {
  production: true,
  emailApiBaseUrl: 'https://2102-escalona-email.azurewebsites.net',
  okta: oktaConfig,
};
