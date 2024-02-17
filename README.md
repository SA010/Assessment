# Sample Microservices and Angular App

This repository contains sample code to demonstrate a microservices architecture using Angular for the frontend.

>  **Note:** The Angular App will be added in the next few days.

## Running the Application

To get the application up and running, you'll need to use Docker Compose.

```bash
docker compose -f .\docker-all.yml up -d
```

> ⚠️ **Warning:**  If you have MS SQL and RabbitMQ instances running locally, there may be conflicts. Ensure that local services are stopped before running the Docker Compose command.

## Docker Compose Services

The docker-all.yml file configures the following services:

* sa010/sag-company-service-api - A sample company service. The code is located in .\CompanyService\.
* sa010/sag-vacancy-service-api - A sample vacancy service. The code is located in .\VacancyService\.
* sa010/sag-vacancy-service-esbworkers - A .NET Core background service that listens to RabbitMQ for updates from CompanyService and synchronizes Company data within a Vacancy microservice.
* MS SQL server 2019 - The database used for data persistence.
* RabbitMQ - The message broker service for communication between services.

## Source Code

Please note that the microservices in this repository rely on my private NuGet packages that are not included, which means the services will not build. This setup is intended to show the microservices architecture and the project setup.

## Features Showcased

* Microservices simplification by moving code to NuGet and adding the necessary middleware.
* Basic CRUD operations.
* Exception handling.
* CORS settings.
* Swagger configurations for API documentation.
* Event handling for publishing and subscribing to messages via RabbitMQ.
* Logging implementation.

## RESTful API

The services are designed to follow RESTful principles, ensuring that the API is modular and easy to integrate with.

## Testing

* SpecFlow for API tests.
* xUnit for unit testing of the service components.

## Logging with Elasticsearch and Kibana

To see the logs in action run the Elastic container using the following command

```bash
docker compose -f .\docker-elastic.yml up -d
```
> ⚠️ **Warning:**  This has dependencies on the `docker-all.yml`, so ensure that `docker-all.yml` is running before you start the Elasticsearch container.