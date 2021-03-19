import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Observable, of } from 'rxjs';
import { EmailApiService } from '../email-api.service';
import Message from '../message';

import { InboxComponent } from './inbox.component';

describe('InboxComponent', () => {
  let component: InboxComponent;
  let fixture: ComponentFixture<InboxComponent>;
  let data: Message[] = [];

  const apiServiceSpy = jasmine.createSpyObj('EmailApiService', ['getMessages']);

  beforeEach(async () => {
    data = [];
    await TestBed.configureTestingModule({
      declarations: [InboxComponent],
      providers: [{ provide: EmailApiService, useValue: apiServiceSpy }],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InboxComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    fixture.detectChanges();
    expect(component).toBeTruthy();
  });

  it('should get empty messages list', () => {
    apiServiceSpy.getMessages.and.returnValue(of(data));
    fixture.detectChanges();
    expect(component.messages).toEqual(data);
  });

  it('should display a message\'s subject on the page', () => {
    data = [{
      id: 1,
      from: '',
      subject: 'the-subject',
      date: '',
      body: ''
    }];
    apiServiceSpy.getMessages.and.returnValue(of(data));
    fixture.detectChanges();
    const elements: HTMLTableDataCellElement[] = Array.from(
      fixture.nativeElement.querySelectorAll('td')
    );
    const results = elements.filter(e =>
      e.textContent?.includes(data[0].subject)
    );
    expect(results.length).toBe(1);
  });
});
