import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { Component, OnInit, AfterViewInit, ViewChild } from '@angular/core';
import {MatPaginator, MatPaginatorModule} from '@angular/material/paginator';
import {MatTableDataSource, MatTableModule} from '@angular/material/table';
import { dndInfoService } from 'src/app/Services/dndInfo.service';
import { JsonTools } from 'src/app/Tools/JsonTools';
import { TokenInterceptor } from 'src/app/Tools/token.interceptor';

@Component({
    selector: 'Main-Spells',
    templateUrl: 'MainSpells.component.html',
    styleUrl:'MainSpells.component.scss',
    standalone:true,
    imports:[
        MatTableModule,
        MatPaginatorModule
    ]
})
export class MainSpells implements AfterViewInit {
    constructor(private infoService:dndInfoService) { 
        
    }
    //Get Data from dnd info service

    

    //Table Setup
    displayedColums: string[] = [];
    dataSource = new MatTableDataSource();

    @ViewChild(MatPaginator) paginator!: MatPaginator;

    ngAfterViewInit() {
        this.dataSource.paginator = this.paginator
    }
}