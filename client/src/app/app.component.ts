import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { TokenInterceptor } from './Tools/token.interceptor';
import { HttpClientModule } from '@angular/common/http';
import { MatTabsModule } from '@angular/material/tabs';
import { navBar } from "./Components/navBar/navBar.component";
import { MainSpells } from './Components/MainSpellsComponent/MainSpells.component';
import { dndInfoService } from './Services/dndInfo.service';
import { JsonTools } from './Tools/JsonTools';
import { AuthService } from './Services/authorize.service';

@Component({
    selector: 'app-root',
    standalone: true,
    templateUrl: './app.component.html',
    styleUrl: './app.component.scss',
    imports: [RouterOutlet, MatTabsModule, navBar, HttpClientModule],
    providers: [dndInfoService,JsonTools,AuthService,
        {
            provide: HTTP_INTERCEPTORS,
            useClass: TokenInterceptor,
            multi: true
        }
    ],
    

    
})
export class AppComponent implements OnInit {
  constructor(private router: Router){}
  ngOnInit(): void {
  }
  newlinks = 
  [
    {name:"Spells",path:""},
    {name:"Characters",path:"characters"}
  ];
  

}
