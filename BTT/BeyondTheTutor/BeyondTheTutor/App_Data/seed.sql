﻿INSERT INTO [dbo].[StudentResources](Topic, URL, DisplayText, UserID)
	VALUES
	('HTML', 'https://www.codecademy.com/learn/learn-html', 'HTML Tutorial codeacademy', '2' ),
	('Linked Lists', 'https://www.geeksforgeeks.org/data-structures/linked-list/', 'Linked Lists Structure', '2'),
	('Linked Lists', 'https://www.geeksforgeeks.org/circular-linked-list-set-2-traversal/', 'Circular Linked List Traversal', '2'),
	('Matrix Chain Multiplication', 'https://www.geeksforgeeks.org/matrix-chain-multiplication-dp-8/', 'Matrix Chain Multiplication (Dynamic)', '2'),
	('Breadth First Search', 'https://www.geeksforgeeks.org/breadth-first-search-or-bfs-for-a-graph/', 'BFS for a Graph', '2'),
	('Breadth First Search', 'https://www.tutorialspoint.com/data_structures_algorithms/breadth_first_traversal.htm', 'Breadth First Traversal', '2'),
	('HTML', 'https://www.w3schools.com/colors/colors_picker.asp', 'HTML Color Picker', '2'),
	('Markdown', 'https://en.wikipedia.org/wiki/Markdown', 'Markdown - Wikipedia', '2'),
	('Markdown', 'https://github.com/adam-p/markdown-here/wiki/Markdown-Cheatsheet', 'Markdown Cheatsheet', '2'),
	('Markdown', 'https://stackedit.io/app#', 'Markdown in Browser Editor', '2'),
	('Semantic UI', 'https://semantic-ui.com/', 'Semantic UI Documenation', '4'),
	('Semantic UI', 'https://github.com/Semantic-Org/Semantic-UI', 'Semantic UI GitHub', '4'),
	('Beyond the Tutor', 'https://github.com/vern97/Khronos', 'Beyond the Tutor Source Code', '4'),
	('Beyond the Tutor', 'https://tinyurl.com/rc8u8jm', 'Beyond the Tutor Inception', '4'),
	('AJAX', 'https://www.w3schools.com/xml/ajax_intro.asp', 'AJAX Introduction', '4'),
	('jQuery', 'https://api.jquery.com/jquery.ajax/', 'jQuery Documentation', '4'),
	('jQuery', 'https://www.w3schools.com/jquery/', 'jQuery Tutorial', '4'),
	('Microservices', 'https://microservices.io/', 'Microservices.io', '4'),
	('Microservices', 'https://martinfowler.com/articles/microservices.html', 'Microservices - Martin Fowler', '4'),
	('Microservices', 'https://en.wikipedia.org/wiki/Microservices', 'Microservices Wikipedia', '4'),
	('CSS', 'https://www.w3schools.com/css/', 'CSS Tutorial', '5'),
	('CSS', 'https://css-tricks.com/', 'CSS Tricks', '5'),
	('CSS', 'https://www.bestcssbuttongenerator.com/', 'Custom Button Generator', '5'),
	('CSS', 'https://animista.net/', 'CSS Animations', '5'),
	('Bootstrap', 'https://getbootstrap.com/', 'Bootstrap Documentation', '5'),
	('Bootstrap', 'https://www.w3schools.com/bootstrap4/', 'Bootstrap Tutorial', '5'),
	('Bootstrap', 'https://en.wikipedia.org/wiki/Bootstrap_(front-end_framework)', 'Bootstrap - Wikipedia', '5'),
	('Kotlin', 'https://kotlinlang.org/', 'Kotlin Documentation', '5'),
	('Kotlin', 'https://en.wikipedia.org/wiki/Kotlin_(programming_language)', 'Kotlin - Wikipedia', '5'),
	('Kotlin', 'https://developer.android.com/kotlin', 'Android Apps with Kotlin', '5');

INSERT INTO [dbo].[Classes](Name)
	VALUES
	('CS 122'),
	('CS 133'),
	('CS 160'),
	('CS 161'),
	('CS 162'),
	('CS 260'),
	('CS 271'),
	('CS 340'),
	('CS 360'),
	('CS 361'),
	('CS 363'),
	('CS 364'),
	('CS 365'),
	('CS 434'),
	('CS 465'),
	('IS 240'),
	('IS 278'),
	('IS 340'),
	('IS 345'),
	('IS 350'),
	('IS 355'),
	('IS 485');


