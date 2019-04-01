Imports LFERP.DataSetting
Imports LFERP.Library.ProductionPieceWorkGroup

Public Class rptProductionPiecePersonnel
    Dim Str_Choice As String
    Dim ds As New DataSet

    Private Sub rptProductionPiecePersonnel_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Str_Choice = tempValue
        tempValue = Nothing

        CreateTable()
        AddRow()

        '�[���t�O�W��
        Dim fc As New FacControler
        lueFacID.Properties.DisplayMember = "FacName"
        lueFacID.Properties.ValueMember = "FacID"
        lueFacID.Properties.DataSource = fc.GetFacList(strInFacID, Nothing)

        
        If Str_Choice = "B" Then
            LabCaption.Text = "�ӤH�p��W��-�򥻪�"
        ElseIf Str_Choice = "D" Then
            LabCaption.Text = "�ӤH�p��W��-�C���"
            rdbPer_NOResign.Enabled = False
            rdbPer_Resign.Enabled = False
            rdbPer_All.Enabled = False

            Start_Date.Text = Format(Now, "yyyy/MM/dd")
            End_Date.Text = Format(Now, "yyyy/MM/dd")
        End If

        ''--------------------�d�߮ɥΤ��v��------------------------
        If strInUserRank = "�Ͳ���" Then
            lueFacID.EditValue = strInFacIDFull
        ElseIf strInUserRank = "�޲z" Then
        ElseIf strInUserRank = "�έp" Then
            lueFacID.EditValue = strInFacIDFull
            gluDepID.EditValue = strInDepIDFull
        End If

    End Sub

    Sub CreateTable()
        ds.Tables.Clear()
        With ds.Tables.Add("Type")
            .Columns.Add("Per_PayType")
            .Columns.Add("Remark")
        End With
        luePer_PayType.Properties.DataSource = ds.Tables("Type")
        luePer_PayType.Properties.DisplayMember = "Per_PayType"
        luePer_PayType.Properties.ValueMember = "Per_PayType"
    End Sub

    Sub AddRow()
        Dim row As DataRow
        ds.Tables("Type").Clear()
        row = ds.Tables("Type").NewRow()
        row("Per_PayType") = "�p��"
        row("Remark") = "�u�ǭp����"
        ds.Tables("Type").Rows.Add(row)
        row = ds.Tables("Type").NewRow()
        row("Per_PayType") = "�p��"
        row("Remark") = "���ɤ�CNC�p����"
        ds.Tables("Type").Rows.Add(row)
        row = ds.Tables("Type").NewRow()
        row("Per_PayType") = "���~"
        row("Remark") = "�ӥ]�դ����~���u"
        ds.Tables("Type").Rows.Add(row)
    End Sub

    Private Sub lueFacID_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lueFacID.EditValueChanged
        Dim dc As New DepartmentControler
        If lueFacID.EditValue Is Nothing Then Exit Sub

        gluDepID.Properties.DisplayMember = "DepName"
        gluDepID.Properties.ValueMember = "DepID"
        gluDepID.Properties.DataSource = dc.BriName_GetList(strInDepID, Nothing, lueFacID.EditValue)
    End Sub

    Private Sub gluDepID_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluDepID.EditValueChanged
        Dim gc As New ProductionPieceWorkGroupControl
        If gluDepID.EditValue Is Nothing Then Exit Sub

        'lueG_NO.Properties.DisplayMember = "G_Name"
        'lueG_NO.Properties.ValueMember = "G_NO"
        'lueG_NO.Properties.DataSource = gc.ProductionPieceWorkGroup_GetList(Nothing, Nothing, Nothing, gluDepID.EditValue, Nothing, Nothing, Nothing, Nothing)

    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub


    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click


        Dim ds As New DataSet

        Dim strPer_NO, strPer_Name, strG_NO, strFacID, strDepID, strStart_Date, strEnd_Date, strPer_PayType, strPer_NOResign As String

        Dim strCompany As String

        strCompany = Mid(strInDPT_ID, 1, 4)   '��o�n���̩��ݤ��qID,�H��^���q�W�١ALOGO


        If txtPer_NO.Text = "" Then
            strPer_NO = Nothing
        Else
            strPer_NO = txtPer_NO.EditValue
        End If

        If txtPer_Name.Text = "" Then
            strPer_Name = Nothing
        Else
            strPer_Name = txtPer_Name.Text.Trim
        End If
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
        'If lueG_NO.EditValue = Nothing Then
        '    strG_NO = Nothing
        'Else
        '    strG_NO = lueG_NO.EditValue
        'End If

        strG_NO = "�L"

        If luePer_PayType.EditValue = "" Then
            strPer_PayType = Nothing
        Else
            strPer_PayType = luePer_PayType.EditValue
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

        strPer_NOResign = Nothing

        If rdbPer_NOResign.Checked = True Then
            strPer_NOResign = "False"
        ElseIf rdbPer_Resign.Checked = True Then
            strPer_NOResign = "True"
        ElseIf rdbPer_All.Checked = True Then
            strPer_NOResign = Nothing
        End If
        ds.Tables.Clear()
        Dim ltc1 As New CollectionToDataSet
        Dim ltc2 As New CollectionToDataSet

        Dim mcCompany As New LFERP.DataSetting.CompanyControler
        Dim ppp As New LFERP.Library.ProductionPiecePersonnel.ProductionPiecePersonnelControl
        Dim pppd As New LFERP.Library.ProductionPiecePersonnelDay.ProductionPiecePersonnelDayControl

        ds.Tables.Clear()
        If Str_Choice = "B" Then

            If ppp.ProductionPiecePersonnel_GetList(Nothing, strPer_NO, Nothing, strG_NO, strDepID, strFacID, strPer_PayType, strStart_Date, Nothing, strPer_NOResign, strEnd_Date, Nothing).Count <= 0 Then
                MsgBox("�򥻪��L�ӤH�p��W��!")
                Exit Sub
            End If
            ltc1.CollToDataSet(ds, "ProductionPiecePersonnel", ppp.ProductionPiecePersonnel_GetList(Nothing, strPer_NO, Nothing, strG_NO, strDepID, strFacID, strPer_PayType, strStart_Date, Nothing, strPer_NOResign, strEnd_Date, Nothing))
            ltc2.CollToDataSet(ds, "Company", mcCompany.Company_Getlist(strCompany, Nothing, Nothing, Nothing))
            PreviewRPT(ds, "rptProductionPiecePersonnel", "�ӤH�p��W��-�򥻪�", True, True)

        ElseIf Str_Choice = "D" Then

            If pppd.ProductionPiecePersonnelDay_GetList(Nothing, Nothing, strPer_NO, Nothing, strG_NO, strDepID, strFacID, strPer_PayType, strStart_Date, Nothing, "False", strEnd_Date, Nothing).Count <= 0 Then
                MsgBox("�C����L�ӤH�p��W��")
                Exit Sub
            End If
            ltc1.CollToDataSet(ds, "ProductionPiecePersonnel", pppd.ProductionPiecePersonnelDay_GetList(Nothing, Nothing, strPer_NO, Nothing, strG_NO, strDepID, strFacID, strPer_PayType, strStart_Date, Nothing, "False", strEnd_Date, Nothing))
            ltc2.CollToDataSet(ds, "Company", mcCompany.Company_Getlist(strCompany, Nothing, Nothing, Nothing))
            PreviewRPT(ds, "rptProductionPiecePersonnelDay", "�ӤH�p��W��-�C���", True, True)
        End If

        ltc2 = Nothing
        ltc1 = Nothing

        Me.Close()


    End Sub
End Class