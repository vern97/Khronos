## Notes About User Stories

 - A **participant** in the computer science program includes students, tutors, professors, and administrators. 
 - A **visitor** is anyone that views the site and is not registered. 
 - A **student** is any visitor that is registered without special credentials. 
 - A **registered user** is any registered person regardless of title. 
 - A **tutor** is also a **grader**. 
 - **Registered (student, tutor, professor, administrator)** are used for clarity when needed.

## User Stories
1. **[E] Have a web application for the Computer Science tutoring department** 
   1. [U] As a participant in the computer science program, I want a web presence for computer science tutoring so that I can access information online.       
      - [T] Create starter ASP dot NET MVC 5 Web Application with Individual User Accounts and no unit test project
      - [T] Choose CSS library (Bootstrap 3 or 4 or other) and use it for all pages
      - [T] Change the displayed application name to Beyond the Tutor
   2. [U] As a visitor, I want to be presented with a modern and informative home page so that I can easily find what I am looking for. 
      * [T] Create and add a custom logo
      * [T] Add image(s) that evoke thoughts of campus life/studying
      * [T] Incorporate Western Oregon University school colors
      * [T] Customize the navigation bar to include features currently available
      * [T] Add visuals and text to describe the main function of the application
   3. [U] As a visitor, I want to view a FAQs page that tells me more about the application so that I can know if the services would benefit me. 
      * [T] Add an obvious link on the homepage that links to a general use FAQs page
      * [T] Write general questions/answers about the application
   4. [U] As an administrator, I want users to see a nice error page when they attempt to access a page that does not exist so that users know the request cannot be fulfilled
      * [T] Direct bad requests to a 404 Not Found page
      * [T] Add a nice visual or amusing text, not just a generic page
2. **[E] Have individual user accounts with different privileges** 
   1. [U] As a visitor, I want the ability to register for an account so that I can access features only available to registered users. 
      * [T] Copy SQL scheme from an existing ASP.NET identity database and integrate into our UP script
      * [T] Configure web app to use our db with identity tables in it
      * [T] Enable login and register links
      * [T] Manually test register and login; user should easily be able to see that they are logged in
   2. [U] As a registered user, I want to the ability to manage my account so that I can keep it secure. 
      * [T] Enable username to link to account management page
      * [T] Manually test management page; user should easily be able to change password
   3. [U] As a registered student, I want to view a FAQs page that tells me more about features available to me as a student.
      * [T] Add an obvious link on the homepage that links to a student use FAQs page
      * [T] Write student-specific questions/answers about the application
   4. [U] As a registered tutor, I want to view a FAQs page that tells me more about features available to me as a tutor.
      * [T] Add an obvious link on the homepage that links to a tutor use FAQs page
      * [T] Write tutor-specific questions/answers about the application
   5. [U] As a registered professor, I want to view a FAQs page that tells me more about features available to me as a professor.
      * [T] Add an obvious link on the homepage that links to a professor use FAQs page
      * [T] Write professor-specific questions/answers about the application 
   6. [U] As an administrator, I want to view a FAQs page that tells me more about features available to me as an administrator.
      * [T] Add an obvious link on the homepage that links to a administrator use FAQs page
      * [T] Write administrator-specific questions/answers about the application 
   7. [U] As an administrator, I want to ensure students read a Rights and Responsibilities section so that tutoring services are used properly.
      * [T] Add an obvious link on the registered student FAQs page that links to a Rights & Responsibilities document
      * [T] Record that a student has viewed the Rights & Responsibilities page
3. **[E] Have real-time tutoring availability**
   1. [U] As a visitor, I want to quickly see tutoring schedules so that I know when I can drop-in the tutoring center for help.
       * [T] Display current tutor schedules clearly on the homepage 
   2. [U] As a visitor, I want to quickly see if a tutor is unavailable during regularly schedules times so that I do not waste my time dropping in the tutoring center when tutors are not available. 
       * [T] Have a feature on the homepage to display "Service Alerts" for that day
   3. [U] As a tutor, I want to enter my tutoring schedule so that students know when I am available in the tutoring center. 
       * [T] Create a feature where a tutor can manually input their scheduled times in the tutoring center
       * [T] Link to the above feature in the page with tutor features
   4. [U] As a tutor, I want to let students know if I am running late or absent so that they do not stop by the tutoring center when I am unavailable. 
       * [T] Create a feature where a tutor can quickly input that they are running late, unavailable for the day, or staying late in the tutoring center -- basically any last minute changes not on the front page schedule 
       * [T] Connect this feature to the "Service Alerts" location on the homepage
   5. [U] As an administrator, I want the ability to edit tutoring schedules so that I can update changes if a student is unable to do this. 
	   * [T] Create a feature where an administrator can manually edit tutor scheduled times in the tutoring center
       * [T] Link to the above feature in the page with administrator features
   6. [U] As an administrator, I want the ability to let students know if a tutor is running late or absent so that I can update changes if a student is unable to do this. 
       * [T] Create a feature where an administrator can quickly input that a tutor is running late, unavailable for the day, or staying late in the tutoring center -- basically any last minute changes not on the front page schedule 
       * [T] Connect this feature to the "Service Alerts" location on the homepage