INSERT INTO [dbo].[TutoringAppts](StartTime, EndTime, TypeOfMeeting, ClassID, Length, Status, StudentID, TutorID)
	VALUES
	('2020-4-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2020-4-28', '2019-11-30', 'eh', 10, '20min', 'Completed', 3, 4),
	('2020-4-28', '2019-11-30', 'eh', 10, '20min', 'Completed', 3, 4),
	('2020-4-28', '2019-11-30', 'eh', 10, '20min', 'Completed', 3, 4),
	('2020-3-28', '2019-11-30', 'eh', 10, '20min', 'Completed', 3, 4),
	('2020-3-28', '2019-11-30', 'eh', 10, '20min', 'Completed', 3, 4),
	('2020-3-28', '2019-11-30', 'eh', 10, '20min', 'Completed', 3, 4),
	('2020-3-28', '2019-11-30', 'eh', 10, '20min', 'Completed', 3, 4),
	('2020-3-28', '2019-11-30', 'eh', 10, '20min', 'Completed', 3, 4),
	('2020-3-28', '2019-11-30', 'eh', 10, '20min', 'Completed', 3, 4),
	('2020-2-28', '2019-11-30', 'eh', 6, '20min', 'Completed', 3, 4),
	('2020-2-28', '2019-11-30', 'eh', 6, '20min', 'Completed', 3, 4),
	('2020-2-28', '2019-11-30', 'eh', 6, '20min', 'Completed', 3, 4),
	('2020-2-28', '2019-11-30', 'eh', 6, '20min', 'Completed', 3, 4),
	('2020-2-28', '2019-11-30', 'eh', 6, '20min', 'Completed', 3, 4),
	('2020-2-28', '2019-11-30', 'eh', 6, '20min', 'Completed', 3, 4),
	('2020-2-28', '2019-11-30', 'eh', 10, '20min', 'Completed', 3, 4),
	('2020-2-28', '2019-11-30', 'eh', 10, '20min', 'Completed', 3, 4),
	('2020-2-28', '2019-11-30', 'eh', 15, '20min', 'Completed', 3, 4),
	('2020-2-28', '2019-11-30', 'eh', 15, '20min', 'Completed', 3, 4),
	('2020-2-28', '2019-11-30', 'eh', 15, '20min', 'Completed', 3, 4),
	('2020-2-28', '2019-11-30', 'eh', 15, '20min', 'Completed', 3, 4),
	('2020-2-28', '2019-11-30', 'eh', 15, '20min', 'Completed', 3, 4),
	('2020-2-28', '2019-11-30', 'eh', 15, '20min', 'Completed', 3, 4),
	('2020-2-28', '2019-11-30', 'eh', 1, '20min', 'Completed', 3, 4),
	('2020-2-28', '2019-11-30', 'eh', 1, '20min', 'Completed', 3, 4),
	('2020-2-28', '2019-11-30', 'eh', 1, '20min', 'Completed', 3, 4),
	('2020-1-28', '2019-11-30', 'eh', 1, '20min', 'Completed', 3, 4),
	('2020-1-28', '2019-11-30', 'eh', 1, '20min', 'Completed', 3, 4),
	('2020-1-28', '2019-11-30', 'eh', 1, '20min', 'Completed', 3, 4),
	('2020-1-28', '2019-11-30', 'eh', 1, '20min', 'Completed', 3, 4),
	('2020-1-28', '2019-11-30', 'eh', 4, '20min', 'Completed', 3, 4),
	('2020-1-28', '2019-11-30', 'eh', 4, '20min', 'Completed', 3, 4),
	('2020-1-28', '2019-11-30', 'eh', 4, '20min', 'Completed', 3, 4),
	('2020-1-28', '2019-11-30', 'eh', 4, '20min', 'Completed', 3, 4),
	('2020-1-28', '2019-11-30', 'eh', 4, '20min', 'Completed', 3, 4),
	('2020-1-28', '2019-11-30', 'eh', 4, '20min', 'Completed', 3, 4),
	('2020-1-28', '2019-11-30', 'eh', 4, '20min', 'Completed', 3, 4),
	('2020-1-28', '2019-11-30', 'eh', 4, '20min', 'Completed', 3, 4),
	('2020-1-28', '2019-11-30', 'eh', 4, '20min', 'Completed', 3, 4),
	('2020-1-28', '2019-11-30', 'eh', 4, '20min', 'Completed', 3, 4),
	('2020-1-28', '2019-11-30', 'eh', 4, '20min', 'Completed', 3, 4),
	('2020-1-28', '2019-11-30', 'eh', 4, '20min', 'Completed', 3, 4),
	('2020-1-28', '2019-11-30', 'eh', 7, '20min', 'Completed', 3, 4),
	('2020-1-28', '2019-11-30', 'eh', 7, '20min', 'Completed', 3, 4),
	('2020-1-28', '2019-11-30', 'eh', 7, '20min', 'Completed', 3, 4),
	('2020-1-28', '2019-11-30', 'eh', 7, '20min', 'Completed', 3, 4),
	('2020-1-28', '2019-11-30', 'eh', 7, '20min', 'Completed', 3, 4),
	('2020-1-28', '2019-11-30', 'eh', 7, '20min', 'Completed', 3, 4),
	('2020-1-28', '2019-11-30', 'eh', 7, '20min', 'Completed', 3, 4),
	('2020-1-28', '2019-11-30', 'eh', 7, '20min', 'Completed', 3, 4),
	('2020-1-28', '2019-11-30', 'eh', 7, '20min', 'Completed', 3, 4),
	('2020-1-28', '2019-11-30', 'eh', 7, '20min', 'Completed', 3, 4),
	('2020-1-28', '2019-11-30', 'eh', 7, '20min', 'Completed', 3, 4),
	('2020-1-28', '2019-11-30', 'eh', 7, '20min', 'Completed', 3, 4),
	('2020-1-28', '2019-11-30', 'eh', 7, '20min', 'Completed', 3, 4),
	('2020-1-28', '2019-11-30', 'eh', 7, '20min', 'Completed', 3, 4),
	('2020-1-28', '2019-11-30', 'eh', 7, '20min', 'Completed', 3, 4),
	('2020-1-28', '2019-11-30', 'eh', 7, '20min', 'Completed', 3, 4),
	('2020-1-28', '2019-11-30', 'eh', 7, '20min', 'Completed', 3, 4),
	('2020-1-28', '2019-11-30', 'eh', 7, '20min', 'Completed', 3, 4),
	('2020-1-28', '2019-11-30', 'eh', 7, '20min', 'Completed', 3, 4),
	('2020-1-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2020-1-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2020-1-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2020-1-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2020-1-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2020-1-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2020-1-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-12-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-12-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-12-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-12-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-12-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-12-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-12-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-12-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-12-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-12-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-12-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-11-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-11-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-11-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-11-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-11-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-11-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-11-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-11-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-11-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-11-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-11-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-11-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-11-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-11-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-11-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-11-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-11-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-11-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-10-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-10-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-10-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-10-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-10-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-10-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-10-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-10-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-10-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-10-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-10-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-10-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-10-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-10-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-10-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-9-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-9-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-6-28', '2019-11-30', 'eh', 8, '20min', 'Completed', 3, 4),
	('2019-9-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-9-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-7-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-7-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-8-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-8-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-6-28', '2019-11-30', 'eh', 8, '20min', 'Completed', 3, 4),
	('2019-6-28', '2019-11-30', 'eh', 8, '20min', 'Completed', 3, 4),
	('2019-6-28', '2019-11-30', 'eh', 8, '20min', 'Completed', 3, 4),
	('2019-6-28', '2019-11-30', 'eh', 8, '20min', 'Completed', 3, 4),
	('2019-6-28', '2019-11-30', 'eh', 8, '20min', 'Completed', 3, 4),
	('2019-6-28', '2019-11-30', 'eh', 8, '20min', 'Completed', 3, 4),
	('2019-6-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-6-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-6-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-6-28', '2019-11-30', 'eh', 5, '20min', 'Completed', 3, 4),
	('2019-6-28', '2019-11-30', 'eh', 10, '20min', 'Completed', 3, 4),
	('2019-6-28', '2019-11-30', 'eh', 10, '20min', 'Completed', 3, 4);


