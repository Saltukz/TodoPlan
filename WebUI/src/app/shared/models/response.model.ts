import { BaseResponse } from "./base.response";

export class ResponseModel<T> extends BaseResponse {
  dataList: T[];
  data: T;
}
