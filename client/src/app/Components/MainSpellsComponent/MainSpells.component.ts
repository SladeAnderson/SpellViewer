import { Component, OnInit, AfterViewInit, ViewChild } from '@angular/core';
import {MatPaginator, MatPaginatorModule} from '@angular/material/paginator';
import {MatTableDataSource, MatTableModule} from '@angular/material/table';
import { dndInfoService } from 'src/app/Services/dndInfo.service';
import {MatExpansionModule} from '@angular/material/expansion';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule, MatFormFieldControl} from '@angular/material/form-field';
import {MatButtonModule} from '@angular/material/button'
import { CommonModule } from '@angular/common';

@Component({
    selector: 'Main-Spells',
    templateUrl: 'MainSpells.component.html',
    styleUrl:'MainSpells.component.scss',
    standalone:true,
    imports:[
        MatTableModule,
        MatPaginatorModule,
        MatExpansionModule,
        CommonModule,
        MatInputModule,
        MatFormFieldModule,
        MatButtonModule
    ]
})
export class MainSpells implements AfterViewInit {
    constructor(private infoService:dndInfoService) { 
        
    }
    //Get Data from dnd info service

    //Table Setup
    displayedColumns: string[] = ['details'];
    dataSource = new MatTableDataSource<spell>(spells);
    
    //Expansion Panel
    public expandedElement?: spell | null;


    //Paginator
    @ViewChild(MatPaginator) paginator!: MatPaginator;

    ngAfterViewInit() {
        this.dataSource.paginator = this.paginator;
        this.dataSource.filterPredicate = (data,filter)=>true
    }


    hasMaterial(spell: spell,){
        let toDisplay: string = '';
        if (spell.components.is_verbal === true) {
            toDisplay += 'V';
        }
        if (spell.components.is_somatic === true) {
            toDisplay += ',S';
        }
        if (spell.components.is_material === true) {
            toDisplay += `,M (${spell.components.materials_needed})`;
        }
        
        return toDisplay;
    }


}


const spells: spell[] = [
    {
        Casting_Time: "1 action",
        Classes: ["Wizard", "Sorcerer"],
        components: {
            is_material: true,
            materials_needed: ["A pinch of bat guano and sulfur"],
            is_somatic: true,
            is_verbal: true
        },
        description: "This spell creates a small flame in your hand.",
        duration: "Instantaneous",
        level: "Cantrip",
        name: "Fire Bolt",
        range: "120 feet",
        is_Ritual: false,
        school: "Evocation",
        type: "Attack"
    },
    {
        Casting_Time: "1 action",
        Classes: ["Cleric", "Paladin"],
        components: {
            is_material: false,
            is_somatic: true,
            is_verbal: true
        },
        description: "You touch a creature and restore 1d8 hit points.",
        duration: "Instantaneous",
        level: "1st",
        name: "Cure Wounds",
        range: "Touch",
        is_Ritual: false,
        school: "Evocation",
        type: "Healing"
    },
    {
        Casting_Time: "1 minute",
        Classes: ["Bard", "Druid"],
        components: {
            is_material: true,
            materials_needed: ["A sprig of mistletoe"],
            is_somatic: true,
            is_verbal: true
        },
        description: "You create a floating, spectral weapon within range.",
        duration: "1 minute",
        level: "2nd",
        name: "Spiritual Weapon",
        range: "60 feet",
        is_Ritual: false,
        school: "Evocation",
        type: "Attack"
    }
];

export interface spell {
    Casting_Time:string,
    Classes:string[],
    components: components,
    description: string,
    higher_level?: string,
    duration: string,
    level:string,
    name:string,
    range:string,
    is_Ritual:boolean,
    school:string,
    type:string
}

interface components {
    is_material:boolean,
    materials_needed?: string[],
    is_somatic:boolean,
    is_verbal: boolean
}
