import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyTrainingsPageComponent } from './my-trainings-page.component';

describe('MyTrainingsPageComponent', () => {
  let component: MyTrainingsPageComponent;
  let fixture: ComponentFixture<MyTrainingsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MyTrainingsPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(MyTrainingsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
