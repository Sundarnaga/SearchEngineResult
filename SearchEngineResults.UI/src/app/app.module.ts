import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgbDateAdapter, NgbDateParserFormatter, NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { ToastrModule } from 'ngx-toastr';
import { AppComponent } from './app.component';
import { SearchEngineResultUserComp as SearchEngineResults } from './SearchEngineResults/SearchEngineResults.component';

import { SearchResultService } from "./shared/SearchResult.service"

@NgModule({
  declarations: [
    AppComponent,
    SearchEngineResults

  ],
  imports: [
    BrowserModule,
    FormsModule,
    NgbModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot()
  ],
  providers: [SearchResultService],
  bootstrap: [AppComponent]
})
export class AppModule { }
