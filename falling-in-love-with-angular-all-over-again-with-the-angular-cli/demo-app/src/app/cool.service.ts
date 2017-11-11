import { Injectable } from '@angular/core';

@Injectable()
export class CoolService {

  constructor() { }

  getLanguages(): string[] {
    return [
      'Ruby',
      'TypeScript',
      'C#',
      'Haskell',
      'Lisp',
      'Coq'
    ];
  }

}
