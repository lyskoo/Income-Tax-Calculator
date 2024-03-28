import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { AppComponent } from './app.component';
import { SalaryService } from './services/salary.service';
import { MatFormFieldModule } from '@angular/material/form-field';
import { of, throwError } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { ButtonComponent } from './components/button/button.component';
import { SalaryItemComponent } from './components/salary-item/salary-item.component';

describe('AppComponent', () => {
  let component: AppComponent;
  let fixture: ComponentFixture<AppComponent>;
  let salaryService: jasmine.SpyObj<SalaryService>;
  
  beforeEach(async () => {
    const salaryServiceSpy = jasmine.createSpyObj('SalaryService', ['getSalaryInfo']);

    await TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        MatFormFieldModule,
        FormsModule
      ],
      declarations: [ AppComponent, ButtonComponent,
        SalaryItemComponent],
      providers: [
        { provide: SalaryService, useValue: salaryServiceSpy }
      ]
    }).compileComponents();

    salaryService = TestBed.inject(SalaryService) as jasmine.SpyObj<SalaryService>;
    fixture = TestBed.createComponent(AppComponent);
    component = fixture.componentInstance;
  });

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });

  it(`should have as title 'TaxCalculator'`, () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app.title).toEqual('TaxCalculator');
  });

  it('should initialize salaryInfo as undefined', () => {
    expect(component.salaryInfo).toBeUndefined();
  });

  it('should initialize inputValue as 0', () => {
    expect(component.inputValue).toBe(0);
  });

  it('should initialize name as "Gross Annual Salary"', () => {
    expect(component.name).toBe('Gross Annual Salary');
  });

  it('should call getSalaryInfo method on button click', () => {
    component.inputValue = 50000;
    const spy = spyOn(component, 'getSalaryInfo').and.callThrough();
    const button = fixture.nativeElement.querySelector('button');
    button.click();
    expect(spy).toHaveBeenCalled();
  });

  it('should call getSalaryInfo method of SalaryService when onButtonClick is called', () => {
    const inputValue = 40000;
    component.inputValue = inputValue;
    salaryService.getSalaryInfo.and.returnValue(of({ 
      grossAnnualSalary: 40000, 
      grossMonthlySalary: 3333.33, 
      netAnnualSalary: 29000, 
      netMonthlySalary: 2416.67, 
      annualTaxPaid: 11000, 
      monthlyTaxPaid: 916.67  }));

    component.onButtonClick();

    expect(salaryService.getSalaryInfo).toHaveBeenCalledWith(inputValue);
    expect(component.salaryInfo).toEqual({ 
      grossAnnualSalary: 40000, 
      grossMonthlySalary: 3333.33, 
      netAnnualSalary: 29000, 
      netMonthlySalary: 2416.67, 
      annualTaxPaid: 11000, 
      monthlyTaxPaid: 916.67  });
  });

  it('should set salaryInfo to undefined on error', () => {
    salaryService.getSalaryInfo.and.returnValue(throwError('error'));
    component.getSalaryInfo(50000);
    expect(component.salaryInfo).toBeUndefined();
  });
});
