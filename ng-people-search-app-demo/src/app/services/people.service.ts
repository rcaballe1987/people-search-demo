import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Person } from '../models/person';

@Injectable({
  providedIn: 'root'
})
export class PeopleService {
  private REST_API_URL = 'https://localhost:44356/api/persons';

  constructor(private httpClient: HttpClient) { }

  public getPeople(): Observable<Person[]> {
    return this.httpClient.get<Person[]>(this.REST_API_URL);
  }

  public addPerson(person: Person): Observable<Person> {
    return this.httpClient.post<Person>(this.REST_API_URL, person);
  }

  public deletePerson(person: Person): Observable<boolean> {
    return this.httpClient.delete<boolean>(`${this.REST_API_URL}/${person.id}`);
  }
}
