/* tslint:disable:no-unused-variable */

import { TestBed, inject } from '@angular/core/testing';
import { GenericService } from './generic.service';

describe('Service: Api', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [GenericService],
    });
  });

  it('should ...', inject([GenericService], (service: GenericService) => {
    expect(service).toBeTruthy();
  }));
});
