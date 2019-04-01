Imports LFERP.DataSetting

Public Class frmProductionPieceWorkGroupFind
    Dim fc As New FacControler
    Public isClickbtnFind As Boolean        '�P�_�O�_�����F���d�ߡ����s

    Private Sub frmProductionPieceWorkGroupFind_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        isClickbtnFind = False

        '�[���t�O�W��
        lueFacID.Properties.DisplayMember = "FacName"
        lueFacID.Properties.ValueMember = "FacID"
        lueFacID.Properties.DataSource = fc.GetFacList(strInFacID, Nothing)

        ''--------------------�d�߮ɥΤ��v��------------------------
        If strInUserRank = "�Ͳ���" Then
            lueFacID.EditValue = strInFacIDFull
        ElseIf strInUserRank = "�޲z" Then
        ElseIf strInUserRank = "�έp" Then
            lueFacID.EditValue = strInFacIDFull
            lueDepID.EditValue = strInDepIDFull
        End If

    End Sub
    ''' <summary>
    ''' '�t�O�W�٭ȧ��ܮɡA'�[�������W��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub lueFacID_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lueFacID.EditValueChanged
        Dim dc As New DepartmentControler
        If lueFacID.EditValue Is Nothing Then Exit Sub

        lueDepID.Properties.DisplayMember = "DepName"
        lueDepID.Properties.ValueMember = "DepID"
        lueDepID.Properties.DataSource = dc.BriName_GetList(strInDepID, Nothing, lueFacID.EditValue)
    End Sub
    ''' <summary>
    ''' ���������������s
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    ''' <summary>
    ''' �������d�ߡ����s
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click
        If txtG_NO.Text.Trim = "" Then
            tempValue = Nothing
        Else
            tempValue = txtG_NO.Text.Trim
        End If

        If txtG_Name.EditValue = "" Then
            tempValue2 = Nothing
        Else
            tempValue2 = txtG_Name.EditValue
        End If

        If txtG_Manager.EditValue = "" Then
            tempValue3 = Nothing
        Else
            tempValue3 = txtG_Manager.EditValue
        End If
        If lueDepID.EditValue = "" Then
            tempValue4 = strInDepID
        Else
            tempValue4 = lueDepID.EditValue
        End If

        If lueFacID.EditValue = "" Then
            tempValue5 = strInFacID
        Else
            tempValue5 = lueFacID.EditValue
        End If

        If dteG_DateBegin.Text.Trim = "" Then
            tempValue6 = Nothing
        Else
            tempValue6 = dteG_DateBegin.Text.Trim
        End If

        If dteG_DateEnd.Text.Trim = "" Then
            tempValue7 = Nothing
        Else
            tempValue7 = dteG_DateEnd.Text.Trim
        End If

        isClickbtnFind = True

        Me.Close()
    End Sub
End Class