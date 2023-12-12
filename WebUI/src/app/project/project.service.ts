import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ResponseModel } from '../shared/models/response.model';
import { ResponseProjectGet } from './project.model';
import { environment } from '../../environment/environment';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  constructor(private http: HttpClient) { }

  getAllProjectDevelopers(id : number): Observable<ResponseModel<ResponseProjectGet>> {
    return  this.http.get<ResponseModel<ResponseProjectGet>>(`${environment.apiUrl}/Project/Get/`+ id)
  }
}
