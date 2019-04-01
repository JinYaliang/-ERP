Imports LFERP.DataSetting
Imports LFERP.Library.ProductionField
Imports LFERP.Library.Production.ProductionFieldDaySummary

Public Class frmProductionDeparmentReport

    Dim upc As New UserPowerControl
    Dim pc As New LFERP.Library.PieceProcess.PersonnelControl
    Dim pdc As New ProductionFieldDaySummaryControl
    Public strDate As String

    Sub LoadDepartment()   '�����H��

        GluDep.Properties.DataSource = pc.FacBriSearch_GetList(strInFacID, Nothing, strInDepID, Nothing) '��l����
        GluDep.Properties.DisplayMember = "DepName"
        GluDep.Properties.ValueMember = "DepID"

    End Sub


    Private Sub frmProductionDeparmentReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadDepartment() '�ɤJ�����H��

        DateEdit1.Text = Format(DateAdd(DateInterval.Day, -7, CDate(Format(Now, "yyyy/MM/dd"))), "yyyy/MM/dd")  '�q�{�p��@�ӬP���O��(�]�[��ѰO��)
        DateEdit2.Text = Format(Now, "yyyy/MM/dd")

    End Sub

    '�O����e�d�߫H��--(��e���նȨϥΤ@��)
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Try
            If GluDep.EditValue = "" Then
                MsgBox("�������ର��,�п�ܳ���!")
                Exit Sub
            End If
            If DateEdit1.Text = "" Or DateEdit2.Text = "" Then
                MsgBox("������ର��,�п�ܬd�ߤ��!")
                Exit Sub
            End If

            Dim ds As New DataSet
            Dim ltc As New CollectionToDataSet

            Dim pfc As New ProductionFieldControl

            ds.Tables.Clear()

            Dim str1, str2 As String

            str1 = DateEdit1.Text
            str2 = DateEdit2.Text
 
            If pfc.ProductionField_GetList1(Nothing, Nothing, Nothing, Nothing, Nothing, "�o��", Nothing, GluDep.EditValue, Nothing, True, True, str1, str2, Nothing, Nothing).Count = 0 Then
                MsgBox("��e�����b������d�򤺵L���Ƶo�X�O��!")
                Exit Sub
            End If

            ltc.CollToDataSet(ds, "ProductionField", pfc.ProductionField_GetList1(Nothing, Nothing, Nothing, Nothing, Nothing, "�o��", Nothing, GluDep.EditValue, Nothing, True, True, str1, str2, Nothing, Nothing))
            PreviewRPT(ds, "rptProductionDetail", "�����Ͳ��u���������`", True, True)

            ltc = Nothing
            Me.Close()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub
End Class