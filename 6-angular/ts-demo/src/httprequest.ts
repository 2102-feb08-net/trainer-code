type HttpMethod = 'GET' | 'POST';

type HttpRequest = GetRequest | PostRequest;

interface HttpRequestBase {
  method: HttpMethod;
  httpVersion: string;
  url: string;
  headers: { [headerName: string]: string };
}

interface GetRequest extends HttpRequestBase {
  method: 'GET';
}

interface PostRequest extends HttpRequestBase {
  method: 'POST';
  body: string;
}
