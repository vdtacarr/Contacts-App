# GuideBook App
Technologies : MassTransit(RabbitMQ), MongoDB, Microservice Architecture (Contact Service, Report Service, Process Service), Swagger, Xunit(Unit Testing)</br>
Specifications : This api allows to add contact , add contact info, delete contact, delete contact info and export report according to location which gives the counts f people in the same town.
#How to launch
After downloading, navigate to solution (/Setur-Assessment), then run these three commands in console,
- dotnet run --project ./ContactService/ContactService.csproj
- dotnet run --project ./ReportService/ReportService.csproj,
- dotnet run --project ./ProcessService/ProcessService.csproj

After that launch the browser and go to https://localhost:5001/swagger/index.html (Contact Service)  and https://localhost:5005/swagger/index.html (Report Service)
Process Service retrieve data from rabbitmq to not create bottleneck in the report service.

After go to swagger UI, you will see the endpoints, 
- /Contact/add-contact -> adds a new contact,
- /Contact/add-contact-info/{id} -> adds a new contact info with a given contact id,
- /Contact/get-contacts -> lists all the contacts,
- /Contact/get-contact-by-id/{id} -> get a contact detail with a given contact id,
- /Contact/delete-contact/{id} -> deletes a contact with a given id
- /Contact/delete-contact-info/{contactId}/{contactInfoId} -> deletes a contact detail with a given contact id

- /Report/add-report -> request a new report ,
- /Report/get-one-report/{id} -> gets a single report with a given report id,
- /Report/get-all-reports -> gets all the reports
