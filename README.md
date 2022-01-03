# eBroker

Follow these steps to run the Application 


Clone repo in local machine by SSH or Https.

Open and Build solution in VS. Solution file is in folder eBroker/ebroker.

Note: This project is using SQL Server Local.
Run the Migration commans:
	1. Add-Migration InitialCreate
	2. Update-Database

Swagger is added to test endpoints. Run the application and open in browser.

To Check test cases execution:

Run All Tests in Test Explorer.

or run below commands in cmd at unit test project path to generate test case coverage files .

dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput=.\TestResults\Coverage\EBroker.coverage.xml

Note: Make sure reportgenerator is install.

reportgenerator -reports:"TestResults\Coverage\EBroker.coverage.xml" -targetdir:"coveragereport" -reporttypes:Html


Assumptions & Key Points :-

1. Bare minimum seed data is used for testing.
2. This application is not fully optimized as it was creadted to testing only.
3. All the best practices are not followed.
4. This application ins not production ready.




