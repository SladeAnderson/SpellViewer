import { Component, OnInit } from '@angular/core';
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
import { registerModalComponent } from './modal/register.modal.component';

@Component({
    selector: 'register',
    templateUrl: 'register.component.html',
    styleUrl:'register.component.scss',
    standalone:true,
    imports:[

    ]
})
export class registerComponent implements OnInit {
    userName: string = '';
    passWord: string = '';

    constructor(public dialog: MatDialog) {}

    openDialog(): void {
        const dialogRef = this.dialog.open(registerModalComponent, {
            width: '45vw',
            data: {userName: this.userName, passWord: this.passWord},
        });

        dialogRef.afterClosed().subscribe(result => {
            
        })

    }

    ngOnInit() { }
}