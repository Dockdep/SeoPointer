import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

import { Project } from '../models/project';
import { Competitor } from '../models/competitor';
@Injectable()
export class CompetitorsService {
    private headers = new Headers({ 'Content-Type': 'application/json' });
    private url = 'http://localhost:5000/competitors'; 

    constructor(private http: Http) { }

    getAll(): Observable<Competitor[]> {
        return this.http.get(this.url, { headers: this.headers })
            .map(response => response.json() as Competitor[])
            .catch(this.handleError);
    }


    getOne(id: number): Observable<Competitor> {
        let url = `${this.url}/details/${id}`;
        
        return this.http.get(url)
            .map(response => response.json() as Competitor)
            .catch(this.handleError);
    }

    getParser(id: number): Observable<Competitor> {
        let url = `${this.url}/parser/${id}`;

        return this.http.get(url)
            .map(response => response.json() as Competitor)
            .catch(this.handleError);
    }

    delete(id: number): Observable<void> {

        let url = `${this.url}/delete/${id}`;
        return this.http.post(url, { headers: this.headers })
            .catch(this.handleError);
    }

    create(data: Competitor): Observable<Competitor> {
        let url = `${this.url}/create`;
        return this.http
            .post(url, JSON.stringify(data), { headers: this.headers })  
            .map(res => res.json() as Competitor)
            .catch(this.handleError);
    }

    edit(data: Competitor): Observable<Competitor> {
        let url = `${this.url}/edit/${data.Id}`;
        return this.http
            .post(url, JSON.stringify(data), { headers: this.headers }) 
            .catch(this.handleError);
    }



    private handleError(error: Response | any) {
        // In a real world app, you might use a remote logging infrastructure
        let errMsg: string;
        if (error instanceof Response) {
            const body = error.json() || '';
            const err =  JSON.stringify(body);
            errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
        } else {
            errMsg = error.message ? error.message : error.toString();
        }
        console.error(errMsg);
        return Observable.throw(errMsg);
    }


}