Imports System.Text.RegularExpressions

Public Class MainWindow
    Private Structure S_BusinessEntity
        Public CompanyCode As String
        Public Site As String
        Public NameOfSite As String

        Public BE_ValidFrom As String
        Public BE_ValidTo As String
        Public ObjectDescr As String
        Public Street As String
        Public HouseNo As String
        Public Postcode As String
        Public City As String
        Public Country As String

        Public TenancyLaw As String

        Public TermNo As String
        Public OA_ValidFrom As String
        Public BusinessArea As String
        Public Proficenter As String
    End Structure

    Dim BE_Instance As S_BusinessEntity

    ''' <summary>
    ''' Loads the main window initially
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MainWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitControls()

        Proxy = New BusinessEntity.Z_HH_BusinessEntityREFXClient()
        Proxy.ClientCredentials.UserName.UserName = "IDES-032"
        Proxy.ClientCredentials.UserName.Password = "ipofipup"

        TbCompanyCodeList.Text = "1000"
        BtnRefreshList.PerformClick()
    End Sub

    ''' <summary>
    ''' Initializes all controls to default values and lookup bindings
    ''' </summary>
    Private Sub InitControls()
        Dim StreamReader As IO.StreamReader
        Dim Line, Vals() As String

        ' tenancy law
        StreamReader = New IO.StreamReader("res/tenancy_law.txt")
        Dim TenancyLawComboSource As New Dictionary(Of String, String) From {
            {Space(1), Nothing}
        }

        Do
            Line = StreamReader.ReadLine()
            If Line = Nothing Then
                Exit Do
            End If

            Vals = Line.Split(vbTab)
            TenancyLawComboSource.Add(Vals(0), Vals(0) & Space(1) & Vals(1))
        Loop

        StreamReader.Close()
        StreamReader = Nothing
        Line = Nothing
        Vals = Nothing

        CombTenancyLaw.DataSource = New BindingSource(TenancyLawComboSource, Nothing)
        CombTenancyLaw.DisplayMember = "Value"
        CombTenancyLaw.ValueMember = "Key"

        ' country
        StreamReader = New IO.StreamReader("res/country_iso.txt")
        Dim CountryComboSource As New Dictionary(Of String, String) From {
            {Space(1), Nothing}
        }

        Do
            Line = StreamReader.ReadLine()
            If Line = Nothing Then
                Exit Do
            End If

            Vals = Line.Split(vbTab)
            CountryComboSource.Add(Vals(0), Vals(0) & Space(1) & Vals(1))
        Loop

        StreamReader.Close()
        StreamReader = Nothing
        Line = Nothing
        Vals = Nothing

        CombCountry.DataSource = New BindingSource(CountryComboSource, Nothing)
        CombCountry.DisplayMember = "Value"
        CombCountry.ValueMember = "Key"

        'init other fields
        IsEditing = False
        BtnNew.PerformClick()
    End Sub

    ''' <summary>
    ''' Handles the click on the "new" button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnNew_Click(sender As Object, e As EventArgs) Handles BtnNew.Click
        BE_Instance = New S_BusinessEntity

        BtnCreate.Enabled = False
        BtnChange.Enabled = False
        TbCompanyCode.ReadOnly = False
        TbSite.ReadOnly = False
        IsEditing = False

        ' header
        TbCompanyCode.Clear()
        TbSite.Clear()
        TbNameOfSite.Clear()

        ' general data
        TbBusEntityValidFrom.Clear()
        TbBusEntityValidTo.Clear()

        TbObjectDescr.Clear()
        TbStreet.Clear()
        TbHouseno.Clear()
        TbPostcode.Clear()
        TbCity.Clear()
        CombCountry.SelectedIndex = 0

        ' reference factors
        CombTenancyLaw.SelectedIndex = 0

        ' posting paramters
        TbTermOrgAssignmentNumber.Clear()
        TbTermOrgAssignmentValidFrom.Clear()
        TbBusinessArea.Clear()
        TbProfitCenter.Clear()
    End Sub

    ''' <summary>
    ''' Handles the click on the "create" button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnCreate_Click(sender As Object, e As EventArgs) Handles BtnCreate.Click
        If CreateEntity() Then
            BtnRefreshList.PerformClick()
        End If
    End Sub

    ''' <summary>
    ''' Handles the click on the "change" button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnChange_Click(sender As Object, e As EventArgs) Handles BtnChange.Click
        ChangeEntity()
    End Sub

    ''' <summary>
    ''' Handles the click on the "refresh" button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnRefreshList_Click(sender As Object, e As EventArgs) Handles BtnRefreshList.Click
        GetList()
    End Sub

    ''' <summary>
    ''' Fills all fields and creates the site in the SAP system
    ''' </summary>
    ''' <returns>Returns true if successful, false otherwise</returns>
    Private Function CreateEntity() As Boolean
        Dim CreateRequest As New BusinessEntity.BusinessEntityREFXCreate()
        Dim CreateResponse As BusinessEntity.BusinessEntityREFXCreateResponse

        ' struct initialization
        CreateRequest.BusEntity = New BusinessEntity.ReBusEntityDat()
        CreateRequest.ObjectAddress = New BusinessEntity.ReObjAddressDat()

        ' header
        CreateRequest.CompCodeExt = TbCompanyCode.Text
        CreateRequest.BusinessEntityNumberExt = TbSite.Text
        CreateRequest.BusEntity.BusinessEntityText = TbNameOfSite.Text

        ' general data
        If TbBusEntityValidFrom.Text = Nothing Then
            TbBusEntityValidFrom.Text = ConvertDateToSAP(Date.Today)
        ElseIf Not IsValidDate(TbBusEntityValidFrom.Text) Then
            ShowWarningMessage("Ungültiges Datum: " & TbBusEntityValidFrom.Text & " (Gültig ab)")
            Return False
        End If
        CreateRequest.BusEntity.ObjectValidFrom = TbBusEntityValidFrom.Text

        If TbBusEntityValidTo.Text = Nothing Then
            TbBusEntityValidTo.Text = "9999-12-31"
        ElseIf Not IsValidDate(TbBusEntityValidTo.Text) Then
            ShowWarningMessage("Ungültiges Datum: " & TbBusEntityValidTo.Text & " (Gültig bis)")
            Return False
        End If
        CreateRequest.BusEntity.ObjectValidFrom = TbBusEntityValidTo.Text

        CreateRequest.ObjectAddress.AddressText = TbObjectDescr.Text
        CreateRequest.ObjectAddress.StreetLng = TbStreet.Text
        CreateRequest.ObjectAddress.HouseNo = TbHouseno.Text
        CreateRequest.ObjectAddress.PostlCod1 = TbPostcode.Text
        CreateRequest.ObjectAddress.City = TbCity.Text
        Dim kvp = DirectCast(CombCountry.SelectedItem, KeyValuePair(Of String, String))
        If kvp.Value <> Nothing Then
            CreateRequest.ObjectAddress.Country = kvp.Key
        End If

        ' reference factors
        Dim TenancyLawKvp = DirectCast(CombTenancyLaw.SelectedItem, KeyValuePair(Of String, String))
        If TenancyLawKvp.Value <> Nothing Then
            CreateRequest.BusEntity.TenancyLaw = TenancyLawKvp.Key
        End If

        ' posting parameters
        If Not TbTermOrgAssignmentValidFrom.Text = Nothing Then
            If Not IsValidDate(TbTermOrgAssignmentValidFrom.Text) Then
                ShowWarningMessage("Ungültiges Datum: " & TbTermOrgAssignmentValidFrom.Text & " (Gültig ab)")
                Return False
            End If
            Dim TermOaDat As New BusinessEntity.ReTermOaDat With {
                .TermNo = TbTermOrgAssignmentNumber.Text, ' can be empty (default value)
                .ValidFrom = TbTermOrgAssignmentValidFrom.Text,
                .BusArea = TbBusinessArea.Text,
                .ProfitCtr = TbProfitCenter.Text
            }
            CreateRequest.TermOrgAssignment = {TermOaDat}
        End If

        ' create entity
        Try
            CreateResponse = Proxy.BusinessEntityREFXCreate(CreateRequest)
        Catch Ex As System.ServiceModel.Security.MessageSecurityException
            ShowSecurityErrorMessage()
            Return False
        Catch Ex As TimeoutException
            ShowTimeoutErrorMessage()
            Return False
        Catch Ex As Exception
            ShowExceptionMessage(Ex)
            Return False
        End Try

        If Not CheckForErrors(CreateResponse.Return) Then
            CommitWork(False)
            Return True
        Else
            RollbackWork()
            Return False
        End If
    End Function

    ''' <summary>
    ''' Checks for changes, fills all necessary fields and changes the site in the SAP system
    ''' </summary>
    Private Sub ChangeEntity()
        Dim ChangeRequest As New BusinessEntity.BusinessEntityREFXChange()
        Dim ChangeResponse As BusinessEntity.BusinessEntityREFXChangeResponse

        ChangeRequest.CompCode = TbCompanyCode.Text
        ChangeRequest.BusinessEntityNumber = TbSite.Text

        ' struct initialization
        ChangeRequest.BusEntity = New BusinessEntity.ReBusEntityDat()
        ChangeRequest.ObjectAddress = New BusinessEntity.ReObjAddressDat()

        ChangeRequest.BusEntityX = New BusinessEntity.ReBusEntityDatx()
        ChangeRequest.ObjectAddressX = New BusinessEntity.ReObjAddressDatx()

        ' header
        If Not TbNameOfSite.Text.Equals(BE_Instance.NameOfSite) Then
            ChangeRequest.BusEntity.BusinessEntityText = TbNameOfSite.Text
            ChangeRequest.BusEntityX.BusinessEntityText = "X"
        End If

        ' general data
        If TbBusEntityValidFrom.Text = Nothing Then
            TbBusEntityValidFrom.Text = ConvertDateToSAP(Date.Today)
        ElseIf Not IsValidDate(TbBusEntityValidFrom.Text) Then
            ShowWarningMessage("Ungültiges Datum: " & TbBusEntityValidFrom.Text & " (Gültig ab)")
            Return
        End If
        If Not TbBusEntityValidFrom.Text.Equals(BE_Instance.BE_ValidFrom) Then
            ChangeRequest.BusEntity.ObjectValidFrom = TbBusEntityValidFrom.Text
            ChangeRequest.BusEntityX.ObjectValidFrom = "X"
        End If

        If TbBusEntityValidTo.Text = Nothing Then
            TbBusEntityValidTo.Text = "9999-12-31"
        ElseIf Not IsValidDate(TbBusEntityValidTo.Text) Then
            ShowWarningMessage("Ungültiges Datum: " & TbBusEntityValidTo.Text & " (Gültig bis)")
            Return
        End If
        If Not TbBusEntityValidTo.Text.Equals(BE_Instance.BE_ValidTo) Then
            ChangeRequest.BusEntity.ObjectValidFrom = TbBusEntityValidTo.Text
            ChangeRequest.BusEntityX.ObjectValidTo = "X"
        End If

        If Not TbObjectDescr.Text.Equals(BE_Instance.ObjectDescr) Then
            ChangeRequest.ObjectAddress.AddressText = TbObjectDescr.Text
            ChangeRequest.ObjectAddressX.AddressText = "X"
        End If
        If Not TbStreet.Text.Equals(BE_Instance.Street) Then
            ChangeRequest.ObjectAddress.StreetLng = TbStreet.Text
            ChangeRequest.ObjectAddressX.StreetLng = "X"
        End If
        If Not TbHouseno.Text.Equals(BE_Instance.HouseNo) Then
            ChangeRequest.ObjectAddress.HouseNo = TbHouseno.Text
            ChangeRequest.ObjectAddressX.HouseNo = "X"
        End If
        If Not TbPostcode.Text.Equals(BE_Instance.Postcode) Then
            ChangeRequest.ObjectAddress.PostlCod1 = TbPostcode.Text
            ChangeRequest.ObjectAddressX.PostlCod1 = "X"
        End If
        If Not TbCity.Text.Equals(BE_Instance.City) Then
            ChangeRequest.ObjectAddress.City = TbCity.Text
            ChangeRequest.ObjectAddressX.City = "X"
        End If
        If Not CombCountry.SelectedValue = Nothing Then
            If Not CombCountry.SelectedValue.Equals(BE_Instance.Country) Then
                ChangeRequest.ObjectAddress.Country = CombCountry.SelectedValue
                ChangeRequest.ObjectAddressX.Country = "X"
            End If
        End If

        ' reference factors
        If Not CombTenancyLaw.SelectedValue = Nothing Then
            If Not CombTenancyLaw.SelectedValue.Equals(BE_Instance.TenancyLaw) Then
                ChangeRequest.BusEntity.TenancyLaw = CombTenancyLaw.SelectedValue
                ChangeRequest.BusEntityX.TenancyLaw = "X"
            End If
        End If

        ' posting parameters
        ' TODO rethink this again
        If Not TbTermOrgAssignmentNumber.Text.Equals(BE_Instance.TermNo) Or Not TbTermOrgAssignmentValidFrom.Text.Equals(BE_Instance.OA_ValidFrom) Or Not TbBusinessArea.Text.Equals(BE_Instance.BusinessArea) Or Not TbProfitCenter.Text.Equals(BE_Instance.Proficenter) Then
            If Not IsValidDate(TbTermOrgAssignmentValidFrom.Text) Then
                MessageBox.Show("Ungültiges Datum: " & TbTermOrgAssignmentValidFrom.Text & " (Gültig ab)", "Warnung", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' TODO make insert option possible
            Dim TermOrgAssignment As New BusinessEntity.ReTermOaDatc With {
                .ChangeIndicator = "U", ' possible indicators are ' ' for ignore, 'I' for insert, 'U' for update, 'D' for delete
                .TermNo = TbTermOrgAssignmentNumber.Text, ' mandatory for identification
                .ValidFrom = TbTermOrgAssignmentValidFrom.Text, ' mandatory for identification
                .BusArea = TbBusinessArea.Text,
                .ProfitCtr = TbProfitCenter.Text
            }
            ChangeRequest.TermOrgAssignment = {TermOrgAssignment}
        End If

        ' change the site
        Try
            ChangeResponse = Proxy.BusinessEntityREFXChange(ChangeRequest)
        Catch Ex As System.ServiceModel.Security.MessageSecurityException
            ShowSecurityErrorMessage()
            Return
        Catch Ex As TimeoutException
            ShowTimeoutErrorMessage()
            Return
        Catch Ex As Exception
            ShowExceptionMessage(Ex)
            Return
        End Try

        If Not CheckForErrors(ChangeResponse.Return) Then
            CommitWork(False)
        Else
            RollbackWork()
        End If
    End Sub

    ''' <summary>
    ''' Queries the details of the site
    ''' </summary>
    ''' <param name="CompanyCode">Company code of the site</param>
    ''' <param name="BusinessEntityNumber">The number of the site</param>
    Private Sub GetDetail(CompanyCode As String, BusinessEntityNumber As String)
        BtnNew.PerformClick()

        Dim DetailRequest As New BusinessEntity.BusinessEntityREFXGetDetail()
        Dim DetailResponse As BusinessEntity.BusinessEntityREFXGetDetailResponse

        DetailRequest.CompCode = CompanyCode
        DetailRequest.BusinessEntityNumber = BusinessEntityNumber

        Try
            DetailResponse = Proxy.BusinessEntityREFXGetDetail(DetailRequest)
        Catch Ex As System.ServiceModel.Security.MessageSecurityException
            ShowSecurityErrorMessage()
            Return
        Catch Ex As TimeoutException
            ShowTimeoutErrorMessage()
            Return
        Catch Ex As Exception
            ShowExceptionMessage(Ex)
            Return
        End Try

        ' restrict functionality so the user actually only changes the selected site
        BtnCreate.Enabled = False
        BtnChange.Enabled = True
        TbCompanyCode.ReadOnly = True
        TbSite.ReadOnly = True
        IsEditing = True

        ' header
        TbCompanyCode.Text = DetailResponse.BusEntity.CompCode
        TbSite.Text = DetailResponse.BusEntity.BusinessEntity
        TbNameOfSite.Text = DetailResponse.BusEntity.BusinessEntityText

        BE_Instance.CompanyCode = TbCompanyCode.Text
        BE_Instance.Site = TbSite.Text
        BE_Instance.NameOfSite = TbNameOfSite.Text

        ' general data
        TbBusEntityValidFrom.Text = DetailResponse.BusEntity.ObjectValidFrom
        TbBusEntityValidTo.Text = DetailResponse.BusEntity.ObjectValidTo

        TbObjectDescr.Text = DetailResponse.ObjectAddress.AddressText
        TbStreet.Text = DetailResponse.ObjectAddress.StreetLng
        TbHouseno.Text = DetailResponse.ObjectAddress.HouseNo
        TbPostcode.Text = DetailResponse.ObjectAddress.PostlCod1
        TbCity.Text = DetailResponse.ObjectAddress.City
        CombCountry.SelectedValue = DetailResponse.ObjectAddress.Country

        BE_Instance.BE_ValidFrom = TbBusEntityValidFrom.Text
        BE_Instance.BE_ValidTo = TbBusEntityValidTo.Text
        BE_Instance.ObjectDescr = TbObjectDescr.Text
        BE_Instance.Street = TbStreet.Text
        BE_Instance.HouseNo = TbHouseno.Text
        BE_Instance.Postcode = TbPostcode.Text
        BE_Instance.City = TbCity.Text
        BE_Instance.Country = CombCountry.SelectedValue

        ' reference factors
        CombTenancyLaw.SelectedValue = DetailResponse.BusEntity.TenancyLaw

        BE_Instance.TenancyLaw = CombTenancyLaw.SelectedValue

        ' posting parameters
        ' assuming there is only one posting parameter
        Dim TermOrgAssignment = DetailResponse.TermOrgAssignment.ElementAt(0)
        TbTermOrgAssignmentNumber.Text = TermOrgAssignment.TermNo
        TbTermOrgAssignmentValidFrom.Text = TermOrgAssignment.ValidFrom
        TbBusinessArea.Text = TermOrgAssignment.BusArea
        TbProfitCenter.Text = TermOrgAssignment.ProfitCtr

        BE_Instance.TermNo = TbTermOrgAssignmentNumber.Text
        BE_Instance.OA_ValidFrom = TbTermOrgAssignmentValidFrom.Text
        BE_Instance.BusinessArea = TbBusinessArea.Text
        BE_Instance.Proficenter = TbProfitCenter.Text
    End Sub

    ''' <summary>
    ''' Queries all sites in the SAP system and display them in a list
    ''' </summary>
    Private Sub GetList()
        Dim ListRequest As New BusinessEntity.BusinessEntityREFXGetList()
        Dim ListResponse As BusinessEntity.BusinessEntityREFXGetListResponse

        Dim Seloption1 As New BusinessEntity.ReSeloption With {
            .FieldName = "COMP_CODE",
            .Sign = "I",
            .Option = "EQ",
            .FieldValueLow = TbCompanyCodeList.Text
        }

        Dim ReSeloption = {Seloption1}
        ListRequest.Seloption = ReSeloption

        DgvItems.Rows.Clear()
        Try
            ListResponse = Proxy.BusinessEntityREFXGetList(ListRequest)
        Catch Ex As System.ServiceModel.Security.MessageSecurityException
            ShowSecurityErrorMessage()
            Return
        Catch Ex As TimeoutException
            ShowTimeoutErrorMessage()
            Return
        Catch Ex As Exception
            ShowExceptionMessage(Ex)
            Return
        End Try

        If Not CheckForErrors(ListResponse.Return) Then
            For Each BusEntity As BusinessEntity.ReBusEntity In ListResponse.BusEntity
                DgvItems.Rows.Add({BusEntity.CompCode, BusEntity.BusinessEntity, BusEntity.BusinessEntityText})
            Next
        Else
            ShowErrorMessage("Wirtschaftseinheiten des Buchungskreises " & TbCompanyCodeList.Text & " konnten nicht abgefragt werden")
        End If
    End Sub

    ''' <summary>
    ''' Checks whether all "basic" mandatory fields have been filled
    ''' </summary>
    ''' <returns>True if all mandatory fields are filled, false otherwise</returns>
    Private Function MandatoryFieldsFilled() As Boolean
        Dim FieldsFilled As Boolean
        FieldsFilled = TbCompanyCode.Text.Length > 0 And TbSite.Text.Length > 0 And TbNameOfSite.Text.Length > 0 And CombTenancyLaw.SelectedIndex <> 0

        If FieldsFilled Then
            If IsEditing Then
                BtnCreate.Enabled = False
                BtnChange.Enabled = True
            Else
                BtnCreate.Enabled = True
                BtnChange.Enabled = False
            End If
        Else
            BtnCreate.Enabled = False
            BtnChange.Enabled = False
        End If

        Return FieldsFilled
    End Function

    ''' <summary>
    ''' Checks if <paramref name="ReturnList"/> has any error messages in it.
    ''' </summary>
    ''' <param name="ReturnList">List of return messages</param>
    ''' <returns>true if an error occurred, false otherwise</returns>
    Private Function CheckForErrors(ByRef ReturnList As BusinessEntity.Bapiret2()) As Boolean
        Dim Message As String = ""
        Dim ErrorOccured As Boolean = False

        For Each ReturnElement As BusinessEntity.Bapiret2 In ReturnList
            If ReturnElement.Type = "S" Then
                Message = Message & "* Erfolg: " & ReturnElement.Message & Environment.NewLine
            ElseIf ReturnElement.Type = "I" Then
                Message = Message & "* Info: " & ReturnElement.Message & Environment.NewLine
            ElseIf ReturnElement.Type = "W" Then
                Message = Message & "* Warnung: " & ReturnElement.Message & Environment.NewLine
            ElseIf ReturnElement.Type = "E" Then
                Message = Message & "* Fehler: " & ReturnElement.Message & Environment.NewLine
                ErrorOccured = True
            ElseIf ReturnElement.Type = "A" Then
                Message = Message & "* Abbruch: " & ReturnElement.Message & Environment.NewLine
                ErrorOccured = True
            End If
        Next

        If Message <> Nothing Then
            Dim Icon As MessageBoxIcon
            If ErrorOccured Then
                Icon = MessageBoxIcon.Error
            Else
                Icon = MessageBoxIcon.Information
            End If
            ShowCustomMessage(Message, "Meldungen zur Wirtschaftseinheit", MessageBoxButtons.OK, Icon)
        End If

        Return ErrorOccured
    End Function

    ''' <summary>
    ''' Submits a commit
    ''' </summary>
    Private Sub CommitWork(ByVal DoWait As Boolean)
        Dim CommitRequest As New BusinessEntity.BapiServiceTransactionCommit()
        Dim CommitResponse As BusinessEntity.BapiServiceTransactionCommitResponse

        If DoWait Then
            CommitRequest.WAIT = "X"
        Else
            CommitRequest.WAIT = Nothing
        End If
        CommitResponse = Proxy.BapiServiceTransactionCommit(CommitRequest)

        ' return messages are only received when a commit and wait is executed
        If DoWait Then
            If CheckForErrors({CommitResponse.Return}) Then
                RollbackWork()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Submits a rollback
    ''' </summary>
    Private Sub RollbackWork()
        Dim RollbackRequest As New BusinessEntity.BapiServiceTransactionRollback()

        ' no return messages are received in case of an error here
        Proxy.BapiServiceTransactionRollback(RollbackRequest)
    End Sub

    ''' <summary>
    ''' Converts the SAP date into a date valid for VB, respecting the bounds
    ''' </summary>
    ''' <param name="Value">The original date</param>
    ''' <param name="MinDate">Minimum value (lower bound) of the date</param>
    ''' <param name="MaxDate">Maximum value (upper bound) of the date</param>
    ''' <returns>The converted date</returns>
    Private Function ConvertDateToInternal(ByVal Value As String, ByVal MinDate As Date, ByVal MaxDate As Date) As Date
        Dim DateSAP As String
        Dim ConvertedDate As Date

        DateSAP = Value

        ' year
        If Value.Substring(0, 4) = "0000" Then
            Value = "0001" & Value.Substring(4)
        End If
        ' month
        If Value.Substring(5, 2) = "00" Then
            Value = Value.Substring(0, 5) & "01" & Value.Substring(7)
        End If
        ' day
        If Value.Substring(8, 2) = "00" Then
            Value = Value.Substring(0, 8) & "01"
        End If

        ConvertedDate = New Date(Value.Substring(0, 4), Value.Substring(5, 2), Value.Substring(8, 2))

        ' check bounds
        If ConvertedDate < MinDate Then
            ConvertedDate = MinDate
        ElseIf ConvertedDate > MaxDate Then
            ConvertedDate = MaxDate
        End If

        Return ConvertedDate
    End Function

    Private Function ConvertDateToSAP(ByVal Value As String) As String
        Dim ConvertedDate As String

        ' format: 9999-12-31
        ConvertedDate = Value.Substring(6, 4) & "-" & Value.Substring(3, 2) & "-" & Value.Substring(0, 2)

        Return ConvertedDate
    End Function

    Private Function IsValidDate(ByVal Value As String) As Boolean
        ' valid dates have the format yyyy-mm-dd
        Return Regex.IsMatch(Value, "(\d{4})\-(0[1-9]|1[012])\-(0[1-9]|1[0-9]|2[0-9]|3[01])")
    End Function

    Private Sub OnKeyUpInTbCompanyCodeList(sender As Object, e As KeyEventArgs) Handles TbCompanyCodeList.KeyUp
        If e.KeyCode = Keys.Enter Then
            GetList()
        End If
    End Sub

    Private Sub OnSelectedItemChangedInDgvItems(sender As Object, e As DataGridViewCellEventArgs) Handles DgvItems.RowEnter
        Dim CompanyCode As Object = DgvItems.Item("BE_CompanyCode", e.RowIndex).Value
        Dim BusinessEntityNumber As Object = DgvItems.Item("BE_Number", e.RowIndex).Value

        GetDetail(CompanyCode, BusinessEntityNumber)

        BtnChange.Enabled = True
    End Sub

    Private Sub OnKeyUpInTbCompanyCode(sender As Object, e As KeyEventArgs) Handles TbCompanyCode.KeyUp
        MandatoryFieldsFilled()
    End Sub

    Private Sub OnKeyUpInTbSite(sender As Object, e As KeyEventArgs) Handles TbSite.KeyUp
        MandatoryFieldsFilled()
    End Sub

    Private Sub OnKeyUpInTbNameOfSite(sender As Object, e As KeyEventArgs) Handles TbNameOfSite.KeyUp
        MandatoryFieldsFilled()
    End Sub

    Private Sub OnSelectedIndexChangeInCombTenancyLaw(sender As Object, e As EventArgs) Handles CombTenancyLaw.SelectedIndexChanged
        MandatoryFieldsFilled()
    End Sub

    ''' <summary>
    ''' Shows information about the mandatory fields to be filled
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnHelp_Click(sender As Object, e As EventArgs) Handles BtnHelp.Click
        ShowHelpMessage()
    End Sub
End Class
