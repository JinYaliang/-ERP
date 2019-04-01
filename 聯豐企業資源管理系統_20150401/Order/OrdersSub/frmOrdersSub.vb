Imports LFERP.DataSetting
Imports LFERP.Library.Orders
Imports LFERP.Library.Product
Imports LFERP.Library.MaterialParam
Imports LFERP.Library.Material
Imports LFERP.Library
Imports LFERP.Library.Purchase.Purchase
Imports LFERP.Library.ProductProcess
Imports LFERP.SystemManager



Public Class frmOrdersSub
    Dim ds As New DataSet
    Dim intTempID As Integer
    Dim intOM_NoMakeQty As Integer

    Dim StrOM_ID As String
    Dim IntOS_Qty_Edit As Double = 0  ''�ק�ɸ��J��


    Dim ChcekPM_Type As Boolean = False  '�O�_�P�_�t��O�_�إ�

    Sub LoadProductNo()
        Dim mc As New ProductController
        PM_M_Code.Properties.DisplayMember = "PM_M_Code"
        PM_M_Code.Properties.ValueMember = "PM_M_Code"
        PM_M_Code.Properties.DataSource = mc.Product_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

    End Sub

    Function GetNowWeek() As String
        Dim dMyDate As DateTime = DateTime.Now
        Dim PastDays
        Dim Weeks

        PastDays = dMyDate.DayOfYear
        Weeks = Val(PastDays) / 7
        Weeks = Int(Weeks) + 1
        GetNowWeek = Weeks
    End Function

    Function GetOS_BatchID() As String
        Dim os As New OrdersSubController
        Dim osi As OrdersSubInfo
        Dim strNowWeek As String

        Dim strOS_BatchID As String

        strOS_BatchID = Format(Now, "yy")
        strNowWeek = GetNowWeek()

        If strNowWeek < 10 Then
            strOS_BatchID = strOS_BatchID & "0" & strNowWeek
        Else
            strOS_BatchID = strOS_BatchID & strNowWeek
        End If

        osi = os.OrdersSub_Get1(strOS_BatchID)
        If osi Is Nothing Then
            strOS_BatchID = strOS_BatchID & "001"
        Else
            strOS_BatchID = CStr(CInt(osi.OS_BatchID) + 1)
        End If

        GetOS_BatchID = strOS_BatchID
    End Function

    '@ 2013/3/26 �K�[ ���v������
    Sub LoadPowerUser()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        '�妸�s���O�_���\��ʿ�J
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "300206")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then
                OS_BatchID.Enabled = True
            Else
                OS_BatchID.Enabled = False
            End If
        Else
            OS_BatchID.Enabled = False
        End If



        ''ChcekPM_Type

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "300208")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then
                ChcekPM_Type = True
            Else
                ChcekPM_Type = False
            End If
        Else
            ChcekPM_Type = False
        End If
    End Sub


    Private Sub frmOrdersSub_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim i%

        StrOM_ID = tempValue

        OM_ID.Text = tempValue
        Label19.Text = tempCode

        LoadFacName()
        LoadFacName1() '�t��H�����[�u����
        LoadProductNo()
        Dim dc As New DepartmentControler
        dc.GetDepartmentModule(TreeView1)
        CreateTables()

        LoadPowerUser()

        'For i = 1 To 54
        '    cboOS_ProductionWeek.Properties.Items.Add(i)
        'Next

        '----------------------------------------------------------------------
        Select Case Label19.Text

            Case "�j�f�妸"
                Dim omc As New OrdersMainController
                Dim omi As New OrdersMainInfo

                omi = omc.OrdersMain_Get(Nothing, OM_ID.Text)
                intOM_NoMakeQty = omi.OM_NoMakeQty
                If Edit = False Then

                    If OS_BatchID.Enabled = False Then
                        OS_BatchID.Text = GetOS_BatchID()
                    End If

                    OS_BatchID.Focus()

                    OS_SendDate.EditValue = Format(tempValue7, "Short Date")
                    OS_CheckDate.EditValue = Format(tempValue8, "Short Date")

                    OS_Qty.Text = omi.OM_NoMakeQty
                    OS_Sprace.Text = omi.OM_OrderSpare
                    PM_M_Code.Text = omi.PM_M_Code

                    OS_Type.EditValue = "�j�f�妸"
                    Label17.Visible = False

                    PM_M_Code.Enabled = False
                    OS_Type.Enabled = False
                    popMaterialAdd.Visible = False
                    popMaterialDel.Visible = False
                    cboOS_ProductionWeek.Text = GetNowWeek() & "�P"
                    tieOS_ProductionWeek_Year.EditValue = Format(Now, "yyyy�~")
                    Me.Text = "�Ͳ���--�s�W"

                Else
                    OS_BatchID.Enabled = False
                    PM_M_Code.Enabled = False
                    gluM_Code.Enabled = False
                    Label17.Visible = False
                    OS_Type.Enabled = False
                    OS_BatchID.Text = tempValue2
                    LoadDataOrdersSub()
                    LoadDataOrdersDetail()
                    LoadDataOrdersBom()
                    LoadDataOrdersTempDetail()
                    popMaterialAdd.Visible = False
                    popMaterialDel.Visible = False
                    TreeListColumn8.OptionsColumn.ReadOnly = True
                    Me.Text = "�Ͳ���--�ק�"
                End If
                'If strInCompany <> "1001" Then
                'OS_Qty.Enabled = False
                'OS_CheckDate.Enabled = False
                'OS_SendDate.Enabled = False
                'End If
                'Label20.Visible = False
                'M_BatchID.Visible = False

                TreeListColumn4.OptionsColumn.ReadOnly = True
            Case "�t��妸"
                If Edit = False Then
                    '@ 2012/9/20 �K�[ ----------------------------------
                    Dim omc As New OrdersMainController
                    Dim omi As New OrdersMainInfo

                    OS_BatchID.Focus()

                    omi = omc.OrdersMain_Get(Nothing, OM_ID.Text)

                    OS_SendDate.EditValue = Format(omi.OM_SendDate, "Short Date")
                    OS_CheckDate.EditValue = Format(omi.OM_CheckDate, "Short Date")

                    OS_Qty.Text = omi.OM_OrderQty
                    OS_Sprace.Text = omi.OM_OrderSpare

                    '---------------------------------------------------------------

                    OS_Type.EditValue = "�t��妸"
                    OS_Type.Enabled = False
                    PM_M_Code.Text = "SP"
                    'PM_M_Code.Enabled = False
                    frmBOMSelect.XtraTabControl1.SelectedTabPageIndex = 1
                    TreeListColumn4.OptionsColumn.ReadOnly = False
                    Me.Text = "�Ͳ���--�s�W"
                Else
                    OS_BatchID.Enabled = False
                    PM_M_Code.Enabled = False
                    gluM_Code.Enabled = False
                    Label17.Visible = False
                    OS_Type.Enabled = False
                    OS_BatchID.Text = tempValue2
                    LoadDataOrdersSub()
                    LoadDataOrdersDetail()
                    LoadDataOrdersBom()
                    LoadDataOrdersTempDetail()
                    popMaterialAdd.Visible = True
                    popMaterialDel.Visible = True
                    TreeListColumn8.OptionsColumn.ReadOnly = True
                    frmBOMSelect.XtraTabControl1.SelectedTabPageIndex = 1
                    TreeListColumn4.OptionsColumn.ReadOnly = False
                    Me.Text = "�Ͳ���--�ק�"
                End If

                OS_Qty.Enabled = False
            Case "Copy"
                If Edit = False Then
                    OS_BatchID.Text = tempValue2
                    Label17.Visible = True
                    Label17.Text = tempValue2
                    PM_M_Code.Enabled = False
                    gluM_Code.Enabled = False
                    LoadDataOrdersSub()
                    LoadDataOrdersDetail()
                    LoadDataOrdersTempDetail()

                    OS_Type.EditValue = tempValue3
                    OS_Type.Enabled = False
                    If OS_Type.EditValue = "�j�f�妸" Then
                        LoadDataOrdersBom()
                        OS_BatchID.Text = ""
                        'Label20.Visible = False
                        'M_BatchID.Visible = False
                        popMaterialAdd.Visible = False
                        popMaterialDel.Visible = False
                    ElseIf OS_Type.EditValue = "�t��妸" Then
                        'M_BatchID.Text = Label17.Text
                        OS_BatchID.Text = ""
                        'Label20.Visible = True
                        M_BatchID.Visible = True
                        popMaterialAdd.Visible = True
                        popMaterialDel.Visible = True
                    End If


                    OS_BatchID.Text = GetOS_BatchID()

                End If

            Case "Check"
                OS_BatchID.Text = tempValue2
                LoadDataOrdersSub()
                LoadDataOrdersDetail()
                LoadDataOrdersBom()
                LoadDataOrdersTempDetail()
                TreeListColumn8.OptionsColumn.ReadOnly = False
                TreeListColumn4.OptionsColumn.ReadOnly = True
                XtraTabControl1.SelectedTabPageIndex = 1
                'If OS_Type.EditValue = "�j�f�妸" Then
                '    Label20.Visible = False
                '    M_BatchID.Visible = False
                'ElseIf OS_Type.EditValue = "�t��妸" Then

                'End If
                EnableView()
                Me.Text = "�Ͳ���--�f��"
            Case "PreView"
                OS_BatchID.Text = tempValue2
                LoadDataOrdersSub()
                LoadDataOrdersDetail()
                LoadDataOrdersBom()
                LoadDataOrdersTempDetail()
                SimpleButton1.Visible = False

                EnableView()
                Me.Text = "�Ͳ���--�d��"
        End Select

        If OS_Type.EditValue = "�j�f�妸" Then
            Label20.Visible = False
            M_BatchID.Visible = False
            cboOS_ProductionWeek.Visible = True
            tieOS_ProductionWeek_Year.Visible = True
            Label22.Visible = True
        ElseIf OS_Type.EditValue = "�t��妸" Then
            Label20.Visible = True
            M_BatchID.Visible = True
            cboOS_ProductionWeek.Visible = False
            tieOS_ProductionWeek_Year.Visible = False
            Label22.Visible = False
        End If
        XtraTabPage3.PageVisible = False
        tempValue = ""
        tempValue2 = ""
        tempValue3 = ""
        tempValue7 = ""
        tempValue8 = ""
        tempCode = ""
    End Sub

    Sub CreateTables()

        ds.Tables.Clear()
        '�Ыؼƾڪ�_OrdersDetail(�Ͳ����ؤ��e)
        With ds.Tables.Add("OrdersDetail")

            .Columns.Add("OD_ID", GetType(String))

            .Columns.Add("OD_Name", GetType(String))
            .Columns.Add("OD_Detail", GetType(String))

        End With
        '�ЫاR���ƾڪ�_OrdersDetail(�Ͳ����ؤ��e)
        With ds.Tables.Add("DelDataOrdersDetail")
            .Columns.Add("OD_ID", GetType(String))
        End With
        '�j�w���
        Grid.DataSource = ds.Tables("OrdersDetail")



        '�Ыؼƾڪ�_OrdersTempDetail(�{�ɳ�)
        With ds.Tables.Add("OrdersTempDetail")

            .Columns.Add("OT_ID", GetType(String))
            .Columns.Add("OT_Detail", GetType(String))

        End With
        '�ЫاR���ƾڪ�_OrdersTempDetail(�{�ɳ�)
        With ds.Tables.Add("DelDataOrdersTempDetail")
            .Columns.Add("OT_ID", GetType(String))
        End With
        '�j�w���
        GridControl1.DataSource = ds.Tables("OrdersTempDetail")




        '�Ыؼƾڪ�_Prod_Mounting_New(���ƲM��)
        '�Ыؼƾڪ�
        With ds.Tables.Add("Prod_Mounting_New")

            .Columns.Add("AutoID", GetType(String))

            .Columns.Add("PM_M_Code", GetType(String)) '�p�׽s��
            .Columns.Add("PM_ID", GetType(Integer))
            .Columns.Add("M_Code", GetType(String))
            .Columns.Add("M_CodePID", GetType(String))
            .Columns.Add("PM_Qty", GetType(Double))
            .Columns.Add("PM_MakeRemark", GetType(String))
            .Columns.Add("PM_Make", GetType(Boolean))
            .Columns.Add("PM_Check", GetType(Boolean))
            .Columns.Add("M_Supplier", GetType(String))
            .Columns.Add("M_SupplierNo", GetType(String))
            .Columns.Add("IsNew", GetType(Boolean))    '�O�_�s�W

            .Columns.Add("M_Name", GetType(String))
            .Columns.Add("M_Gauge", GetType(String))
            .Columns.Add("M_Unit", GetType(String))
            .Columns.Add("PM_LVL", GetType(Integer))
            .Columns.Add("PM_Key", GetType(String))
            .Columns.Add("PM_PID", GetType(String))
            .Columns.Add("M_CodeType", GetType(String))
            .Columns.Add("M_CodeMouldNO", GetType(String))
            .Columns.Add("OB_MakeDepartment", GetType(String))
            .Columns.Add("OB_MModeCusterNO", GetType(String))

            .Columns.Add("M_Weight", GetType(String))
        End With
        '�ЫاR���ƾڪ�
        With ds.Tables.Add("DelDataOrdersBomProduct")
            .Columns.Add("OS_BatchID", GetType(String))
            .Columns.Add("PM_Key", GetType(String))
        End With
        '�j�w���
        TreeList1.DataSource = ds.Tables("Prod_Mounting_New")

        '@ 2012/9/24   �K�[ �Ыت��ƽs�X��
        With ds.Tables.Add("ProductM_Code")
            .Columns.Add("M_Code", GetType(String))
            .Columns.Add("M_Name", GetType(String))
        End With
        gluM_Code.Properties.DisplayMember = "M_Name"
        gluM_Code.Properties.ValueMember = "M_Code"
        gluM_Code.Properties.DataSource = ds.Tables("ProductM_Code")

    End Sub

    Sub LoadFacName()
        Dim fc As New FacControler
        Dim fi As New FacInfo

        OS_Fac.Properties.DataSource = fc.GetFacList(Nothing, Nothing)
        OS_Fac.Properties.DisplayMember = "FacName"
        OS_Fac.Properties.ValueMember = "FacID"

    End Sub
    Sub LoadFacName1()
        Dim fc As New FacControler
        Dim fi As New FacInfo
        Me.RepositoryItemLookUpEdit1.DataSource = fc.GetFacList(Nothing, Nothing)
        Me.RepositoryItemLookUpEdit1.DisplayMember = "FacName"
        Me.RepositoryItemLookUpEdit1.ValueMember = "FacID"
    End Sub
    Private Sub SimpleButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton1.Click

        If OS_BatchID.Text.Trim = "" Then
            MsgBox("�妸�s�����ର�šA�п�J�妸�s��!", 64, "����")
            OS_BatchID.Focus()
            Exit Sub
        End If
        If OS_Qty.Text.Trim = "" Then
            MsgBox("�Ͳ��ƶq���ର�šA�п�J�Ͳ��ƶq!", 64, "����")
            OS_Qty.Focus()
            Exit Sub
        End If
        If PM_M_Code.EditValue Is Nothing Or PM_M_Code.EditValue = "" Then
            MsgBox("���~�s�����ର�šA�п�J���~�s��", 64, "����")
            PM_M_Code.Focus()
            Exit Sub
        End If
        If OS_Sprace.Text.Trim = "" Then
            MsgBox("�h�ण�ର�šA�п�J�h��!", 64, "����")
            OS_Sprace.Focus()
            Exit Sub
        End If
        If OS_Fac.EditValue Is Nothing Or OS_Fac.EditValue = "" Then
            MsgBox("�Ͳ������ର�šA�п�J�Ͳ���", 64, "����")
            OS_Fac.Focus()
            Exit Sub
        End If


        If Val(OS_Qty.Text) <= 0 Then
            MsgBox("�Ͳ��ƶq���ର��!", 64, "����")
            OS_Qty.Select()
            Exit Sub
        End If


        'If CInt(OS_Qty.Text.Trim) > intOM_NoMakeQty Then
        '    MsgBox("�Ͳ��ƶq����j�󥼥Ͳ��ƶq,���Ͳ��ƶq���G" & intOM_NoMakeQty, 64, "����")
        '    OS_Qty.Focus()
        '    Exit Sub
        'End If


        If ds.Tables("Prod_Mounting_New").Rows.Count = 0 And ChcekPM_Type = False Then
            MsgBox("�����J���~�t���Ʃβ��~��ƥ��إߡI", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
            Exit Sub
        End If
        '-------------------------------
        If Label19.Text <> "Check" Then
            If CheckCopyData() = False Then '2013-5-21 
                Exit Sub
            End If
        End If


        Select Case Label19.Text

            Case "�j�f�妸"
                Select Case Edit
                    Case False
                        SaveNew()
                    Case True
                        SaveEdit()
                End Select
            Case "�t��妸" '�w���p�ץ�f��������P�A�B�g�����������~�ϥ�
                Select Case Edit
                    Case False
                        SaveNew()
                    Case True
                        SaveEdit()
                End Select
            Case "Copy"
                Select Case Edit
                    Case False
                        SaveNew()
                End Select
            Case "Check"

                UpdateOrdersSubCheck()

        End Select
        Me.Close()

    End Sub
    '�ͦ���---�f��
    Sub UpdateOrdersSubCheck()

        Dim oblist As List(Of OrdersBomInfo)
        Dim obi As New OrdersBomInfo
        Dim obc As New OrdersBomController
        Dim i As Integer
        oblist = obc.OrdersBom_GetList(Nothing, OS_BatchID.Text, Nothing, Nothing)

        If oblist.Count = 0 Then
            If ChcekPM_Type = False Then
                Exit Sub
            Else
                GoTo AA
            End If
        Else
            For i = 0 To oblist.Count - 1
                obi.M_Code = oblist(i).M_Code
                obi.OS_BatchID = OS_BatchID.Text
                obi.OB_PID = ds.Tables("Prod_Mounting_New").Rows(i)("PM_PID")
                obi.OB_AccountCheck = ds.Tables("Prod_Mounting_New").Rows(i)("PM_Check")
                obi.OB_AccountRemark = MemoEdit1.Text

                obc.OrdersBom_Check_Update(obi)

            Next

        End If


            If ChcekPM_Type = False Then
                Dim l, j As Integer
                For l = 0 To ds.Tables("Prod_Mounting_New").Rows.Count - 1
                    If ds.Tables("Prod_Mounting_New").Rows(l)("PM_Check") = False Then
                        j = j + 1
                    End If
                Next
                If j = ds.Tables("Prod_Mounting_New").Rows.Count Then
                    CheckBox1.Checked = False
                    MsgBox("���妸�f�֪��A�ܧ󬰥��f�֡I")
                End If
            End If

AA:

            Dim oi As New OrdersSubInfo
            Dim oc As New OrdersSubController
            oi.OS_BatchID = OS_BatchID.Text
            oi.OS_Check = CheckBox1.Checked
            oi.OS_CheckRemark = MemoEdit1.Text
            oi.OS_CheckAction = InUser

            If oc.OrdersSub_UpdateCheck(oi) = True Then
                MsgBox("�f�֪��A�w���ܡI", , "����")
            Else
                MsgBox("�f�֥��ѡA���ˬd��]�I")
            End If


    End Sub
    Sub SaveNew()

        '((((((�q��򥻸�Ʒs�W
        Dim oi As New OrdersSubInfo
        Dim oc As New OrdersSubController
        OS_BatchID.Text = GetOS_BatchID()

        oi.OM_ID = OM_ID.Text
        oi.OS_BatchID = OS_BatchID.Text

        oi.PM_M_Code = PM_M_Code.Text

        oi.M_Code = gluM_Code.EditValue             '@ 2012/9/22 �K�[

        oi.OS_Qty = OS_Qty.Text
        oi.OS_Sprace = OS_Sprace.Text
        oi.OS_SpraceQty = CInt(CStr(CInt(OS_Qty.Text) + CInt(OS_Qty.Text) * Val(OS_Sprace.Text)))

        oi.OS_NoSendQty = OS_Qty.Text
        oi.OS_NoOutQty = OS_Qty.Text

        If OS_SendDate.Text = "" Then
            oi.OS_SendDate = Nothing
        Else
            oi.OS_SendDate = OS_SendDate.Text
        End If


        If OS_CheckDate.Text = "" Then
            oi.OS_CheckDate = Nothing
        Else
            oi.OS_CheckDate = OS_CheckDate.Text
        End If

        oi.OS_Price = Nothing

        If OS_Type.Text = "" Then
            oi.OS_Type = Nothing
        Else
            oi.OS_Type = OS_Type.Text
        End If

        oi.OS_MakeDetail = Nothing
        If OS_Fac.Text = "" Then
            oi.OS_Fac = Nothing
        Else
            oi.OS_Fac = OS_Fac.EditValue
        End If

        If OS_Plate.Text = "" Then
            oi.OS_Plate = Nothing
        Else
            oi.OS_Plate = OS_Plate.Text
        End If

        If OS_SendNeed.Text = "" Then
            oi.OS_SendNeed = Nothing
        Else
            oi.OS_SendNeed = OS_SendNeed.Text

        End If
        oi.OS_SendType = OS_SendType.EditValue
        oi.M_BatchID = M_BatchID.Text

        oi.OS_ToHK = "0"
        oi.OS_AccountCheck = "False"
        oi.OS_AccountAction = Nothing
        oi.OS_AccountRemark = Nothing
        oi.OS_Check = "False"
        oi.OS_Action = InUserID
        oi.OS_CheckAction = Nothing
        oi.OS_CheckRemark = Nothing

        If OS_Remark.Text = "" Then
            oi.OS_Remark = Nothing
        Else
            oi.OS_Remark = OS_Remark.Text
        End If

        oi.OS_AddDate = Now
        oi.OS_EditDate = Now

        oi.OS_ProductionWeek = tieOS_ProductionWeek_Year.Text & cboOS_ProductionWeek.Text

        If oc.OrdersSub_Add(oi) = False Then
            MsgBox("����,���ˬd�򥻸�ơI")
            Exit Sub

        End If
        ' )))))))�q��򥻸�Ʒs�W


        '(((((((�q��Ͳ����ؤ��e�s�W,�]���Ͳ����ର�šA�_�h������ܥX���A�ҥHGOTOA
        If ds.Tables("OrdersDetail").Rows.Count = 0 Then GoTo A
        Dim i As Integer
        For i = 0 To ds.Tables("OrdersDetail").Rows.Count - 1

            Dim odi As New OrdersDetailInfo
            Dim odc As New OrdersDetailController
            odi.OS_BatchID = OS_BatchID.Text
            If IsDBNull(ds.Tables("OrdersDetail").Rows(i)("OD_Name")) Then
                odi.OD_Name = Nothing
            Else
                odi.OD_Name = ds.Tables("OrdersDetail").Rows(i)("OD_Name")
            End If

            If IsDBNull(ds.Tables("OrdersDetail").Rows(i)("OD_Detail")) Then
                odi.OD_Detail = Nothing
            Else
                odi.OD_Detail = ds.Tables("OrdersDetail").Rows(i)("OD_Detail")
            End If

            odc.OrdersDetail_Add(odi)
        Next

A:      Dim odi2 As New OrdersDetailInfo
        Dim odc2 As New OrdersDetailController
        odi2.OS_BatchID = OS_BatchID.Text

        odi2.OD_Name = "�q���ƪ`"



        odi2.OD_Detail = "Null"


        odc2.OrdersDetail_Add(odi2)
        '))))))))))))))))))))�q��Ͳ����حק�

        '((((((((((((((���ƲM��
        i = 0
        For i = 0 To ds.Tables("Prod_Mounting_New").Rows.Count - 1
            Dim obc As New OrdersBomController
            Dim obi As New OrdersBomInfo
            obi.OS_BatchID = OS_BatchID.Text
            obi.M_Code = ds.Tables("Prod_Mounting_New").Rows(i)("M_Code")
            obi.OB_PID = ds.Tables("Prod_Mounting_New").Rows(i)("PM_PID")
            obi.OB_Key = ds.Tables("Prod_Mounting_New").Rows(i)("PM_Key")
            obi.OB_Qty = ds.Tables("Prod_Mounting_New").Rows(i)("PM_Qty")

            Dim c As Integer = 0
            c = CInt(OS_Qty.Text) + CInt(OS_Qty.Text) * Val(OS_Sprace.Text)
            c = ds.Tables("Prod_Mounting_New").Rows(i)("PM_Qty") * c
            obi.OB_MainQty = CStr(c)

            If IsDBNull(ds.Tables("Prod_Mounting_New").Rows(i)("M_Supplier")) Then
                obi.OB_Supplier = Nothing
            Else
                obi.OB_Supplier = ds.Tables("Prod_Mounting_New").Rows(i)("M_Supplier")
            End If


            If IsDBNull(ds.Tables("Prod_Mounting_New").Rows(i)("M_SupplierNo")) Then
                obi.OB_SupplierNo = Nothing
            Else
                obi.OB_SupplierNo = ds.Tables("Prod_Mounting_New").Rows(i)("M_SupplierNo")
            End If


            obi.OB_Make = ds.Tables("Prod_Mounting_New").Rows(i)("PM_Make")

            If IsDBNull(ds.Tables("Prod_Mounting_New").Rows(i)("PM_MakeRemark")) Then
                obi.OB_MakeRemark = Nothing
            Else
                obi.OB_MakeRemark = ds.Tables("Prod_Mounting_New").Rows(i)("PM_MakeRemark")
            End If

            If IsDBNull(ds.Tables("Prod_Mounting_New").Rows(i)("PM_ID")) Then
                obi.OB_ID = Nothing
            Else
                obi.OB_ID = ds.Tables("Prod_Mounting_New").Rows(i)("PM_ID")
            End If

            Dim strA As String
            Dim IntA As Integer = 0

            '�ˬdDataSet�����O��,�P�_�O�_�ݭn����
            strA = ds.Tables("Prod_Mounting_New").Rows(i)("PM_Key")

            For Each DR As DataRow In ds.Tables("Prod_Mounting_New").Select("PM_PID = '" & strA & "'")
                IntA = IntA + 1
            Next
            If IntA > 0 Then
                obi.OB_Product = "���ݱ���"
            Else
                obi.OB_Product = "�ݱ���"
            End If

            If IsDBNull(ds.Tables("Prod_Mounting_New").Rows(i)("PM_LVL")) Then
                obi.PM_LVL = Nothing
            Else
                obi.PM_LVL = ds.Tables("Prod_Mounting_New").Rows(i)("PM_LVL")
            End If


            If IsDBNull(ds.Tables("Prod_Mounting_New").Rows(i)("M_CodeType")) Then
                obi.M_CodeType = Nothing
            Else
                obi.M_CodeType = ds.Tables("Prod_Mounting_New").Rows(i)("M_CodeType")
            End If
            If IsDBNull(ds.Tables("Prod_Mounting_New").Rows(i)("M_CodeMouldNO")) Then
                obi.M_CodeMouldNO = Nothing
            Else
                obi.M_CodeMouldNO = ds.Tables("Prod_Mounting_New").Rows(i)("M_CodeMouldNO")
            End If
            If IsDBNull(ds.Tables("Prod_Mounting_New").Rows(i)("OB_MakeDepartment")) Then
                obi.OB_MakeDepartment = Nothing
            Else
                obi.OB_MakeDepartment = ds.Tables("Prod_Mounting_New").Rows(i)("OB_MakeDepartment")
            End If
            If IsDBNull(ds.Tables("Prod_Mounting_New").Rows(i)("OB_MModeCusterNO")) Then
                obi.OB_MModeCusterNO = Nothing
            Else
                obi.OB_MModeCusterNO = ds.Tables("Prod_Mounting_New").Rows(i)("OB_MModeCusterNO")
            End If
            obi.M_Weight = ds.Tables("Prod_Mounting_New").Rows(i)("M_Weight")    '@ 2013/4/17 �K�[

            obi.OB_AccountCheck = "False"
            obc.OrdersBom_Add(obi)
        Next
        ')))))))))))))))))))))))))))))))))))))))))���ƲM��


        '(((((((�q��Ͳ����{�ɳ�s�W
        If ds.Tables("OrdersTempDetail").Rows.Count = 0 Then Exit Sub

        For i = 0 To ds.Tables("OrdersTempDetail").Rows.Count - 1

            Dim odi As New OrdersTempDetailInfo
            Dim odc As New OrdersTempDetailController

            odi.OS_BatchID = OS_BatchID.Text
            If IsDBNull(ds.Tables("OrdersTempDetail").Rows(i)("OT_Detail")) Then
                odi.OT_Detail = Nothing
            Else
                odi.OT_Detail = ds.Tables("OrdersTempDetail").Rows(i)("OT_Detail")
            End If
            odi.OT_AddDate = Format(Now, "yyyy-MM-dd HH:mm:ss")
            odi.OT_EditDate = Format(Now, "yyyy-MM-dd HH:mm:ss")
            odi.OT_Check = "False"
            odi.OT_Action = InUserID
            odc.OrdersTempDetail_Add(odi)
        Next
        '))))))))))))))))))))�q��Ͳ����{�ɳ�

    End Sub

    Private Sub SimpleButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton2.Click
        Me.Close()
    End Sub

    Private Sub popOrderDetailAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles popOrderDetailAdd.Click

        Dim row As DataRow = ds.Tables("OrdersDetail").NewRow()
        ds.Tables("OrdersDetail").Rows.Add(row)
        GridView1.MoveLast()
    End Sub

    Private Sub popOrderDetailDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles popOrderDetailDel.Click

        If ds.Tables("OrdersDetail").Rows.Count = 1 Then
            MsgBox("����R���̦Z�@���O��!", , "����")
            Exit Sub
        End If
        If GridView1.RowCount = 0 Then Exit Sub
        Dim DelTemp As String
        DelTemp = GridView1.GetRowCellDisplayText(ArrayToString(GridView1.GetSelectedRows()), "OD_ID")
        If DelTemp = "OD_ID" Then
        Else
            '�b�R�����W�[�Q�R�����O��
            Dim row As DataRow = ds.Tables("DelDataOrdersDetail").NewRow
            row("OD_ID") = DelTemp
            ds.Tables("DelDataOrdersDetail").Rows.Add(row)
        End If
        ds.Tables("OrdersDetail").Rows.RemoveAt(CInt(ArrayToString(GridView1.GetSelectedRows())))
    End Sub

    Function GetP_ID() As Integer
        '���o�Ǹ�
        On Error Resume Next
        Dim i As Long
        Dim l As Long
        For i = 0 To ds.Tables("Prod_Mounting_New").Rows.Count - 1
            If ds.Tables("Prod_Mounting_New").Rows(i)("PM_LVL") = 0 Then
                l = l + 1
            End If
        Next
        GetP_ID = l + 1
    End Function

    '�ɤJ��w�t��U���Ҧ��l����
    Sub LoadBomSubToTable(ByVal tList As List(Of ProductBomInfo))

        '�S���l���ƫh�h�X
        If tList Is Nothing Then Exit Sub

        On Error Resume Next
        Dim i As Integer
        Dim row As DataRow
        For i = 0 To tList.Count - 1
            row = ds.Tables("Prod_Mounting_New").NewRow
            row("PM_M_Code") = tList(i).PM_M_Code
            row("PM_ID") = intTempID
            row("M_Code") = tList(i).M_Code
            row("M_CodePID") = tList(i).M_CodePID
            row("PM_Qty") = Format(tList(i).PM_Qty, "0.0000")
            row("PM_MakeRemark") = tList(i).PM_MakeRemark
            row("PM_Make") = tList(i).PM_Make
            row("PM_Check") = tList(i).PM_Check
            row("M_Supplier") = tList(i).M_Supplier
            row("M_SupplierNo") = tList(i).M_SupplierNo
            row("IsNew") = False
            row("M_Name") = tList(i).M_Name
            row("M_Gauge") = tList(i).M_Gauge
            row("M_Unit") = tList(i).M_Unit
            row("PM_LVL") = tList(i).PM_LVL
            row("PM_Key") = tList(i).PM_Key
            row("PM_PID") = tList(i).PM_PID
            row("M_CodeType") = tList(i).M_CodeType
            row("M_CodeMouldNO") = tList(i).M_CodeMouldNO

            row("M_Weight") = tList(i).M_Weight      '@ 2013/4/17 �K�[

            ds.Tables("Prod_Mounting_New").Rows.Add(row)
        Next

        TreeList1.ExpandAll()
    End Sub
    Sub LoadDataOrdersSub()
        '���J�Ͳ���ƾ�

        Dim objInfo As New OrdersSubInfo
        Dim oc As New OrdersSubController

        objInfo = oc.OrdersSub_Get(CStr(OS_BatchID.Text))
        If objInfo Is Nothing Then
            '�S���ƾ�

            Exit Sub
        End If

        If objInfo.OS_AccountCheck = "True" Then
            CheckBox2.Checked = True
        Else
            If objInfo.OS_AccountCheck = "False" Then
                CheckBox2.Checked = False
            End If
        End If

        If objInfo.OS_Check = "True" Then
            CheckBox1.Checked = True
        Else
            If objInfo.OS_Check = "False" Then
                CheckBox1.Checked = False
            End If
        End If


        If tempCode <> "Copy" Then
            OM_ID.Text = objInfo.OM_ID
            OS_Qty.Text = objInfo.OS_Qty
            intOM_NoMakeQty = intOM_NoMakeQty + objInfo.OS_Qty
            OS_Sprace.Text = objInfo.OS_Sprace
            IntOS_Qty_Edit = objInfo.OS_Qty  '�b�ק��,�P�_�ƶq�ɭn��
        End If

        'intOM_NoMakeQty = intOM_NoMakeQty + objInfo.OS_Qty

        PM_M_Code.Text = objInfo.PM_M_Code
        gluM_Code.EditValue = objInfo.M_Code


        OS_Type.EditValue = objInfo.OS_Type
        OS_Fac.EditValue = objInfo.OS_Fac
        OS_SendDate.Text = Format(objInfo.OS_SendDate, "short date")
        OS_CheckDate.Text = Format(objInfo.OS_CheckDate, "short date")
        OS_Plate.Text = objInfo.OS_Plate
        OS_Remark.Text = objInfo.OS_Remark
        OS_SendNeed.Text = objInfo.OS_SendNeed
        OS_SendType.EditValue = objInfo.OS_SendType
        M_BatchID.Text = objInfo.M_BatchID
        cboOS_ProductionWeek.Text = Mid(objInfo.OS_ProductionWeek, 6)
        tieOS_ProductionWeek_Year.EditValue = Mid(objInfo.OS_ProductionWeek, 1, 5)
        '----------------------------------------------------
        If objInfo.OS_Check = False Then
            CheckBox1.Checked = False
        Else
            CheckBox1.Checked = True
        End If
        MemoEdit1.Text = objInfo.OS_CheckRemark
    End Sub
    Sub LoadDataOrdersDetail()
        '���J�Ͳ����ؤ��e
        Dim odc As New OrdersDetailController
        Dim odl As List(Of OrdersDetailInfo)
        odl = odc.OrdersDetail_GetList(OS_BatchID.Text, Nothing)
        If odl Is Nothing Then Exit Sub
        Dim i As Integer
        Dim row As DataRow
        For i = 0 To odl.Count - 1
            row = ds.Tables("OrdersDetail").NewRow
            row("OD_ID") = odl(i).OD_ID
            row("OD_Name") = odl(i).OD_Name
            row("OD_Detail") = odl(i).OD_Detail
            ds.Tables("OrdersDetail").Rows.Add(row)
        Next
    End Sub
    Sub LoadDataOrdersTempDetail()
        '���J�{�ɳ椺�e
        Dim odc As New OrdersTempDetailController
        Dim odl As List(Of OrdersTempDetailInfo)
        odl = odc.OrdersTempDetail_GetList(Nothing, OS_BatchID.Text)
        If odl Is Nothing Then Exit Sub
        Dim i As Integer
        Dim row As DataRow
        For i = 0 To odl.Count - 1
            row = ds.Tables("OrdersTempDetail").NewRow
            row("OT_ID") = odl(i).OT_ID
            row("OT_Detail") = odl(i).OT_Detail
            ds.Tables("OrdersTempDetail").Rows.Add(row)
        Next
    End Sub
    Sub LoadDataOrdersBom()
        '���J���ƲM��

        Dim obc As New OrdersBomController
        Dim tlist As List(Of OrdersBomInfo)
        ds.Tables("Prod_Mounting_New").Rows.Clear()

        tlist = obc.OrdersBom_GetList(Nothing, OS_BatchID.Text, Nothing, Nothing)
        If tlist Is Nothing Then Exit Sub

        On Error Resume Next
        Dim i As Integer
        Dim row As DataRow
        For i = 0 To tlist.Count - 1
            row = ds.Tables("Prod_Mounting_New").NewRow

            row("AutoID") = tlist(i).AutoID
            row("PM_ID") = intTempID
            row("M_Code") = tlist(i).M_Code
            row("PM_Qty") = tlist(i).OB_Qty
            row("PM_MakeRemark") = tlist(i).OB_MakeRemark
            row("PM_Make") = tlist(i).OB_Make

            row("M_Supplier") = tlist(i).OB_Supplier
            row("M_SupplierNo") = tlist(i).OB_SupplierNo
            row("IsNew") = False
            row("M_Name") = tlist(i).M_Name
            row("M_Gauge") = tlist(i).M_Gauge

            row("PM_Check") = tlist(i).OB_AccountCheck '�q�L�妸�f�֦Z��ӧ妸���Ƽf�֫H��

            '    row("M_Unit") = tList(i).M_Unit
            '  row("PM_LVL") = tList(i).PM_LVL
            row("PM_Key") = tlist(i).OB_Key
            row("PM_PID") = tlist(i).OB_PID
            row("PM_LVL") = tlist(i).PM_LVL
            row("M_CodeType") = tlist(i).M_CodeType
            row("M_CodeMouldNO") = tlist(i).M_CodeMouldNO
            row("OB_MakeDepartment") = tlist(i).OB_MakeDepartment
            row("OB_MModeCusterNO") = tlist(i).OB_MModeCusterNO

            row("M_Weight") = tlist(i).M_Weight           '@ 2013/4/17 �K�[
            row("M_Unit") = tlist(i).M_Unit            '@ 2013/4/17 �K�[


            ds.Tables("Prod_Mounting_New").Rows.Add(row)
        Next

        TreeList1.ExpandAll()


        If ds.Tables("Prod_Mounting_New").Rows.Count = 0 And ChcekPM_Type = False Then
            MsgBox("�����J���~�t���Ʃβ��~��ưt�󥼫إߡI", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)

        End If


    End Sub

    Sub SaveEdit()
        '(((((((((((((�q��򥻸�ƭק�
        Dim oi As New OrdersSubInfo
        Dim oc As New OrdersSubController
        oi.OM_ID = OM_ID.Text
        oi.OS_BatchID = OS_BatchID.Text

        oi.PM_M_Code = PM_M_Code.Text

        oi.M_Code = gluM_Code.EditValue             '@ 2012/9/22 �K�[

        oi.OS_Qty = OS_Qty.Text
        oi.OS_Sprace = OS_Sprace.Text
        oi.OS_SpraceQty = CInt(CStr(CInt(OS_Qty.Text) + CInt(OS_Qty.Text) * Val(OS_Sprace.Text)))

        'oi.OS_NoSendQty = OS_Qty.Text
        'oi.OS_NoOutQty = OS_Qty.Text

        If OS_SendDate.Text = "" Then
            oi.OS_SendDate = Nothing
        Else
            oi.OS_SendDate = OS_SendDate.Text
        End If


        If OS_CheckDate.Text = "" Then
            oi.OS_CheckDate = Nothing
        Else
            oi.OS_CheckDate = OS_CheckDate.Text
        End If

        oi.OS_Price = Nothing

        If OS_Type.Text = "" Then
            oi.OS_Type = Nothing
        Else
            oi.OS_Type = OS_Type.Text
        End If

        oi.OS_MakeDetail = Nothing
        If OS_Fac.Text = "" Then
            oi.OS_Fac = Nothing
        Else
            oi.OS_Fac = OS_Fac.EditValue
        End If

        If OS_Plate.Text = "" Then
            oi.OS_Plate = Nothing
        Else
            oi.OS_Plate = OS_Plate.Text
        End If

        If OS_SendNeed.Text = "" Then
            oi.OS_SendNeed = Nothing
        Else
            oi.OS_SendNeed = OS_SendNeed.Text
        End If
        oi.OS_SendType = OS_SendType.EditValue
        oi.M_BatchID = M_BatchID.Text

        oi.OS_ToHK = "0"
        oi.OS_AccountCheck = "False"
        oi.OS_AccountAction = Nothing
        oi.OS_AccountRemark = Nothing
        oi.OS_Check = "False"
        oi.OS_Action = InUserID
        oi.OS_CheckRemark = Nothing

        If OS_Remark.Text = "" Then
            oi.OS_Remark = Nothing
        Else
            oi.OS_Remark = OS_Remark.Text
        End If

        '     oi.OS_AddDate = Now
        oi.OS_EditDate = Now

        oi.OS_ProductionWeek = tieOS_ProductionWeek_Year.Text & cboOS_ProductionWeek.Text

        If oc.OrdersSub_Update(oi) = False Then
            MsgBox("����,���ˬd�򥻸�ơI")
            Exit Sub

        End If
        ')))))))�q��򥻸�ƭק�


        '((((((((((((�Ͳ����ؤ��e�ק�
        Dim i As Integer
        If ds.Tables("OrdersDetail").Rows.Count = 0 Then GoTo A
        For i = 0 To ds.Tables("OrdersDetail").Rows.Count - 1

            If IsDBNull(ds.Tables("OrdersDetail").Rows(i)("OD_ID")) Then
                '�p�G��ƬO�s�W�[��
                Dim odc As New OrdersDetailController
                Dim odi As New OrdersDetailInfo
                odi.OS_BatchID = OS_BatchID.Text

                If IsDBNull(ds.Tables("OrdersDetail").Rows(i)("OD_Name")) Then
                    odi.OD_Name = ""
                Else
                    odi.OD_Name = ds.Tables("OrdersDetail").Rows(i)("OD_Name")
                End If
                If IsDBNull(ds.Tables("OrdersDetail").Rows(i)("OD_Detail")) Then
                    odi.OD_Detail = ""
                Else
                    odi.OD_Detail = ds.Tables("OrdersDetail").Rows(i)("OD_Detail")
                End If

                odc.OrdersDetail_Add(odi)
            End If

            If Not IsDBNull(ds.Tables("OrdersDetail").Rows(i)("OD_ID")) Then
                '�p�G��ư��O�ק�
                Dim odc As New OrdersDetailController
                Dim odi As New OrdersDetailInfo
                odi.OS_BatchID = OS_BatchID.Text
                odi.OD_ID = ds.Tables("OrdersDetail").Rows(i)("OD_ID")

                If IsDBNull(ds.Tables("OrdersDetail").Rows(i)("OD_Name")) Then
                    odi.OD_Name = ""
                Else
                    odi.OD_Name = ds.Tables("OrdersDetail").Rows(i)("OD_Name")
                End If
                If IsDBNull(ds.Tables("OrdersDetail").Rows(i)("OD_Detail")) Then
                    odi.OD_Detail = ""
                Else
                    odi.OD_Detail = ds.Tables("OrdersDetail").Rows(i)("OD_Detail")
                End If

                odc.OrdersDetail_Update(odi)


            End If


        Next

        '��s�R�����O��
        If ds.Tables("DelDataOrdersDetail").Rows.Count > 0 Then
            For i = 0 To ds.Tables("DelDataOrdersDetail").Rows.Count - 1
                Dim odc As New OrdersDetailController
                Dim odi As New OrdersDetailInfo
                odi.OD_ID = ds.Tables("DelDataOrdersDetail").Rows(i)("OD_ID")
                odc.OrdersDetail_ODID_Del(odi)
            Next i
        End If
        ')))))))))))))))))))))))))�Ͳ����ؤ��e�ק�

        '((((((((((((((���ƲM��ק�

A:      Dim j As Integer     ''''���t�ʡ��Ҧ��U�R�����ƲM�椤�Y�ǰO��
        If ds.Tables("DelDataOrdersBomProduct").Rows.Count > 0 Then
            For j = 0 To ds.Tables("DelDataOrdersBomProduct").Rows.Count - 1

                Dim obi As New OrdersBomInfo
                Dim obc As New OrdersBomController

                obi.OB_Key = ds.Tables("DelDataOrdersBomProduct").Rows(j)("PM_Key")
                obi.OS_BatchID = OS_BatchID.Text

                obc.OrdersBom_Del_1(obi.OS_BatchID, obi.OB_Key)

            Next j
        End If

        i = 0

        For i = 0 To ds.Tables("Prod_Mounting_New").Rows.Count - 1

            If IsDBNull(ds.Tables("Prod_Mounting_New").Rows(i)("AutoID")) Then
                Dim obc1 As New OrdersBomController
                Dim obi1 As New OrdersBomInfo
                obi1.OS_BatchID = OS_BatchID.Text
                obi1.M_Code = ds.Tables("Prod_Mounting_New").Rows(i)("M_Code")
                obi1.OB_PID = ds.Tables("Prod_Mounting_New").Rows(i)("PM_PID")
                obi1.OB_Key = ds.Tables("Prod_Mounting_New").Rows(i)("PM_Key")
                obi1.OB_Qty = ds.Tables("Prod_Mounting_New").Rows(i)("PM_Qty")

                Dim c As Integer = 0
                c = CInt(OS_Qty.Text) + CInt(OS_Qty.Text) * Val(OS_Sprace.Text)
                c = ds.Tables("Prod_Mounting_New").Rows(i)("PM_Qty") * c
                obi1.OB_MainQty = CStr(c)

                If IsDBNull(ds.Tables("Prod_Mounting_New").Rows(i)("M_Supplier")) Then
                    obi1.OB_Supplier = Nothing
                Else
                    obi1.OB_Supplier = ds.Tables("Prod_Mounting_New").Rows(i)("M_Supplier")
                End If


                If IsDBNull(ds.Tables("Prod_Mounting_New").Rows(i)("M_SupplierNo")) Then
                    obi1.OB_SupplierNo = Nothing
                Else
                    obi1.OB_SupplierNo = ds.Tables("Prod_Mounting_New").Rows(i)("M_SupplierNo")
                End If


                obi1.OB_Make = ds.Tables("Prod_Mounting_New").Rows(i)("PM_Make")

                If IsDBNull(ds.Tables("Prod_Mounting_New").Rows(i)("PM_MakeRemark")) Then
                    obi1.OB_MakeRemark = Nothing
                Else
                    obi1.OB_MakeRemark = ds.Tables("Prod_Mounting_New").Rows(i)("PM_MakeRemark")
                End If

                If IsDBNull(ds.Tables("Prod_Mounting_New").Rows(i)("PM_ID")) Then
                    obi1.OB_ID = Nothing
                Else
                    obi1.OB_ID = ds.Tables("Prod_Mounting_New").Rows(i)("PM_ID")
                End If

                Dim strA As String
                Dim IntA As Integer = 0

                '�ˬdDataSet�����O��,�P�_�O�_�ݭn����
                strA = ds.Tables("Prod_Mounting_New").Rows(i)("PM_Key")

                For Each DR As DataRow In ds.Tables("Prod_Mounting_New").Select("PM_PID = '" & strA & "'")
                    IntA = IntA + 1
                Next
                If IntA > 0 Then
                    obi1.OB_Product = "���ݱ���"
                Else
                    obi1.OB_Product = "�ݱ���"
                End If

                If IsDBNull(ds.Tables("Prod_Mounting_New").Rows(i)("PM_LVL")) Then
                    obi1.PM_LVL = Nothing
                Else
                    obi1.PM_LVL = ds.Tables("Prod_Mounting_New").Rows(i)("PM_LVL")
                End If


                If IsDBNull(ds.Tables("Prod_Mounting_New").Rows(i)("M_CodeType")) Then
                    obi1.M_CodeType = Nothing
                Else
                    obi1.M_CodeType = ds.Tables("Prod_Mounting_New").Rows(i)("M_CodeType")
                End If
                If IsDBNull(ds.Tables("Prod_Mounting_New").Rows(i)("M_CodeMouldNO")) Then
                    obi1.M_CodeMouldNO = Nothing
                Else
                    obi1.M_CodeMouldNO = ds.Tables("Prod_Mounting_New").Rows(i)("M_CodeMouldNO")
                End If
                If IsDBNull(ds.Tables("Prod_Mounting_New").Rows(i)("OB_MakeDepartment")) Then
                    obi1.OB_MakeDepartment = Nothing
                Else
                    obi1.OB_MakeDepartment = ds.Tables("Prod_Mounting_New").Rows(i)("OB_MakeDepartment")
                End If
                If IsDBNull(ds.Tables("Prod_Mounting_New").Rows(i)("OB_MModeCusterNO")) Then
                    obi1.OB_MModeCusterNO = Nothing
                Else
                    obi1.OB_MModeCusterNO = ds.Tables("Prod_Mounting_New").Rows(i)("OB_MModeCusterNO")
                End If

                obi1.M_Weight = ds.Tables("Prod_Mounting_New").Rows(i)("M_Weight")    '@ 2013/4/17 �K�[

                obi1.OB_AccountCheck = "False"
                obc1.OrdersBom_Add(obi1)
            End If
            If Not IsDBNull(ds.Tables("Prod_Mounting_New").Rows(i)("AutoID")) Then
                Dim obc As New OrdersBomController
                Dim obi As New OrdersBomInfo
                obi.OS_BatchID = OS_BatchID.Text
                obi.M_Code = ds.Tables("Prod_Mounting_New").Rows(i)("M_Code")
                obi.OB_Qty = ds.Tables("Prod_Mounting_New").Rows(i)("PM_Qty")

                Dim c As Integer = 0
                c = CInt(OS_Qty.Text) + CInt(OS_Qty.Text) * Val(OS_Sprace.Text)
                c = ds.Tables("Prod_Mounting_New").Rows(i)("PM_Qty") * c
                obi.OB_MainQty = CStr(c)

                obi.OB_Make = ds.Tables("Prod_Mounting_New").Rows(i)("PM_Make")

                If IsDBNull(ds.Tables("Prod_Mounting_New").Rows(i)("PM_MakeRemark")) Then
                    obi.OB_MakeRemark = Nothing
                Else
                    obi.OB_MakeRemark = ds.Tables("Prod_Mounting_New").Rows(i)("PM_MakeRemark")
                End If

                If IsDBNull(ds.Tables("Prod_Mounting_New").Rows(i)("M_CodeType")) Then
                    obi.M_CodeType = Nothing
                Else
                    obi.M_CodeType = ds.Tables("Prod_Mounting_New").Rows(i)("M_CodeType")
                End If
                If IsDBNull(ds.Tables("Prod_Mounting_New").Rows(i)("M_CodeMouldNO")) Then
                    obi.M_CodeMouldNO = Nothing
                Else
                    obi.M_CodeMouldNO = ds.Tables("Prod_Mounting_New").Rows(i)("M_CodeMouldNO")
                End If
                If IsDBNull(ds.Tables("Prod_Mounting_New").Rows(i)("OB_MakeDepartment")) Then
                    obi.OB_MakeDepartment = Nothing
                Else
                    obi.OB_MakeDepartment = ds.Tables("Prod_Mounting_New").Rows(i)("OB_MakeDepartment")
                End If
                If IsDBNull(ds.Tables("Prod_Mounting_New").Rows(i)("OB_MModeCusterNO")) Then
                    obi.OB_MModeCusterNO = Nothing
                Else
                    obi.OB_MModeCusterNO = ds.Tables("Prod_Mounting_New").Rows(i)("OB_MModeCusterNO")
                End If
                obi.M_Weight = ds.Tables("Prod_Mounting_New").Rows(i)("M_Weight")    '@ 2013/4/17 �K�[

                obc.OrdersBom_Make_Update(obi)
            End If

        Next



        ')))))))))))))))))))))))))))))))))))))))))���ƲM��ק�



        '((((((((((((�{�ɳ�ק�

        If ds.Tables("OrdersTempDetail").Rows.Count = 0 Then GoTo B
        For i = 0 To ds.Tables("OrdersTempDetail").Rows.Count - 1

            If IsDBNull(ds.Tables("OrdersTempDetail").Rows(i)("OT_ID")) Then
                '�p�G��ƬO�s�W�[��
                Dim odi As New OrdersTempDetailInfo
                Dim odc As New OrdersTempDetailController

                odi.OS_BatchID = OS_BatchID.Text
                If IsDBNull(ds.Tables("OrdersTempDetail").Rows(i)("OT_Detail")) Then
                    odi.OT_Detail = Nothing
                Else
                    odi.OT_Detail = ds.Tables("OrdersTempDetail").Rows(i)("OT_Detail")
                End If
                odi.OT_AddDate = Format(Now, "yyyy-MM-dd HH:mm:ss")
                odi.OT_EditDate = Format(Now, "yyyy-MM-dd HH:mm:ss")
                odi.OT_Check = "False"
                odi.OT_Action = InUserID
                odc.OrdersTempDetail_Add(odi)
            End If

            If Not IsDBNull(ds.Tables("OrdersTempDetail").Rows(i)("OT_ID")) Then
                '�p�G��ư��O�ק�
                Dim odc As New OrdersTempDetailController
                Dim odi As New OrdersTempDetailInfo
                If IsDBNull(ds.Tables("OrdersTempDetail").Rows(i)("OT_Detail")) Then
                    odi.OT_Detail = Nothing
                Else
                    odi.OT_Detail = ds.Tables("OrdersTempDetail").Rows(i)("OT_Detail")
                End If

                odi.OS_BatchID = OS_BatchID.Text
                odi.OT_EditDate = Format(Now, "yyyy-MM-dd HH:mm:ss")
                odi.OT_Check = "False"
                odi.OT_Action = InUserID
                odi.OT_ID = ds.Tables("OrdersTempDetail").Rows(i)("OT_ID")
                odc.OrdersTempDetail_Update(odi)
            End If


        Next

        '��s�R�����O��
B:      If ds.Tables("DelDataOrdersTempDetail").Rows.Count > 0 Then
            For i = 0 To ds.Tables("DelDataOrdersTempDetail").Rows.Count - 1
                Dim odc As New OrdersTempDetailController
                Dim odi As New OrdersTempDetailInfo
                odi.OT_ID = ds.Tables("DelDataOrdersTempDetail").Rows(i)("OT_ID")
                odc.OrdersTempDetail_OTID_Del(odi)
            Next i
        End If
        ')))))))))))))))))))))))))�{�ɳ�ק�

    End Sub

    Private Sub popOrderTempDetailAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles popOrderTempDetailAdd.Click
        Dim row As DataRow = ds.Tables("OrdersTempDetail").NewRow()
        ds.Tables("OrdersTempDetail").Rows.Add(row)
        GridView2.MoveLast()
    End Sub

    Private Sub popOrderTempDetailDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles popOrderTempDetailDel.Click


        If GridView1.RowCount = 0 Then Exit Sub
        Dim DelTemp As String
        DelTemp = GridView2.GetRowCellDisplayText(ArrayToString(GridView2.GetSelectedRows()), "OT_ID")
        If DelTemp = "OT_ID" Then
        Else
            '�b�R�����W�[�Q�R�����O��
            Dim row As DataRow = ds.Tables("DelDataOrdersTempDetail").NewRow
            row("OT_ID") = DelTemp
            ds.Tables("DelDataOrdersTempDetail").Rows.Add(row)
        End If
        ds.Tables("OrdersTempDetail").Rows.RemoveAt(CInt(ArrayToString(GridView2.GetSelectedRows())))

    End Sub

    Private Sub PM_M_Code_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles PM_M_Code.EditValueChanged
        'On Error GoTo A
        Dim pbc As New ProductBomController
        Dim pbi As List(Of ProductBomInfo)
        Dim row3 As DataRow
        Dim i%

        gluM_Code.Text = ""
        If IsDBNull(PM_M_Code.EditValue) = False And PM_M_Code.EditValue <> "" Then
            ds.Tables("ProductM_Code").Clear()

            row3 = ds.Tables("ProductM_Code").NewRow
            row3("M_Code") = PM_M_Code.EditValue
            row3("M_Name") = PM_M_Code.EditValue

            ds.Tables("ProductM_Code").Rows.Add(row3)

            pbi = pbc.Prod_Mounting_New_GetList(PM_M_Code.EditValue, Nothing, Nothing, 0, Nothing, Nothing, Nothing)

            If pbi.Count > 0 Then
                For i = 0 To pbi.Count - 1
                    Dim row1 As DataRow
                    row1 = ds.Tables("ProductM_Code").NewRow
                    row1("M_Code") = pbi(i).M_Code
                    row1("M_Name") = pbi(i).M_Name

                    ds.Tables("ProductM_Code").Rows.Add(row1)
                Next
            End If
        End If

        Select Case Label19.Text

            Case "�j�f�妸"
                If Edit = False And tempCode <> "Copy" Then
                    Dim mcBom As New ProductBomController
                    ds.Tables("Prod_Mounting_New").Rows.Clear()
                    ds.Tables("OrdersDetail").Rows.Clear()
                    'LoadBomSubToTable(mcBom.ProductBom_GetList(PM_M_Code.Text, Nothing, Nothing, Nothing, Nothing, True))
                    LoadBomSubToTable(mcBom.ProductBom_GetList(PM_M_Code.Text, Nothing, Nothing, Nothing, Nothing, Nothing))

                    If ds.Tables("Prod_Mounting_New").Rows.Count = 0 And ChcekPM_Type = False Then
                        MsgBox("�����J���~�t���Ʃβ��~��ưt�󥼫إߡI", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
                        OS_Remark.Focus()
                        Exit Sub
                    End If

                    Dim pcc As New ProductController
                    Dim pci As New ProductInfo
                    pci = pcc.Product_Get(PM_M_Code.Text)


                    If pci Is Nothing Then Exit Sub

                    Dim row As DataRow = ds.Tables("OrdersDetail").NewRow


                    row("OD_Name") = "�����r"
                    row("OD_Detail") = pci.PM_DiNaZi
                    OS_Remark.Text = pci.PM_Remark
                    ds.Tables("OrdersDetail").Rows.Add(row)

                    Dim row2 As DataRow = ds.Tables("OrdersDetail").NewRow


                    row2("OD_Name") = "���~�r"
                    row2("OD_Detail") = pci.PM_DiWaiZi
                    ds.Tables("OrdersDetail").Rows.Add(row2)

                End If
            Case "�t��妸"
                If Edit = False And tempCode <> "Copy" Then
                    ds.Tables("Prod_Mounting_New").Rows.Clear()
                    ds.Tables("OrdersDetail").Rows.Clear()
                End If

        End Select

    End Sub

    Private Sub OS_BatchID_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles OS_BatchID.LostFocus
        Dim os As New OrdersSubController
        Dim osi As OrdersSubInfo
        osi = os.OrdersSub_Get(OS_BatchID.Text)
        If osi Is Nothing Then
        Else
            MsgBox("�w�s�b���妸�s��,�Э��s��g�I")
            OS_BatchID.Text = ""
            OS_BatchID.Focus()
            Exit Sub
        End If
    End Sub

    Private Sub TreeView1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect
        On Error Resume Next
        If TreeView1.SelectedNode.Level = 3 Then

            Dim strTemp As String
            strTemp = TreeView1.SelectedNode.Tag
            TreeList1.FocusedNode.Item("OB_MakeDepartment") = strTemp
            PopupContainerControl1.OwnerEdit.ClosePopup()
        End If
    End Sub
    '�ɤJ�q����
    Private Sub popOrderDetailElectorPlate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popOrderDetailElectorPlate.Click
        Dim i As Integer
        'Dim n As Integer

        tempValue4 = PM_M_Code.EditValue
        tempValue5 = "�q��"
        Dim myfrm As New frmOrderDetailLoad
        myfrm.ShowDialog()

        Dim odc As New OrdersDetailController
        Dim odi As List(Of OrdersDetailInfo)

        odi = odc.OrdersDetail_GetList(tempValue, "�q��")
        If odi.Count = 0 Then
            Exit Sub
        Else
            For i = 0 To odi.Count - 1
                Dim row As DataRow = ds.Tables("OrdersDetail").NewRow

                row("OD_ID") = odi(i).OD_ID
                row("OD_Name") = odi(i).OD_Name
                row("OD_Detail") = odi(i).OD_Detail

                ds.Tables("OrdersDetail").Rows.Add(row)
            Next
        End If

        'If RefreshT = True Then
        '    Dim arr(n) As String
        '    arr = Split(tempValue, ",")
        '    n = Len(Replace(tempValue, ",", "," & "*")) - Len(tempValue)


        '    For i = 0 To n
        '        Dim odc As New OrdersDetailController
        '        Dim odi As New OrdersDetailInfo
        '        odi = odc.OrdersDetail_Get(arr(i))
        '        Dim row As DataRow = ds.Tables("OrdersDetail").NewRow

        '        row("OD_ID") = odi.OD_ID
        '        row("OD_Name") = odi.OD_Name
        '        row("OD_Detail") = odi.OD_Detail

        '        ds.Tables("OrdersDetail").Rows.Add(row)
        '    Next

        'End If
        tempValue = ""
        RefreshT = False

    End Sub
    '�ɤJ���i���
    Private Sub popOrderDetailPolished_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popOrderDetailPolished.Click
        Dim i As Integer
        'Dim n As Integer

        tempValue4 = PM_M_Code.EditValue
        tempValue5 = "���i"
        Dim myfrm As New frmOrderDetailLoad
        myfrm.ShowDialog()

        Dim odc As New OrdersDetailController
        Dim odi As List(Of OrdersDetailInfo)

        odi = odc.OrdersDetail_GetList(tempValue, "���i")
        If odi.Count = 0 Then
            Exit Sub
        Else
            For i = 0 To odi.Count - 1
                Dim row As DataRow = ds.Tables("OrdersDetail").NewRow

                row("OD_ID") = odi(i).OD_ID
                row("OD_Name") = odi(i).OD_Name
                row("OD_Detail") = odi(i).OD_Detail

                ds.Tables("OrdersDetail").Rows.Add(row)
            Next
        End If

        'If RefreshT = True Then
        '    Dim arr(n) As String
        '    arr = Split(tempValue, ",")
        '    n = Len(Replace(tempValue, ",", "," & "*")) - Len(tempValue)


        '    For i = 0 To n
        '        Dim odc As New OrdersDetailController
        '        Dim odi As New OrdersDetailInfo
        '        odi = odc.OrdersDetail_Get(arr(i))
        '        Dim row As DataRow = ds.Tables("OrdersDetail").NewRow

        '        row("OD_ID") = odi.OD_ID
        '        row("OD_Name") = odi.OD_Name
        '        row("OD_Detail") = odi.OD_Detail

        '        ds.Tables("OrdersDetail").Rows.Add(row)
        '    Next

        'End If
        tempValue = ""
        RefreshT = False
    End Sub
    ' �t��妸---�s�W���ƫH��
    Private Sub popMaterialAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popMaterialAdd.Click
        tempCode = ""
        tempValue6 = "���~�q��޲z"
        tempValue7 = Label19.Text
        tempValue8 = Label17.Text
        frmBOMSelect.XtraTabPage1.PageVisible = True
        frmBOMSelect.XtraTabPage2.PageVisible = True
        frmBOMSelect.XtraTabPage3.PageVisible = True
        frmBOMSelect.ShowDialog()

        Dim strBatchID, strLFID As String

        If frmBOMSelect.XtraTabControl1.SelectedTabPageIndex = 0 Then

            If tempCode = PM_M_Code.Text Then
                MsgBox("���i�H��ܥD���ƽs�X!", , "����")
                Exit Sub
            Else
                If CheckSelectBom(tempCode) = True Then
                    MsgBox("���t��w�[�J,���i�H���_,�Э��s���", , "ĵ�i")
                    Exit Sub
                Else
                    '   If PM_M_Code.Text = "SP" Then
                    AddRow2(tempCode)    '�w�ﴶ�q�d��
                    Dim mPB As New ProductBomController
                    LoadBomSubToTable(mPB.GetMaterialCodeSubList(tempCode))
                    '    Else
                    '    AddRow1(tempCode)   '�w��妸�A���~�d��
                    'End If

                End If

            End If


            '�W�[�O��
        ElseIf frmBOMSelect.XtraTabControl1.SelectedTabPageIndex = 1 Then

            strBatchID = tempValue7
            tempValue7 = ""
            Dim i, n As Integer
            Dim arr(n) As String
            arr = Split(strBatchID, ",")
            n = Len(Replace(strBatchID, ",", "," & "*")) - Len(strBatchID)

            For i = 0 To n
                If CheckSelectBom(arr(i)) = True Then
                    MsgBox("���t��w�[�J,���i�H���_,�Э��s���", , "ĵ�i")
                    Exit Sub
                Else
                    AddRow(arr(i), tempValue2, Nothing)
                End If
            Next
            '----------------------------------------------------------------------------------
        ElseIf frmBOMSelect.XtraTabControl1.SelectedTabPageIndex = 2 Then

            strLFID = tempValue8
            tempValue8 = ""
            Dim j, k As Integer
            Dim arr(k) As String
            arr = Split(strLFID, ",")
            k = Len(Replace(strLFID, ",", "," & "*")) - Len(strLFID)

            For j = 0 To k
                If CheckSelectBom(arr(j)) = True Then
                    MsgBox("���t��w�[�J,���i�H���_,�Э��s���", , "ĵ�i")
                    Exit Sub
                Else
                    AddRow(arr(j), Nothing, tempValue3)
                End If

            Next


        Else
            MsgBox("�d�ߦ����~�A�п�ܧ妸�β��~�s���d�ߡI")
            Exit Sub

        End If

        tempValue2 = ""
        tempValue3 = ""

    End Sub
    '�t��妸---�R�����ƫH��
    Private Sub popMaterialDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popMaterialDel.Click
        Dim strTemp As String
        strTemp = ""
        If Not TreeList1.FocusedNode Is Nothing Then
            If TreeList1.FocusedNode.Level > 0 Then
                MsgBox("�o���O�Ĥ@�h,�п�ܲĤ@�h�t��!", , "����")
                Exit Sub
            Else
                Dim psi As List(Of PurchaseMainInfo)
                Dim psc As New PurchaseMainController
                psi = psc.PurchaseSub_GetBatchList(Nothing, TreeList1.FocusedNode.Item("M_Code").ToString, OS_BatchID.Text)
                If psi.Count > 0 Then
                    MsgBox("�w�s�b���妸���ƪ����ʰO���A�����\�R��", , "����")
                    Exit Sub
                Else
                    '�b�R�����W�[�Q�R�����O��
                    Dim row As DataRow = ds.Tables("DelDataOrdersBomProduct").NewRow
                    'row("AutoID") = DelTemp
                    row("OS_BatchID") = OS_BatchID.Text
                    row("PM_Key") = TreeList1.FocusedNode.Item("PM_Key").ToString
                    strTemp = TreeList1.FocusedNode.Item("PM_Key").ToString
                    ds.Tables("DelDataOrdersBomProduct").Rows.Add(row)
                End If

            End If

            '  �ˬdDataSet���ۦP���O��, �}�R��
            For Each DR As DataRow In ds.Tables("Prod_Mounting_New").Select("PM_Key Like '" & strTemp & "%'")

                Dim obc As New OrdersBomController
                obc.OrdersBom_Del_1(OS_BatchID.Text, DR("PM_Key"))

                ds.Tables("Prod_Mounting_New").Rows.Remove(DR)

            Next
        End If

    End Sub
    Sub AddRow(ByVal M_Code As String, ByVal BatchID As String, ByVal PM_M_Code As String)

        If BatchID <> "" Then
            Dim osc As New OrdersSubController
            Dim osi As List(Of OrdersSubInfo)
            Dim pbc As New ProductBomController
            osi = osc.OrdersSub_GetList(Nothing, BatchID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

            'LoadBomSubToTable(pbc.ProductBom_GetList(osi(0).PM_M_Code, M_Code, Nothing, Nothing, Nothing, True))
            LoadBomSubToTable(pbc.ProductBom_GetList(osi(0).PM_M_Code, Nothing, M_Code, Nothing, Nothing, True))
            '-------------------------------------------------------------------------------------�H�W�q���~���ɥX�֦��l�t�󪺤l���ƫH��
            '------------------------------------------------------------------------------------�H�U�O�����q�妸���ɥX��e���ƪ��H��  10-07-02
            Dim obc As New Orders.OrdersBomController
            Dim tlist As List(Of Orders.OrdersBomInfo)
            tlist = obc.OrdersBom_GetList(M_Code, BatchID, Nothing, Nothing)
            If tlist Is Nothing Then Exit Sub
            Dim i As Integer
            Dim row As DataRow
            For i = 0 To tlist.Count - 1
                row = ds.Tables("Prod_Mounting_New").NewRow
                row("M_Code") = tlist(i).M_Code
                row("PM_Qty") = tlist(i).OB_Qty
                row("PM_MakeRemark") = tlist(i).OB_MakeRemark
                row("PM_Make") = tlist(i).OB_Make

                row("M_Supplier") = tlist(i).OB_Supplier
                row("M_SupplierNo") = tlist(i).OB_SupplierNo
                row("M_Name") = tlist(i).M_Name
                row("M_Gauge") = tlist(i).M_Gauge

                row("PM_Key") = tlist(i).OB_Key
                row("PM_PID") = tlist(i).OB_PID
                row("PM_LVL") = tlist(i).PM_LVL
                row("M_CodeType") = tlist(i).M_CodeType
                row("M_CodeMouldNO") = tlist(i).M_CodeMouldNO
                row("OB_MakeDepartment") = tlist(i).OB_MakeDepartment
                row("OB_MModeCusterNO") = tlist(i).OB_MModeCusterNO
                row("M_Weight") = tlist(i).M_Weight

                ds.Tables("Prod_Mounting_New").Rows.Add(row)
            Next
            TreeList1.ExpandAll()

        ElseIf PM_M_Code <> "" Then
            Dim pbc As New ProductBomController

            LoadBomSubToTable(pbc.ProductBom_GetList(PM_M_Code, M_Code, Nothing, Nothing, Nothing, True))
            LoadBomSubToTable(pbc.ProductBom_GetList(PM_M_Code, Nothing, M_Code, Nothing, Nothing, True))

        Else
            MsgBox("��Ƥ�����,�Э��s�ɤJ�I")
            Exit Sub
        End If

    End Sub
    Function CheckSelectBom(ByVal strCode) As Boolean
        '�ˬd��ܪ��l���ƬO�_�w�[�J
        Dim i As Long
        CheckSelectBom = False
        For i = 0 To ds.Tables("Prod_Mounting_New").Rows.Count - 1
            If ds.Tables("Prod_Mounting_New").Rows(i)("PM_LVL") = 0 Then
                If ds.Tables("Prod_Mounting_New").Rows(i)("M_Code") = strCode Then
                    MsgBox(ds.Tables("Prod_Mounting_New").Rows(i)("M_Code"))
                    CheckSelectBom = True
                    Exit Function
                Else
                    CheckSelectBom = False
                End If
            End If
        Next

    End Function
    Sub AddRow1(ByVal M_Code As String)

        Dim row As DataRow
        row = ds.Tables("Prod_Mounting_New").NewRow

        If M_Code = "" Then

        Else
            Dim pi As List(Of ProductBomInfo)
            Dim pc As New ProductBomController
            pi = pc.ProductBom_GetList(PM_M_Code.Text, M_Code, Nothing, Nothing, Nothing, Nothing)

            row("M_Code") = M_Code
            row("PM_Qty") = pi(0).PM_Qty
            row("PM_Make") = pi(0).PM_Make
            row("PM_Check") = pi(0).PM_Check
            row("PM_MakeRemark") = pi(0).PM_MakeRemark
            row("M_Supplier") = pi(0).M_Supplier
            row("M_SupplierNo") = pi(0).M_SupplierNo
            row("M_Name") = pi(0).M_Name
            row("M_Gauge") = pi(0).M_Gauge
            row("M_CodeType") = pi(0).M_CodeType
            row("M_CodeMouldNO") = pi(0).M_CodeMouldNO
            row("PM_Key") = pi(0).PM_Key
            row("PM_PID") = pi(0).PM_PID

            row("OB_MakeDepartment") = ""
            row("OB_MModeCusterNO") = ""
            row("M_Weight") = pi(0).M_Weight

            ds.Tables("Prod_Mounting_New").Rows.Add(row)

        End If
    End Sub
    Sub AddRow2(ByVal strCode As String)
        If strCode = "" Then
        Else

            Dim i As Integer

            For i = 0 To ds.Tables("Prod_Mounting_New").Rows.Count - 1
                If strCode = ds.Tables("Prod_Mounting_New").Rows(i)("M_Code") Then
                    MsgBox("�@�i�椣���\�����_���ƽs�X....")
                    Exit Sub
                End If
            Next
            Dim mc As New LFERP.Library.Material.MaterialController
            Dim objInfo As New LFERP.Library.Material.MaterialInfo
            objInfo = mc.MaterialCode_Get(strCode)

            Dim row As DataRow
            row = ds.Tables("Prod_Mounting_New").NewRow
            row("M_Code") = objInfo.M_Code
            row("PM_Qty") = 0
            row("PM_Make") = False
            row("PM_Check") = False
            row("PM_MakeRemark") = ""
            row("M_Supplier") = objInfo.M_Supplier
            row("M_SupplierNo") = objInfo.M_SupplierNo
            row("M_Name") = objInfo.M_Name
            row("M_Gauge") = objInfo.M_Gauge
            row("PM_LVL") = 0
            row("M_CodeType") = ""
            row("M_CodeMouldNO") = ""
            row("PM_Key") = objInfo.M_Code & "."
            row("PM_PID") = PM_M_Code.Text
            row("OB_MakeDepartment") = ""
            row("OB_MModeCusterNO") = ""

            ds.Tables("Prod_Mounting_New").Rows.Add(row)

        End If
    End Sub
    Sub EnableView()
        OS_BatchID.Enabled = False
        OS_Qty.Enabled = False
        PM_M_Code.Enabled = False
        OS_Sprace.Enabled = False
        OS_Type.Enabled = False
        OS_Fac.Enabled = False
        OS_CheckDate.Enabled = False
        OS_SendDate.Enabled = False
        OS_SendType.Enabled = False
        OS_Plate.Enabled = False
        popOrderTempDetailAdd.Visible = False
        popOrderTempDetailDel.Visible = False
        popOrderDetailAdd.Visible = False
        popOrderDetailDel.Visible = False
        popOrderDetailElectorPlate.Visible = False
        popOrderDetailPolished.Visible = False
        popMaterialAdd.Enabled = False
        popMaterialDel.Enabled = False
        gluM_Code.Enabled = False

        OS_SendNeed.Enabled = False
        OS_Remark.Enabled = False
        trere.OptionsColumn.ReadOnly = True
        TreeListColumn5.OptionsColumn.ReadOnly = True
        TreeListColumn6.OptionsColumn.ReadOnly = True
        TreeListColumn7.OptionsColumn.ReadOnly = True
        TreeListColumn9.OptionsColumn.ReadOnly = True
        TreeListColumn10.OptionsColumn.ReadOnly = True
        GridView1.OptionsBehavior.Editable = False
        GridView2.OptionsBehavior.Editable = False
        cboOS_ProductionWeek.Enabled = False
        Me.tieOS_ProductionWeek_Year.Enabled = False
    End Sub

    Private Sub CheckBox1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox1.Click
        If ChcekPM_Type = False Then
        Else
            Exit Sub
        End If

        Dim i, j As Integer
        For i = 0 To ds.Tables("Prod_Mounting_New").Rows.Count - 1
            If ds.Tables("Prod_Mounting_New").Rows(i)("PM_Check") = False Then
                j = j + 1
            End If
        Next
        If j = ds.Tables("Prod_Mounting_New").Rows.Count Then
            CheckBox1.Checked = False
            MsgBox("���妸�����Ƴ����Q�ҥΡI")
        End If
    End Sub

    '@ 2012/9/14 �K�[ ���Ů�����ܤU�ԦC��
    Private Sub PM_M_Code_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles PM_M_Code.KeyDown, OS_Fac.KeyDown, OS_Plate.KeyDown, OS_SendType.KeyDown
        If e.KeyCode = Keys.Space Then
            sender.ShowPopup()
        End If
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub


    ''�[�_��P�_ �����q��,�w�U�妸�Ҧ��ƶq 2013-5-21
    Function CheckCopyData() As Boolean
        CheckCopyData = True

        ''�d�ߥXOM_ID �q��y���� �P�w�Ͳ��`��

        Dim SumOM_OrderQty As Double = 0 '�q���`��
        Dim SumOS_Qty As Double = 0

        Dim objInfo As New List(Of OrdersSubInfo)
        Dim oc As New OrdersSubController

        objInfo = oc.OrdersSub_GetList1(StrOM_ID, Nothing, Nothing, Nothing, Nothing, Nothing,nothing)
        If objInfo.Count <= 0 Then
            '�S���ƾ�
            GoTo aa
        End If

        Dim i As Integer
        For i = 0 To objInfo.Count - 1
            SumOS_Qty = SumOS_Qty + objInfo(i).OS_Qty
        Next

aa:
        Dim ml As New List(Of OrdersMainInfo)
        Dim mc As New OrdersMainController

        ml = mc.OrdersMain_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, StrOM_ID)
        If ml.Count <= 0 Then
            '�S���ƾ�
            MsgBox("�L���q��s�����s�b!")
            CheckCopyData = False
            Exit Function
        End If

        SumOM_OrderQty = ml(0).OM_OrderQty


        'OS_Qty
        If SumOM_OrderQty - SumOS_Qty + IntOS_Qty_Edit >= CDbl(OS_Qty.Text) Then
        Else
            MsgBox("�Ͳ��ƶq����j�󥼥Ͳ��ƶq,���Ͳ��ƶq���G" & CStr(SumOM_OrderQty - SumOS_Qty + IntOS_Qty_Edit), 64, "����")

            'MsgBox(SumOM_OrderQty & "--" & SumOS_Qty & "--" & StrOM_ID)
            OS_Qty.Focus()
            CheckCopyData = False
            Exit Function
        End If

    End Function


    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged

    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged

    End Sub
End Class