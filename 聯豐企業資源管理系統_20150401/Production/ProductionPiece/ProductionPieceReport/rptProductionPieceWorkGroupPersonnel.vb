Imports LFERP.DataSetting
Imports LFERP.Library.ProductionPieceWorkGroup

Public Class rptProductionPieceWorkGroupPersonnel
    Dim ds As New DataSet
    Dim Str_Choice As String 'D: �C��p��W��+�էO   B :�򥻭p��W��+�էO 

    Private Sub rptProductionPieceWorkGroupPersonnel_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Str_Choice = tempValue
        tempValue = Nothing
        '�[���t�O�W��
        Dim fc As New FacControler
        lueFacID.Properties.DisplayMember = "FacName"
        lueFacID.Properties.ValueMember = "FacID"
        lueFacID.Properties.DataSource = fc.GetFacList(strInFacID, Nothing)

        If Str_Choice = "B" Then
            Me.LabCaption.Text = "�էO�W��-�򥻪�"
        ElseIf Str_Choice = "D" Then
            Start_Date.EditValue = Format(Now, "yyyy/MM/dd")
            End_Date.EditValue = Format(Now, "yyyy/MM/dd")
            Me.LabCaption.Text = "�էO�W��-�C���"
        End If

        lueFacID.Focus()
        lueFacID.Select()


        ''--------------------�d�߮ɥΤ��v��------------------------
        If strInUserRank = "�Ͳ���" Then
            lueFacID.EditValue = strInFacIDFull
        ElseIf strInUserRank = "�޲z" Then
        ElseIf strInUserRank = "�έp" Then
            lueFacID.EditValue = strInFacIDFull
            lueFacID.EditValue = strInDepIDFull
        End If

    End Sub


    Private Sub gluDepID_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluDepID.EditValueChanged
      
        Dim gc As New ProductionPieceWorkGroupControl
        If gluDepID.EditValue Is Nothing Then Exit Sub

        lueG_NO.Properties.DisplayMember = "G_Name"
        lueG_NO.Properties.ValueMember = "G_NO"
        lueG_NO.Properties.DataSource = gc.ProductionPieceWorkGroup_GetList(Nothing, Nothing, Nothing, gluDepID.EditValue, Nothing, Nothing, Nothing, Nothing)

    End Sub

    Private Sub lueG_NO_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lueG_NO.EditValueChanged

    End Sub

    Private Sub lueFacID_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lueFacID.EditValueChanged
        Dim dc As New DepartmentControler
        If lueFacID.EditValue Is Nothing Then Exit Sub

        gluDepID.Properties.DisplayMember = "DepName"
        gluDepID.Properties.ValueMember = "DepID"
        gluDepID.Properties.DataSource = dc.BriName_GetList(strInDepID, Nothing, lueFacID.EditValue)
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click


        Dim ds As New DataSet

        Dim strG_NO, strFacID, strDepID, strStart_Date, strEnd_Date As String

        Dim strCompany As String

        strCompany = Mid(strInDPT_ID, 1, 4)   '��o�n���̩��ݤ��qID,�H��^���q�W�١ALOGO

        ''--------------------------------------------------

        If lueFacID.EditValue = Nothing Then
            strFacID = Nothing
        Else
            strFacID = lueFacID.EditValue
        End If

        If gluDepID.EditValue = "" Then
            strDepID = Nothing
        Else
            strDepID = gluDepID.EditValue
        End If
        ''----------------------------------------------
        If lueG_NO.EditValue = Nothing Then
            strG_NO = Nothing
        Else
            strG_NO = lueG_NO.EditValue
        End If



        ''-----------------------------------------------------------------
        If Start_Date.EditValue = Nothing Then
            strStart_Date = Nothing
        Else
            strStart_Date = Start_Date.EditValue
        End If

        If End_Date.EditValue = Nothing Then
            strEnd_Date = Nothing
        Else
            strEnd_Date = End_Date.EditValue
        End If
        ''------------------------------------------------------------------
        If Str_Choice = "B" Then
            ds.Tables.Clear()

            Dim ltc1 As New CollectionToDataSet
            Dim ltc2 As New CollectionToDataSet
            Dim ltc3 As New CollectionToDataSet

            Dim mcCompany As New LFERP.DataSetting.CompanyControler
            Dim ppp As New LFERP.Library.ProductionPiecePersonnel.ProductionPiecePersonnelControl
            Dim pwg As New ProductionPieceWorkGroupControl

            ds.Tables.Clear()

            ltc1.CollToDataSet(ds, "ProductionPieceWorkGroup", pwg.ProductionPieceWorkGroup_GetList(strG_NO, Nothing, Nothing, strDepID, strFacID, strStart_Date, strEnd_Date, Nothing))
            ltc2.CollToDataSet(ds, "ProductionPiecePersonnel", ppp.ProductionPiecePersonnel_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing))
            ltc3.CollToDataSet(ds, "Company", mcCompany.Company_Getlist(strCompany, Nothing, Nothing, Nothing))

            PreviewRPT(ds, "rptProductionPieceWorkGroupPersonnel", "�էO�W��", True, True)

            ltc1 = Nothing
            ltc2 = Nothing
            ltc3 = Nothing
        End If

        If Str_Choice = "D" Then
            ds.Tables.Clear()

            Dim ltc1 As New CollectionToDataSet
            Dim ltc2 As New CollectionToDataSet
            Dim ltc3 As New CollectionToDataSet

            Dim mcCompany As New LFERP.DataSetting.CompanyControler
            Dim pppd As New LFERP.Library.ProductionPiecePersonnelDay.ProductionPiecePersonnelDayControl
            Dim pwg As New ProductionPieceWorkGroupControl

            ds.Tables.Clear()
            ltc2.CollToDataSet(ds, "ProductionPiecePersonnel", pppd.ProductionPiecePersonnelDay_GetList(Nothing, Nothing, Nothing, Nothing, strG_NO, strDepID, strFacID, Nothing, strStart_Date, Nothing, Nothing, strEnd_Date, Nothing))

            ' ltc1.CollToDataSet(ds, "ProductionPieceWorkGroup", pwg.ProductionPieceWorkGroup_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))

            ltc3.CollToDataSet(ds, "Company", mcCompany.Company_Getlist(strCompany, Nothing, Nothing, Nothing))

            PreviewRPT(ds, "rptProductionPieceWorkGroupPersonnelDay", "�էO�C��W��", True, True)

            ltc1 = Nothing
            ltc2 = Nothing
            ltc3 = Nothing
        End If

        'Me.Close()
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub
End Class