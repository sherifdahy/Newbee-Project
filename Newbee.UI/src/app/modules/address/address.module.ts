import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { CountriesComponent } from './pages/country/countries/countries.component';
import { GovernoratesComponent } from './pages/governorate/governorates/governorates.component';
import { CitiesComponent } from './pages/city/cities/cities.component';
import { GovernorateDialogComponent } from './components/governorate-dialog/governorate-dialog.component';
import { CityDialogComponent } from './components/city-dialog/city-dialog.component';
import { CountryDialogComponent } from './components/country-dialog/country-dialog.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { hasPermissionGuard } from '../../core/guards/has-permission-guard';
import { Permissions } from '../../core/enums/permissions.enum';


const routes: Routes = [
  {
    path: 'countries',
    canActivate: [hasPermissionGuard],
    data: { permissions: [Permissions.CountiesRead] },
    component: CountriesComponent
  },
  {
    path: 'governorates',
    canActivate: [hasPermissionGuard],
    data: { permissions: [Permissions.GovernoratiesRead] },
    component: GovernoratesComponent,
  },
  {
    path: 'cities',
    canActivate: [hasPermissionGuard],
    data: { permissions: [Permissions.CitiesRead] },
    component: CitiesComponent
  }
]

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule,
    FormsModule
  ],
  declarations: [
    CountriesComponent,
    GovernoratesComponent,
    CitiesComponent,
    GovernorateDialogComponent,
    CityDialogComponent,
    CountryDialogComponent
  ]
})
export class AddressModule { }
