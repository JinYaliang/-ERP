Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.Data.Sql
Imports LFERP.Library.NmetalSampleManager.NmetalSampleOrdersSub
Namespace LFERP.Library.NmetalSampleManager.NmetalSampleOrdersMain
    Public Class NmetalSampleOrdersMainControler
        Public Function NmetalSampleOrdersMain_UpdateState(ByVal objinfo As NmetalSampleOrdersMainInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleOrdersMain_UpdateState")
                db.AddInParameter(dbComm, "SO_ID", DbType.String, objinfo.SO_ID)
                db.AddInParameter(dbComm, "SO_State", DbType.String, objinfo.SO_State)
                db.ExecuteNonQuery(dbComm)
                NmetalSampleOrdersMain_UpdateState = True
            Catch ex As Exception
                NmetalSampleOrdersMain_UpdateState = False
            End Try
        End Function

        Public Function NmetalSampleOrdersMain_UpdateType(ByVal objinfo As NmetalSampleOrdersMainInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleOrdersMain_UpdateType")

                db.AddInParameter(dbComm, "@SO_ID", DbType.String, objinfo.SO_ID)
                db.AddInParameter(dbComm, "@M_Code_Type", DbType.String, objinfo.M_Code_Type)
                db.AddInParameter(dbComm, "@TMaterialType", DbType.String, objinfo.TMaterialType)
                db.AddInParameter(dbComm, "@SO_OrdersType", DbType.String, objinfo.SO_OrdersType)

                db.ExecuteNonQuery(dbComm)
                NmetalSampleOrdersMain_UpdateType = True
            Catch ex As Exception
                NmetalSampleOrdersMain_UpdateType = False
            End Try
        End Function


        Public Function NmetalSampleOrdersMain_UpdateQty(ByVal objinfo As NmetalSampleOrdersMainInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleOrdersMain_UpdateQty")
                db.AddInParameter(dbComm, "SO_ID", DbType.String, objinfo.SO_ID)
                db.AddInParameter(dbComm, "SS_Edition", DbType.String, objinfo.SS_Edition)
                db.AddInParameter(dbComm, "SO_OrderQty", DbType.Int32, objinfo.SO_OrderQty)
                db.AddInParameter(dbComm, "SO_NoSendQty", DbType.Int32, objinfo.SO_NoSendQty)
                db.ExecuteNonQuery(dbComm)
                NmetalSampleOrdersMain_UpdateQty = True
            Catch ex As Exception
                NmetalSampleOrdersMain_UpdateQty = False
            End Try
        End Function

        Public Function NmetalSampleOrdersMain_UpdateSampleID(ByVal objinfo As NmetalSampleOrdersMainInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleOrdersMain_UpdateSampleID")
                db.AddInParameter(dbComm, "SO_ID", DbType.String, objinfo.SO_ID)
                db.AddInParameter(dbComm, "SO_SampleID", DbType.String, objinfo.SO_SampleID)

                db.ExecuteNonQuery(dbComm)
                NmetalSampleOrdersMain_UpdateSampleID = True
            Catch ex As Exception
                NmetalSampleOrdersMain_UpdateSampleID = False
            End Try
        End Function


        Public Function ExChangeRate_UpdateCheck(ByVal objinfo As NmetalSampleOrdersMainInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleOrdersMain_UpdateCheck")

                db.AddInParameter(dbComm, "SO_ID", DbType.String, objinfo.SO_ID)
                db.AddInParameter(dbComm, "SO_Check", DbType.Boolean, objinfo.SO_Check)
                db.AddInParameter(dbComm, "SO_CheckDate", DbType.Date, objinfo.SO_CheckDate)
                db.AddInParameter(dbComm, "SO_CheckUserID", DbType.String, objinfo.SO_CheckUserID)
                db.AddInParameter(dbComm, "SO_CheckRemark", DbType.String, objinfo.SO_CheckRemark)
                db.ExecuteNonQuery(dbComm)
                ExChangeRate_UpdateCheck = True

            Catch ex As Exception
                MsgBox(ex.Message)
                ExChangeRate_UpdateCheck = False
            End Try
        End Function


        Public Function NmetalSampleOrdersMain_GetList(ByVal SO_ID As String, ByVal SO_CusterID As String, ByVal PM_M_Code As String, ByVal M_Code As String, ByVal SO_Check As String, ByVal SO_AddDateStrat As String, ByVal SO_AddDateEnd As String, ByVal SO_PODateStrat As String, ByVal SO_PODateEnd As String, ByVal SO_SendDateStrat As String, ByVal SO_SendDateEnd As String, ByVal SO_SampleID As String, ByVal SO_AddUserID As String) As List(Of NmetalSampleOrdersMainInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleOrdersMain_GetList")

            db.AddInParameter(dbComm, "@SO_ID", DbType.String, SO_ID)
            db.AddInParameter(dbComm, "@SO_CusterID", DbType.String, SO_CusterID)
            db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, PM_M_Code)
            db.AddInParameter(dbComm, "@M_Code", DbType.String, M_Code)
            db.AddInParameter(dbComm, "@SO_Check", DbType.Boolean, SO_Check)
            db.AddInParameter(dbComm, "@SO_AddDateStrat", DbType.Date, SO_AddDateStrat)
            db.AddInParameter(dbComm, "@SO_AddDateEnd", DbType.Date, SO_AddDateEnd)
            db.AddInParameter(dbComm, "@SO_PODateStrat", DbType.Date, SO_PODateStrat)
            db.AddInParameter(dbComm, "@SO_PODateEnd", DbType.Date, SO_PODateEnd)
            db.AddInParameter(dbComm, "@SO_SendDateStrat", DbType.Date, SO_SendDateStrat)
            db.AddInParameter(dbComm, "@SO_SendDateEnd", DbType.Date, SO_SendDateEnd)
            db.AddInParameter(dbComm, "@SO_SampleID", DbType.String, SO_SampleID)
            db.AddInParameter(dbComm, "@SO_AddUserID", DbType.String, SO_AddUserID)

            Dim FeatureList As New List(Of NmetalSampleOrdersMainInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillNmetalSampleOrdersMainType(reader))
                End While
                Return FeatureList
            End Using
        End Function




        Public Function NmetalSampleOrdersMain_GetListItem(ByVal SO_ID As String, ByVal SS_Edition As String, ByVal SO_CusterID As String, ByVal SO_Check As String) As List(Of NmetalSampleOrdersMainInfo)

            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleOrdersMain_GetListItem")

            db.AddInParameter(dbComm, "@SO_ID", DbType.String, SO_ID)
            db.AddInParameter(dbComm, "@SS_Edition", DbType.String, SS_Edition)
            db.AddInParameter(dbComm, "@SO_CusterID", DbType.String, SO_CusterID)
            db.AddInParameter(dbComm, "@SO_Check", DbType.String, SO_Check)


            Dim FeatureList As New List(Of NmetalSampleOrdersMainInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillNmetalSampleOrdersMainType(reader))
                End While
                Return FeatureList
            End Using
        End Function
        ''' <summary>
        ''' 獲取客戶列表
        ''' </summary>
        ''' <param name="C_IDList"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampleProcess_GetPRONO(ByVal C_IDList As String) As List(Of NmetalSampleOrdersMainInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleProcess_GetPRONO")
            Dim FeatureList As New List(Of NmetalSampleOrdersMainInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillNmetalSampleOrdersMainType(reader))
                End While
                Return FeatureList
            End Using
        End Function

        Public Function NmetalSampleProcess_GetCusterNo(ByVal C_IDList As String) As String
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleProcess_GetCusterNo")

            Dim str As String
            db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, C_IDList)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                reader.Read()
                str = reader("SO_CusterNo")

                Return str
            End Using
        End Function

        Friend Function FillNmetalSampleOrdersMainType(ByVal reader As IDataReader) As NmetalSampleOrdersMainInfo
            '对应取得的数据
            On Error Resume Next

            Dim objInfo As New NmetalSampleOrdersMainInfo
            objInfo.AutoID = reader("AutoID").ToString
            objInfo.SS_Edition = reader("SS_Edition").ToString
            objInfo.CO_ID = reader("CO_ID").ToString
            objInfo.M_Code = reader("M_Code").ToString
            objInfo.PM_M_Code = reader("PM_M_Code").ToString
            objInfo.SO_AddDate = CDate(reader("SO_AddDate").ToString)
            objInfo.SO_AddUserID = reader("SO_AddUserID").ToString
            objInfo.SO_Check = CBool(reader("SO_Check").ToString)
            objInfo.SO_CheckDate = reader("SO_CheckDate")
            objInfo.SO_CheckUserID = reader("SO_CheckUserID").ToString
            objInfo.SO_Completion = reader("SO_Completion").ToString
            objInfo.SO_CusterID = reader("SO_CusterID").ToString
            objInfo.SO_CusterNo = reader("SO_CusterNo").ToString
            objInfo.SO_CusterPO = reader("SO_CusterPO").ToString
            objInfo.SO_Gauge = reader("SO_Gauge").ToString
            objInfo.SO_ID = reader("SO_ID").ToString
            objInfo.SO_SampleID = reader("SO_SampleID").ToString

            objInfo.SO_ModifyUserID = reader("SO_ModifyUserID").ToString
            objInfo.SO_NoSendQty = CInt(reader("SO_NoSendQty").ToString)
            objInfo.SO_OrderQty = CInt(reader("SO_OrderQty").ToString)
            objInfo.SO_PoDate = CDate(reader("SO_PoDate").ToString)
            objInfo.SO_Remark = reader("SO_Remark").ToString
            objInfo.SO_SendDate = CDate(reader("SO_SendDate").ToString)
            objInfo.SO_State = reader("SO_State").ToString.Trim
            objInfo.M_Name = reader("M_Name").ToString
            'objInfo.SO_ModifyUserName = reader("SO_ModifyUserName").ToString
            objInfo.SO_addUserName = reader("SO_addUserName").ToString
            objInfo.SO_CheckUserName = reader("SO_CheckUserName").ToString
            objInfo.SO_CheckRemark = reader("SO_CheckRemark").ToString
            objInfo.SO_No = reader("SO_No").ToString
            objInfo.SO_Rank = reader("SO_Rank").ToString
            objInfo.SO_CreateDate = Format(CDate(reader("SO_CreateDate")), "yyyy/MM/dd")

            ''2013-11-05
            objInfo.TID = reader("TID").ToString
            objInfo.TName = reader("TName").ToString
            objInfo.TType = reader("TType").ToString
            objInfo.TEnable = reader("TEnable")

            objInfo.MaterialTypeID = reader("MaterialTypeID").ToString
            objInfo.SampTypeID = reader("SampTypeID").ToString
            objInfo.MaterialTypeName = reader("MaterialTypeName").ToString
            objInfo.SampTypeName = reader("SampTypeName").ToString
            objInfo.M_Code_Type = reader("M_Code_Type").ToString
            objInfo.TMaterialType = reader("TMaterialType").ToString
            objInfo.SO_OrdersType = reader("SO_OrdersType").ToString
            objInfo.SO_Closed = reader("SO_Closed")

            objInfo.SO_Type = reader("SO_Type").ToString
            objInfo.SO_TypeName = reader("SO_TypeName").ToString

            '2014-07-23   Mark
            objInfo.Manual_Number = reader("Manual_Number").ToString

            Return objInfo
        End Function

        Friend Function FillNmetalSampleOrdersSubType(ByVal reader As IDataReader) As NmetalSampleOrdersSubInfo
            '对应取得的数据
            On Error Resume Next

            Dim objInfo As New NmetalSampleOrdersSubInfo

            objInfo.AutoID = reader("AutoID").ToString
            objInfo.CO_ID = reader("CO_ID").ToString
            objInfo.PM_M_Code = reader("PM_M_Code").ToString
            objInfo.SS_Edition = reader("SS_Edition").ToString
            objInfo.SWI_Qty = CInt(reader("SWI_Qty").ToString)
            objInfo.SO_NoSendQty = CInt(reader("SO_NoSendQty").ToString)
            objInfo.SS_OrderQty = CInt(reader("SS_OrderQty").ToString)
            objInfo.SS_Price = CDbl(reader("SS_Price").ToString)
            objInfo.SS_Remark = reader("SS_Remark").ToString
            objInfo.SO_ID = reader("SO_ID").ToString
            Return objInfo
        End Function

        Public Function NmetalSampleOrdersMain_UpdateClose(ByVal PM_M_Code As String, ByVal CloseUserID As String) As DataSet
            Dim ds As New DataSet
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleOrdersMain_UpdateClose")
                db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, PM_M_Code)
                db.AddInParameter(dbComm, "@CloseUserID", DbType.String, CloseUserID)
                ds = db.ExecuteDataSet(dbComm)
                Return ds
            Catch ex As Exception
                Return ds
            End Try
        End Function

        Public Function NmetalSampleOrdersMain_GetWareCount(ByVal PM_M_Code As String) As Int32
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim ds As New DataSet
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleOrdersMain_GetWareCount")
                db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, PM_M_Code)
                ds = db.ExecuteDataSet(dbComm)
                If ds.Tables.Count <= 0 Then
                    Return -1
                End If
                If ds.Tables(0).Rows.Count <= 0 Then
                    Return -1
                End If
                Return CInt(ds.Tables(0).Rows(0)("WareCount"))
            Catch ex As Exception
                Return -1
            End Try
        End Function

        Friend Function FillCustomerInof(ByVal reader As IDataReader) As NmetalSampleOrdersMainInfo
            '对应取得客戶的数据
            On Error Resume Next
            Dim objInfo As New NmetalSampleOrdersMainInfo
            objInfo.SO_CusterID = reader("客戶代号").ToString
            objInfo.SO_CusterNo = reader("中文名").ToString
            Return objInfo
        End Function
        Friend Function FillProcessCustomerInof(ByVal reader As IDataReader) As NmetalSampleOrdersMainInfo
            '对应取得客戶的数据
            On Error Resume Next
            Dim objInfo As New NmetalSampleOrdersMainInfo
            objInfo.M_Code = reader("M_Code").ToString
            objInfo.PM_M_Code = reader("PM_M_Code").ToString
            objInfo.SO_CusterNo = reader("SO_CusterNo").ToString
            objInfo.M_Name = reader("M_Name").ToString
            Return objInfo
        End Function
        Friend Function FillComPanyInof(ByVal reader As IDataReader) As NmetalSampleOrdersMainInfo
            '对应取得公司的数据
            On Error Resume Next
            Dim objInfo As New NmetalSampleOrdersMainInfo
            objInfo.CO_ID = reader("Company_ID").ToString
            objInfo.CO_No = reader("Company_Name").ToString
            Return objInfo
        End Function
        Friend Function FillProduct(ByVal reader As IDataReader) As NmetalSampleOrdersMainInfo
            '对应取得产品的数据
            On Error Resume Next
            Dim objInfo As New NmetalSampleOrdersMainInfo
            objInfo.PM_M_Code = reader("PM_M_Code").ToString
            objInfo.M_Code = reader("M_Code").ToString
            objInfo.M_Name = reader("M_Name").ToString
            Return objInfo
        End Function
        ''' <summary>
        ''' 獲取订单数据清单
        ''' </summary>
        ''' <param name="soid">订单编号</param>
        ''' <param name="cid">客戶编号</param>
        ''' <param name="mc">产品类型</param>
        ''' <param name="fromdate">开始日期</param>
        ''' <param name="todate">结束日期</param>
        ''' <param name="sono">流水号</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampleOrdersMain_GetList(ByVal soid As String, ByVal cid As String, ByVal mc As String, ByVal fromdate As Date, ByVal todate As Date, ByVal sono As String, ByVal QtyCheck As Boolean) As List(Of NmetalSampleOrdersMainInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleOrdersMain_GetList")

            db.AddInParameter(dbComm, "@SO_ID", DbType.String, soid)
            db.AddInParameter(dbComm, "@SO_CusterID", DbType.String, cid)
            db.AddInParameter(dbComm, "@M_Code", DbType.String, mc)
            db.AddInParameter(dbComm, "@SO_No", DbType.String, sono)
            db.AddInParameter(dbComm, "@QtyCheck", DbType.String, QtyCheck)

            Dim FeatureList As New List(Of NmetalSampleOrdersMainInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillNmetalSampleOrdersMainType(reader))
                End While
                Return FeatureList
            End Using
        End Function
        ''' <summary>
        ''' 獲取客戶列表
        ''' </summary>
        ''' <param name="C_IDList"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Custer_GetList(ByVal C_IDList As String) As List(Of NmetalSampleOrdersMainInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("Custer_GetList")
            Dim FeatureList As New List(Of NmetalSampleOrdersMainInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillCustomerInof(reader))
                End While
                Return FeatureList
            End Using
        End Function


        ''' <summary>
        ''' 獲取公司列表
        ''' </summary>
        ''' <param name="C_IDList"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function CompanyUnion_GetList(ByVal C_IDList As String) As List(Of NmetalSampleOrdersMainInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("CompanyUnion_GetList")
            Dim FeatureList As New List(Of NmetalSampleOrdersMainInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillComPanyInof(reader))
                End While
                Return FeatureList
            End Using
        End Function
        ''' <summary>
        ''' 獲取定单列表
        ''' </summary>
        ''' <param name="C_IDList"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampleOrdersSub_GetList(ByVal C_IDList As String) As List(Of NmetalSampleOrdersSubInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleOrdersSub_GetList")
            db.AddInParameter(dbComm, "@SO_No", DbType.String, C_IDList)
            Dim FeatureList As New List(Of NmetalSampleOrdersSubInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillNmetalSampleOrdersSubType(reader))
                End While
                Return FeatureList
            End Using
        End Function
        '獲取订单号
        Public Function NmetalSampleOrdersMain_GetOrdersID(ByVal SOyymm As String) As String
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleOrdersMain_GetOrdersID")
            'Dim ym, s, str As String
            Dim ordersNo As String = ""
            db.AddInParameter(dbComm, "@yymm", DbType.String, SOyymm)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    ordersNo = IIf(IsDBNull(reader("SO_ID")), String.Empty, reader("SO_ID").ToString)
                End While
                Return ordersNo
            End Using
        End Function
        Public Function NmetalSampleOrdersMain_GetSoNo() As String
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleOrdersMain_GetSoNo")

            Dim ordersNo As String = ""

            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    ordersNo = IIf(IsDBNull(reader("SO_No")), String.Empty, reader("SO_No").ToString)
                End While
                Return ordersNo
            End Using
        End Function


        '獲取订单审核
        Public Function NmetalSampleOrdersMain_GetCheck(ByVal sono As String) As Boolean
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleOrdersMain_GetCheck")
            db.AddInParameter(dbComm, "@SO_No", DbType.String, sono)

            Using reader As IDataReader = db.ExecuteReader(dbComm)
                reader.Read()
                Return reader("SO_Check")
            End Using
        End Function
        'MAO 2014
        '''''' <summary>
        '''''' 獲取产品信息
        '''''' </summary>
        '''''' <returns></returns>
        '''''' <remarks></remarks>
        ' ''Public Function ProcessMain_GetList1() As List(Of NmetalSampleOrdersMainInfo)
        ' ''    Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
        ' ''    Dim dbComm As DbCommand = db.GetStoredProcCommand("ProcessMain_GetList1")
        ' ''    Dim FeatureList As New List(Of NmetalSampleOrdersMainInfo)
        ' ''    Using reader As IDataReader = db.ExecuteReader(dbComm)
        ' ''        While reader.Read
        ' ''            FeatureList.Add(FillProduct(reader))
        ' ''        End While
        ' ''        Return FeatureList
        ' ''    End Using
        ' ''End Function
        ''' <summary>
        ''' 增加订单記錄
        ''' </summary>
        ''' <param name="objinfo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampleOrdersMain_Add(ByVal objinfo As NmetalSampleOrdersMainInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleOrdersMain_Add")

                db.AddInParameter(dbComm, "CO_ID", DbType.String, objinfo.CO_ID)
                db.AddInParameter(dbComm, "M_Code", DbType.String, objinfo.M_Code)
                db.AddInParameter(dbComm, "PM_M_Code", DbType.String, objinfo.PM_M_Code)
                db.AddInParameter(dbComm, "SO_AddDate", DbType.Date, objinfo.SO_AddDate)
                db.AddInParameter(dbComm, "SO_AddUserID", DbType.String, objinfo.SO_AddUserID)

                db.AddInParameter(dbComm, "SO_CusterID", DbType.String, objinfo.SO_CusterID)
                db.AddInParameter(dbComm, "SO_CusterNo", DbType.String, objinfo.SO_CusterNo)
                db.AddInParameter(dbComm, "SO_CusterPO", DbType.String, objinfo.SO_CusterPO)
                db.AddInParameter(dbComm, "SO_ID", DbType.String, objinfo.SO_ID)
                db.AddInParameter(dbComm, "SO_NoSendQty", DbType.Int32, objinfo.SO_NoSendQty)

                db.AddInParameter(dbComm, "SO_OrderQty", DbType.Int32, objinfo.SO_OrderQty)
                db.AddInParameter(dbComm, "SO_PoDate", DbType.Date, objinfo.SO_PoDate)

                db.AddInParameter(dbComm, "SO_Remark", DbType.String, objinfo.SO_Remark)
                db.AddInParameter(dbComm, "SO_Gauge", DbType.String, objinfo.SO_Gauge)
                db.AddInParameter(dbComm, "SO_SendDate", DbType.Date, objinfo.SO_SendDate)
                db.AddInParameter(dbComm, "SO_Rank", DbType.String, objinfo.SO_Rank)
                db.AddInParameter(dbComm, "SO_No", DbType.String, objinfo.SO_No)
                db.AddInParameter(dbComm, "SO_SampleID", DbType.String, objinfo.SO_SampleID)

                '2013-11-05
                db.AddInParameter(dbComm, "MaterialTypeID", DbType.String, objinfo.MaterialTypeID)
                db.AddInParameter(dbComm, "SampTypeID", DbType.String, objinfo.SampTypeID)
                db.AddInParameter(dbComm, "M_Code_Type", DbType.String, objinfo.M_Code_Type)
                db.AddInParameter(dbComm, "TMaterialType", DbType.String, objinfo.TMaterialType)
                db.AddInParameter(dbComm, "SO_OrdersType", DbType.String, objinfo.SO_OrdersType)

                db.AddInParameter(dbComm, "SO_Type", DbType.String, objinfo.SO_Type)

                '2014-07-23  Mark
                db.AddInParameter(dbComm, "Manual_Number", DbType.String, objinfo.Manual_Number)

                db.ExecuteNonQuery(dbComm)
                NmetalSampleOrdersMain_Add = True

            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSampleOrdersMain_Add = False
            End Try
        End Function

        Public Function NmetalSampleOrdersMain_UpdateCheck(ByVal objinfo As NmetalSampleOrdersMainInfo) As Boolean

            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleOrdersMain_UpdateCheck")

                db.AddInParameter(dbComm, "SO_ID", DbType.String, objinfo.SO_ID)
                db.AddInParameter(dbComm, "SO_Check", DbType.Boolean, objinfo.SO_Check)
                db.AddInParameter(dbComm, "SO_CheckDate", DbType.Date, objinfo.SO_CheckDate)
                db.AddInParameter(dbComm, "SO_CheckUserID", DbType.String, objinfo.SO_CheckUserID)
                db.AddInParameter(dbComm, "SO_CheckRemark", DbType.String, objinfo.SO_CheckRemark)

                db.ExecuteNonQuery(dbComm)
                NmetalSampleOrdersMain_UpdateCheck = True
            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSampleOrdersMain_UpdateCheck = False
            End Try
        End Function
        ''' <summary>
        '''修改订单記錄
        ''' </summary>
        ''' <param name="objinfo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampleOrdersMain_Update(ByVal objinfo As NmetalSampleOrdersMainInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleOrdersMain_Update")

                db.AddInParameter(dbComm, "SO_SampleID", DbType.String, objinfo.SO_SampleID)
                db.AddInParameter(dbComm, "SO_NO", DbType.String, objinfo.SO_No)
                db.AddInParameter(dbComm, "SO_PoDate", DbType.Date, objinfo.SO_PoDate)

                db.AddInParameter(dbComm, "SO_ModifyDate", DbType.Date, objinfo.SO_ModifyDate)
                db.AddInParameter(dbComm, "SO_ModifyUserID", DbType.String, objinfo.SO_ModifyUserID)
                db.AddInParameter(dbComm, "CO_ID", DbType.String, objinfo.CO_ID)
                db.AddInParameter(dbComm, "M_Code", DbType.String, objinfo.M_Code)
                db.AddInParameter(dbComm, "PM_M_Code", DbType.String, objinfo.PM_M_Code)

                db.AddInParameter(dbComm, "SO_CusterID", DbType.String, objinfo.SO_CusterID)
                db.AddInParameter(dbComm, "SO_CusterNo", DbType.String, objinfo.SO_CusterNo)
                db.AddInParameter(dbComm, "SO_CusterPO", DbType.String, objinfo.SO_CusterPO)
                db.AddInParameter(dbComm, "SO_ID", DbType.String, objinfo.SO_ID)
                db.AddInParameter(dbComm, "SO_NoSendQty", DbType.Int32, objinfo.SO_NoSendQty)

                db.AddInParameter(dbComm, "SO_OrderQty", DbType.Int32, objinfo.SO_OrderQty)
                db.AddInParameter(dbComm, "SO_Remark", DbType.String, objinfo.SO_Remark)
                db.AddInParameter(dbComm, "SO_SendDate", DbType.Date, objinfo.SO_SendDate)
                db.AddInParameter(dbComm, "SO_Rank", DbType.String, objinfo.SO_Rank)

                '2013-11-05
                db.AddInParameter(dbComm, "MaterialTypeID", DbType.String, objinfo.MaterialTypeID)
                db.AddInParameter(dbComm, "SampTypeID", DbType.String, objinfo.SampTypeID)
                db.AddInParameter(dbComm, "M_Code_Type", DbType.String, objinfo.M_Code_Type)
                db.AddInParameter(dbComm, "TMaterialType", DbType.String, objinfo.TMaterialType)
                db.AddInParameter(dbComm, "SO_OrdersType", DbType.String, objinfo.SO_OrdersType)

                db.AddInParameter(dbComm, "SO_Type", DbType.String, objinfo.SO_Type)

                '2014-07-23   Mark
                db.AddInParameter(dbComm, "Manual_Number", DbType.String, objinfo.Manual_Number)
                db.ExecuteNonQuery(dbComm)
                NmetalSampleOrdersMain_Update = True
            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSampleOrdersMain_Update = False
            End Try
        End Function
        ''' <summary>
        ''' 刪除订单
        ''' </summary>
        ''' <param name="soid"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampleOrdersMain_Delete(ByVal soid As String) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleOrdersMain_Delete")
                db.AddInParameter(dbComm, "SO_No", DbType.String, soid)
                db.ExecuteNonQuery(dbComm)
                NmetalSampleOrdersMain_Delete = True
            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSampleOrdersMain_Delete = False
            End Try
        End Function
        Friend Function FillNmetalSampleOrdersInfo(ByVal reader As IDataReader) As NmetalSampleOrdersMainInfo
            '对应取得的数据
            On Error Resume Next

            Dim objInfo As New NmetalSampleOrdersMainInfo
            objInfo.AutoID = reader("AutoID").ToString
            objInfo.CO_ID = reader("CO_ID").ToString
            objInfo.SO_SampleID = reader("SO_SampleID").ToString
            objInfo.M_Code = reader("M_Code").ToString
            objInfo.M_Name = reader("M_Name").ToString
            objInfo.PM_M_Code = reader("PM_M_Code").ToString
            objInfo.SO_AddDate = reader("SO_AddDate").ToString
            objInfo.SO_AddUserID = reader("SO_AddUserID").ToString
            objInfo.SO_addUserName = reader("SO_AddUserName").ToString
            objInfo.SO_Check = reader("SO_Check").ToString
            objInfo.SO_CheckDate = Format(reader("SO_CheckDate"), "yyyy/MM/dd")
            objInfo.SO_CheckUserID = reader("SO_CheckUserID").ToString
            objInfo.SO_CheckUserName = reader("SO_CheckUserName").ToString
            objInfo.SO_CheckRemark = reader("SO_CheckRemark").ToString
            objInfo.SO_Completion = reader("SO_Completion").ToString
            objInfo.SO_CusterID = reader("SO_CusterID").ToString
            objInfo.SO_CusterNo = reader("SO_CusterNo").ToString
            objInfo.SO_CusterPO = reader("SO_CusterPO").ToString
            objInfo.SO_Gauge = reader("SO_Gauge").ToString
            objInfo.SO_ID = reader("SO_ID").ToString
            objInfo.SO_ModifyDate = reader("SO_ModifyDate").ToString
            objInfo.SO_ModifyUserID = reader("SO_ModifyUserID").ToString
            objInfo.SO_NoSendQty = reader("SO_NoSendQty").ToString
            objInfo.SO_OrderQty = reader("SO_OrderQty").ToString
            objInfo.SO_State = reader("SO_State").ToString.Trim
            objInfo.SO_Rank = reader("SO_Rank").ToString
            objInfo.SS_Price = CDbl(reader("SS_Price").ToString)
            objInfo.SWI_Qty = CInt(reader("SWI_Qty").ToString)
            objInfo.SO_CreateDate = Format(reader("SO_CreateDate"), "yyyy/MM/dd")
            If reader("SO_PoDate") Is DBNull.Value Then
                objInfo.SO_PoDate = Nothing
            Else
                objInfo.SO_PoDate = Format(CDate(reader("SO_PoDate")), "yyyy/MM/dd")
            End If
            objInfo.SO_Closed = CBool(reader("SO_Closed").ToString)
            objInfo.SO_Price = reader("SO_Price").ToString
            objInfo.SO_PriceUine = reader("SO_PriceUine").ToString
            objInfo.SO_Remark = reader("SO_Remark").ToString
            objInfo.SO_No = reader("SO_No").ToString
            objInfo.SO_SendDate = Format(reader("SO_SendDate"), "yyyy/MM/dd")
            objInfo.SS_Remark = reader("SS_Remark").ToString
            objInfo.SS_Edition = reader("SS_Edition").ToString


            ''2013-11-05

            objInfo.TID = reader("TID").ToString
            objInfo.TName = reader("TName").ToString
            objInfo.TType = reader("TType").ToString
            objInfo.TEnable = reader("TEnable")

            objInfo.MaterialTypeID = reader("MaterialTypeID").ToString
            objInfo.SampTypeID = reader("SampTypeID").ToString
            objInfo.MaterialTypeName = reader("MaterialTypeName").ToString
            objInfo.SampTypeName = reader("SampTypeIDName").ToString
            objInfo.M_Code_Type = reader("M_Code_Type").ToString
            objInfo.TMaterialType = reader("TMaterialType").ToString
            objInfo.SO_OrdersType = reader("SO_OrdersType").ToString
            '2014-07-02
            objInfo.SO_Type = reader("SO_Type").ToString
            objInfo.SO_TypeName = reader("SO_TypeName").ToString



            Return objInfo
        End Function
        ''' <summary>
        ''' 樣辦類型
        ''' </summary>
        ''' <param name="TID"></param>
        ''' <param name="TType"></param>
        ''' <param name="TEnable"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampleOrdersType_GetList(ByVal TID As String, ByVal TType As String, ByVal TEnable As String, ByVal TName As String) As List(Of NmetalSampleOrdersMainInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleOrdersType_GetList")

            db.AddInParameter(dbComm, "@TID", DbType.String, TID)
            db.AddInParameter(dbComm, "@TType", DbType.String, TType)
            db.AddInParameter(dbComm, "@TEnable", DbType.Boolean, TEnable)
            db.AddInParameter(dbComm, "@TName", DbType.String, TName)

            Dim FeatureList As New List(Of NmetalSampleOrdersMainInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillNmetalSampleOrdersMainType(reader))
                End While
                Return FeatureList
            End Using
        End Function

        Public Function NmetalSampleOrdersType_GetID(ByVal SoType As String) As NmetalSampleOrdersMainInfo
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleOrdersType_GetID")
            db.AddInParameter(dbComm, "@SoType", DbType.String, SoType)
            Dim FeatureList As New NmetalSampleOrdersMainInfo
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.TID = reader("TID").ToString
                End While
                Return FeatureList
            End Using
        End Function

        Public Function NmetalSampleOrdersType_Update(ByVal objinfo As NmetalSampleOrdersMainInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleOrdersType_Update")

                db.AddInParameter(dbComm, "@TID", DbType.String, objinfo.TID)
                db.AddInParameter(dbComm, "@TType", DbType.String, objinfo.TType)
                db.AddInParameter(dbComm, "@TName", DbType.String, objinfo.TName)
                db.AddInParameter(dbComm, "@TEnable", DbType.Boolean, objinfo.TEnable)
                db.AddInParameter(dbComm, "@AutoID", DbType.String, objinfo.AutoID)
                db.AddInParameter(dbComm, "@TMaterialType", DbType.String, objinfo.TMaterialType)

                db.ExecuteNonQuery(dbComm)
                NmetalSampleOrdersType_Update = True

            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSampleOrdersType_Update = False
            End Try
        End Function

        Public Function NmetalSampleOrdersType_Add(ByVal objinfo As NmetalSampleOrdersMainInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleOrdersType_Add")

                db.AddInParameter(dbComm, "@TID", DbType.String, objinfo.TID)
                db.AddInParameter(dbComm, "@TType", DbType.String, objinfo.TType)
                db.AddInParameter(dbComm, "@TName", DbType.String, objinfo.TName)
                db.AddInParameter(dbComm, "@TEnable", DbType.Boolean, objinfo.TEnable)
                db.AddInParameter(dbComm, "@TMaterialType", DbType.String, objinfo.TMaterialType)

                db.ExecuteNonQuery(dbComm)
                NmetalSampleOrdersType_Add = True

            Catch ex As Exception
                MsgBox(ex.Message)
                NmetalSampleOrdersType_Add = False
            End Try
        End Function
        ''' <summary>
        ''' Mark
        ''' 2014-07-23
        ''' </summary>
        ''' <param name="objinfo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NmetalSampleOrdersMain_UpdateManual_Number(ByVal objinfo As NmetalSampleOrdersMainInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleOrdersMain_UpdateManual_Number")
                db.AddInParameter(dbComm, "SO_ID", DbType.String, objinfo.SO_ID)
                db.AddInParameter(dbComm, "Manual_Number", DbType.String, objinfo.Manual_Number)

                db.ExecuteNonQuery(dbComm)
                NmetalSampleOrdersMain_UpdateManual_Number = True
            Catch ex As Exception
                NmetalSampleOrdersMain_UpdateManual_Number = False
            End Try
        End Function

        Public Function NmetalSampleOrdersMain_GetTMaterialType(ByVal MaterialTypeID As String) As List(Of NmetalSampleOrdersMainInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("NmetalSampleOrdersMain_GetTMaterialType")
            db.AddInParameter(dbComm, "@MaterialTypeID", DbType.String, MaterialTypeID)
            Dim FeatureList As New List(Of NmetalSampleOrdersMainInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillNmetalSampleOrdersMainType(reader))
                End While
                Return FeatureList
            End Using
        End Function

    End Class
End Namespace

