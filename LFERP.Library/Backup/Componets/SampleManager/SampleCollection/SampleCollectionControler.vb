Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.Data.Sql

Namespace LFERP.Library.SampleManager.SampleCollection
    Public Class SampleCollectionControler
        Public Function SampleCollection_Add(ByVal objinfo As SampleCollectionInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleCollection_Add")
                dbComm.CommandTimeout = 0
                db.AddInParameter(dbComm, "@Code_ID", DbType.String, objinfo.Code_ID)
                db.AddInParameter(dbComm, "@Qty", DbType.Int16, objinfo.Qty)
                db.AddInParameter(dbComm, "@StatusType", DbType.String, objinfo.StatusType)
                db.AddInParameter(dbComm, "@Remark", DbType.String, objinfo.Remark)
                db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, objinfo.PM_M_Code)
                db.AddInParameter(dbComm, "@AddUserID", DbType.String, objinfo.AddUserID)
                db.AddInParameter(dbComm, "@AddDate", DbType.Date, objinfo.AddDate)

                db.AddInParameter(dbComm, "@PM_Type", DbType.String, objinfo.PM_Type)
                db.AddInParameter(dbComm, "@SO_ID", DbType.String, objinfo.SO_ID)
                db.AddInParameter(dbComm, "@SP_ID", DbType.String, objinfo.SP_ID)
                db.AddInParameter(dbComm, "@SS_Edition", DbType.String, objinfo.SS_Edition)
                db.AddInParameter(dbComm, "@BarcodeType", DbType.String, objinfo.BarcodeType)
                db.AddInParameter(dbComm, "@D_ID", DbType.String, objinfo.D_ID)
                db.AddInParameter(dbComm, "@BitAuto", DbType.Boolean, objinfo.BitAuto)
                db.ExecuteNonQuery(dbComm)
                SampleCollection_Add = True
            Catch ex As Exception
                MsgBox(ex.Message)
                SampleCollection_Add = False
            End Try
        End Function

        Public Function SampleCollection_Update(ByVal objinfo As SampleCollectionInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleCollection_Update")
                dbComm.CommandTimeout = 0
                db.AddInParameter(dbComm, "@Remark", DbType.String, objinfo.Remark)
                db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, objinfo.PM_M_Code)
                db.AddInParameter(dbComm, "@AddUserID", DbType.String, objinfo.AddUserID)
                db.AddInParameter(dbComm, "@AddDate", DbType.Date, objinfo.AddDate)
                db.AddInParameter(dbComm, "@ModifyUserID", DbType.String, objinfo.ModifyUserID)
                db.AddInParameter(dbComm, "@ModifyDate", DbType.DateTime, objinfo.ModifyDate)
                db.AddInParameter(dbComm, "@Code_ID", DbType.String, objinfo.Code_ID)

                db.AddInParameter(dbComm, "@PM_Type", DbType.String, objinfo.PM_Type)
                db.AddInParameter(dbComm, "@SO_ID", DbType.String, objinfo.SO_ID)
                db.AddInParameter(dbComm, "@SP_ID", DbType.String, objinfo.SP_ID)
                db.AddInParameter(dbComm, "@SS_Edition", DbType.String, objinfo.SS_Edition)
                db.AddInParameter(dbComm, "@BitAuto", DbType.Boolean, objinfo.BitAuto)

                db.ExecuteNonQuery(dbComm)
                SampleCollection_Update = True

            Catch ex As Exception
                MsgBox(ex.Message)
                SampleCollection_Update = False
            End Try
        End Function


        Public Function SampleCollection_Delete(ByVal Code_ID As String, ByVal AutoID As String) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleCollection_Delete")
                dbComm.CommandTimeout = 0
                db.AddInParameter(dbComm, "@Code_ID", DbType.String, Code_ID)
                db.AddInParameter(dbComm, "@AutoID", DbType.String, AutoID)
                db.ExecuteNonQuery(dbComm)
                SampleCollection_Delete = True
            Catch ex As Exception
                MsgBox(ex.Message)
                SampleCollection_Delete = False
            End Try
        End Function


        Public Function SampleCollection_DCGetlist(ByVal StatusType As String, ByVal Code_ID As String, ByVal AutoID As String, ByVal SO_ID As String, ByVal SS_Edition As String, ByVal SP_ID As String, ByVal ReportEmpty As Boolean, ByVal ClientBarcode As String, ByVal PM_M_Code As String, ByVal BarcodeType As String, ByVal D_ID As String, ByVal StartDate As String, ByVal EndDate As String, ByVal AddUserID As String, ByVal BitAuto As String) As DataTable
            Try
                Dim ds As New DataSet
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleCollection_Getlist")
                dbComm.CommandTimeout = 0
                db.AddInParameter(dbComm, "@StatusType", DbType.String, StatusType)
                db.AddInParameter(dbComm, "@Code_ID", DbType.String, Code_ID)
                db.AddInParameter(dbComm, "@AutoID", DbType.String, AutoID)

                db.AddInParameter(dbComm, "@SO_ID", DbType.String, SO_ID)
                db.AddInParameter(dbComm, "@SP_ID", DbType.String, SP_ID)
                db.AddInParameter(dbComm, "@SS_Edition", DbType.String, SS_Edition)
                db.AddInParameter(dbComm, "@ClientBarcode", DbType.String, ClientBarcode)
                db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, PM_M_Code)
                db.AddInParameter(dbComm, "@BarcodeType", DbType.String, BarcodeType)
                db.AddInParameter(dbComm, "@D_ID", DbType.String, D_ID)

                db.AddInParameter(dbComm, "@StartDate", DbType.String, StartDate)
                db.AddInParameter(dbComm, "@EndDate", DbType.String, EndDate)
                db.AddInParameter(dbComm, "@AddUserID", DbType.String, AddUserID)
                db.AddInParameter(dbComm, "@BitAuto", DbType.Boolean, BitAuto)

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

        Public Function SampleCollection_Getlist(ByVal StatusType As String, ByVal Code_ID As String, ByVal AutoID As String, ByVal SO_ID As String, ByVal SS_Edition As String, ByVal SP_ID As String, ByVal ReportEmpty As Boolean, ByVal ClientBarcode As String, ByVal PM_M_Code As String, ByVal BarcodeType As String, ByVal D_ID As String, ByVal StartDate As String, ByVal EndDate As String, ByVal AddUserID As String, ByVal BitAuto As String) As List(Of SampleCollectionInfo)

            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleCollection_Getlist")
            'dbComm.CommandTimeout = 0
            db.AddInParameter(dbComm, "@StatusType", DbType.String, StatusType)
            db.AddInParameter(dbComm, "@Code_ID", DbType.String, Code_ID)
            db.AddInParameter(dbComm, "@AutoID", DbType.String, AutoID)

            db.AddInParameter(dbComm, "@SO_ID", DbType.String, SO_ID)
            db.AddInParameter(dbComm, "@SP_ID", DbType.String, SP_ID)
            db.AddInParameter(dbComm, "@SS_Edition", DbType.String, SS_Edition)
            db.AddInParameter(dbComm, "@ClientBarcode", DbType.String, ClientBarcode)
            db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, PM_M_Code)
            db.AddInParameter(dbComm, "@BarcodeType", DbType.String, BarcodeType)
            db.AddInParameter(dbComm, "@D_ID", DbType.String, D_ID)

            db.AddInParameter(dbComm, "@StartDate", DbType.String, StartDate)
            db.AddInParameter(dbComm, "@EndDate", DbType.String, EndDate)
            db.AddInParameter(dbComm, "@AddUserID", DbType.String, AddUserID)
            db.AddInParameter(dbComm, "@BitAuto", DbType.Boolean, BitAuto)


            Dim FeatureList As New List(Of SampleCollectionInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillSampleCollectionType(reader))
                End While
                If FeatureList.Count <= 0 And ReportEmpty Then
                    FeatureList.Add(New SampleCollectionInfo())
                End If
                Return FeatureList
            End Using
        End Function

        ''' <summary>
        ''' 條碼採集查詢
        ''' 2014-04-21
        ''' 姚     駿
        ''' </summary>
        ''' <param name="D_ID"></param>
        ''' <param name="StatusType"></param>
        ''' <param name="SO_SampleID"></param>
        ''' <param name="PM_M_Code"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SampleCollection_GetListByNewCondition(ByVal D_ID As String, ByVal StatusType As String, ByVal SO_SampleID As String, ByVal PM_M_Code As String) As List(Of SampleCollectionInfo)

            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleCollection_GetListByNewCondition")
            dbComm.CommandTimeout = 0
            db.AddInParameter(dbComm, "@D_ID", DbType.String, D_ID)
            db.AddInParameter(dbComm, "@StatusType", DbType.String, StatusType)
            db.AddInParameter(dbComm, "@SO_SampleID", DbType.String, SO_SampleID)
            db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, PM_M_Code)

            Dim FeatureList As New List(Of SampleCollectionInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillSampleCollectionType(reader))
                End While
                Return FeatureList
            End Using
        End Function


        ''' <summary>
        ''' 2014-04-18
        ''' 姚   駿
        ''' 存放循環報警-查詢部門超時的條碼明細
        ''' </summary>
        ''' <param name="D_ID"></param>
        ''' <param name="D_Second"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SampleCollection_GetListByStore(ByVal D_ID As String, ByVal D_Second As String) As List(Of SampleCollectionInfo)

            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleCollection_GetListByStore")
            dbComm.CommandTimeout = 0
            db.AddInParameter(dbComm, "@D_ID", DbType.String, D_ID)
            db.AddInParameter(dbComm, "@D_Second", DbType.String, D_Second)

            Dim FeatureList As New List(Of SampleCollectionInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillSampleCollectionType(reader))
                End While
                Return FeatureList
            End Using
        End Function


        Public Function SampleCollection_GetID(ByVal Code_ID As String) As Boolean
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleCollection_GetID")
            dbComm.CommandTimeout = 0
            db.AddInParameter(dbComm, "@Code_ID", DbType.String, Code_ID)

            Dim StrCode_ID As String = String.Empty
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    StrCode_ID = reader("Code_ID").ToString
                End While
                If StrCode_ID = String.Empty Then
                    Return False
                Else
                    Return True
                End If
            End Using
        End Function

        'Public Function SampleCollectionType_GetID(ByVal Code_ID As String) As String
        '    Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
        '    Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleCollectionType_GetID")

        '    db.AddInParameter(dbComm, "@Code_ID", DbType.String, Code_ID)

        '    Dim StrCode_ID As String = String.Empty
        '    Using reader As IDataReader = db.ExecuteReader(dbComm)
        '        While reader.Read
        '            StrCode_ID = reader("StatusType").ToString
        '        End While
        '    End Using
        '    SampleCollectionType_GetID = StrCode_ID
        'End Function


        Public Function SampleCollection_UpdateA(ByVal Code_ID As String, ByVal StatusType As String) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleCollection_UpdateA")
                dbComm.CommandTimeout = 0
                db.AddInParameter(dbComm, "@StatusType", DbType.String, StatusType)
                db.AddInParameter(dbComm, "@Code_ID", DbType.String, Code_ID)
                db.ExecuteNonQuery(dbComm)

                SampleCollection_UpdateA = True
            Catch ex As Exception
                MsgBox(ex.Message)
                SampleCollection_UpdateA = False
            End Try
        End Function

        Public Function SampleCollection_UpdateB(ByVal Code_ID As String, ByVal SP_ID As String) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleCollection_UpdateB")
                dbComm.CommandTimeout = 0
                db.AddInParameter(dbComm, "@SP_ID", DbType.String, SP_ID)
                db.AddInParameter(dbComm, "@Code_ID", DbType.String, Code_ID)
                db.ExecuteNonQuery(dbComm)

                SampleCollection_UpdateB = True
            Catch ex As Exception
                MsgBox(ex.Message)
                SampleCollection_UpdateB = False
            End Try
        End Function

        Public Function SampleCollection_UpdateC(ByVal Code_ID As String, ByVal D_ID As String) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleCollection_UpdateC")
                dbComm.CommandTimeout = 0
                db.AddInParameter(dbComm, "@D_ID", DbType.String, D_ID)
                db.AddInParameter(dbComm, "@Code_ID", DbType.String, Code_ID)
                db.ExecuteNonQuery(dbComm)

                SampleCollection_UpdateC = True
            Catch ex As Exception
                MsgBox(ex.Message)
                SampleCollection_UpdateC = False
            End Try
        End Function

        Public Function SampleCollection_UpdateD(ByVal Code_ID As String, ByVal ClientBarcode As String) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleCollection_UpdateD")
                dbComm.CommandTimeout = 0
                db.AddInParameter(dbComm, "@ClientBarcode", DbType.String, ClientBarcode)
                db.AddInParameter(dbComm, "@Code_ID", DbType.String, Code_ID)
                db.ExecuteNonQuery(dbComm)

                SampleCollection_UpdateD = True
            Catch ex As Exception
                MsgBox(ex.Message)
                SampleCollection_UpdateD = False
            End Try
        End Function

        Public Function SampleCollection_UpdateE(ByVal Code_ID As String, ByVal StatusType As String, ByVal D_ID As String) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleCollection_UpdateE")
                dbComm.CommandTimeout = 0
                db.AddInParameter(dbComm, "@Code_ID", DbType.String, Code_ID)
                db.AddInParameter(dbComm, "@StatusType", DbType.String, StatusType)
                db.AddInParameter(dbComm, "@D_ID", DbType.String, D_ID)
                db.ExecuteNonQuery(dbComm)

                SampleCollection_UpdateE = True
            Catch ex As Exception
                MsgBox(ex.Message)
                SampleCollection_UpdateE = False
            End Try
        End Function

        Public Function SampleCollection_UpdateF(ByVal Code_ID As String, ByVal StatusType As String, ByVal ClientBarcode As String) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleCollection_UpdateF")
                dbComm.CommandTimeout = 0
                db.AddInParameter(dbComm, "@Code_ID", DbType.String, Code_ID)
                db.AddInParameter(dbComm, "@StatusType", DbType.String, StatusType)
                db.AddInParameter(dbComm, "@ClientBarcode", DbType.String, ClientBarcode)
                db.ExecuteNonQuery(dbComm)

                SampleCollection_UpdateF = True
            Catch ex As Exception
                MsgBox(ex.Message)
                SampleCollection_UpdateF = False
            End Try
        End Function

        Public Function SampleCollection_UpdateG(ByVal SE_ID As String, ByVal StatusType As String, ByVal D_ID As String) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleCollection_UpdateG")
                dbComm.CommandTimeout = 0
                db.AddInParameter(dbComm, "@SE_ID", DbType.String, SE_ID)
                db.AddInParameter(dbComm, "@StatusType", DbType.String, StatusType)
                db.AddInParameter(dbComm, "@D_ID", DbType.String, D_ID)
                db.ExecuteNonQuery(dbComm)

                SampleCollection_UpdateG = True
            Catch ex As Exception
                MsgBox(ex.Message)
                SampleCollection_UpdateG = False
            End Try
        End Function
        Public Function SampleCollection_UpdateI(ByVal SE_ID As String) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleCollection_UpdateI")
                dbComm.CommandTimeout = 0
                db.AddInParameter(dbComm, "@SE_ID", DbType.String, SE_ID)
                db.ExecuteNonQuery(dbComm)

                SampleCollection_UpdateI = True
            Catch ex As Exception
                MsgBox(ex.Message)
                SampleCollection_UpdateI = False
            End Try
        End Function



        'Public Function SampleCollection_Get(ByVal SP_ID As String) As SampleCollectionInfo
        '    Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
        '    Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleCollection_Get")
        '    db.AddInParameter(dbComm, "@SP_ID", DbType.String, SP_ID)
        '    Dim FeatureList As New SampleCollectionInfo
        '    Using reader As IDataReader = db.ExecuteReader(dbComm)
        '        While reader.Read
        '            FeatureList.SP_ID = reader("SP_ID").ToString
        '        End While
        '        Return FeatureList
        '    End Using
        'End Function
        'Public Function SampleCollection_GetItem(ByVal SO_ID As String, ByVal SS_Edition As String) As List(Of SampleCollectionInfo)
        '    Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
        '    Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleCollection_GetItem")
        '    db.AddInParameter(dbComm, "@SO_ID", DbType.String, SO_ID)
        '    db.AddInParameter(dbComm, "@SS_Edition", DbType.String, SS_Edition)

        '    Dim FeatureList As New List(Of SampleCollectionInfo)
        '    Using reader As IDataReader = db.ExecuteReader(dbComm)
        '        While reader.Read
        '            FeatureList.Add(FillSampleCollectionType(reader))
        '        End While
        '        Return FeatureList
        '    End Using
        'End Function


        Friend Function FillSampleCollectionType(ByVal reader As IDataReader) As SampleCollectionInfo
            '对应取得的数据
            On Error Resume Next

            Dim objInfo As New SampleCollectionInfo

            objInfo.AddDate = CDate(reader("AddDate").ToString)
            objInfo.AddUserID = reader("AddUserID").ToString
            objInfo.AddUserName = reader("AddUserName").ToString
            objInfo.AutoID = reader("AutoID").ToString
            objInfo.Code_ID = reader("Code_ID").ToString

            objInfo.PM_M_Code = reader("PM_M_Code").ToString
            objInfo.ModifyDate = CDate(reader("ModifyDate").ToString)
            objInfo.ModifyUserID = reader("ModifyUserID").ToString
            objInfo.Qty = CInt(reader("Qty"))
            objInfo.Remark = reader("Remark").ToString
            objInfo.StatusType = reader("StatusType").ToString
            objInfo.StatusTypeName = reader("StatusTypeName").ToString

            objInfo.PM_Type = reader("PM_Type").ToString
            objInfo.SO_ID = reader("SO_ID").ToString
            objInfo.SP_ID = reader("SP_ID").ToString
            objInfo.SS_Edition = reader("SS_Edition").ToString
            objInfo.ClientBarcode = reader("ClientBarcode").ToString
            objInfo.BarcodeType = reader("BarcodeType").ToString
            objInfo.D_Dep = reader("D_Dep").ToString
            objInfo.D_ID = reader("D_ID").ToString
            objInfo.SO_SampleID = reader("SO_SampleID").ToString
            objInfo.ID = reader("ID").ToString
            objInfo.Type = reader("Type").ToString
            objInfo.BitAuto = reader("BitAuto")

            '2014-04-18 姚駿
            objInfo.CodeCount = reader("CodeCount")
            objInfo.SE_InTime = reader("SE_InTime")
            objInfo.IntSecond = reader("IntSecond")

            Return objInfo
        End Function

