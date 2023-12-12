
import { Component, OnInit } from '@angular/core';
import { NavbarService } from './navbar.service';
import { BehaviorSubject, map } from 'rxjs';
import { ResponseProjectGetAll } from './models/ResponseProjectGetAll';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit {


  projectList$ = this.service.projectList$;

  constructor(private service: NavbarService) {}

  ngOnInit(): void {
    this.service.getAllProjectForNavbar();
    console.log('projectList',this.projectList$)
  }


}
