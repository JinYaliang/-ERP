Namespace LFERP.Library.KnifeWareInventoryCheck
    Public Class KnifeWareInventoryCheckControl

        ''' <summary>
        ''' WareInventoryCheckProcess��s�W
        ''' </summary>
        ''' <param name="objFile1"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function KnifeWareInventoryCheckProcess_Add(ByVal objFile1 As KnifeWareInventoryCheckInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("KnifeWareInventoryCheckProcess_Add")
                db.AddInParameter(dbComm, "@WIC_NO", DbType.String, objFile1.WIC_NO)
                db.AddInParameter(dbComm, "@M_Code", DbType.String, objFile1.M_Code)
                db.AddInParameter(dbComm, "@DepotNO", DbType.String, objFile1.DepotNO)
                db.AddInParameter(dbComm, "@WIC_NewQty", DbType.String, objFile1.WIC_NewQty)
                db.AddInParameter(dbComm, "@WIC_OldQty", DbType.String, objFile1.WIC_OldQty)
                db.AddInParameter(dbComm, "@WIC_Difference", DbType.String, objFile1.WIC_Difference)
                db.AddInParameter(dbComm, "@WIC_Type", DbType.String, objFile1.WIC_Type)
                db.AddInParameter(dbComm, "@WIC_KnifeKType", DbType.String, objFile1.WIC_KnifeKType)
                db.AddInParameter(dbComm, "@WIC_PDType", DbType.String, objFile1.WIC_PDType)
                db.ExecuteNonQuery(dbComm)
                KnifeWareInventoryCheckProcess_Add = True
            Catch ex As Exception
                MsgBox(ex.Message)
                KnifeWareInventoryCheckProcess_Add = False
            End Try
        End Function

        Public Function KnifeWareInventoryCheckProcess_Delete(ByVal WIC_NO As String, ByVal DepotNO As String) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("KnifeWareInventoryCheckProcess_Delete")
                db.AddInParameter(dbComm, "@WIC_NO", DbType.String, WIC_NO)
                db.AddInParameter(dbComm, "@DepotNO", DbType.String, DepotNO)

                db.ExecuteNonQuery(dbComm)
                KnifeWareInventoryCheckProcess_Delete = True
            Catch ex As Exception
                MsgBox(ex.Message)
                KnifeWareInventoryCheckProcess_Delete = False
            End Try
        End Function

        ''' <summary>
        ''' WareInventoryCheckSub��s�W
        ''' </summary>
        ''' <param name="objFile1"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function KnifeWareInventoryCheckSub_Add(ByVal objFile1 As KnifeWareInventoryCheckInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("KnifeWareInventoryCheckSub_Add")
                db.AddInParameter(dbComm, "@WIC_NO", DbType.String, objFile1.WIC_NO)
                db.AddInParameter(dbComm, "@M_Code", DbType.String, objFile1.M_Code)
                db.AddInParameter(dbComm, "@DepotNO", DbType.String, objFile1.DepotNO)
                db.AddInParameter(dbComm, "@WIC_NewQty", DbType.String, objFile1.WIC_NewQty)
                db.AddInParameter(dbComm, "@WIC_OldQty", DbType.String, objFile1.WIC_OldQty)
                db.AddInParameter(dbComm, "@WIC_Difference", DbType.String, objFile1.WIC_Difference)

                db.AddInParameter(dbComm, "@InputType", DbType.String, objFile1.InputType)
                db.AddInParameter(dbComm, "@Remark", DbType.String, objFile1.Remark)
                db.AddInParameter(dbComm, "@WIC_KnifeKType", DbType.String, objFile1.WIC_KnifeKType)
                db.AddInParameter(dbComm, "@WIC_PDType", DbType.String, objFile1.WIC_PDType)


                db.ExecuteNonQuery(dbComm)
                KnifeWareInventoryCheckSub_Add = True
            Catch ex As Exception
                MsgBox(ex.Message)
                KnifeWareInventoryCheckSub_Add = False

            End Try
        End Function


        ''' <summary>
        ''' WareInventoryCheck��s�W
        ''' </summary>
        ''' <param name="objFile1"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function KnifeWareInventoryCheck_Add(ByVal objFile1 As KnifeWareInventoryCheckInfo) As Boolean


            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("KnifeWareInventoryCheck_Add")
                db.AddInParameter(dbComm, "@WIC_NO", DbType.String, objFile1.WIC_NO)
                db.AddInParameter(dbComm, "@WIC_Date", DbType.Date, objFile1.WIC_Date)
                db.AddInParameter(dbComm, "@DepotNO", DbType.String, objFile1.DepotNO)
                db.AddInParameter(dbComm, "@WIC_Remark", DbType.String, objFile1.WIC_Remark)
                db.AddInParameter(dbComm, "@WIC_Action", DbType.String, objFile1.WIC_Action)
                db.AddInParameter(dbComm, "@WIC_KnifeKType", DbType.String, objFile1.WIC_KnifeKType)
                db.AddInParameter(dbComm, "@WIC_PDType", DbType.String, objFile1.WIC_PDType)


                db.ExecuteNonQuery(dbComm)
                KnifeWareInventoryCheck_Add = True
            Catch ex As Exception
                MsgBox(ex.Message)
                KnifeWareInventoryCheck_Add = False

            End Try
        End Function


        Public Function KnifeWareInventoryCheck_Delete(ByVal WIC_NO As String, ByVal DepotNO As String) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("KnifeWareInventoryCheck_Delete")
                db.AddInParameter(dbComm, "@WIC_NO", DbType.String, WIC_NO)
                db.AddInParameter(dbComm, "@DepotNO", DbType.String, DepotNO)

                db.ExecuteNonQuery(dbComm)
                KnifeWareInventoryCheck_Delete = True
            Catch ex As Exception
                MsgBox(ex.Message)
                KnifeWareInventoryCheck_Delete = False

            End Try
        End Function

        ''' <summary>
        ''' �o���eWareInventoryCheck��WIC_NO�̤j�@���O��
        ''' </summary>
        ''' <param name="NDate"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function KnifeWareInventoryCheck_GetNO(ByVal NDate As String) As KnifeWareInventoryCheckInfo
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("KnifeWareInventoryCheck_GetNO")
            db.AddInParameter(dbComm, "@NDate", DbType.String, NDate)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    Return FillKnifeWareInventoryCheck(reader)
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
        Public Function KnifeWareInventoryCheck_GetList(ByVal WIC_NO As String, ByVal DepotNO As String) As List(Of KnifeWareInventoryCheckInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("KnifeWareInventoryCheck_GetList")
            db.AddInParameter(dbComm, "@WIC_NO", DbType.String, WIC_NO)
            db.AddInParameter(dbComm, "@DepotNO", DbType.String, DepotNO)

            Dim FeatureList As New List(Of KnifeWareInventoryCheckInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillKnifeWareInventoryCheck(reader))
                End While
                Return FeatureList
            End Using
        End Function
        ''' <summary>
        ''' ���L
        ''' </summary>
        ''' <param name="WIC_NO"></param>
        ''' <param name="DepotNO"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function KnifeWareInventoryCheck_Analyse(ByVal WIC_NO As String, ByVal DepotNO As String) As List(Of KnifeWareInventoryCheckInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("KnifeWareInventoryCheck_Analyse")
            db.AddInParameter(dbComm, "@WIC_NO", DbType.String, WIC_NO)
            db.AddInParameter(dbComm, "@DepotNO", DbType.String, DepotNO)

            Dim FeatureList As New List(Of KnifeWareInventoryCheckInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillKnifeWareInventoryCheck(reader))
                End While
                Return FeatureList
            End Using
        End Function

        ''' <summary>
        ''' ��L
        ''' </summary>
        ''' <param name="WIC_NO"></param>
        ''' <param name="DepotNO"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function KnifeWareInventoryCheck_AnalyseSub(ByVal WIC_NO As String, ByVal DepotNO As String) As List(Of KnifeWareInventoryCheckInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("KnifeWareInventoryCheck_AnalyseSub")
            db.AddInParameter(dbComm, "@WIC_NO", DbType.String, WIC_NO)
            db.AddInParameter(dbComm, "@DepotNO", DbType.String, DepotNO)

            Dim FeatureList As New List(Of KnifeWareInventoryCheckInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillKnifeWareInventoryCheck(reader))
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
        Public Function KnifeWareInventoryCheckSub_GetList(ByVal WIC_NO As String, ByVal DepotNO As String, ByVal M_Code As String) As List(Of KnifeWareInventoryCheckInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("KnifeWareInventoryCheckSub_GetList")
            db.AddInParameter(dbComm, "@WIC_NO", DbType.String, WIC_NO)
            db.AddInParameter(dbComm, "@DepotNO", DbType.String, DepotNO)
            db.AddInParameter(dbComm, "@M_Code", DbType.String, M_Code)

            Dim FeatureList As New List(Of KnifeWareInventoryCheckInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillKnifeWareInventoryCheck(reader))
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
        Public Function KnifeWareInventoryCheckProcess_GetList(ByVal WIC_NO As String, ByVal DepotNO As String, ByVal M_Code As String) As List(Of KnifeWareInventoryCheckInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("KnifeWareInventoryCheckProcess_GetList")
            db.AddInParameter(dbComm, "@WIC_NO", DbType.String, WIC_NO)
            db.AddInParameter(dbComm, "@DepotNO", DbType.String, DepotNO)
            db.AddInParameter(dbComm, "@M_Code", DbType.String, M_Code)

            Dim FeatureList As New List(Of KnifeWareInventoryCheckInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillKnifeWareInventoryCheck(reader))
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
        Friend Function FillKnifeWareInventoryCheck(ByVal reader As IDataReader) As KnifeWareInventoryCheckInfo

            On Error Resume Next

            Dim objInfo As New KnifeWareInventoryCheckInfo

            objInfo.DepotNO = reader("DepotNO").ToString
            objInfo.WIC_NO = reader("WIC_NO").ToString
            objInfo.M_Code = reader("M_Code").ToString
            objInfo.WIC_NewQty = reader("WIC_NewQty")
            objInfo.WIC_OldQty = reader("WIC_OldQty")
            objInfo.WIC_Difference = reader("WIC_Difference")
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

            objInfo.InputType = reader("InputType").ToString
            objInfo.Remark = reader("Remark").ToString

            objInfo.WIC_KnifeKType = reader("WIC_KnifeKType").ToString
            objInfo.WIC_PDType = reader("WIC_PDType").ToString
            objInfo.M_Unit = reader("M_Unit").ToString


            Return objInfo
        End Function



        ''' <summary>
        ''' ��WareInventoryCheck_Check�f��
        ''' </summary>
        ''' <param name="objFile1"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function KnifeWareInventoryCheck_Check(ByVal objFile1 As KnifeWareInventoryCheckInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("KnifeWareInventoryCheck_Check")
                db.AddInParameter(dbComm, "@WIC_NO", DbType.String, objFile1.WIC_NO)

                db.AddInParameter(dbComm, "@WIC_Check", DbType.String, objFile1.WIC_Check)
                db.AddInParameter(dbComm, "@WIC_CheckAction", DbType.String, objFile1.WIC_CheckAction)
                db.AddInParameter(dbComm, "@WIC_CheckType", DbType.String, objFile1.WIC_CheckType)
                db.AddInParameter(dbComm, "@WIC_CheckRemark", DbType.String, objFile1.WIC_CheckRemark)

                db.ExecuteNonQuery(dbComm)
                KnifeWareInventoryCheck_Check = True
            Catch ex As Exception
                MsgBox(ex.Message)
                KnifeWareInventoryCheck_Check = False
            End Try
        End Function
 

    End Class
End Namespace
