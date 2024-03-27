import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Salary } from '../models/salary.model';

@Injectable({
  providedIn: 'root'
})
export class SalaryService {

  private apiUrl = 'https://localhost:7041/api/TaxBand';

  constructor(private http: HttpClient) { }

  getSalaryInfo(income: number): Observable<Salary> {
    return this.http.get<Salary>(this.apiUrl + '/salary-info/' + income);
  }
}