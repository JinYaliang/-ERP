Namespace LFERP.Library.NmetalSampleSamllBox
    Public Class NmetalSampleSmallBoxController
#Region "獲取單號"
        ''' <summary>
        ''' 獲取單號
        ''' </summary>
        ''' <param name="SmallBoxID"></param>
        ''' <param name="SmallBoxNum"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampleSmallBox_GetNO(ByVal SmallBoxID As String, ByVal SmallBoxNum As String) As NmetalSampleSmallBoxInfo

            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleSmallBox_GetNO")

            db.AddInParameter(dbComm, "@SmallBoxID", DbType.String, SmallBoxID)
            db.AddInParameter(dbComm, "@SmallBoxNum", DbType.String, SmallBoxNum)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    Return FillNmetalSampleSmallBox(reader)
                End While
                Return Nothing
            End Using
        End Function
#End Region

#Region "新增內箱包裝"
        ''' <summary>
        ''' 新增內箱包裝
        ''' </summary>
        ''' <param name="objInfo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampleSamllBox_Add(ByVal objInfo As NmetalSampleSmallBoxInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleSamllBox_Add")

                db.AddInParameter(dbComm, "@SmallBoxID", DbType.String, objInfo.SmallBoxID)
                db.AddInParameter(dbComm, "@SmallBoxNum", DbType.String, objInfo.SmallBoxNum)
                db.AddInParameter(dbComm, "@SO_ID", DbType.String, objInfo.SO_ID)
                db.AddInParameter(dbComm, "@Code_ID", DbType.String, objInfo.Code_ID)
                db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, objInfo.PM_M_Code)
                db.AddInParameter(dbComm, "@StatusType", DbType.String, objInfo.StatusType)
                db.AddInParameter(dbComm, "@SmallBoxName", DbType.String, objInfo.SmallBoxName)
                db.AddInParameter(dbComm, "@ClientNumber", DbType.String, objInfo.ClientNumber)
                db.AddInParameter(dbComm, "@SmallBoxType", DbType.String, objInfo.SmallBoxType)
                db.AddInParameter(dbComm, "@SmallBoxCount", DbType.String, objInfo.SmallBoxCount)
                db.AddInParameter(dbComm, "@SmallBoxMatrial", DbType.String, objInfo.SmallBoxMatrial)
                db.AddInParameter(dbComm, "@SmallBoxAddAction", DbType.String, objInfo.SmallBoxAddAction)
                db.AddInParameter(dbComm, "@SmallBoxAddDate", DbType.DateTime, objInfo.SmallBoxAddDate)
                db.AddInParameter(dbComm, "@SmallBoxRemark", DbType.String, objInfo.SmallBoxRemark)
                db.AddInParameter(dbComm, "@Remark", DbType.String, objInfo.Remark)
                db.AddInParameter(dbComm, "@DepName", DbType.String, objInfo.DepName)
                db.AddInParameter(dbComm, "@SmallBoxWeight", DbType.String, objInfo.SmallBoxWeight)
                db.AddInParameter(dbComm, "@SO_CusterID", DbType.String, objInfo.SO_CusterID)

                db.ExecuteNonQuery(dbComm)
                NmetalSampleSamllBox_Add = True
            Catch ex As Exception
                NmetalSampleSamllBox_Add = False
            End Try
        End Function
#End Region

#Region "獲取內箱包裝"
        ''' <summary>
        ''' 獲取內箱包裝
        ''' </summary>
        ''' <param name="SO_SampleID"></param>
        ''' <param name="Code_ID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampleSmallBox_GetListParam(ByVal SO_SampleID As String, ByVal Code_ID As String) As List(Of NmetalSampleSmallBoxInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleSmallBox_GetListParam")

            db.AddInParameter(dbComm, "@SO_SampleID", DbType.String, SO_SampleID)
            db.AddInParameter(dbComm, "@Code_ID", DbType.String, Code_ID)


            Dim FeatureList As New List(Of NmetalSampleSmallBoxInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillNmetalSampleSmallBox(reader))
                End While
                Return FeatureList
            End Using
        End Function

        ''' <summary>
        ''' 根据收发单号获取列表
        ''' </summary>
        ''' <param name="SO_SampleID"></param>
        ''' <param name="Code_ID"></param>
        ''' <param name="PK_ID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampleSmallBox_GetListParamByPkID(ByVal SO_SampleID As String, ByVal Code_ID As String, ByVal PK_ID As String, ByVal Flag As String, ByVal PK_Code_ID As String) As List(Of NmetalSampleSmallBoxInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleSmallBox_GetListParamByPkID")

            db.AddInParameter(dbComm, "@SO_SampleID", DbType.String, SO_SampleID)
            db.AddInParameter(dbComm, "@Code_ID", DbType.String, Code_ID)
            db.AddInParameter(dbComm, "@PK_ID", DbType.String, PK_ID)
            db.AddInParameter(dbComm, "@Flag", DbType.String, Flag)
            db.AddInParameter(dbComm, "@PK_Code_ID", DbType.String, PK_Code_ID)

            Dim FeatureList As New List(Of NmetalSampleSmallBoxInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillNmetalSampleSmallBox(reader))
                End While
                Return FeatureList
            End Using
        End Function

        ''' <summary>
        ''' 內箱包裝列表
        ''' </summary>
        ''' <param name="SmallBoxID"></param>
        ''' <param name="SmallBoxNum"></param>
        ''' <param name="SO_ID"></param>
        ''' <param name="Code_ID"></param>
        ''' <param name="PM_M_Code"></param>
        ''' <param name="SmallBoxAddStartDate"></param>
        ''' <param name="SmallBoxAddEndDate"></param>
        ''' <param name="SmallBoxAddAction"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampleSmallBox_GetList(ByVal SmallBoxID As String, ByVal SmallBoxNum As String, ByVal SO_ID As String, ByVal Code_ID As String, ByVal PM_M_Code As String, ByVal SmallBoxAddStartDate As String, ByVal SmallBoxAddEndDate As String, ByVal SmallBoxAddAction As String) As List(Of NmetalSampleSmallBoxInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleSmallBox_GetList")

            db.AddInParameter(dbComm, "@SmallBoxID", DbType.String, SmallBoxID)
            db.AddInParameter(dbComm, "@SmallBoxNum", DbType.String, SmallBoxNum)
            db.AddInParameter(dbComm, "@SO_ID", DbType.String, SO_ID)
            db.AddInParameter(dbComm, "@Code_ID", DbType.String, Code_ID)
            db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, PM_M_Code)
            db.AddInParameter(dbComm, "@SmallBoxAddStartDate", DbType.String, SmallBoxAddStartDate)
            db.AddInParameter(dbComm, "@SmallBoxAddEndDate", DbType.String, SmallBoxAddEndDate)
            db.AddInParameter(dbComm, "@SmallBoxAddAction", DbType.String, SmallBoxAddAction)

            Dim FeatureList As New List(Of NmetalSampleSmallBoxInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillNmetalSampleSmallBox(reader))
                End While
                Return FeatureList
            End Using
        End Function

        ''' <summary>
        ''' 獲取內箱裝箱單號
        ''' </summary>
        ''' <param name="SmallBoxID"></param>
        ''' <param name="SmallBoxNum"></param>
        ''' <param name="SO_ID"></param>
        ''' <param name="Code_ID"></param>
        ''' <param name="PM_M_Code"></param>
        ''' <param name="SmallBoxAddStartDate"></param>
        ''' <param name="SmallBoxAddEndDate"></param>
        ''' <param name="SmallBoxAddAction"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampleSmallBox_GetSmallBoxID(ByVal SmallBoxID As String, ByVal SmallBoxNum As String, ByVal SO_ID As String, ByVal Code_ID As String, ByVal PM_M_Code As String, ByVal SmallBoxAddStartDate As String, ByVal SmallBoxAddEndDate As String, ByVal SmallBoxAddAction As String, ByVal bitNo As String) As List(Of NmetalSampleSmallBoxInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleSmallBox_GetSmallBoxID")

            db.AddInParameter(dbComm, "@SmallBoxID", DbType.String, SmallBoxID)
            db.AddInParameter(dbComm, "@SmallBoxNum", DbType.String, SmallBoxNum)
            db.AddInParameter(dbComm, "@SO_ID", DbType.String, SO_ID)
            db.AddInParameter(dbComm, "@Code_ID", DbType.String, Code_ID)
            db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, PM_M_Code)
            db.AddInParameter(dbComm, "@SmallBoxAddStartDate", DbType.String, SmallBoxAddStartDate)
            db.AddInParameter(dbComm, "@SmallBoxAddEndDate", DbType.String, SmallBoxAddEndDate)
            db.AddInParameter(dbComm, "@SmallBoxAddAction", DbType.String, SmallBoxAddAction)
            db.AddInParameter(dbComm, "@bitNo", DbType.String, bitNo)

            Dim FeatureList As New List(Of NmetalSampleSmallBoxInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillNmetalSampleSmallBox(reader))
                End While
                Return FeatureList
            End Using
        End Function

        ''' <summary>
        ''' 获取收发装箱单号
        ''' </summary>
        ''' <param name="PK_ID"></param>
        ''' <param name="Code_ID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampleSmallBox_GetPkID(ByVal PK_ID As String, ByVal Code_ID As String) As List(Of NmetalSampleSmallBoxInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleSmallBox_GetPkID")

            db.AddInParameter(dbComm, "@PK_ID", DbType.String, PK_ID)
            db.AddInParameter(dbComm, "@Code_ID", DbType.String, Code_ID)

            Dim FeatureList As New List(Of NmetalSampleSmallBoxInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillNmetalSampleSmallBox(reader))
                End While
                Return FeatureList
            End Using
        End Function
#End Region

#Region "修改內箱包裝"
        ''' <summary>
        ''' 修改內箱包裝
        ''' </summary>
        ''' <param name="objInfo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampleSamllBox_Update(ByVal objInfo As NmetalSampleSmallBoxInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleSamllBox_Update")

                db.AddInParameter(dbComm, "@SmallBoxID", DbType.String, objInfo.SmallBoxID)
                db.AddInParameter(dbComm, "@SmallBoxNum", DbType.String, objInfo.SmallBoxNum)
                db.AddInParameter(dbComm, "@SmallBoxCount", DbType.String, objInfo.SmallBoxCount)
                db.AddInParameter(dbComm, "@SmallBoxName", DbType.String, objInfo.SmallBoxName)
                db.AddInParameter(dbComm, "@ClientNumber", DbType.String, objInfo.ClientNumber)
                db.AddInParameter(dbComm, "@SmallBoxType", DbType.String, objInfo.SmallBoxType)
                db.AddInParameter(dbComm, "@SmallBoxMatrial", DbType.String, objInfo.SmallBoxMatrial)
                db.AddInParameter(dbComm, "@SmallBoxAddAction", DbType.String, objInfo.SmallBoxAddAction)
                db.AddInParameter(dbComm, "@SmallBoxAddDate", DbType.DateTime, objInfo.SmallBoxAddDate)
                db.AddInParameter(dbComm, "@SmallBoxRemark", DbType.String, objInfo.SmallBoxRemark)
                db.AddInParameter(dbComm, "@Remark", DbType.String, objInfo.Remark)
                db.AddInParameter(dbComm, "@DepName", DbType.String, objInfo.DepName)
                db.AddInParameter(dbComm, "@SmallBoxWeight", DbType.String, objInfo.SmallBoxWeight)
                db.AddInParameter(dbComm, "@SO_CusterID", DbType.String, objInfo.SO_CusterID)

                db.ExecuteNonQuery(dbComm)
                NmetalSampleSamllBox_Update = True
            Catch ex As Exception
                NmetalSampleSamllBox_Update = False
            End Try
        End Function

        ''' <summary>
        ''' 更新條碼狀態
        ''' </summary>
        ''' <param name="Code_ID"></param>
        ''' <param name="StatusType"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampleSmallBox_UpdateStatusType(ByVal Code_ID As String, ByVal StatusType As String) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleSmallBox_UpdateStatusType")

                db.AddInParameter(dbComm, "@Code_ID", DbType.String, Code_ID)
                db.AddInParameter(dbComm, "@StatusType", DbType.String, StatusType)

                db.ExecuteNonQuery(dbComm)
                NmetalSampleSmallBox_UpdateStatusType = True
            Catch ex As Exception
                NmetalSampleSmallBox_UpdateStatusType = False
            End Try
        End Function
