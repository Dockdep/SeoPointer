import { NgModule }       from '@angular/core';
import { CommonModule }   from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { ProjectsComponent }        from './projects.component';
import { ProjectDetailComponent }  from './project-detail.component';
import { ProjectComponent }  from './project.component';
import { ProjectRoutingModule }       from './project-routing.module';
import { ModalPopupModule } from './../../shared/modal-popup/modalpopup.module';

@NgModule({
  imports: [
    CommonModule,
      ProjectRoutingModule,
      ReactiveFormsModule,
      ModalPopupModule
  ],
  declarations: [
    ProjectsComponent,
    ProjectDetailComponent,
    ProjectComponent
    
  ]
})
export class ProjectModule {}
