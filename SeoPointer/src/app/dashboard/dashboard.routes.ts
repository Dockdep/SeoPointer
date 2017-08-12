import { Route } from '@angular/router';
import { DashboardComponent } from './dashboard.component';
import { ParserComponent } from './parser/parser.component';
import { IframeComponent } from './parser/iframe.component';
export const MODULE_ROUTES: Route[] =[
    { path: 'parser', component: ParserComponent },
    {
        path: 'project',
        loadChildren: 'app/dashboard/projects/project.module#ProjectModule'
    },
    {
        path: 'competitor',
        loadChildren: 'app/dashboard/competitors/competitor.module#CompetitorModule'
    },
    { path: '', redirectTo: 'dashboard', pathMatch: 'full' }
]

export const MODULE_COMPONENTS = [
    ParserComponent,
    IframeComponent
]
