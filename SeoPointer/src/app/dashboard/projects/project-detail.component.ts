import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import 'rxjs/add/operator/switchMap';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Location } from '@angular/common';
import { Project } from '../../models/Project';
import { ProjectsService } from '../../services/projects.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import {
    Router, Resolve, RouterStateSnapshot,
    ActivatedRouteSnapshot
} from '@angular/router';
declare var $: any;
@Component({
    selector: 'project-detail-cmp',
    templateUrl: 'project-detail.component.html',
    providers: [ProjectsService]
})

export class ProjectDetailComponent implements OnInit{
    project: Project;
    projectForm: FormGroup;
    submitted = false;
    actionName = "Create";

    constructor(
        private projectsService: ProjectsService,
        private route: ActivatedRoute,
        private location: Location,
        private fb: FormBuilder
    ) { }

    onSubmit() {
        this.project = this.projectForm.value;
        this.save();
    }

    ngOnInit(): void {
        this.route.params.switchMap((params: Params) => {
            if (params['id'] == 'add') {
                return Observable.of(new Project());
                } else {
                    return this.projectsService.getOne(+params['id'])
                }
            })
            .subscribe(project =>
            {
                this.project = project;
                this.buildForm();
            });
     
    }


    buildForm(): void {
        this.projectForm = this.fb.group({
            'Id': [this.project.Id],
            'Name': [this.project.Name, [
                Validators.required,
                Validators.minLength(4),
                Validators.maxLength(24)
                ]
            ],
            'Url': [this.project.Url, [
                Validators.required,
                Validators.minLength(4),
                Validators.maxLength(24)
            ]],
            'Contacts': [this.project.Contacts, [
                Validators.minLength(4),
                Validators.maxLength(24)
            ]],
            'Info': [this.project.Info]
        });

        this.projectForm.valueChanges
            .subscribe(data => this.onValueChanged(data));

        this.onValueChanged(); // (re)set validation messages now
    }


    onValueChanged(data?: any) {
        if (!this.projectForm) { return; }
        const form = this.projectForm;

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
        'Name': '',
        'Url': '',
        'Contact': ''
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
        'Contact': {
            'minlength': 'Name must be at least 4 characters long.',
            'maxlength': 'Name cannot be more than 24 characters long.',
        }
    };


    save(): void {
        if (this.project.Id == undefined) {
            this.projectsService.create(this.project)
                .subscribe(() => {
                    this.handleSuccess();
                    this.goBack();
                },
                (error) => {
                    this.handleError(error);
                });
        } else {
            this.projectsService.edit(this.project)
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
