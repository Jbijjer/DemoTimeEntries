Public Class EntriesController


    ''' <summary>
    ''' Create an instance of a new Entry
    ''' </summary>
    ''' <param name="Description">Description of the entry</param>
    ''' <param name="EntryDate">Date of the Entry</param>
    ''' <param name="NbHours">Number of hours spent on this entry</param>
    ''' <param name="ProjectName">Name of the project associated to this entry</param>
    ''' <param name="TaskName">Name of the task associated to this entry</param>
    ''' <param name="UserName">Name of the user associated to this entry</param>
    ''' <returns>Returns an instance of the created entry</returns>
    Public Function CreateEntry(ByRef dde As DBDemoEntities, ByVal Description As String, ByVal EntryDate As DateTime, ByVal NbHours As Decimal, ByVal ProjectName As String, ByVal TaskName As String, ByVal UserName As String) As Entry

        'set properties
        CreateEntry = New Entry()
        CreateEntry.EntryDate = EntryDate
        CreateEntry.Description = Description
        CreateEntry.NbHours = NbHours
        CreateEntry.CreatedDate = DateTime.Now

        'get project id
        Dim pc As New ProjectsController()
        CreateEntry.Project_Id = pc.GetProjectByName(dde, ProjectName).ProjectId

        'get task id
        Dim tc As New TasksController()
        CreateEntry.Task_Id = tc.GetTaskByName(dde, TaskName).TaskId

        'get user_id
        Dim uc As New UsersController()
        CreateEntry.User_Id = uc.GetUserByName(dde, UserName).UserId
    End Function

    ''' <summary>
    ''' Get the instance of an entry for a given EntryId
    ''' </summary>
    ''' <param name="dde">An Instance of DBDemoEntities byref</param>
    ''' <param name="EntryId">The EntryId of the entry to retrieve</param>
    ''' <returns>Returns an instance of an entry or null</returns>
    Public Function GetEntryById(ByRef dde As DBDemoEntities, ByVal EntryId As Integer) As Entry
        GetEntryById = dde.Entries.Where(Function(x) x.EntryId = EntryId And x.IsDeleted = False).FirstOrDefault
    End Function

    ''' <summary>
    ''' Deletes an entry from database
    ''' </summary>
    ''' <param name="dde">An Instance of DBDemoEntities byref</param>
    ''' <param name="Entry">Instance of an entry to be deleted</param>
    ''' <returns>Returns true if delete was a success</returns>
    Public Function DeleteEntry(ByRef dde As DBDemoEntities, ByRef entry As Entry) As Boolean
        ' save the entry as deleted
        entry.IsDeleted = True
        entry.DeletedDate = DateTime.Now
        DeleteEntry = dde.SaveChanges()
    End Function

    ''' <summary>
    ''' Inserts an entry in database
    ''' </summary>
    ''' <param name="dde">An Instance of DBDemoEntities byref</param>
    ''' <param name="entry">Instance of an entry to be inserted</param>
    ''' <returns>Returns true if insert was a success</returns>
    Public Function InsertNewEntry(ByRef dde As DBDemoEntities, ByRef entry As Entry) As Boolean
        dde.Entries.Add(entry)
        InsertNewEntry = dde.SaveChanges()
    End Function

    ''' <summary>
    ''' Updates an entry in database
    ''' </summary>
    ''' <param name="dde">An Instance of DBDemoEntities byref</param>
    ''' <param name="entry">Instance of an entry to be updated</param>
    ''' <returns>Returns true if update was a success</returns>
    Public Function UpdateEntry(ByRef dde As DBDemoEntities, ByRef entry As Entry) As Boolean
        entry.UpdatedDate = DateTime.Now
        UpdateEntry = dde.SaveChanges()
    End Function

End Class
