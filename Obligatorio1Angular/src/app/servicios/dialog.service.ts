import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { AlertDialogComponent } from '../componentes/alert-dialog/alert-dialog.component';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class DialogService {
  private okClickedSubject = new Subject<void>();
  
  okClicked$ = this.okClickedSubject.asObservable();

  constructor(private dialog: MatDialog) {}

  openAlertDialog(title: string, message: string) {
    const dialogRef = this.dialog.open(AlertDialogComponent, {
      data: {
        title: title,
        message: message,
      },
    });

    dialogRef.afterClosed().subscribe(() => {
      // Cuando la modal se cierra, notifica que se hizo clic en OK
      this.okClickedSubject.next();
    });
  }

  notifyOkClicked() {
    // Notifica que se hizo clic en OK
    this.okClickedSubject.next();
  }
}
