import { NavbarService } from './../navbar/navbar.service';
import { Component } from '@angular/core';
import { ProviderService } from './provider.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ProjectModel } from '../project/project.model';

@Component({
  selector: 'app-provider',
  templateUrl: './provider.component.html',
  styleUrl: './provider.component.css'
})
export class ProviderComponent {
  myForm: FormGroup;


  constructor(private service : ProviderService,private navbarService : NavbarService,private fb: FormBuilder)
  {
    this.myForm = this.fb.group({
      url: ['', Validators.compose([Validators.required])],
      name:['', Validators.compose([Validators.required])]
    });
  }




  onSubmit(){
    this.service.getDatas(this.myForm.get('url').value).subscribe( (data: ProjectModel) => {
      console.log('ben data',data)
      data.ProjectName = this.myForm.get('name').value;
      data.ProviderUrl = this.myForm.get('url').value;
      // Veriyi başarılı bir şekilde aldık
      // Şimdi bu veriyi kullanarak başka işlemler yapabilir veya başka bir servis fonksiyonunu çağırabiliriz
      this.service.saveDatas(data);
      this.navbarService.getAllProject();
    },
    (error) => {
      // Hata durumunda buraya düşer
      console.error('Veri alınamadı:', error);
    });

  }
}
