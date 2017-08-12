"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var http_1 = require("@angular/http");
var Observable_1 = require("rxjs/Observable");
require("rxjs/add/operator/catch");
require("rxjs/add/operator/map");
var CompetitorsService = (function () {
    function CompetitorsService(http) {
        this.http = http;
        this.headers = new http_1.Headers({ 'Content-Type': 'application/json' });
        this.url = 'http://localhost:5000/competitors';
    }
    CompetitorsService.prototype.getAll = function () {
        return this.http.get(this.url, { headers: this.headers })
            .map(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    CompetitorsService.prototype.getOne = function (id) {
        var url = this.url + "/details/" + id;
        return this.http.get(url)
            .map(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    CompetitorsService.prototype.getParser = function (id) {
        var url = this.url + "/parser/" + id;
        return this.http.get(url)
            .map(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    CompetitorsService.prototype.delete = function (id) {
        var url = this.url + "/delete/" + id;
        return this.http.post(url, { headers: this.headers })
            .catch(this.handleError);
    };
    CompetitorsService.prototype.create = function (data) {
        var url = this.url + "/create";
        return this.http
            .post(url, JSON.stringify(data), { headers: this.headers })
            .map(function (res) { return res.json(); })
            .catch(this.handleError);
    };
    CompetitorsService.prototype.edit = function (data) {
        var url = this.url + "/edit/" + data.Id;
        return this.http
            .post(url, JSON.stringify(data), { headers: this.headers })
            .catch(this.handleError);
    };
    CompetitorsService.prototype.handleError = function (error) {
        // In a real world app, you might use a remote logging infrastructure
        var errMsg;
        if (error instanceof Response) {
            var body = error.json() || '';
            var err = JSON.stringify(body);
            errMsg = error.status + " - " + (error.statusText || '') + " " + err;
        }
        else {
            errMsg = error.message ? error.message : error.toString();
        }
        console.error(errMsg);
        return Observable_1.Observable.throw(errMsg);
    };
    return CompetitorsService;
}());
CompetitorsService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [http_1.Http])
], CompetitorsService);
exports.CompetitorsService = CompetitorsService;
//# sourceMappingURL=competitors.service.js.map