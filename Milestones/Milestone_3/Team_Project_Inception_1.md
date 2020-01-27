## Initial Vision Discussion with Stakeholder(s)
Primary Stakeholder -- Dr. Becka Morgan, professor with the Western Oregon University Computer Science Department and hopeful entrepreneur              
CSD tutoring at WOU is outdated and inefficient. There is little online presence and most tasks are done on paper. This leads to poor communication between tutors and students, a payroll system that doesn't lend itself to easy logging, and professors in the dark regarding student needs. Dr. Morgan wants to create a system that brings tutoring into 2020 and provides a useful platform for everyone connected to the department. Dr. Morgan is hiring your team to create the product. 

#### The product is centered around these core features:

 - Display real-time tutoring availability. This should include current tutoring hours and recent service alerts. 
 - Provide appointment scheduling with quick response time. This requires a registration system for easy communication between student and tutor. 
 - Provide an online interface for tutoring sessions. 
 - Allow online tutoring sessions tracking that will update and store tutor time sheets and analyze sessions information to provide student information for professors. This may include an algorithm to predict future student needs based on records of past student needs. 
 - Create a tool that will allow professors to submit grading requests, grading criteria, and information to assist in the grading process. 
 - Create and consolidate additional student resources such as calculators for grade and GPA computation and store this information for long-term tracking by the user.  

## Questions and Interviews
Q: Do tutors have a set schedule that doesn't change from week to week?    
*A: Yes, tutors set a schedule at the beginning of each term and work the same hours in the tutoring center between weeks 2 or 3 to the week before finals. Tutors can work during finals week if they choose.*

Q: You include "service alerts" as a part of real-time availability. Can you provide examples of when service alerts would be needed?   
*A: Sure, there are times when tutors are running late or take a sick day and busy times when tutors stay past their normal work hours. When availability changes from regularly posted times, we want students to easily find that information.*

Q: When you mention normal work hours, are you referring to tutors that are present in the tutoring center?   
*A: Yes, we currently offer no online tutoring so our tutors are only working in the center.*

Q: You want a quick response time for scheduling requests. Is there a time window you consider "quick"?      
*A: There is no time requirement, we just want something faster than our current process. We want to expedite the process with an online feature that somehow alerts tutors when requests are made. There could possibly be a priority feature for last minute requests but we will let your team decide if that makes sense from a design standpoint.* 

Q: Should there be any restrictions on appointment requests, such as needing to book 24 hours in advance? What about cancellation policies or missed appointments?     
*A: 24 hours might be too long because students have last minute needs. There should be a reasonable window that is longer than a few hours but not quite an entire day. Cancellation requests should be requested within a wider window but can be done at any time leading up the appointment. We do think it would be a good idea to somehow track whether a student attends the online session. And perhaps revoke the ability to make an appointment for the remainder of the term if a number of sessions are missed without proper cancellation.*       

Q: If a student has an appointment with a tutor who can't make the originally scheduled time slot, should the student be notified about a "service alert"? How should they be notified?          
*A: Tutors should also have a way to cancel appointments, and this could go through the same system as students. However, there is no punishment for tutors that need to cancel.*       

Q: Will notifications for certain "service alerts", including appointment confirmations, be sent through email or a similar service?     
*A: The simplest route would be messaging and alerts send to the registered email. We would love to see a messaging system within the application, simply for the convenience, but would be satisfied with either implementation, as long as something exists.*         

Q: Should tutoring requests be a specified amount of time 30 minutes, 1 hour?     
*A:  Realistically, a session would likely be at least 30 minutes long. Maybe sessions can be booked in 30 minute increments. The entire time does not need to be used and there is no penalization for only taking some of the session time.*

Q: Can online sessions be scheduled during times the tutor is in the tutoring center?      
*A: We feel this would complicate things and make it unfair for students that just want to drop-in. At this time, online sessions should only be available when a tutor is out of the tutoring center. Let's leave open the possibility of taking a different approach here.*

Q: Should there be real time updates on the current status of a tutor? E.g a tutor is offline, online or currently helping a student?   
*A: We think this could be a helpful feature.* 

Q: During an active time slot that a tutor isn't working with another student, will there be an option for drop in tutoring via text, audio or video chat if a tutor is online?     
*A: As we stated earlier, online sessions should not be scheduled during times a tutor is in the center. However, we don't oppose an online "drop-in" feature where students can ask a quick question. This would tie in with the feature you mentioned previously. This is something to note in a FAQ, that drop-ins are on a limited basis and scheduled sessions are recommended, as students in the tutoring center would take precedence.*  

Q: How advanced an interface do you want for online tutoring? Should we implement chat, video, audio? Is there screen sharing capability or some way to share documents?      
*A: An ideal interface would have a way for students and tutors to share documents to make sessions flow easily. We like the interface used on [Chegg.com](www.chegg.com), where tutors use a virtual whiteboard, or chat via text, audio or video. However, we are primarily focused on the virtual whiteboard and chat via text at this time. We would also like language filtering to ensure no one abuses the system.* 

