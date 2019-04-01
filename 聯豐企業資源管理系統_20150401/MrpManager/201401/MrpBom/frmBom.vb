Imports System
Imports LFERP.SystemManager
Imports LFERP.SystemManager.SystemUser
Imports LFERP.Library.MrpManager.Bom_M
Imports LFERP.Library.MrpManager.Bom_D
Imports LFERP.Library.MrpManager.MrpMaterialCode
Public Class frmBom

#Region "�r�q�ݩ�"
    Public BomType As String
    Public BomMCode As String
    Public BomVersion As String
    Public BomAutoID As String
    Public BomCheckValue As Boolean
    Public BomCheckRemark As String

    Dim ds As New DataSet
    Dim bmm As New Bom_MController
    Dim bmd As New Bom_DController
    Dim MMICcon As New MrpMaterialCodeController
    'Dim mcc As New MCodeController

    Public Property GetCheckRemark() As String    ''''����f�ֳƵ�
        Get
            Return BomCheckRemark
        End Get
        Set(ByVal value As String)
            BomCheckRemark = value
        End Set
    End Property
    Public Property GetCheck() As Boolean    ''''�O�_�f��
        Get
            Return BomCheckValue
        End Get
        Set(ByVal value As Boolean)
            BomCheckValue = value
        End Set
    End Property
    Private Property GetAutoID() As String    ''''��������ƪ��ߤ@��AutoID
        Get
            Return BomAutoID
        End Get
        Set(ByVal value As String)
            BomAutoID = value
        End Set
    End Property
    Private Property operateType() As String    ''''����W�@��檺�ާ@�����B�s�W�A�ק�A�f��
        Get
            Return BomType
        End Get
        Set(ByVal value As String)
            BomType = value
        End Set
    End Property
    Private Property GetCode() As String    ''''������ƽs�X
        Get
            Return BomMCode
        End Get
        Set(ByVal value As String)
            BomMCode = value
        End Set
    End Property
    Public Property GetVersion() As String    ''''���������
        Get
            Return BomVersion
        End Get
        Set(ByVal value As String)
            BomVersion = value
        End Set
    End Property
