import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MatDialogModule, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { publicDecrypt } from 'crypto';
import { BehaviorSubject } from 'rxjs';
import { CommonModule } from '@angular/common';
import { UserResponse } from 'src/app/Models/responses/userResponse.model';
import { AuthService } from 'src/app/Services/authorize.service';

@Component({
    selector: 'selector-name',
    templateUrl: 'login.component.html',
    styleUrls:['login.component.scss'],
    standalone:true,
    imports:[
        MatDialogModule,
        CommonModule
    ]
})
export class LoginComponent implements OnInit {
    constructor(public dialogRef: MatDialogRef<LoginComponent>, @Inject(MAT_DIALOG_DATA) public data: UserResponse, public authService: AuthService ) { }

    public usernameValid:BehaviorSubject<boolean> = new BehaviorSubject(false);
    public passwordValid:BehaviorSubject<boolean> = new BehaviorSubject(false);

 

    ngOnInit() { 
        this.usernameValid = this.authService.getLoginStatus();
        this.passwordValid = this.authService.getRegisterStatus();
    }
}