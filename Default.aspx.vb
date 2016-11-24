Imports DevExpress.Web

Public Class _Default
    Inherits System.Web.UI.Page
    Dim dde As New DBDemoEntities

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Keep the data from being refetched on callbacks and postbacks
        If Not IsPostBack And Not IsCallback Then
            BindCboUsers()
            gvEntries.DataSource() = FetchGridDataSource()
            gvEntries.DataBind()
            SetGridViewComboBoxes()
        End If
    End Sub

    ''' <summary>
    ''' Fill the grid view's combo boxes with data, and replace Id by Text
    ''' </summary>
    Protected Sub SetGridViewComboBoxes()
        'Set combo values for projet
        Dim cboProject As GridViewDataComboBoxColumn = gvEntries.Columns.Item("Projet")
        Dim projetList As New List(Of Project)
        projetList = dde.Projects.Where(Function(x) x.IsDeleted = 0).ToList()
        cboProject.PropertiesComboBox.DataSource = projetList
        cboProject.PropertiesComboBox.ValueField = "ProjectId"
        cboProject.PropertiesComboBox.TextField = "Name"
        cboProject.PropertiesComboBox.NullText = "Veuillez choisir un projet"

        'Set combo values for tache
        Dim cboTask As GridViewDataComboBoxColumn = gvEntries.Columns.Item("Tâche")
        Dim tacheList As New List(Of Task)
        tacheList = dde.Tasks.Where(Function(x) x.IsDeleted = 0).ToList()
        cboTask.PropertiesComboBox.DataSource = tacheList
        cboTask.PropertiesComboBox.ValueField = "TaskId"
        cboTask.PropertiesComboBox.TextField = "Name"
        cboTask.PropertiesComboBox.NullText = "Veuillez choisir une tache"
    End Sub

    ''' <summary>
    ''' Bind date source to cboUsers
    ''' </summary>
    Protected Sub BindCboUsers()
        Dim UsersList As List(Of User)

        UsersList = dde.Users.Where(Function(x) x.IsDeleted = 0).ToList()

        'Create demo date if database empty
        If UsersList.Count = 0 Then
            DemoHelper.CreateNewData(dde)
            UsersList = dde.Users.Where(Function(x) x.IsDeleted = 0).ToList()
        End If

        'Bind User list to combobox
        cboUsers.DataSource = UsersList.ToArray()
        cboUsers.TextField = "Name"
        cboUsers.ValueField = "UserId"
        cboUsers.DataBind()

    End Sub

    ''' <summary>
    ''' Get datasource for Entries Grid view
    ''' </summary>
    ''' <returns>A list(of entry) to use as data source</returns>
    Protected Function FetchGridDataSource() As List(Of Entry)
        If cboUsers.SelectedItem IsNot Nothing Then
            FetchGridDataSource = dde.Entries.Where(Function(x) x.User_Id = cboUsers.SelectedItem.Value.ToString() And x.IsDeleted = False).ToList()
        End If
    End Function

    Protected Sub cboConsultants_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboUsers.SelectedIndexChanged
        'Hide Grid View CRUD Column if no User is selected
        If cboUsers.SelectedItem IsNot Nothing Then
            gvEntries.Columns(5).Visible = True
        Else
            gvEntries.Columns(5).Visible = False
        End If

        gvEntries.DataBind()
        gvEntries.CancelEdit()
    End Sub

    Protected Sub gvEntrees_InitNewRow(sender As Object, e As DevExpress.Web.Data.ASPxDataInitNewRowEventArgs) Handles gvEntries.InitNewRow
        'Validate that a user is selected
        If cboUsers.SelectedItem Is Nothing Then
            gvEntries.CancelEdit()
            Return
        End If
        SetGridViewComboBoxes()
    End Sub

    Protected Sub gvEntrees_RowUpdating(sender As Object, e As Data.ASPxDataUpdatingEventArgs) Handles gvEntries.RowUpdating
        Dim updatedEntry As New Entry
        Dim ec As New EntriesController()
        updatedEntry = ec.GetEntryById(dde, e.Keys("EntryId"))

        'set new entry properties
        updatedEntry.Project_Id = e.NewValues(0)
        updatedEntry.Task_Id = e.NewValues(1)
        updatedEntry.Description = e.NewValues(2)
        updatedEntry.NbHours = e.NewValues(3)
        updatedEntry.EntryDate = e.NewValues(4)

        'update entry in database
        ec.UpdateEntry(dde, updatedEntry)

        'refresh gridview data
        gvEntries.DataBind()

        'prevent gridview to execute the command
        e.Cancel = True

        'Brings back gridview mode as browsing
        gvEntries.CancelEdit()
    End Sub

    Protected Sub gvEntrees_StartRowEditing(sender As Object, e As Data.ASPxStartRowEditingEventArgs) Handles gvEntries.StartRowEditing
        SetGridViewComboBoxes()
    End Sub

    Protected Sub gvEntries_RowInserting(sender As Object, e As Data.ASPxDataInsertingEventArgs) Handles gvEntries.RowInserting
        Dim newEntry As New Entry
        Dim ec As New EntriesController()

        'set new entry properties
        newEntry.User_Id = cboUsers.SelectedItem.Value.ToString()
        newEntry.Project_Id = e.NewValues(0)
        newEntry.Task_Id = e.NewValues(1)
        newEntry.Description = e.NewValues(2)
        newEntry.NbHours = e.NewValues(3)
        newEntry.EntryDate = e.NewValues(4)

        'add inserted entry in database
        ec.InsertNewEntry(dde, newEntry)

        'refresh gridview data
        gvEntries.DataBind()

        'prevent gridview to execute the command
        e.Cancel = True

        'Brings back gridview mode as browsing
        gvEntries.CancelEdit()
    End Sub

    Protected Sub gvEntries_RowDeleting(sender As Object, e As Data.ASPxDataDeletingEventArgs) Handles gvEntries.RowDeleting
        Dim deletedEntry As New Entry
        Dim ec As New EntriesController()
        deletedEntry = ec.GetEntryById(dde, e.Keys("EntryId"))

        ec.DeleteEntry(dde, deletedEntry)

        'refresh gridview data
        gvEntries.DataBind()

        'prevent gridview to execute the command
        e.Cancel = True

        'Brings back gridview mode as browsing
        gvEntries.CancelEdit()

    End Sub

    Protected Sub gvEntries_DataBinding(sender As Object, e As EventArgs) Handles gvEntries.DataBinding
        gvEntries.DataSource() = FetchGridDataSource()
    End Sub

    Protected Sub lnkCreateNewUser_Click(sender As Object, e As EventArgs) Handles lnkCreateNewUser.Click
        ShowCreateUserPanel(True)
    End Sub

    Protected Sub butConfirmCreateUser_Click(sender As Object, e As EventArgs) Handles btnConfirmCreateUser.Click
        'check if txtUserName is not empty and than create the new user
        If Not String.IsNullOrWhiteSpace(txtUserName.Text) Then
            Dim uc As New UsersController()
            Dim newUser = uc.CreateUser(txtUserName.Text.Trim())

            If uc.InsertUser(dde, newUser) = True Then
                'If insert was a success, we bind cboUser, focus on the newUser and trigger SelectedIndexchangedEvent
                BindCboUsers()
                cboUsers.Items.FindByText(txtUserName.Text.Trim()).Selected = True
                cboConsultants_SelectedIndexChanged(sender, e)

                ShowCreateUserPanel(False)
            Else
                lblUserNameRequired.Text = "*Impossible de créer ce consultant"
                lblUserNameRequired.Visible = True
            End If
        Else
            lblUserNameRequired.Text = "*Nom obligatoire"
            lblUserNameRequired.Visible = True
        End If
    End Sub

    Protected Sub btnCancelCreateUser_Click(sender As Object, e As EventArgs) Handles btnCancelCreateUser.Click
        ShowCreateUserPanel(False)
    End Sub

    Protected Sub txtUserName_TextChanged(sender As Object, e As EventArgs) Handles txtUserName.TextChanged
        If String.IsNullOrWhiteSpace(txtUserName.Text) Then
            btnConfirmCreateUser.Enabled = False
        Else
            btnConfirmCreateUser.Enabled = True
        End If
    End Sub
    ''' <summary>
    ''' Toggle on or off the CreateUserPanel
    ''' </summary>
    ''' <param name="visible">True to show the panel, false to hide it</param>
    Private Sub ShowCreateUserPanel(ByVal visible As Boolean)
        txtUserName.Text = ""
        lblUserNameRequired.Visible = False
        panCreateUser.Visible = visible
    End Sub

    Protected Sub gvTimeSheet_HtmlDataCellPrepared(sender As Object, e As DevExpress.Web.ASPxGridViewTableDataCellEventArgs) Handles gvEntries.HtmlDataCellPrepared
        Dim dde As New DBDemoEntities()

        'Set the texts to the values instead of ids
        Select Case e.DataColumn.Name
            Case "dcProject"
                Dim project As Project = dde.Projects.Find(CType(e.CellValue, Integer))
                e.Cell.Text = project.Name
            Case "dcTask"
                Dim task As Task = dde.Tasks.Find(CType(e.CellValue, Integer))
                e.Cell.Text = task.Name
        End Select
    End Sub
End Class