import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from './shared/components/not-found/not-found.component';
import { hasRoleGuard } from './core/guards/has-role-guard';
import { Role } from './core/enums/role.enum';
import { NoAccessComponent } from './shared/components/no-access/no-access.component';
import { hasBranchGuard } from './core/guards/has-branch.guard';
import { AuthGuard } from './core/guards/is-logged-in-guard';
import { BranchSelectionComponent } from './shared/components/branch-selection/branch-selection.component';
import { alreadyLoggedInGuard } from './core/guards/already-logged-in.guard';
import { MainLayoutComponent } from './shared/layouts/main-layout/main-layout.component';

const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./modules/landing-page/landing-page.module').then(x => x.LandingPageModule),
  },
  {
    path: 'auth',
    canActivate: [alreadyLoggedInGuard],
    loadChildren: () => import('./modules/authentication/authentication.module').then(x => x.AuthenticationModule)
  },
  {
    path: 'portal',
    component: MainLayoutComponent,
    canActivate: [hasBranchGuard],
    children: [
      {
        path : 'dashboard',
        loadChildren :()=> import('./modules/portal-dashboard/portal-dashboard.module').then(x=>x.PortalDashboardModule),
      },
      {
        path: 'product-management',
        loadChildren: () => import('./modules/product/product.module').then(x => x.ProductModule),
      }
    ]
  },
  {
    path: 'admin',
    component: MainLayoutComponent,
    children: [
      {
        path: '',
        canActivate: [AuthGuard],
        loadChildren: () => import('./modules/dashboard/dashboard.module').then(x => x.DashboardModule)
      },
      {
        path: 'address-management',
        canActivate: [AuthGuard, hasRoleGuard],
        data: { roles: [Role.Admin] },
        loadChildren: () => import('./modules/address/address.module').then(x => x.AddressModule),
      },
      {
        path: 'active-code-management',
        canActivate: [AuthGuard, hasRoleGuard],
        data: { roles: [Role.Admin] },
        loadChildren: () => import('./modules/active-code/active-code.module').then(x => x.ActiveCodeModule),
      },
      {
        path: 'role-management',
        canActivate: [AuthGuard, hasRoleGuard],
        data: { roles: [Role.Admin] },
        loadChildren: () => import('./modules/role/role.module').then(x => x.RoleModule),
      },
      {
        path: 'company-management',
        canActivate: [AuthGuard, hasRoleGuard],
        data: { roles: [Role.Admin] },
        loadChildren: () => import('./modules/company/company.module').then(x => x.CompanyModule),
      }
    ]
  },
  {
    path: 'branch-selection',
    component: BranchSelectionComponent
  },
  {
    path: 'no-access',
    component: NoAccessComponent
  },
  {
    path: '**',
    component: NotFoundComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
