Imports LFERP.Library.MrpManager.MrpInfo

Public Class frmMrpPurchaseCalcRecord

    Private Sub frmMrpPurchaseCalcRecord_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim mpcrc As New MrpPurchaseCalcRecordController
        GridControl1.DataSource = mpcrc.MrpPurchaseCalcRecord_GetList(Nothing)
        BandedGridView1.Bands.Item(0).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        'BandedGridView1.ActiveFilterString = " CreateDate > " + Now.AddMonths(-3)
        BandedGridView1.ActiveFilterString = "Mrp_ID like 'M%' "
    End Sub

#Region "��Grid�����f�֤���M�B�������]�m��ܮ榡"
    Private Sub GridView1_CustomColumnDisplayText(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles BandedGridView1.CustomColumnDisplayText
        Try
            '----------------��q��}�l������ŮɡA�h�����----------------
            If e.Column.FieldName = "NeedBeginDate" Then
                If e.Value = Nothing Then e.DisplayText = ""
            End If
            '----------------��q�浲��������ŮɡA�h�����----------------
            If e.Column.FieldName = "NeedEndDate" Then
                If e.Value = Nothing Then e.DisplayText = ""
            End If
            '----------------��������1����ܥ����A�_�h��ܼ���----------------
            If e.Column.FieldName = "MRPType" Then
                If e.Value = 1 Then
                    e.DisplayText = "����"
                Else
                    e.DisplayText = "����"
                End If
            End If
            '----------------��B�����1�ɫh��ܫ�����B��A�_�h��ܫ��渹�B��----------------
            If e.Column.FieldName = "CalcType" Then
                If e.Value = 1 Then
                    e.DisplayText = "������B��"
                Else
                    e.DisplayText = "���渹�B��"
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        
    End Sub
#End Region

End Class