import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import {MatTabsModule} from '@angular/material/tabs';
import {ThemePalette} from '@angular/material/core';
import {MatButtonModule} from '@angular/material/button';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatTooltipModule} from '@angular/material/tooltip';
import {MatIconModule} from '@angular/material/icon';
import { RouterModule, Routes } from '@angular/router';

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
        MatIconModule
    ]
})
export class navBar implements OnInit {
    constructor() {}

    @Input() links: NavItem[] = [];

    public activeLink: NavItem = this.links[0];



    ngOnInit() { 
    }


}

interface NavItem {
    name: string;
    path: string;
}