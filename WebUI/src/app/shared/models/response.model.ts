import { BaseResponse } from "./base.response";

export class ResponseModel<T> extends BaseResponse {
  DataList: T[];
  Data: T;
}
