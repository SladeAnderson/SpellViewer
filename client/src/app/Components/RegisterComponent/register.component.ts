import { Component, Inject, Input, OnInit } from '@angular/core';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import {MatTooltipModule} from '@angular/material/tooltip';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { registerModal } from './registerModal/registerModal.component';

@Component({
    selector: 'register',
    templateUrl: 'register.component.html',
    styleUrl:'register.component.scss',
    standalone:true,
    imports:[
        MatDialogModule,
        MatIconModule,
        MatTooltipModule,
        MatButtonModule,
        registerModal,
        CommonModule

    ]
})
export class registerComponent implements OnInit {
    @Input() isLoggedIn: boolean;

    constructor(public dialog: MatDialog) {
    }


    openDialog() {
        const dialog = this.dialog.open(registerModal, {
            width: '30vw',
            height: '30vh'
        })

        dialog.afterClosed().subscribe(val=>{
            
        })
    }

    ngOnInit() {
       console.log(`is logged in? : ${this.isLoggedIn}`);
       
    }
}