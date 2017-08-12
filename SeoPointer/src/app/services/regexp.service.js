"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var RegexpService = (function () {
    function RegexpService() {
    }
    RegexpService.prototype.getRegexpList = function () {
        return [
            {
                id: '[^\,\d]+',
                text: 'цена с запятой',
            },
            {
                id: '[^\.\d]+',
                text: 'цена с точкой',
            },
        ];
    };
    return RegexpService;
}());
RegexpService = __decorate([
    core_1.Injectable()
], RegexpService);
exports.RegexpService = RegexpService;
//# sourceMappingURL=regexp.service.js.map