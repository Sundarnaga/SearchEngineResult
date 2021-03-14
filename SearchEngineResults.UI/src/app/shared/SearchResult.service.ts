import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Observable, throwError } from 'rxjs';
import { SearchParameter } from './Model';

@Injectable({
  providedIn: 'root'
})
export class SearchResultService {

  readonly rootURL = 'http://localhost:63330/searchEngine';
  constructor(private http: HttpClient) { }

  GetEngines() {
    return this.http.get(this.rootURL + '/Engines');
  }

  GetResults(searchParamters: SearchParameter[]){
    return this.http.post<SearchParameter[]>(this.rootURL + '/SearchResults',{ SearchParameter:searchParamters});
  }

}
