import { ManagerRequest } from "../../manager/requests/manager-request";

export interface RegisterCompanyRequest {
  name : string;
  rin : string;
  manager : ManagerRequest;
}
