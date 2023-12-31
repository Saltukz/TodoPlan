import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ResponseModel } from '../shared/models/response.model';
import { environment } from '../../environment/environment';
import { ResponseScheduleGet } from './schedule.model';


@Injectable({
  providedIn: 'root'
})
export class ScheduleService {



  constructor(private http: HttpClient) { }

  getSchduleByDeveloper(id : number): Observable<ResponseModel<ResponseScheduleGet>> {
    return  this.http.get<ResponseModel<ResponseScheduleGet>>(`${environment.apiUrl}/Schedule/Get/`+ id)
  }



}

