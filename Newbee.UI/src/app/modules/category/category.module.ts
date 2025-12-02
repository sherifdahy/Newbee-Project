import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Route, RouterModule } from '@angular/router';
import { CategoriesComponent } from './pages/categories/categories.component';
import { CategoryGridComponent } from './components/category-grid/category-grid.component';
import { CategoryDialogComponent } from './components/category-dialog/category-dialog.component';

const routes : Route[] =[
  {
    path : 'categories',
    component : CategoriesComponent,
  }
]

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
  ],
  declarations: [
    CategoriesComponent,
    CategoryGridComponent,
    CategoryDialogComponent,
  ]
})
export class CategoryModule { }
