Imports LFERP.Library.KnifeWare
Public Class frmTheTimeNoReturn

    Private Sub frmTheTimeNoReturn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim gpatc As New GetPerAndTeamControllerKnife
        gridTeam.Properties.DataSource = gpatc.GetTeam(Nothing)
        gridTeam.Properties.DisplayMember = "G_Name"
        gridTeam.Properties.ValueMember = "G_NO"
    End Sub

    Private Sub okButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles okButton.Click
        If RadPerNO.Checked = True Then
            If txtPerNO.Text.Trim = "" Then
                MsgBox("�п�J�u��", 64, "����")
                txtPerNO.Text = ""
                txtPerNO.Focus()
                Exit Sub
            End If
        End If
        If RadTeam.Checked = True Then
            If gridTeam.EditValue = Nothing Then
                MsgBox("�п�ܲէO", 64, "����")
                Exit Sub
            End If
        End If
        If RadMore.Checked = True Then
            If txtMore.Text.Trim = "" Then
                MsgBox("�п�J�h�Ӥu���A�åB�u�������H�I���Ϥ�", 64, "����")
                txtMore.Text = ""
                txtMore.Focus()
                Exit Sub
            End If
        End If
        If txtTheDay.Text.Trim = "" Then
            MsgBox("�п�J�O�����٤Ѽ�", 64, "����")
            txtTheDay.Text = ""
            txtTheDay.Focus()
            Exit Sub
        End If

        Dim timeSpan As New TimeSpan(txtTheDay.Text, 0, 0, 0)
        Dim newDate As New DateTime
        newDate = Date.Today.Date.Subtract(timeSpan)

        If RadPerNO.Checked = True Then
            Dim ttnrc As New TheTimeNoReturnControllerKnife
            Dim ds As New DataSet
            Dim ltc As New CollectionToDataSet
            Dim ttnrinfo As New List(Of TheTimeNoReturnInfoKnife)
            ttnrinfo = ttnrc.GetTheTimeNoReturn(txtPerNO.Text, newDate)
            If ttnrinfo.Count > 0 Then
                ltc.CollToDataSet(ds, "TheTimeNoReturn", ttnrinfo)
                PreviewRPT(ds, "20rptTheTimeNoReturn", "�O�������M���", True, True)
            Else
                MsgBox("�S���ŦX���󪺼ƾ�", 64, "����")
                Exit Sub
            End If
            ltc = Nothing
        End If

        If RadAll.Checked = True Then
            Dim ttnrc As New TheTimeNoReturnControllerKnife
            Dim ds As New DataSet
            Dim ltc As New CollectionToDataSet
            Dim ttnrinfo As New List(Of TheTimeNoReturnInfoKnife)
            ttnrinfo = ttnrc.GetTheTimeNoReturn(Nothing, newDate)
            If ttnrinfo.Count > 0 Then
                ltc.CollToDataSet(ds, "TheTimeNoReturn", ttnrinfo)
                PreviewRPT(ds, "20rptTheTimeNoReturn", "�O�������M���", True, True)
            Else
                MsgBox("�S���ŦX���󪺼ƾ�", 64, "����")
                Exit Sub
            End If
            ltc = Nothing
        End If

        If RadTeam.Checked = True Then
            Dim gp As New GetPerAndTeamControllerKnife
            Dim gpinfo As New List(Of GetPerAndTeamInfoKnife)
            gpinfo = gp.GetPerByTeam(gridTeam.EditValue, Nothing)
            Dim i As Integer
            Dim j As Integer
            Dim ttnrListSub As New List(Of TheTimeNoReturnInfoKnife)
            Dim ttnrListMain As New List(Of TheTimeNoReturnInfoKnife)
            Dim ttnrc As New TheTimeNoReturnControllerKnife
            For i = 0 To gpinfo.Count - 1
                ttnrListSub = ttnrc.GetTheTimeNoReturn(gpinfo(i).Per_ID, newDate)
                For j = 0 To ttnrListSub.Count - 1
                    ttnrListMain.Add(ttnrListSub(i))
                Next
            Next
            Dim ds As New DataSet
            Dim ltc As New CollectionToDataSet
            If ttnrListMain.Count > 0 Then
                ltc.CollToDataSet(ds, "TheTimeNoReturn", ttnrListMain)
                PreviewRPT(ds, "20rptTheTimeNoReturn", "�O�������M���", True, True)
            Else
                MsgBox("�S���ŦX���󪺼ƾ�", 64, "����")
                Exit Sub
            End If
            ltc = Nothing
        End If
        If RadMore.Checked = True Then
            Dim perNOStr As String = txtMore.Text
            Dim perNO() As String
            perNO = perNOStr.Split(".")
            perNO = Array_unique(perNO)    ''''�h��perNO�������Ƥ���
            Dim i As Integer
            Dim j As Integer
            Dim ttnrListSub As New List(Of TheTimeNoReturnInfoKnife)
            Dim ttnrListMain As New List(Of TheTimeNoReturnInfoKnife)
            Dim ttnrc As New TheTimeNoReturnControllerKnife
            For i = 0 To perNO.Length - 1
                ttnrListSub = ttnrc.GetTheTimeNoReturn(perNO(i), newDate)
                For j = 0 To ttnrListSub.Count - 1
                    ttnrListMain.Add(ttnrListSub(j))
                Next
            Next
            Dim ds As New DataSet
            Dim ltc As New CollectionToDataSet
            If ttnrListMain.Count > 0 Then
                ltc.CollToDataSet(ds, "TheTimeNoReturn", ttnrListMain)
                PreviewRPT(ds, "20rptTheTimeNoReturn", "�O�������M���", True, True)
            Else
                MsgBox("�S���ŦX���󪺼ƾ�", 64, "����")
                Exit Sub
            End If
            ltc = Nothing
        End If
        Me.Close()
    End Sub

    Private Sub canButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles canButton.Click
        Me.Close()
    End Sub

    ''' <summary>
    ''' �h���Ʋդ������Ƥ���1
    ''' </summary>
    ''' <param name="Arr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Array_unique(ByVal Arr() As String)
        Arr = QuickSort(Arr)
        Dim k As Integer, i As Integer
        For i = 0 To UBound(Arr)
            If Arr(k) <> Arr(i) Then
                Arr(k + 1) = Arr(i)
                k = k + 1
            End If
        Next
        Dim NewArr() As String
        ReDim NewArr(k)
        For i = 0 To k
            NewArr(i) = Arr(i)
        Next
        Array_unique = NewArr
    End Function

    ''' <summary>
    ''' �h���Ʋդ������Ƥ���2
    ''' </summary>
    ''' <param name="Arr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function QuickSort(ByVal Arr() As String)
        Dim i, j
        Dim bound, t
        bound = UBound(Arr)
        For i = 0 To bound - 1
            For j = i + 1 To bound
                If Arr(i) > Arr(j) Then
                    t = Arr(i)
                    Arr(i) = Arr(j)
                    Arr(j) = t
                End If
            Next
        Next
        QuickSort = Arr
    End Function
End Class