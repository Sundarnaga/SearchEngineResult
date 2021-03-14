Pre-Requiste to run API code:
1. Open the SearchEngineResults.sln in Visual Studio 2019. It requires .Net Core 3.1 Version
2. Buildthe application
3. Run the code.
4. The API will open the browser with port number http://localhost:63330/searchEngine/Engines


Pre-Requiste to run UI code:
1.Open SearchEngineResults.UI folder from visual studio code.
2. install npm packages from terminal by typing "npm install".
3.If the base url is "http://localhost:63330/searchEngine/" then  Open SearchResult.service.ts file from SearchEngineResults.UI\src\app\shared" folder and replace the variable "rootURL" value with the base API url.

Best Practices used in API:
1. Implemented .Net Core In-memory cache to avoid the multiple client request for the same request parameter.
2. Implmented httpclientfactory pattern to connect the search engine.
3. Implemented parallel tasks concepts to span across different threads to get the results from the search engine.
4. Async / Await has been implemented not to block the thread.
5. Request validation has been done using ValidationAttribute.
6. Logging mechanism has been implemeneted to capture the activities in the application and the exception
7. Generic exception handler mechanism has been implemented.
8. Unit test has been written and tested for all important functions.

Best Practices used in UI:
1. Bootstrap is used for UI design
2. UI functionalities are validated
2. ToastrService package is used for error message display
