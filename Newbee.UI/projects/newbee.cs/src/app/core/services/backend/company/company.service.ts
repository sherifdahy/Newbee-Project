import { Injectable } from '@angular/core';
import { StorageKeys } from '../../../statics/storage-keys';
import { GenericService } from '../generic/generic.service';
import { ICompany } from '../../../models/company';
import { Observable } from 'rxjs';
@Injectable()
export class CompanyService {
  constructor(private genericService: GenericService<ICompany>) {}
  getAll(): Observable<ICompany[]> {
    return this.genericService.getAll(StorageKeys.COMPANIES);
  }
  getById(id: number): Observable<ICompany> {
    return this.genericService.getById(StorageKeys.COMPANIES, id);
  }
  post(data: ICompany): Observable<ICompany> {
    return this.genericService.post(StorageKeys.COMPANIES, data);
  }

  put(data: ICompany, id: number): Observable<ICompany> {
    return this.genericService.put(StorageKeys.COMPANIES, data, id);
  }
  delete(id: number): Observable<void> {
    return this.genericService.delete(StorageKeys.COMPANIES, id);
  }
}
