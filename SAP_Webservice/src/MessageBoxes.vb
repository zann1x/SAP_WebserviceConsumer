Module MessageBoxes
    Public Sub ShowTimeoutErrorMessage()
        MessageBox.Show("Verbindungstimeout! Bitte überprüfen Sie Ihre Verbindung zum System", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Public Sub ShowSecurityErrorMessage()
        MessageBox.Show("Nicht autorisierter Zugriff! Bitte überprüfen Sie Ihre Anmeldedaten oder wenden Sie sich an den Systemadministrator.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Public Sub ShowGeneralErrorMessage(ByRef Ex As Exception)
        MessageBox.Show(Ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Public Sub ShowHelpMessage()
        MessageBox.Show("Pflichtfelder unabhängig des Buchungskreises:" + Environment.NewLine +
                        " * Buchungskreis" + Environment.NewLine +
                        " * Wirtschaftseinheit" + Environment.NewLine +
                        " * Bezeichnung der WE" + Environment.NewLine +
                        " * Mietrecht (s. Bezugsgrößen)" + Environment.NewLine +
                        "" + Environment.NewLine +
                        "Eingabehilfe:" + Environment.NewLine +
                        " * Allg. Datumsformat: YYYY-MM-DD", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
End Module
