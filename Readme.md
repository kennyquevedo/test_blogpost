
# BlogPost Managment
.Net solution with web app and web appi project that contains all de code that implement an app to manage (Add, edit) post. This solution use MS SQL Server for database engine.
This soultions has differents types of project that build a ntier solution:

 - BlogPost.Domain: Class Library (.NET 5.0) Contain all entity classes that represent an object in the solution.
 - BlogPost.AppCore: Class Library (.NET 5.0) Represent Data access layer in the solution.
 - BlogPost.Common: Class Library (.NET 5.0) Contain all util code common in all the solution.
 - BlogPost.WebApi: Asp.Net Core (.NET 5.0) Contain all api methods to allow manage the post flow.
 - BlogPost.BLogic: Class Library (.NET 5.0) This is the project between core (DAL) and api project.
 - BlogPost.WebApp: Asp.Net Core (.NET 5.0) Presentation layer that contain al the views.
 - BlogPost.Testing: MSTest Project to create an test the unit tests.


## Setup
 1. Please download or fork the code to get a copy of this example.
 2. After that you need to run some commands to create database, first
    please check your connection string to run these commands. You can
    check connection string in the file **appsettings.Development.json**
    and **appsettings.json**.
3. Build Solution.
4. Set startup project **BlogPost.WebApp**, this contain connection string to database.
5. Check **ASPNETCORE_ENVIRONMENT** variable in the **WebApi** and **WebApp** projects and set to "Development"
6. Open **Package Manager Console** and set default project to **BlogPost.WebApp**
7. run the command `update-database`, this create the new database (check connection string name) and all the tables.


# Description Flow.
By default after setup and first run of WebApp, this create necesary roles and admin user to allow navigate the app. However you can create your own user in the **Register** option in the main Menu and fill the form to create a new one.
![User Registration](https://i.imgur.com/Hv4KNjT.png)

After you logged, please log in as admin again to manage user roles. By default all recent user are **Viewer**
![Manage Roles](https://i.imgur.com/aRdyIgd.png)

This app has different roles, Editor, Super, Viewer, Writer.

 - Super: Can manage roles, users and post..
 - Editor: Can manage post (approve, reject).
 - Writer: Can create and see posts.
 - Viewer Only can view Approved posts.

To manage post, please login as Writer and click on **Posts** in the main menu. Here you can see a List with different status to filter the posts list **(Approved, Rejected, Published, Needs Review)**.
please click on **Add New Post** to navigate to add form view. By default all new post are saved with "Published" status.
![Add new post](https://i.imgur.com/Dlqhqaz.png)

After create a new post, please login as **Editor** user to change status post and add the comment of the change. To edit a post you need to click on the specificy post and then app will go to the Edit post view.
You can edit the content of the post, add a comment related to this change and apply status to the post. Remember that Viewer users only can see Approved posts.

![Edit Post](https://i.imgur.com/9gIQNdY.png)

