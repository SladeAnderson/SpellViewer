import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JsonTools } from '../Tools/JsonTools';
import { spell } from '../Models/spells.model';
import { BehaviorSubject, Observable, map, tap  } from 'rxjs';



@Injectable({providedIn: 'root'})
export class dndInfoService {
    constructor(private http:HttpClient, private jsonTools:JsonTools) {}

    private runOnce: boolean = true;



    public addMassSpells(){
        let spellsJson:spell[] = require("../data/spells.json");

        if (this.runOnce != null) {
            spellsJson.forEach(spell => {
                this.createSpell(spell.Casting_Time,
                                spell.Classes,
                                spell.components.is_material,
                                spell.components.is_somatic,
                                spell.components.is_verbal,
                                spell.description,
                                spell.duration,
                                spell.level,
                                spell.name,
                                spell.range,
                                spell.is_Ritual,
                                spell.school,
                                spell.type,
                                spell.components.materials_needed)
            });
            this.runOnce = false;
        }
    };
  

    public createSpell(
        Casting_Time: string,
        Classes:string[],
        isMaterial: boolean,
        is_somatic:boolean,
        is_verbal: boolean,
        description: string,
        duration: string,
        level:string,
        name:string,
        range:string,
        is_Ritual:boolean,
        school:string,
        type?:string,
        materials_needed?: string[],
    ){
        let components = {
            isMaterial,
            materials_needed,
            is_somatic,
            is_verbal
        }

        return this.http.post(`api/MasterSpells/Create`, {
            Casting_Time,
            Classes,
            components,
            description,
            duration,
            level,
            name,
            range,
            is_Ritual,
            school,
            type
        })

    };
    
}