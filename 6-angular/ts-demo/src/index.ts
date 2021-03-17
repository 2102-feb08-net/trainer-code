import { DefaultRequestFormatter } from './default-request-formatter';
import { GetRequest, HttpRequest } from './httprequest';
import RequestFormatter from './request-formatter';

export function updateDisplay(
  request?: HttpRequest,
  formatter?: RequestFormatter
) {
  if (!formatter) {
    formatter = new DefaultRequestFormatter();
  }
  // const asdf = $('#request-display code');
  const display = document.querySelector('#request-display code');
  if (!display) {
    throw new Error('page in invalid state');
  }
  if (request === undefined) {
    // var input = <HTMLTextAreaElement>document.querySelector('#request');
    // var input = document.querySelector('#request') as HTMLTextAreaElement;
    const input = document.querySelector<HTMLTextAreaElement>('#request');
    if (!input) {
      throw new Error('page in invalid state');
    }
    request = JSON.parse(input.value) as HttpRequest;
  }

  const result = formatter.format(request);

  display.textContent = result;
}

export function showARequest() {
  const request = {
    method: 'GET',
    httpVersion: '1.1',
    url: '/user-data',
    headers: {
      Accept: 'application/json',
      Host: 'localhost:8080'
    }
  };
  updateDisplay(request as GetRequest);
}

document
  .getElementById('update-display-button')
  ?.addEventListener('click', () => updateDisplay());
document
  .getElementById('show-request-button')
  ?.addEventListener('click', () => showARequest());

class ObjWithLength {
  length: number = 5;
}

abstract class HelperMethods {
  abstract nonstatic(): void;

  static tsStuff(parameter: number | string) {
    let variable: number | string | ObjWithLength = 5;

    variable = 3;
    variable = 'asdf';
    variable = { length: 1 };
    // variable = true;
    // if (typeof parameter === "number") {
    if (typeof parameter !== 'number') {
      const length = parameter.length;
    }
  }
}

HelperMethods.tsStuff('asdf');
