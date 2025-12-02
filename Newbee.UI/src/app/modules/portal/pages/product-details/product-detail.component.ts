import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-product-detail',
  standalone: false,
  templateUrl: './product-detail.component.html',
  styleUrl: './product-detail.component.css'
})
export class ProductDetailComponent implements OnInit {
  product: any = {
    id: 1,
    name: 'هاتف ذكي Pro Max',
    category: 'إلكترونيات',
    price: 12500,
    originalPrice: 15000,
    image: 'https://via.placeholder.com/400x400?text=Phone',
    images: [
      'https://via.placeholder.com/400x400?text=Phone1',
      'https://via.placeholder.com/400x400?text=Phone2',
      'https://via.placeholder.com/400x400?text=Phone3'
    ],
    rating: 4.5,
    reviews: 328,
    inStock: true,
    quantity: 15,
    description: 'هاتف ذكي متقدم بأحدث التكنولوجيا والمواصفات العالية',
    features: [
      'شاشة AMOLED 6.7 بوصة',
      'معالج Snapdragon 8 Gen 3',
      'كاميرا 200 ميجابكسل',
      'بطارية 5000mAh',
      'شحن سريع 120W',
      'مقاوم للماء IP68'
    ],
    specifications: {
      'الشاشة': 'AMOLED 6.7 بوصة',
      'المعالج': 'Snapdragon 8 Gen 3',
      'الذاكرة': '12GB RAM',
      'التخزين': '256GB',
      'الكاميرا الخلفية': '200 ميجابكسل',
      'الكاميرا الأمامية': '40 ميجابكسل',
      'البطارية': '5000mAh',
      'الشحن': '120W سريع'
    },
    reviews_list: [
      { author: 'أحمد محمد', rating: 5, comment: 'منتج ممتاز وسريع التوصيل', date: '2025-11-10' },
      { author: 'فاطمة علي', rating: 4, comment: 'جودة عالية، السعر مناسب', date: '2025-11-09' },
      { author: 'محمود حسن', rating: 5, comment: 'أفضل هاتف اشتريته', date: '2025-11-08' }
    ]
  };

  quantity: number = 1;
  selectedImage: string = '';

  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.selectedImage = this.product.image;
  }

  getStarArray(rating: number) {
    return Array(5).fill(0).map((_, i) => i < Math.floor(rating) ? '⭐' : '☆');
  }

  selectImage(image: string) {
    this.selectedImage = image;
  }

  increaseQuantity() {
    if (this.quantity < this.product.quantity) this.quantity++;
  }

  decreaseQuantity() {
    if (this.quantity > 1) this.quantity--;
  }
}
