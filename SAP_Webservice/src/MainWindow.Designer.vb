<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainWindow
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.LblCompanyCode = New System.Windows.Forms.Label()
        Me.BtnNew = New System.Windows.Forms.Button()
        Me.BtnChange = New System.Windows.Forms.Button()
        Me.TbCompanyCode = New System.Windows.Forms.TextBox()
        Me.LblSite = New System.Windows.Forms.Label()
        Me.TbSite = New System.Windows.Forms.TextBox()
        Me.LblNameOfSite = New System.Windows.Forms.Label()
        Me.TbNameOfSite = New System.Windows.Forms.TextBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TpGeneralData = New System.Windows.Forms.TabPage()
        Me.TbBusEntityValidTo = New System.Windows.Forms.TextBox()
        Me.TbBusEntityValidFrom = New System.Windows.Forms.TextBox()
        Me.CombCountry = New System.Windows.Forms.ComboBox()
        Me.LblObjectDescr = New System.Windows.Forms.Label()
        Me.TbObjectDescr = New System.Windows.Forms.TextBox()
        Me.TbCity = New System.Windows.Forms.TextBox()
        Me.TbHouseno = New System.Windows.Forms.TextBox()
        Me.TbPostcode = New System.Windows.Forms.TextBox()
        Me.LblCountry = New System.Windows.Forms.Label()
        Me.LblPostcodeCity = New System.Windows.Forms.Label()
        Me.TbStreet = New System.Windows.Forms.TextBox()
        Me.LblStreetHouseno = New System.Windows.Forms.Label()
        Me.LblBusEntityValidTo = New System.Windows.Forms.Label()
        Me.LblBusEntityValidFrom = New System.Windows.Forms.Label()
        Me.TpReferenceFactors = New System.Windows.Forms.TabPage()
        Me.CombTenancyLaw = New System.Windows.Forms.ComboBox()
        Me.LblTenancyLaw = New System.Windows.Forms.Label()
        Me.TpPostingParameters = New System.Windows.Forms.TabPage()
        Me.TbTermOrgAssignmentText = New System.Windows.Forms.TextBox()
        Me.TbTermOrgAssignmentValidTo = New System.Windows.Forms.TextBox()
        Me.LblTermOrgAssignmentValidTo = New System.Windows.Forms.Label()
        Me.LblActiveTerm = New System.Windows.Forms.Label()
        Me.BtnNextTerm = New System.Windows.Forms.Button()
        Me.BtnPreviousTerm = New System.Windows.Forms.Button()
        Me.TbTaxJurisd = New System.Windows.Forms.TextBox()
        Me.LblTaxJurisd = New System.Windows.Forms.Label()
        Me.TbTermOrgAssignmentValidFrom = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LblTermOrgAssignmentValidFrom = New System.Windows.Forms.Label()
        Me.TbTermOrgAssignmentNumber = New System.Windows.Forms.TextBox()
        Me.LblTermOrgAssignmentNumber = New System.Windows.Forms.Label()
        Me.TbBusinessArea = New System.Windows.Forms.TextBox()
        Me.LblBusinessArea = New System.Windows.Forms.Label()
        Me.LblProfitcenter = New System.Windows.Forms.Label()
        Me.TbProfitCenter = New System.Windows.Forms.TextBox()
        Me.LblExistingSites = New System.Windows.Forms.Label()
        Me.BtnCreate = New System.Windows.Forms.Button()
        Me.BtnRefreshList = New System.Windows.Forms.Button()
        Me.TbCompanyCodeList = New System.Windows.Forms.TextBox()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.BtnHelp = New System.Windows.Forms.Button()
        Me.DgvItems = New System.Windows.Forms.DataGridView()
        Me.BE_CompanyCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BE_Number = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BE_Name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BusinessEntityREFXGetListResponseBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.BusinessEntityREFXGetListResponse1BindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.TabControl1.SuspendLayout()
        Me.TpGeneralData.SuspendLayout()
        Me.TpReferenceFactors.SuspendLayout()
        Me.TpPostingParameters.SuspendLayout()
        CType(Me.DgvItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BusinessEntityREFXGetListResponseBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BusinessEntityREFXGetListResponse1BindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LblCompanyCode
        '
        Me.LblCompanyCode.AutoSize = True
        Me.LblCompanyCode.Location = New System.Drawing.Point(12, 9)
        Me.LblCompanyCode.Name = "LblCompanyCode"
        Me.LblCompanyCode.Size = New System.Drawing.Size(81, 13)
        Me.LblCompanyCode.TabIndex = 3
        Me.LblCompanyCode.Text = "Buchungskreis*"
        '
        'BtnNew
        '
        Me.BtnNew.Location = New System.Drawing.Point(12, 93)
        Me.BtnNew.Name = "BtnNew"
        Me.BtnNew.Size = New System.Drawing.Size(75, 23)
        Me.BtnNew.TabIndex = 21
        Me.BtnNew.Text = "Neu"
        Me.BtnNew.UseVisualStyleBackColor = True
        '
        'BtnChange
        '
        Me.BtnChange.Enabled = False
        Me.BtnChange.Location = New System.Drawing.Point(180, 93)
        Me.BtnChange.Name = "BtnChange"
        Me.BtnChange.Size = New System.Drawing.Size(75, 23)
        Me.BtnChange.TabIndex = 23
        Me.BtnChange.Text = "Ändern"
        Me.BtnChange.UseVisualStyleBackColor = True
        '
        'TbCompanyCode
        '
        Me.TbCompanyCode.Location = New System.Drawing.Point(155, 6)
        Me.TbCompanyCode.MaxLength = 4
        Me.TbCompanyCode.Name = "TbCompanyCode"
        Me.TbCompanyCode.Size = New System.Drawing.Size(40, 20)
        Me.TbCompanyCode.TabIndex = 1
        '
        'LblSite
        '
        Me.LblSite.AutoSize = True
        Me.LblSite.Location = New System.Drawing.Point(12, 35)
        Me.LblSite.Name = "LblSite"
        Me.LblSite.Size = New System.Drawing.Size(95, 13)
        Me.LblSite.TabIndex = 7
        Me.LblSite.Text = "Wirtschaftseinheit*"
        '
        'TbSite
        '
        Me.TbSite.Location = New System.Drawing.Point(155, 32)
        Me.TbSite.MaxLength = 8
        Me.TbSite.Name = "TbSite"
        Me.TbSite.Size = New System.Drawing.Size(100, 20)
        Me.TbSite.TabIndex = 2
        '
        'LblNameOfSite
        '
        Me.LblNameOfSite.AutoSize = True
        Me.LblNameOfSite.Location = New System.Drawing.Point(12, 61)
        Me.LblNameOfSite.Name = "LblNameOfSite"
        Me.LblNameOfSite.Size = New System.Drawing.Size(112, 13)
        Me.LblNameOfSite.TabIndex = 9
        Me.LblNameOfSite.Text = "Bezeichnung der WE*"
        '
        'TbNameOfSite
        '
        Me.TbNameOfSite.Location = New System.Drawing.Point(155, 58)
        Me.TbNameOfSite.MaxLength = 60
        Me.TbNameOfSite.Name = "TbNameOfSite"
        Me.TbNameOfSite.Size = New System.Drawing.Size(345, 20)
        Me.TbNameOfSite.TabIndex = 3
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TpGeneralData)
        Me.TabControl1.Controls.Add(Me.TpReferenceFactors)
        Me.TabControl1.Controls.Add(Me.TpPostingParameters)
        Me.TabControl1.Location = New System.Drawing.Point(12, 122)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(776, 205)
        Me.TabControl1.TabIndex = 12
        '
        'TpGeneralData
        '
        Me.TpGeneralData.Controls.Add(Me.TbBusEntityValidTo)
        Me.TpGeneralData.Controls.Add(Me.TbBusEntityValidFrom)
        Me.TpGeneralData.Controls.Add(Me.CombCountry)
        Me.TpGeneralData.Controls.Add(Me.LblObjectDescr)
        Me.TpGeneralData.Controls.Add(Me.TbObjectDescr)
        Me.TpGeneralData.Controls.Add(Me.TbCity)
        Me.TpGeneralData.Controls.Add(Me.TbHouseno)
        Me.TpGeneralData.Controls.Add(Me.TbPostcode)
        Me.TpGeneralData.Controls.Add(Me.LblCountry)
        Me.TpGeneralData.Controls.Add(Me.LblPostcodeCity)
        Me.TpGeneralData.Controls.Add(Me.TbStreet)
        Me.TpGeneralData.Controls.Add(Me.LblStreetHouseno)
        Me.TpGeneralData.Controls.Add(Me.LblBusEntityValidTo)
        Me.TpGeneralData.Controls.Add(Me.LblBusEntityValidFrom)
        Me.TpGeneralData.Location = New System.Drawing.Point(4, 22)
        Me.TpGeneralData.Name = "TpGeneralData"
        Me.TpGeneralData.Padding = New System.Windows.Forms.Padding(3)
        Me.TpGeneralData.Size = New System.Drawing.Size(768, 179)
        Me.TpGeneralData.TabIndex = 0
        Me.TpGeneralData.Text = "Allgemeine Daten"
        Me.TpGeneralData.UseVisualStyleBackColor = True
        '
        'TbBusEntityValidTo
        '
        Me.TbBusEntityValidTo.Location = New System.Drawing.Point(242, 6)
        Me.TbBusEntityValidTo.MaxLength = 10
        Me.TbBusEntityValidTo.Name = "TbBusEntityValidTo"
        Me.TbBusEntityValidTo.Size = New System.Drawing.Size(100, 20)
        Me.TbBusEntityValidTo.TabIndex = 5
        '
        'TbBusEntityValidFrom
        '
        Me.TbBusEntityValidFrom.Location = New System.Drawing.Point(101, 6)
        Me.TbBusEntityValidFrom.MaxLength = 10
        Me.TbBusEntityValidFrom.Name = "TbBusEntityValidFrom"
        Me.TbBusEntityValidFrom.Size = New System.Drawing.Size(100, 20)
        Me.TbBusEntityValidFrom.TabIndex = 4
        '
        'CombCountry
        '
        Me.CombCountry.FormattingEnabled = True
        Me.CombCountry.Location = New System.Drawing.Point(101, 122)
        Me.CombCountry.Name = "CombCountry"
        Me.CombCountry.Size = New System.Drawing.Size(133, 21)
        Me.CombCountry.Sorted = True
        Me.CombCountry.TabIndex = 11
        '
        'LblObjectDescr
        '
        Me.LblObjectDescr.AutoSize = True
        Me.LblObjectDescr.Location = New System.Drawing.Point(1, 48)
        Me.LblObjectDescr.Name = "LblObjectDescr"
        Me.LblObjectDescr.Size = New System.Drawing.Size(88, 13)
        Me.LblObjectDescr.TabIndex = 13
        Me.LblObjectDescr.Text = "Obj-Bezeichnung"
        '
        'TbObjectDescr
        '
        Me.TbObjectDescr.Location = New System.Drawing.Point(101, 45)
        Me.TbObjectDescr.MaxLength = 35
        Me.TbObjectDescr.Name = "TbObjectDescr"
        Me.TbObjectDescr.Size = New System.Drawing.Size(234, 20)
        Me.TbObjectDescr.TabIndex = 6
        '
        'TbCity
        '
        Me.TbCity.Location = New System.Drawing.Point(177, 96)
        Me.TbCity.MaxLength = 40
        Me.TbCity.Name = "TbCity"
        Me.TbCity.Size = New System.Drawing.Size(290, 20)
        Me.TbCity.TabIndex = 10
        '
        'TbHouseno
        '
        Me.TbHouseno.Location = New System.Drawing.Point(397, 71)
        Me.TbHouseno.MaxLength = 10
        Me.TbHouseno.Name = "TbHouseno"
        Me.TbHouseno.Size = New System.Drawing.Size(70, 20)
        Me.TbHouseno.TabIndex = 8
        '
        'TbPostcode
        '
        Me.TbPostcode.Location = New System.Drawing.Point(101, 96)
        Me.TbPostcode.MaxLength = 10
        Me.TbPostcode.Name = "TbPostcode"
        Me.TbPostcode.Size = New System.Drawing.Size(70, 20)
        Me.TbPostcode.TabIndex = 9
        '
        'LblCountry
        '
        Me.LblCountry.AutoSize = True
        Me.LblCountry.Location = New System.Drawing.Point(1, 125)
        Me.LblCountry.Name = "LblCountry"
        Me.LblCountry.Size = New System.Drawing.Size(31, 13)
        Me.LblCountry.TabIndex = 7
        Me.LblCountry.Text = "Land"
        '
        'LblPostcodeCity
        '
        Me.LblPostcodeCity.AutoSize = True
        Me.LblPostcodeCity.Location = New System.Drawing.Point(1, 99)
        Me.LblPostcodeCity.Name = "LblPostcodeCity"
        Me.LblPostcodeCity.Size = New System.Drawing.Size(46, 13)
        Me.LblPostcodeCity.TabIndex = 6
        Me.LblPostcodeCity.Text = "PLZ/Ort"
        '
        'TbStreet
        '
        Me.TbStreet.Location = New System.Drawing.Point(101, 71)
        Me.TbStreet.MaxLength = 60
        Me.TbStreet.Name = "TbStreet"
        Me.TbStreet.Size = New System.Drawing.Size(290, 20)
        Me.TbStreet.TabIndex = 7
        '
        'LblStreetHouseno
        '
        Me.LblStreetHouseno.AutoSize = True
        Me.LblStreetHouseno.Location = New System.Drawing.Point(1, 74)
        Me.LblStreetHouseno.Name = "LblStreetHouseno"
        Me.LblStreetHouseno.Size = New System.Drawing.Size(80, 13)
        Me.LblStreetHouseno.TabIndex = 4
        Me.LblStreetHouseno.Text = "Straße/Hausnr."
        '
        'LblBusEntityValidTo
        '
        Me.LblBusEntityValidTo.AutoSize = True
        Me.LblBusEntityValidTo.Location = New System.Drawing.Point(218, 12)
        Me.LblBusEntityValidTo.Name = "LblBusEntityValidTo"
        Me.LblBusEntityValidTo.Size = New System.Drawing.Size(21, 13)
        Me.LblBusEntityValidTo.TabIndex = 2
        Me.LblBusEntityValidTo.Text = "Bis"
        '
        'LblBusEntityValidFrom
        '
        Me.LblBusEntityValidFrom.AutoSize = True
        Me.LblBusEntityValidFrom.Location = New System.Drawing.Point(3, 9)
        Me.LblBusEntityValidFrom.Name = "LblBusEntityValidFrom"
        Me.LblBusEntityValidFrom.Size = New System.Drawing.Size(49, 13)
        Me.LblBusEntityValidFrom.TabIndex = 0
        Me.LblBusEntityValidFrom.Text = "Gültig ab"
        '
        'TpReferenceFactors
        '
        Me.TpReferenceFactors.Controls.Add(Me.CombTenancyLaw)
        Me.TpReferenceFactors.Controls.Add(Me.LblTenancyLaw)
        Me.TpReferenceFactors.Location = New System.Drawing.Point(4, 22)
        Me.TpReferenceFactors.Name = "TpReferenceFactors"
        Me.TpReferenceFactors.Padding = New System.Windows.Forms.Padding(3)
        Me.TpReferenceFactors.Size = New System.Drawing.Size(768, 179)
        Me.TpReferenceFactors.TabIndex = 1
        Me.TpReferenceFactors.Text = "Bezugsgrößen"
        Me.TpReferenceFactors.UseVisualStyleBackColor = True
        '
        'CombTenancyLaw
        '
        Me.CombTenancyLaw.FormattingEnabled = True
        Me.CombTenancyLaw.Location = New System.Drawing.Point(96, 6)
        Me.CombTenancyLaw.Name = "CombTenancyLaw"
        Me.CombTenancyLaw.Size = New System.Drawing.Size(152, 21)
        Me.CombTenancyLaw.Sorted = True
        Me.CombTenancyLaw.TabIndex = 12
        '
        'LblTenancyLaw
        '
        Me.LblTenancyLaw.AutoSize = True
        Me.LblTenancyLaw.Location = New System.Drawing.Point(6, 9)
        Me.LblTenancyLaw.Name = "LblTenancyLaw"
        Me.LblTenancyLaw.Size = New System.Drawing.Size(55, 13)
        Me.LblTenancyLaw.TabIndex = 0
        Me.LblTenancyLaw.Text = "Mietrecht*"
        '
        'TpPostingParameters
        '
        Me.TpPostingParameters.BackColor = System.Drawing.Color.White
        Me.TpPostingParameters.Controls.Add(Me.TbTermOrgAssignmentText)
        Me.TpPostingParameters.Controls.Add(Me.TbTermOrgAssignmentValidTo)
        Me.TpPostingParameters.Controls.Add(Me.LblTermOrgAssignmentValidTo)
        Me.TpPostingParameters.Controls.Add(Me.LblActiveTerm)
        Me.TpPostingParameters.Controls.Add(Me.BtnNextTerm)
        Me.TpPostingParameters.Controls.Add(Me.BtnPreviousTerm)
        Me.TpPostingParameters.Controls.Add(Me.TbTaxJurisd)
        Me.TpPostingParameters.Controls.Add(Me.LblTaxJurisd)
        Me.TpPostingParameters.Controls.Add(Me.TbTermOrgAssignmentValidFrom)
        Me.TpPostingParameters.Controls.Add(Me.Label1)
        Me.TpPostingParameters.Controls.Add(Me.Label2)
        Me.TpPostingParameters.Controls.Add(Me.LblTermOrgAssignmentValidFrom)
        Me.TpPostingParameters.Controls.Add(Me.TbTermOrgAssignmentNumber)
        Me.TpPostingParameters.Controls.Add(Me.LblTermOrgAssignmentNumber)
        Me.TpPostingParameters.Controls.Add(Me.TbBusinessArea)
        Me.TpPostingParameters.Controls.Add(Me.LblBusinessArea)
        Me.TpPostingParameters.Controls.Add(Me.LblProfitcenter)
        Me.TpPostingParameters.Controls.Add(Me.TbProfitCenter)
        Me.TpPostingParameters.Location = New System.Drawing.Point(4, 22)
        Me.TpPostingParameters.Name = "TpPostingParameters"
        Me.TpPostingParameters.Padding = New System.Windows.Forms.Padding(3)
        Me.TpPostingParameters.Size = New System.Drawing.Size(768, 179)
        Me.TpPostingParameters.TabIndex = 5
        Me.TpPostingParameters.Text = "Buchungsparameter"
        '
        'TbTermOrgAssignmentText
        '
        Me.TbTermOrgAssignmentText.Location = New System.Drawing.Point(158, 5)
        Me.TbTermOrgAssignmentText.MaxLength = 60
        Me.TbTermOrgAssignmentText.Name = "TbTermOrgAssignmentText"
        Me.TbTermOrgAssignmentText.Size = New System.Drawing.Size(264, 20)
        Me.TbTermOrgAssignmentText.TabIndex = 14
        '
        'TbTermOrgAssignmentValidTo
        '
        Me.TbTermOrgAssignmentValidTo.Location = New System.Drawing.Point(252, 31)
        Me.TbTermOrgAssignmentValidTo.Name = "TbTermOrgAssignmentValidTo"
        Me.TbTermOrgAssignmentValidTo.ReadOnly = True
        Me.TbTermOrgAssignmentValidTo.Size = New System.Drawing.Size(100, 20)
        Me.TbTermOrgAssignmentValidTo.TabIndex = 16
        '
        'LblTermOrgAssignmentValidTo
        '
        Me.LblTermOrgAssignmentValidTo.AutoSize = True
        Me.LblTermOrgAssignmentValidTo.Location = New System.Drawing.Point(225, 34)
        Me.LblTermOrgAssignmentValidTo.Name = "LblTermOrgAssignmentValidTo"
        Me.LblTermOrgAssignmentValidTo.Size = New System.Drawing.Size(21, 13)
        Me.LblTermOrgAssignmentValidTo.TabIndex = 17
        Me.LblTermOrgAssignmentValidTo.Text = "Bis"
        '
        'LblActiveTerm
        '
        Me.LblActiveTerm.AutoSize = True
        Me.LblActiveTerm.Location = New System.Drawing.Point(428, 34)
        Me.LblActiveTerm.Name = "LblActiveTerm"
        Me.LblActiveTerm.Size = New System.Drawing.Size(45, 13)
        Me.LblActiveTerm.TabIndex = 16
        Me.LblActiveTerm.Text = "(Aktuell)"
        '
        'BtnNextTerm
        '
        Me.BtnNextTerm.Location = New System.Drawing.Point(399, 29)
        Me.BtnNextTerm.Name = "BtnNextTerm"
        Me.BtnNextTerm.Size = New System.Drawing.Size(23, 23)
        Me.BtnNextTerm.TabIndex = 15
        Me.BtnNextTerm.Text = ">"
        Me.BtnNextTerm.UseVisualStyleBackColor = True
        '
        'BtnPreviousTerm
        '
        Me.BtnPreviousTerm.Location = New System.Drawing.Point(370, 29)
        Me.BtnPreviousTerm.Name = "BtnPreviousTerm"
        Me.BtnPreviousTerm.Size = New System.Drawing.Size(23, 23)
        Me.BtnPreviousTerm.TabIndex = 14
        Me.BtnPreviousTerm.Text = "<"
        Me.BtnPreviousTerm.UseVisualStyleBackColor = True
        '
        'TbTaxJurisd
        '
        Me.TbTaxJurisd.Location = New System.Drawing.Point(114, 120)
        Me.TbTaxJurisd.MaxLength = 15
        Me.TbTaxJurisd.Name = "TbTaxJurisd"
        Me.TbTaxJurisd.Size = New System.Drawing.Size(100, 20)
        Me.TbTaxJurisd.TabIndex = 19
        '
        'LblTaxJurisd
        '
        Me.LblTaxJurisd.AutoSize = True
        Me.LblTaxJurisd.Location = New System.Drawing.Point(6, 123)
        Me.LblTaxJurisd.Name = "LblTaxJurisd"
        Me.LblTaxJurisd.Size = New System.Drawing.Size(61, 13)
        Me.LblTaxJurisd.TabIndex = 12
        Me.LblTaxJurisd.Text = "Steuerst.ort"
        '
        'TbTermOrgAssignmentValidFrom
        '
        Me.TbTermOrgAssignmentValidFrom.Location = New System.Drawing.Point(114, 31)
        Me.TbTermOrgAssignmentValidFrom.MaxLength = 10
        Me.TbTermOrgAssignmentValidFrom.Name = "TbTermOrgAssignmentValidFrom"
        Me.TbTermOrgAssignmentValidFrom.Size = New System.Drawing.Size(100, 20)
        Me.TbTermOrgAssignmentValidFrom.TabIndex = 15
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(220, 96)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(268, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "(z.B. N999 = Dummy für nicht zugeordnete Buchungen)"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(220, 70)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(119, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "(z.B. 9100 = Immobilien)"
        '
        'LblTermOrgAssignmentValidFrom
        '
        Me.LblTermOrgAssignmentValidFrom.AutoSize = True
        Me.LblTermOrgAssignmentValidFrom.Location = New System.Drawing.Point(6, 33)
        Me.LblTermOrgAssignmentValidFrom.Name = "LblTermOrgAssignmentValidFrom"
        Me.LblTermOrgAssignmentValidFrom.Size = New System.Drawing.Size(49, 13)
        Me.LblTermOrgAssignmentValidFrom.TabIndex = 6
        Me.LblTermOrgAssignmentValidFrom.Text = "Gültig ab"
        '
        'TbTermOrgAssignmentNumber
        '
        Me.TbTermOrgAssignmentNumber.Location = New System.Drawing.Point(114, 5)
        Me.TbTermOrgAssignmentNumber.MaxLength = 4
        Me.TbTermOrgAssignmentNumber.Name = "TbTermOrgAssignmentNumber"
        Me.TbTermOrgAssignmentNumber.ReadOnly = True
        Me.TbTermOrgAssignmentNumber.Size = New System.Drawing.Size(38, 20)
        Me.TbTermOrgAssignmentNumber.TabIndex = 13
        '
        'LblTermOrgAssignmentNumber
        '
        Me.LblTermOrgAssignmentNumber.AutoSize = True
        Me.LblTermOrgAssignmentNumber.Location = New System.Drawing.Point(6, 8)
        Me.LblTermOrgAssignmentNumber.Name = "LblTermOrgAssignmentNumber"
        Me.LblTermOrgAssignmentNumber.Size = New System.Drawing.Size(101, 13)
        Me.LblTermOrgAssignmentNumber.TabIndex = 4
        Me.LblTermOrgAssignmentNumber.Text = "Nummer der Klausel"
        '
        'TbBusinessArea
        '
        Me.TbBusinessArea.Location = New System.Drawing.Point(114, 67)
        Me.TbBusinessArea.MaxLength = 4
        Me.TbBusinessArea.Name = "TbBusinessArea"
        Me.TbBusinessArea.Size = New System.Drawing.Size(38, 20)
        Me.TbBusinessArea.TabIndex = 17
        '
        'LblBusinessArea
        '
        Me.LblBusinessArea.AutoSize = True
        Me.LblBusinessArea.Location = New System.Drawing.Point(6, 70)
        Me.LblBusinessArea.Name = "LblBusinessArea"
        Me.LblBusinessArea.Size = New System.Drawing.Size(90, 13)
        Me.LblBusinessArea.TabIndex = 2
        Me.LblBusinessArea.Text = "Geschäftsbereich"
        '
        'LblProfitcenter
        '
        Me.LblProfitcenter.AutoSize = True
        Me.LblProfitcenter.Location = New System.Drawing.Point(6, 96)
        Me.LblProfitcenter.Name = "LblProfitcenter"
        Me.LblProfitcenter.Size = New System.Drawing.Size(61, 13)
        Me.LblProfitcenter.TabIndex = 1
        Me.LblProfitcenter.Text = "Profitcenter"
        '
        'TbProfitCenter
        '
        Me.TbProfitCenter.Location = New System.Drawing.Point(114, 93)
        Me.TbProfitCenter.MaxLength = 10
        Me.TbProfitCenter.Name = "TbProfitCenter"
        Me.TbProfitCenter.Size = New System.Drawing.Size(100, 20)
        Me.TbProfitCenter.TabIndex = 18
        '
        'LblExistingSites
        '
        Me.LblExistingSites.AutoSize = True
        Me.LblExistingSites.Location = New System.Drawing.Point(12, 339)
        Me.LblExistingSites.Name = "LblExistingSites"
        Me.LblExistingSites.Size = New System.Drawing.Size(250, 13)
        Me.LblExistingSites.TabIndex = 13
        Me.LblExistingSites.Text = "Vorhandene Wirtschaftseinheiten im Buchungskreis"
        '
        'BtnCreate
        '
        Me.BtnCreate.Enabled = False
        Me.BtnCreate.Location = New System.Drawing.Point(93, 93)
        Me.BtnCreate.Name = "BtnCreate"
        Me.BtnCreate.Size = New System.Drawing.Size(75, 23)
        Me.BtnCreate.TabIndex = 22
        Me.BtnCreate.Text = "Erstellen"
        Me.BtnCreate.UseVisualStyleBackColor = True
        '
        'BtnRefreshList
        '
        Me.BtnRefreshList.Font = New System.Drawing.Font("Wingdings 3", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.BtnRefreshList.Location = New System.Drawing.Point(764, 334)
        Me.BtnRefreshList.Name = "BtnRefreshList"
        Me.BtnRefreshList.Size = New System.Drawing.Size(24, 23)
        Me.BtnRefreshList.TabIndex = 25
        Me.BtnRefreshList.Text = "P"
        Me.BtnRefreshList.UseVisualStyleBackColor = True
        '
        'TbCompanyCodeList
        '
        Me.TbCompanyCodeList.Location = New System.Drawing.Point(270, 336)
        Me.TbCompanyCodeList.MaxLength = 4
        Me.TbCompanyCodeList.Name = "TbCompanyCodeList"
        Me.TbCompanyCodeList.Size = New System.Drawing.Size(40, 20)
        Me.TbCompanyCodeList.TabIndex = 24
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(0, 0)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 628)
        Me.Splitter1.TabIndex = 18
        Me.Splitter1.TabStop = False
        '
        'BtnHelp
        '
        Me.BtnHelp.Location = New System.Drawing.Point(743, 4)
        Me.BtnHelp.Name = "BtnHelp"
        Me.BtnHelp.Size = New System.Drawing.Size(45, 23)
        Me.BtnHelp.TabIndex = 20
        Me.BtnHelp.Text = "? Hilfe"
        Me.BtnHelp.UseVisualStyleBackColor = True
        '
        'DgvItems
        '
        Me.DgvItems.AllowUserToAddRows = False
        Me.DgvItems.AllowUserToDeleteRows = False
        Me.DgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvItems.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.BE_CompanyCode, Me.BE_Number, Me.BE_Name})
        Me.DgvItems.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DgvItems.Location = New System.Drawing.Point(12, 360)
        Me.DgvItems.MultiSelect = False
        Me.DgvItems.Name = "DgvItems"
        Me.DgvItems.ReadOnly = True
        Me.DgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvItems.Size = New System.Drawing.Size(776, 256)
        Me.DgvItems.TabIndex = 21
        '
        'BE_CompanyCode
        '
        Me.BE_CompanyCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.BE_CompanyCode.HeaderText = "Buchungskreis"
        Me.BE_CompanyCode.Name = "BE_CompanyCode"
        Me.BE_CompanyCode.ReadOnly = True
        Me.BE_CompanyCode.Width = 102
        '
        'BE_Number
        '
        Me.BE_Number.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.BE_Number.HeaderText = "Wirtschaftseinheit"
        Me.BE_Number.Name = "BE_Number"
        Me.BE_Number.ReadOnly = True
        Me.BE_Number.Width = 116
        '
        'BE_Name
        '
        Me.BE_Name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.BE_Name.HeaderText = "Bezeichnung"
        Me.BE_Name.Name = "BE_Name"
        Me.BE_Name.ReadOnly = True
        '
        'BusinessEntityREFXGetListResponseBindingSource
        '
        Me.BusinessEntityREFXGetListResponseBindingSource.DataSource = GetType(SAP_Webservice.BusinessEntity.BusinessEntityREFXGetListResponse)
        '
        'BusinessEntityREFXGetListResponse1BindingSource
        '
        Me.BusinessEntityREFXGetListResponse1BindingSource.DataSource = GetType(SAP_Webservice.BusinessEntity.BusinessEntityREFXGetListResponse1)
        '
        'MainWindow
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(800, 628)
        Me.Controls.Add(Me.DgvItems)
        Me.Controls.Add(Me.BtnHelp)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.TbCompanyCodeList)
        Me.Controls.Add(Me.BtnRefreshList)
        Me.Controls.Add(Me.BtnCreate)
        Me.Controls.Add(Me.LblExistingSites)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.TbNameOfSite)
        Me.Controls.Add(Me.LblNameOfSite)
        Me.Controls.Add(Me.TbSite)
        Me.Controls.Add(Me.LblSite)
        Me.Controls.Add(Me.TbCompanyCode)
        Me.Controls.Add(Me.BtnChange)
        Me.Controls.Add(Me.BtnNew)
        Me.Controls.Add(Me.LblCompanyCode)
        Me.Name = "MainWindow"
        Me.Text = "BusinessEntityREFX"
        Me.TabControl1.ResumeLayout(False)
        Me.TpGeneralData.ResumeLayout(False)
        Me.TpGeneralData.PerformLayout()
        Me.TpReferenceFactors.ResumeLayout(False)
        Me.TpReferenceFactors.PerformLayout()
        Me.TpPostingParameters.ResumeLayout(False)
        Me.TpPostingParameters.PerformLayout()
        CType(Me.DgvItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BusinessEntityREFXGetListResponseBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BusinessEntityREFXGetListResponse1BindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LblCompanyCode As Label
    Friend WithEvents BtnNew As Button
    Friend WithEvents BtnChange As Button
    Friend WithEvents TbCompanyCode As TextBox
    Friend WithEvents LblSite As Label
    Friend WithEvents TbSite As TextBox
    Friend WithEvents LblNameOfSite As Label
    Friend WithEvents TbNameOfSite As TextBox
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TpGeneralData As TabPage
    Friend WithEvents TpReferenceFactors As TabPage
    Friend WithEvents TpPostingParameters As TabPage
    Friend WithEvents LblExistingSites As Label
    Friend WithEvents BtnCreate As Button
    Friend WithEvents BtnRefreshList As Button
    Friend WithEvents CombTenancyLaw As ComboBox
    Friend WithEvents LblTenancyLaw As Label
    Friend WithEvents LblBusEntityValidFrom As Label
    Friend WithEvents LblBusEntityValidTo As Label
    Friend WithEvents TbPostcode As TextBox
    Friend WithEvents LblCountry As Label
    Friend WithEvents LblPostcodeCity As Label
    Friend WithEvents TbStreet As TextBox
    Friend WithEvents LblStreetHouseno As Label
    Friend WithEvents TbHouseno As TextBox
    Friend WithEvents TbCity As TextBox
    Friend WithEvents LblObjectDescr As Label
    Friend WithEvents TbObjectDescr As TextBox
    Friend WithEvents CombCountry As ComboBox
    Friend WithEvents TbCompanyCodeList As TextBox
    Friend WithEvents LblProfitcenter As Label
    Friend WithEvents TbProfitCenter As TextBox
    Friend WithEvents TbBusinessArea As TextBox
    Friend WithEvents LblBusinessArea As Label
    Friend WithEvents Splitter1 As Splitter
    Friend WithEvents TbTermOrgAssignmentNumber As TextBox
    Friend WithEvents LblTermOrgAssignmentNumber As Label
    Friend WithEvents LblTermOrgAssignmentValidFrom As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents TbBusEntityValidTo As TextBox
    Friend WithEvents TbBusEntityValidFrom As TextBox
    Friend WithEvents TbTermOrgAssignmentValidFrom As TextBox
    Friend WithEvents BtnHelp As Button
    Friend WithEvents DgvItems As DataGridView
    Friend WithEvents BusinessEntityREFXGetListResponseBindingSource As BindingSource
    Friend WithEvents BusinessEntityREFXGetListResponse1BindingSource As BindingSource
    Friend WithEvents BE_CompanyCode As DataGridViewTextBoxColumn
    Friend WithEvents BE_Number As DataGridViewTextBoxColumn
    Friend WithEvents BE_Name As DataGridViewTextBoxColumn
    Friend WithEvents TbTaxJurisd As TextBox
    Friend WithEvents LblTaxJurisd As Label
    Friend WithEvents BtnNextTerm As Button
    Friend WithEvents BtnPreviousTerm As Button
    Friend WithEvents LblActiveTerm As Label
    Friend WithEvents TbTermOrgAssignmentValidTo As TextBox
    Friend WithEvents LblTermOrgAssignmentValidTo As Label
    Friend WithEvents TbTermOrgAssignmentText As TextBox
End Class
