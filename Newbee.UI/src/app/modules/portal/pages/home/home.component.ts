import { Component } from '@angular/core';

@Component({
  selector: 'app-ecom-home',
  standalone: false,
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  categories = [
    { id: 1, name: 'الإلكترونيات', icon: '💻' },
    { id: 2, name: 'الملابس', icon: '👕' },
    { id: 3, name: 'الأحذية', icon: '👟' },
    { id: 4, name: 'الإكسسوارات', icon: '⌚' },
    { id: 5, name: 'المنزل والحديقة', icon: '🏠' },
    { id: 6, name: 'الكتب', icon: '📚' }
  ];

  products = [
    {
      id: 2,
      name: 'سماعة AirPods Pro (الجيل الثاني)',
      category: 'إلكترونيات',
      price: 8999,
      image: 'https://store.storeimages.cdn-apple.com/4668/as-images.apple.com/is/MQD83_AV1?wid=1144&hei=1144&fmt=jpeg&qlt=90&.v=1660803961719',
      rating: 4.7,
      reviews: 986
    },


    {
      id: 5,
      name: 'حذاء رياضي Nike Air Max 270',
      category: 'أحذية',
      price: 3299,
      image: 'https://static.nike.com/a/images/c_limit,w_592,f_auto/t_product_v1/151a7b30-cd84-43e0-b0db-36e2c2e9b1e5/air-max-270-mens-shoes-KkLcGR.png',
      rating: 4.8,
      reviews: 1523
    },

    {
      id: 8,
      name: 'جاكيت شتوي مبطن من H&M',
      category: 'ملابس',
      price: 2499,
      image: 'https://lp2.hm.com/hmgoepprod?set=quality[79],source[/f1/68/f16852791a7e7b0e7b5d7a4c9de4a2aafab0c5d2.jpg],origin[dam],category[men_jacketscoats_parka],type[LOOKBOOKIMAGE],res[m],hmver[1]&call=url[file:/product/main]',
      rating: 4.6,
      reviews: 452
    }
  ];


  selectedCategory: number | null = null;

  get filteredProducts() {
    return this.selectedCategory
      ? this.products.filter(p => p.category === this.categories.find(c => c.id === this.selectedCategory)?.name)
      : this.products;
  }

  selectCategory(categoryId: number) {
    this.selectedCategory = this.selectedCategory === categoryId ? null : categoryId;
  }

  getStarArray(rating: number) {
    return Array(5).fill(0).map((_, i) => i < Math.floor(rating) ? '⭐' : '☆');
  }
}
