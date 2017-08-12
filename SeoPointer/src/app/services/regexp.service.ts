import { Injectable } from "@angular/core";
import { Select2OptionData } from 'ng2-select2/ng2-select2';
@Injectable()
export class RegexpService {
    getRegexpList(): Select2OptionData[] {
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
    }
}