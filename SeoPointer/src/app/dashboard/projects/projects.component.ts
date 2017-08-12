import { Component, OnInit } from '@angular/core';
import { Project } from '../../models/Project';
import { ProjectsService } from '../../services/projects.service';

declare var $: any;
@Component({
    selector: 'projects-cmp',
    templateUrl: 'projects.component.html',
    providers: [ProjectsService]
})

export class ProjectsComponent implements OnInit {
    projects: Project[];
    showDialog: boolean;
    objDelete: Project;
    constructor(private projectsService: ProjectsService ) {
    }

    ngOnInit() {
        this.bindGrid();
    }
    bindGrid() {

        this.projectsService.getAll().subscribe(projects => this.projects = projects)
    }

    confirmDelete() {
        this.showDialog = false; /// Close dialog
        this.delete(this.objDelete);
    }

    delete(project) {
        this.projectsService.delete(project.Id).subscribe(
            () => {
                this.handleSuccess();
                this.bindGrid();
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
            message: "Data was successfully deleted"

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
