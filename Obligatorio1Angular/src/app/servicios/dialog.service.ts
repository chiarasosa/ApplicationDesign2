import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { AlertDialogComponent } from '../componentes/alert-dialog/alert-dialog.component';

@Injectable({
  providedIn: 'root',
})
export class DialogService {
  constructor(private dialog: MatDialog) {}

  openAlertDialog(title: string, message: string) {
    this.dialog.open(AlertDialogComponent, {
      data: {
        title: title,
        message: message,
      },
    });
  }
}
