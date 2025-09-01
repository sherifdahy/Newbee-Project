import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainLayout } from './core/layout/main-layout/main-layout';
import { SecondaryLayout } from './core/layout/secondary-layout/secondary-layout';

const routes: Routes = [
  {
    path: 'auth',
    component: SecondaryLayout, // wrap with SecondaryLayout
    children: [
      {
        path: '',
        loadChildren: () =>
          import('./features/auth/auth-module').then(m => m.AuthModule)
      }
    ]
  },
  {
    path: 'cart',
    component: MainLayout,
    children: [
      {
        path: '',
        loadChildren: () =>
          import('./features/cart/cart-module').then(m => m.CartModule)
      }
    ]
  },
  { path: '', redirectTo: 'auth/login', pathMatch: 'full' }, // default
  { path: '**', redirectTo: 'auth/login' } // not-found redirect
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
