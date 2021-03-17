import { HttpRequest } from "./httprequest";

export default interface RequestFormatter {
  format(request: HttpRequest): string;
}
