import { Injectable } from '@angular/core';
import { Developer, ProjectModel, Task } from '../project/project.model';
import { Observable,lastValueFrom,map,of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import * as xml2js from 'xml2js';
import { environment } from '../../environment/environment';
import { ResponseModel } from '../shared/models/response.model';
@Injectable({
  providedIn: 'root'
})
export class ProviderService {

  constructor(private http: HttpClient) { }

  getDatas(url: string): Observable<ProjectModel> {
    return this.http.get(url, { responseType: 'text' }).pipe(
      map(data => this.parseData(data))
    );
  }



  async saveDatas(request: ProjectModel): Promise<ResponseModel<ProjectModel>> {
    return await lastValueFrom(
      this.http.post<ResponseModel<ProjectModel>>(`${environment.apiUrl}/Provider/Save`, request)
        .pipe(
          map((response: ResponseModel<ProjectModel>) => {
            if (response.IsSuccess) {
              return response;
            }
          })
        )
    );
  }

  private parseData(data: string): ProjectModel {

    let jsonData: any;

     // Check if the data is XML or JSON
     if (data.startsWith('<')) {
      // If data starts with '<', it is likely XML
      xml2js.parseString(data, { explicitArray: false }, (error, result) => {
        if (error) {
          throw new Error('Error parsing XML data: ' + error);
        }
        jsonData = result;
      });

      // If the XML data is wrapped inside a 'project' element, extract it
      if (jsonData.project) {
        jsonData = jsonData.project;
      }
    } else {
      // Assume JSON if it doesn't start with '<'
      jsonData = JSON.parse(data);
    }

    // Now jsonData contains the parsed data (either JSON or XML)
    // Continue with the mapping logic based on the data structure

    const developers: Developer[] = this.mapDevelopers(jsonData);
    const tasks: Task[] = this.mapTasks(jsonData);

    return {
      ProjectName:'',
      ProviderUrl:'',
      Developers: developers,
      Tasks: tasks
    };
  }

  private mapDevelopers(data: any): Developer[] {
    console.log('data.developers.developer',data.developers.developer)
    if (data.developers) {
      if(data.developers.developer){
        return data.developers.developer.map(dev => ({
          Name: this.resolvePropertyName(dev, ['Name', 'name', 'dev_name']),
          Capacity: this.resolvePropertyName(dev, ['capacity_per_hour', 'capacity', 'hourly_capacity'])
        }));
      }
      return data.developers.map(dev => ({
        Name: this.resolvePropertyName(dev, ['Name', 'name', 'dev_name']),
        Capacity: this.resolvePropertyName(dev, ['capacity_per_hour', 'capacity', 'hourly_capacity'])
      }));
    }
    return [];
  }

  private mapTasks(data: any): Task[] {
    if (data.tasks) {
      if(data.tasks.task){
        return data.tasks.task.map(task => ({
          Name: this.resolvePropertyName(task, ['name', 'task_name', 'task_name']),
          Duration: this.resolvePropertyName(task, ['duration', 'task_duration', 'task_duration']),
          Difficulty: this.resolvePropertyName(task, ['difficulty', 'task_difficulty', 'task_difficulty']),
          AssignedDeveloper: this.resolvePropertyName(task, ['assigned_developer', 'developerId', 'task_assigned_to'])
        }));
      }
      return data.tasks.map(task => ({
        Name: this.resolvePropertyName(task, ['name', 'task_name', 'task_name']),
        Duration: this.resolvePropertyName(task, ['duration', 'task_duration', 'task_duration']),
        Difficulty: this.resolvePropertyName(task, ['difficulty', 'task_difficulty', 'task_difficulty']),
        AssignedDeveloper: this.resolvePropertyName(task, ['assigned_developer', 'developerId', 'task_assigned_to'])
      }));
    }
    return [];
  }

  private resolvePropertyName(obj: any, possibleKeys: string[]): any {
    for (const key of possibleKeys) {
      if (obj.hasOwnProperty(key)) {
        return obj[key];
      }
    }
    return null;
  }
}



