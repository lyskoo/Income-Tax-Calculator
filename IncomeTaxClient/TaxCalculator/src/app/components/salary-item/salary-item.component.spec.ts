import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SalaryItemComponent } from './salary-item.component';
import { Salary } from 'src/app/models/salary.model';

describe('SalaryItemComponent', () => {
  let component: SalaryItemComponent;
  let fixture: ComponentFixture<SalaryItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SalaryItemComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SalaryItemComponent);
    component = fixture.componentInstance;
    
    fixture.detectChanges();
    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should have undefined salaryInfo by default', () => {
    expect(component.salaryInfo).toBeUndefined();
  });

  it('should set salaryInfo when input is provided', () => {
    component.salaryInfo = component.salaryInfo;
    fixture.detectChanges();
    expect(component.salaryInfo).toEqual(component.salaryInfo);
  });

  it('should render salaryInfo properties in the template when salaryInfo is defined', () => {
    const salaryInfo = { 
      grossAnnualSalary: 40000, 
      grossMonthlySalary: 3333.33, 
      netAnnualSalary: 29000, 
      netMonthlySalary: 2416.67, 
      annualTaxPaid: 11000, 
      monthlyTaxPaid: 916.67 
    };

    component.salaryInfo = salaryInfo;
    console.log('salaryInfo:', component.salaryInfo);
    fixture.detectChanges();
    const compiled = fixture.nativeElement;
    const infoDiv = compiled.querySelector('.info');
    expect(infoDiv).not.toBeNull();

    if (infoDiv) {
        expect(infoDiv.textContent).toContain('Gross Annual Salary: 40000');
        expect(infoDiv.textContent).toContain('Gross Monthly Salary: 3333.33');
        expect(infoDiv.textContent).toContain('Net Annual Salary: 29000');
        expect(infoDiv.textContent).toContain('Net Monthly Salary: 2416.67');
        expect(infoDiv.textContent).toContain('Annual Tax Paid: 11000');
        expect(infoDiv.textContent).toContain('Monthly Tax Paid: 916.67');
    }
  });

  it('should not render salaryInfo properties in the template when salaryInfo is undefined', () => {
    component.salaryInfo = undefined;
    fixture.detectChanges();
    const compiled = fixture.nativeElement;
    const infoDiv = compiled.querySelector('.info');
    expect(infoDiv).toBeNull();
  });
});
