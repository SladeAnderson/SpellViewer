import { RouterModule, Routes } from '@angular/router';
import { MainSpells } from './Components/MainSpellsComponent/MainSpells.component';
import { NgModel } from '@angular/forms';
import { charactersComponent } from './Components/charactersComponent/characters.component';

export const routes: Routes = [
    {
        path:'',
        component: MainSpells,
    },
    {
        path:'characters',
        component:charactersComponent
    },
   /*
    More Routes Here!


    ---------------- ↑ */
    {
        path:'**',
        component: MainSpells,
    },
];

