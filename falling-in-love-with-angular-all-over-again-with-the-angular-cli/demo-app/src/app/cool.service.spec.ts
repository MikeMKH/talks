import { TestBed, inject } from '@angular/core/testing';

import { CoolService } from './cool.service';

describe('CoolService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CoolService]
    });
  });

  it('should be created', inject([CoolService], (service: CoolService) => {
    expect(service).toBeTruthy();
  }));
});
