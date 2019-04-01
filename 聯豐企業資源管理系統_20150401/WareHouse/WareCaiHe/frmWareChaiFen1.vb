Imports LFERP.Library.Material
Imports LFERP.Library.WareHouse.WareInventory
Imports LFERP.Library.WareHouse.WareHouseSplit
'Imports LFERP.Library.Purchase.Purchase
Imports LFERP.Library.Purchase.SharePurchase

Public Class frmWareChaiFen1

    Dim ds As New DataSet

    Private Sub frmWareChaiFen1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CreateTable()

    End Sub

    Sub CreateTable()

        ds.Tables.Clear()
        With ds.Tables.Add("CodeSub")

            .Columns.Add("M_SCode", GetType(String))   '�l���ƽs�X
            .Columns.Add("M_Name", GetType(String))    '�l���ƦW��
            .Columns.Add("S_Qty", GetType(Double))     '����Z�l���ƪ��ƶq
            .Columns.Add("Unit", GetType(String))     '�l���Ƴ��
            .Columns.Add("S_Ratio", GetType(Double))   '������

        End With

        GridControl1.DataSource = ds.Tables("CodeSub")

    End Sub
    Private Sub ButtonEdit1_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles ButtonEdit1.ButtonClick
        tempCode = ""
        tempValue5 = txtWH.Tag

        frmBOMSelect.ShowDialog()
        '���o�ݩ�����ƽs�X
        If tempCode = "" Then
        Else
            ButtonEdit1.Text = tempCode

            Dim mw As New WareInventoryMTController
            Dim mwiL As List(Of WareInventoryInfo)
            Dim mc As New MaterialController


            mwiL = mw.WareInventory_GetList3(ButtonEdit1.Text, txtWH.Tag, "True")
            If mwiL.Count = 0 Then
                Label6.Text = 0
                MsgBox("��e��������Ʀb��e�ܮw�w�s��0�A�����\����I", 60, "����")
                Exit Sub
            Else
                Label7.Visible = True
                Label7.Text = "���ƦW�١G" & mwiL(0).M_Name
                Label6.Text = mwiL(0).WI_Qty    'Ū����e�ܮw�����ƪ��w�s�H��
            End If

            '------------------------------------------------------  �l���ƪ��s�X�H�μƶq
            Dim mcl1 As List(Of MaterialInfo)

            mcl1 = mc.MaterialCodeSub_GetList1(ButtonEdit1.Text, Nothing)

            If mcl1.Count = 0 Then
                MsgBox("��e��ܪ����Ƥ��s�b�l����", 60, "����")
                Exit Sub
            Else
                Dim i As Integer

                ds.Tables("CodeSub").Clear()

                For i = 0 To mcl1.Count - 1

                    Dim row As DataRow

                    row = ds.Tables("CodeSub").NewRow

                    row("M_SCode") = mcl1(i).M_CodeSub
                    row("M_Name") = mcl1(i).M_Name
                    row("S_Qty") = 0        '�ܧ�Z���ƶq
                    row("Unit") = mcl1(i).M_Unit
                    row("S_Ratio") = mcl1(i).M_Qty

                    ds.Tables("CodeSub").Rows.Add(row)
                Next

            End If

        End If

    End Sub

    Function GetID() As String  '�ͦ��sID
        Dim swc As New WareHouseSplitControl
        Dim swi As List(Of WareHouseSplitInfo)
        Dim ndate As String
        ndate = "WS" + Format(Now(), "yyMM")
        swi = swc.WareHouseSplit_GetList(ndate, Nothing, Nothing, Nothing, Nothing, Nothing)
        If swi.Count <= 0 Then
            GetID = ndate + "0001"
        Else
            GetID = ndate + Mid((CInt(Mid(swi(0).S_ID, 7)) + 10001), 2)
        End If
    End Function

    '��ܩ�����ƩҦb���ܮw�H��
    Private Sub txtWH_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtWH.ButtonClick
        frmWareHouseSelect.SelectWareID = ""
        tempValue3 = "500701"
        frmWareHouseSelect.StartPosition = FormStartPosition.Manual
        frmWareHouseSelect.Left = MDIMain.Left + MDIMain.tvModule.Width + Me.Left + txtWH.Left + 16
        frmWareHouseSelect.Top = MDIMain.Top + Me.Top + 176
        frmWareHouseSelect.ShowDialog()
        If frmWareHouseSelect.SelectWareID = "" Then
        Else
            txtWH.Tag = frmWareHouseSelect.SelectWareID
            txtWH.Text = frmWareHouseSelect.SelectWareUpName & "-" & frmWareHouseSelect.SelectWareName
            txtWH.Enabled = False
        End If
    End Sub

    '�����e����H���ɡA�ٻݭn�P�_��e�ܮw�ƶq�O�_����
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        DataNew()

    End Sub

    Sub DataNew()   '�O�d����O��


        If txtWH.Text = "" Then
            MsgBox("�ާ@�ܮw�W�٤��ର�šA�п�J�ާ@�ܮw�W��!", 64, "����")
            txtWH.Focus()
            Exit Sub
        End If
        If ButtonEdit1.Text = "" Then
            MsgBox("�j���D���Ƥ��ର�šA�п�J�j���D����!", 64, "����")
            ButtonEdit1.Focus()
            Exit Sub
        End If
        If TextEdit2.Text = "" Then
            MsgBox("����ƶq���ର�šA�п�J����ƶq!", 64, "����")
            TextEdit2.Focus()
            Exit Sub
        End If

        Dim strqty As Double   'Ū����e�D���Ʃ���e���w�s��


        Dim mw2 As New WareInventoryMTController
        Dim mwiL2 As List(Of WareInventoryInfo)
        mwiL2 = mw2.WareInventory_GetList3(ButtonEdit1.Text, txtWH.Tag, "True")

        If mwiL2.Count = 0 Then
            strqty = 0
        Else
            strqty = mwiL2(0).WI_Qty
        End If

        If CDbl(TextEdit2.Text.Trim) > strqty Then
            MsgBox("��e����ƶq�j��e�w�s�`��,�����\���!", 60, "����")
            Exit Sub
        End If

        Dim i As Integer

        Dim swi As New WareHouseSplitInfo
        Dim swc As New WareHouseSplitControl

        swi.S_ID = GetID()
        swi.WH_ID = txtWH.Tag
        swi.M_LCode = ButtonEdit1.Text
        swi.S_Type = "���"
        swi.S_Qty = TextEdit2.Text

        swi.S_AddDate = Now
        swi.S_Action = InUserID
        swi.S_Remark = txtS_Remark.Text.Trim

        For i = 0 To ds.Tables("CodeSub").Rows.Count - 1

            swi.M_SCode = ds.Tables("CodeSub").Rows(i)("M_SCode") '�l���ƽs�X
            swi.S_Ratio = ds.Tables("CodeSub").Rows(i)("S_Ratio") '��C

            swi.WI_LQty = strqty - CDbl(TextEdit2.Text) '�D���Ʒ�e���l��

            Dim Qty As Double

            Dim wi As LFERP.Library.WareHouse.WareInventory.WareInventoryInfo
            Dim wc As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController
            wi = wc.WareInventory_GetSub(ds.Tables("CodeSub").Rows(i)("M_SCode"), txtWH.Tag)

            If wi Is Nothing Then
                Qty = 0
            Else
                Qty = wi.WI_Qty
            End If

            swi.WI_SQty = Qty + CDbl(ds.Tables("CodeSub").Rows(i)("S_Qty")) '�l���Ʒ�e���l��

            If swc.WareHouseSplit_Add(swi) = True Then

                Dim mt As New SharePurchaseController
                Dim mm As New SharePurchaseInfo

                Dim mm1 As New SharePurchaseInfo
                '�ܧ�D���ƪ��ܮw�w�s


                mm.WH_ID = txtWH.Tag
                mm.M_Code = ButtonEdit1.Text
                mm.WI_Qty = strqty - CDbl(TextEdit2.Text)

                mt.UpdateWareInventory_WIQty2(mm)

                '�ܧ�l���ƪ��ܮw�w�s

                mm1.WH_ID = txtWH.Tag
                mm1.M_Code = ds.Tables("CodeSub").Rows(i)("M_SCode")
                mm1.WI_Qty = Qty + CDbl(ds.Tables("CodeSub").Rows(i)("S_Qty")) '�l���Ʒ�e���l��

                mt.UpdateWareInventory_WIQty2(mm1)

            End If
        Next
        MsgBox("������\�I", 60, "����")
        Me.Close()
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub TextEdit2_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextEdit2.EditValueChanged
        If ButtonEdit1.Text = "" Then
            MsgBox("�D���ƽs�X���ର�šI", 60, "����")
            Exit Sub
        End If

        Dim i As Integer

        For i = 0 To ds.Tables("CodeSub").Rows.Count - 1

            ds.Tables("CodeSub").Rows(i)("S_Qty") = Val(ds.Tables("CodeSub").Rows(i)("S_Ratio")) * Val(TextEdit2.Text)

        Next
    End Sub

    Private Sub TextEdit2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextEdit2.KeyDown

        If e.KeyCode = Keys.Enter Then
            If ButtonEdit1.Text = "" Then
                MsgBox("�D���ƽs�X���ର�šI", 60, "����")
                Exit Sub
            End If

            Dim i As Integer

            For i = 0 To ds.Tables("CodeSub").Rows.Count - 1

                ds.Tables("CodeSub").Rows(i)("S_Qty") = Val(ds.Tables("CodeSub").Rows(i)("S_Ratio")) * Val(TextEdit2.Text)

            Next

        End If

    End Sub
End Class