import { Injectable, OnDestroy } from '@angular/core';
import { BehaviorSubject, catchError, map, Observable, throwError } from 'rxjs';
import { ApiCallService } from './api-call.service';
import { BranchResponse } from '../models/branch/responses/branch-response';
import { BranchRequest } from '../models/branch/requests/branch-request';

@Injectable({
  providedIn: 'root'
})
export class BranchService implements OnDestroy {
  private readonly STORAGE_KEY = 'currentBranch';
  private branchSubject: BehaviorSubject<BranchResponse | null>;

  constructor(private apiCall: ApiCallService) {
    const savedBranch = this.loadFromStorage();
    this.branchSubject = new BehaviorSubject<BranchResponse | null>(savedBranch);
  }


  getAll(companyId : number) : Observable<BranchResponse[]>{
    return this.apiCall.get<BranchResponse[]>(`api/companies/${companyId}/branches`).pipe(
      catchError((response)=>{
        return throwError(()=> response.error.errors);
      })
    )
  }

  get(id : number) : Observable<BranchResponse[]>{
    return this.apiCall.get<BranchResponse[]>(`api/branches/${id}`).pipe(
      catchError((response)=>{
        return throwError(()=> response.error.errors);
      })
    )
  }

  create(companyId : number, request :BranchRequest) : Observable<BranchResponse>{
    return this.apiCall.post<BranchResponse>(`api/companies/${companyId}/branches`, request).pipe(
      catchError((response)=>{
        return throwError(()=> response.error.errors);
      })
    )
  }

  private loadFromStorage(): BranchResponse | null {
    try {
      const saved = localStorage.getItem(this.STORAGE_KEY);
      return saved ? JSON.parse(saved) : null;
    } catch (error) {
      console.error('Error loading branch from localStorage:', error);
      localStorage.removeItem(this.STORAGE_KEY);
      return null;
    }
  }

  get branch$(): Observable<BranchResponse | null> {
    return this.branchSubject.asObservable();
  }

  get currentBranchValue(): BranchResponse | null {
    return this.branchSubject.value;
  }

  setBranch(branchResponse: BranchResponse): void {
    this.branchSubject.next(branchResponse);
    try {
      localStorage.setItem(this.STORAGE_KEY, JSON.stringify(branchResponse));
    } catch (error) {
      console.error('Error saving branch to localStorage:', error);
    }
  }

  logoutFromBranch(): void {
    this.branchSubject.next(null);
    localStorage.removeItem(this.STORAGE_KEY);
  }

  hasBranch(): boolean {
    return this.branchSubject.value !== null;
  }

  ngOnDestroy(): void {
    this.branchSubject.complete();
  }
}