#End Region

#Region "刪除內箱包裝"
        ''' <summary>
        ''' 刪除內箱包裝
        ''' </summary>
        ''' <param name="SmallBoxID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampleSmallBox_Delete(ByVal SmallBoxID As String) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleSmallBox_Delete")

                db.AddInParameter(dbComm, "@SmallBoxID", DbType.String, SmallBoxID)

                db.ExecuteNonQuery(dbComm)
                NmetalSampleSmallBox_Delete = True
            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSampleSmallBox_Delete = False
            End Try
        End Function

        ''' <summary>
        ''' 根據內箱包裝流水號刪除
        ''' </summary>
        ''' <param name="SmallBoxNum"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampleSmallBox_DeleteByNum(ByVal SmallBoxNum As String) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleSmallBox_DeleteByNum")

                db.AddInParameter(dbComm, "@SmallBoxNum", DbType.String, SmallBoxNum)

                db.ExecuteNonQuery(dbComm)
                NmetalSampleSmallBox_DeleteByNum = True
            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSampleSmallBox_DeleteByNum = False
            End Try
        End Function
#End Region

#Region "審核內箱包裝"
        ''' <summary>
        ''' 審     核
        ''' </summary>
        ''' <param name="objInfo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampleSmallBox_Check(ByVal objInfo As NmetalSampleSmallBoxInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleSmallBox_Check")

                db.AddInParameter(dbComm, "@SmallBoxID", DbType.String, objInfo.SmallBoxID)
                db.AddInParameter(dbComm, "@SmallBoxCheck", DbType.Boolean, objInfo.SmallBoxCheck)
                db.AddInParameter(dbComm, "@SmallBoxCheckAction", DbType.String, objInfo.SmallBoxCheckAction)
                db.AddInParameter(dbComm, "@SmallBoxCheckDate", DbType.String, objInfo.SmallBoxCheckDate)
                db.AddInParameter(dbComm, "@SmallBoxCheckRemark", DbType.String, objInfo.SmallBoxCheckRemark)

                db.ExecuteNonQuery(dbComm)
                NmetalSampleSmallBox_Check = True
            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSampleSmallBox_Check = False
            End Try

        End Function
