
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable,lastValueFrom,map,of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import * as xml2js from 'xml2js';
import { environment } from '../../environment/environment';

import { ResponseModel } from '../shared/models/response.model';
import { ResponseProjectGetAll } from './models/ResponseProjectGetAll';

@Injectable({
  providedIn: 'root'
})
export class NavbarService {

  private readonly _projectList = new BehaviorSubject<ResponseProjectGetAll[]>(null);
  readonly projectList$ = this._projectList.asObservable();

  constructor(private http: HttpClient) { }



  getProject<ResponseProjectGet>(id: number): Promise<ResponseProjectGet> {
    return lastValueFrom(
      this.http.get<ResponseModel<ResponseProjectGet>>(`${environment.apiUrl}/Project/Get/${id}`).pipe(
        map((response: ResponseModel<ResponseProjectGet>) => {
          if (response.IsSuccess) {
            return response.data;
          }
        })
      )
    );
  }


  getAllProject(): Observable<ResponseModel<ResponseProjectGetAll>> {
    return  this.http.get<ResponseModel<ResponseProjectGetAll>>(`${environment.apiUrl}/Project/GetAll`)
  }


  async getAllProjectForNavbar() {
    await lastValueFrom(
      this.getAllProject().pipe(
        map(response => {
          console.log('response',response.dataList)
            this._projectList.next(response.dataList);
            console.log('projectListService',this.projectList$)
        })
      )
    );
  }




}



