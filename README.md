# Mintec - Urner Barry Wrapper API

This is a sample Web API acting as a wrapper around Urner Barry API. If exposes one endpoint, which enables returning data from Urner Barry's API `/api/myitems/data` with currency conversion.

## Running locally
- .NET 7 is required
- Update `appSettings.json` with the path of the JSON file holding exchange rates (sorry forgot to include this in the solution)
- Add the following user secret
```json
"UrnerBarry": {
    "ApiKey": "your-api-key"
  }
```
- Run the application using the `dotnet` CLI or via your favorite IDE

## TODOs and future enhacements
- Discuss the logic for currency conversion (currently falling back to the latest rate, as JSON only had data till May 2023)
- API should support `USD` currency code and return same quote values as raw API :)
- Error handling
  - Use global exception filter or middleware
  - Return `ProblemDetails` model on errors
  - Document error handling in swagger
- Use another logging provider such as NLog or Serilog
- Integration tests
- More unit tests, especially on currency conversion and maybe JSON parsing
- Return currency in response
- Add request parameters to specify date ranges for returned quotes
