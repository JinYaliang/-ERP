Imports LFERP.Library.ProductionSchedule
Imports LFERP.Library.Product
Imports LFERP.DataSetting
Imports LFERP.Library.ProductProcess

Public Class frmProductionScheduleDemand

#Region "�w�s�Ѽ�"

    Private ds As New DataSet                       '�ƾڶ�


    Private m_EditItemDescrip As String             '�y�z

    Public Property EditItemDescrip() As String
        Get
            Return m_EditItemDescrip
        End Get
        Set(ByVal value As String)
            m_EditItemDescrip = value
        End Set
    End Property

#End Region

#Region "�Ы��{�ɪ�"
    Private Sub CreateTables()
        ds.Tables.Clear()
        With ds.Tables.Add("ProductionSchedule")
            .Columns.Add("dtDate", GetType(String))
            .Columns.Add("PlanCount", GetType(String))
            .Columns.Add("C_Qty", GetType(String))
            .Columns.Add("PS_Num", GetType(String))
        End With
        GridProductionSchedule.DataSource = ds.Tables("ProductionSchedule")



        With ds.Tables.Add("ProductType")
            .Columns.Add("PM_Type", GetType(String))
        End With
        gluType.Properties.ValueMember = "PM_Type"
        gluType.Properties.DisplayMember = "PM_Type"
        gluType.Properties.DataSource = ds.Tables("ProductType")

    End Sub
