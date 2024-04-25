import { Component, Input, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import {MatTabsModule} from '@angular/material/tabs';
import {ThemePalette} from '@angular/material/core';
import {MatButtonModule} from '@angular/material/button';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatTooltipModule} from '@angular/material/tooltip';
import {MatIconModule} from '@angular/material/icon';
import { RouterModule, Routes } from '@angular/router';
import { registerComponent } from '../RegisterComponent/register.component';
import { AuthService } from 'src/app/Services/authorize.service';
import { BehaviorSubject, Subscription } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
    selector: 'navBar',
    templateUrl: 'navBar.component.html',
    styleUrl:'navBar.component.scss',
    standalone:true,
    encapsulation: ViewEncapsulation.None,
    imports:[
        MatTabsModule,
        MatButtonModule,
        RouterModule,
        MatToolbarModule,
        MatTooltipModule,
        MatIconModule,
        registerComponent,
        CommonModule
    ]
})
export class navBar implements OnInit, OnDestroy {
    constructor(public authService:AuthService) {}
    ngOnDestroy(): void {
        this.subs.unsubscribe()
    }
    public subs = new Subscription();

    @Input() links: NavItem[] = [];

    public activeLink: NavItem = this.links[0];

    public isLoggedIn:BehaviorSubject<boolean> = new BehaviorSubject(false);

    logout(){
        this.authService.logout()
    };

    navCheck(result:any) {
        console.log(`NavCheck result: ${result}`);

        if (result) {
            this.isLoggedIn.next(true);
        } else {
            this.isLoggedIn.next(false);
        }
    }

    async ngOnInit() { 
        await this.isLoggedInCheck();
        this.authService.getLoggedInValue().subscribe(this.navCheck)
    };


    private async isLoggedInCheck(){
        let isLoggedIn = await this.authService.isLoggedIn();
        this.subs.add(isLoggedIn.subscribe(this.navCheck));
    }

}

interface NavItem {
    name: string;
    path: string;
}