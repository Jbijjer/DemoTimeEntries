Public Class DemoHelper

    ''' <summary>
    ''' Used once at first launch to create demo data
    ''' </summary>
    Public Shared Sub CreateNewData(ByRef dde As DBDemoEntities)
        'Create Users
        Dim uc As New UsersController()
        dde.Users.Add(uc.CreateUser("Sébastien Bouchard"))
        dde.Users.Add(uc.CreateUser("Martin Lambert"))
        dde.Users.Add(uc.CreateUser("Donald Trump"))
        dde.Users.Add(uc.CreateUser("Barack Obama"))
        dde.SaveChanges()

        'Create Projects
        Dim pc As New ProjectsController()
        dde.Projects.Add(pc.CreateProject("Create an application for demo"))
        dde.Projects.Add(pc.CreateProject("Upgrade old app to use Entity and DevExpress"))
        dde.SaveChanges()

        'Create Tasks
        Dim tc As New TasksController()
        dde.Tasks.Add(tc.CreateTask("Analyzis"))
        dde.Tasks.Add(tc.CreateTask("Modeling"))
        dde.Tasks.Add(tc.CreateTask("Meeting"))
        dde.Tasks.Add(tc.CreateTask("Formation"))
        dde.Tasks.Add(tc.CreateTask("Programming"))
        dde.Tasks.Add(tc.CreateTask("Testing"))
        dde.SaveChanges()

        'Create Entries
        Dim ec As New EntriesController()
        dde.Entries.Add(ec.CreateEntry(dde, "Analyze the project", "2016-11-11", 3.5, "Create an application for demo", "Analyzis", "Sébastien Bouchard"))
        dde.Entries.Add(ec.CreateEntry(dde, "Analyze the DB Model", "2016-11-11", 3.5, "Create an application for demo", "Modeling", "Sébastien Bouchard"))
        dde.Entries.Add(ec.CreateEntry(dde, "Execute the DB Model", "2016-11-11", 3.5, "Create an application for demo", "Modeling", "Sébastien Bouchard"))
        dde.Entries.Add(ec.CreateEntry(dde, "Trying to program as Code First approach", "2016-11-11", 3.5, "Create an application for demo", "Programming", "Sébastien Bouchard"))
        dde.Entries.Add(ec.CreateEntry(dde, "Program as model first approach", "2016-11-11", 3.5, "Create an application for demo", "Programming", "Sébastien Bouchard"))
        dde.Entries.Add(ec.CreateEntry(dde, "Testing the app", "2016-11-12", 3.5, "Create an application for demo", "Testing", "Sébastien Bouchard"))
        dde.Entries.Add(ec.CreateEntry(dde, "Meeting with Barack Obama", "2016-11-09", 1.5, "Upgrade old app to use Entity and DevExpress", "Meeting", "Donald Trump"))
        dde.Entries.Add(ec.CreateEntry(dde, "Meeting with Donald Trump", "2016-11-09", 1.5, "Upgrade old app to use Entity and DevExpress", "Meeting", "Barack Obama"))
        dde.Entries.Add(ec.CreateEntry(dde, "Testing the Sebastien's demo", "2016-11-13", 2, "Create an application for demo", "Testing", "Martin Lambert"))
        dde.SaveChanges()


    End Sub
End Class
