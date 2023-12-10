import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { NavbarComponent } from './navbar/navbar.component';
import { AppRoutingModule } from './app-routing.module';
import { ProviderComponent } from './provider/provider.component';
import { ProjectComponent } from './project/project.component';
import { ProviderService } from './provider/provider.service';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NavbarService } from './navbar/navbar.service';
import { ScheduleComponent } from './schedule/schedule.component';
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { SchedulerModule } from "@progress/kendo-angular-scheduler";
import { IntlModule } from "@progress/kendo-angular-intl";
import { ScheduleService } from './schedule/shedule.service';
@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavbarComponent,
    ProviderComponent,
    ProjectComponent,
    ProjectComponent,
    ScheduleComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    SchedulerModule,
    IntlModule,
    ReactiveFormsModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [ProviderService,NavbarService,ScheduleService],
  bootstrap: [AppComponent]
})
export class AppModule { }
