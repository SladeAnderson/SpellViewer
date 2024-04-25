import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from 'src/app/Services/authorize.service';
import { FormBuilder } from '@angular/forms';
import { Subscription } from 'rxjs';

@Component({
    selector: 'selector-name',
    templateUrl: 'loginModal.component.html',
    styleUrls:['loginModal.component.scss'],
    standalone:true,
    imports:[
        CommonModule
    ]
})
export class loginModal implements OnDestroy {
    constructor(private authService:AuthService, private formBuilder:FormBuilder){}
    ngOnDestroy(): void {
        this.subs.unsubscribe();
    }
    public subs = new Subscription();

    
    

}