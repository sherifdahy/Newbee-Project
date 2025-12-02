import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../../../../core/services/category.service';

@Component({
  selector: 'app-categories',
  standalone : false,
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class CategoriesComponent implements OnInit {

  constructor(
    private categoryService : CategoryService,
  ) { }

  ngOnInit() {
  }

}
