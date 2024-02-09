Feature: Configuration
Test the configuration of the application

Background:
	When I initially startup the application

@happyflow
Scenario: Dependency injection on startup
	Then I expect that all API controllers can be instantiated correctly

@happyflow
Scenario: Swagger is available
	Then I expect that a swagger endpoint is available

@happyflow
Scenario: AutoMapper profiles are configured correctly
	Then I expect that all AutoMapper profiles are configured correctly
