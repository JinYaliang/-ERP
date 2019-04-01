Namespace LFERP.Library.WareHouse.WareInventoryCheck
    Public Class WareInventoryCheckController
        ''' <summary>
        ''' WareInventoryCheckProcess��s�W
        ''' </summary>
        ''' <param name="objFile1"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function WareInventoryCheckProcess_Add(ByVal objFile1 As WareInventoryCheckInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("WareInventoryCheckProcess_Add")
                db.AddInParameter(dbComm, "@WIC_NO", DbType.String, objFile1.WIC_NO)
                db.AddInParameter(dbComm, "@M_Code", DbType.String, objFile1.M_Code)
                db.AddInParameter(dbComm, "@DepotNO", DbType.String, objFile1.DepotNO)
                db.AddInParameter(dbComm, "@WIC_NewQty", DbType.String, objFile1.WIC_NewQty)
                db.AddInParameter(dbComm, "@WIC_OldQty", DbType.String, objFile1.WIC_OldQty)
                db.AddInParameter(dbComm, "@WIC_Difference", DbType.String, objFile1.WIC_Difference)
                db.AddInParameter(dbComm, "@WIC_Type", DbType.String, objFile1.WIC_Type)
                db.ExecuteNonQuery(dbComm)
                WareInventoryCheckProcess_Add = True
            Catch ex As Exception
                MsgBox(ex.Message)
                WareInventoryCheckProcess_Add = False
            End Try
        End Function

        ''' <summary>
        ''' WareInventoryCheckSub��s�W
        ''' </summary>
        ''' <param name="objFile1"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function WareInventoryCheckSub_Add(ByVal objFile1 As WareInventoryCheckInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("WareInventoryCheckSub_Add")
                db.AddInParameter(dbComm, "@WIC_NO", DbType.String, objFile1.WIC_NO)
                db.AddInParameter(dbComm, "@M_Code", DbType.String, objFile1.M_Code)
                db.AddInParameter(dbComm, "@DepotNO", DbType.String, objFile1.DepotNO)
                db.AddInParameter(dbComm, "@WIC_NewQty", DbType.String, objFile1.WIC_NewQty)
                db.AddInParameter(dbComm, "@WIC_OldQty", DbType.String, objFile1.WIC_OldQty)
                db.AddInParameter(dbComm, "@WIC_Difference", DbType.String, objFile1.WIC_Difference)

                db.ExecuteNonQuery(dbComm)
                WareInventoryCheckSub_Add = True
            Catch ex As Exception
                MsgBox(ex.Message)
                WareInventoryCheckSub_Add = False

            End Try
        End Function


        ''' <summary>
        ''' WareInventoryCheck��s�W
        ''' </summary>
        ''' <param name="objFile1"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function WareInventoryCheck_Add(ByVal objFile1 As WareInventoryCheckInfo) As Boolean


            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("WareInventoryCheck_Add")
                db.AddInParameter(dbComm, "@WIC_NO", DbType.String, objFile1.WIC_NO)
                db.AddInParameter(dbComm, "@WIC_Date", DbType.Date, objFile1.WIC_Date)
                db.AddInParameter(dbComm, "@DepotNO", DbType.String, objFile1.DepotNO)
                db.AddInParameter(dbComm, "@WIC_Remark", DbType.String, objFile1.WIC_Remark)
                db.AddInParameter(dbComm, "@WIC_Action", DbType.String, objFile1.WIC_Action)

                db.ExecuteNonQuery(dbComm)
                WareInventoryCheck_Add = True
            Catch ex As Exception
                MsgBox(ex.Message)
                WareInventoryCheck_Add = False

            End Try
        End Function
        ''' <summary>
        ''' �o���eWareInventoryCheck��WIC_NO�̤j�@���O��
        ''' </summary>
        ''' <param name="NDate"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function WareInventoryCheck_GetNO(ByVal NDate As String) As WareInventoryCheckInfo
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("WareInventoryCheck_GetNO")
            db.AddInParameter(dbComm, "@NDate", DbType.String, NDate)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    Return FillWareInventoryCheck(reader)
                End While
                Return Nothing
            End Using
        End Function


        ''' <summary>
        '''  ��d��WareInventoryCheck
        ''' </summary>
        ''' <param name="WIC_NO"></param>
        ''' <param name="DepotNO"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function WareInventoryCheck_GetList(ByVal WIC_NO As String, ByVal DepotNO As String) As List(Of WareInventoryCheckInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("WareInventoryCheck_GetList")
            db.AddInParameter(dbComm, "@WIC_NO", DbType.String, WIC_NO)
            db.AddInParameter(dbComm, "@DepotNO", DbType.String, DepotNO)

            Dim FeatureList As New List(Of WareInventoryCheckInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillWareInventoryCheck(reader))
                End While
                Return FeatureList
            End Using
        End Function

        ''' <summary>
        ''' ��d��WareInventoryCheckSub
        ''' </summary>
        ''' <param name="WIC_NO"></param>
        ''' <param name="DepotNO"></param>
        ''' <param name="M_Code"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function WareInventoryCheckSub_GetList(ByVal WIC_NO As String, ByVal DepotNO As String, ByVal M_Code As String) As List(Of WareInventoryCheckInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("WareInventoryCheckSub_GetList")
            db.AddInParameter(dbComm, "@WIC_NO", DbType.String, WIC_NO)
            db.AddInParameter(dbComm, "@DepotNO", DbType.String, DepotNO)
            db.AddInParameter(dbComm, "@M_Code", DbType.String, M_Code)

            Dim FeatureList As New List(Of WareInventoryCheckInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillWareInventoryCheck(reader))
                End While
                Return FeatureList
            End Using

        End Function


        ''' <summary>
        ''' ��d��WareInventoryCheckProcess
        ''' </summary>
        ''' <param name="WIC_NO"></param>
        ''' <param name="DepotNO"></param>
        ''' <param name="M_Code"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function WareInventoryCheckProcess_GetList(ByVal WIC_NO As String, ByVal DepotNO As String, ByVal M_Code As String) As List(Of WareInventoryCheckInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("WareInventoryCheckProcess_GetList")
            db.AddInParameter(dbComm, "@WIC_NO", DbType.String, WIC_NO)
            db.AddInParameter(dbComm, "@DepotNO", DbType.String, DepotNO)
            db.AddInParameter(dbComm, "@M_Code", DbType.String, M_Code)

            Dim FeatureList As New List(Of WareInventoryCheckInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillWareInventoryCheck(reader))
                End While
                Return FeatureList
            End Using
        End Function


        ''' <summary>
        '''  �������o���ƾ�
        ''' </summary>
        ''' <param name="reader"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend Function FillWareInventoryCheck(ByVal reader As IDataReader) As WareInventoryCheckInfo

            On Error Resume Next

            Dim objInfo As New WareInventoryCheckInfo

            objInfo.DepotNO = reader("DepotNO").ToString
            objInfo.WIC_NO = reader("WIC_NO").ToString
            objInfo.M_Code = reader("M_Code").ToString
            objInfo.WIC_NewQty = reader("WIC_NewQty").ToString
            objInfo.WIC_OldQty = reader("WIC_OldQty").ToString
            objInfo.WIC_Difference = reader("WIC_Difference").ToString
            objInfo.WIC_Process = reader("WIC_Process").ToString
            objInfo.WIC_Type = reader("WIC_Type").ToString

            If reader("WIC_Date") Is DBNull.Value Then
                objInfo.WIC_Date = Nothing
            Else
                objInfo.WIC_Date = CStr(reader("WIC_Date"))
            End If

            objInfo.WIC_Remark = reader("WIC_Remark").ToString
            objInfo.WIC_Action = reader("WIC_Action").ToString

            If reader("WIC_Check") Is DBNull.Value Then
                objInfo.WIC_Check = Nothing
            Else
                objInfo.WIC_Check = reader("WIC_Check")
            End If
            objInfo.WIC_CheckAction = reader("WIC_CheckAction").ToString
            objInfo.WIC_CheckRemark = reader("WIC_CheckRemark").ToString
            objInfo.WIC_CheckType = reader("WIC_CheckType").ToString


            objInfo.M_Gauge = reader("M_Gauge").ToString
            objInfo.M_Name = reader("M_Name").ToString
            objInfo.ActionName = reader("ActionName").ToString
            objInfo.CheckActionName = reader("CheckActionName").ToString
            objInfo.WH_Name = reader("WH_PName").ToString & "-" & reader("WH_Name").ToString

            Return objInfo
        End Function


        ''' <summary>
        ''' ���Y�L�I�渹�Y���Ƥ��ն��`�p��X�s�L�I��
        ''' </summary>
        ''' <param name="WIC_NO"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function WareInventoryCheckSub_SumWICNewQty(ByVal WIC_NO As String, ByVal DepotNO As String, ByVal M_Code As String) As List(Of WareInventoryCheckInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("WareInventoryCheckSub_SumWICNewQty")
            db.AddInParameter(dbComm, "@WIC_NO", DbType.String, WIC_NO)
            db.AddInParameter(dbComm, "@DepotNO", DbType.String, DepotNO)
            db.AddInParameter(dbComm, "@M_Code", DbType.String, M_Code)

            Dim FeatureList As New List(Of WareInventoryCheckInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillWareInventoryCheck(reader))
                End While
                Return FeatureList
            End Using

        End Function

        ''' <summary>
        ''' ��WareInventoryCheck_Check�f��
        ''' </summary>
        ''' <param name="objFile1"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function WareInventoryCheck_Check(ByVal objFile1 As WareInventoryCheckInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("WareInventoryCheck_Check")
                db.AddInParameter(dbComm, "@WIC_NO", DbType.String, objFile1.WIC_NO)
                db.AddInParameter(dbComm, "@M_Code", DbType.String, objFile1.M_Code)
                db.AddInParameter(dbComm, "@DepotNO", DbType.String, objFile1.DepotNO)
                db.AddInParameter(dbComm, "@WIC_NewQty", DbType.String, objFile1.WIC_NewQty)
                db.AddInParameter(dbComm, "@WIC_Check", DbType.String, objFile1.WIC_Check)
                db.AddInParameter(dbComm, "@WIC_CheckAction", DbType.String, objFile1.WIC_CheckAction)
                db.AddInParameter(dbComm, "@WIC_CheckType", DbType.String, objFile1.WIC_CheckType)
                db.AddInParameter(dbComm, "@WIC_CheckRemark", DbType.String, objFile1.WIC_CheckRemark)

                db.ExecuteNonQuery(dbComm)
                WareInventoryCheck_Check = True
            Catch ex As Exception
                MsgBox(ex.Message)
                WareInventoryCheck_Check = False
            End Try
        End Function
        ''' <summary>
        ''' ��WareInventoryCheck_Check�f��
        ''' </summary>
        ''' <param name="objFile1"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function WareInventoryCheck_UpdateCheck(ByVal objFile1 As WareInventoryCheckInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("WareInventoryCheck_UpdateCheck")
                db.AddInParameter(dbComm, "@WIC_NO", DbType.String, objFile1.WIC_NO)
                db.AddInParameter(dbComm, "@WIC_Check", DbType.String, objFile1.WIC_Check)
                db.AddInParameter(dbComm, "@WIC_CheckAction", DbType.String, objFile1.WIC_CheckAction)
                db.AddInParameter(dbComm, "@WIC_CheckType", DbType.String, objFile1.WIC_CheckType)
                db.AddInParameter(dbComm, "@WIC_CheckRemark", DbType.String, objFile1.WIC_CheckRemark)

                db.ExecuteNonQuery(dbComm)
                WareInventoryCheck_UpdateCheck = True
            Catch ex As Exception
                MsgBox(ex.Message)
                WareInventoryCheck_UpdateCheck = False
            End Try
        End Function

    End Class

End Namespace
