import { TestBed } from '@angular/core/testing';

import { PeopleService } from './people.service';

describe('People.ServiceService', () => {
  let service: PeopleService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PeopleService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
