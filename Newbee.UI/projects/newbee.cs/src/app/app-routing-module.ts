import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainLayout } from './core/layout/main-layout/main-layout';
import { SecondaryLayout } from './core/layout/secondary-layout/secondary-layout';
import { authGuard } from './core/guards/auth-guard';

const routes: Routes = [
  {
    path: 'auth',
    component: SecondaryLayout, // wrap with SecondaryLayout
    children: [
      {
        path: '',
        loadChildren: () =>
          import('./features/auth/auth-module').then((m) => m.AuthModule),
      },
    ],
  },
  {
    path: 'home',
    canActivate: [authGuard],
    component: MainLayout,
    children: [
      {
        path: '',
        loadChildren: () =>
          import('./features/home/home-module').then((m) => m.HomeModule),
      },
    ],
  },
  {
    path: 'company',
    canActivate: [authGuard],
    component: MainLayout,
    children: [
      {
        path: '',
        loadChildren: () =>
          import('./features/company/company-module').then(
            (m) => m.CompanyModule
          ),
      },
    ],
  },
  { path: '', redirectTo: 'home', pathMatch: 'full' }, // default
  { path: '**', redirectTo: 'home' }, // not-found redirect
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
