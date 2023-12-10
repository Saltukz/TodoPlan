import { Component } from '@angular/core';
import { ProviderService } from '../provider/provider.service';

@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrl: './project.component.css'
})
export class ProjectComponent {

  constructor(private service : ProviderService){}

}
