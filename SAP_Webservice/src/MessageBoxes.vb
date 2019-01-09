Module MessageBoxes
    Public Sub ShowTimeoutErrorMessage()
        MessageBox.Show("Verbindungstimeout! Bitte überprüfen Sie Ihre Verbindung zum System", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Public Sub ShowSecurityErrorMessage()
        MessageBox.Show("Nicht autorisierter Zugriff! Bitte überprüfen Sie Ihre Anmeldedaten oder wenden Sie sich an den Systemadministrator.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Public Sub ShowExceptionMessage(ByRef Ex As Exception)
        MessageBox.Show(Ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Public Sub ShowErrorMessage(Text As String)
        MessageBox.Show(Text, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Public Sub ShowWarningMessage(Text As String)
        MessageBox.Show(Text, "Warnung", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

    Public Sub ShowInfoMessage(Text As String)
        MessageBox.Show(Text, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Public Sub ShowCustomMessage(Text As String, Title As String, Buttons As MessageBoxButtons, Icon As MessageBoxIcon)
        MessageBox.Show(Text, Title, Buttons, Icon)
    End Sub

    Public Sub ShowHelpMessage()
        MessageBox.Show("Pflichtfelder unabhängig des Buchungskreises:" + Environment.NewLine +
                        " * Buchungskreis" + Environment.NewLine +
                        " * Wirtschaftseinheit" + Environment.NewLine +
                        " * Bezeichnung der WE" + Environment.NewLine +
                        " * Mietrecht (s. Bezugsgrößen)" + Environment.NewLine +
                        "" + Environment.NewLine +
                        "Eingabehilfe:" + Environment.NewLine +
                        " * Allg. Datumsformat: YYYY-MM-DD", "Hilfe", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
End Module
