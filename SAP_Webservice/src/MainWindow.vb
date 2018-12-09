Public Class MainWindow
    ''' <summary>
    ''' Loads the main window initially
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MainWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitControls()

        sap_proxy = New BusinessEntity.Z_HH_BusinessEntityREFXClient()
        sap_proxy.ClientCredentials.UserName.UserName = "IDES-032"
        sap_proxy.ClientCredentials.UserName.Password = "ipofipup"

        GetList()
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

        ' valid from
        DtpBusEntityValidFrom.Value = Date.Today
        DtpBusEntityValidTo.Value = DtpBusEntityValidTo.MaxDate
    End Sub

    ''' <summary>
    ''' Handles the click on button BtnNew
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnNew_Click(sender As Object, e As EventArgs) Handles BtnNew.Click
        BtnChange.Enabled = False

        ' header
        TbCompanyCode.Clear()
        TbSite.Clear()
        TbNameOfSite.Clear()

        ' general data
        DtpBusEntityValidFrom.Value = Date.Today
        DtpBusEntityValidTo.Value = DtpBusEntityValidTo.MaxDate

        TbObjectDescr.Clear()
        TbStreet.Clear()
        TbHouseno.Clear()
        TbPostcode.Clear()
        TbCity.Clear()
        CombCountry.SelectedIndex = 0

        ' reference factors
        CombTenancyLaw.SelectedIndex = 0
    End Sub

    ''' <summary>
    ''' Handles the click on button BtnCreate
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnCreate_Click(sender As Object, e As EventArgs) Handles BtnCreate.Click
        CreateEntity()
    End Sub

    ''' <summary>
    ''' Handles the click on button BtnChange
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnChange_Click(sender As Object, e As EventArgs) Handles BtnChange.Click
        ChangeEntity()
    End Sub

    ''' <summary>
    ''' Handles the click on button BtnRefresh
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnRefreshList_Click(sender As Object, e As EventArgs) Handles BtnRefreshList.Click
        GetList()
    End Sub

    ''' <summary>
    ''' Fills all fields and creates the site in the SAP system
    ''' </summary>
    Private Sub CreateEntity()
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
        CreateRequest.BusEntity.ObjectValidFrom = ConvertDateToSAP(DtpBusEntityValidFrom.Value)
        CreateRequest.BusEntity.ObjectValidTo = ConvertDateToSAP(DtpBusEntityValidTo.Value)

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
        '.TermNo = " ", 'TODO ?
        '.ValidFrom = "0001-01-01", 'TODO ?
        Dim TermOaDat As New BusinessEntity.ReTermOaDat With {
            .BusArea = TbBusinessArea.Text,
            .ProfitCtr = TbProfitCenter.Text
        }
        CreateRequest.TermOrgAssignment = {TermOaDat}

        'TODO trans seems not to work
        'CreateRequest.Trans = "BFRE" ' = release -> defined in type pool reca1 as constants reca1_bustrans

        'TODO status seems not to work
        'CreateRequest.BusEntity.StatProf = "I0098"
        'Dim Status As New BusinessEntity.ReStatusDat With {
        '    .StatProf = "I0098",
        '    .UsrStatusText = "ANGL"
        '}
        'CreateRequest.Status = {Status}

        ' create site
        CreateResponse = sap_proxy.BusinessEntityREFXCreate(CreateRequest)

        If Not CheckErrors(CreateResponse.Return) Then
            MessageBox.Show("Wirtschaftseinheit " & CreateResponse.BusinessEntityNumber & " wurde im Buchungskreis " & CreateResponse.CompCode & " erstellt", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CommitWork()
        Else
            RollbackWork()
        End If
    End Sub

    ''' <summary>
    ''' Checks for changes, fills all necessary fields and changes the site in the SAP system
    ''' </summary>
    Private Sub ChangeEntity()
        Dim ChangeRequest As New BusinessEntity.BusinessEntityREFXChange()
        Dim ChangeResponse As BusinessEntity.BusinessEntityREFXChangeResponse

        ChangeRequest.CompCode = TbCompanyCode.Text
        ChangeRequest.BusinessEntityNumber = TbSite.Text

        'TODO find out what has changed and set corresponding fields

        ' struct initialization
        ChangeRequest.BusEntity = New BusinessEntity.ReBusEntityDat()
        ChangeRequest.ObjectAddress = New BusinessEntity.ReObjAddressDat()

        ChangeRequest.BusEntityX = New BusinessEntity.ReBusEntityDatx()
        ChangeRequest.ObjectAddressX = New BusinessEntity.ReObjAddressDatx()

        ' header
        ChangeRequest.BusEntity.BusinessEntityText = TbNameOfSite.Text
        ChangeRequest.BusEntityX.BusinessEntityText = "X"

        ' general data
        ChangeRequest.BusEntity.ObjectValidFrom = ConvertDateToSAP(DtpBusEntityValidFrom.Value)
        ChangeRequest.BusEntityX.ObjectValidFrom = "X"
        ChangeRequest.BusEntity.ObjectValidTo = ConvertDateToSAP(DtpBusEntityValidTo.Value)
        ChangeRequest.BusEntityX.ObjectValidTo = "X"

        ChangeRequest.ObjectAddress.AddressText = TbObjectDescr.Text
        ChangeRequest.ObjectAddressX.AddressText = "X"
        ChangeRequest.ObjectAddress.StreetLng = TbStreet.Text
        ChangeRequest.ObjectAddressX.StreetLng = "X"
        ChangeRequest.ObjectAddress.HouseNo = TbHouseno.Text
        ChangeRequest.ObjectAddressX.HouseNo = "X"
        ChangeRequest.ObjectAddress.PostlCod1 = TbPostcode.Text
        ChangeRequest.ObjectAddressX.PostlCod1 = "X"
        ChangeRequest.ObjectAddress.City = TbCity.Text
        ChangeRequest.ObjectAddressX.City = "X"
        ChangeRequest.ObjectAddress.Country = CombCountry.SelectedValue
        ChangeRequest.ObjectAddressX.Country = "X"

        ' reference factors
        ChangeRequest.BusEntity.TenancyLaw = CombTenancyLaw.SelectedValue
        ChangeRequest.BusEntityX.TenancyLaw = "X"

        ' posting paramters
        Dim TermOrgAssignment As New BusinessEntity.ReTermOaDatc With {
            .ChangeIndicator = "U", 'TODO/ possible indicators are ' ' for ignore, 'I' for insert, 'U' for update, 'D' for delete
            .TermNo = TbTermOrgAssignmentNumber.Text, ' mandatory for identification
            .ValidFrom = ConvertDateToSAP(DtpTermOrgAssignmentValidFrom.Value), ' mandatory for identification
            .BusArea = TbBusinessArea.Text,
            .ProfitCtr = TbProfitCenter.Text
        }
        ChangeRequest.TermOrgAssignment = {TermOrgAssignment}

        ' change the site
        ChangeResponse = sap_proxy.BusinessEntityREFXChange(ChangeRequest)

        If Not CheckErrors(ChangeResponse.Return) Then
            MessageBox.Show("Wirtschaftseinheit " & TbSite.Text & " wurde im Buchungskreis " & TbCompanyCode.Text & " geändert", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CommitWork()
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
        Dim DetailRequest As New BusinessEntity.BusinessEntityREFXGetDetail()
        Dim DetailResponse As BusinessEntity.BusinessEntityREFXGetDetailResponse

        DetailRequest.CompCode = CompanyCode
        DetailRequest.BusinessEntityNumber = BusinessEntityNumber

        DetailResponse = sap_proxy.BusinessEntityREFXGetDetail(DetailRequest)

        ' header
        TbCompanyCode.Text = DetailResponse.BusEntity.CompCode
        TbSite.Text = DetailResponse.BusEntity.BusinessEntity
        TbNameOfSite.Text = DetailResponse.BusEntity.BusinessEntityText

        ' general data
        DtpBusEntityValidFrom.Value = ConvertDateToInternal(DetailResponse.BusEntity.ObjectValidFrom, DtpBusEntityValidFrom.MinDate, DtpBusEntityValidFrom.MaxDate)
        DtpBusEntityValidTo.Value = ConvertDateToInternal(DetailResponse.BusEntity.ObjectValidTo, DtpBusEntityValidTo.MinDate, DtpBusEntityValidTo.MaxDate)

        TbObjectDescr.Text = DetailResponse.ObjectAddress.AddressText
        TbStreet.Text = DetailResponse.ObjectAddress.StreetLng
        TbHouseno.Text = DetailResponse.ObjectAddress.HouseNo
        TbPostcode.Text = DetailResponse.ObjectAddress.PostlCod1
        TbCity.Text = DetailResponse.ObjectAddress.City
        CombCountry.SelectedValue = DetailResponse.ObjectAddress.Country

        ' reference factors
        CombTenancyLaw.SelectedValue = DetailResponse.BusEntity.TenancyLaw

        ' posting parameters
        ' assuming there is only one posting parameter
        Dim TermOrgAssignment = DetailResponse.TermOrgAssignment.ElementAt(0)
        TbTermOrgAssignmentNumber.Text = TermOrgAssignment.TermNo
        DtpTermOrgAssignmentValidFrom.Value = ConvertDateToInternal(TermOrgAssignment.ValidFrom, DtpTermOrgAssignmentValidFrom.MinDate, DtpTermOrgAssignmentValidFrom.MaxDate)
        TbBusinessArea.Text = TermOrgAssignment.BusArea
        TbProfitCenter.Text = TermOrgAssignment.ProfitCtr
    End Sub

    ''' <summary>
    ''' Queries all sites in the SAP system
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

        ListResponse = sap_proxy.BusinessEntityREFXGetList(ListRequest)

        LbxItems.Items.Clear()
        If Not CheckErrors(ListResponse.Return) Then
            For Each BusEntity As BusinessEntity.ReBusEntity In ListResponse.BusEntity
                LbxItems.Items.Add(BusEntity.IdentKey)
            Next
        Else
            MessageBox.Show("Wirtschaftseinheiten des Buchungskreises " & TbCompanyCodeList.Text & " konnten nicht abgefragt werden", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            If BtnCreate.Enabled = False Then
                BtnCreate.Enabled = True
            End If
        Else
            If BtnCreate.Enabled = True Then
                BtnCreate.Enabled = False
            End If
        End If

        Return FieldsFilled
    End Function

    ''' <summary>
    ''' Checks if <paramref name="ReturnList"/> has any error messages in it.
    ''' </summary>
    ''' <param name="ReturnList">List of return messages</param>
    ''' <returns>true if an error occurred, false otherwise</returns>
    Private Function CheckErrors(ByRef ReturnList As BusinessEntity.Bapiret2()) As Boolean
        Dim ErrorOccured As Boolean = False

        For Each ReturnElement As BusinessEntity.Bapiret2 In ReturnList
            If ReturnElement.Type = "S" Or ReturnElement.Type = "I" Or ReturnElement.Type = "W" Then
                'If ReturnElement.Type = "I" Then
                '    MessageBox.Show(ReturnElement.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'Else
                If ReturnElement.Type = "W" Then
                    Dim result As DialogResult
                    result = MessageBox.Show(ReturnElement.Message & Environment.NewLine & "Wollen Sie dennoch fortfahren (nicht empfohlen)?", "Warnung", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                    If result = DialogResult.No Then
                        ErrorOccured = True
                    End If
                End If
                'End If
            ElseIf ReturnElement.Type = "E" Or ReturnElement.Type = "A" Then
                MessageBox.Show(ReturnElement.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ErrorOccured = True
            End If
        Next

        Return ErrorOccured
    End Function

    ''' <summary>
    ''' Submits a commit
    ''' </summary>
    Private Sub CommitWork()
        Dim CommitRequest As New BusinessEntity.BapiServiceTransactionCommit()
        Dim CommitResponse As BusinessEntity.BapiServiceTransactionCommitResponse

        CommitRequest.WAIT = "X"
        CommitResponse = sap_proxy.BapiServiceTransactionCommit(CommitRequest)

        If CommitResponse.Return.Type = "E" Then
            MessageBox.Show(CommitResponse.Return.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
            RollbackWork()
        End If
    End Sub

    ''' <summary>
    ''' Submits a rollback
    ''' </summary>
    Private Sub RollbackWork()
        Dim RollbackRequest As New BusinessEntity.BapiServiceTransactionRollback()

        ' no return messages are received in case of an error here
        sap_proxy.BapiServiceTransactionRollback(RollbackRequest)
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

        ConvertedDate = New Date(Value.Substring(0, 4), Value.Substring(5, 2), Value.Substring(8, 2), 0, 0, 0)

        ' check bounds
        If ConvertedDate < MinDate Then
            ConvertedDate = MinDate
        ElseIf ConvertedDate > MaxDate Then
            ConvertedDate = MaxDate
        End If

        Return ConvertedDate
    End Function

    Private Function ConvertDateToSAP(ByVal Value As String) As String
        Return Value.Substring(6, 4) & "-" & Value.Substring(3, 2) & "-" & Value.Substring(0, 2) ' format: 2018-12-31
    End Function

    Private Sub OnKeyUpInTbCompanyCodeList(sender As Object, e As KeyEventArgs) Handles TbCompanyCodeList.KeyUp
        If e.KeyCode = Keys.Enter Then
            GetList()
        End If
    End Sub

    Private Sub OnSelectedIndexChangedInLbxItems(sender As Object, e As EventArgs) Handles LbxItems.SelectedIndexChanged
        Dim CompanyCode = LbxItems.SelectedItem().ToString().Substring(0, 4)
        Dim BusinessEntityNumber = LbxItems.SelectedItem().ToString().Substring(5)

        GetDetail(CompanyCode, BusinessEntityNumber)

        BtnChange.Enabled = True
    End Sub

    Private Sub OnKeyUpInTbCompanyCode(sender As Object, e As KeyEventArgs) Handles TbCompanyCode.KeyUp
        If MandatoryFieldsFilled() Then
            If BtnCreate.Enabled = False Then
                BtnCreate.Enabled = True
            End If
        Else
            If BtnCreate.Enabled = True Then
                BtnCreate.Enabled = False
            End If
        End If
    End Sub

    Private Sub OnKeyUpInTbSite(sender As Object, e As KeyEventArgs) Handles TbSite.KeyUp
        If MandatoryFieldsFilled() Then
            If BtnCreate.Enabled = False Then
                BtnCreate.Enabled = True
            End If
        Else
            If BtnCreate.Enabled = True Then
                BtnCreate.Enabled = False
            End If
        End If
    End Sub

    Private Sub OnKeyUpInTbNameOfSite(sender As Object, e As KeyEventArgs) Handles TbNameOfSite.KeyUp
        If MandatoryFieldsFilled() Then
            If BtnCreate.Enabled = False Then
                BtnCreate.Enabled = True
            End If
        Else
            If BtnCreate.Enabled = True Then
                BtnCreate.Enabled = False
            End If
        End If
    End Sub

    Private Sub OnSelectedIndexChangeInCombTenancyLaw(sender As Object, e As EventArgs) Handles CombTenancyLaw.SelectedIndexChanged
        If MandatoryFieldsFilled() Then
            If BtnCreate.Enabled = False Then
                BtnCreate.Enabled = True
            End If
        Else
            If BtnCreate.Enabled = True Then
                BtnCreate.Enabled = False
            End If
        End If
    End Sub

    ''' <summary>
    ''' Shows information about the mandatory fields to be filled
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub LblMandatoryFields_Click(sender As Object, e As EventArgs) Handles LblMandatoryFields.Click
        MessageBox.Show("Pflichtfelder unabhängig des Buchungskreises:" + Environment.NewLine +
                        " * Buchungskreis" + Environment.NewLine +
                        " * Wirtschaftseinheit" + Environment.NewLine +
                        " * Bezeichnung der WE" + Environment.NewLine +
                        " * Bezugsgrößen - Mietrecht", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
End Class
