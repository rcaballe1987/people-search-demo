import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PeopleComponentComponent } from './people-component.component';

describe('PeopleComponentComponent', () => {
  let component: PeopleComponentComponent;
  let fixture: ComponentFixture<PeopleComponentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PeopleComponentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PeopleComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
