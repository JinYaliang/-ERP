Imports LFERP.DataSetting

Public Class frmDepartmentSelect
    Dim dpm As New DepartmentControler
    Public DptID, DptName, UpDptName As String
    Public FacID, FacName As String
    Public blnSelectParentNode As Boolean  '�O���O�_���\��ܤ��`�I

    Private Sub frmDepartmentSelect_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dpm.GetDepartmentModule(TV)
        Label2.Text = tempValue

        TV.Nodes(0).Expand()

        If strInCompany = "1001" Then
            TV.Nodes(0).Nodes(0).ExpandAll()
        ElseIf strInCompany = "1002" Then
            TV.Nodes(0).Nodes(1).ExpandAll()
        Else
            TV.Nodes(0).Nodes(2).ExpandAll()

        End If

        TV.Focus()
        '  TV.ExpandAll()   
        Me.Width = 213
        Me.Height = 245
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If Label2.Text = "�X�w���`-����" Then
            If TV.SelectedNode.Level = 3 Then
                DptID = TV.SelectedNode.Tag
                DptName = TV.SelectedNode.Text
            End If
            Me.Close()
        ElseIf Label2.Text = "�X�w���`-�t�O" Then
            If TV.SelectedNode.Level = 2 Then
                FacID = TV.SelectedNode.Tag
                FacName = TV.SelectedNode.Text
                Me.Close()
            Else
                MsgBox("�Х��T��ܡI", 64, "����")
                Exit Sub
            End If
        Else
            If TV.Focus = True Then
                If TV.SelectedNode.Level = 3 Then
                    DptID = TV.SelectedNode.Tag
                    DptName = TV.SelectedNode.Text
                    UpDptName = TV.SelectedNode.Parent.Text
                    Me.Close()
                Else
                    If TV.SelectedNode.Level = 2 And blnSelectParentNode = True Then
                        DptID = TV.SelectedNode.Tag
                        DptName = TV.SelectedNode.Text
                        UpDptName = TV.SelectedNode.Parent.Text
                        blnSelectParentNode = False
                        Me.Close()
                        Exit Sub
                    End If
                    DptID = ""
                    DptName = ""
                    UpDptName = ""
                    MsgBox("�Х��T��ܳ���!", 64, "����")
                End If
            Else
                MsgBox("�п�ܳ���!", 64, "����")
            End If
        End If
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        DptID = ""
        DptName = ""
        UpDptName = ""
        Me.Close()
    End Sub

    Private Sub TV_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TV.DoubleClick
        If Label2.Text = "�X�w���`-����" Then
            If TV.SelectedNode.Level = 3 Then
                DptID = TV.SelectedNode.Tag
                DptName = TV.SelectedNode.Text
            End If
            Me.Close()
        ElseIf Label2.Text = "�X�w���`-�t�O" Then
            If TV.SelectedNode.Level = 2 Then
                FacID = TV.SelectedNode.Tag
                FacName = TV.SelectedNode.Text
                Me.Close()
            End If
        Else
            If TV.SelectedNode.Level = 3 Then
                DptID = TV.SelectedNode.Tag
                DptName = TV.SelectedNode.Text
                UpDptName = TV.SelectedNode.Parent.Text
                Me.Close()
            End If
        End If
    End Sub
End Class