#End Region

    Private Sub frmBomSub_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        GridMCode.DataSource = MMICcon.MrpMaterialCode_GetList(Nothing, Nothing, Nothing, Nothing, Nothing)
        gluM_Code.Properties.DisplayMember = "M_Code"
        gluM_Code.Properties.ValueMember = "M_Code"
        gluM_Code.Properties.DataSource = GridMCode.DataSource
        GridLookUpEdit1View.ActiveFilterString = "M_Code not like '1%' and M_Code not like '2%'"
        createTable()

        Select Case operateType
            Case "BomAdd"
                lblinfo.Text = "Bom���~���c�X�X�s�W"
                Me.Text = "Bom���~���c�X�X�s�W"
                startDate.Text = Format(Date.Today.Date, "yyyy/MM/dd")
                'endDate.Text = String.Empty
                GetEnable(True, False)
                startDate.Enabled = True
                endDate.Enabled = False

                EffectiveDate.OptionsColumn.ReadOnly = False
                InvalidDate.OptionsColumn.ReadOnly = True

                tab.TabPages.Remove(tabCheck)
                Exit Sub
            Case "BomEdit"
                lblinfo.Text = "Bom���~���c�X�X�ק�"
                Me.Text = "Bom���~���c�X�X�ק�"
                tab.TabPages.Remove(tabCheck)
                GetEnable(True, False)
                startDate.Enabled = False
                endDate.Enabled = True
                gluM_Code.Enabled = False

                EffectiveDate.OptionsColumn.ReadOnly = True
                InvalidDate.OptionsColumn.ReadOnly = False

            Case "BomView"
                lblinfo.Text = "Bom���~���c�X�X�d��"
                Me.Text = "Bom���~���c�X�X�d��"
                BomCheck.Checked = GetCheck
                MemoCheck.Text = GetCheckRemark
                okButton.Enabled = False
                GetEnable(False, False)
            Case "BomCheck"
                Dim uu As New SystemUserController
                tab.SelectedTabPage = tabCheck
                lblinfo.Text = "Bom���~���c�X�X�f��"
                Me.Text = "Bom���~���c�X�X�f��"
                BomCheck.Checked = GetCheck
                MemoCheck.Text = GetCheckRemark
                GetEnable(False, False)
                lblCheckDate.Text = Date.Today.Date
                Dim us As SystemUserInfo = uu.SystemUser_Get(InUserID)
                lblCheckID.Text = us.U_Name
                gluM_Code.Enabled = False
        End Select
        LoadData()
    End Sub

    ''' <summary>
    ''' �]�m�U���󪺥i���ݩ�
    ''' </summary>
    ''' <param name="a"></param>
    ''' <param name="b"></param>
    ''' <remarks></remarks>
    Private Sub GetEnable(ByVal a As Boolean, ByVal b As Boolean)
        gluM_Code.Enabled = a
        txtName.Enabled = b
        txtGauge.Enabled = b
        txtUnit.Enabled = b
        txtVersion.Enabled = a
        txtSource.Enabled = b
        startDate.Enabled = a
        endDate.Enabled = a
        GridView2.OptionsBehavior.Editable = a
    End Sub

    ''' <summary>
    ''' �[���ƾ�
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadData()
        '--------------------------------------------------
        Dim bmminfo As New List(Of Bom_MInfo)
        bmminfo = bmm.Bom_M_GetList(GetCode, GetVersion, Nothing, Nothing, Nothing, Nothing)
        gluM_Code.Text = bmminfo(0).ParentGroup
        txtName.Text = bmminfo(0).M_Name
        txtGauge.Text = bmminfo(0).M_Gauge
        txtUnit.Text = bmminfo(0).M_Unit
        txtVersion.Text = bmminfo(0).Version.Replace("V", "")
        txtSource.Text = bmminfo(0).M_Source
        startDate.Text = bmminfo(0).EffectiveDate
        endDate.Text = bmminfo(0).InvalidDate
        '-----------------�r��ƾڥ[��---------------------------------
        Dim bmdinfo As New List(Of Bom_DInfo)
        bmdinfo = bmd.Bom_D_GetList(GetCode)

        Dim i As Integer = 0
        For i = 0 To bmdinfo.Count - 1
            Dim dr As DataRow = ds.Tables("Bom_Detail").NewRow
            dr("Item") = bmdinfo(i).Item
            dr("ChildGroup") = bmdinfo(i).ChildGroup
            dr("ChildName") = bmdinfo(i).ChildName
            dr("ChildGauge") = bmdinfo(i).ChildGauge

            dr("ChildMC_Source") = bmdinfo(i).ChildMC_Source

            dr("IsUnfold") = bmdinfo(i).IsUnfold
            dr("ReplaceType1") = bmdinfo(i).ReplaceType1
            dr("UseFeatures") = bmdinfo(i).UseFeatures
            dr("EffectiveDate") = bmdinfo(i).EffectiveDate
            dr("InvalidDate") = bmdinfo(i).InvalidDate
            dr("Mount") = bmdinfo(i).Mount
            dr("Tmrtc") = bmdinfo(i).Tmrtc
            dr("SendUnit") = bmdinfo(i).SendUnit
            dr("LossRate") = bmdinfo(i).LossRate
            dr("AutoID") = bmdinfo(i).AutoID
            ds.Tables("Bom_Detail").Rows.Add(dr)
        Next

    End Sub

    ''' <summary>
    ''' �T�w���s�ާ@
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub okButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles okButton.Click
        Select Case operateType
            Case "BomAdd"
                If DataEmptyCheck() = True Then
                    DataAdd()
                    Me.Close()
                End If
            Case "BomEdit"
                If DataEmptyCheck() = True Then
                    DataEdit()
                    Me.Close()
                End If
            Case "BomCheck"
                If DataEmptyCheck() = True Then
                    DataCheck()
                    Me.Close()
                End If
        End Select
    End Sub

    ''' <summary>
    ''' �s�W�ާ@
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DataAdd()
        Dim bminfo As New Bom_MInfo
        bminfo.CheckBit = False
        bminfo.CreateDate = Date.Now
        bminfo.CreateUserID = InUserID
        bminfo.EffectiveDate = startDate.EditValue
        bminfo.InvalidDate = endDate.EditValue
        bminfo.M_Gauge = txtGauge.Text
        bminfo.ParentGroup = gluM_Code.EditValue
        bminfo.M_Unit = txtUnit.Text
        bminfo.SourceID = txtSource.Text
        bminfo.Version = txtVersion.Text
        Try
            bmm.Bom_M_Add(bminfo)
        Catch ex As Exception
            MsgBox("�s�W����", 64, "����")
            Exit Sub
        End Try
        Dim bdinfo As New Bom_DInfo
        Dim i As Integer = 0
        For i = 0 To ds.Tables("Bom_Detail").Rows.Count - 1
            With ds.Tables("Bom_Detail")
                bdinfo.ChildGroup = .Rows(i)("ChildGroup").ToString
                bdinfo.CreateDate = Date.Now
                bdinfo.CreateUserID = InUserID
                bdinfo.EffectiveDate = .Rows(i)("EffectiveDate")
                bdinfo.InvalidDate = .Rows(i)("InvalidDate")
                bdinfo.IsUnfold = .Rows(i)("IsUnfold")
                bdinfo.Item = .Rows(i)("Item").ToString
                bdinfo.LossRate = .Rows(i)("LossRate")
                bdinfo.Mount = .Rows(i)("Mount")
                bdinfo.ParentGroup = gluM_Code.EditValue
                If .Rows(i)("ReplaceType1").ToString = "���`(normal)" Then
                    bdinfo.ReplaceType = "0"
                ElseIf .Rows(i)("ReplaceType1").ToString = "���N(UTE)" Then
                    bdinfo.ReplaceType = "1"
                ElseIf .Rows(i)("ReplaceType1").ToString = "���N(SUB)" Then
                    bdinfo.ReplaceType = "2"
                End If
                bdinfo.SendUnit = .Rows(i)("SendUnit").ToString
                bdinfo.Tmrtc = .Rows(i)("Tmrtc")
                bdinfo.UseFeatures = .Rows(i)("UseFeatures").ToString
            End With
            Try
                bmd.Bom_D_Add(bdinfo)
            Catch ex As Exception
                MsgBox("�s�W����", 64, "����")
                Exit Sub
            End Try
        Next
        MsgBox("�s�W���\", 64, "����")
    End Sub

    ''' <summary>
    ''' �ק�ާ@
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DataEdit()
        Dim bminfo As New Bom_MInfo
        bminfo.Version = txtVersion.Text
        bminfo.SourceID = txtSource.Text
        bminfo.EffectiveDate = startDate.Text

        If Me.endDate.Text = String.Empty Then
            bminfo.InvalidDate = Nothing
        Else
            bminfo.InvalidDate = Me.endDate.Text
        End If

        bminfo.ModifyUserID = InUserID
        bminfo.AutoID = GetAutoID
        bminfo.ModifyDate = Date.Now
        Try
            bmm.Bom_M_Update(bminfo)
        Catch ex As Exception
            MsgBox("�ק異��", 64, "����")
            Exit Sub
        End Try
        Dim i As Integer = 0
        For i = 0 To ds.Tables("AutoID").Rows.Count - 1
            With ds.Tables("AutoID")
                bmd.Bom_D_Delete(.Rows(i)("AutoID"), Nothing)
            End With
        Next
        Dim j As Integer = 0
        Dim bdinfo As New Bom_DInfo
        For j = 0 To ds.Tables("Bom_Detail").Rows.Count - 1
            With ds.Tables("Bom_Detail")
                bdinfo.ChildGroup = .Rows(j)("ChildGroup").ToString
                bdinfo.EffectiveDate = .Rows(j)("EffectiveDate")
                bdinfo.InvalidDate = Nothing
                bdinfo.IsUnfold = .Rows(j)("IsUnfold")
                bdinfo.Item = .Rows(j)("Item").ToString
                bdinfo.LossRate = .Rows(j)("LossRate")
                bdinfo.Mount = .Rows(j)("Mount")
                If .Rows(j)("ReplaceType1").ToString = "���`(normal)" Then
                    bdinfo.ReplaceType = "0"
                ElseIf .Rows(j)("ReplaceType1").ToString = "���N(UTE)" Then
                    bdinfo.ReplaceType = "1"
                ElseIf .Rows(j)("ReplaceType1").ToString = "���N(SUB)" Then
                    bdinfo.ReplaceType = "2"
                End If
                bdinfo.SendUnit = .Rows(j)("SendUnit").ToString
                bdinfo.Tmrtc = .Rows(j)("Tmrtc")
                bdinfo.UseFeatures = .Rows(j)("UseFeatures").ToString
                If .Rows(j)("AutoID").ToString.Trim = "" Then
                    bdinfo.ParentGroup = gluM_Code.EditValue
                    bdinfo.CreateDate = Date.Now
                    bdinfo.CreateUserID = InUserID
                    Try
                        bmd.Bom_D_Add(bdinfo)
                    Catch ex As Exception
                        MsgBox("�ק異��", 64, "����")
                        Exit Sub
                    End Try
                Else
                    bdinfo.AutoID = .Rows(j)("AutoID").ToString
                    bdinfo.ModifyDate = Date.Now
                    bdinfo.ModifyUserID = InUserID
                    Try
                        bmd.Bom_D_Update(bdinfo)
                    Catch ex As Exception
                        MsgBox("�ק異��", 64, "����")
                        Exit Sub
                    End Try
                End If
            End With
        Next
        MsgBox("�ק令�\", 64, "����")
    End Sub

    ''' <summary>
    ''' �f�־ާ@
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DataCheck()
        Dim bminfo As New Bom_MInfo
        bminfo.CheckDate = Date.Now
        bminfo.CheckUserID = InUserID
        bminfo.CheckBit = BomCheck.Checked
        bminfo.CheckRemark = MemoCheck.Text
        bminfo.AutoID = GetAutoID
        Try
            bmm.Bom_M_Check(bminfo)
        Catch ex As Exception
            MsgBox("�f�֥���", 64, "����")
        End Try
    End Sub

    ''' <summary>
    ''' �ƾڤ���������
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DataEmptyCheck() As Boolean
        DataEmptyCheck = False
        If gluM_Code.Text.Trim = "" Then
            MsgBox("�п�ܲ��~�s�X", 64, "����")
            gluM_Code.Focus()
            Exit Function
        End If
        If txtVersion.Text.Trim = "" Then
            MsgBox("���������\����", 64, "����")
            txtVersion.Focus()
            Exit Function
        End If
        'If txtSource.Text.Trim = "" Then
        '    MsgBox("�ӷ��X�����\����", 64, "����")
        '    txtSource.Focus()
        '    Exit Function
        'End If
        'If startDate.EditValue > endDate.EditValue Then
        '    MsgBox("�ͮĤ���j�󥢮Ĥ��", 64, "����")
        '    startDate.Focus()
        '    Exit Function
        'End If

        Dim intTableCount As Integer = ds.Tables("Bom_Detail").Rows.Count
        If intTableCount <= 0 Then
            MsgBox("���Ʃ��Ӫ�S���ƾڵL�k�O�s", 64, "����")
            Exit Function
        End If

        Dim i As Integer = 0
        For i = 0 To ds.Tables("Bom_Detail").Rows.Count - 1
            With ds.Tables("Bom_Detail")
                If .Rows(i)("ChildGroup").ToString.Trim = "" Then
                    MsgBox("���Ʃ��Ӳ�" + (i + 1).ToString + "�� ����ƥ� �����\����", 64, "����")
                    Exit Function
                End If
                If .Rows(i)("Mount") = 0 Then
                    MsgBox("���Ʃ��Ӳ�" + (i + 1).ToString + "�� �զ��ζq �����\��0", 64, "����")
                    Exit Function
                End If
                If .Rows(i)("Tmrtc") = 0 Then
                    MsgBox("���Ʃ��Ӳ�" + (i + 1).ToString + "�� �D�󩳼� �����\��0", 64, "����")
                    Exit Function
                End If
                If gluM_Code.Text = .Rows(i)("ChildGroup").ToString Then
                    MsgBox("���Ʃ��Ӳ�" + (i + 1).ToString + "�� ���~�s���P����ۦP���d", 64, "����")
                    Exit Function
                End If
                'If .Rows(i)("LossRate") = 0 Then
                '    MsgBox("���Ʃ��Ӳ�" + (i + 1).ToString + "�� �l�v �����\��0", 64, "����")
                '    Exit Function
                'End If
            End With
        Next

        Dim bdc As New Bom_DController
        Dim bmc As New Bom_MController
        If bdc.Mrp_BomNestedCheck = True Then
            bdc.Bom_D_Delete(Nothing, gluM_Code.EditValue.ToString)
            bmc.Bom_M_Delete(Nothing, gluM_Code.EditValue.ToString)
            Exit Function
        End If
        DataEmptyCheck = True
    End Function

    ''' <summary>
    ''' ���Ʃ��ӷs�W
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BomDetailAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BomDetailAdd.Click
        Dim dr As DataRow
        dr = LoadTable()
        ds.Tables("Bom_Detail").Rows.Add(dr)
        GridDetail.DataSource = ds.Tables("Bom_Detail")
        FreshGridview()
    End Sub

    ''' <summary>
    ''' ���Ʃ��ӧR��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BomDetailDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BomDetailDel.Click

        Dim num As String
        num = GridView2.GetFocusedRowCellValue("Item")

        Dim drow1 As DataRow = ds.Tables("AutoID").NewRow
        drow1("AutoID") = GridView2.GetFocusedRowCellValue("AutoID")
        ds.Tables("AutoID").Rows.Add(drow1)

        Dim drow2 As DataRow
        drow2 = ds.Tables("Bom_Detail").Rows(num - 1)
        ds.Tables("Bom_Detail").Rows.Remove(drow2)
        FreshGridview()

    End Sub

    ''' <summary>
    ''' �Ыص�����
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub createTable()
        ds.Tables.Clear()
        With ds.Tables.Add("Bom_Detail")
            .Columns.Add("Item", GetType(String))
            .Columns.Add("ChildGroup", GetType(String))
            .Columns.Add("ChildName", GetType(String))
            .Columns.Add("ChildGauge", GetType(String))
            .Columns.Add("ChildMC_Source", GetType(String))
            .Columns.Add("IsUnfold", GetType(Boolean))
            .Columns.Add("ReplaceType1", GetType(String))
            .Columns.Add("UseFeatures", GetType(String))
            .Columns.Add("EffectiveDate", GetType(String))
            .Columns.Add("InvalidDate", GetType(String))
            .Columns.Add("Mount", GetType(Decimal))
            .Columns.Add("Tmrtc", GetType(Decimal))
            .Columns.Add("SendUnit", GetType(String))
            .Columns.Add("LossRate", GetType(Decimal))
            .Columns.Add("AutoID", GetType(String))
        End With

        With ds.Tables.Add("AutoID")
            .Columns.Add("AutoID", GetType(String))
        End With

        GridDetail.DataSource = ds.Tables("Bom_Detail")
    End Sub

    ''' <summary>
    ''' ������K�[�q�{�ƾ�
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function LoadTable() As DataRow
        Dim dr As DataRow = ds.Tables("Bom_Detail").NewRow
        dr("Item") = String.Empty
        dr("ChildGroup") = String.Empty
        dr("ChildName") = String.Empty
        dr("ChildGauge") = String.Empty
        dr("ChildMC_Source") = String.Empty
        dr("IsUnfold") = 1
        dr("ReplaceType1") = "���`(normal)"
        dr("UseFeatures") = String.Empty
        dr("EffectiveDate") = Format(Date.Today.Date, "yyyy/MM/dd")
        dr("InvalidDate") = String.Empty
        dr("Mount") = 0
        dr("Tmrtc") = 0
        dr("SendUnit") = String.Empty
        dr("LossRate") = 0
        dr("AutoID") = String.Empty
        Return dr
    End Function

    ''' <summary>
    ''' ��������Ǹ��Ƨ�
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub FreshGridview()
        Dim i As Integer = 0
        Dim num As String
        For i = 0 To ds.Tables("Bom_Detail").Rows.Count - 1
            num = ds.Tables("Bom_Detail").Rows(i)("Item")
            If num = (i + 1).ToString Then
                Continue For
            Else
                ds.Tables("Bom_Detail").Rows(i)("Item") = (i + 1).ToString
            End If
        Next
    End Sub

    ''' <summary>
    ''' ���~���c��Ʒs�W�ɡA�q�L���~�s���۰ʱo�X �W�١B�W��B���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub gluM_Code_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluM_Code.EditValueChanged
        If gluM_Code.EditValue <> String.Empty Then
            '���ƳB�z
            If operateType = "BomAdd" Then
                Dim bmminfo As New List(Of Bom_MInfo)
                bmminfo = bmm.Bom_M_GetList(gluM_Code.EditValue, Nothing, Nothing, Nothing, Nothing, Nothing)
                If bmminfo.Count > 0 Then
                    MsgBox("�z��J�����~�s�X����,�Э���", 64, "����")
                    gluM_Code.EditValue = String.Empty
                    gluM_Code.Focus()
                    Exit Sub
                End If
            End If
            '---------------------------
            Dim mmcList As New List(Of MrpMaterialCodeInfo)
            mmcList = MMICcon.MrpMaterialCode_GetList(gluM_Code.EditValue, Nothing, Nothing, Nothing, Nothing)
            If mmcList.Count > 0 Then
                txtName.Text = mmcList(0).M_Name
                txtGauge.Text = mmcList(0).M_Gauge
                txtUnit.Text = mmcList(0).M_Unit
                txtSource.Text = mmcList(0).MC_Source
            End If
        End If
    End Sub

    ''' <summary>
    ''' ���Ʃ��Ӫ����J�ާ@
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BomDetailInsert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BomDetailInsert.Click
        Dim num As Integer
        Dim dr As DataRow
        dr = LoadTable()
        num = GridView2.GetFocusedRowCellValue("Item")
        ds.Tables("Bom_Detail").Rows.InsertAt(dr, num - 1)
        FreshGridview()
    End Sub

    ''' <summary>
    ''' �����ާ@
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub canButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles canButton.Click
        Me.Close()
    End Sub

    ''' <summary>
    ''' ���Ʃ��ӷs�W�ɡA�q�L����ƥ�۰ʱo�X �W�١B�W��B���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub GridView1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridView1.DoubleClick
        Dim num As Integer = GridView2.GetFocusedRowCellValue("Item")
        ds.Tables("Bom_Detail").Rows(num - 1)("ChildGroup") = GridView1.GetFocusedRowCellValue("M_Code")
        ds.Tables("Bom_Detail").Rows(num - 1)("ChildName") = GridView1.GetFocusedRowCellValue("M_Name")
        ds.Tables("Bom_Detail").Rows(num - 1)("ChildGauge") = GridView1.GetFocusedRowCellValue("M_Gauge")
        ds.Tables("Bom_Detail").Rows(num - 1)("SendUnit") = GridView1.GetFocusedRowCellValue("M_Unit")
        ds.Tables("Bom_Detail").Rows(num - 1)("ChildMC_Source") = GridView1.GetFocusedRowCellValue("MC_Source")
        PopupContainerControl1.OwnerEdit.ClosePopup()
        okButton.Focus()
    End Sub

End Class