import { GovernorateResponse } from "../../governorate/responses/governorate-response";

export interface CityResponse {
  id : number;
  name : string;
  code : string;
  governorate : GovernorateResponse;
}
