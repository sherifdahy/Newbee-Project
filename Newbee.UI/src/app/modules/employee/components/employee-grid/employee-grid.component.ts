import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { EmployeeService } from '../../../../core/services/employee.service';
import { NotificationService } from '../../../../core/services/notification.service';
import { EmployeeResponse } from '../../../../core/models/employee/responses/employee-response';

@Component({
  selector: 'app-employee-grid',
  standalone: false,
  templateUrl: './employee-grid.component.html',
  styleUrls: ['./employee-grid.component.css']
})
export class EmployeeGridComponent implements OnInit, OnChanges {

  @Input('branchId') branchId!: number;

  employees!: EmployeeResponse[];

  constructor(
    private employeeService: EmployeeService,
    private notificationService: NotificationService,
  ) {
    this.employees = [];
  }
  ngOnChanges(changes: SimpleChanges): void {
    this.loadEmployees();
  }

  ngOnInit() {
  }

  loadEmployees() {
    if (this.branchId) {
      this.employeeService.getAll(this.branchId).subscribe({
        next: (response: EmployeeResponse[]) => {
          this.employees = response;
        },
        error: (errors) => {
          this.notificationService.showError(errors);
        }
      });
    }
  }
}
