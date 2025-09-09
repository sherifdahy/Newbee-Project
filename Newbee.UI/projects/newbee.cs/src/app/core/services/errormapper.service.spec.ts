/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ErrormapperService } from './errormapper.service';

describe('Service: Errormapper', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ErrormapperService]
    });
  });

  it('should ...', inject([ErrormapperService], (service: ErrormapperService) => {
    expect(service).toBeTruthy();
  }));
});
