import { Input, HostListener, AfterViewInit, Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
declare var $: any;

@Component({
    selector: 'iframe-cmp',
    templateUrl: 'iframe.component.html'
})

export class IframeComponent implements OnInit {
    @Input() modelForm: FormGroup;
    @Input() url: string;
    constructor(public sanitizer: DomSanitizer) { }


    ngOnInit(): void {

    }
}
