import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import 'rxjs/add/observable/of';

@Injectable()
export class CoolService {

  constructor() { }

  getLanguages(): Observable<string[]> {
    return Observable.of([
      'TypeScript',
      'C#',
      'Idris',
      'Haskell',
      'Lisp'
    ]);
  }

}
