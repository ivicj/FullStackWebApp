import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MakelaarListComponent } from './makelaar-list.component';

describe('MakelaarListComponent', () => {
  let component: MakelaarListComponent;
  let fixture: ComponentFixture<MakelaarListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MakelaarListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MakelaarListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
