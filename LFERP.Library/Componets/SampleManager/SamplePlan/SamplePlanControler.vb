Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.Data.Sql

Namespace LFERP.Library.SampleManager.SamplePlan

    Public Class SamplePlanControler
        Public Function SamplePlan_Add(ByVal objinfo As SamplePlanInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("SamplePlan_Add")
                dbComm.CommandTimeout = 0
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
                SamplePlan_Add = True
            Catch ex As Exception
                MsgBox(ex.Message)
                SamplePlan_Add = False
            End Try
        End Function

        Public Function SamplePlan_Update(ByVal objinfo As SamplePlanInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("SamplePlan_Update")
                dbComm.CommandTimeout = 0
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
                SamplePlan_Update = True

            Catch ex As Exception
                MsgBox(ex.Message)
                SamplePlan_Update = False
            End Try
        End Function

        Public Function SamplePlan_Delete(ByVal AutoID As Decimal) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("SamplePlan_Delete")
                dbComm.CommandTimeout = 0
                db.AddInParameter(dbComm, "@AutoID", DbType.String, AutoID)
                db.ExecuteNonQuery(dbComm)
                SamplePlan_Delete = True
            Catch ex As Exception
                MsgBox(ex.Message)
                SamplePlan_Delete = False
            End Try
        End Function

        Public Function SamplePlan_Getlist(ByVal SP_ID As String, ByVal SO_ID As String, ByVal M_Code As String, ByVal StartDate As String, ByVal EndDate As String, ByVal strStartDate As String, ByVal strEndDate As String, ByVal SO_IDCheck As Boolean, ByVal StrStartAddDate As String, ByVal StrStartEndDate As String, ByVal SP_AddUserID As String) As List(Of SamplePlanInfo)

            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("SamplePlan_Getlist")
            dbComm.CommandTimeout = 0
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

            Dim FeatureList As New List(Of SamplePlanInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillSamplePlanType(reader))
                End While
                Return FeatureList
            End Using
        End Function


        Public Function SamplePlan_Get(ByVal SP_ID As String) As SamplePlanInfo
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("SamplePlan_Get")
            dbComm.CommandTimeout = 0
            db.AddInParameter(dbComm, "@SP_ID", DbType.String, SP_ID)
            Dim FeatureList As New SamplePlanInfo
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.SP_ID = reader("SP_ID").ToString
                End While
                Return FeatureList
            End Using
        End Function
        Public Function SamplePlan_GetItem(ByVal SO_ID As String, ByVal SS_Edition As String) As List(Of SamplePlanInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("SamplePlan_GetItem")
            dbComm.CommandTimeout = 0
            db.AddInParameter(dbComm, "@SO_ID", DbType.String, SO_ID)
            db.AddInParameter(dbComm, "@SS_Edition", DbType.String, SS_Edition)

            Dim FeatureList As New List(Of SamplePlanInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillSamplePlanType(reader))
                End While
                Return FeatureList
            End Using
        End Function


        Friend Function FillSamplePlanType(ByVal reader As IDataReader) As SamplePlanInfo
            '对应取得的数据
            On Error Resume Next

            Dim objInfo As New SamplePlanInfo

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

            '袁毅龙 201408 05
            objInfo.CVSName = reader("CVSName").ToString
            objInfo.CreateDate = CDate(reader("CreateDate")).ToString("yyyy/MM/dd")
            objInfo.UploadDate = CDate(reader("UploadDate")).ToString("yyyy/MM/dd")
            objInfo.Flag = reader("Flag").ToString
            Return objInfo
        End Function
#Region "袁毅龍20140806 獲取單號"
        ''' <summary>
        ''' FTP上传名称
        ''' </summary>
        ''' <param name="CVSName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SamplePaceFTP_GetCVsName(ByVal CVSName As String) As SamplePlanInfo
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("SamplePaceFTP_GetCVsName")
            dbComm.CommandTimeout = 0
            db.AddInParameter(dbComm, "@CVSName", DbType.String, CVSName)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    Return FillSamplePlanType(reader)
                End While
                Return Nothing
            End Using
        End Function
#End Region
#Region "袁毅龍20140806 獲取外發號需上傳ＣＳＶ的信息"
        Public Function SamplePace_CSVGetList(ByVal SE_ID As String) As DataTable
            Try
                Dim ds As New DataSet
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("SamplePace_CSVGetList")
                dbComm.CommandTimeout = 0
                db.AddInParameter(dbComm, "@SE_ID", DbType.String, SE_ID)

                ds = db.ExecuteDataSet(dbComm)
                If ds.Tables.Count > 0 Then
                    Return ds.Tables(0)
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
                Return Nothing
            End Try
        End Function
#End Region
#Region "袁毅龍20140807　CSV 上傳記錄"
        Public Function SamplePaceFTP_Add(ByVal objInfo As SamplePlanInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("SamplePaceFTP_Add")
                dbComm.CommandTimeout = 0
                db.AddInParameter(dbComm, "@SP_ID", DbType.String, objInfo.SP_ID)
                db.AddInParameter(dbComm, "@CVSName", DbType.String, objInfo.CVSName)
                db.AddInParameter(dbComm, "@CreateDate", DbType.String, objInfo.CreateDate)
                db.AddInParameter(dbComm, "@OperationUserID", DbType.String, objInfo.OperationUserID)
                db.ExecuteNonQuery(dbComm)
                SamplePaceFTP_Add = True
            Catch ex As Exception
                MsgBox(ex.Message)
                SamplePaceFTP_Add = False
            End Try
        End Function
#End Region
#Region "袁毅龍20140807　更新ＣＳＶ上傳記錄"
        Public Function SamplePaceFTP_Update(ByVal objinfo As SamplePlanInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("SamplePaceFTP_Update")

                dbComm.CommandTimeout = 0
                db.AddInParameter(dbComm, "@SP_ID", DbType.String, objinfo.SP_ID)
                db.AddInParameter(dbComm, "@UploadDate", DbType.String, objinfo.UploadDate)
                db.AddInParameter(dbComm, "@Flag", DbType.Boolean, objinfo.Flag)
                db.AddInParameter(dbComm, "@OperationUserID", DbType.String, objinfo.OperationUserID)
                db.ExecuteNonQuery(dbComm)
                SamplePaceFTP_Update = True
            Catch ex As Exception
                MsgBox(ex.Message)
                SamplePaceFTP_Update = False
            End Try
        End Function
#End Region
#Region "袁毅龍20140806　更新SamplePace 的ＣＳＶ上傳記錄"
        Public Function SamplePace_UpdateCVSName(ByVal objinfo As SamplePlanInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("SamplePace_UpdateCVSName")
                dbComm.CommandTimeout = 0
                db.AddInParameter(dbComm, "@SE_ID", DbType.String, objinfo.SP_ID)
                db.AddInParameter(dbComm, "@CVSName", DbType.String, objinfo.CVSName)

                db.ExecuteNonQuery(dbComm)
                SamplePace_UpdateCVSName = True
            Catch ex As Exception
                MsgBox(ex.Message)
                SamplePace_UpdateCVSName = False
            End Try
        End Function
#End Region
    End Class
End Namespace