Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.Data.Sql

Namespace LFERP.Library.NmetalSampleManager.NmetalSamplePlan

    Public Class NmetalSamplePlanControler
        Public Function NmetalSamplePlan_Add(ByVal objinfo As NmetalSamplePlanInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSamplePlan_Add")

                db.AddInParameter(dbComm, "SP_ID", DbType.String, objinfo.SP_ID)
                db.AddInParameter(dbComm, "SO_ID", DbType.String, objinfo.SO_ID)
                db.AddInParameter(dbComm, "SS_Edition", DbType.String, objinfo.SS_Edition)
                db.AddInParameter(dbComm, "SP_StartDate", DbType.DateTime, CDate(objinfo.SP_StartDate))
                db.AddInParameter(dbComm, "SP_EndDate", DbType.DateTime, CDate(objinfo.SP_EndDate))
                db.AddInParameter(dbComm, "SP_Remark", DbType.String, objinfo.SP_Remark)
                db.AddInParameter(dbComm, "SP_AddUserID", DbType.String, objinfo.SP_AddUserID)
                db.AddInParameter(dbComm, "SP_AddDate", DbType.DateTime, CDate(objinfo.SP_AddDate))
                'db.AddInParameter(dbComm, "SP_ModifyUserID", DbType.String, objinfo.SP_ModifyUserID)
                'db.AddInParameter(dbComm, "SP_ModifyDate", DbType.DateTime, objinfo.SP_ModifyDate)
                db.AddInParameter(dbComm, "PM_M_Code", DbType.String, objinfo.PM_M_Code)
                db.AddInParameter(dbComm, "M_Code", DbType.String, objinfo.M_Code)

                db.ExecuteNonQuery(dbComm)
                NmetalSamplePlan_Add = True
            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSamplePlan_Add = False
            End Try
        End Function

        Public Function NmetalSamplePlan_Update(ByVal objinfo As NmetalSamplePlanInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSamplePlan_Update")
                db.AddInParameter(dbComm, "SP_ID", DbType.String, objinfo.SP_ID)
                db.AddInParameter(dbComm, "SO_ID", DbType.String, objinfo.SO_ID)
                db.AddInParameter(dbComm, "SS_Edition", DbType.String, objinfo.SS_Edition)
                db.AddInParameter(dbComm, "SP_StartDate", DbType.DateTime, objinfo.SP_StartDate)
                db.AddInParameter(dbComm, "SP_EndDate", DbType.DateTime, objinfo.SP_EndDate)
                db.AddInParameter(dbComm, "SP_Remark", DbType.String, objinfo.SP_Remark)
                'db.AddInParameter(dbComm, "SP_AddUserID", DbType.String, objinfo.SP_AddUserID)
                'db.AddInParameter(dbComm, "SP_AddDate", DbType.DateTime, objinfo.SP_AddDate)
                db.AddInParameter(dbComm, "SP_ModifyUserID", DbType.String, objinfo.SP_ModifyUserID)
                db.AddInParameter(dbComm, "SP_ModifyDate", DbType.DateTime, objinfo.SP_ModifyDate)
                db.AddInParameter(dbComm, "PM_M_Code", DbType.String, objinfo.PM_M_Code)
                db.AddInParameter(dbComm, "M_Code", DbType.String, objinfo.M_Code)
                db.AddInParameter(dbComm, "AutoID", DbType.Decimal, objinfo.AutoID)

                db.ExecuteNonQuery(dbComm)
                NmetalSamplePlan_Update = True

            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSamplePlan_Update = False
            End Try
        End Function

        Public Function NmetalSamplePlan_Delete(ByVal AutoID As Decimal) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSamplePlan_Delete")
                db.AddInParameter(dbComm, "@AutoID", DbType.String, AutoID)
                db.ExecuteNonQuery(dbComm)
                NmetalSamplePlan_Delete = True
            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSamplePlan_Delete = False
            End Try
        End Function

        Public Function NmetalSamplePlan_Getlist(ByVal SP_ID As String, ByVal SO_ID As String, ByVal M_Code As String, ByVal StartDate As String, ByVal EndDate As String, ByVal strStartDate As String, ByVal strEndDate As String, ByVal SO_IDCheck As Boolean, ByVal StrStartAddDate As String, ByVal StrStartEndDate As String, ByVal SP_AddUserID As String) As List(Of NmetalSamplePlanInfo)

            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSamplePlan_Getlist")

            db.AddInParameter(dbComm, "@SO_ID", DbType.String, SO_ID)
            db.AddInParameter(dbComm, "@SP_ID", DbType.String, SP_ID)
            db.AddInParameter(dbComm, "@M_Code", DbType.String, M_Code)
            db.AddInParameter(dbComm, "@StartDate", DbType.String, StartDate)
            db.AddInParameter(dbComm, "@EndDate", DbType.String, EndDate)
            db.AddInParameter(dbComm, "@strStartDate", DbType.String, strStartDate)
            db.AddInParameter(dbComm, "@strEndDate", DbType.String, strEndDate)
            db.AddInParameter(dbComm, "@SO_IDCheck", DbType.Boolean, SO_IDCheck)

            db.AddInParameter(dbComm, "@StrStartAddDate", DbType.String, StrStartAddDate)
            db.AddInParameter(dbComm, "@StrStartEndDate", DbType.String, StrStartEndDate)
            db.AddInParameter(dbComm, "@SP_AddUserID", DbType.String, SP_AddUserID)

            Dim FeatureList As New List(Of NmetalSamplePlanInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillNmetalSamplePlanType(reader))
                End While
                Return FeatureList
            End Using
        End Function


        Public Function NmetalSamplePlan_Get(ByVal SP_ID As String) As NmetalSamplePlanInfo
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSamplePlan_Get")
            db.AddInParameter(dbComm, "@SP_ID", DbType.String, SP_ID)
            Dim FeatureList As New NmetalSamplePlanInfo
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.SP_ID = reader("SP_ID").ToString
                End While
                Return FeatureList
            End Using
        End Function
        Public Function NmetalSamplePlan_GetItem(ByVal SO_ID As String, ByVal SS_Edition As String) As List(Of NmetalSamplePlanInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSamplePlan_GetItem")
            db.AddInParameter(dbComm, "@SO_ID", DbType.String, SO_ID)
            db.AddInParameter(dbComm, "@SS_Edition", DbType.String, SS_Edition)

            Dim FeatureList As New List(Of NmetalSamplePlanInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillNmetalSamplePlanType(reader))
                End While
                Return FeatureList
            End Using
        End Function


        Friend Function FillNmetalSamplePlanType(ByVal reader As IDataReader) As NmetalSamplePlanInfo
            '对应取得的数据
            On Error Resume Next

            Dim objInfo As New NmetalSamplePlanInfo

            objInfo.SP_ID = reader("SP_ID").ToString
            objInfo.SO_ID = reader("SO_ID").ToString
            objInfo.SS_Edition = reader("SS_Edition").ToString
            objInfo.SP_StartDate = CDate(reader("SP_StartDate").ToString)
            objInfo.SP_EndDate = CDate(reader("SP_EndDate").ToString)
            objInfo.SP_Remark = reader("SP_Remark").ToString
            objInfo.SP_AddUserID = reader("SP_AddUserID").ToString
            objInfo.SP_AddDate = CDate(reader("SP_AddDate").ToString)
            objInfo.SP_ModifyUserID = reader("SP_ModifyUserID").ToString
            objInfo.SP_ModifyDate = CDate(reader("SP_ModifyDate").ToString)
            objInfo.PM_M_Code = reader("PM_M_Code").ToString
            objInfo.M_Code = reader("M_Code").ToString
            objInfo.AutoID = CDbl(reader("AutoID").ToString)
            objInfo.SO_Closed = CBool(reader("SO_Closed").ToString)
            objInfo.M_Name = reader("M_Name").ToString
            objInfo.SP_AddUserName = reader("SP_AddUserName").ToString
            objInfo.SO_OrderQty = reader("SO_OrderQty")
            objInfo.SO_SampleID = reader("SO_SampleID").ToString
            Return objInfo
        End Function

    End Class
End Namespace