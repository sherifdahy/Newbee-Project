import { Component, OnInit } from '@angular/core';
import { CityService } from '../../../../../core/services/city.service';
import { NotificationService } from '../../../../../core/services/notification.service';
import { CityResponse } from '../../../../../core/models/city/responses/city-response';
import { MatDialog } from '@angular/material/dialog';
import { CityDialogComponent } from '../../../components/city-dialog/city-dialog.component';

@Component({
  selector: 'app-cities',
  standalone: false,
  templateUrl: './cities.component.html',
  styleUrls: ['./cities.component.css']
})
export class CitiesComponent implements OnInit {

  cities!: CityResponse[];

  constructor(
    private cityService: CityService,
    private notificationService: NotificationService,
    private dialog : MatDialog
  ) {
    this.cities = [];
  }

  ngOnInit() {
    this.loadCities();
  }
  handleOpenDialog(id: number) {

    const dialogRef = this.dialog.open(CityDialogComponent, {
      width: '600px',
      data: id
        ? { editMode: true, id }
        : { editMode: false }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadCities();
      }
    });
  }
  handleDeleteGovernorate(id: number) {

  }
  loadCities() {
    this.cityService.getAll().subscribe({
      next: (response: CityResponse[]) => this.cities = response,
      error: (err: any) => this.notificationService.showError(err)
    })
  }
}
