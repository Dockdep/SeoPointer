import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ModalPopupComponent } from './modalpopup.component';

@NgModule({
    imports: [ CommonModule],
    declarations: [ModalPopupComponent],
    bootstrap: [ModalPopupComponent],
    exports: [ModalPopupComponent]
})
export class ModalPopupModule { }
