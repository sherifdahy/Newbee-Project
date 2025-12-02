import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LandingPageComponent } from './pages/landing-page.component';
import { RouterModule, Routes } from '@angular/router';
import { MainLayoutComponent } from '../../shared/layouts/main-layout/main-layout.component';
import { AuthGuard } from '../../core/guards/is-logged-in-guard';
import { hasRoleGuard } from '../../core/guards/has-role-guard';
import { Role } from '../../core/enums/role.enum';

const routes: Routes = [
  {
    path: '',

    children: [
      {
        path: 'info',
        component: LandingPageComponent
      },
      {
        path: '',
        canActivate: [AuthGuard,hasRoleGuard],
        data: { roles: [Role.Admin, Role.Manager] },
        component: MainLayoutComponent,
        children: [
          {
            path: 'dashboard',
            canActivate: [AuthGuard],
            loadChildren: () => import('../dashboard/dashboard.module').then(x => x.DashboardModule)
          },
          {
            path: 'address-management',
            canActivate: [hasRoleGuard],
            data: { roles: [Role.Admin] },
            loadChildren: () => import('../address/address.module').then(x => x.AddressModule),
          },
          {
            path: 'active-code-management',
            canActivate: [hasRoleGuard],
            data: { roles: [Role.Admin] },
            loadChildren: () => import('../active-code/active-code.module').then(x => x.ActiveCodeModule),
          },
          {
            path: 'role-management',
            canActivate: [hasRoleGuard],
            data: { roles: [Role.Admin] },
            loadChildren: () => import('../role/role.module').then(x => x.RoleModule),
          },
          {
            path: 'company-management',
            canActivate: [hasRoleGuard],
            data: { roles: [Role.Admin] },
            loadChildren: () => import('../company/company.module').then(x => x.CompanyModule),
          },
          {
            path: 'category-management',
            canActivate: [hasRoleGuard],
            data: { roles: [Role.Manager] },
            loadChildren: () => import('../category/category.module').then(x => x.CategoryModule)
          },
          {
            path: 'product-management',
            canActivate: [hasRoleGuard],
            data: { roles: [Role.Manager] },
            loadChildren: () => import('../product/product.module').then(x => x.ProductModule)
          }
        ]
      }
    ]
  },
]

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
  ],
  declarations: [LandingPageComponent]
})
export class LandingPageModule { }
