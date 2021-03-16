function updateDisplay(request: HttpRequest | undefined) {
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

  let result = '';

  result += `${request.method} ${request.url} HTTP/${request.httpVersion}\n`;
  for (const headerName in request.headers) {
    result += `${headerName}: ${request.headers[headerName]}\n`;
  }
  result += '\n';

  if (request.method !== "GET") {
    result += request.body;
  }

  display.textContent = result;
}

function showARequest() {
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

type HttpMethod =
  | "GET"
  | "POST";

type HttpRequest = GetRequest | PostRequest;

interface HttpRequestBase {
  method: HttpMethod;
  httpVersion: string;
  url: string;
  headers: { [headerName: string]: string };
}

interface GetRequest extends HttpRequestBase {
  method: "GET";
}

interface PostRequest extends HttpRequestBase {
  method: "POST";
  body: string;
}

class ObjWithLength {
  length: number = 5;
}

function tsStuff(parameter: number | string) {
  let variable: number | string | ObjWithLength = 5;

  variable = 3;
  variable = "asdf";
  variable = { length: 1 };
  // variable = true;
  // if (typeof parameter === "number") {
  if (typeof parameter !== "number") {
    const length = parameter.length;
  }
}
