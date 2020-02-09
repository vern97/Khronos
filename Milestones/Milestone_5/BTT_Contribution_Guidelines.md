## Guidelines
These guidelines are meant to be followed when contribting code to this project.

#### Microsoft .NET Framework
- Create a ViewModels folder to hold any ViewModels within the Models folder
    - ViewModel classes should be descriptive - e.g AthleteDetailsViewModel or MeetingDetailsViewModel
- Create a DAL folder within the project to hold any Context(s)
- Models should be named in the singular context, however, collections within Models should be named in the plural context.
 
#### C#
- Follow all C# coding conventions found on this [Microsoft Documentation](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/inside-a-program/coding-conventions)
- Comment often; especially on code in which the use may not be obvious
 
#### Microsoft SQL
- Pluralize table names
- All primary and foreign keys should be an INT
- Primary keys are to be denoted as *ID*
- Foreign keys are to be denoted as *EntityID*
- Table names should be plural
- A third table is to be used for M:N relationships
    - The table name should be descriptive for its use - e.g a table for an M:N relationship between Athletes and Teams should be AthleteTeams

#### CSS
- All CSS should be written in a seperate .CSS file (no CSS in Views)

#### Javascript
- All JavaScript should be written in a seperate .JS file (no Javascript in Views)
    
#### GitHub
- Use feature branches
    - Feature branches should be named in this context - *first_initial-task_name* (b-navbar)
    - A feature branch should only contain changes for a single feature
- Commit often; make it easy for the GitMaster to see what code has changed and why
    - Commits should always be written as present tense
- Merge dev into your feature branch to ensure there are no merge conflicts
    - You are responsible for resolving merge conflicts, not the GitMaster
- Push your changes to your remote feature branch
- Create a pull request on the main repository with the changes to your feauture branch
- Once pull request is accepted, pull from upstream to update local repository
    - If your pull request is rejected, check pull request comments and fix whatever needs to be fixed
- For more information on pull requests visit this [GitHub Documentation](https://help.github.com/en/desktop/contributing-to-projects/creating-a-pull-request)