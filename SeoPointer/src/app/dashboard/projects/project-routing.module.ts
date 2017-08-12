import { NgModule }             from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ProjectsComponent }        from './projects.component';
import { ProjectDetailComponent }  from './project-detail.component';
import { ProjectComponent }  from './project.component';


const projectRoutes: Routes = [
  {
    path: '',
    component: ProjectComponent,
    children: [
      {
        path: '',
        children: [
          { 
            path: ':id', 
            component: ProjectDetailComponent 
          },
          {
            path: 'add',
            component: ProjectDetailComponent
          },
          { 
            path: '', 
            component: ProjectsComponent 
          },
        ]
      }
    ]
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(projectRoutes)
  ],
  exports: [
    RouterModule
  ]
})
export class ProjectRoutingModule {}
