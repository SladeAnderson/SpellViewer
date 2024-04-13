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
  import { MatButton } from '@angular/material/button';

@Component({
    selector: 'register-modal',
    templateUrl: 'register.modal.component.html',
    styleUrl: 'register.modal.component.scss',
    standalone:true,
    imports:[
        MatDialogModule
    ]
})
export class NameComponent implements OnInit {
    constructor() { }

    ngOnInit() { }
}