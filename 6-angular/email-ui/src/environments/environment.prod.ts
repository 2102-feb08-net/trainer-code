import { OktaConfig } from '@okta/okta-angular';

const oktaConfig: OktaConfig = {
  issuer: 'https://dev-723797.okta.com/oauth2/default',
  clientId: '0oa2mo0y1cehXX75q4x7',
  redirectUri: `${location.origin}/implicit/callback`,
  pkce: true,
  scopes: ['openid', 'profile', 'email'],
};

export const environment = {
  production: true,
  emailApiBaseUrl: 'https://2102-escalona-email.azurewebsites.net',
  okta: oktaConfig,
};
