import { CountryResponse } from "../../country/responses/country-response";

export interface GovernorateResponse {
  id : number;
  name : string;
  code : string;
  country : CountryResponse;
}
