import { browser, by, element } from 'protractor';

export class AppPage {
  async navigateTo(): Promise<unknown> {
    return browser.get(browser.baseUrl);
  }

  async getHomeText(): Promise<string> {
    return element(by.css('app-root main p')).getText();
  }
}