4. **[E] Have appointment scheduling**
   1. [U] As a student, I want the ability to request an online or in-person tutoring session so that I can work around my busy schedule. 
       * [T] Add a feature where a student can input a date, time, and subject they would like tutored on
       * [T] This feature should check if the tutor is already booked or unavailable during that time
       * [T] There should be a confirmation message displayed when the request is successfully processed
       * [T] This feature should be linked on the homepage navigation bar
   2. [U] As a tutor, I want the ability to "block out" dates and  times when I am unavailable for scheduled tutoring sessions so that I do not get requests for times when I am definitely not available. 
       * [T] Add a feature where a tutor can choose dates and times that they want to make themselves unavailable for tutoring
       * [T] This feature should also allow the tutor to unblock any previously blacked out dates in the case their schedules opens
   3. [U] As a student, I want to receive a prompt response when I request a tutoring session so that I can plan my schedule accordingly. 
       * [T] Add an alert message to the tutor's homepage letting them know there are tutoring requests
       * [T] Add a feature for the tutor that allows them to mark a request as confirmed or denied
   4. [U] As a tutor, I want the option to give the student alternative tutoring options if I am unable to fulfill their request so that I can help the student. 
       * [T] Add a feature where a tutor can input a date and time they would be available
       * [T] There should be a confirmation message displayed when the request is successfully processed
       * [T] Add an alert message to the student's homepage letting them know of the alternate option
       * [T] Add a feature for the tutor that allows them to mark the alternate option as confirmed or denied
       * [T] Make this feature a "once per communication" option - the tutor and student should not be able to endlessly go back and forth with alternate options
   5. [U] As a student, I want the ability to cancel an appointment with a tutor so that the tutor knows I will not be attending the session.  
	   * [T] Add a feature in the same location where a student schedules an appointment that also allows them to cancel an appointment
	   * [T] Display a message that confirms the request was processed successfully
	   * [T] Add an alert message to the tutor's homepage letting them know of the cancellation 
   6. [U] As a tutor, I want the ability to cancel an appointment with a student so that the student knows I will not be attending the session.  
       * [T] Add a feature in the same location where a tutors accepts/denies an appointment that also allows them to cancel an appointment
	   * [T] Display a message that confirms the request was processed successfully
	   * [T] Add an alert message to the student's homepage letting them know of the cancellation
 5. **[E] Provide online tutoring**
    1. [U] As a student, I want a drop-in chat feature so that I can receive help when I am not able to visit the tutoring center. 
       * [T] Add a chat feature to the homepage 
       * [T] This feature should also be displayed on the tutor homepage
    2. [U] As a student, I want to see if a tutor is online so that I know it is okay to ask a question through the drop-in chat feature. 
	   * [T] Add a feature that allows a tutor (working in the tutoring center) to set their status to indicate whether they are available, busy, offline 
    3. [U] As a tutor, I want to hear an alert when a a student uses the drop-in chat feature so that I do not miss a student question.
	* [T] Incorporate a feature that makes a sound (such as a text message or PM sound) to alert the tutor a message was received  
    4. [U] As a student, I want an online tutoring interface so that I can receive help when I am not able to visit the tutoring center. 
	    * [T] Use an API to add an interactive whiteboard interface
	    * [T] Make the "online tutoring room" its own page
	    * [T] Add a link to this feature on the student homepage
    5. [U] As a student, I want to upload documents during online tutoring so that my tutor can see what I am working on. 
	    * [T] Add the ability to upload and share documents between student and tutor and vice versa 
    6. [U] As a tutor, I want a chat filter during interactive tutoring sessions so that students do not abuse the system with inappropriate language. 
	    * [T] Use an API to filter language during "online tutoring room" sessions
    7. [U] As a tutor, I want a chat filter during drop-in chats so that students do not abuse the system with inappropriate language. 
	    * [T] Use an API to filter language during drop-in chat sessions
6. **[E] Log data from tutoring sessions**
   1. [U] As a tutor, I want to manually input session information so that I can make a record of sessions in the tutoring center.
	* [T] Crate db table for session information
	* [T] Create CRUD for session information
	* [T] Add a link to tutor menu to "create session" page 
   2. [U] As a tutor, I want to mark if a student attended a scheduled session so that students with several no-show no-cancel sessions are flagged.
	* [T] Create db table for attendance
	* [T] Create CRUD for attendance
	* [T] Add a link to tutor menu to "create attendance"
	* [T] List active sessions to flag attendance status for student
   4. [U] As a student, I want the option to provide additional information about my tutoring needs so that the department can improve the computer science program.
	* [T] 
