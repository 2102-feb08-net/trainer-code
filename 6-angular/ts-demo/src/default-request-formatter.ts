import { HttpRequest } from "./httprequest";
import RequestFormatter from "./request-formatter";

export class DefaultRequestFormatter implements RequestFormatter {
  format(request: HttpRequest): string {
    let result = '';

    result += `${request.method} ${request.url} HTTP/${request.httpVersion}\n`;
    for (const headerName in request.headers) {
      result += `${headerName}: ${request.headers[headerName]}\n`;
    }
    result += '\n';

    if (request.method !== 'GET') {
      result += request.body;
    }

    return result;
  }

}
