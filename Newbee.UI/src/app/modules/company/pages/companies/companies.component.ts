import { Component, OnInit } from '@angular/core';
import { CompanyService } from '../../../../core/services/company.service';
import { CompanyResponse } from '../../../../core/models/company/responses/company-response';
import { NotificationService } from '../../../../core/services/notification.service';
import { MatDialog } from '@angular/material/dialog';
import { CompanyDialogComponent } from '../../components/company-dialog/company-dialog.component';
import { ActivatedRoute, Route, Router } from '@angular/router';

@Component({
  selector: 'app-companies',
  standalone: false,
  templateUrl: './companies.component.html',
  styleUrls: ['./companies.component.css']
})
export class CompaniesComponent implements OnInit {

  companies!: CompanyResponse[];
  constructor(
    private notificationService: NotificationService,
    private companyService: CompanyService,
    private dialog : MatDialog,
    private router : Router,
    private route : ActivatedRoute
  ) {
    this.companies = [];
  }

  ngOnInit() {
    this.loadCompanies();
  }

  loadCompanies() {
    this.companyService.getAll().subscribe({
      next: (response) => {
        this.companies = response;
      },
      error: (errors) => {
        this.notificationService.showError(errors);
      }
    })
  }

  openCompanyDialog(){
    let dialog =  this.dialog.open(CompanyDialogComponent,{
      width : '80vw',
    });

    dialog.afterClosed().subscribe((result : CompanyResponse)=>{
      if(result)
      {
        this.router.navigate([result.id], { relativeTo: this.route });
      }
    })
  }
}
