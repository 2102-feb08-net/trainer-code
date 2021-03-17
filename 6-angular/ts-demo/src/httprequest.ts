export type HttpMethod = 'GET' | 'POST';

export type HttpRequest = GetRequest | PostRequest;

export interface HttpRequestBase {
  method: HttpMethod;
  httpVersion: string;
  url: string;
  headers: { [headerName: string]: string };
}

export interface GetRequest extends HttpRequestBase {
  method: 'GET';
}

export interface PostRequest extends HttpRequestBase {
  method: 'POST';
  body: string;
}