#End Region

#Region "填充數據集合"
        ''' <summary>
        ''' 填充數據集合
        ''' </summary>
        ''' <param name="reader"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function FillNmetalSampleSmallBox(ByVal reader As IDataReader) As NmetalSampleSmallBoxInfo
            On Error Resume Next

            Dim poInfo As New NmetalSampleSmallBoxInfo

            poInfo.AutoID = reader("AutoID").ToString
            poInfo.SmallBoxID = reader("SmallBoxID").ToString
            poInfo.SmallBoxNum = reader("SmallBoxNum").ToString
            poInfo.SO_ID = reader("SO_ID").ToString
            poInfo.Code_ID = reader("Code_ID").ToString
            poInfo.PM_M_Code = reader("PM_M_Code").ToString
            poInfo.ClientNumber = reader("ClientNumber").ToString
            poInfo.SmallBoxCount = reader("SmallBoxCount").ToString
            poInfo.StatusType = reader("StatusType").ToString
            poInfo.SmallBoxName = reader("SmallBoxName").ToString
            poInfo.SmallBoxType = reader("SmallBoxType").ToString
            poInfo.SmallBoxMatrial = reader("SmallBoxMatrial").ToString
            poInfo.SmallBoxCount = reader("SmalllBoxCount").ToString
            poInfo.SmallBoxRemark = reader("SmallBoxRemark").ToString
            poInfo.Remark = reader("Remark").ToString
            poInfo.SmallBoxAddAction = reader("SmallBoxAddAction").ToString
            poInfo.SmallBoxAddDate = CDate(reader("SmallBoxAddDate")).ToString("yyyy/MM/dd")
            poInfo.SmallBoxCheck = reader("SmallBoxCheck").ToString
            poInfo.SmallBoxCheckAction = reader("SmallBoxCheckAction").ToString
            poInfo.SmallBoxCheckDate = CDate(reader("SmallBoxCheckDate")).ToString("yyyy/MM/dd")
            poInfo.SmallBoxCheckRemark = reader("SmallBoxCheckRemark").ToString

            poInfo.SO_SampleID = reader("SO_SampleID").ToString
            poInfo.PK_ID = reader("PK_ID").ToString
            poInfo.D_Dep = reader("D_Dep").ToString

            poInfo.ActionName = reader("ActionName").ToString
            poInfo.CheckActionName = reader("CheckActionName").ToString
            poInfo.DepName = reader("DepName").ToString
            poInfo.SmallBoxWeight = reader("SmallBoxWeight").ToString
            poInfo.SO_CusterID = reader("SO_CusterID").ToString

            Return poInfo
        End Function
#End Region

    End Class
End Namespace
