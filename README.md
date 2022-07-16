# microsoft-marketing-contacts

Microsoft Marketing Contacts App 
Weekly Log

Week of Jun 20, 2022

    Understand the requirements
      - Setup requirements
      - Knowledge requirements
      - Project requirements

    Setup requirements
      - PC; I will use Virtual Box on my Mac
      - Didn’t work, had to acquire a PC and install Win10
      - Windows 10 operating system
      - Download ISO and os install, https://www.microsoft.com/en-us/software-download/windows10
      - Visual Studio install on Windows 10 (make sure it includes)
          - .NET 6
          - WPF
          - C#
      - MS SQL express on Windows 10
          - Download and install, https://www.microsoft.com/en-us/sql-server/sql-server-downloads
              - Downloaded with Visual Studio installer

    Knowledge requirements
      - Learn basic C#	
          - Microsoft introduction to C#, https://docs.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/tutorials/
          - C# for Python developers, https://gist.github.com/mrkline/8302959
      - Learn WPF
          - WPF tutorial, https://wpf-tutorial.com/
          - Microsoft tutorial, https://docs.microsoft.com/en-us/dotnet/desktop/wpf/get-started/create-app-visual-studio?view=netdesktop-6.0
      - Learn how to use MSSQL Express database
          - Wiki, https://en.wikipedia.org/wiki/SQL_Server_Express
          - Tools, https://docs.microsoft.com/en-us/sql/tools/overview-sql-tools?view=sql-server-ver16
          - Learning resources, tutorials 
                https://docs.microsoft.com/en-us/sql/sql-server/educational-sql-resources?view=sql-server-ver16
                https://docs.microsoft.com/en-us/sql/sql-server/tutorials-for-sql-server-2016?view=sql-server-ver16
          - LocalDB
                https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver16
          - Use entity framework to access data, https://www.entityframeworktutorial.net/
          - Quick refresher on SQLAlchemy - previous learning experience which looks similar to Entity Framework
                https://www.educative.io/courses/flask-develop-web-applications-in-python/gxxPNMjXLm6

Week of Jun 27, 2022

    Status 
      - Ensuring setup requirements is completed
      - Knowledge requirements acquisition is in process
      - Understanding project requirements
      - Working on presentation layer
      - Working on application layer, where data is processed

    What I learned last week
      - How to install Win10
      - That I cannot run Win10 on VirtualBox on my Mac laptop
      - Some basic C# so I am not blocked to make progress
      - Basic Visual Studio project setup
      - Found WPF online tutorials and other learning resources (Microsoft docs)

    Work items this week
      - Initialize private git repository
      - Work on presentation layer
          - Add empty project
          - Add logo and title
          - Add first tab and form elements
          - Input validation
          - Displaying empty DataGrid in the first tab
          - Add a second tab and form elements 
          - Input validation for the second tab form elements
          - Displaying the data grid in the second tab
          - Code reorganization and phone number validation 
          - Adding database support for customers 
          - Adding database support for vendors 

    Resources
      - WPF tutorial (forms and grid), https://wpf-tutorial.com/

Week of Jul 4, 2022

    Status
      - Updating user interface
      - Working on application layer
      - Working on data layer, how is data stored and retrieved

    What I learned last week
      - More about the UI, properties and styling
      - What is Regex? Found a better way to validate phone numbers
      - Managing DB connections in VS using LocalDB
      - Using prepared statements in SQLs to prevent SQL injection

    Work items this week 
      - Adding a digital clock 
      - Adding database support for Master List
      - Adding a third tab for master list data grid
      - Adding a vendor code window 
      - Validating if the company exists in the Master List
      - Vendor Code validation and update 
      - Cleaning the database data and updating field definitions 
      - Using parameterized queries and error handling 
      - Adding clear entry buttons 
      - Presentation layer refining 
      - Set background image 
      - Buttons style 
      - Creating a resource dictionary
      - Vendor code validation
      - Adding a fourth tab for All Contacts
      - Displaying the data grid in the fourth tab
      
