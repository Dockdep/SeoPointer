import { Component, OnInit } from '@angular/core';
import { Competitor } from '../../models/competitor';
import { CompetitorsService } from '../../services/competitors.service';
declare var $: any;
@Component({
    selector: 'competitors-cmp',
    templateUrl: 'competitors.component.html',
    providers: [CompetitorsService]
})

export class CompetitorsComponent implements OnInit {
    models: Competitor[];
    showDialog: boolean;
    objDelete: Competitor;
    constructor(private competitorsService: CompetitorsService) {
    }

    ngOnInit() {
        this.bindGrid();
    }
    bindGrid() {

        this.competitorsService.getAll().subscribe(data => this.models = data)
    }

    confirmDelete() {
        this.showDialog = false; /// Close dialog
        this.delete(this.objDelete);
    }

    delete(project) {
        this.competitorsService.delete(project.Id).subscribe(
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
