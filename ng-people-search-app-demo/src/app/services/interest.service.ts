import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Interest } from '../models/interest';

@Injectable({
  providedIn: 'root'
})
export class InterestService {
  private REST_API_URL = 'https://localhost:44356/api/interests';

  constructor(private httpClient: HttpClient) { }

  public getInterests(): Observable<Interest[]> {
    return this.httpClient.get<Interest[]>(this.REST_API_URL);
  }
}
