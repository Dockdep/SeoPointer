import { NgModule }             from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CompetitorsComponent }        from './competitors.component';
import { CompetitorDetailComponent }  from './competitor-detail.component';
import { CompetitorComponent }  from './competitor.component';


const competitorRoutes: Routes = [
  {
    path: '',
    component: CompetitorComponent,
    children: [
      {
        path: '',
        children: [
          { 
            path: ':id', 
            component: CompetitorDetailComponent 
          },
          {
            path: 'add',
            component: CompetitorDetailComponent
          },
          { 
            path: '', 
            component: CompetitorsComponent 
          },
        ]
      }
    ]
  }
];

@NgModule({
  imports: [
      RouterModule.forChild(competitorRoutes)
  ],
  exports: [
    RouterModule
  ]
})
export class CompetitorRoutingModule {}