7. **[E] Handle professor grading requests**
   1. [U] As a professor, I want to submit grading requests so that I can easily get help from graders.
	* [T] Add a grading request table to db
	* [T] Create CRUD for grading request
	* [T] Add a "submit request" button to Professor menu linking to "create grading request"
   2. [U] As a professor, I want to submit relevant information about a grading request so that the grader knows of any special criteria.
	* [T] Make sure there is an "extra information" column in grading requst table
	* [T] Add a text box to add "extra information" on "create grading request" page
   3. [U] As a professor, I want to upload relevant documents for a grading request so that the grader has the resources they need to properly grade an assignment.
	* [T] Make sure there is a "add documents" column in grading requst table
	* [T] Add an upload button to add "relevant documents" on "create grading request" page
   4. [U] As a professor, I want to see a a list of tutors so that I can choose a specific tutor to request grading from.
	* [T] Add a drop down to "create grading request" to select a tutor
   5. [U] As a professor, I want to see how many grading tasks are currently assigned to each tutor so that I do not overwhelm any one tutor with too much work.
	* [T] Add a "view grading requests" button to professor menu
	* [T] List all active grading requests and which tutor owns them
   6. [U] As a grader, I want the ability to mark the status of a grading request so that professors know whether I received a a request and whether I finished a task. 
	* [T] Add a grader specific view for "view grading requests"
	* [T] Add a button for grader to access "view grading request"
	* [T] Allow grader to select "view grading request" for details
	* [T] Create an edit button on "view grading request" to update grading request
	* [T] Allow grader to mark grading request with current status
8. **[E] Track tutoring time sheets for payroll**
   1. [U] As a tutor, I want to manually input my work times at the tutoring center into a digital time sheet so that I can easily keep a record of hours worked.
	* [T] Add a "time sheet" table to db
	* [T] Create CRUD for "time sheet"
	* [T] Create a "record hours" button for tutor menu only
   2. [U] As a tutor, I want a digital time sheet that automatically calculates my total hours worked according to payroll department criteria so that I can easily keep an accurate record of hours worked.
	* [T] Add a button to tutor menu to view recorded hours
	* [T] Add a drop down to view tables of hours for a month and year
	* [T] Add table for each month-year to view total hours worked
   3. [U] As a tutor, I want my online tutoring sessions automatically logged into my digital time sheet so that I do not have to personally time the length of sessions.
	* [T] Add a button on tutoring menu to start tutoring session
	* [T] Add a button on tutoring menu to stop tutoring session
	* [T] Record tutoring hours from start to stop time
   4. [U] As an administrator, I want to download tutor time sheets so that I can keep accurate records. 
	* [T] Add a button to view tutoring time sheets
	* [T] Select a tutor from drop down
	* [T] Display time sheets in a table for each month-year for selected tutor
	* [T] Download spreadsheet for selected month-year
9. **[E] Provide data analysis for professors**
   1. [U] As a professor, I want to know which classes and subject matter students are getting tutored on so that I can address student needs.
   2. [U] As a professor, I want to know additional student information that is gained from tutoring sessions so that I know how tutoring services are being used. 
   3. [U] As a professor, I want to sort collected data in interesting ways so that I can see patterns in student needs.
   4. [U] As a professor, I want to see future predictions on data so that I can understand student needs over a long period of time. 
10.  **[E] Provide bonus resources for students**
     1. [U] As a visitor, I want to calculate my current weighted grade so that I know how I am doing in my class. 
     2. [U] As a visitor, I want to predict my cumulative GPA so that I can set my goals for the current term. 
     3. [U] As a visitor, I want to calculate what I need to get on my final exam to meet a specific threshold so that I can plan my studying for the final exam. 
     4. [U] As a student, I want the ability to save my current weighted grade so that I do not need to re-enter information the next time I visit the site. 
     5. [U] As a student, I want the ability to save my predicted GPA so that I do not need to re-enter information the next time I visit the site. 
     6. [U] As a student, I want the ability to save what I need to get on my final so that I do not need to re-enter information the next time I visit the site. 
     7. [U] As a visitor, I want a list of well-organized computer science resources so that I can refer to them when I need help on a specific subject matter. 
     8. [U] As a tutor, I want to input and edit the list of computer science resources so that I can add helpful links I discover and remove outdated or bad links. 
     9. [U] As a tutor, I want to input and edit the list of computer science resources so that I can add helpful links I discover and remove outdated or bad links. 


 
