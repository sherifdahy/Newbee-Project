import { Injectable } from '@angular/core';
import { ApiRoute } from '../../../statics/api-routes';
import { GenericService } from '../generic/generic.service';
import { ICompany } from '../../../models/company';
import { Observable } from 'rxjs';
@Injectable()
export class CompanyService {
  constructor(private genericService: GenericService<ICompany>) {}
  getAll(): Observable<ICompany[]> {
    return this.genericService.getAll(ApiRoute.COMPANIES);
  }
  getById(id: number): Observable<ICompany> {
    return this.genericService.getById(ApiRoute.COMPANIES, id);
  }
  post(data: ICompany): Observable<ICompany> {
    return this.genericService.post(ApiRoute.COMPANIES, data);
  }

  put(data: ICompany, id: number): Observable<ICompany> {
    return this.genericService.put(ApiRoute.COMPANIES, data, id);
  }
  delete(id: number): Observable<void> {
    return this.genericService.delete(ApiRoute.COMPANIES, id);
  }
}