#End Region


    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Select Case m_EditItemDescrip
            Case "Add"
                If CheckData() Then
                    DataNew()
                End If
            Case "Modify"
                If CheckData() Then
                    DataModifyData()
                End If
            Case "Check"
                UpdateCheck()
        End Select

    End Sub

    ''' <summary>
    ''' �f��
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateCheck()

        Dim pi As New ProductionScheduleInfo
        Dim pc As New ProductionScheduleControl

        pi.PS_NO = txtNO.Text
        pi.PS_Check = chkCheck.Checked
        pi.PS_CheckDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
        pi.PS_CheckAction = InUserID
        pi.PS_CheckRemark = txtCheckRemark.Text
        If pc.ProductionSchedule_UpdateCheck(pi) = True Then
            MsgBox("�f�֪��A�w����!", 64, "����")
        Else
            MsgBox("�f�֥���,���ˬd��]!", 64, "����")
        End If
        Me.Close()
    End Sub



    ''' <summary>
    ''' �f�d�O�s�ƾ�
    ''' </summary>
    ''' <remarks></remarks>
    Private Function CheckData() As Boolean
        CheckData = False

        If GluDep.EditValue = "" Then
            MsgBox("�Ͳ��������ର��!", 64, "����")
            GluDep.Focus()
            Exit Function
        End If

        If cbType.EditValue = "" Then
            MsgBox("�u���������ର��!", 64, "����")
            cbType.Focus()
            Exit Function
        End If

        If dtPMCode.EditValue = "" Then
            MsgBox("���~�s�����ର��!", 64, "����")
            dtPMCode.Focus()
            Exit Function
        End If

        If gluType.EditValue = "" Then
            MsgBox("�������ର��!", 64, "����")
            gluType.Focus()
            Exit Function
        End If

        If txtQty.Text = "" Then
            MsgBox("�p�e�ݨD�q���ର��!", 64, "����")
            Exit Function
        End If

        If CInt(txtQty.Text) <= 0 Then
            MsgBox("�p�e�ݨD�q����p�󵥩�s!", 64, "����")
            Exit Function
        End If

        Dim ts As New TimeSpan

        ts = DateTime.Parse(dtPlanEnd.Text) - DateTime.Parse(dtPlanStart.Text)


        If ts.Days > 6 Then
            MsgBox("�Ͳ��p�e����W�L�@�g!", 64, "����")
            Exit Function
        End If



        '2014-03-05  ���@
        Dim sum As Integer
        For nIndex As Integer = 0 To ds.Tables("ProductionSchedule").Rows.Count - 1


            Try
                If ds.Tables("ProductionSchedule").Rows(nIndex)("PlanCount").ToString() <> String.Empty Then
                    If CInt(ds.Tables("ProductionSchedule").Rows(nIndex)("PlanCount")) <= 0 Then
                        MsgBox("�p�e�ƶq����p�󵥩�s!", 64, "����")
                        Exit Function
                    End If
                    sum = sum + CInt(ds.Tables("ProductionSchedule").Rows(nIndex)("PlanCount"))
                End If
            Catch ex As Exception
                MsgBox("�p�e�ƶq�W�X�d��!", 64, "����")
                Exit Function
            End Try

        Next

        If sum > CInt(txtQty.Text) Then
            MsgBox("�p�e�ƶq����j��p�e�`�ݨD�q!", 64, "����")
            Exit Function
        End If

        CheckData = True

    End Function

    ''' <summary>
    ''' �O�s�ק�ƾ�
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DataModifyData()
        Dim pi As New ProductionScheduleInfo
        Dim psc As New ProductionScheduleControl

        Dim i As Integer

        For i = 0 To ds.Tables("ProductionSchedule").Rows.Count - 1
            pi.PS_NO = txtNO.Text
            pi.Pro_Type = cbType.EditValue
            pi.PM_M_Code = dtPMCode.EditValue
            pi.PM_Type = gluType.EditValue
            pi.M_Code = GetCode(cbType.EditValue, dtPMCode.EditValue, gluType.EditValue)
            pi.PS_KaiLiao = False
            pi.PS_Detail = "�ƮƤ�"
            pi.PS_Action = InUserID
            pi.PS_Dep = GluDep.EditValue
            pi.PS_AddDate = Format(Now, "yyyy/MM/dd")

            pi.PS_Num = ds.Tables("ProductionSchedule").Rows(i)("PS_Num")

            If IsDBNull(ds.Tables("ProductionSchedule").Rows(i)("PlanCount")) Then
                pi.PS_DayNumber = 0
            Else
                pi.PS_DayNumber = ds.Tables("ProductionSchedule").Rows(i)("PlanCount")
            End If

            'pi.PS_Date = DateAdd(DateInterval.Day, +i, CDate(dtPlanStart.Text))
            pi.PS_Date = ds.Tables("ProductionSchedule").Rows(i)("dtDate")

            If Not psc.ProductionSchedule_Update(pi) Then
                MsgBox("�Ͳ��p�e�ק異��,���ˬd��]!")
                Exit Sub
            End If


        Next

        MsgBox("�ק�Ͳ��p�����\!")
      
        Me.Close()

    End Sub

    ''' <summary>
    ''' �O�s�s�W�ƾ�
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DataNew()


        

        Dim psi As List(Of ProductionScheduleInfo)
        Dim pi As New ProductionScheduleInfo
        Dim psc As New ProductionScheduleControl


        psi = psc.ProductionSchedule_GetList(Nothing, Nothing, Nothing, dtPMCode.EditValue, Nothing, dtPlanStart.Text, dtPlanEnd.Text, Nothing)

        If psi.Count > 0 Then
            MsgBox("�Ͳ��p�e�w�g�s�b,����s�W!", 64, "����")
            Exit Sub
        End If


        Dim i As Integer

        pi.PS_NO = GetNO()

        For i = 0 To ds.Tables("ProductionSchedule").Rows.Count - 1

            pi.Pro_Type = cbType.EditValue
            pi.PM_M_Code = dtPMCode.EditValue
            pi.PM_Type = gluType.EditValue
            pi.M_Code = GetCode(cbType.EditValue, dtPMCode.EditValue, gluType.EditValue)
            pi.PS_KaiLiao = False
            pi.PS_Detail = "�ƮƤ�"
            pi.PS_Action = InUserID
            pi.PS_Dep = GluDep.EditValue

            pi.PS_AddDate = Format(Now, "yyyy/MM/dd")

            pi.PS_Num = GetNum()

            If IsDBNull(ds.Tables("ProductionSchedule").Rows(i)("PlanCount")) Then
                pi.PS_DayNumber = 0
            Else
                pi.PS_DayNumber = ds.Tables("ProductionSchedule").Rows(i)("PlanCount")
            End If

            'pi.PS_Date = DateAdd(DateInterval.Day, +i, CDate(dtPlanStart.Text))

            pi.PS_Date = ds.Tables("ProductionSchedule").Rows(i)("dtDate")

            psc.ProductionSchedule_Add(pi)

        Next

        MsgBox("�����إ߷�e�Ͳ��p��!", 64, "����")
        Me.Close()
    End Sub

    '����̷s������جy����
    Public Function GetNum() As String
        Dim psi As New ProductionScheduleInfo
        Dim psc As New ProductionScheduleControl
        Dim strName As String
        strName = "P" + Format(Now, "yyMM")
        psi = psc.ProductionSchedule_GetNum(strName)
        If psi Is Nothing Then
            GetNum = strName + "00001"
        Else
            GetNum = strName + Mid((CInt(Mid(psi.PS_Num, 6)) + 100001), 2)
        End If
    End Function


    '����̷s���ظ�
    Public Function GetNO() As String
        Dim psi As New ProductionScheduleInfo
        Dim psc As New ProductionScheduleControl
        Dim strName As String
        strName = Format(Now, "yyMM")
        psi = psc.ProductionSchedule_GetNO(strName)
        If psi Is Nothing Then
            GetNO = "PS" + strName + "0001"
        Else
            GetNO = "PS" + strName + Mid((CInt(Mid(psi.PS_NO, 7)) + 10001), 2)
        End If

    End Function


    Public Function GetCode(ByVal Pro_Type As String, ByVal PM_M_Code As String, ByVal PM_Type As String) As String

        Dim ppc As New ProcessMainControl
        Dim ppi As List(Of ProcessMainInfo)
        ppi = ppc.ProcessMain_GetList(Nothing, PM_M_Code, Pro_Type, PM_Type, Nothing, Nothing)
        If ppi.Count = 0 Then
            GetCode = Nothing
            Exit Function
        Else
            GetCode = ppi(0).M_Code
        End If

    End Function



