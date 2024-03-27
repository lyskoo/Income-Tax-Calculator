import { Component, Input } from '@angular/core';
import { SalaryService } from './services/salary.service';
import { Salary } from './models/salary.model';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'TaxCalculator';
  salaryInfo?: Salary;
  inputValue: number = 0;
  name: string = 'Gross Annual Salary'

  public constructor(private salaryService: SalaryService){
  }

  onButtonClick() {
    this.getSalaryInfo(this.inputValue)
  }

  getSalaryInfo(salary :number): void {
    this.salaryService.getSalaryInfo(salary).subscribe({
      next: (data) => {
        this.salaryInfo = data;
      },
      error: (error) => {
        this.salaryInfo = undefined;
      }
    });
  }
}
