Public Class UsersController

    ''' <summary>
    ''' Create an instance of a new user
    ''' </summary>
    ''' <param name="Name">Name of user</param>
    ''' <returns>Returns an instance of a user</returns>
    Public Function CreateUser(ByVal Name As String) As User
        'set properties
        CreateUser = New User()
        CreateUser.Name = Name
        CreateUser.CreatedDate = DateTime.Now
    End Function

    ''' <summary>
    ''' Get a user by its name
    ''' </summary>
    ''' <param name="dde">An instance of DBDemoEntites</param>
    ''' <param name="Name">Name of user to retrieve</param>
    ''' <returns>User with the given name</returns>
    Public Function GetUserByName(ByRef dde As DBDemoEntities, ByVal Name As String) As User
        GetUserByName = dde.Users.Where(Function(x) x.Name = Name And x.IsDeleted = False).FirstOrDefault
    End Function

    ''' <summary>
    ''' Insert new user in Database
    ''' </summary>
    ''' <param name="dde">An instance of DBDemoEntites</param>
    ''' <param name="newUser">New User to insert</param>
    ''' <returns>Returns true if insert was a success</returns>
    Public Function InsertUser(ByRef dde As DBDemoEntities, ByRef newUser As User) As Boolean
        dde.Users.Add(newUser)
        InsertUser = dde.SaveChanges()
    End Function
End Class