#Region "條碼記錄"
        Public Function SamplePaceBarCodeAll_Getlist(ByVal Code_ID As String, ByVal AddDate1 As String, ByVal AddDate2 As String) As List(Of SampleCollectionInfo)

            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("SamplePaceBarCodeAll_Getlist")
            dbComm.CommandTimeout = 0
            db.AddInParameter(dbComm, "@Code_ID", DbType.String, Code_ID)
            db.AddInParameter(dbComm, "@AddDate1", DbType.String, AddDate1)
            db.AddInParameter(dbComm, "@AddDate2", DbType.String, AddDate2)

            Dim FeatureList As New List(Of SampleCollectionInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillSampleCollectionType(reader))
                End While
                Return FeatureList
            End Using
        End Function

        Public Function SampleCollectionLogin_Add(ByVal objinfo As SampleCollectionInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("SampleCollectionLogin_Add")
                dbComm.CommandTimeout = 0
                db.AddInParameter(dbComm, "@Code_ID", DbType.String, objinfo.Code_ID)
                db.AddInParameter(dbComm, "@Qty", DbType.Int16, objinfo.Qty)
                db.AddInParameter(dbComm, "@StatusType", DbType.String, objinfo.StatusType)

                db.AddInParameter(dbComm, "@Barcode", DbType.String, objinfo.BarCode)
                db.AddInParameter(dbComm, "@D_ID", DbType.String, objinfo.D_ID)
                db.AddInParameter(dbComm, "@AddUserID", DbType.String, objinfo.AddUserID)

                db.ExecuteNonQuery(dbComm)
                SampleCollectionLogin_Add = True
            Catch ex As Exception
                MsgBox(ex.Message)
                SampleCollectionLogin_Add = False
            End Try
        End Function

#End Region

    End Class
End Namespace