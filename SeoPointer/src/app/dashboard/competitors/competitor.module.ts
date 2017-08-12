import { NgModule }       from '@angular/core';
import { CommonModule }   from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { CompetitorsComponent }        from './competitors.component';
import { CompetitorDetailComponent } from './competitor-detail.component';
import { CompetitorComponent } from './competitor.component';
import { ModalPopupModule } from './../../shared/modal-popup/modalpopup.module';
import { CompetitorRoutingModule }       from './competitor-routing.module';
import { Select2Module } from 'ng2-select2/ng2-select2';

@NgModule({
  imports: [
Select2Module,
    CommonModule,
      CompetitorRoutingModule,
      ReactiveFormsModule,
      ModalPopupModule
  ],
  declarations: [
      CompetitorsComponent,
      CompetitorDetailComponent,
      CompetitorComponent
  ]
})
export class CompetitorModule {}
