import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from './shared/components/not-found/not-found.component';
import { NoAccessComponent } from './shared/components/no-access/no-access.component';
import { BranchSelectionComponent } from './shared/components/branch-selection/branch-selection.component';
import { alreadyLoggedInGuard } from './core/guards/already-logged-in.guard';
import { hasRoleGuard } from './core/guards/has-role-guard';
import { Role } from './core/enums/role.enum';

const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./modules/portal/portal.module').then(x => x.PortalModule),
  },
  {
    path: 'auth',
    canActivate: [alreadyLoggedInGuard],
    loadChildren: () => import('./modules/authentication/authentication.module').then(x => x.AuthenticationModule)
  },
  {
    path: 'partner',
    loadChildren: () => import('./modules/landing-page/landing-page.module').then(x => x.LandingPageModule)
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
