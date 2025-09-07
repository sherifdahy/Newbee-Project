/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { LocalStorgeService } from './local-storge.service';

describe('Service: LocalStorge', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LocalStorgeService]
    });
  });

  it('should ...', inject([LocalStorgeService], (service: LocalStorgeService) => {
    expect(service).toBeTruthy();
  }));
});
