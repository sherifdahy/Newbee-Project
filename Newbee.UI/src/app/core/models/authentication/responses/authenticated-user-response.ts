import { Role } from "../../../enums/role.enum";

export interface AuthenticatedUserResponse {
  id : string,
  email : string;
  roles : Role[],
  permissions : string[]
}
