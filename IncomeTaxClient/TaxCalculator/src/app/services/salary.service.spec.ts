import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { SalaryService } from './salary.service';
import { HttpClient } from '@angular/common/http';

describe('SalaryService', () => {
  let service: SalaryService;
  let httpClient: HttpClient;
  let httpTestingController: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [SalaryService]
    });
    service = TestBed.inject(SalaryService);
    httpClient = TestBed.inject(HttpClient);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTestingController.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should return salary info for a given income', () => {
    const income = 50000;
    const salaryInfoData = { 
      grossAnnualSalary: 40000, 
      grossMonthlySalary: 3333.33, 
      netAnnualSalary: 29000, 
      netMonthlySalary: 2416.67, 
      annualTaxPaid: 11000, 
      monthlyTaxPaid: 916.67 
    };

    service.getSalaryInfo(income).subscribe(salaryInfo => {
      expect(salaryInfo).toEqual(salaryInfoData);
    });

    const req = httpTestingController.expectOne(`${service.apiUrl}/salary-info/${income}`);
    expect(req.request.method).toBe('GET');
    req.flush(salaryInfoData);
  });
});
