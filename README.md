# Jobsity.Chat

This Application is a chat with rooms developed with .NET 5. The goal of this aplications is be a web chat where the users can talk to each other and with the command `/stock={stock_code}` they can consult the price of this stock code.

This solution contain WebApi REST for clients and a Service to consume the Stooq API.

This solutions have 5 layers:

    1.Presentation
    2.Application
    3.Domain
    4.Infra
        4.1.Data
        4.2.IoC
        4.3.CrossCutting
    5.WorkerServices
	6.Tests

### Requirements
- RabbitMQ
- MSSQL Server

**For this application to work with full functionality, you need to run the following projects:**

- Jobsity.Chat.WebApi
- Jobsity.Chat.StooqService



## WEB API

This WebApi is built in 4 Layers:

    1.Presentation
    2.Application
    3.Domain
    4.Infra
        4.1.Data
        4.2.IoC
        4.3.CrossCutting

The startup project is `Jobsity.Chat.WebApi`

For run this project nescessary to update the connections string, 
and connection configurations of RabbitMQ, 
these configurations are in appsetings.json on Presentation Project.

```
"JWT": {
    "Key": "43e4dbf0-52ed-4203-895d-42b586496bd4"
  },
  "ConnectionString": {
    "Default": "Server=localhost\\SQLEXPRESS;Database=JobsityChat; Trusted_Connection=True;"
  },
  "Brokers": [
    {
      "Name": "RequestBroker",
      "Hostname": "localhost",
      "Username": "admin",
      "Password": "7BdBmeJ6_RcQZbk",
      "QueueName": "stooqRequest"
    },
    {
      "Name": "ResponseBroker",
      "Hostname": "localhost",
      "Username": "admin",
      "Password": "7BdBmeJ6_RcQZbk",
      "QueueName": "stooqResponse"
    }
  ]
```

The **RequestBroker** and **ResponseBroker** is the broker to do the integrations for get the stock prices.

**Is nescessary to run the migrations of the project 4.1-Data**

## Stooq Service

This service is the last folder in solution

    5.WorkerServices


It is a independently worker service for consume the queue of comands from chat, 
for run this project is nescessary to update the connection configurations of RabbitMQ,
these configurations are in own appsetings.json

```
"Brokers": [
    {
      "Name": "RequestBroker",
      "Hostname": "localhost",
      "QueueName": "stooqRequest"
    },
    {
      "Name": "ResponseBroker",
      "Hostname": "localhost",
      "QueueName": "stooqResponse"
    }
  ]
```
The **RequestBroker** and **ResponseBroker** is the broker to do the integrations for get the stock prices. Is the same of the WEB API project.



