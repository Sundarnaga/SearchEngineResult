import { Component, OnInit } from '@angular/core'
import { formArrayNameProvider } from '@angular/forms/src/directives/reactive_directives/form_group_name';
import { SearchResultService } from "../shared/SearchResult.service"
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { analyzeAndValidateNgModules } from '@angular/compiler';
import { SearchParameter, SearchResult } from '../shared/Model';


@Component({
  selector: 'search-engine-results',
  templateUrl: './SearchEngineResults.component.html',
  styles: []
})

export class SearchEngineResultUserComp implements OnInit {
  errorTitle: string = 'Search Engine Result';
  engines: string[] = [];
  selectedEngines: string[] = [];
  keywords: string = 'e-settlements';
  urlToMatch: string = "www.sympli.com"
  searchParameters: SearchParameter[] = [];
  searchResults: SearchResult[] = [];

  constructor(private service: SearchResultService, private toastr: ToastrService) {
    this.GetEngines();
  }

  onChange(engine: string, isChecked: boolean) {
    if (isChecked) {
      this.selectedEngines.push(engine);
    } else {
      let index = this.selectedEngines.findIndex(x => x == engine)
      this.selectedEngines.splice(index, 1);
    }
  }
  onClick() {
    var words: string[] = [];
    this.searchParameters = [];
    if (this.selectedEngines.length <= 0) {
      this.toastr.error("No search engine selected",this.errorTitle);
      return;
    }
    else if (this.urlToMatch == '') {
      this.toastr.error("URL to match is not available",this.errorTitle);
      return;
    }
    else if(this.keywords == ''){
      this.toastr.error("Keyword is not available",this.errorTitle);
      return;
    }
    else {
        this.selectedEngines.forEach(engine => {
          var parameter: SearchParameter =
          {
            keywords: this.keywords.split(','),
            searchEngine: engine,
            urlToMatch: this.urlToMatch
          }
          this.searchParameters.push(parameter);
        });
        this.searchResults = [];
      this.service.GetResults(this.searchParameters).subscribe(
          (res: any) => {
            this.searchResults = res;
            if(this.searchResults.length >0){
              this.searchResults.forEach(element => {
                element.result = element.results.join(", ");
              });
            }
          },
          err => {
              this.toastr.error("Server error occured", this.errorTitle);
            }
        )
    }
  }
  GetEngines() {
    this.service.GetEngines().subscribe((res: any) => {
      this.engines = res;
      this.selectedEngines = this.engines.map(x => x);
    });
  }

  ngOnInit() {
  }
}
