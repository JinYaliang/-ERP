Imports LFERP.Library.Orders
Imports LFERP.Library.Product
Public Class frmOrdersSubPJ
    '    Dim ds As New DataSet
    '    Dim intTempID As Integer
    '    Private Sub frmOrdersSubPJ_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    '        CreateTable()
    '        LoadOS_BatchID()
    '    End Sub


    '    Sub LoadOS_BatchID()
    '        Dim osc As New OrdersSubController
    '        OS_BatchID.Properties.DisplayMember = "OS_BatchID"
    '        OS_BatchID.Properties.ValueMember = "OS_BatchID"
    '        OS_BatchID.Properties.DataSource = osc.OrdersSub_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

    '    End Sub

    '    Private Sub OS_BatchID_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OS_BatchID.EditValueChanged
    '        On Error Resume Next
    '        Dim osc As New OrdersSubController
    '        Dim osi As New OrdersSubInfo
    '        If OS_BatchID.Text <> "" Then
    '            osi = osc.OrdersSub_Get(OS_BatchID.Text)
    '            txPM_M_Code.Text = osi.PM_M_Code
    '        End If
    '        '���J���~��ưt��
    '        Dim mcBom As New ProductBomController
    '        ds.Tables("Prod_Mounting_New").Rows.Clear()
    '        LoadBomSubToTable(mcBom.ProductBom_GetList(txPM_M_Code.Text, Nothing, Nothing, Nothing, Nothing))
    '        '���J���ƲM��
    '        LoadDataOrdersBom()
    '    End Sub

    '    Sub CreateTable()

    '        '�Ыؼƾڪ�_Prod_Mounting_New(���~���)
    '        With ds.Tables.Add("Prod_Mounting_New")
    '            .Columns.Add("PM_M_Code", GetType(String)) '�p�׽s��
    '            .Columns.Add("PM_ID", GetType(Integer))
    '            .Columns.Add("M_Code", GetType(String))
    '            .Columns.Add("M_CodePID", GetType(String))
    '            .Columns.Add("PM_Qty", GetType(Integer))
    '            .Columns.Add("PM_MakeRemark", GetType(String))
    '            .Columns.Add("PM_Make", GetType(Boolean))
    '            .Columns.Add("PM_Check", GetType(Boolean))
    '            .Columns.Add("M_Supplier", GetType(String))
    '            .Columns.Add("M_SupplierNo", GetType(String))
    '            .Columns.Add("IsNew", GetType(Boolean))    '�O�_�s�W

    '            .Columns.Add("M_Name", GetType(String))
    '            .Columns.Add("M_Gauge", GetType(String))
    '            .Columns.Add("M_Unit", GetType(String))
    '            .Columns.Add("PM_LVL", GetType(Integer))
    '            .Columns.Add("PM_Key", GetType(String))
    '            .Columns.Add("PM_PID", GetType(String))
    '        End With

    '        TreeList1.DataSource = ds.Tables("Prod_Mounting_New")


    '        '�Ыؼƾڪ�_OrdersBom(���ƲM��)
    '        With ds.Tables.Add("OrdersBom")
    '            .Columns.Add("PM_M_Code", GetType(String)) '�p�׽s��
    '            .Columns.Add("PM_ID", GetType(Integer))
    '            .Columns.Add("M_Code", GetType(String))
    '            .Columns.Add("M_CodePID", GetType(String))
    '            .Columns.Add("PM_Qty", GetType(Integer))
    '            .Columns.Add("PM_MakeRemark", GetType(String))
    '            .Columns.Add("PM_Make", GetType(Boolean))
    '            .Columns.Add("PM_Check", GetType(Boolean))
    '            .Columns.Add("M_Supplier", GetType(String))
    '            .Columns.Add("M_SupplierNo", GetType(String))
    '            .Columns.Add("IsNew", GetType(Boolean))
    '            .Columns.Add("ZT", GetType(String))

    '            .Columns.Add("M_Name", GetType(String))
    '            .Columns.Add("M_Gauge", GetType(String))
    '            .Columns.Add("M_Unit", GetType(String))
    '            .Columns.Add("PM_LVL", GetType(Integer))
    '            .Columns.Add("PM_Key", GetType(String))
    '            .Columns.Add("PM_PID", GetType(String))

    '            .Columns.Add("OB_Product", GetType(String))
    '            .Columns.Add("OB_ProductNo", GetType(String))
    '        End With
    '        TreeList2.DataSource = ds.Tables("OrdersBom")
    '        DataGridView1.DataSource = ds.Tables("OrdersBom")
    '    End Sub

    '    Sub LoadBomSubToTable(ByVal tList As List(Of ProductBomInfo))

    '        '�S���l���ƫh�h�X
    '        If tList Is Nothing Then Exit Sub

    '        On Error Resume Next
    '        Dim i As Integer
    '        Dim row As DataRow
    '        For i = 0 To tList.Count - 1
    '            row = ds.Tables("Prod_Mounting_New").NewRow
    '            row("PM_M_Code") = tList(i).PM_M_Code
    '            row("PM_ID") = intTempID
    '            row("M_Code") = tList(i).M_Code
    '            row("M_CodePID") = tList(i).M_CodePID
    '            row("PM_Qty") = tList(i).PM_Qty
    '            row("PM_MakeRemark") = tList(i).PM_MakeRemark
    '            row("PM_Make") = tList(i).PM_Make
    '            row("PM_Check") = tList(i).PM_Check
    '            row("M_Supplier") = tList(i).M_Supplier
    '            row("M_SupplierNo") = tList(i).M_SupplierNo
    '            row("IsNew") = True
    '            row("M_Name") = tList(i).M_Name
    '            row("M_Gauge") = tList(i).M_Gauge
    '            row("M_Unit") = tList(i).M_Unit
    '            row("PM_LVL") = tList(i).PM_LVL
    '            row("PM_Key") = tList(i).PM_Key
    '            row("PM_PID") = tList(i).PM_PID
    '            ds.Tables("Prod_Mounting_New").Rows.Add(row)
    '        Next

    '        TreeList1.ExpandAll()





    '    End Sub

    '    Sub LoadDataOrdersBom()
    '        '���J���ƲM��

    '        Dim obc As New OrdersBomController
    '        Dim tlist As List(Of OrdersBomInfo)
    '        ds.Tables("OrdersBom").Rows.Clear()

    '        tlist = obc.OrdersBom_GetList(OS_BatchID.Text)
    '        If tList Is Nothing Then Exit Sub

    '        On Error Resume Next
    '        Dim i As Integer
    '        Dim row As DataRow
    '        For i = 0 To tList.Count - 1
    '            row = ds.Tables("OrdersBom").NewRow

    '            row("PM_ID") = intTempID
    '            row("M_Code") = tList(i).M_Code
    '            row("PM_Qty") = tlist(i).OB_Qty
    '            row("PM_MakeRemark") = tlist(i).OB_MakeRemark
    '            row("PM_Make") = tlist(i).OB_Make

    '            row("M_Supplier") = tlist(i).OB_Supplier
    '            row("M_SupplierNo") = tlist(i).OB_SupplierNo
    '            '  row("IsNew") = False
    '            row("M_Name") = tlist(i).M_Name
    '            row("M_Gauge") = tlist(i).M_Gauge
    '            '    row("M_Unit") = tList(i).M_Unit
    '            '  row("PM_LVL") = tList(i).PM_LVL
    '            row("PM_Key") = tlist(i).OB_Key
    '            row("PM_PID") = tlist(i).OB_PID
    '            row("OB_Product") = tlist(i).OB_Product
    '            row("OB_ProductNo") = tlist(i).OB_ProductNo
    '            row("IsNew") = True
    '            row("ZT") = ""
    '            ds.Tables("OrdersBom").Rows.Add(row)
    '        Next

    '        TreeList2.ExpandAll()





    '        If ds.Tables("OrdersBom").Rows.Count = 0 Then
    '            MsgBox("�����J���~�t���Ʃβ��~��ưt�󥼫إߡI", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)

    '        End If


    '    End Sub


    '    Private Sub SimpleButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton2.Click
    '        Dim i As Integer

    '        If ds.Tables("Prod_Mounting_New").Rows.Count = 0 Then Exit Sub
    '        For i = 0 To ds.Tables("Prod_Mounting_New").Rows.Count - 1
    '            If ds.Tables("Prod_Mounting_New").Rows(i)("IsNew") = False Then GoTo A

    '            '�ˬdDataSet"ORdersBom"���O�_���P�˽s�X���O��,�Y�L�h�[�J��"OrdersBom"

    '            Dim strA As String
    '            Dim IntA As Integer = 0
    '            strA = ds.Tables("Prod_Mounting_New").Rows(i)("M_Code")

    '            For Each DR As DataRow In ds.Tables("OrdersBom").Select("M_Code = '" & strA & "'")
    '                IntA = IntA + 1
    '            Next
    '            If IntA < 1 Then
    '                SimpleButton2.Enabled = False
    '                Dim row2 As DataRow
    '                row2 = ds.Tables("OrdersBom").NewRow


    '                '       row2("PM_M_Code") = ds.Tables("Prod_Mounting_New").Rows(i)("PM_M_Code")
    '                row2("PM_ID") = ds.Tables("Prod_Mounting_New").Rows(i)("PM_ID")
    '                row2("M_Code") = ds.Tables("Prod_Mounting_New").Rows(i)("M_Code")
    '                row2("M_CodePID") = ds.Tables("Prod_Mounting_New").Rows(i)("M_CodePID")
    '                row2("PM_Qty") = ds.Tables("Prod_Mounting_New").Rows(i)("PM_Qty")
    '                row2("PM_MakeRemark") = ds.Tables("Prod_Mounting_New").Rows(i)("PM_MakeRemark")
    '                row2("PM_Make") = ds.Tables("Prod_Mounting_New").Rows(i)("PM_Check")
    '                row2("PM_Check") = ds.Tables("Prod_Mounting_New").Rows(i)("PM_Check")
    '                row2("M_Supplier") = ds.Tables("Prod_Mounting_New").Rows(i)("M_Supplier")
    '                row2("M_SupplierNo") = ds.Tables("Prod_Mounting_New").Rows(i)("M_SupplierNo")
    '                row2("IsNew") = ds.Tables("Prod_Mounting_New").Rows(i)("IsNew")
    '                row2("M_Name") = ds.Tables("Prod_Mounting_New").Rows(i)("M_Name")
    '                row2("M_Gauge") = ds.Tables("Prod_Mounting_New").Rows(i)("M_Gauge")
    '                row2("M_Unit") = ds.Tables("Prod_Mounting_New").Rows(i)("M_Unit")
    '                row2("PM_LVL") = ds.Tables("Prod_Mounting_New").Rows(i)("PM_LVL")
    '                row2("PM_Key") = ds.Tables("Prod_Mounting_New").Rows(i)("PM_Key")
    '                row2("PM_PID") = ds.Tables("Prod_Mounting_New").Rows(i)("PM_PID")
    '                row2("ZT") = "�s�W"

    '                ds.Tables("OrdersBom").Rows.Add(row2)
    '            End If

    'A:      Next
    '    End Sub

    '    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    '        Dim row2 As DataRow
    '        row2 = ds.Tables("OrdersBom").NewRow


    '        row2("PM_M_Code") = "PMC"
    '        row2("PM_ID") = 100
    '        row2("M_Code") = "MC"
    '        row2("M_CodePID") = "MC"
    '        row2("PM_Qty") = 11
    '        row2("PM_MakeRemark") = "dsfsF"
    '        row2("PM_Make") = True
    '        row2("PM_Check") = True
    '        row2("M_Supplier") = "aaa"
    '        row2("M_SupplierNo") = "bbb"
    '        row2("IsNew") = True
    '        row2("M_Name") = "sdfsfs"
    '        row2("M_Gauge") = "dfsgfd"
    '        row2("M_Unit") = "dsfsds"
    '        row2("PM_LVL") = 0
    '        row2("PM_Key") = "dfgd"
    '        row2("PM_PID") = "dfgd"
    '        row2("OB_Product") = "�ݱ���"

    '        ds.Tables("OrdersBom").Rows.Add(row2)
    '    End Sub



    '    Private Sub SimpleButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton3.Click
    '        Dim obc As New OrdersBomController
    '        Dim obi As New OrdersBomInfo
    '        obi.OS_BatchID = OS_BatchID.Text
    '        obc.OrdersBom_Del(obi)
    '    End Sub

    '    Private Sub SimpleButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton4.Click
    '        '�ˬd�t��,��sds.Tables("OrdersBom").Rows(i)("ZT") �����A
    '        Dim i As Integer
    '        i = 0
    '        For i = 0 To ds.Tables("OrdersBom").Rows.Count - 1
    '            '�Y���~��Ʀ��A���ƽs�X�S���A�h=���s�W",�äU�@���O��
    '            If ds.Tables("OrdersBom").Rows(i)("ZT") = "�s�W" Then GoTo A

    '            '�YZT="" ,�h���}�P�_,�H��sZT���A
    '            If ds.Tables("OrdersBom").Rows(i)("ZT") = "" Then
    '                '�p���t��O�S�ĩΤw�g�o�ͱ���,�h ZT="����",�äU�@���O��
    '                If ds.Tables("OrdersBom").Rows(i)("OB_Product") = "�S��" Or IsDBNull(ds.Tables("OrdersBom").Rows(i)("OB_ProductNo")) = False Then
    '                    ds.Tables("OrdersBom").Rows(i)("ZT") = "����"
    '                    GoTo A
    '                End If

    '                '�Y���t��O���ƽs�X�����~��ƵL,�h ZT="�R��",�äU�@���O��
    '                Dim strA As String
    '                Dim IntA As Integer = 0
    '                strA = ds.Tables("OrdersBom").Rows(i)("M_Code")
    '                For Each DR As DataRow In ds.Tables("Prod_Mounting_New").Select("M_Code = '" & strA & "'")
    '                    IntA = IntA + 1
    '                Next
    '                If IntA < 1 Then
    '                    ds.Tables("OrdersBom").Rows(i)("ZT") = "�R��"
    '                    GoTo A
    '                End If

    '                '�Y���t��[�u�n�D,�A�[�u,�ζq�ܧ󪺱��p�U�AZT='�ܧ�", �íp��s���`�ζq,�äU�@���O��
    '                Dim strB As String
    '                strB = ds.Tables("OrdersBom").Rows(i)("M_Code")
    '                Dim IntB As Integer = 0


    '                If IntB > 0 Then
    '                    ds.Tables("OrdersBom").Rows(i)("ZT") = "�R��"
    '                    GoTo A
    '                End If
    '            End If
    'A:      Next
    '    End Sub

    Private Sub frmOrdersSubPJ_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class