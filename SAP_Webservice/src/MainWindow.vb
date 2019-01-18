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

        Public TermOrgAsssignment_SelectedKey As Integer
        'Public TermOrgAssignment As Dictionary(Of Integer, S_TermOrgAssignment)
        Public TermOrgAssignment As List(Of S_TermOrgAssignment)
    End Structure

    Private Structure S_TermOrgAssignment
        Dim TermNo As String
        Dim TermText As String
        Dim ValidFrom As String
        Dim ValidTo As String
        Dim BusinessArea As String
        Dim Profitcenter As String
        Dim TaxJurcode As String
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

        TbCompanyCode.ReadOnly = False
        TbSite.ReadOnly = False

        BtnCreate.Enabled = False
        BtnChange.Enabled = False

        TbTermOrgAssignmentValidFrom.ReadOnly = False
        BtnPreviousTerm.Enabled = False
        BtnNextTerm.Enabled = False
        LblActiveTerm.Visible = False

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
        TbTermOrgAssignmentText.Clear()
        TbTermOrgAssignmentValidFrom.Clear()
        TbTermOrgAssignmentValidTo.Clear()
        TbBusinessArea.Clear()
        TbProfitCenter.Clear()
        TbTaxJurisd.Clear()
    End Sub

    ''' <summary>
    ''' Handles the click on the "create" button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnCreate_Click(sender As Object, e As EventArgs) Handles BtnCreate.Click
        If CreateEntity() Then
            BtnNew.PerformClick()
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
        Dim KVP = DirectCast(CombCountry.SelectedItem, KeyValuePair(Of String, String))
        If KVP.Value <> Nothing Then
            CreateRequest.ObjectAddress.Country = KVP.Key
        End If

        ' reference factors
        Dim TenancyLawKvp = DirectCast(CombTenancyLaw.SelectedItem, KeyValuePair(Of String, String))
        If TenancyLawKvp.Value <> Nothing Then
            CreateRequest.BusEntity.TenancyLaw = TenancyLawKvp.Key
        End If

        ' posting parameters
        If TbTermOrgAssignmentValidFrom.Text = Nothing Then
            TbTermOrgAssignmentValidFrom.Text = "0000-00-00"
        End If
        If Not IsValidDate(TbTermOrgAssignmentValidFrom.Text) Then
            ShowWarningMessage("Ungültiges Datum: " & TbTermOrgAssignmentValidFrom.Text & " (Gültig von)")
            Return False
        End If

        If TbTermOrgAssignmentValidTo.Text = Nothing Then
            TbTermOrgAssignmentValidTo.Text = "9999-12-31"
        End If

        Dim TermOaDat As New BusinessEntity.ReTermOaDat With {
            .TermNo = TbTermOrgAssignmentNumber.Text, ' can be empty (default value)
            .TermText = TbTermOrgAssignmentText.Text,
            .ValidFrom = TbTermOrgAssignmentValidFrom.Text,
            .ValidTo = TbTermOrgAssignmentValidTo.Text,
            .BusArea = TbBusinessArea.Text,
            .ProfitCtr = TbProfitCenter.Text,
            .Taxjurcode = TbTaxJurisd.Text
        }
        If TbTermOrgAssignmentValidFrom.Text > "0000-00-00" Then
            Dim TermOaDat1 As New BusinessEntity.ReTermOaDat With {
                .TermNo = TbTermOrgAssignmentNumber.Text, ' can be empty (default value)
                .TermText = TbTermOrgAssignmentText.Text,
                .ValidFrom = "0000-00-00",
                .ValidTo = SubtractOneFromDate(TbTermOrgAssignmentValidFrom.Text),
                .BusArea = TbBusinessArea.Text,
                .ProfitCtr = TbProfitCenter.Text,
                .Taxjurcode = TbTaxJurisd.Text
            }

            CreateRequest.TermOrgAssignment = {TermOaDat1, TermOaDat}
        Else
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
            If CommitWork(True) Then
                Return True
            Else
                Return False
            End If
        Else
            RollbackWork()
            Return False
        End If
    End Function

    ''' <summary>
    ''' Checks for changes, fills all necessary fields and changes the site in the SAP system
    ''' </summary>
    Private Function ChangeEntity() As Boolean
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
            Return False
        End If
        If Not TbBusEntityValidFrom.Text.Equals(BE_Instance.BE_ValidFrom) Then
            ChangeRequest.BusEntity.ObjectValidFrom = TbBusEntityValidFrom.Text
            ChangeRequest.BusEntityX.ObjectValidFrom = "X"
        End If

        If TbBusEntityValidTo.Text = Nothing Then
            TbBusEntityValidTo.Text = "9999-12-31"
        ElseIf Not IsValidDate(TbBusEntityValidTo.Text) Then
            ShowWarningMessage("Ungültiges Datum: " & TbBusEntityValidTo.Text & " (Gültig bis)")
            Return False
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
        Dim TermOrgAssignments(BE_Instance.TermOrgAssignment.Count - 1) As BusinessEntity.ReTermOaDatc
        For Index = 0 To BE_Instance.TermOrgAssignment.Count - 1
            Dim TermOA As S_TermOrgAssignment = BE_Instance.TermOrgAssignment.ElementAt(Index)

            TermOrgAssignments(Index) = New BusinessEntity.ReTermOaDatc()

            ' possible indicators are ' ' for ignore, 'I' for insert, 'U' for update, 'D' for delete
            If Not TbTermOrgAssignmentText.Text.Equals(TermOA.TermText) Or Not TbBusinessArea.Text.Equals(TermOA.BusinessArea) Or Not TbProfitCenter.Text.Equals(TermOA.Profitcenter) Or Not TbTaxJurisd.Text.Equals(TermOA.TaxJurcode) Then
                TermOrgAssignments(Index).ChangeIndicator = "U"
            Else
                TermOrgAssignments(Index).ChangeIndicator = " "
            End If

            TermOrgAssignments(Index).TermNo = TermOA.TermNo
            TermOrgAssignments(Index).TermText = TermOA.TermText
            TermOrgAssignments(Index).ValidFrom = TermOA.ValidFrom
            TermOrgAssignments(Index).ValidTo = TermOA.ValidTo
            TermOrgAssignments(Index).BusArea = TermOA.BusinessArea
            TermOrgAssignments(Index).ProfitCtr = TermOA.Profitcenter
            TermOrgAssignments(Index).Taxjurcode = TermOA.TaxJurcode
        Next

        ChangeRequest.TermOrgAssignment = TermOrgAssignments

        ' change the site
        Try
            ChangeResponse = Proxy.BusinessEntityREFXChange(ChangeRequest)
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

        If Not CheckForErrors(ChangeResponse.Return) Then
            If CommitWork(False) Then
                Return True
            Else
                Return False
            End If
        Else
            RollbackWork()
            Return False
        End If
    End Function

    ''' <summary>
    ''' Queries the details of the site
    ''' </summary>
    ''' <param name="CompanyCode">Company code of the site</param>
    ''' <param name="BusinessEntityNumber">The number of the site</param>
    Private Sub GetDetail(CompanyCode As String, BusinessEntityNumber As String)
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
        TbTermOrgAssignmentValidFrom.ReadOnly = True
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
        BE_Instance.TermOrgAssignment = New List(Of S_TermOrgAssignment)
        Dim Assignments As New Dictionary(Of Integer, S_TermOrgAssignment)

        Dim LastElementIndex As Integer = DetailResponse.TermOrgAssignment.Length - 1
        Dim ActiveTermIndex As Integer = LastElementIndex
        For Index As Integer = 0 To LastElementIndex
            Dim TermOA As BusinessEntity.ReTermOa = DetailResponse.TermOrgAssignment.ElementAt(Index)

            Dim Assignment As New S_TermOrgAssignment With {
                .TermNo = TermOA.TermNo,
                .TermText = TermOA.TermText,
                .ValidFrom = TermOA.ValidFrom,
                .ValidTo = TermOA.ValidTo,
                .BusinessArea = TermOA.BusArea,
                .Profitcenter = TermOA.ProfitCtr,
                .TaxJurcode = TermOA.Taxjurcode
            }

            ' find active term org assignment
            If DateLiesInRange(ConvertDateToSAP(Date.Today), Assignment.ValidFrom, Assignment.ValidTo) Then
                LblActiveTerm.Visible = True
                ActiveTermIndex = Index
                BE_Instance.TermOrgAsssignment_SelectedKey = ActiveTermIndex
            End If

            Assignments.Add(Index, Assignment)
            BE_Instance.TermOrgAssignment.Add(Assignment)
        Next

        ' set buttons to switch between term org assignments
        If LastElementIndex > 0 Then
            If ActiveTermIndex = 0 Then
                BtnNextTerm.Enabled = True
            ElseIf ActiveTermIndex = LastElementIndex Then
                BtnPreviousTerm.Enabled = True
            Else
                BtnPreviousTerm.Enabled = True
                BtnNextTerm.Enabled = True
            End If
        End If

        Dim ActiveTerm As S_TermOrgAssignment = Assignments.ElementAt(ActiveTermIndex).Value
        SetTermOrgAssignmentFields(ActiveTerm)
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
    Private Function CommitWork(ByVal DoWait As Boolean) As Boolean
        Dim CommitRequest As New BusinessEntity.BapiServiceTransactionCommit()
        Dim CommitResponse As BusinessEntity.BapiServiceTransactionCommitResponse

        If DoWait Then
            CommitRequest.WAIT = "X"
        Else
            CommitRequest.WAIT = Nothing
        End If

        Try
            CommitResponse = Proxy.BapiServiceTransactionCommit(CommitRequest)
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

        ' return messages are only received when a commit and wait is executed
        If DoWait Then
            If CheckForErrors({CommitResponse.Return}) Then
                RollbackWork()
                Return False
            Else
                Return True
            End If
        Else
            Return True
        End If
    End Function

    ''' <summary>
    ''' Submits a rollback
    ''' </summary>
    Private Function RollbackWork() As Boolean
        Dim RollbackRequest As New BusinessEntity.BapiServiceTransactionRollback()

        ' no return messages are received in case of an error here
        Try
            Proxy.BapiServiceTransactionRollback(RollbackRequest)
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

        Return True
    End Function

    ''' <summary>
    ''' Converts the SAP date into a date valid for VB, respecting the bounds
    ''' </summary>
    ''' <param name="Value">The original date</param>
    ''' <param name="Low">Minimum value (lower bound) of the date</param>
    ''' <param name="High">Maximum value (upper bound) of the date</param>
    ''' <returns>The converted date</returns>
    Private Function ConvertDateToInternal(ByVal Value As String, ByVal Low As String, ByVal High As String) As Date
        Dim DateSAP As String
        Dim ConvertedDate As Date
        Dim MinDate As Date
        Dim MaxDate As Date

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
        MinDate = New Date(Low.Substring(0, 4), Low.Substring(5, 2), Low.Substring(8, 2))
        MaxDate = New Date(High.Substring(0, 4), High.Substring(5, 2), High.Substring(8, 2))

        ' check bounds
        If ConvertedDate < MinDate Then
            ConvertedDate = MinDate
        ElseIf ConvertedDate > MaxDate Then
            ConvertedDate = MaxDate
        End If

        Return ConvertedDate
    End Function

    ''' <summary>
    ''' Converts dates of format DD.MM.YYYY into format YYYY-MM-DD
    ''' </summary>
    ''' <param name="Value">The date to convert</param>
    ''' <returns>Returns the converted date</returns>
    Private Function ConvertDateToSAP(ByVal Value As String) As String
        Dim ConvertedDate As String

        ' format: 9999-12-31
        ConvertedDate = Value.Substring(6, 4) & "-" & Value.Substring(3, 2) & "-" & Value.Substring(0, 2)

        Return ConvertedDate
    End Function

    ''' <summary>
    ''' Checks whether the passed date has a valid format or not
    ''' </summary>
    ''' <param name="Value">The date to check</param>
    ''' <returns>Returns true if the date is valid, false otherwise</returns>
    Private Function IsValidDate(ByVal Value As String) As Boolean
        ' valid dates have the format yyyy-mm-dd
        Return Regex.IsMatch(Value, "(\d{4})\-(0[0-9]|1[012])\-(0[0-9]|1[0-9]|2[0-9]|3[01])")
    End Function

    Private Sub OnKeyUpInTbCompanyCodeList(sender As Object, e As KeyEventArgs) Handles TbCompanyCodeList.KeyUp
        If e.KeyCode = Keys.Enter Then
            GetList()
        End If
    End Sub

    Private Sub OnSelectedItemChangedInDgvItems(sender As Object, e As DataGridViewCellEventArgs) Handles DgvItems.RowEnter
        Dim CompanyCode As Object = DgvItems.Item("BE_CompanyCode", e.RowIndex).Value
        Dim BusinessEntityNumber As Object = DgvItems.Item("BE_Number", e.RowIndex).Value

        BtnNew.PerformClick()

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

    Private Sub BtnHelp_Click(sender As Object, e As EventArgs) Handles BtnHelp.Click
        ShowHelpMessage()
    End Sub

    Private Sub BtnPreviousTerm_Click(sender As Object, e As EventArgs) Handles BtnPreviousTerm.Click
        BE_Instance.TermOrgAsssignment_SelectedKey = BE_Instance.TermOrgAsssignment_SelectedKey - 1

        BtnNextTerm.Enabled = True

        If BE_Instance.TermOrgAsssignment_SelectedKey = 0 Then
            BtnPreviousTerm.Enabled = False
        End If

        Dim CurrentTerm As S_TermOrgAssignment = BE_Instance.TermOrgAssignment.ElementAt(BE_Instance.TermOrgAsssignment_SelectedKey)
        SetTermOrgAssignmentFields(CurrentTerm)

        If DateLiesInRange(ConvertDateToSAP(Date.Today), CurrentTerm.ValidFrom, CurrentTerm.ValidTo) Then
            LblActiveTerm.Visible = True
        Else
            LblActiveTerm.Visible = False
        End If
    End Sub

    Private Sub BtnNextTerm_Click(sender As Object, e As EventArgs) Handles BtnNextTerm.Click
        BE_Instance.TermOrgAsssignment_SelectedKey = BE_Instance.TermOrgAsssignment_SelectedKey + 1

        BtnPreviousTerm.Enabled = True

        If BE_Instance.TermOrgAssignment.Count - 1 = BE_Instance.TermOrgAsssignment_SelectedKey Then
            BtnNextTerm.Enabled = False
        End If

        Dim CurrentTerm As S_TermOrgAssignment = BE_Instance.TermOrgAssignment.ElementAt(BE_Instance.TermOrgAsssignment_SelectedKey)
        SetTermOrgAssignmentFields(CurrentTerm)

        If DateLiesInRange(ConvertDateToSAP(Date.Today), CurrentTerm.ValidFrom, CurrentTerm.ValidTo) Then
            LblActiveTerm.Visible = True
        Else
            LblActiveTerm.Visible = False
        End If
    End Sub

    ''' <summary>
    ''' Sets all term org assignments fields
    ''' </summary>
    ''' <param name="CurrentTerm">The values to set</param>
    Private Sub SetTermOrgAssignmentFields(ByRef CurrentTerm As S_TermOrgAssignment)
        TbTermOrgAssignmentNumber.Text = CurrentTerm.TermNo
        TbTermOrgAssignmentText.Text = CurrentTerm.TermText
        TbTermOrgAssignmentValidFrom.Text = CurrentTerm.ValidFrom
        TbTermOrgAssignmentValidTo.Text = CurrentTerm.ValidTo
        TbBusinessArea.Text = CurrentTerm.BusinessArea
        TbProfitCenter.Text = CurrentTerm.Profitcenter
        TbTaxJurisd.Text = CurrentTerm.TaxJurcode
    End Sub

    ''' <summary>
    ''' Checks whether the passed date lies within the range passed
    ''' </summary>
    ''' <param name="DateToCheck">The date to check</param>
    ''' <param name="Low">Lower bound of the range</param>
    ''' <param name="High">Upper bound of the range</param>
    ''' <returns>Returns true if the date is in the range, false otherwise</returns>
    Private Function DateLiesInRange(DateToCheck As String, Low As String, High As String) As Boolean
        If Low <= DateToCheck And High >= DateToCheck Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Subtract one day from the date passed
    ''' </summary>
    ''' <param name="DateToSubtractFrom">The date to subtract one day from</param>
    ''' <returns>Returns the result of the calculation</returns>
    Private Function SubtractOneFromDate(DateToSubtractFrom As String) As String
        Dim InternalDate As Date = ConvertDateToInternal(DateToSubtractFrom, "0001-01-01", "9999-12-31")

        InternalDate = InternalDate.AddDays(-1)

        Return ConvertDateToSAP(InternalDate)
    End Function
End Class
