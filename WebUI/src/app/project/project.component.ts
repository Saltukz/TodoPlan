import { Component, OnInit } from '@angular/core';
import { ProjectService } from './project.service';
import { ResponseProjectGet } from './project.model';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrl: './project.component.css'
})
export class ProjectComponent implements OnInit {

  lstModel : ResponseProjectGet ;
  id:number;
  constructor(private service : ProjectService,  private _route: ActivatedRoute,   private _router: Router,){

  }
  ngOnInit(): void {
    this._route.params.subscribe((params) => {
      if (params['id'] !== undefined && params['id'] !== null && params['id'] != 0 && params['id'] != undefined) {
        this.id = params['id'];
      }
    });
   this.GetAll(this.id);

  }

  async GetAll(id?: any) {
    await this.service.getAllProjectDevelopers(id).subscribe(x=>{
      this.lstModel = x.data
    })
  }

  async GetSchedule(id:number){
    this._router.navigate(['/schdule/'+id]);
  }



}
