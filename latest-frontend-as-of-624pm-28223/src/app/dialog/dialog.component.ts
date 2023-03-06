import { Component, Inject } from '@angular/core';
  import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
  import { taxiObj } from '../Model/model';

@Component({
  selector: 'app-dialog',
  templateUrl: './dialog.component.html',
  styleUrls: ['./dialog.component.css']
})
export class DialogComponent {
    constructor(
      public dialogRef: MatDialogRef<DialogComponent>,
      @Inject(MAT_DIALOG_DATA) public data: { taxiobj: taxiObj}
    ) {}
  
    panelOpenState = false; 
    onCancel(): void {
      this.dialogRef.close();
    }
  meterFare:number = Math.ceil(Number(this.data.taxiobj.meter))
  CDGFare:number = Math.ceil(Number(this.data.taxiobj.appFare))
    username:any = localStorage.getItem('username');
  
    
  }
  

