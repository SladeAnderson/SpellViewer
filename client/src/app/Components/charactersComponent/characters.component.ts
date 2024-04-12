import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatListModule} from '@angular/material/list';
import {MatButtonModule} from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { character } from 'src/app/Models/character.model';

@Component({
    selector: 'characters',
    templateUrl: 'characters.component.html',
    styleUrl:'characters.component.scss',
    standalone: true,
    imports: [
       CommonModule,
       MatListModule,
       MatButtonModule,
       MatIconModule
    ]
})

export class charactersComponent implements OnInit {
    constructor() { }

    ngOnInit() { }



    public characters: character[] = [
        {
            name: 'Eldric the Elven Enchanter',
            race: 'Elf',
            characterClass: 'Enchanter',
            knownSpells: [
                {
                    name:"asdgf"
                },
                {
                    name: 'Whispering Winds',
                },
                {
                    name: 'Arcane Lock',
                },
            ],
        },
        {
            name: 'Kara Stoneheart, Dwarven Warrior',
            race: 'Dwarf',
            characterClass: 'Warrior',
            knownSpells: [
                {
                    name: 'Stone Skin',
                },
                {
                    name: 'Earthquake Strike',
                },
                {
                    name: 'Forgefire Rage',
                },
            ],
        },
        {
            name: 'Lyra Nightshade, Half-Elven Rogue',
            race: 'Half-Elf',
            characterClass: 'Rogue',
            knownSpells: [
                {
                    name: 'Shadowstep',
                },
                {
                    name: 'Silent Blade',
                },
                {
                    name: 'Invisibility Cloak',
                },
            ],
        },
    ];
    
    
    
}

