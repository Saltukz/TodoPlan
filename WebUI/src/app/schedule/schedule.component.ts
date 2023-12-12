import { Component, OnInit } from '@angular/core';

import { ScheduleService, } from './shedule.service';
import { SchedulerEvent } from '@progress/kendo-angular-scheduler';
import { ActivatedRoute } from '@angular/router';
@Component({
  selector: 'app-schedule',
  template: `
  <kendo-scheduler
    [events]="events"
    [selectedDate]="selectedDate"
    [showWorkHours]="true"
  >
    <kendo-scheduler-day-view> </kendo-scheduler-day-view>
    <kendo-scheduler-week-view> </kendo-scheduler-week-view>
    <kendo-scheduler-month-view> </kendo-scheduler-month-view>
    <kendo-scheduler-timeline-view> </kendo-scheduler-timeline-view>
  </kendo-scheduler>
`,
  styleUrl: './schedule.component.css'
})
export class ScheduleComponent implements OnInit{

  id:number;

  baseData: any[] = [];
 currentYear = new Date().getFullYear();
 parseAdjust = (eventDate: string): Date => {
  const date = new Date(eventDate);
  date.setFullYear(this.currentYear);
  return date;
};

 randomInt = (min, max): number => {
  return Math.floor(Math.random() * (max - min + 1)) + min;
}

    displayDate = new Date(this.currentYear, 5, 24);

  sampleData =  this.baseData.map(dataItem => (
  <SchedulerEvent> {
      id: dataItem.taskID,
      start: this.parseAdjust(dataItem.start),
      startTimezone: dataItem.startTimezone,
      end: this.parseAdjust(dataItem.end),
      endTimezone: dataItem.endTimezone,
      isAllDay: dataItem.IsAllDay,
      title: dataItem.title,
      description: dataItem.Description,
      recurrenceRule: dataItem.RecurrenceRule,
      recurrenceId: dataItem.RecurrenceID,
      recurrenceException: dataItem.RecurrenceException,

      roomId: dataItem.RoomID,
      ownerID: dataItem.OwnerID
  }
));

  sampleDataWithResources = this.baseData.map(dataItem => (
  <SchedulerEvent> {
      id: dataItem.taskID,
      start: this.parseAdjust(dataItem.start),
      startTimezone: dataItem.startTimezone,
      end: this.parseAdjust(dataItem.end),
      endTimezone: dataItem.endTimezone,
      isAllDay: dataItem.IsAllDay,
      title: dataItem.title,
      description: dataItem.Description,
      recurrenceRule: dataItem.RecurrenceRule,
      recurrenceId: dataItem.RecurrenceID,
      recurrenceException: dataItem.RecurrenceException,
      roomId: this.randomInt(1, 2),
      attendees: [this.randomInt(1, 3)]
  }
));

 sampleDataWithCustomSchema = this.baseData.map(dataItem => (
  {
      ...dataItem,
      Start: this.parseAdjust(dataItem.Start),
      End: this.parseAdjust(dataItem.End)
  }
));

   public selectedDate: Date = this.displayDate;
   public events: SchedulerEvent[] = [];



  constructor(private service : ScheduleService,  private _route: ActivatedRoute){}
  ngOnInit(): void {
    this._route.params.subscribe((params) => {
      if (params['id'] !== undefined && params['id'] !== null && params['id'] != 0 && params['id'] != undefined) {
        this.id = params['id'];
      }
    });

    this.GetAll(this.id);

  }


  async GetAll(id?: any) {
    await this.service.getSchduleByDeveloper(id).subscribe((x) => {
      this.baseData = x.dataList.map((item) => {
        const startDate = new Date(item.start);
        const endDate = new Date(item.end);
        const formattedStartDate = startDate.toISOString();
        const formattedEndDate = endDate.toISOString();
        return { ...item, start: formattedStartDate, end: formattedEndDate };
      });

      // Update the events array after fetching and mapping the data
      this.events = this.baseData.map(dataItem => (
        <SchedulerEvent> {
            id: dataItem.taskID,
            start: this.parseAdjust(dataItem.start),
            startTimezone: dataItem.startTimezone,
            end: this.parseAdjust(dataItem.end),
            endTimezone: dataItem.endTimezone,
            isAllDay: dataItem.IsAllDay,
            title: dataItem.title,
            description: dataItem.Description,
            recurrenceRule: dataItem.RecurrenceRule,
            recurrenceId: dataItem.RecurrenceID,
            recurrenceException: dataItem.RecurrenceException,

            roomId: dataItem.RoomID,
            ownerID: dataItem.OwnerID
        }
      ));

      console.log('baseData', this.baseData);
    });
  }

}






