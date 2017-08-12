import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import 'rxjs/add/operator/switchMap';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Location } from '@angular/common';
import { Competitor } from '../../models/competitor';
import { CompetitorsService } from '../../services/competitors.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import {Router, Resolve, RouterStateSnapshot,ActivatedRouteSnapshot} from '@angular/router';
import { ProjectsService } from '../../services/projects.service';
import { Select2OptionData } from 'ng2-select2/ng2-select2';

declare var $: any;
@Component({
    selector: 'competitor-detail-cmp',
    templateUrl: 'competitor-detail.component.html',
    providers: [CompetitorsService, ProjectsService]
})

export class CompetitorDetailComponent implements OnInit{
    model: Competitor;
    modelForm: FormGroup;
    submitted = false;
    actionName = "Create";
    projects: Select2OptionData[];
    constructor(
        private dataService: CompetitorsService,
        private projectsService: ProjectsService,
        private route: ActivatedRoute,
        private location: Location,
        private fb: FormBuilder
    ) { }

    onSubmit() {
        this.model = this.modelForm.value;
        this.save();
    }

    ngOnInit(): void {
        this.getProjects();
        this.route.params.switchMap((params: Params) => {
            if (params['id'] == 'add') {
                return Observable.of(new Competitor());
                } else {
                return this.dataService.getOne(+params['id'])
                }
            })
            .subscribe(project =>
            {
                this.model = project;
                this.buildForm();
            });
     
    }

    getProjects() {
        this.projectsService.getAll().subscribe(projects => this.projects = projects.map(project => {
            return {
                id: project.Id.toString(),
                text: project.Name,

            }
        }));
    }
    buildForm(): void {
        this.modelForm = this.fb.group({
            'Id': [this.model.Id],
            'Name': [this.model.Name, [
                Validators.required,
                Validators.minLength(4),
                Validators.maxLength(24)
                ]
            ],
            'Url': [this.model.Url, [
                Validators.required,
                Validators.minLength(4),
                Validators.maxLength(24)
            ]],
            'ProjectId': [this.model.ProjectId, [
                Validators.required
            ]],
        });

        this.modelForm.valueChanges
            .subscribe(data => this.onValueChanged(data));

        this.onValueChanged(); // (re)set validation messages now
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

    public changed(e: any): void {

        this.modelForm.patchValue({ ProjectId: +e.value }); 
    }
    
    formErrors = {
        'Name': '',
        'Url': '',
        'ProjectId':'',
    };

    validationMessages = {
        'Name': {
            'required': 'Name is required.',
            'minlength': 'Name must be at least 4 characters long.',
            'maxlength': 'Name cannot be more than 24 characters long.',
        },
        'Url': {
            'required': 'Name is required.',
            'minlength': 'Name must be at least 4 characters long.',
            'maxlength': 'Name cannot be more than 24 characters long.',
        },
        'ProjectId': {
            'required': 'Name is required.',
        }
    };


    save(): void {
        if (this.model.Id == undefined) {
            this.dataService.create(this.model)
                .subscribe(() => {
                    this.handleSuccess();
                    this.goBack();
                },
                (error) => {
                    this.handleError(error);
                });
        } else {
   
            this.dataService.edit(this.model)
                .subscribe(
                    () => {
                        this.handleSuccess();
                        this.goBack();
                    },
                    (error) => {
                        this.handleError(error);
                    }
                );
        }

    }

    goBack(): void {
        this.location.back();
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
}
