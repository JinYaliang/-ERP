Imports LFERP.DataSetting
Imports LFERP.Library.ProductionPiecePersonnel
Imports LFERP.Library.ProductionPieceWorkGroup

Imports LFERP.Library.ProductionPiecePayWGMain
Imports LFERP.Library.ProductionPiecePayWGSub
Imports LFERP.Library.ProductionPiecePayPersonnel


Public Class frmProductionPiecePayFind
    Dim Load_OK As String ''�T�wLoad�ƥ�O�_�w���J����
    Dim Str_Choice As String

    Dim ds As New DataSet
    Dim dsF As New DataSet

    Private Sub frmProductionPiecePayPersonnelFind_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '�[���t�O�W��
        Str_Choice = tempValue2
        GluG_NO.Text = tempValue8

        tempValue2 = Nothing
        tempValue8 = Nothing

        Dim fc As New FacControler
        lueFacID.Properties.DisplayMember = "FacName"
        lueFacID.Properties.ValueMember = "FacID"
        lueFacID.Properties.DataSource = fc.GetFacList(strInFacID, Nothing)

        DatePL_YYMM.EditValue = Format(Now, "yyyy�~MM��")

        If InStr(Str_Choice, "���L", CompareMethod.Text) > 0 Then
            btnFind.Text = "���L(&P)"
            GroupBox1.Text = "���L����"
        Else
            btnFind.Text = "�d��(&F)"
            GroupBox1.Text = "�d�߱���"
        End If

        If InStr(Str_Choice, "�ӤH", CompareMethod.Text) > 0 Then '�����ӤH  
            G_NO.FieldName = "Per_NO"
            G_Name.FieldName = "Per_Name"

            G_NO.Caption = "�t���s��"
            G_Name.Caption = "�m�W"

            LabG_NO.Text = "�t���s��(&I)"
            GluG_NO.Properties.ValueMember = "Per_NO"
            GluG_NO.Properties.DisplayMember = "Per_NOName"

            If Str_Choice = "�ӤH�~���d��" Or Str_Choice = "�ӤH�~�����L" Then
                ComboCheck.Text = "�w�f��"
            End If

        Else
            GluG_NO.Properties.ValueMember = "G_NO"
            GluG_NO.Properties.DisplayMember = "G_NOName"
        End If

        LabCaption.Text = Str_Choice
        Me.Text = Str_Choice

        lueFacID.Focus()
        lueFacID.Select()

        Load_OK = "OK"

        ''--------------------�d�߮ɥΤ��v��------------------------
        If strInUserRank = "�Ͳ���" Then
            lueFacID.EditValue = strInFacIDFull
        ElseIf strInUserRank = "�޲z" Then
        ElseIf strInUserRank = "�έp" Then
            lueFacID.EditValue = strInFacIDFull
            lueDepID.EditValue = strInDepIDFull
        End If

    End Sub

    Private Sub lueFacID_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lueFacID.EditValueChanged
        If lueFacID.EditValue Is Nothing Then Exit Sub

        Dim dc As New DepartmentControler
        lueDepID.Properties.DisplayMember = "DepName"
        lueDepID.Properties.ValueMember = "DepID"
        lueDepID.Properties.DataSource = dc.BriName_GetList(strInDepID, Nothing, lueFacID.EditValue)
    End Sub



    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click

        If lueFacID.EditValue Is Nothing Then
            tempValue3 = Nothing
        Else
            tempValue3 = lueFacID.EditValue
        End If

        If lueDepID.EditValue Is Nothing Then
            tempValue4 = Nothing
        Else
            tempValue4 = lueDepID.EditValue
        End If

        If GluG_NO.Text = "" Then
            tempValue5 = Nothing
        Else
            tempValue5 = GluG_NO.EditValue
        End If

        If DatePL_YYMM.EditValue Is Nothing Then
            tempValue6 = Nothing
        Else
            tempValue6 = Format(CDate(DatePL_YYMM.EditValue), "yyyy/MM")
        End If

        If ComboCheck.Text = "����" Then
            tempValue7 = Nothing
        ElseIf ComboCheck.Text = "�w�f��" Then
            tempValue7 = "True"
        ElseIf ComboCheck.Text = "���f��" Then
            tempValue7 = "False"
        End If

        ''-----------------------------------------------------------------

        If InStr(Str_Choice, "�d��", CompareMethod.Text) > 0 Then
            tempValue = "F"
            Me.Close()
        ElseIf InStr(Str_Choice, "���L", CompareMethod.Text) > 0 Then

        Else
            Exit Sub
        End If

        Select Case Str_Choice
            Case "�էO�p���~�����L"
                ''���L �էO�p��u��
                ds.Tables.Clear()
                Dim ltc1 As New CollectionToDataSet
                Dim ltc2 As New CollectionToDataSet
                Dim ltc3 As New CollectionToDataSet


                Dim mcCompany As New LFERP.DataSetting.CompanyControler
                Dim WGppM As New LFERP.Library.ProductionPiecePayWGMain.ProductionPiecePayWGMainControl
                Dim WGppS As New LFERP.Library.ProductionPiecePayWGSub.ProductionPiecePayWGSubControl

                Dim strCompany As String
                strCompany = Mid(strInDPT_ID, 1, 4)   '��o�n���̩��ݤ��qID,�H��^���q�W�١ALOGO

                ds.Tables.Clear()

                ltc1.CollToDataSet(ds, "ProductionPiecePayWGMain", WGppM.ProductionPiecePayWGMain_GetList(Nothing, Nothing, tempValue5, tempValue6, Nothing, Nothing, tempValue4, tempValue3, Nothing, Nothing, tempValue7))
                ltc2.CollToDataSet(ds, "ProductionPiecePayWGSub", WGppS.ProductionPiecePayWGSub_GetList(Nothing, Nothing, Nothing, Nothing))
                ltc3.CollToDataSet(ds, "Company", mcCompany.Company_Getlist(strCompany, Nothing, Nothing, Nothing))
                PreviewRPT(ds, "rptProductionPiecePayWG", "�էO�p��u��", True, True)

                ltc1 = Nothing
                ltc2 = Nothing
                ltc3 = Nothing

            Case "�ӤH�p���~�����L"
                ds.Tables.Clear()
                Dim ltc1 As New CollectionToDataSet
                Dim ltc2 As New CollectionToDataSet

                Dim mcCompany As New LFERP.DataSetting.CompanyControler
                Dim Ppp As New LFERP.Library.ProductionPiecePayPersonnel.ProductionPiecePayPersonnelContol

                Dim strCompany As String
                strCompany = Mid(strInDPT_ID, 1, 4)   '��o�n���̩��ݤ��qID,�H��^���q�W�١ALOGO

                ds.Tables.Clear()

                ltc1.CollToDataSet(ds, "ProductionPiecePayPersonnel", Ppp.ProductionPiecePayPersonnel_GetList(Nothing, tempValue5, Nothing, tempValue6, tempValue4, tempValue7, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, tempValue3, Nothing))
                ltc2.CollToDataSet(ds, "Company", mcCompany.Company_Getlist(strCompany, Nothing, Nothing, Nothing))
                PreviewRPT(ds, "RPTProductionPiecePayPersonnel", "�ӤH�p��u��", True, True)

                ltc1 = Nothing
                ltc2 = Nothing

            Case "�ӤH�~�����L"

                Dim ltc1 As New CollectionToDataSet
                Dim ltc2 As New CollectionToDataSet
                Dim ltc3 As New CollectionToDataSet
                Dim ltc4 As New CollectionToDataSet
                Dim ltc5 As New CollectionToDataSet


                Dim mcCompany As New LFERP.DataSetting.CompanyControler
                Dim payp As New LFERP.Library.ProductionPayPersonnel.ProductionPayPersonnelContol
                Dim pppayp As New LFERP.Library.ProductionPiecePayPersonnel.ProductionPiecePayPersonnelContol
                Dim ppwgMain As New LFERP.Library.ProductionPiecePayWGMain.ProductionPiecePayWGMainControl
                Dim ppwgSub As New LFERP.Library.ProductionPiecePayWGSub.ProductionPiecePayWGSubControl

                Dim strCompany As String
                strCompany = Mid(strInDPT_ID, 1, 4)   '��o�n���̩��ݤ��qID,�H��^���q�W�١ALOGO

                ds.Tables.Clear()

                If payp.ProductionPieceMeritedPaySum_View(tempValue6, tempValue4, tempValue3, tempValue5, tempValue7).Count <= 0 Then
                    MsgBox("���w���󤺵L�O���s�b!")
                    Exit Sub
                End If

                ltc1.CollToDataSet(ds, "Company", mcCompany.Company_Getlist(strCompany, Nothing, Nothing, Nothing))
                ltc2.CollToDataSet(ds, "ProductionPayPersonnel", payp.ProductionPieceMeritedPaySum_View(tempValue6, tempValue4, tempValue3, tempValue5, tempValue7))
                ltc3.CollToDataSet(ds, "ProductionPiecePayPersonnel", pppayp.ProductionPiecePayPersonnel_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, tempValue7, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))

                ltc4.CollToDataSet(ds, "ProductionPiecePayWGMain", ppwgMain.ProductionPiecePayWGMain_GetList(Nothing, Nothing, Nothing, tempValue6, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, tempValue7))
                ltc5.CollToDataSet(ds, "ProductionPiecePayWGSub", ppwgSub.ProductionPiecePayWGSub_GetList(Nothing, Nothing, tempValue5, Nothing))

                ltc1 = Nothing
                ltc2 = Nothing
                ltc3 = Nothing
                ltc4 = Nothing
                ltc5 = Nothing

                PreviewRPT(ds, "rptProductionPayPersonnel", "�ӤH�p��u��", True, True)

        End Select

        Me.Close()

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub lueDepID_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lueDepID.EditValueChanged
        If lueDepID.EditValue Is Nothing Then Exit Sub

        If InStr(Str_Choice, "�ӤH", CompareMethod.Text) > 0 Then
            Dim Pppp As New ProductionPiecePersonnelControl
            GluG_NO.Properties.DataSource = Pppp.ProductionPiecePersonnel_GetList(Nothing, Nothing, Nothing, Nothing, lueDepID.EditValue, lueFacID.EditValue, Nothing, Nothing, Nothing, "False", Nothing, Nothing)
        Else
            Dim Pppp As New ProductionPieceWorkGroupControl
            GluG_NO.Properties.DataSource = Pppp.ProductionPieceWorkGroup_GetList(Nothing, Nothing, Nothing, lueDepID.EditValue, Nothing, Nothing, Nothing, Nothing)

        End If
    End Sub

End Class