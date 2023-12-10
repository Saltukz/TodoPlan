import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ProviderComponent } from './provider/provider.component';
import { ProjectComponent } from './project/project.component';


const routes:Routes = [
  {path:'home',component:HomeComponent},
  {path:'provider',component:ProviderComponent},
  {path:'projects',component:ProjectComponent},
  { path: '',   redirectTo: 'home', pathMatch: 'full' },
  // { path: '**', component: PageNotFoundComponent },
]

@NgModule({
  exports:[RouterModule],
  imports: [
    RouterModule.forRoot(routes)
  ]
})
export class AppRoutingModule { }
