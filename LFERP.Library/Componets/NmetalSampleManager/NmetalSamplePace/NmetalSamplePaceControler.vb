Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.Data.Sql

Namespace LFERP.Library.NmetalSampleManager.NmetalSamplePace

    Public Class NmetalSamplePaceControler
        Public Function NmetalSamplePace_Add(ByVal objinfo As NmetalSamplePaceInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSamplePace_Add")

                db.AddInParameter(dbComm, "SO_ID", DbType.String, objinfo.SO_ID)
                db.AddInParameter(dbComm, "SS_Edition", DbType.String, objinfo.SS_Edition)
                db.AddInParameter(dbComm, "PS_NO", DbType.String, objinfo.PS_NO)
                db.AddInParameter(dbComm, "SE_PaceDescribe", DbType.String, objinfo.SE_PaceDescribe)
                db.AddInParameter(dbComm, "State", DbType.String, objinfo.State)
                db.AddInParameter(dbComm, "SE_AddUserID", DbType.String, objinfo.SE_AddUserID)
                db.AddInParameter(dbComm, "SE_AddDate", DbType.DateTime, CDate(objinfo.SE_AddDate))
  
                db.AddInParameter(dbComm, "PM_M_Code", DbType.String, objinfo.PM_M_Code)
                db.AddInParameter(dbComm, "M_Code", DbType.String, objinfo.M_Code)
                db.AddInParameter(dbComm, "SE_Type", DbType.String, objinfo.SE_Type)
                db.AddInParameter(dbComm, "SE_Qty", DbType.Int32, objinfo.SE_Qty)
                db.AddInParameter(dbComm, "SE_ID", DbType.String, objinfo.SE_ID)
                db.AddInParameter(dbComm, "SPID", DbType.String, objinfo.SPID)
                db.AddInParameter(dbComm, "SE_OutInType", DbType.String, objinfo.SE_OutInType)
                db.AddInParameter(dbComm, "SE_OutD_ID", DbType.String, objinfo.SE_OutD_ID)
                db.AddInParameter(dbComm, "SE_OutPS_NO", DbType.String, objinfo.SE_OutPS_NO)
                db.AddInParameter(dbComm, "SE_OutTime", DbType.DateTime, objinfo.SE_OutTime)
                db.AddInParameter(dbComm, "SE_InD_ID", DbType.String, objinfo.SE_InD_ID)
                db.AddInParameter(dbComm, "SE_InPS_NO", DbType.String, objinfo.SE_InPS_NO)
                db.AddInParameter(dbComm, "SE_OutCardID", DbType.String, objinfo.SE_OutCardID)
                db.AddInParameter(dbComm, "SE_Remark", DbType.String, objinfo.SE_Remark)
                db.AddInParameter(dbComm, "SE_LoanID", DbType.String, objinfo.SE_LoanID)
                db.AddInParameter(dbComm, "SE_BorrowType", DbType.String, objinfo.SE_BorrowType)
                db.AddInParameter(dbComm, "SE_BarCodeType", DbType.String, objinfo.SE_BarCodeType)
                db.AddInParameter(dbComm, "SE_BorrowTime", DbType.Int16, objinfo.SE_BorrowTime)
                db.AddInParameter(dbComm, "OutWeighing", DbType.Decimal, objinfo.OutWeighing)
                db.AddInParameter(dbComm, "InWeighing", DbType.Decimal, objinfo.InWeighing)
                db.AddInParameter(dbComm, "SupplierID", DbType.String, objinfo.SupplierID)
                db.AddInParameter(dbComm, "SE_CodeType", DbType.Int16, objinfo.SE_CodeType)
                '2014-07-03

                db.AddInParameter(dbComm, "@OutQtying", DbType.Int16, objinfo.OutQtying)
                db.AddInParameter(dbComm, "@InQtying", DbType.Int16, objinfo.InQtying)

                db.AddInParameter(dbComm, "SE_OutYCCardID", DbType.String, objinfo.SE_OutYCCardID)


                db.ExecuteNonQuery(dbComm)
                NmetalSamplePace_Add = True
            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSamplePace_Add = False
            End Try
        End Function

        ''' <summary>
        ''' 2014-04-23 
        ''' 姚      駿
        ''' 更改借出人和還入人
        ''' </summary>
        ''' <param name="objinfo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSamplePace_UpdateInOutCardID(ByVal objinfo As NmetalSamplePaceInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSamplePace_UpdateInOutCardID")

                db.AddInParameter(dbComm, "@SE_ID", DbType.String, objinfo.SE_ID)
                db.AddInParameter(dbComm, "@SE_OutCardID", DbType.String, objinfo.SE_OutCardID)
                db.AddInParameter(dbComm, "@SE_InCardID", DbType.String, objinfo.SE_InCardID)

                db.ExecuteNonQuery(dbComm)
                NmetalSamplePace_UpdateInOutCardID = True

            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSamplePace_UpdateInOutCardID = False
            End Try
        End Function

        ''' <summary>
        ''' 2014-04-23
        ''' 姚      駿
        ''' 獲取樣辦借出人信息
        ''' </summary>
        ''' <param name="SE_InCardID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSamplePace_GetBrrowInfo(ByVal SE_InCardID As String) As List(Of NmetalSamplePaceInfo)

            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSamplePace_GetBrrowInfo")

            db.AddInParameter(dbComm, "@SE_InCardID", DbType.String, SE_InCardID)

            Dim FeatureList As New List(Of NmetalSamplePaceInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillNmetalSamplePaceType(reader))
                End While
                Return FeatureList
            End Using
        End Function

        ''' <summary>
        ''' 獲取數據庫連接信息
        ''' 2014-05-15 
        ''' 姚      駿
        ''' </summary>
        ''' <param name="ServerIP"></param>
        ''' <param name="DataBaseName"></param>
        ''' <param name="UserID"></param>
        ''' <param name="PassWord"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampleMachineServer_GetList(ByVal ServerIP As String, ByVal DataBaseName As String, ByVal UserID As String, ByVal PassWord As String) As List(Of NmetalSamplePaceInfo)

            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleMachineServer_GetList")
            db.AddInParameter(dbComm, "@ServerIP", DbType.String, ServerIP)
            db.AddInParameter(dbComm, "@DataBaseName", DbType.String, DataBaseName)
            db.AddInParameter(dbComm, "@UserID", DbType.String, UserID)
            db.AddInParameter(dbComm, "@PassWord", DbType.String, PassWord)

            Dim FeatureList As New List(Of NmetalSamplePaceInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillNmetalSamplePaceType(reader))
                End While
                Return FeatureList
            End Using

        End Function



        Public Function NmetalSamplePace_GetOrdersID(ByVal soid As String) As Boolean
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSamplePace_GetOrdersID")
            db.AddInParameter(dbComm, "@SO_ID", DbType.String, soid)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                reader.Read()
                If reader.FieldCount > 1 Then
                    Return True
                Else
                    Return False
                End If
            End Using
        End Function

        Public Function NmetalSamplePace_Update(ByVal objinfo As NmetalSamplePaceInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSamplePace_Update")

                db.AddInParameter(dbComm, "SO_ID", DbType.String, objinfo.SO_ID)
                db.AddInParameter(dbComm, "SS_Edition", DbType.String, objinfo.SS_Edition)
                db.AddInParameter(dbComm, "PS_NO", DbType.String, objinfo.PS_NO)
                db.AddInParameter(dbComm, "SE_PaceDescribe", DbType.String, objinfo.SE_PaceDescribe)
                db.AddInParameter(dbComm, "State", DbType.String, objinfo.State)
                db.AddInParameter(dbComm, "SE_ModifyUserID", DbType.String, objinfo.SE_ModifyUserID)
                db.AddInParameter(dbComm, "SE_ModifyDate", DbType.DateTime, CDate(objinfo.SE_ModifyDate))
                db.AddInParameter(dbComm, "PM_M_Code", DbType.String, objinfo.PM_M_Code)
                db.AddInParameter(dbComm, "M_Code", DbType.String, objinfo.M_Code)
                db.AddInParameter(dbComm, "AutoID", DbType.String, objinfo.AutoID)

                db.AddInParameter(dbComm, "SE_Type", DbType.String, objinfo.SE_Type)
                db.AddInParameter(dbComm, "SE_Qty", DbType.Int32, objinfo.SE_Qty)
                db.AddInParameter(dbComm, "SE_AddDate", DbType.DateTime, CDate(objinfo.SE_AddDate))
                db.AddInParameter(dbComm, "SE_ID", DbType.String, objinfo.SE_ID)
                db.AddInParameter(dbComm, "SPID", DbType.String, objinfo.SPID)
                db.AddInParameter(dbComm, "SE_OutInType", DbType.String, objinfo.SE_OutInType)

                db.AddInParameter(dbComm, "SE_OutD_ID", DbType.String, objinfo.SE_OutD_ID)
                db.AddInParameter(dbComm, "SE_OutPS_NO", DbType.String, objinfo.SE_OutPS_NO)
                db.AddInParameter(dbComm, "SE_InD_ID", DbType.String, objinfo.SE_InD_ID)
                db.AddInParameter(dbComm, "SE_InPS_NO", DbType.String, objinfo.SE_InPS_NO)
                db.AddInParameter(dbComm, "SE_OutCardID", DbType.String, objinfo.SE_OutCardID)

                db.AddInParameter(dbComm, "SE_Remark", DbType.String, objinfo.SE_Remark)
                db.AddInParameter(dbComm, "SE_LoanID", DbType.String, objinfo.SE_LoanID)
                db.AddInParameter(dbComm, "SE_BorrowType", DbType.String, objinfo.SE_BorrowType)
                db.AddInParameter(dbComm, "SE_BarCodeType", DbType.String, objinfo.SE_BarCodeType)
                db.AddInParameter(dbComm, "SE_BorrowTime", DbType.Int16, objinfo.SE_BorrowTime)
                db.AddInParameter(dbComm, "OutWeighing", DbType.Decimal, objinfo.OutWeighing)
                db.AddInParameter(dbComm, "InWeighing", DbType.Decimal, objinfo.InWeighing)
                db.AddInParameter(dbComm, "SupplierID", DbType.String, objinfo.SupplierID)
                db.AddInParameter(dbComm, "SE_CodeType", DbType.Int16, objinfo.SE_CodeType)

                db.AddInParameter(dbComm, "@OutQtying", DbType.Int16, objinfo.OutQtying)
                db.AddInParameter(dbComm, "@InQtying", DbType.Int16, objinfo.InQtying)

                db.AddInParameter(dbComm, "SE_OutYCCardID", DbType.String, objinfo.SE_OutYCCardID)

                db.ExecuteNonQuery(dbComm)
                NmetalSamplePace_Update = True

            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSamplePace_Update = False
            End Try
        End Function

        Public Function NmetalSamplePace_AddDelLogin(ByVal objinfo As NmetalSamplePaceInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSamplePace_AddDelLogin")

                db.AddInParameter(dbComm, "@AutoID", DbType.String, objinfo.AutoID)
                db.AddInParameter(dbComm, "@SE_ID", DbType.String, objinfo.SE_ID)
                db.AddInParameter(dbComm, "@SE_DelUserID", DbType.String, objinfo.SE_AddUserID)

                db.ExecuteNonQuery(dbComm)
                NmetalSamplePace_AddDelLogin = True
            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSamplePace_AddDelLogin = False
            End Try
        End Function

        Public Function NmetalSamplePacePk_Update(ByVal objinfo As NmetalSamplePaceInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSamplePacePk_Update")

                db.AddInParameter(dbComm, "@AutoID", DbType.String, objinfo.AutoID)
                db.AddInParameter(dbComm, "@PK_Code_ID", DbType.String, objinfo.PK_Code_ID)
                db.AddInParameter(dbComm, "@SealCode_ID", DbType.String, objinfo.SealCode_ID)
                db.ExecuteNonQuery(dbComm)
                NmetalSamplePacePk_Update = True

            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSamplePacePk_Update = False
            End Try
        End Function

        Public Function NmetalSamplePace_Delete(ByVal AutoID As Decimal) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSamplePace_DeleteAutoID")
                db.AddInParameter(dbComm, "@AutoID", DbType.String, AutoID)
                db.ExecuteNonQuery(dbComm)
                NmetalSamplePace_Delete = True
            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSamplePace_Delete = False
            End Try
        End Function

        Public Function NmetalSamplePace_Delete(ByVal SO_ID As String, ByVal SS_Edition As String) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSamplePace_Delete")
                db.AddInParameter(dbComm, "@SO_ID", DbType.String, SO_ID)
                db.AddInParameter(dbComm, "@SS_Edition", DbType.String, SS_Edition)
                db.ExecuteNonQuery(dbComm)
                NmetalSamplePace_Delete = True
            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSamplePace_Delete = False
            End Try
        End Function

        Public Function NmetalSamplePace_DeleteSE_ID(ByVal SE_ID As String) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSamplePace_DeleteSE_ID")
                db.AddInParameter(dbComm, "@SE_ID", DbType.String, SE_ID)
                db.ExecuteNonQuery(dbComm)
                NmetalSamplePace_DeleteSE_ID = True
            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSamplePace_DeleteSE_ID = False
            End Try
        End Function

        Public Function NmetalSamplePace_Getlist(ByVal AutoID As String, ByVal SO_ID As String, ByVal SS_Edition As String, ByVal M_Code As String, ByVal PM_M_Code As String, ByVal StartDate As String, ByVal EndDate As String, ByVal SO_IDCheck As Boolean, ByVal SE_OutInType As String, ByVal SE_OutD_ID As String, ByVal SE_InD_ID As String, ByVal SE_Check As String, ByVal SE_InCheck As String, ByVal SE_AddUserID As String) As List(Of NmetalSamplePaceInfo)

            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSamplePace_Getlist")

            db.AddInParameter(dbComm, "@AutoID", DbType.String, AutoID)
            db.AddInParameter(dbComm, "@SO_ID", DbType.String, SO_ID)
            db.AddInParameter(dbComm, "@SS_Edition", DbType.String, SS_Edition)
            db.AddInParameter(dbComm, "@M_Code", DbType.String, M_Code)
            db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, PM_M_Code)
            db.AddInParameter(dbComm, "@StartDate", DbType.String, StartDate)
            db.AddInParameter(dbComm, "@EndDate", DbType.String, EndDate)
            db.AddInParameter(dbComm, "@SO_IDCheck", DbType.Boolean, SO_IDCheck)

            db.AddInParameter(dbComm, "@SE_OutInType", DbType.String, SE_OutInType)
            db.AddInParameter(dbComm, "@SE_OutD_ID", DbType.String, SE_OutD_ID)
            db.AddInParameter(dbComm, "@SE_InD_ID", DbType.String, SE_InD_ID)
            db.AddInParameter(dbComm, "@SE_Check", DbType.String, SE_Check)
            db.AddInParameter(dbComm, "@SE_InCheck", DbType.String, SE_InCheck)
            db.AddInParameter(dbComm, "@SE_AddUserID", DbType.String, SE_AddUserID)

            Dim FeatureList As New List(Of NmetalSamplePaceInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillNmetalSamplePaceType(reader))
                End While
                Return FeatureList
            End Using
        End Function

        ''' <summary>
        ''' 查詢樣板借出
        ''' 2014-04-10
        ''' 姚    駿
        ''' </summary>
        ''' <param name="AutoID"></param>
        ''' <param name="SO_ID"></param>
        ''' <param name="SS_Edition"></param>
        ''' <param name="M_Code"></param>
        ''' <param name="PM_M_Code"></param>
        ''' <param name="StartDate"></param>
        ''' <param name="EndDate"></param>
        ''' <param name="SO_IDCheck"></param>
        ''' <param name="SE_OutInType"></param>
        ''' <param name="SE_OutD_ID"></param>
        ''' <param name="SE_InD_ID"></param>
        ''' <param name="SE_Check"></param>
        ''' <param name="SE_InCheck"></param>
        ''' <param name="SE_AddUserID"></param>
        ''' <param name="SE_Type"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSamplePace_GetlistByType(ByVal AutoID As String, ByVal SO_ID As String, ByVal SS_Edition As String, ByVal M_Code As String, ByVal PM_M_Code As String, ByVal StartDate As String, ByVal EndDate As String, ByVal SO_IDCheck As Boolean, ByVal SE_OutInType As String, ByVal SE_OutD_ID As String, ByVal SE_InD_ID As String, ByVal SE_Check As String, ByVal SE_InCheck As String, ByVal SE_AddUserID As String, ByVal SE_Type As String, ByVal SE_ID As String) As List(Of NmetalSamplePaceInfo)

            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSamplePace_GetlistByType")

            db.AddInParameter(dbComm, "@AutoID", DbType.String, AutoID)
            db.AddInParameter(dbComm, "@SO_ID", DbType.String, SO_ID)
            db.AddInParameter(dbComm, "@SS_Edition", DbType.String, SS_Edition)
            db.AddInParameter(dbComm, "@M_Code", DbType.String, M_Code)
            db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, PM_M_Code)
            db.AddInParameter(dbComm, "@StartDate", DbType.String, StartDate)
            db.AddInParameter(dbComm, "@EndDate", DbType.String, EndDate)
            db.AddInParameter(dbComm, "@SO_IDCheck", DbType.Boolean, SO_IDCheck)

            db.AddInParameter(dbComm, "@SE_OutInType", DbType.String, SE_OutInType)
            db.AddInParameter(dbComm, "@SE_OutD_ID", DbType.String, SE_OutD_ID)
            db.AddInParameter(dbComm, "@SE_InD_ID", DbType.String, SE_InD_ID)
            db.AddInParameter(dbComm, "@SE_Check", DbType.String, SE_Check)
            db.AddInParameter(dbComm, "@SE_InCheck", DbType.String, SE_InCheck)
            db.AddInParameter(dbComm, "@SE_AddUserID", DbType.String, SE_AddUserID)
            db.AddInParameter(dbComm, "@SE_Type", DbType.String, SE_Type)
            db.AddInParameter(dbComm, "@SE_ID", DbType.String, SE_ID)

            Dim FeatureList As New List(Of NmetalSamplePaceInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillNmetalSamplePaceType(reader))
                End While
                Return FeatureList
            End Using
        End Function


        Public Function NmetalSamplePace_Getlist2(ByVal AutoID As String, ByVal SO_ID As String, ByVal SS_Edition As String, ByVal M_Code As String, ByVal PM_M_Code As String, ByVal StartDate As String, ByVal EndDate As String, ByVal SO_IDCheck As Boolean, ByVal SE_OutInType As String, ByVal SE_OutD_ID As String, ByVal SE_InD_ID As String, ByVal SE_Check As String, ByVal SE_InCheck As String) As List(Of NmetalSamplePaceInfo)

            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSamplePace_Getlist2")

            db.AddInParameter(dbComm, "@AutoID", DbType.String, AutoID)
            db.AddInParameter(dbComm, "@SO_ID", DbType.String, SO_ID)
            db.AddInParameter(dbComm, "@SS_Edition", DbType.String, SS_Edition)
            db.AddInParameter(dbComm, "@M_Code", DbType.String, M_Code)
            db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, PM_M_Code)
            db.AddInParameter(dbComm, "@StartDate", DbType.String, StartDate)
            db.AddInParameter(dbComm, "@EndDate", DbType.String, EndDate)
            db.AddInParameter(dbComm, "@SO_IDCheck", DbType.Boolean, SO_IDCheck)

            db.AddInParameter(dbComm, "@SE_OutInType", DbType.String, SE_OutInType)
            db.AddInParameter(dbComm, "@SE_OutD_ID", DbType.String, SE_OutD_ID)
            db.AddInParameter(dbComm, "@SE_InD_ID", DbType.String, SE_InD_ID)
            db.AddInParameter(dbComm, "@SE_Check", DbType.String, SE_Check)
            db.AddInParameter(dbComm, "@SE_InCheck", DbType.String, SE_InCheck)

            Dim FeatureList As New List(Of NmetalSamplePaceInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillNmetalSamplePaceType(reader))
                End While
                Return FeatureList
            End Using
        End Function


        Public Function NmetalSamplePace_Getlist1(ByVal AutoID As String, ByVal SO_ID As String, ByVal SS_Edition As String, ByVal M_Code As String, ByVal PM_M_Code As String, ByVal StartDate As String, ByVal EndDate As String, ByVal SO_IDCheck As String, ByVal SE_ID As String, ByVal SE_Check As String) As List(Of NmetalSamplePaceInfo)

            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSamplePace_Getlist1")

            db.AddInParameter(dbComm, "@AutoID", DbType.String, AutoID)
            db.AddInParameter(dbComm, "@SO_ID", DbType.String, SO_ID)
            db.AddInParameter(dbComm, "@SS_Edition", DbType.String, SS_Edition)
            db.AddInParameter(dbComm, "@M_Code", DbType.String, M_Code)
            db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, PM_M_Code)
            db.AddInParameter(dbComm, "@StartDate", DbType.String, StartDate)
            db.AddInParameter(dbComm, "@EndDate", DbType.String, EndDate)
            db.AddInParameter(dbComm, "@SO_IDCheck", DbType.String, SO_IDCheck)

            db.AddInParameter(dbComm, "@SE_ID", DbType.String, SE_ID)
            db.AddInParameter(dbComm, "@SE_Check", DbType.String, SE_Check)

            Dim FeatureList As New List(Of NmetalSamplePaceInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillNmetalSamplePaceType(reader))
                End While
                Return FeatureList
            End Using
        End Function

        Public Function NmetalSamplePace_Getlist3(ByVal PS_NO As String) As List(Of NmetalSamplePaceInfo)

            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSamplePace_Getlist3")

            db.AddInParameter(dbComm, "@PS_NO", DbType.String, PS_NO)

            Dim FeatureList As New List(Of NmetalSamplePaceInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillNmetalSamplePaceType(reader))
                End While
                Return FeatureList
            End Using
        End Function

        Public Function NmetalSamplePace_Get(ByVal SP_ID As String) As NmetalSamplePaceInfo
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSamplePace_Get")
            db.AddInParameter(dbComm, "@PS_NO", DbType.String, SP_ID)
            Dim FeatureList As New NmetalSamplePaceInfo
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.PS_NO = reader("PS_NO").ToString
                End While
                Return FeatureList
            End Using
        End Function

        Friend Function FillNmetalSamplePaceType(ByVal reader As IDataReader) As NmetalSamplePaceInfo
            On Error Resume Next

            Dim objInfo As New NmetalSamplePaceInfo

            objInfo.SO_ID = reader("SO_ID").ToString
            objInfo.SS_Edition = reader("SS_Edition").ToString
            objInfo.PS_NO = reader("PS_NO").ToString
            objInfo.SE_AddDate = CDate(reader("SE_AddDate").ToString)
            objInfo.SE_PaceDescribe = reader("SE_PaceDescribe").ToString
            objInfo.State = reader("State").ToString
            objInfo.SE_AddUserID = reader("SE_AddUserID").ToString
            objInfo.SE_AddDate = CDate(reader("SE_AddDate").ToString)
            objInfo.SE_ModifyUserID = reader("SE_ModifyUserID").ToString
            objInfo.SE_ModifyDate = CDate(reader("SE_ModifyDate").ToString)
            objInfo.PM_M_Code = reader("PM_M_Code").ToString
            objInfo.M_Code = reader("M_Code").ToString
            objInfo.AutoID = CDbl(reader("AutoID").ToString)
            objInfo.M_Name = reader("M_Name").ToString
            objInfo.SE_AddUserName = reader("SE_AddUserName").ToString
            objInfo.PS_Name = reader("PS_Name").ToString

            objInfo.SE_Type = reader("SE_Type").ToString
            objInfo.SE_TypeName = reader("SE_TypeName").ToString

            objInfo.SE_Qty = reader("SE_Qty")

            objInfo.SE_Check = reader("SE_Check")
            objInfo.SE_CheckAction = reader("SE_CheckAction").ToString
            objInfo.SE_CheckActionName = reader("SE_CheckActionName").ToString
            objInfo.SE_CheckDate = CDate(reader("SE_CheckDate").ToString)
            objInfo.SE_ID = reader("SE_ID").ToString

            objInfo.SE_OutInType = reader("SE_OutInType").ToString
            objInfo.SPID = reader("SPID").ToString
            objInfo.Code_ID = reader("Code_ID").ToString
            objInfo.Qty = CInt(reader("Qty").ToString)
            objInfo.ClientBarcode = reader("ClientBarcode").ToString

            objInfo.SE_OutD_ID = reader("SE_OutD_ID").ToString
            objInfo.SE_OutPS_NO = reader("SE_OutPS_NO").ToString
            objInfo.SE_OutTime = reader("SE_OutTime")
            objInfo.SE_InD_ID = reader("SE_InD_ID").ToString
            objInfo.SE_InPS_NO = reader("SE_InPS_NO").ToString
            objInfo.SE_InTime = reader("SE_InTime")

            objInfo.SE_InD_Dep = reader("SE_InD_Dep").ToString
            objInfo.SE_OutD_Dep = reader("SE_OutD_Dep").ToString
            objInfo.SE_OutPS_Name = reader("SE_OutPS_Name").ToString
            objInfo.SE_InPS_Name = reader("SE_InPS_Name").ToString

            objInfo.SE_InCardID = reader("SE_InCardID").ToString
            objInfo.SE_OutCardID = reader("SE_OutCardID").ToString
            objInfo.SE_InCardIDName = reader("SE_InCardIDName").ToString
            objInfo.SE_OutCardIDName = reader("SE_OutCardIDName").ToString


            objInfo.SE_InCheck = reader("SE_InCheck")
            objInfo.SE_OutVisible = reader("SE_OutVisible")
            objInfo.SE_InVisible = reader("SE_InVisible")


            objInfo.SE_OutEnabled = reader("SE_OutEnabled")
            objInfo.SE_InEnabled = reader("SE_InEnabled")
            objInfo.SE_OutPSEnabled = reader("SE_OutPSEnabled")
            objInfo.SE_InPSEnabled = reader("SE_InPSEnabled")
            objInfo.SE_InCheckBarcode = reader("SE_InCheckBarcode")
            objInfo.StatusType = reader("StatusType").ToString


            objInfo.SE_IncheckAction = reader("SE_IncheckAction").ToString
            objInfo.SE_InCheckActionName = reader("SE_InCheckActionName").ToString

            objInfo.InCheck = reader("InCheck")
            objInfo.InCheckDate = reader("InCheckDate").ToString
            objInfo.SO_SampleID = reader("SO_SampleID").ToString

            objInfo.SE_Remark = reader("SE_Remark").ToString
            objInfo.SE_LoanID = reader("SE_LoanID").ToString
            objInfo.BarCodeCount = reader("BarCodeCount")

            objInfo.SE_BorrowType = reader("SE_BorrowType").ToString
            objInfo.MaterialTypeName = reader("MaterialTypeName").ToString
            objInfo.PK_Code_ID = reader("PK_Code_ID").ToString
            objInfo.SealCode_ID = reader("SealCode_ID").ToString
            objInfo.SE_BarcodeLink = reader("SE_BarcodeLink").ToString
            objInfo.SE_BarCodeType = reader("SE_BarCodeType").ToString
            objInfo.SE_BarCodeAuto = reader("SE_BarCodeAuto")
            objInfo.SE_EditInD_ID = reader("SE_EditInD_ID")
            objInfo.SE_BorrowTime = reader("SE_BorrowTime")
            objInfo.OutWeighing = reader("OutWeighing")
            objInfo.InWeighing = reader("InWeighing")
            objInfo.SupplierID = reader("SupplierID")
            objInfo.SE_CodeType = reader("SE_CodeType")
            '2014-05-15  姚駿
            objInfo.ServerIP = reader("ServerIP")
            objInfo.DataBaseName = reader("DataBaseName")
            objInfo.UserID = reader("UserID")
            objInfo.PassWord = reader("PassWord")

            objInfo.SE_StatusTypeIsNull = reader("SE_StatusTypeIsNull")
            objInfo.SE_IsRweight = reader("SE_IsRweight")

            objInfo.OutQty = reader("OutQty")
            objInfo.OutWeight = reader("OutWeight")
            objInfo.InQty = reader("InQty")
            objInfo.InWeight = reader("InWeight")
            objInfo.InWeighingEnd = reader("InWeighingEnd")
            objInfo.OutWeighingEnd = reader("OutWeighingEnd")

            objInfo.OutQtying = reader("OutQtying")
            objInfo.InQtying = reader("InQtying")
            objInfo.OutQtyingEnd = reader("OutQtyingEnd")
            objInfo.InQtyingEnd = reader("InQtyingEnd")

            objInfo.SE_IsKailiao = reader("SE_IsKailiao")
            objInfo.SE_IsComplete = reader("SE_IsComplete")

            objInfo.OutEndQty = reader("OutEndQty")
            objInfo.OutEndWeight = reader("OutEndWeight")
            objInfo.OutWightChaYi = objInfo.OutEndWeight - objInfo.OutWeight

            '----------------------------------------------------------------
            objInfo.SE_OutYCCardID = reader("SE_OutYCCardID").ToString  ''异常相关
            objInfo.SE_InYCCardID = reader("SE_InYCCardID").ToString
            objInfo.OutDwonRate = reader("OutDwonRate")
            objInfo.OutUpRate = reader("OutUpRate")
            objInfo.InDwonRate = reader("InDwonRate")
            objInfo.InUpRate = reader("InUpRate")



            Return objInfo
        End Function

        ''' <summary>
        ''' 載入類理
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSamplePaceType_Getlist(ByVal SE_Type As String) As List(Of NmetalSamplePaceInfo)

            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSamplePaceType_Getlist")
            db.AddInParameter(dbComm, "@SE_Type", DbType.String, SE_Type)

            Dim FeatureList As New List(Of NmetalSamplePaceInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillNmetalSamplePaceType(reader))
                End While
                Return FeatureList
            End Using
        End Function

        Public Function NmetalSamplePaceType_GetlistIn(ByVal inSE_Type As String) As List(Of NmetalSamplePaceInfo)

            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSamplePaceType_GetlistIn")
            db.AddInParameter(dbComm, "@SE_Type", DbType.String, inSE_Type)

            Dim FeatureList As New List(Of NmetalSamplePaceInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillNmetalSamplePaceType(reader))
                End While
                Return FeatureList
            End Using
        End Function

        Public Function NmetalSamplePace_UpdateInWeighing(ByVal objinfo As NmetalSamplePaceInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSamplePace_UpdateInWeighing")

                db.AddInParameter(dbComm, "@SE_ID", DbType.String, objinfo.SE_ID)
                db.AddInParameter(dbComm, "@InWeighing", DbType.Decimal, objinfo.InWeighing)

                db.ExecuteNonQuery(dbComm)
                NmetalSamplePace_UpdateInWeighing = True

            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSamplePace_UpdateInWeighing = False
            End Try
        End Function

        Public Function NmetalSamplePace_UpdateCheck(ByVal objinfo As NmetalSamplePaceInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSamplePace_UpdateCheck")

                db.AddInParameter(dbComm, "@SE_ID", DbType.String, objinfo.SE_ID)
                db.AddInParameter(dbComm, "@SE_Check", DbType.Boolean, objinfo.SE_Check)
                db.AddInParameter(dbComm, "@SE_CheckDate", DbType.Date, objinfo.SE_CheckDate)
                db.AddInParameter(dbComm, "@SE_CheckAction", DbType.String, objinfo.SE_CheckAction)

                db.AddInParameter(dbComm, "@OutDwonRate", DbType.Decimal, objinfo.OutDwonRate)
                db.AddInParameter(dbComm, "@OutUpRate", DbType.Decimal, objinfo.OutUpRate)


                db.ExecuteNonQuery(dbComm)
                NmetalSamplePace_UpdateCheck = True

            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSamplePace_UpdateCheck = False
            End Try
        End Function

        Public Function NmetalSamplePace_UpdateInCheck(ByVal objinfo As NmetalSamplePaceInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSamplePace_UpdateInCheck")

                db.AddInParameter(dbComm, "@SE_ID", DbType.String, objinfo.SE_ID)
                db.AddInParameter(dbComm, "@SE_InCheck", DbType.Boolean, objinfo.SE_InCheck)
                db.AddInParameter(dbComm, "@SE_InCheckAction", DbType.String, objinfo.SE_IncheckAction)
                db.AddInParameter(dbComm, "@SE_InCardID", DbType.String, objinfo.SE_InCardID)

                db.AddInParameter(dbComm, "@InQtying", DbType.Decimal, objinfo.InQtying)
                db.AddInParameter(dbComm, "@InWeighing", DbType.Decimal, objinfo.InWeighing)

                db.AddInParameter(dbComm, "@SE_InYCCardID", DbType.String, objinfo.SE_InYCCardID)
                db.AddInParameter(dbComm, "@InDwonRate", DbType.Decimal, objinfo.InDwonRate)
                db.AddInParameter(dbComm, "@InUpRate", DbType.Decimal, objinfo.InUpRate)

                db.ExecuteNonQuery(dbComm)
                NmetalSamplePace_UpdateInCheck = True

            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSamplePace_UpdateInCheck = False
            End Try
        End Function

        Public Function NmetalSamplePace_UpdateInCheckEnd(ByVal objinfo As NmetalSamplePaceInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSamplePace_UpdateInCheckEnd")

                db.AddInParameter(dbComm, "@SE_ID", DbType.String, objinfo.SE_ID)
                '2014-07-03
                db.AddInParameter(dbComm, "@OutWeighingEnd", DbType.Decimal, objinfo.OutWeighingEnd)
                db.AddInParameter(dbComm, "@InWeighingEnd", DbType.Decimal, objinfo.InWeighingEnd)
                db.AddInParameter(dbComm, "@OutQtyingEnd", DbType.Int16, objinfo.OutQtyingEnd)
                db.AddInParameter(dbComm, "@InQtyingEnd", DbType.Int16, objinfo.InQtyingEnd)

                db.ExecuteNonQuery(dbComm)
                NmetalSamplePace_UpdateInCheckEnd = True

            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSamplePace_UpdateInCheckEnd = False
            End Try
        End Function

        Public Function NmetalSamplePace_GetID(ByVal SE_ID As String) As NmetalSamplePaceInfo
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSamplePace_GetID")
            db.AddInParameter(dbComm, "@SE_ID", DbType.String, SE_ID)
            Dim FeatureList As New NmetalSamplePaceInfo
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.SE_ID = reader("SE_ID").ToString
                End While
                Return FeatureList
            End Using
        End Function

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

#Region "半成品條碼"
        Public Function NmetalSamplePaceBarCode_Add(ByVal objinfo As NmetalSamplePaceInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSamplePaceBarCode_Add")

                db.AddInParameter(dbComm, "@SPID", DbType.String, objinfo.SPID)
                db.AddInParameter(dbComm, "@Code_ID", DbType.String, objinfo.Code_ID)

                db.AddInParameter(dbComm, "@SE_ID", DbType.String, objinfo.SE_ID)
                db.AddInParameter(dbComm, "@SE_AddDate", DbType.DateTime, objinfo.SE_AddDate)
                db.AddInParameter(dbComm, "@SE_AddUserID", DbType.String, objinfo.SE_AddUserID)
                db.AddInParameter(dbComm, "@ClientBarcode", DbType.String, objinfo.ClientBarcode)
                db.AddInParameter(dbComm, "@SE_Type", DbType.String, objinfo.SE_Type)

                db.AddInParameter(dbComm, "@OutQty", DbType.Int32, objinfo.OutQty)
                db.AddInParameter(dbComm, "@OutWeight", DbType.Decimal, objinfo.OutWeight)
                db.AddInParameter(dbComm, "@InQty", DbType.Int32, objinfo.InQty)
                db.AddInParameter(dbComm, "@InWeight", DbType.Decimal, objinfo.InWeight)

                db.ExecuteNonQuery(dbComm)
                NmetalSamplePaceBarCode_Add = True
            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSamplePaceBarCode_Add = False
            End Try

        End Function

        Public Function NmetalSamplePaceBarCode_Update(ByVal objinfo As NmetalSamplePaceInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSamplePaceBarCode_Update")

                db.AddInParameter(dbComm, "@AutoID", DbType.String, objinfo.AutoID)
                db.AddInParameter(dbComm, "@InCheck", DbType.String, objinfo.InCheck)
                db.AddInParameter(dbComm, "@InCheckDate", DbType.DateTime, objinfo.InCheckDate)

                db.AddInParameter(dbComm, "@InQty", DbType.Int16, objinfo.InQty)
                db.AddInParameter(dbComm, "@InWeight", DbType.Decimal, objinfo.InWeight)

                db.AddInParameter(dbComm, "@OutEndQty", DbType.Int16, objinfo.OutEndQty)
                db.AddInParameter(dbComm, "@OutEndWeight", DbType.Decimal, objinfo.OutEndWeight)

                db.ExecuteNonQuery(dbComm)
                NmetalSamplePaceBarCode_Update = True
            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSamplePaceBarCode_Update = False
            End Try
        End Function
        Public Function NmetalSamplePaceBarCode_UpdateA(ByVal objinfo As NmetalSamplePaceInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSamplePaceBarCode_UpdateA")

                db.AddInParameter(dbComm, "@SE_ID", DbType.String, objinfo.SE_ID)
                db.AddInParameter(dbComm, "@Code_ID", DbType.String, objinfo.Code_ID)
                db.AddInParameter(dbComm, "@SE_Type", DbType.String, objinfo.SE_Type)

                db.ExecuteNonQuery(dbComm)
                NmetalSamplePaceBarCode_UpdateA = True
            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSamplePaceBarCode_UpdateA = False
            End Try
        End Function


        '更新节余数
   
        Public Function NmetalSamplePaceBarCode_UpdateEndBatch(ByVal SE_ID As String, ByVal InputType As Boolean) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSamplePaceBarCode_UpdateEndBatch")

                db.AddInParameter(dbComm, "@SE_ID", DbType.String, SE_ID)
                db.AddInParameter(dbComm, "@InputType", DbType.Boolean, InputType)


                db.ExecuteNonQuery(dbComm)
                NmetalSamplePaceBarCode_UpdateEndBatch = True
            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSamplePaceBarCode_UpdateEndBatch = False
            End Try
        End Function

        Public Function NmetalSamplePaceBarCode_Delete(ByVal AutoID As String, ByVal SE_ID As String, ByVal Code_ID As String) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSamplePaceBarCode_Delete")

                db.AddInParameter(dbComm, "@AutoID", DbType.String, AutoID)
                db.AddInParameter(dbComm, "@SE_ID", DbType.String, SE_ID)
                db.AddInParameter(dbComm, "@Code_ID", DbType.String, Code_ID)


                db.ExecuteNonQuery(dbComm)
                NmetalSamplePaceBarCode_Delete = True
            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSamplePaceBarCode_Delete = False
            End Try
        End Function

        Public Function NmetalSamplePaceBarCode_Getlist(ByVal SPID As String, ByVal Code_ID As String, ByVal SE_ID As String) As List(Of NmetalSamplePaceInfo)

            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSamplePaceBarCode_Getlist")

            db.AddInParameter(dbComm, "@SPID", DbType.String, SPID)
            db.AddInParameter(dbComm, "@Code_ID", DbType.String, Code_ID)
            db.AddInParameter(dbComm, "@SE_ID", DbType.String, SE_ID)

            Dim FeatureList As New List(Of NmetalSamplePaceInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillNmetalSamplePaceType(reader))
                End While
                Return FeatureList
            End Using
        End Function
        Public Function NmetalSamplePaceBarCode_Getlist2(ByVal Code_ID As String, ByVal SE_ID As String) As List(Of NmetalSamplePaceInfo)

            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSamplePaceBarCode_Getlist2")

            db.AddInParameter(dbComm, "@Code_ID", DbType.String, Code_ID)
            db.AddInParameter(dbComm, "@SE_ID", DbType.String, SE_ID)

            Dim FeatureList As New List(Of NmetalSamplePaceInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillNmetalSamplePaceType(reader))
                End While
                Return FeatureList
            End Using
        End Function

        Public Function NmetalSamplePace_GetIDA(ByVal SPID As String) As NmetalSamplePaceInfo
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSamplePace_GetIDA")
            db.AddInParameter(dbComm, "@SPID", DbType.String, SPID)
            Dim FeatureList As New NmetalSamplePaceInfo
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.SPID = reader("SPID").ToString
                End While
                Return FeatureList
            End Using
        End Function

      
#End Region

#Region "報警-在指定時間內未收料確認時提示"
        Public Function NmetalSamplePace_GetlistWarning(ByVal SE_InD_ID As String, ByVal SE_OutD_ID As String) As List(Of NmetalSamplePaceInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSamplePace_GetlistWarning")

            db.AddInParameter(dbComm, "@SE_InD_ID", DbType.String, SE_InD_ID)
            db.AddInParameter(dbComm, "@SE_OutD_ID", DbType.String, SE_OutD_ID)

            Dim FeatureList As New List(Of NmetalSamplePaceInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillNmetalSamplePaceType(reader))
                End While
                Return FeatureList
            End Using
        End Function
#End Region


    End Class

End Namespace