Q: Can you tell us more about tutor time sheets? How is this information used?     
*A: Currently, our tutors track their work hours on paper. We would like to move this online. Online session time should be logged automatically and we don't want tutors to edit these times. Other hours can be entered manually or based on current scheduling.* 

Q: You mentioned analyzing session information for professors. Can you elaborate?     
*A: We want tutors to keep a record of classes tutored and general information that was covered during sessions. This can provide valuable insight to faculty in terms of seeing student needs and considering those needs in course design.* 

Q: How do you want tutors to record session information? Should this be manual input?     
*A: We will let your team decide on the best approach here. Classes tutored could be chosen from a list of options, but it might be best to allow manual input for topics covered, since needs would vary and may be specific. The length of sessions should be automatically generated.*

Q: Do you need us to store additional information like chat logs?     
*A: We don't want any information aside from session length, class tutored, and a short descriptor of what was covered. We want to grant students and tutors privacy so that everyone is comfortable in their interactions.* 

Q: How would you like us to analyze the results for professors? Are you thinking graphs? And we noticed you mentioned predictive analysis.    
*A: Data should be displayed on visually appealing graphs and be generated quickly and easily. We don't want to open up a page in the browser or redirect the user every time a new graph is chosen. Because it takes time to gather useful data, we would like a system that can recognize patterns and predict future student needs.* 

Q: Is there a specific way you want to display data? Should it be searchable by class? School term? Week? Etc?   
*A: There should be some sorting going on. We would like professors to see term-specific data but they should also be able to see all data, or a time window of data, to analyze long-term trends. They should definitely be able to view data by class name and I think data by day and week is implied, so we're not sure if that needs to be a specific feature.*

Q: It sounds like tutors also assist in grading. Can you tell us what you would like to see in a professor-grading request feature?    
*A: In person, a professor and tutor should have already connected so that a tutor knows grading will be expected. And professors have to grant the tutor access to the course material they are grading of course. In the application, we would like a simple feature where professors can request grading and a messaging or comment feature to elaborate on other needs such as preferred finish by date or things the tutor should know.* 

Q: Should there be any restrictions on how many grading assignments can be given at one time?    
*A: Good question, we didn't consider that! It might be a good idea to somehow let the professor know how many grading tasks are currently assigned to a tutor so that no tutor is overwhelmed. This would also help in dividing work evenly when there are multiple tutors to choose from. And maybe there should be some type of confirmation that everything was received on both sides of the communication.* 

Q: Can you elaborate on the additional student resources for the application?    
*A: A weighted grade calculator, an overall GPA calculator, and a calculator that determines what grade is needed on the final based on current grade and exam weight.*

Q: Let's discuss the login features. First, who needs login capabilities?   
*A: Students, tutors, professors, and administrators.*

Q: Which features are accessible without logging in?   
*A: All visitors can view real-time tutoring availability, FAQs about the application, and use calculators and other bonus resources. Anything that requires tracking will require registration and logging in.* 

Q: Who is allowed to register?   
*A: Anyone with a valid WOU email can register. The registration process should validate this information. We are dealing with student information here, so we need your team to make security a top priority. Passwords should be secure and not stored "as-is". We also don't think you should use V-numbers. An email should be enough and you can assign IDs as needed within the system.* 

Q: You said that an email should be enough. Do you want us to require other information such as name or student standing for registration? Should we ask for any information that could be used to generate insights for professors?   
*A: First and last name, WOU email, password and password confirmation should be required. Maybe a security measure to protect against bots. Now that you mention it, knowing which grade levels use the site might be useful information. Perhaps you could include voluntary sections. Things like major and whether the student is a transfer. But it should be very clear that information is voluntary and not required for registration. The information should also not be attributed to a specific student on graphs. It should just be put into a "pool" of information. At no time, anywhere in our system, should an elevated user be able to see which specific student used which service. Address privacy concerns in a FAQ. Collected data is only meant to improve the system and CSD program,* 

Q: Since WOU emails stay active when a student graduates or leaves WOU, will someone registering as a student need to be currently enrolled at WOU? If so, how will we verify their enrollment? How will we determine their enrollment has expired?   
*A: Student year, such as freshman, sophomore, and so on, could be a part of the registration process and once a student passes senior status, their account can be removed from the system or marked as inactive or graduated. Should a student return to WOU after that time, they would need to register again.*     

Q: Which features--for students--require logging in by the student?   
*A: Scheduling and canceling appointments, participating in online tutoring, any messaging within the application, and storing information generated by calculators.*

Q: Which features--for tutors--require logging in by the tutor?    
*A: All features built specifically for the tutor require logging in.*

Q: From our understanding, *a tutor is a student*--should this require separate logins?   
*A: That isn't necessary.* 

