Namespace LFERP.DataSetting
    ''' <summary>
    ''' �Ȥ���
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CusterControler
        Public Function GetCusterList(ByVal CusterID As String, ByVal EngName As String, ByVal ChsName As String) As List(Of CusterInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("Custer_GetList")
            db.AddInParameter(dbComm, "@C_CusterID", DbType.String, CusterID)
            db.AddInParameter(dbComm, "@C_EngName", DbType.String, EngName)
            db.AddInParameter(dbComm, "@C_ChsName", DbType.String, ChsName)
            Dim FeatureList As New List(Of CusterInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillCuster(reader))
                End While
                Return FeatureList
            End Using
        End Function
        ''' <summary>
        ''' �W�[�O��
        ''' </summary>
        Public Function Custer_Add(ByVal objinfo As CusterInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("Custer_Add")

                db.AddInParameter(dbComm, "C_CusterID", DbType.String, objinfo.C_CusterID)
                db.AddInParameter(dbComm, "C_EngName", DbType.String, objinfo.C_EngName)
                db.AddInParameter(dbComm, "C_ChsName", DbType.String, objinfo.C_ChsName)
                db.AddInParameter(dbComm, "C_Link", DbType.String, objinfo.C_Link)
                db.AddInParameter(dbComm, "C_LinkTel", DbType.String, objinfo.C_LinkTel)
                db.AddInParameter(dbComm, "C_Fax", DbType.String, objinfo.C_Fax)
                db.AddInParameter(dbComm, "C_AddDate", DbType.Date, CDate(objinfo.C_AddDate))
                db.AddInParameter(dbComm, "C_Adder1", DbType.String, objinfo.C_Adder1)
                db.AddInParameter(dbComm, "C_Adder2", DbType.String, objinfo.C_Adder2)
                db.AddInParameter(dbComm, "C_Adder3", DbType.String, objinfo.C_Adder3)
                db.AddInParameter(dbComm, "C_Adder4", DbType.String, objinfo.C_Adder4)
                db.AddInParameter(dbComm, "C_Department", DbType.String, objinfo.C_Department)
                db.AddInParameter(dbComm, "C_Email", DbType.String, objinfo.C_Email)
 
                db.ExecuteNonQuery(dbComm)
                Custer_Add = True
            Catch ex As Exception
                MsgBox(ex.Message)
                Custer_Add = False
            End Try
        End Function
        ''' <summary>
        ''' �W�[�O��
        ''' </summary>
        Public Function Custer_Update(ByVal objinfo As CusterInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("Custer_Update")

                db.AddInParameter(dbComm, "C_CusterID", DbType.String, objinfo.C_CusterID)
                db.AddInParameter(dbComm, "C_EngName", DbType.String, objinfo.C_EngName)
                db.AddInParameter(dbComm, "C_ChsName", DbType.String, objinfo.C_ChsName)
                db.AddInParameter(dbComm, "C_Link", DbType.String, objinfo.C_Link)
                db.AddInParameter(dbComm, "C_LinkTel", DbType.String, objinfo.C_LinkTel)
                db.AddInParameter(dbComm, "C_Fax", DbType.String, objinfo.C_Fax)
                db.AddInParameter(dbComm, "C_AddDate", DbType.Date, CDate(objinfo.C_AddDate))
                db.AddInParameter(dbComm, "C_Adder1", DbType.String, objinfo.C_Adder1)
                db.AddInParameter(dbComm, "C_Adder2", DbType.String, objinfo.C_Adder2)
                db.AddInParameter(dbComm, "C_Adder3", DbType.String, objinfo.C_Adder3)
                db.AddInParameter(dbComm, "C_Adder4", DbType.String, objinfo.C_Adder4)
                db.AddInParameter(dbComm, "C_Department", DbType.String, objinfo.C_Department)
                db.AddInParameter(dbComm, "C_Email", DbType.String, objinfo.C_Email)

                db.ExecuteNonQuery(dbComm)
                Custer_Update = True
            Catch ex As Exception
                MsgBox(ex.Message)
                Custer_Update = False
            End Try
        End Function

        ''' <summary>
        ''' �Ȥ��ƲK�[
        ''' 2014-05-13
        ''' �B���Q
        ''' </summary>
        ''' <param name="objinfo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Custer_Add2(ByVal objinfo As CusterInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("Custer_Add2")

                db.AddInParameter(dbComm, "C_CusterID", DbType.String, objinfo.C_CusterID)
                db.AddInParameter(dbComm, "C_EngName", DbType.String, objinfo.C_EngName)
                db.AddInParameter(dbComm, "C_ChsName", DbType.String, objinfo.C_ChsName)
                db.AddInParameter(dbComm, "C_Link", DbType.String, objinfo.C_Link)
                db.AddInParameter(dbComm, "C_LinkTel", DbType.String, objinfo.C_LinkTel)
                db.AddInParameter(dbComm, "C_Fax", DbType.String, objinfo.C_Fax)
                db.AddInParameter(dbComm, "C_AddDate", DbType.Date, CDate(objinfo.C_AddDate))
                db.AddInParameter(dbComm, "C_Adder1", DbType.String, objinfo.C_Adder1)
                db.AddInParameter(dbComm, "C_Adder2", DbType.String, objinfo.C_Adder2)
                db.AddInParameter(dbComm, "C_Adder3", DbType.String, objinfo.C_Adder3)
                db.AddInParameter(dbComm, "C_Adder4", DbType.String, objinfo.C_Adder4)
                db.AddInParameter(dbComm, "C_Department", DbType.String, objinfo.C_Department)
                db.AddInParameter(dbComm, "C_Email", DbType.String, objinfo.C_Email)
                db.AddInParameter(dbComm, "CustomerType", DbType.String, objinfo.CustomerType)

                db.ExecuteNonQuery(dbComm)
                Custer_Add2 = True
            Catch ex As Exception
                MsgBox(ex.Message)
                Custer_Add2 = False
            End Try
        End Function

        ''' <summary>
        ''' �Ȥ��ƭק�
        ''' 2014-05-13
        ''' �B���Q
        ''' </summary>
        ''' <param name="objinfo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Custer_Update2(ByVal objinfo As CusterInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("Custer_Update2")

                db.AddInParameter(dbComm, "C_CusterID", DbType.String, objinfo.C_CusterID)
                db.AddInParameter(dbComm, "C_EngName", DbType.String, objinfo.C_EngName)
                db.AddInParameter(dbComm, "C_ChsName", DbType.String, objinfo.C_ChsName)
                db.AddInParameter(dbComm, "C_Link", DbType.String, objinfo.C_Link)
                db.AddInParameter(dbComm, "C_LinkTel", DbType.String, objinfo.C_LinkTel)
                db.AddInParameter(dbComm, "C_Fax", DbType.String, objinfo.C_Fax)
                db.AddInParameter(dbComm, "C_AddDate", DbType.Date, CDate(objinfo.C_AddDate))
                db.AddInParameter(dbComm, "C_Adder1", DbType.String, objinfo.C_Adder1)
                db.AddInParameter(dbComm, "C_Adder2", DbType.String, objinfo.C_Adder2)
                db.AddInParameter(dbComm, "C_Adder3", DbType.String, objinfo.C_Adder3)
                db.AddInParameter(dbComm, "C_Adder4", DbType.String, objinfo.C_Adder4)
                db.AddInParameter(dbComm, "C_Department", DbType.String, objinfo.C_Department)
                db.AddInParameter(dbComm, "C_Email", DbType.String, objinfo.C_Email)
                db.AddInParameter(dbComm, "CustomerType", DbType.String, objinfo.CustomerType)
                db.ExecuteNonQuery(dbComm)
                Custer_Update2 = True
            Catch ex As Exception
                MsgBox(ex.Message)
                Custer_Update2 = False
            End Try
        End Function

        ''' <summary>
        ''' ����Ȥ���
        ''' 2014-05-13
        ''' �B���Q
        ''' </summary>
        ''' <param name="CusterID"></param>
        ''' <param name="EngName"></param>
        ''' <param name="ChsName"></param>
        ''' <param name="CusPO"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetCusterList2(ByVal CusterID As String, ByVal EngName As String, ByVal ChsName As String, ByVal CustomerType As String) As List(Of CusterInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("Custer_GetList2")
            db.AddInParameter(dbComm, "@C_CusterID", DbType.String, CusterID)
            db.AddInParameter(dbComm, "@C_EngName", DbType.String, EngName)
            db.AddInParameter(dbComm, "@C_ChsName", DbType.String, ChsName)
            db.AddInParameter(dbComm, "CustomerType", DbType.String, CustomerType)
            Dim FeatureList As New List(Of CusterInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillCuster(reader))
                End While
                Return FeatureList
            End Using
        End Function

        Public Function Custer_GetNo(ByVal C_CusterID As String) As CusterInfo
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("Custer_GetNo")
            db.AddInParameter(dbComm, "@C_CusterID", DbType.String, C_CusterID)
            Dim FeatureList As New CusterInfo
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.C_CusterID = reader("�Ȥ�N��").ToString
                End While
                Return FeatureList
            End Using
        End Function
        ''' <summary>
        ''' �R���O��
        ''' </summary>
        Public Function GetCuster_Delete(ByVal C_CusterID As String) As Boolean
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("Custer_Delete")
            db.AddInParameter(dbComm, "@C_CusterID", DbType.String, C_CusterID)
            Return db.ExecuteNonQuery(dbComm)
        End Function

        Friend Function FillCuster(ByVal reader As IDataReader) As CusterInfo
            '�������o���ƾ�
            On Error Resume Next
            Dim objInfo As New CusterInfo
            objInfo.C_CusterID = reader("�Ȥ�N��").ToString
            objInfo.C_EngName = reader("�^��W").ToString
            objInfo.C_ChsName = reader("����W").ToString
            objInfo.C_Link = reader("�p�t�H").ToString
            objInfo.C_LinkTel = reader("�p�t�q��").ToString
            objInfo.C_Fax = reader("Fax_no").ToString
            objInfo.C_AddDate = CDate(reader("�إ���").ToString)
            objInfo.C_Adder1 = reader("�Ȥ�a�}1").ToString
            objInfo.C_Adder2 = reader("�Ȥ�a�}2").ToString
            objInfo.C_Adder3 = reader("�Ȥ�a�}3").ToString
            objInfo.C_Adder4 = reader("�Ȥ�a�}4").ToString
            objInfo.C_Department = reader("����").ToString
            objInfo.C_Email = reader("Email").ToString
            If IsDBNull(reader("CustomerType")) = False Then
                objInfo.CustomerType = reader("CustomerType").ToString
            End If
            Return objInfo
        End Function
    End Class
End Namespace

