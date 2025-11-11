import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { CompanyResponse } from '../../../core/models/company/responses/company-response';
import { BranchResponse } from '../../../core/models/branch/responses/branch-response';
import { BranchService } from '../../../core/services/branch.service';
import { CompanyService } from '../../../core/services/company.service';
import { AuthService } from '../../../core/services/auth.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-branch-selection',
  imports : [CommonModule],
  templateUrl: './branch-selection.component.html',
  styleUrls: ['./branch-selection.component.css']
})
export class BranchSelectionComponent{
  // companies: CompanyResponse[] = [];
  // branches: BranchResponse[] = [];

  // selectedCompany: CompanyResponse | null = null;
  // selectedBranch: BranchResponse | null = null;

  // isLoadingCompanies = false;
  // isLoadingBranches = false;
  // isConfirming = false;

  // private destroy$ = new Subject<void>();

  // // TODO: استبدل هذه بـ API calls حقيقية للـ lookups
  // private cities: { [key: number]: string } = {
  //   1: 'القاهرة',
  //   2: 'الإسكندرية',
  //   3: 'الجيزة',
  //   4: 'المنصورة',
  //   5: 'طنطا',
  //   6: 'أسيوط'
  // };

  // private governorates: { [key: number]: string } = {
  //   1: 'القاهرة',
  //   2: 'الإسكندرية',
  //   3: 'الجيزة',
  //   4: 'الدقهلية',
  //   5: 'الغربية',
  //   6: 'أسيوط'
  // };

  // private countries: { [key: number]: string } = {
  //   1: 'مصر',
  //   2: 'السعودية',
  //   3: 'الإمارات'
  // };

  // constructor(
  //   private branchService: BranchService,
  //   private companyService: CompanyService,
  //   private authService: AuthService,
  //   private router: Router
  // ) {}

  // ngOnInit(): void {
  //   // لو في شركة وفرع محفوظين، روح للداشبورد
  //   if (this.companyService.hasCompany() && this.branchService.hasBranch()) {
  //     this.router.navigate(['/dashboard']);
  //     return;
  //   }

  //   // لو في شركة محفوظة بس، حملها واعرض الفروع
  //   if (this.companyService.hasCompany()) {
  //     this.selectedCompany = this.companyService.currentCompanyValue;
  //     this.loadBranches();
  //   }

  //   // حمّل الشركات في كل الأحوال
  //   this.loadCompanies();
  // }

  // loadCompanies(): void {
  //   this.isLoadingCompanies = true;

  //   // TODO: استبدل بالـ API call الحقيقي
  //   this.apiCall.get<CompanyResponse[]>('api/companies')
  //     .pipe(takeUntil(this.destroy$))
  //     .subscribe({
  //       next: (companies) => {
  //         this.companies = companies;
  //         this.isLoadingCompanies = false;
  //       },
  //       error: (error) => {
  //         console.error('Error loading companies:', error);
  //         this.isLoadingCompanies = false;
  //       }
  //     });


  // }

  // onSelectCompany(company: CompanyResponse): void {
  //   // لو نفس الشركة، ما نعملش حاجة
  //   if (this.selectedCompany?.id === company.id) {
  //     return;
  //   }

  //   this.selectedCompany = company;
  //   this.selectedBranch = null; // Reset الفرع المختار
  //   this.companyService.setCompany(company);

  //   // Scroll للفروع
  //   setTimeout(() => {
  //     const branchesSection = document.querySelector('hr');
  //     if (branchesSection) {
  //       branchesSection.scrollIntoView({ behavior: 'smooth', block: 'start' });
  //     }
  //   }, 100);

  //   this.loadBranches();
  // }

  // loadBranches(): void {
  //   if (!this.selectedCompany) return;

  //   // لو الشركة فيها فروع جاهزة، استخدمها
  //   if (this.selectedCompany.branches && this.selectedCompany.branches.length > 0) {
  //     this.branches = this.selectedCompany.branches;
  //     this.isLoadingBranches = false;
  //   } else {
  //     // لو مش موجودة، اجلبها من الـ API
  //     this.loadBranchesFromAPI(this.selectedCompany.id);
  //   }
  // }

  // loadBranchesFromAPI(companyId: number): void {
  //   this.isLoadingBranches = true;

  //   // TODO: استبدل بالـ API call الحقيقي
  //   this.apiCall.get<BranchResponse[]>(`api/companies/${companyId}/branches`)
  //     .pipe(takeUntil(this.destroy$))
  //     .subscribe({
  //       next: (branches) => {
  //         this.branches = branches;
  //         this.isLoadingBranches = false;
  //       },
  //       error: (error) => {
  //         console.error('Error loading branches:', error);
  //         this.isLoadingBranches = false;
  //       }
  //     });
  // }

  // onSelectBranch(branch: BranchResponse): void {
  //   this.selectedBranch = branch;
  // }

  // onConfirmSelection(branch: BranchResponse): void {
  //   this.selectedBranch = branch;
  //   this.isConfirming = true;

  //   // حفظ الفرع والدخول للداشبورد
  //   this.branchService.setBranch(branch);

  //   // Simulate API call delay (احذفه في الإنتاج)
  //   setTimeout(() => {
  //     this.router.navigate(['/dashboard']);
  //   }, 500);
  // }

  // clearCompanySelection(): void {
  //   this.selectedCompany = null;
  //   this.selectedBranch = null;
  //   this.branches = [];
  //   this.companyService.clearCompany();

  //   // Scroll للشركات
  //   window.scrollTo({ top: 0, behavior: 'smooth' });
  // }

  // logout(): void {
  //   this.authService.logout();
  //   this.companyService.clearCompany();
  //   this.branchService.logoutFromBranch();
  //   this.router.navigate(['/auth/login']);
  // }

  // getCityName(cityId: number): string | null {
  //   return this.cities[cityId] || null;
  // }

  // getGovernorateName(governorateId: number): string | null {
  //   return this.governorates[governorateId] || null;
  // }

  // getCountryName(countryId: number): string | null {
  //   return this.countries[countryId] || null;
  // }

  // ngOnDestroy(): void {
  //   this.destroy$.next();
  //   this.destroy$.complete();
  // }
}
