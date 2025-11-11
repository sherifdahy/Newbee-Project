import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(private matSnackBar: MatSnackBar) { }

  showSuccess(message:string)
  {
    this.matSnackBar.open(message,'exit',{
      duration:3000,
      horizontalPosition : 'start',
      verticalPosition : 'bottom',
      panelClass: ['snackbar-success']
    })
  }

  showError(errors : Record<string,string[]>)
  {
    Object.keys(errors).forEach(key=>{
      this.matSnackBar.open(errors[key].join('\n'),key,{
        duration : 3000,
        horizontalPosition : 'start',
        verticalPosition : 'bottom',
        panelClass: ['snackbar-error']
      })
    })
}
}
