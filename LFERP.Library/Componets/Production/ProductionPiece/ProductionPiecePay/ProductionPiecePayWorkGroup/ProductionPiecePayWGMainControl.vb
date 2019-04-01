Imports System.Data.Common
Namespace LFERP.Library.ProductionPiecePayWGMain

    Public Class ProductionPiecePayWGMainControl
        ''' <summary>
        ''' �d�ߥX�էO�~���p��D�� 
        ''' </summary>
        ''' <param name="AutoID"></param>
        ''' <param name="PY_ID"></param>
        ''' <param name="G_NO"></param>
        ''' <param name="PY_YYMM"></param>
        ''' <param name="PY_CheckUserID"></param>
        ''' <param name="PY_AddUserID"></param>
        ''' <param name="DepID"></param>
        ''' <param name="FacID"></param>
        ''' <param name="Py_DateStart"></param>
        ''' <param name="Py_DateEnd"></param>
        ''' <param name="PY_Check"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ProductionPiecePayWGMain_GetList(ByVal AutoID As String, ByVal PY_ID As String, ByVal G_NO As String, ByVal PY_YYMM As String, ByVal PY_CheckUserID As String, _
                                                         ByVal PY_AddUserID As String, ByVal DepID As String, ByVal FacID As String, ByVal Py_DateStart As String, ByVal Py_DateEnd As String, ByVal PY_Check As String) As List(Of ProductionPiecePayWGMainInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbcomm As DbCommand = db.GetStoredProcCommand("ProductionPiecePayWGMain_GetList")

            db.AddInParameter(dbcomm, "@AutoID", DbType.String, AutoID) '�۰ʽs��ID
            db.AddInParameter(dbcomm, "@PY_ID", DbType.String, PY_ID) '�渹
            db.AddInParameter(dbcomm, "@G_NO", DbType.String, G_NO) '�էO�s��
            db.AddInParameter(dbcomm, "@PY_YYMM", DbType.String, PY_YYMM) '�~��
            db.AddInParameter(dbcomm, "@PY_CheckUserID", DbType.String, PY_CheckUserID) '�f�ֽs��

            db.AddInParameter(dbcomm, "@PY_AddUserID", DbType.String, PY_AddUserID) '�K�[�H(�ާ@�H)�s��
            db.AddInParameter(dbcomm, "@DepID", DbType.String, DepID) '����
            db.AddInParameter(dbcomm, "@FacID", DbType.String, FacID) '�t�O
            db.AddInParameter(dbcomm, "@Py_DateStart", DbType.String, Py_DateStart)
            db.AddInParameter(dbcomm, "@Py_DateEnd", DbType.String, Py_DateEnd)

            db.AddInParameter(dbcomm, "@PY_Check", DbType.String, PY_Check)

            Dim FeatureList As New List(Of ProductionPiecePayWGMainInfo)
            Using reader As IDataReader = db.ExecuteReader(dbcomm)
                While reader.Read
                    FeatureList.Add(FillProductionPiecePayWGMain(reader))
                End While
                Return FeatureList
            End Using
        End Function
        ''' <summary>
        ''' ���J�d�ߥX���ƾ�
        ''' </summary>
        ''' <param name="reader"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FillProductionPiecePayWGMain(ByVal reader As IDataReader) As ProductionPiecePayWGMainInfo
            Dim ai As New ProductionPiecePayWGMainInfo

            'G_NO_OUTSum = 0
            'G_NO_InSum = 0
            ''-------------------------------------------------------
            If reader("G_NO_OUTSum") Is DBNull.Value Then
                ai.G_NO_OUTSum = 0
            Else
                ai.G_NO_OUTSum = reader("G_NO_OUTSum").ToString
            End If

            If reader("G_NO_InSum") Is DBNull.Value Then
                ai.G_NO_InSum = 0
            Else
                ai.G_NO_InSum = reader("G_NO_InSum").ToString
            End If
            ''-------------------------------------------------------


            If reader("AutoID") Is DBNull.Value Then
                ai.AutoID = Nothing
            Else
                ai.AutoID = reader("AutoID").ToString     '�۽s��
            End If


            If reader("PY_G_Name") Is DBNull.Value Then
                ai.PY_G_Name = Nothing
            Else
                ai.PY_G_Name = reader("PY_G_Name").ToString    '�էO�W��
            End If


            If reader("PY_ID") Is DBNull.Value Then
                ai.PY_ID = Nothing
            Else
                ai.PY_ID = reader("PY_ID").ToString     '�渹
            End If

            If reader("G_NO") Is DBNull.Value Then
                ai.G_NO = Nothing
            Else
                ai.G_NO = reader("G_NO").ToString     '�էO�s��
            End If

            If reader("PY_YYMM") Is DBNull.Value Then
                ai.PY_YYMM = Nothing
            Else
                ai.PY_YYMM = reader("PY_YYMM").ToString     '�~��
            End If

            If reader("PY_CompleteSum") Is DBNull.Value Then
                ai.PY_CompleteSum = 0
            Else
                ai.PY_CompleteSum = reader("PY_CompleteSum").ToString      '�����`���B
            End If

            If reader("PY_UseSum") Is DBNull.Value Then
                ai.PY_UseSum = 0
            Else
                ai.PY_UseSum = reader("PY_UseSum").ToString      '�ӥ��`���B
            End If

            If reader("PY_TimeAllSum") Is DBNull.Value Then
                ai.PY_TimeAllSum = 0
            Else
                ai.PY_TimeAllSum = reader("PY_TimeAllSum").ToString      '�p���`���B
            End If

            If reader("PY_PieceAllSum") Is DBNull.Value Then
                ai.PY_PieceAllSum = 0
            Else
                ai.PY_PieceAllSum = reader("PY_PieceAllSum").ToString      '�p���`���B
            End If

            If reader("PY_TimeSum") Is DBNull.Value Then
                ai.PY_TimeSum = 0
            Else
                ai.PY_TimeSum = reader("PY_TimeSum")     '�p�ɪ��B
            End If

            If reader("PY_PieceSum") Is DBNull.Value Then
                ai.PY_PieceSum = 0
            Else
                ai.PY_PieceSum = reader("PY_PieceSum").ToString      '�p����B
            End If

            If reader("PY_CompensateSum") Is DBNull.Value Then
                ai.PY_CompensateSum = 0
            Else
                ai.PY_CompensateSum = reader("PY_CompensateSum").ToString      '���ɪ��B
            End If

            If reader("PY_SubtractSum") Is DBNull.Value Then
                ai.PY_SubtractSum = 0
            Else
                ai.PY_SubtractSum = reader("PY_SubtractSum").ToString      '�������B
            End If

            If reader("PY_BonusSum") Is DBNull.Value Then
                ai.PY_BonusSum = 0
            Else
                ai.PY_BonusSum = reader("PY_BonusSum").ToString      '�䥦���B
            End If

            If reader("PY_Remark") Is DBNull.Value Then
                ai.PY_Remark = Nothing
            Else
                ai.PY_Remark = reader("PY_Remark").ToString     '�ƪ`
            End If

            If reader("PY_Check") Is DBNull.Value Then
                ai.PY_Check = Nothing
            Else
                ai.PY_Check = reader("PY_Check").ToString     '�f��
            End If

            If reader("PY_CheckUserID") Is DBNull.Value Then
                ai.PY_CheckUserID = Nothing
            Else
                ai.PY_CheckUserID = reader("PY_CheckUserID").ToString     '�f�ֽs��
            End If

            If reader("PY_CheckDate") Is DBNull.Value Then
                ai.PY_CheckDate = Nothing
            Else
                ai.PY_CheckDate = reader("PY_CheckDate").ToString     '�f�֤��
            End If

            If reader("PY_AddUserID") Is DBNull.Value Then
                ai.PY_AddUserID = Nothing
            Else
                ai.PY_AddUserID = reader("PY_AddUserID").ToString     '�K�[�H(�ާ@�H)�s��
            End If

            If reader("PY_AddDate") Is DBNull.Value Then
                ai.PY_AddDate = Nothing
            Else
                ai.PY_AddDate = reader("PY_AddDate").ToString     '�K�[���
            End If

            If reader("PY_ModifyUserID") Is DBNull.Value Then
                ai.PY_ModifyUserID = Nothing
            Else
                ai.PY_ModifyUserID = reader("PY_ModifyUserID").ToString     '�ק�H
            End If

            If reader("PY_ModifyDate") Is DBNull.Value Then
                ai.PY_ModifyDate = Nothing
            Else
                ai.PY_ModifyDate = reader("PY_ModifyDate").ToString     '�ק���
            End If

            If reader("DepID") Is DBNull.Value Then
                ai.DepID = Nothing
            Else
                ai.DepID = reader("DepID").ToString     '����
            End If

            If reader("FacID") Is DBNull.Value Then
                ai.FacID = Nothing
            Else
                ai.FacID = reader("FacID").ToString     '�t�O
            End If

            If reader("PY_DepName") Is DBNull.Value Then
                ai.PY_DepName = Nothing
            Else
                ai.PY_DepName = reader("PY_DepName").ToString     '����
            End If

            If reader("PY_FacName") Is DBNull.Value Then
                ai.PY_FacName = Nothing
            Else
                ai.PY_FacName = reader("PY_FacName").ToString     '�t�O
            End If
            'PY_AddUserName

            If reader("PY_AddUserName") Is DBNull.Value Then
                ai.PY_AddUserName = Nothing
            Else
                ai.PY_AddUserName = reader("PY_AddUserName").ToString
            End If

            Return ai
        End Function
        ''' <summary>
        ''' ��s�էO�~���p��D��
        ''' </summary>
        ''' <param name="obj"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ProductionPiecePayWGMain_Update(ByVal obj As ProductionPiecePayWGMainInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbcomm As DbCommand = db.GetStoredProcCommand("ProductionPiecePayWGMain_Update")

                db.AddInParameter(dbcomm, "@PY_ID", DbType.String, obj.PY_ID) '�渹
                db.AddInParameter(dbcomm, "@G_NO", DbType.String, obj.G_NO) '�էO�s��
                db.AddInParameter(dbcomm, "@PY_YYMM", DbType.String, obj.PY_YYMM) '�~��
                db.AddInParameter(dbcomm, "@PY_CompleteSum", DbType.Double, obj.PY_CompleteSum) '�����`���B
                db.AddInParameter(dbcomm, "@PY_UseSum", DbType.Double, obj.PY_UseSum) '�ӥ��`���B

                db.AddInParameter(dbcomm, "@PY_TimeAllSum", DbType.Double, obj.PY_TimeAllSum) '�p���`���B
                db.AddInParameter(dbcomm, "@PY_PieceAllSum", DbType.Double, obj.PY_PieceAllSum) '�p���`���B

                db.AddInParameter(dbcomm, "@PY_CompensateSum", DbType.Double, obj.PY_CompensateSum) '���ɪ��B
                db.AddInParameter(dbcomm, "@PY_SubtractSum", DbType.Double, obj.PY_SubtractSum) '�������B
                db.AddInParameter(dbcomm, "@PY_BonusSum", DbType.Double, obj.PY_BonusSum) '�䥦���B

                db.AddInParameter(dbcomm, "@PY_Remark", DbType.String, obj.PY_Remark) '�ƪ`
                db.AddInParameter(dbcomm, "@PY_ModifyUserID", DbType.String, obj.PY_ModifyUserID) '�ק�H
                db.AddInParameter(dbcomm, "@PY_ModifyDate", DbType.String, obj.PY_ModifyDate) '�ק���
                db.AddInParameter(dbcomm, "@DepID", DbType.String, obj.DepID) '����
                db.AddInParameter(dbcomm, "@FacID", DbType.String, obj.FacID) '�t�O

                db.AddInParameter(dbcomm, "@PY_TimeSum", DbType.Double, obj.PY_TimeSum) '�p�ɪ��B
                db.AddInParameter(dbcomm, "@PY_PieceSum", DbType.Double, obj.PY_PieceSum) '�p����B

                db.AddInParameter(dbcomm, "@G_NO_InSum", DbType.Double, obj.G_NO_InSum) '�վ㦬�J
                db.AddInParameter(dbcomm, "@G_NO_OUTSum", DbType.Double, obj.G_NO_OUTSum) '�վ���X


                db.ExecuteNonQuery(dbcomm)
                ProductionPiecePayWGMain_Update = True
            Catch ex As Exception
                MsgBox(ex.Message)
                ProductionPiecePayWGMain_Update = False
            End Try
        End Function


        ''' <summary>
        ''' �W�[�էO�~���p��D��
        ''' </summary>
        ''' <param name="obj"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ProductionPiecePayWGMain_Add(ByVal obj As ProductionPiecePayWGMainInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbcomm As DbCommand = db.GetStoredProcCommand("ProductionPiecePayWGMain_Add")

                db.AddInParameter(dbcomm, "@PY_ID", DbType.String, obj.PY_ID) '�渹
                db.AddInParameter(dbcomm, "@G_NO", DbType.String, obj.G_NO) '�էO�s��
                db.AddInParameter(dbcomm, "@PY_YYMM", DbType.String, obj.PY_YYMM) '�~��
                db.AddInParameter(dbcomm, "@PY_CompleteSum", DbType.Double, obj.PY_CompleteSum) '�����`���B
                db.AddInParameter(dbcomm, "@PY_UseSum", DbType.Double, obj.PY_UseSum) '�ӥ��`���B

                db.AddInParameter(dbcomm, "@PY_TimeAllSum", DbType.Double, obj.PY_TimeAllSum) '�p���`���B
                db.AddInParameter(dbcomm, "@PY_PieceAllSum", DbType.Double, obj.PY_PieceAllSum) '�p���`���B
                db.AddInParameter(dbcomm, "@PY_CompensateSum", DbType.Double, obj.PY_CompensateSum) '���ɪ��B
                db.AddInParameter(dbcomm, "@PY_SubtractSum", DbType.Double, obj.PY_SubtractSum) '�������B
                db.AddInParameter(dbcomm, "@PY_BonusSum", DbType.Double, obj.PY_BonusSum) '�䥦���B

                db.AddInParameter(dbcomm, "@PY_Remark", DbType.String, obj.PY_Remark) '�ƪ`
                db.AddInParameter(dbcomm, "@PY_Check", DbType.String, obj.PY_Check) '�f��
                db.AddInParameter(dbcomm, "@PY_AddUserID", DbType.String, obj.PY_AddUserID) '�K�[�H(�ާ@�H)�s��
                db.AddInParameter(dbcomm, "@PY_AddDate", DbType.String, obj.PY_AddDate) '�K�[���
                db.AddInParameter(dbcomm, "@DepID", DbType.String, obj.DepID) '����

                db.AddInParameter(dbcomm, "@FacID", DbType.String, obj.FacID) '�t�O
                db.AddInParameter(dbcomm, "@PY_TimeSum", DbType.Double, obj.PY_TimeSum) '�p�ɪ��B
                db.AddInParameter(dbcomm, "@PY_PieceSum", DbType.Double, obj.PY_PieceSum) '�p����B

                db.AddInParameter(dbcomm, "@G_NO_InSum", DbType.Double, obj.G_NO_InSum) '�վ㦬�J
                db.AddInParameter(dbcomm, "@G_NO_OUTSum", DbType.Double, obj.G_NO_OUTSum) '�վ���X



                db.ExecuteNonQuery(dbcomm)
                ProductionPiecePayWGMain_Add = True
            Catch ex As Exception
                MsgBox(ex.Message)
                ProductionPiecePayWGMain_Add = False
            End Try
        End Function
        ''' <summary>
        ''' �R���էO�~���D��O��
        ''' </summary>
        ''' <param name="AutoID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ProductionPiecePayWGMain_Delete(ByVal AutoID As String) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbcomm As DbCommand = db.GetStoredProcCommand("ProductionPiecePayWGMain_Delete")

                db.AddInParameter(dbcomm, "@AutoID", DbType.String, AutoID)

                db.ExecuteNonQuery(dbcomm)
                ProductionPiecePayWGMain_Delete = True
            Catch ex As Exception
                MsgBox(ex.Message)
                ProductionPiecePayWGMain_Delete = False
            End Try
        End Function


        ''' <summary>
        ''' ��s �էO�~���p�� �f��
        ''' </summary>
        ''' <param name="obj"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ProductionPiecePayWGMain_Updatecheck(ByVal obj As ProductionPiecePayWGMainInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbcomm As DbCommand = db.GetStoredProcCommand("ProductionPiecePayWGMain_Updatecheck")

                db.AddInParameter(dbcomm, "@PY_ID", DbType.String, obj.PY_ID) '�渹
                db.AddInParameter(dbcomm, "@PY_Check", DbType.Boolean, obj.PY_Check) ' /�f��
                db.AddInParameter(dbcomm, "@PY_CheckUserID", DbType.String, obj.PY_CheckUserID)
                db.AddInParameter(dbcomm, "@PY_CheckDate", DbType.String, obj.PY_CheckDate)


                db.ExecuteNonQuery(dbcomm)
                ProductionPiecePayWGMain_Updatecheck = True
            Catch ex As Exception
                MsgBox(ex.Message)
                ProductionPiecePayWGMain_Updatecheck = False
            End Try
        End Function

        ''' <summary>
        ''' ����էO�~���D��s��
        ''' </summary>
        ''' <param name="Ndate"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ProductionPiecePayWGMain_GetNO(ByVal Ndate As String) As ProductionPiecePayWGMainInfo
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("ProductionPiecePayWGMain_GetNO")
            db.AddInParameter(dbComm, "@NDate", DbType.String, Ndate)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    Return FillProductionPiecePayWGMain1(reader)
                End While
                Return Nothing
            End Using
        End Function

        ''' <summary>
        ''' ���J  �էO�~���D��s�� 
        ''' </summary>
        ''' <param name="reader"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FillProductionPiecePayWGMain1(ByVal reader As IDataReader) As ProductionPiecePayWGMainInfo
            Dim ai1 As New ProductionPiecePayWGMainInfo

            If reader("PY_ID") Is DBNull.Value Then
                ai1.PY_ID = Nothing
            Else
                ai1.PY_ID = reader("PY_ID").ToString     '�渹
            End If

            Return ai1
        End Function


        ''' <summary>
        ''' �d�߲έp�X���w�էO�Y�@����p���`�B(����)
        ''' </summary>
        ''' <param name="G_NO"></param>
        ''' <param name="GP_DateStart"></param>
        ''' <param name="GP_DateEnd"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>

        Public Function ProductionSumPieceWorkGroupView(ByVal G_NO As String, ByVal GP_DateStart As String, ByVal GP_DateEnd As String) As ProductionPiecePayWGMainInfo
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("ProductionSumPieceWorkGroupView")

            db.AddInParameter(dbComm, "@G_NO", DbType.String, G_NO) '�էO�s��
            db.AddInParameter(dbComm, "@GP_DateStart", DbType.String, GP_DateStart)
            db.AddInParameter(dbComm, "@GP_DateEnd", DbType.String, GP_DateEnd)

            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    Return FillProductionSumPieceWorkGroupView(reader)
                End While
                Return Nothing
            End Using
        End Function
        ''' <summary>
        '''  ���J  �d�߲έp�X���w�էO�Y�@����p���`�B(����)
        ''' </summary>
        ''' <param name="reader"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FillProductionSumPieceWorkGroupView(ByVal reader As IDataReader) As ProductionPiecePayWGMainInfo
            Dim ai1 As New ProductionPiecePayWGMainInfo
            If reader("PWGtotal_P") Is DBNull.Value Then
                ai1.PWGtotal_P = 0
            Else
                ai1.PWGtotal_P = reader("PWGtotal_P")
            End If
            Return ai1
        End Function
        ''' <summary>
        ''' �d�߲έp�X���w�էO�Y�@����p���`�B(����)
        ''' </summary>
        ''' <param name="G_NO"></param>
        ''' <param name="GT_DateStart"></param>
        ''' <param name="GT_DateEnd"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ProductionSumTimeWorkGroupView(ByVal G_NO As String, ByVal GT_DateStart As String, ByVal GT_DateEnd As String) As ProductionPiecePayWGMainInfo
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbcomm As DbCommand = db.GetStoredProcCommand("ProductionSumTimeWorkGroupView")

            db.AddInParameter(dbcomm, "@G_NO", DbType.String, G_NO) '�էO�s��
            db.AddInParameter(dbcomm, "@GT_DateStart", DbType.String, GT_DateStart)
            db.AddInParameter(dbcomm, "@GT_DateEnd", DbType.String, GT_DateEnd)

            Using reader As IDataReader = db.ExecuteReader(dbcomm)
                While reader.Read
                    Return FillProductionSumTimeWorkGroupView(reader)
                End While
                Return Nothing
            End Using
        End Function
        ''' <summary>
        '''  ���J  �d�߲έp�X���w�էO�Y�@����p���`�B(����)
        ''' </summary>
        ''' <param name="reader"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FillProductionSumTimeWorkGroupView(ByVal reader As IDataReader) As ProductionPiecePayWGMainInfo
            Dim ai1 As New ProductionPiecePayWGMainInfo
            If reader("PWGtotal_T") Is DBNull.Value Then
                ai1.PWGtotal_T = 0
            Else
                ai1.PWGtotal_T = reader("PWGtotal_T")
            End If
            Return ai1
        End Function


    End Class

End Namespace