Q: Which features--for professors--require logging in by the professor?    
*A: All features built specifically for the professor require logging in.*

Q: What is the administrator role and what privileges are granted to this role?    
*A: The administrator role will be the administrator in the department, and perhaps other high-level positions. We don't anticipate this role belonging to more than 1 or 2 people. An administrator should be able to view, but not edit online session information; view, but not edit time sheets; view how many grading tasks are assigned to a tutor but not details such as messaging--we also don't want an admin to see private messages. The admin can also view graphed data that is available to professors. This role is more of an oversight role. They could also have the ability to alter availability if a tutor is unable.* 

Q: Will tutors, professors, and admins go through the same registration portal as students?   
*A: No, the registration process and login in feature should be separate for students and elevated users. It's okay if they are presented on the same page, but there should be a safeguard to protect against students accessing elevated permissions. We could give elevated users special credentials in person if that helps.*

Q: Do you have a vision in mind for how this should look? What do you see when you open the homepage?    
*A: We want a modern, simple style. Try to incorporate WOU colors. The homepage should display current tutoring schedules and service alerts for that day. It should be obvious how to register and login. We don't want a busy homepage so other features should be linked and easy to navigate. We want a FAQs section that is easy to find so that students know exactly how to use the system.* 

Q: Just to confirm, every service is free of charge?   
*A: Yes, student services are 100% free. State this somewhere on the application.*

Q: Do you plan to expand this application beyond the computer science department?   
*A: Our current focus is on this department only and we want to tailor the app to CS needs. However, we don't want this designed in a way that completely closes the door to other departments. We think our ideas so far leave the door open.* 

Q: Is there anything else you want us to know before we get started?    
*A: I think that is enough information to get your team going. We would be happy seeing these features implemented but will be in touch if we think of anything else. We also really like the name Beyond the Tutor and would like a custom logo on the home page.*  

## List of Needs and Features

 1. They want a modern site with school colors and a custom logo. The homepage should be simple yet informative. It should be obvious how to register, login, use additional features, and read FAQs. Current tutoring availability is visible.   
 2. The general public will be able to view tutoring availability, read FAQs, and use calculators. 
 3. Student logins with a valid WOU email will be required to schedule online tutoring, participate in online tutoring, and to store information generated by calculators.
 4. Tutor logins with valid credentials will be required to update availability, provide online tutoring, accept grading requests, and update time sheets. 
 5. Professor logins with valid credentials will be required to view collected data and to submit grading requests. 
 6. Admin logins with valid credentials will be required to provide oversight on the application.
 7. Student registration will require first and last name, valid school email, class standing, and secure password. There will be clearly marked voluntary fields to provide additional information that is useful for professor insights. Password should be confirmed and email should be validated. 
 8.  Student registration and login should somehow be separated from elevated user registration and login. 
 9. Student accounts will be marked inactive or removed from the system once the student has graduated. 
 10. There should be a FAQs section that explains how the application works. 
 11. There should be an additional resources section that includes grades calculators.
 12. When a student submits a scheduling request, there should be a way to alert the tutors and a messaging system to confirm or deny the request. Requests must be submitted within a reasonable window and cancellations are required if a student can't attend the session. Tutors should have a way to note whether a student attended the session and there should be reasonable action taken if a student has a pattern of not attending sessions without cancellation. 
 13. Tutoring sessions should be available in common sense increments, such as 15, 30, 45, or 60 minute blocks. 
 14. Scheduling online sessions with tutors working in the tutoring center is not allowed, however it is okay to have an online drop-in feature for quick questions. There should be a way for students accessing the online application to see if a tutor is online or busy with another student. 
 15. Tutors should be able to log which class they tutored and input a short descriptor of topics covered. Other session information, such as duration of session, should be auto generated upon completion of the session.
 16. There should be an online interface with interactive whiteboard and chat via text capabilities. Both student and tutor should be able to upload documents to share during the session. 
 17. Tutors should be able to add real-time service alerts to display on the homepage.  Only service alerts for that day should display on the homepage, otherwise display a message saying everything is on schedule. 
 18. There should be a online time sheet tracking tool that automatically receives duration time from online sessions and can manually accept input from time worked in the tutoring center. Duration from online sessions should not be something a tutor can edit. Tutors can input and edit other times. 
 19. Data collected from sessions and registration should be displayed in useful ways on dynamically generated graphs. Professors and admins can view this information. There should be machine learning or an algorithm implemented to make predictive analysis. 
 20. Chat logs from online sessions should not be stored. 
 21. There should be a feature that easily allows a professor to submit grading requests to a tutor. Professors should be able to see how many grading tasks are currently assigned to each tutor. Students should be able to mark a request as received and professors should be able to see the request was received. Students should be able to mark a request as finished to update their current amount of tasks. 

 ## Initial Modeling
 [Use Case Diagram](BTT_Use_Case_Diagram.png)

