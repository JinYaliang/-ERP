Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.Data.Sql
Imports LFERP.Library.SampleManager.SampleOrdersMain
Namespace LFERP.Library.SampleManager.SampleStorage
    Public Class SampleStorageController
        Friend Function FillSampleStorage(ByVal reader As IDataReader) As SampleStorageInfo
            On Error Resume Next
            Dim objInfo As New SampleStorageInfo
            objInfo.AutoID = reader("AutoID")
            objInfo.D_ID = reader("D_ID").ToString
            objInfo.Code_ID = reader("Code_ID").ToString
            objInfo.SS_StorageLocation = reader("SS_StorageLocation").ToString
            objInfo.SS_ShelveID = reader("SS_ShelveID").ToString
            objInfo.SO_ID = reader("SO_ID").ToString
            objInfo.CreateUserID = reader("CreateUserID").ToString
            If reader("CreateDate") Is DBNull.Value Then
                objInfo.CreateDate = Nothing
            Else
                objInfo.CreateDate = Format(CDate(reader("CreateDate")), "yyyy/MM/dd")
            End If
            objInfo.ModifyUserID = reader("ModifyUserID").ToString
            If reader("ModifyDate") Is DBNull.Value Then
                objInfo.ModifyDate = Nothing
            Else
                objInfo.ModifyDate = Format(CDate(reader("ModifyDate")), "yyyy/MM/dd")
            End If
            If reader("CheckBit") Is DBNull.Value Then
                objInfo.CheckBit = Nothing
            Else
                objInfo.CheckBit = reader("CheckBit")
            End If
            If reader("CheckDate") Is DBNull.Value Then
                objInfo.CheckDate = Nothing
            Else
                objInfo.CheckDate = Format(CDate(reader("CheckDate")), "yyyy/MM/dd")
            End If
            objInfo.CheckRemark = reader("CheckRemark").ToString
            objInfo.CheckUserID = reader("CheckUserID").ToString
            objInfo.Remark = reader("Remark").ToString
            objInfo.CheckUserName = reader("CheckUserName").ToString
            objInfo.CreateUserName = reader("CreateUserName").ToString
            objInfo.DepName = reader("DepName").ToString
            objInfo.ModifyUserName = reader("ModifyUserName").ToString
            If reader("Qty") Is DBNull.Value Then
                objInfo.Qty = 0
            Else
                objInfo.Qty = reader("Qty")
            End If
            objInfo.PM_M_Code = reader("PM_M_Code").ToString
            objInfo.SE_ID = reader("SE_ID").ToString
            objInfo.SO_SampleID = reader("SO_SampleID").ToString
            objInfo.PK_Code_ID = reader("PK_Code_ID").ToString

            Return objInfo
        End Function

        Public Function SampleStorage_GetList(ByVal AutoID As String, ByVal D_ID As String, ByVal Code_ID As String, ByVal SS_StorageLocation As String, ByVal SS_ShelveID As String) As List(Of SampleStorageInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleStorage_GetList")
            dbComm.CommandTimeout = 0
            If AutoID <> 0 Then
                db.AddInParameter(dbComm, "@AutoID", DbType.String, AutoID)
            End If

            db.AddInParameter(dbComm, "@D_ID", DbType.String, D_ID)
            db.AddInParameter(dbComm, "@Code_ID", DbType.String, Code_ID)
            db.AddInParameter(dbComm, "@SS_StorageLocation", DbType.String, SS_StorageLocation)
            db.AddInParameter(dbComm, "@SS_ShelveID", DbType.String, SS_ShelveID)

            Dim FeatureList As New List(Of SampleStorageInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillSampleStorage(reader))
                End While
                Return FeatureList
            End Using
        End Function

        Public Function SampleStorageA_GetList(ByVal SE_InD_ID As String) As List(Of SampleStorageInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleStorageA_GetList")
            dbComm.CommandTimeout = 0
            db.AddInParameter(dbComm, "@SE_InD_ID", DbType.String, SE_InD_ID)

            Dim FeatureList As New List(Of SampleStorageInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillSampleStorage(reader))
                End While
                Return FeatureList
            End Using
        End Function

        Public Function SampleStorage_SE_IDDelete(ByVal SE_ID As String) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleStorage_SE_IDDelete")
                dbComm.CommandTimeout = 0
                db.AddInParameter(dbComm, "@SE_ID", DbType.String, SE_ID)
                db.ExecuteNonQuery(dbComm)
                SampleStorage_SE_IDDelete = True
            Catch ex As Exception
                MsgBox(ex.Message)
                SampleStorage_SE_IDDelete = False
            End Try
        End Function

        Public Function SampleStorage_Delete(ByVal AutoID As String, ByVal Code_ID As String) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleStorage_Delete")
                dbComm.CommandTimeout = 0
                db.AddInParameter(dbComm, "@AutoID", DbType.String, AutoID)
                db.AddInParameter(dbComm, "@Code_ID", DbType.String, Code_ID)
                db.ExecuteNonQuery(dbComm)
                SampleStorage_Delete = True
            Catch ex As Exception
                MsgBox(ex.Message)
                SampleStorage_Delete = False
            End Try
        End Function


        Public Function SampleStorage_Update(ByVal objinfo As SampleStorageInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleStorage_Update")
                dbComm.CommandTimeout = 0
                db.AddInParameter(dbComm, "@AutoID", DbType.Decimal, objinfo.AutoID)
                db.AddInParameter(dbComm, "@SS_StorageLocation", DbType.String, objinfo.SS_StorageLocation)
                db.AddInParameter(dbComm, "@SS_ShelveID", DbType.String, objinfo.SS_ShelveID)
                db.AddInParameter(dbComm, "@ModifyUserID", DbType.String, objinfo.ModifyUserID)
                db.AddInParameter(dbComm, "@Remark", DbType.String, objinfo.Remark)
                db.ExecuteNonQuery(dbComm)
                SampleStorage_Update = True
            Catch ex As Exception
                MsgBox(ex.Message)
                SampleStorage_Update = False
            End Try
        End Function

        Public Function SampleStorage_Check(ByVal objinfo As SampleStorageInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleStorage_Check")
                dbComm.CommandTimeout = 0
                db.AddInParameter(dbComm, "@AutoID", DbType.Decimal, objinfo.AutoID)
                db.AddInParameter(dbComm, "@CheckBit", DbType.String, objinfo.CheckBit)

                db.AddInParameter(dbComm, "@CheckUserID", DbType.String, objinfo.CheckUserID)
                db.AddInParameter(dbComm, "@CheckRemark", DbType.String, objinfo.CheckRemark)
                db.ExecuteNonQuery(dbComm)
                SampleStorage_Check = True
            Catch ex As Exception
                MsgBox(ex.Message)
                SampleStorage_Check = False
            End Try
        End Function

        Public Function SampleStorage_Add(ByVal objInfo As SampleStorageInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleStorage_Add")
                dbComm.CommandTimeout = 0
                db.AddInParameter(dbComm, "@Code_ID", DbType.String, objInfo.Code_ID)
                db.AddInParameter(dbComm, "@D_ID", DbType.String, objInfo.D_ID)
                db.AddInParameter(dbComm, "@SS_StorageLocation", DbType.String, objInfo.SS_StorageLocation)
                db.AddInParameter(dbComm, "@SS_ShelveID", DbType.String, objInfo.SS_ShelveID)
                db.AddInParameter(dbComm, "@CreateUserID", DbType.String, objInfo.CreateUserID)
                db.AddInParameter(dbComm, "@Remark", DbType.String, objInfo.Remark)
                db.AddInParameter(dbComm, "@Qty", DbType.Decimal, objInfo.Qty)
                db.ExecuteNonQuery(dbComm)
                SampleStorage_Add = True
            Catch ex As Exception
                MsgBox(ex.Message)
                SampleStorage_Add = False
            End Try
        End Function

        Public Function SamplePaceBarCode_GetCodeIDBySE_ID(ByVal SE_ID As String) As DataTable
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("SamplePaceBarCode_GetCodeIDBySE_ID")
            dbComm.CommandTimeout = 0
            db.AddInParameter(dbComm, "@SE_ID", DbType.String, SE_ID)
            Dim ds As New DataSet
            ds = db.ExecuteDataSet(dbComm)
            If ds.Tables.Count > 0 Then
                Return ds.Tables(0)
            Else
                Return Nothing
            End If
        End Function
    End Class
End Namespace