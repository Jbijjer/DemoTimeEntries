Public Class ProjectsController

    ''' <summary>
    ''' Create an instance of a new project
    ''' </summary>
    ''' <param name="Name">Name of new project</param>
    ''' <returns></returns>
    Public Function CreateProject(ByVal Name As String) As Project
        'set properties
        CreateProject = New Project()
        CreateProject.Name = Name
        CreateProject.CreatedDate = DateTime.Now
    End Function

    ''' <summary>
    ''' Get a project by its name
    ''' </summary>
    ''' <param name="dde">An instance of DBDemoEntites</param>
    ''' <param name="Name">Name of project to retrieve</param>
    ''' <returns>Project with the given name</returns>
    Public Function GetProjectByName(ByRef dde As DBDemoEntities, ByVal Name As String) As Project
        GetProjectByName = dde.Projects.Where(Function(x) x.Name = Name And x.IsDeleted = False).FirstOrDefault
    End Function
End Class
