Public Class frmKnifeWareInputExport

    '**************************************
    '**********2013-11-8 ���@�s�W**********
    '**************************************


    Private Shared m_DTReport As String              '����d�ߤ��

    Public Shared Property DTReport() As String
        Get
            Return m_DTReport
        End Get
        Set(ByVal value As String)
            m_DTReport = value
        End Set
    End Property

    Private Shared m_DTReportAll As String           '������
    Public Shared Property DTReportAll() As String
        Get
            Return m_DTReportAll
        End Get
        Set(ByVal value As String)
            m_DTReportAll = value
        End Set
    End Property

    Private Shared m_BolReport As Boolean              '���L����P�O����
    Public Shared Property BolReport() As Boolean
        Get
            Return m_BolReport
        End Get
        Set(ByVal value As Boolean)
            m_BolReport = value
        End Set
    End Property

    Private Sub lblExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblExportExcel.Click
        m_DTReport = DtEditSelect.DateTime.Month.ToString
        m_DTReportAll = DtEditSelect.Text.ToString
        m_BolReport = True
        Me.Close()
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        m_DTReport = DtEditSelect.DateTime.Month.ToString
        m_DTReportAll = DtEditSelect.Text.ToString
        Me.Close()
    End Sub

    Private Sub frmKnifeWareInputExport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        m_DTReport = ""
        m_BolReport = False
        DtEditSelect.DateTime = DateTime.Now
    End Sub


   
End Class