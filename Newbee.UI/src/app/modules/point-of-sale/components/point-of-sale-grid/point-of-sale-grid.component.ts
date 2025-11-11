import { Component, EventEmitter, Input, input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { PointOfSaleResponse } from '../../../../core/models/point-of-sale/responses/point-of-sale-response';
import { PointOfSaleService } from '../../../../core/services/point-of-sale.service';
import { NotificationService } from '../../../../core/services/notification.service';

@Component({
  selector: 'app-point-of-sale-grid',
  standalone: false,
  templateUrl: './point-of-sale-grid.component.html',
  styleUrls: ['./point-of-sale-grid.component.css']
})
export class PointOfSaleGridComponent implements OnInit, OnChanges {

  @Input('branchId') branchId!: number;

  @Output() openPointOfSaleDialogListner = new EventEmitter<number>();

  pointOfSales!: PointOfSaleResponse[];
  constructor(
    private notificationService: NotificationService,
    private pointOfSaleService: PointOfSaleService
  ) {
    this.pointOfSales = [];
    this.branchId = 0;
  }
  ngOnChanges(): void {
    this.loadPointOfSales();
  }

  ngOnInit() {

  }

  removePointOfSale(pointOfSaleId: number) {
    this.pointOfSaleService.delete(pointOfSaleId).subscribe({
      next: () => {
        this.loadPointOfSales();
        this.notificationService.showSuccess("تم حذف نقطة البيع بنجاح.");
      },
      error: (errors: any) => {
        this.notificationService.showError(errors);
      }
    });
  }

  loadPointOfSales() {
    if (this.branchId) {
      this.pointOfSaleService.getAll(this.branchId).subscribe({
        next: (response) => {
          this.pointOfSales = response;
        },
        error: (errors: any) => {
          this.notificationService.showError(errors);
        }
      });
    }
  }

}
