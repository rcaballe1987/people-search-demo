import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';

import {MatTableDataSource} from '@angular/material/table';

import {
  debounceTime,
  map,
  distinctUntilChanged,
  filter
} from 'rxjs/operators';
import { fromEvent, of } from 'rxjs';

import { PeopleService } from '../services/people.service';
import { InterestService } from '../services/interest.service';
import { Person } from '../models/person';
import { Interest } from '../models/interest';

@Component({
  selector: 'app-people-component',
  templateUrl: './people-component.component.html',
  styleUrls: ['./people-component.component.scss']
})
export class PeopleComponentComponent implements OnInit {
  public people: Person[] = [];
  public originalPeopleArray: Person[] = [];
  public interests: Interest[] = [];
  public displayedColumns: string[] = ['firstName', 'lastName', 'address', 'age', 'interests', 'image', 'save'];
  public dataSource;
  public isSearching: boolean;
  @ViewChild('peopleSearchInput', { static: true }) peopleSearchInput: ElementRef;

  constructor(private peopleService: PeopleService, private interestService: InterestService) {
    this.isSearching = false;
  }

  ngOnInit(): void {
    this.interestService.getInterests().subscribe(data => {
      if (data) {
        this.interests = data;
      }
    });

    this.getPeopleList();

    fromEvent(this.peopleSearchInput.nativeElement, 'keyup').pipe(

      // get value
      map((event: any) => {
        this.isSearching = true;
        return event.target.value;
      })

      // Time in milliseconds between key events
      , debounceTime(1000)

      // If previous query is diffent from current
      , distinctUntilChanged()

      // subscription for response
    ).subscribe((text: string) => {

      this.searchPeople(text).subscribe((res) => {
        console.log('res', res);
        this.isSearching = false;
        this.people = res;
        this.renderTable();
      }, (err) => {
        this.isSearching = false;
        console.log('error', err);
      });

    });
  }

  public searchPeople(term: string) {
    if (this.originalPeopleArray.length) {
      this.people = this.originalPeopleArray;
    }

    this.originalPeopleArray = this.people;

    if (term.length) {
      this.people = this.people
        .filter(person => person.firstName.toLowerCase().includes(term.toLowerCase())
                        || person.lastName.toLowerCase().includes(term.toLowerCase()));
    }

    return of(this.people);
  }

  public addNewPersonRow(): void {
    const person: Person = { firstName: '', lastName: '', address: '', age: null, imageLink: '', personInterestIds: [] };
    this.people.push(person);
    this.renderTable();
  }

  public savePerson(rowData): void {
    this.peopleService.addPerson(rowData).subscribe(person => {
      if (person) {
        this.getPeopleList();
      }
    });
  }

  public deletePerson(rowData): void {
    if (!rowData.id) {
      const index = this.people.indexOf(rowData);
      this.people.splice(index, 1);
      this.renderTable();
      return;
    }

    this.peopleService.deletePerson(rowData).subscribe(_ => {
      this.getPeopleList();
    });
  }

  public isInputDisabled(rowData): boolean {
    return rowData.id;
  }

  public isSaveBtnDisabled(rowData): boolean {
    return rowData.firstName.length === 0 || rowData.lastName.length === 0 || !rowData.age || rowData.age < 0;
  }

  private getPeopleList() {
    this.peopleService.getPeople().subscribe(data => {
      if (data) {
        this.people = data;
        this.originalPeopleArray = [];
        this.renderTable();
      }
    });
  }

  private renderTable() {
    this.dataSource = new MatTableDataSource(this.people);
  }

}
