import { Component, OnInit } from '@angular/core';
import { SchedulerEvent } from '@progress/kendo-angular-scheduler';
import { ScheduleService } from './shedule.service';
import { sampleData, displayDate } from "./events.utc"
@Component({
  selector: 'app-schedule',
  template: `
  <kendo-scheduler
    [kendoSchedulerBinding]="events"
    [selectedDate]="selectedDate"
  >
    <ng-template kendoSchedulerMultiWeekDaySlotTemplate let-date="date">
      <strong>{{ date | kendoDate }}</strong>
    </ng-template>
    <kendo-scheduler-multi-week-view [numberOfWeeks]="5">
    </kendo-scheduler-multi-week-view>
  </kendo-scheduler>
`,
  styleUrl: './schedule.component.css'
})
export class ScheduleComponent implements OnInit{

  public selectedDate: Date = displayDate;
  public events: SchedulerEvent[] = sampleData;

  constructor(private service : ScheduleService){}
  ngOnInit(): void {

  }

}
