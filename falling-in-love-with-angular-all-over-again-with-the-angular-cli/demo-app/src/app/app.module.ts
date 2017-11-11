import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CoolListComponent } from './cool-list/cool-list.component';
import { CoolService } from './cool.service';

@NgModule({
  declarations: [
    AppComponent,
    CoolListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [CoolService],
  bootstrap: [AppComponent]
})
export class AppModule { }
