import { Component, Inject, OnInit } from '@angular/core';
import {
    MatDialog,
    MAT_DIALOG_DATA,
    MatDialogRef,
    MatDialogTitle,
    MatDialogContent,
    MatDialogActions,
    MatDialogClose,
    MatDialogModule
  } from '@angular/material/dialog';
import {MatButtonModule} from '@angular/material/button';
import {FormsModule} from '@angular/forms';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import { RegisterReq } from 'src/app/Models/requests/register.model';

@Component({
    selector: 'register-modal',
    templateUrl: 'register.modal.component.html',
    styleUrl: 'register.modal.component.scss',
    standalone:true,
    imports:[
        MatDialogModule,
        MatButtonModule,
        FormsModule,
        MatInputModule
    ]
})
export class registerModalComponent implements OnInit {
    constructor(
        public dialogRef: MatDialogRef<registerModalComponent>,
        @Inject(MAT_DIALOG_DATA) public data: RegisterReq,
    ) {}

    onNoClick(): void {
        this.dialogRef.close();
    }

    submit():void {

    }

    ngOnInit() { }
}