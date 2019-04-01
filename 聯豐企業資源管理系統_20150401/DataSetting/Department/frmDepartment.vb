Imports LFERP.DataSetting

Public Class frmDepartment

    Dim dc As New DepartmentControler


    Private Sub frmDepartment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        TreeList1.DataSource = dc.Department_GetList(Nothing, Nothing, Nothing)
        TreeList1.ExpandAll()
        Grid1.DataSource = dc.BriName_GetList(Nothing, Nothing, Nothing)

    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        Edit = False
        tempValue2 = TreeList1.FocusedNode.Item("DPT_ID").ToString()
        Dim l As Integer
        l = tempValue2.Length
        If l = 8 Then
            MsgBox("�w���̩��h�C�����\�A�s�W�l���O�I")
            Exit Sub
        Else
            Dim MyFrm As New frmDepartment_Add
            MyFrm.ShowDialog()

            If tempValue = "RE" Then
                TreeList1.DataSource = dc.Department_GetList(Nothing, Nothing, Nothing)
                TreeList1.ExpandAll()
                tempValue = ""
            End If
        End If

    End Sub

    Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
        Edit = True
        tempValue3 = TreeList1.FocusedNode.Item("DPT_PID").ToString
        frmDepartment_Add.TextEdit1.Text = TreeList1.FocusedNode.Item("DPT_ID").ToString
        frmDepartment_Add.TextEdit2.Text = TreeList1.FocusedNode.Item("DPT_Name").ToString
        frmDepartment_Add.TextEdit3.Text = TreeList1.FocusedNode.Item("DPT_PID").ToString
        frmDepartment_Add.ShowDialog()

        If tempValue = "RE" Then
            TreeList1.DataSource = dc.Department_GetList(Nothing, Nothing, Nothing)
            TreeList1.ExpandAll()
            tempValue = ""
        End If

    End Sub

    Private Sub cmdDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDel.Click
        If MsgBox("�T�w�n�R���W�٬��G '" & TreeList1.FocusedNode.Item("DPT_Name").ToString & "' ���O����?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            If dc.Department_Delete(TreeList1.FocusedNode.Item("DPT_ID").ToString) = True Then
                MsgBox("�w�R��!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
                TreeList1.DataSource = dc.Department_GetList(Nothing, Nothing, Nothing)
                TreeList1.ExpandAll()
            Else
                MsgBox("�R������!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
            End If
        End If
    End Sub

    '�ק�Ͳ������s��   --�Ȯɤ����Ѧ����\��׳]�p�쪺�H���H���ܰʼv�T���j
    Private Sub cmsEditBriID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsEditBriID.Click
        Edit = True
        tempValue = GridView1.GetFocusedRowCellValue("DepID").ToString
        tempValue2 = "ID"
        tempValue3 = GridView1.GetFocusedRowCellValue("DepName").ToString
        Dim fr As New frmBriName
        fr.ShowDialog()

    End Sub
    '�ק�Ͳ������W��
    Private Sub cmsEditBriName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsEditBriName.Click
        Edit = True
        tempValue = GridView1.GetFocusedRowCellValue("DepID").ToString
        tempValue2 = "Name"
        tempValue3 = GridView1.GetFocusedRowCellValue("DepName").ToString
        Dim fr As New frmBriName
        fr.ShowDialog()
        Grid1.DataSource = dc.BriName_GetList(Nothing, Nothing, Nothing)
    End Sub
    '�s�W�����H��
    Private Sub cmsAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsAdd.Click
        Edit = False

        Dim fr As New frmBriName
        fr.ShowDialog()
        Grid1.DataSource = dc.BriName_GetList(Nothing, Nothing, Nothing)

    End Sub
End Class