import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ButtonComponent } from './button.component';

describe('ButtonComponent', () => {
  let component: ButtonComponent;
  let fixture: ComponentFixture<ButtonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ButtonComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should render the button with default text', () => {
    const compiled = fixture.nativeElement;
    expect(compiled.querySelector('button').textContent).toContain('Click me');
  });

  it('should emit buttonClick event when clicked', () => {
    let buttonClicked = false;
    component.buttonClick.subscribe(() => {
      buttonClicked = true;
    });
    const buttonElement = fixture.nativeElement.querySelector('button');
    buttonElement.click();
    expect(buttonClicked).toBeTruthy();
  });

  it('should render the button with provided text', () => {
    component.buttonText = 'Custom Text';
    fixture.detectChanges();
    const compiled = fixture.nativeElement;
    expect(compiled.querySelector('button').textContent).toContain('Custom Text');
  });
});
