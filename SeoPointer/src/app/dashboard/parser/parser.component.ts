import { HostListener, AfterViewInit, Component, OnInit, trigger, state, style, transition, animate } from '@angular/core';
import { Select2Module } from 'ng2-select2/ng2-select2';
import { RegexpService } from '../../services/regexp.service';
import { CompetitorsService } from '../../services/competitors.service';
import { DomSanitizer } from '@angular/platform-browser';
import 'rxjs/add/operator/switchMap';
import { Competitor } from '../../models/competitor';
import { CompetitorField } from '../../models/competitor-field';
import { Select2OptionData } from 'ng2-select2/ng2-select2';
import { FormGroup, FormBuilder, FormControl,  Validators } from '@angular/forms';
declare var $:any;

@Component({
    selector: 'parser-cmp',
    templateUrl: 'parser.component.html',
    providers: [RegexpService, CompetitorsService]
})

export class ParserComponent implements OnInit{
    name: string = "Tom";
    model: Competitor;
    modelForm: FormGroup;
    competitors: Select2OptionData[];
    url: any ;
    private _selectedFields: Array<string> = [];
    constructor(
        private regexpService: RegexpService,
        private competitorsService: CompetitorsService,
        private fb: FormBuilder,
        public sanitizer: DomSanitizer
    ) { }
  
    @HostListener('window:message', ['$event'])
    onMessage(e) {
        var data = e.data;
        var origin = e.origin;

        /**
         * Проверка октуда пришел запрос
         */

        if (origin !== "http://localhost:3000") {
            console.log('Запрос пришел с другого домена origin =' + origin);
            return;
        }
        if (this.modelForm && this.modelForm.value.ActiveField != null) {
            switch (this.modelForm.value.ActiveField){
                case 'Price':
                    this.modelForm.patchValue({ PriceXPath: data.XPath, Price: data.value }); 
                    break;
                case 'EventPrice':
                    this.modelForm.patchValue({ EventPriceXPath: data.XPath, EventPrice: data.value }); 
                    break;
                case 'Title':
                    this.modelForm.patchValue({ TitleXPath: data.XPath, Title: data.value }); 
                    break;
            }

        }
       

    };


    ngOnInit(): void {
        this.getCompetitors();
        this.model = new Competitor();

    }



    public ActiveFieldChange(xpath: string): void {
        this.sendIframeAvtiveField(xpath);
    }


    public changed(e: any): void {

        this.competitorsService.getParser(+e.value).subscribe(parserData => {
        
            this.model = parserData;
            let competitorFields: CompetitorField[] = parserData.CompetitorField;
            let competitorField: any;
            this.modelForm = this.fb.group({
                'CompetitorId': [this.model.Id],
                'TestUrl': [this.model.TestUrl, [
                    Validators.required,
                    Validators.minLength(2),
                    Validators.maxLength(250)
                ]],
                'ActiveField': null,
                'Price': '',
                'PriceId': [competitorFields[0].Id],
                'PriceXPath': [competitorFields[0].XPath], 
                'PriceRegexp': [ competitorFields[0].Regexp],
                'EventPrice': [''],
                'EventPriceId': [competitorFields[1].Id],
                'EventPriceXPath': [competitorFields[1].XPath],
                'EventPriceRegexp': [competitorFields[1].Regexp],
                'Title': [''],
                'TitleId': [competitorFields[2].Id],
                'TitleXPath': [competitorFields[2].XPath],
            });
       
            this.modelForm.valueChanges
                .subscribe(data => this.onValueChanged(data));

            this.onValueChanged(); // (re)set validation messages now
            this.url = this.sanitizer.bypassSecurityTrustResourceUrl('../assets/sturt_up/iframe/test1.html')
            setTimeout(this.sendIframeConfig, 1000, this.modelForm);
        });


    }



    onValueChanged(data?: any) {
        if (!this.modelForm) { return; }
        const form = this.modelForm;

        for (const field in this.formErrors) {
            // clear previous error message (if any)
            this.formErrors[field] = '';
            const control = form.get(field);

            if (control && control.dirty && !control.valid) {
                const messages = this.validationMessages[field];
                for (const key in control.errors) {
                    this.formErrors[field] += messages[key] + ' ';
                }
            }
        }
    }

    formErrors = {
        'TestUrl': ''
    };

    validationMessages = {
        'TestUrl': {
            'required': 'Test Url is required.',
            'minlength': 'Name must be at least 4 characters long.',
            'maxlength': 'Name cannot be more than 24 characters long.',
        }
    };


    getCompetitors() {
        this.competitorsService.getAll().subscribe(competitors => this.competitors = competitors.map(competitor => {
            return {
                id: competitor.Id.toString(),
                text: competitor.Name,

            }
        }));
    }


    onSubmit() {
        this.model.TestUrl = this.modelForm.value.TestUrl;
        this.model.CompetitorField[0].XPath = this.modelForm.value.PriceXPath;
        this.model.CompetitorField[1].XPath = this.modelForm.value.EventPriceXPath;
        this.model.CompetitorField[2].XPath = this.modelForm.value.TitleXPath;
        this.model.CompetitorField[0].Regexp = this.modelForm.value.PriceRegexp;
        this.model.CompetitorField[1].Regexp = this.modelForm.value.EventPriceRegexp;
        this.save();

    }


    save(): void {

        this.competitorsService.edit(this.model)
            .subscribe(
                () => {
                    this.handleSuccess();
      
                },
                (error) => {
                    this.handleError(error);
                }
            );


    }

    private handleError(error: any): Promise<any> {

        $.notify({
            icon: "notifications",
            message: "An error occurred", error

        }, {
                type: 'danger',
                timer: 1000,
                placement: {
                    from: 'top',
                    align: 'right'
                }
            });
        return Promise.reject(error.message || error);
    }


    private handleSuccess(): any {

        $.notify({
            icon: "notifications",
            message: "Data was successfully saved"

        }, {
                type: 'success',
                timer: 1000,
                placement: {
                    from: 'top',
                    align: 'right'
                }
            });

    }

    sendIframeAvtiveField(config) {
        var sendObject = {
            title: 'Active Input',
            value: config
        };
   
        var iframe = document.getElementsByTagName('iframe')[0];
        iframe.contentWindow.postMessage(sendObject, '*');
    }
    sendIframeConfig(modelForm) {
        let config = {
            'Fields': [
                { 'Name': 'Price', 'XPath': modelForm.value.PriceXPath },
                { 'Name': 'EventPrice', 'XPath': modelForm.value.EventPriceXPath },
                { 'Name': 'Title', 'XPath': modelForm.value.TitleXPath }
            ]
        };
        var sendObject = {
            title: 'Config Iframe',
            value: config
        };

        var iframe = document.getElementsByTagName('iframe')[0];
        iframe.contentWindow.postMessage(sendObject, '*');
    }
}
