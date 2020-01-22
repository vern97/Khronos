2019-20 Class Project Inception
=====================================

## Summary of Our Approach to Software Development

[Approach to Development](Approach_to_Development.pdf)

## Initial Vision Discussion with Stakeholders

[Vision Statement](Vision_Statement.pdf)

## Initial Requirements Elaboration and Elicitation

### Interviews         

## List of Needs and Features

[Needs and Features](Needs_and_Features.pdf)

## Initial Modeling

### Use Case Diagrams

[Use Case Diagram](Use_Case_Diagram.png)

### Other Modeling

[Mind Map 1](Mind_Map_1.jpg)
[Mind Map 2](Mind_Map_2.jpg)

## Identify Non-Functional Requirements

[Nonfunctional Requirements](Nonfunctional_Reqs.pdf)

## Identify Functional Requirements (User Stories)

E: Epic  
U: User Story  
T: Task  

1. [U] As a visitor to the site I would like to see a fantastic and modern homepage that introduces me to the site and the features currently available.
	- [T] Create starter ASP dot NET MVC 5 Web Application with Individual User Accounts and no unit test project
	- [T] Choose CSS library (Bootstrap 3, 4, or ?) and use it for all pages
	- [T] Create nice homepage: write initial content, customize navbar, hide links to login/register
	- [T] Create SQL Server database on Azure and configure web app to use it. Hide credentials.

1. [U] As a visitor to the site I would like to be able to register an account so I will be able to access athlete statistics
	- [T] Copy SQL schema from an existing ASP.NET Identity database and integrate it into our UP script
	- [T] Configure web app to use our db with Identity tables in it
	- [T] Create a user table and customize user pages to display additional data
	- [T] Re-enable login/register links
	- [T] Manually test register and login; user should easily be able to see that they are logged in

1. [U] As a visitor I want to be able to search for an athlete and then view their athlete page so I can find out more information about them
	- [T] Create a search bar to find an athlete based on visitor input
	- [T] Create an athlete table to store athlete information
	- [T] Create athlete pages to display athlete information

1. [U] As a visitor I want to be able to view race results for an athlete so I can see how they have performed
	- [T] Create a section under each athlete page to display race results for a given race distance
	- [T] Manually sort by date or time

1. [U] As a visitor I want to be able to view PR's (personal records) for an athlete so I can see their best performances
	- [T] Create a section under each athlete to display personal records (all races for an athlete)
	- [T] Manually sort by date, race distance or time
	- [T] Create a graph and plot performance per race type and distance against the average of other athletes
	- [T] Create a table to show how often an athlete competes in each race type

1. [U] As an administrator I want an admin exclusive menu so that I can easily access administrator only features
	- [T] Enable an administrator menu with custom administrator only buttons

1. [U] As an administrator I want to be able to enter, edit or delete race results for an athlete quickly and easily so that I don't have to spend a lot of time scrolling through pages to find an athlete
	- [T] Create an administrator page under the administrator menu to select any active athlete
	- [T] Create a table to sort race events by race distance, time and date with buttons to edit or delete events

1. [U] As a robot I would like to be prevented from creating an account on your website so I don't ask millions of my friends to join your website and try to add comments about male enhancement drugs.
	- [T] Create a captcha to verify humanity on user account creation

1. [E] As an administrator I want to be able to upload a spreadsheet of results so that new data can be added to our system

	1. [U] As an administrator I want to be able to quickly and easily upload a spreadsheet of results so that I don't have to enter in the data manually
		- [T] Create an athlete result table to store spreadsheets as binary objects
		- [T] Retrieve contents of any stored spreadsheets as .CSV for input into athlete result table

	1. [U] As an administrator I want to be able to access all uploaded result spreadsheets so that I can cross check information imported for an athlete(s)
		- [T] Create a table of stored result spreadsheets that can easily be sorted by date and time uploaded

## Initial Architecture Envisioning

[Architecture](Architecture_Drawing.jpg)

## Agile Data Modeling

[ER Diagram](ERD_Diagram.png)

## Timeline and Release Plan

[Timeline and Release Plan](Timeline_and_Release_Plan.pdf)