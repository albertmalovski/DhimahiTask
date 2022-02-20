Your task is to create a REST/JSON service using the C# programming language and .NET Framework/Core platform. The service should be hosted in IIS/Kestrel process and expose the following endpoints:

 

/countries/populate

The purpose of this endpoint is to call a remote service and populate the database tables from the response. Steps for implementation:
The WSDL definition of the remote service is located at: http://webservices.oorsprong.org/websamples.countryinfo/CountryInfoService.wso?wsdl
Perform a call to the "FullCountryInfoAllCountries" remote function
Save the values of the specified response fields to an SQL database of your choice: ISOCode, Name, CapitalCity, ContinentCode, CountryFlag and Languages.
Subsequent/parallel calls to this enpoint should only update the data and not create new records
 

/countries/{isoCode}/flag

The purpose of this endpoint is to return file contents to the client. Steps for implementation:
Search for a record from the database by the given "isoCode" criteria
Read the "CountryFlag" value
Download the image data defined in the "CountryFlag" URL and save it to a file on the disk
Return response to the client in the following format:
 

            {

                  "fileName": -> the name of the file

                  "fileBase64": -> the base64 encoded string of the saved file contents

                  "sha256": -> the sha256 hash of the file saved to the disk

            }

 

If the file has already been downloaded, return the file contents directly from the disk
If multiple requests with same input data are issued in parallel, only a single request should be allowed to write the file to the disk
 

/continents/{continentCode}/languages

The purpose of this endpoint is to execute a database query and transform the results before sending them to the client. Steps for implementation:
Retrieve the records from the database that match the "continentCode" search criteria
Transform the results in the following format, without duplicates:
 

            {

                  "languages": [...]

            }

 

Save the response to a cache system of your choice and use it for subsequent calls
 

Bonus points:

Solution layout and architecture
Design patterns
Error handling
Logging