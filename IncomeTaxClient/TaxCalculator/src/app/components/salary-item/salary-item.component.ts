import { Component, Input } from '@angular/core';
import { Salary } from 'src/app/models/salary.model';

@Component({
  selector: 'app-salary-item',
  templateUrl: './salary-item.component.html',
  styleUrls: ['./salary-item.component.scss']
})
export class SalaryItemComponent {
  @Input()
  salaryInfo? : Salary
}
