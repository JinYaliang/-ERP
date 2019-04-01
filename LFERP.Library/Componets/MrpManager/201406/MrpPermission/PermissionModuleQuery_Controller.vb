Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.Data.Sql
Namespace LFERP.Library.MrpManager.MrpPermission
    Public Class PermissionModuleQuery_Controller
        Public Function PermissionModuleQuery_GetList(ByVal PMS_ID As String, ByVal Category As String, ByVal FieldName As String, ByVal Type As String, ByVal AutoID As String) As List(Of PermissionModuleQueryInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("PermissionModuleQuery_GetList")
            db.AddInParameter(dbComm, "@PMS_ID", DbType.String, PMS_ID)
            db.AddInParameter(dbComm, "@Category", DbType.String, Category)
            db.AddInParameter(dbComm, "@FieldName", DbType.String, FieldName)
            db.AddInParameter(dbComm, "@Type", DbType.String, Type)
            db.AddInParameter(dbComm, "@AutoID", DbType.String, AutoID)

            Dim FeatureList As New List(Of PermissionModuleQueryInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillPermissionModuleQuery(reader))
                End While
                Return FeatureList
            End Using
        End Function



        Friend Function FillPermissionModuleQuery(ByVal reader As IDataReader) As PermissionModuleQueryInfo
            '�������o���ƾ�
            On Error Resume Next
            Dim objInfo As New PermissionModuleQueryInfo
            objInfo.PMS_ID = reader("PMS_ID").ToString
            objInfo.Category = reader("Category").ToString
            objInfo.FieldName = reader("FieldName").ToString
            objInfo.Type = reader("Type").ToString
            objInfo.AutoID = reader("AutoID").ToString
            Return objInfo
        End Function

    End Class
End Namespace
