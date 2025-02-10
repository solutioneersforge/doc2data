import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReceiptProcessDashboardComponent } from './receipt-process-dashboard.component';

describe('ReceiptProcessDashboardComponent', () => {
  let component: ReceiptProcessDashboardComponent;
  let fixture: ComponentFixture<ReceiptProcessDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReceiptProcessDashboardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ReceiptProcessDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
