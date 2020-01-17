2019-20 Class Project Inception
=====================================

## Summary of Our Approach to Software Development

[What processes are we following?  What are we choosing to do and at what level of detail or extent?]

## Initial Vision Discussion with Stakeholders

Primary Stakeholder -- Katimichael Phelpedecky, swimming legend and hopeful entrepreneur

Katimichael's experience being on the US Olympic team led to an appreciation of how advanced tools can help athletes perform at their best.  The problem is those tools are very expensive and require personnel with advanced training, i.e. elite analysts for elite athletes.  They want to create a business to give regular swimming coaches, from high school, club, college, and masters, advanced analytical and predictive tools to help the athletes on their teams.  Katimichael has assembled a team of investors to fund this project and is hiring your team to create the product.

The product is centered around three core features:

1. Record, store and provide tracking, viewing and simple stats for race results for swimming athletes.  This would have a number of features found in [Athletic.net](https://www.athletic.net/), which is used for Track and Cross Country running.
2. Provide complex analysis of athlete performance over time and over different race types, to give coaches deep insight into their athlete's fitness and performance that they cannot get from their own analyses.  This includes machine learning to predict future performance based on records of past race performance, given different training scenarios.  Validation of this feature will enable the next feature.
3. Create a tool that will optimize a coaches strategy for winning a specific meet.  This feature will automatically assign athletes to specific races based on their predicted race times in order to beat an opponent coache's strategy.  There will be two modes: one in which we have no knowledge of the opponent team's performance, and one where we do have their performance and can predict their times.

## Initial Requirements Elaboration and Elicitation

### Interviews

*Should the athlete data be public or private for coaches only as mentioned in The 3 Product Core Features #2? Is it exclusive to the coaches and their athletes or is it meant to be seen by anyone*?

The information that we would like our site to display for each athlete will only be visible to visitors on our site if they have an account. Until we implement paid features, there are no restrictions to who can make an account or the information users can view.

*Referring to List of Needs and Features #4; do employees and Contractors take up the title of an Admin? or are admins separate entities from Employees and Contractors*?

At this time, we can define employees and contractors as admins. When we roll out the ability to have athletes separated into teams, we would like to have it so that coaches and contractors can only add, edit or delete any information that belongs to the athletes on their team only.

*You request that all athlete information is made public; are visitors required to make an account and sign in to view information on athletes, or can a visitor be an ‘anonymous’ guest of the website and still view athlete information*?

As we talked about earlier, the information is public to any user at this time, however, they must have made an account on our website to view the information. Because anyone can make an account for free, we consider this public information.

*Athletes that are entered into your system, are they meant to be tagged as an “athlete” and given access to paid features once you make the transition to paid plans, or will they be given a “standard” account and be required to purchase access to “non-standard” features*?

Currently we have no plans to differentiate "standard" users from "athlete" users. Our current plans reflect so as that all user must purchase our paid features when we decide to roll them out, with he exception of administrator users.

*If athlete accounts are to be tagged as "athlete", what type of verification can an athlete provide to prove they are in fact an athlete*?

Currently we have no plans to implement "athlete" accounts.

*In the future, will you expect to track and record more than just swimming athletes? If so, we can plan on creating code that is easily scalable to other event and athlete types*.

At this time, our vision only reflects tracking events that are associated with swimming and swimming only. We are not worried about future scaleability at this time.

## List of Needs and Features

1. They want a nice looking site, with a clean light modern style, images that evoke swimming and competition.  (More like [Strava](https://www.strava.com/features) and less like [Athletic.net](https://www.athletic.net/TrackAndField/Division/Event.aspx?DivID=100004&Event=14))  It should be easy to find the features available for free and then have an obvious link to register for an account or log in.  It should be fast and easily navigable.  
2. The general public will be able to view all results (just the race distance, type and time).  These are public events and the results should be freely available.  They should be able to search by athlete name, team, coach or possibly event date and location.  Not sure if they want to be able to filter or drill down as Athletic.net does.  They're not trying to organize by state, school, etc. Athletes are athletes and it doesn't matter where they're competing.  This is completely general, but only for swimming.
3. Logins will be required for viewing statistics and all other advanced features.  We eventually plan to offer paid plans for accessing these advanced features.  They'll be free initially and we'll transition to paid plans once we get people hooked.
4. Admin logins are needed for entering new data.  Only employees and contractors will be allowed to enter, edit or delete data.
5. "Standard" logins are fine.  Use email (must be unique) for username and then require an 8+ character password.  Will eventually need to confirm email to try to prevent some forms of misuse.  Admins and contractors must have an offline confirmation by our employees and then the "super" admin adds them manually.
6. The core entity is the athlete.  They are essentially free agents in the system.  They can be a member of one or more teams at one time, then change at any time.  Later when we want to have teams and do predictive analysis we'll let the coaches assemble their own teams and add/remove athletes from their rosters.
7. The first stats we want are: 1) display PR's prominantly in each race event, 2) show a historical picture/plot of performance, per race type and distance, 3) some measure of how they rank compared to other athletes, both current and historical, 4) something that shows how often they compete in each race event, i.e. which events are they competing in most frequently, and alternately, which events are they "avoiding"

## Initial Modeling

### Use Case Diagrams

### Other Modeling

[Mind Map 1](Mind_Map_1.jpg)
[Mind Map 2](Mind_Map_2.jpg)

## Identify Non-Functional Requirements

1. User accounts and data must be stored indefinitely.  They don't want to delete; rather, mark items as "deleted" but don't actually delete them.  They also used the word "inactive" as a synonym for deleted.
2. Passwords should not expire
3. Site should never return debug error pages.  Web server should have a custom 404 page that is cute or funny and has a link to the main index page.
4. All server errors must be logged so we can investigate what is going on in a page accessible only to Admins.
5. English will be the default language.

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



## Timeline and Release Plan