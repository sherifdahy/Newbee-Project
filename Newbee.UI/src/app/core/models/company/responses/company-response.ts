import { ActiveCodeResponse } from "../../active-code/response/active-code-response";
import { BranchResponse } from "../../branch/responses/branch-response";
import { ManagerResponse } from "../../manager/responses/manager-response";

export interface CompanyResponse {
  id : number;
  name : string;
  rin : string;
  activeCodes : ActiveCodeResponse[];
  branches : BranchResponse[];
  managers : ManagerResponse[];
}