#Region "��l��"
    Private Sub frmProductionScheduleDemand_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CreateTables()

        LoadFacName()
        LoadProductNo()
        LoadDateTime()


        Select Case m_EditItemDescrip
            Case "Add"
                AddControlValid()
                XtraTabPage2.PageVisible = False
                cbType.SelectedIndex = 1
                pnlCheck.Enabled = False
                Me.Text = "�K�[�Ͳ��p���ݨD"
            Case "Modify"
                ModifyLoadData()
                pnlCheck.Enabled = False
                btnQuery.Enabled = True
                XtraTabPage2.PageEnabled = False
                Me.Text = "�ק�Ͳ��p���ݨD"
            Case "Check"
                ModifyLoadData()
                GridView1.OptionsBehavior.Editable = False
                lblCheckAction.Text = InUserID
                lblCheckDateTime.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
                Me.Text = "�f�֥Ͳ��p���ݨD"
                XtraTabControl1.SelectedTabPage = XtraTabPage2
            Case "View"
                ModifyLoadData()
                cmdSave.Enabled = False
              
                pnlCheck.Enabled = False
                GridView1.OptionsBehavior.Editable = False
                Me.Text = "�f�֬d�ݭp���ݨD"
        End Select
    End Sub

    ''' <summary>
    ''' �ק�[���ƾ�
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ModifyLoadData() As Boolean
        ModifyLoadData = False

        txtNO.Text = tempValue3

        Dim psi As List(Of ProductionScheduleInfo)
        Dim psc As New ProductionScheduleControl

        psi = psc.ProductionSchedule_GetList(txtNO.Text, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If psi Is Nothing Then
            Exit Function
        End If

        If psi.Count <= 0 Then
            Exit Function
        End If


        dtPlanStart.EditValue = Format(psi(0).PS_Date, "yyyy/MM/dd")
        dtPlanEnd.EditValue = Format(psi(0).PS_Date.AddDays(psi.Count - 1), "yyyy/MM/dd")

        cbType.EditValue = psi(0).Pro_Type
        GluDep.EditValue = psi(0).PS_Dep

        dtPMCode.EditValue = psi(0).PM_M_Code
  

        dtPMCode_EditValueChanged(Nothing, Nothing)
        gluType.EditValue = psi(0).PM_Type


        '�[���f�ּƾ�
      
        chkCheck.Checked = psi(0).PS_Check
        lblCheckDateTime.Text = psi(0).PS_CheckDate
        lblCheckAction.Text = psi(0).PS_CheckAction
        txtCheckRemark.Text = psi(0).PS_CheckRemark


        Dim i As Integer
        For i = 0 To psi.Count - 1
            Dim row As DataRow
            row = ds.Tables("ProductionSchedule").NewRow

            row("dtDate") = psi(i).PS_Date.ToString("yyyy/MM/dd")
            row("PlanCount") = psi(i).PS_DayNumber
            row("C_Qty") = psi(i).C_Qty
            row("PS_Num") = psi(i).PS_Num
            ds.Tables("ProductionSchedule").Rows.Add(row)
        Next

        LoadQty()

    End Function



    ''' <summary>
    ''' �s�W,���󦳮�
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddControlValid()


        GluDep.Enabled = True
        dtPMCode.Enabled = True

        gluType.Enabled = True
        cbType.Enabled = True
        btnQuery.Enabled = True
    End Sub

    ''' <summary>
    ''' �Ͳ�����
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadFacName()
        Dim fc As New FacControler
        Dim fi As New FacInfo

        GluDep.Properties.DataSource = fc.GetFacList(Nothing, Nothing)
        GluDep.Properties.DisplayMember = "FacName"
        GluDep.Properties.ValueMember = "FacID"

    End Sub

    ''' <summary>
    ''' �[�����~�s��
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadProductNo()
        Dim pc As New ProcessMainControl
        dtPMCode.Properties.DisplayMember = "PM_M_Code"
        dtPMCode.Properties.ValueMember = "PM_M_Code"
        dtPMCode.Properties.DataSource = pc.ProcessMain_GetList3(Nothing, Nothing)
    End Sub

    ''' <summary>
    ''' ��l�ƭp�e���
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadDateTime()
        dtPlanStart.Text = Format(Now.AddDays(8 - Now.DayOfWeek), "yyyy/MM/dd")
        dtPlanEnd.Text = Format(Now.AddDays(8 - Now.DayOfWeek + 6), "yyyy/MM/dd")
    End Sub

    '�[���ݨD�ƶq
    Private Function LoadQty() As Boolean

        LoadQty = False

        txtQty.Text = String.Empty


        Dim psc As New ProductionScheduleControl
        Dim psi As New List(Of ProductionScheduleInfo)
        psi = psc.ProductPlan_GetQty(dtPMCode.EditValue, dtPlanStart.Text)

        If psi.Count <= 0 Then
            Exit Function
        End If

        Dim scheduleQty As String
        scheduleQty = psi(0).SumNoSendQty - (psi(0).SumDayQty + psi(0).SumPIQty)

        If scheduleQty > 0 Then
            GridView1.OptionsBehavior.Editable = True
            GridView1.Columns.Item("dtDate").OptionsColumn.ReadOnly = True
            GridView1.Columns.Item("C_Qty").OptionsColumn.ReadOnly = True
            LoadQty = True
        End If

        lblCount.Text = String.Empty
        lblCount.Text = "�q�楼��ơG" & psi(0).SumNoSendQty.ToString() & ",  ���u���l�ơG" & psi(0).SumDayQty.ToString() & ",  �w�s�ƶq�G" & psi(0).SumPIQty.ToString()
        lblCount.Visible = True
        txtQty.Text = scheduleQty


    End Function

    ''' <summary>
    ''' ��R�ƾں���
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillDataGrid()
        Dim ts As New TimeSpan

        Dim strDate1 As DateTime
        Dim strDate2 As DateTime


        strDate1 = DateTime.Parse(dtPlanEnd.Text)
        strDate2 = DateTime.Parse(dtPlanStart.Text)

        ts = strDate1 - strDate2


        Dim i As Integer
        For i = 0 To ts.Days
            Dim row As DataRow
            row = ds.Tables("ProductionSchedule").NewRow
            row("dtDate") = Convert.ToDateTime(dtPlanStart.Text).AddDays(i).ToString("yyyy/MM/dd")
            ds.Tables("ProductionSchedule").Rows.Add(row)
        Next

    End Sub

#End Region

#Region "�ھڲ��~�s���[���ƾ�"
    Private Sub dtPMCode_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtPMCode.EditValueChanged

        On Error Resume Next

        Dim ppc As New ProcessMainControl
        Dim ppi As List(Of ProcessMainInfo)
        ds.Tables("ProductType").Clear()
        ppi = ppc.ProcessMain_GetList2(cbType.EditValue, dtPMCode.EditValue)
        If ppi.Count = 0 Then
        Else

            Dim i As Integer
            For i = 0 To ppi.Count - 1
                Dim row As DataRow
                row = ds.Tables("ProductType").NewRow
                row("PM_Type") = ppi(i).Type3ID
                ds.Tables("ProductType").Rows.Add(row)
            Next

        End If

    End Sub
#End Region

#Region "�ھڤu�������եβ��~�s���[���ƾ�"
    Private Sub cbType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbType.SelectedIndexChanged
        If dtPMCode.Text <> "" Then
            dtPMCode_EditValueChanged(Nothing, Nothing)
        End If
    End Sub
#End Region

#Region "�d�ߥͲ��p�e�ƶq"
    Private Sub btnQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuery.Click

        If dtPMCode.Text <> "" And dtPlanStart.Text <> "" Then
            GridView1.OptionsBehavior.Editable = False
       
            ds.Tables("ProductionSchedule").Clear()
            Select Case m_EditItemDescrip
                Case "Add"
                    If LoadQty() Then
                        FillDataGrid()
                    End If
                Case Else
                    ModifyLoadData()
            End Select

          
           
        Else
            MsgBox("�p�e�_�l����Ϊ̲��~�s�����ର�šI", 64, "����")
        End If

    End Sub
#End Region

#Region "��������"
    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub
#End Region

End Class