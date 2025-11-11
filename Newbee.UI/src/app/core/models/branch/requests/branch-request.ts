import { PointOfSaleRequest } from "../../point-of-sale/requests/point-of-sale-request";

export interface BranchRequest {
  buildingNumber: string;
  street: string;
  code: string;
  cityId: number;
  employeeIds : number[];
  pointOfSales : PointOfSaleRequest[];
}
