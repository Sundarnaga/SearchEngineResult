
export interface SearchResults {
  results: SearchResult[];
}

export interface SearchResult {
  searchEngine: string;
  keyWord: string;
  results: number[];
  result: string;
}

export interface SearchParameter {
  keywords: string[];
  searchEngine: string;
  urlToMatch: string;
}

export interface SearchEngineListResponse {
  SearchEngines: string[];
}

export interface SearchResultRequest {
  SearchParameter: SearchParameter[];
}

export interface SearchEngineListResponse {
  SearchEngines: string[];
}


