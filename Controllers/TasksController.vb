Public Class TasksController

    ''' <summary>
    ''' Create an instance of a new task
    ''' </summary>
    ''' <param name="Name">Name of new task</param>
    ''' <returns>Task created</returns>
    Public Function CreateTask(ByVal Name As String) As Task
        'set properties
        CreateTask = New Task()
        CreateTask.Name = Name
        CreateTask.CreatedDate = DateTime.Now
    End Function

    ''' <summary>
    ''' Get a task by its name
    ''' </summary>
    ''' <param name="dde">An instance of DBDemoEntites</param>
    ''' <param name="Name">Name of task to retrieve</param>
    ''' <returns>Task with the given name</returns>
    Public Function GetTaskByName(ByRef dde As DBDemoEntities, ByVal Name As String) As Task
        GetTaskByName = dde.Tasks.Where(Function(x) x.Name = Name And x.IsDeleted = False).FirstOrDefault
    End Function
End Class
