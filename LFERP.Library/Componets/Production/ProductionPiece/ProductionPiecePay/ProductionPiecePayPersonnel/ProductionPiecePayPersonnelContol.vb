Imports System.Data.Common
Namespace LFERP.Library.ProductionPiecePayPersonnel

    Public Class ProductionPiecePayPersonnelContol
        ''' <summary>
        ''' �ӤH�p���~�����` �d��
        ''' </summary>
        ''' <param name="AutoID"></param>
        ''' <param name="Per_NO"></param>
        ''' <param name="Per_Name"></param>
        ''' <param name="PL_YYMM"></param>
        ''' <param name="DepID"></param>
        ''' <param name="PL_Check"></param>
        ''' <param name="PL_CheckUserID"></param>
        ''' <param name="PL_AddUserID"></param>
        ''' <param name="PL_ModifyUserID"></param>
        ''' <param name="PL_DateStart">�w�若�L �}�l�ɶ�</param>
        ''' <param name="PL_DateEnd">�w�若�L �����ɶ�</param>
        ''' <param name="Print_Action">�w�若�L �H�W</param>
        ''' <returns></returns>
        ''' <remarks></remarks>

        Public Function ProductionPiecePayPersonnel_GetList(ByVal AutoID As String, ByVal Per_NO As String, ByVal Per_Name As String, ByVal PL_YYMM As String, ByVal DepID As String, ByVal PL_Check As String, _
                                                    ByVal PL_CheckUserID As String, ByVal PL_AddUserID As String, ByVal PL_ModifyUserID As String, ByVal PL_DateStart As String, ByVal PL_DateEnd As String, ByVal Print_Action As String, ByVal FacID As String, ByVal Per_Class As String) As List(Of ProductionPiecePayPersonnelInfo)

            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("ProductionPiecePayPersonnel_GetList")

            db.AddInParameter(dbComm, "@AutoID", DbType.String, AutoID) ' 
            db.AddInParameter(dbComm, "@Per_NO", DbType.String, Per_NO) '
            db.AddInParameter(dbComm, "@Per_Name", DbType.String, Per_Name) '
            db.AddInParameter(dbComm, "@PL_YYMM", DbType.String, PL_YYMM) '-- /�����s��

            db.AddInParameter(dbComm, "@DepID", DbType.String, DepID) '-- /�t�O
            db.AddInParameter(dbComm, "@PL_Check", DbType.String, PL_Check) '--/�إߤ��
            db.AddInParameter(dbComm, "@PL_CheckUserID", DbType.String, PL_CheckUserID) ' --/�ާ@�H
            db.AddInParameter(dbComm, "@PL_AddUserID", DbType.String, PL_AddUserID) '  --> < =
            db.AddInParameter(dbComm, "@PL_ModifyUserID", DbType.String, PL_ModifyUserID)

            db.AddInParameter(dbComm, "@PL_DateStart", DbType.String, PL_DateStart) '  --> < =
            db.AddInParameter(dbComm, "@PL_DateEnd", DbType.String, PL_DateEnd)
            db.AddInParameter(dbComm, "@FacID", DbType.String, FacID)

            'Per_Class
            db.AddInParameter(dbComm, "@Per_Class", DbType.String, Per_Class)

            Dim FeatureList As New List(Of ProductionPiecePayPersonnelInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillProductionPiecePayPersonnel(reader, PL_DateStart, PL_DateEnd, Print_Action))
                End While
                Return FeatureList
            End Using

        End Function
        ''' <summary>
        '''  ���J�ƾ� �ӤH�p���~�����`
        ''' </summary>
        ''' <param name="reader"></param>
        ''' <param name="FPL_DateStart"></param>
        ''' <param name="FPL_DateEnd"></param>
        ''' <param name="FPrint_Action"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FillProductionPiecePayPersonnel(ByVal reader As IDataReader, ByVal FPL_DateStart As String, ByVal FPL_DateEnd As String, ByVal FPrint_Action As String) As ProductionPiecePayPersonnelInfo
            Dim ai As New ProductionPiecePayPersonnelInfo

            ''�u�Φb���L��
            ai.PL_DateStart = FPL_DateStart
            ai.PL_DateEnd = FPL_DateEnd
            ai.Print_Action = FPrint_Action

            If reader("Per_Class") Is DBNull.Value Then
                ai.Per_Class = Nothing
            Else
                ai.Per_Class = reader("Per_Class").ToString  '      
            End If

            If reader("AutoID") Is DBNull.Value Then
                ai.AutoID = Nothing
            Else
                ai.AutoID = reader("AutoID").ToString  '       ' * int                 /�۰ʽs��ID
            End If

            If reader("Per_NO") Is DBNull.Value Then
                ai.Per_NO = Nothing
            Else
                ai.Per_NO = reader("Per_NO").ToString  '         ' * nvarchar(20)        /�t�ҽs��
            End If

            If reader("Per_Name") Is DBNull.Value Then
                ai.Per_Name = Nothing
            Else
                ai.Per_Name = reader("Per_Name").ToString  '          ' * nvarchar(20)        /�m�W
            End If

            If reader("PL_YYMM") Is DBNull.Value Then
                ai.PL_YYMM = Nothing
            Else
                ai.PL_YYMM = reader("PL_YYMM").ToString  '          '* datetime            /�~��
            End If

            If reader("DepID") Is DBNull.Value Then
                ai.DepID = Nothing
            Else
                ai.DepID = reader("DepID").ToString  '         '* nvarchar(10)        /����
            End If

            If reader("Per_DayPrice") Is DBNull.Value Then
                ai.Per_DayPrice = 0
            Else
                ai.Per_DayPrice = FormatNumber(reader("Per_DayPrice"), 1, TriState.True)  '     '* real                /���~  
            End If
            ''-------------------------------------------------------------------------
            If reader("PL_OnDutyDays") Is DBNull.Value Then
                ai.PL_OnDutyDays = 0
            Else
                ai.PL_OnDutyDays = reader("PL_OnDutyDays")  '     '* real                  /�W�Z�Ѽ�
            End If

            If reader("PL_UsualOverHours") Is DBNull.Value Then
                ai.PL_UsualOverHours = 0
            Else
                ai.PL_UsualOverHours = reader("PL_UsualOverHours")   '     ' * real                  /���ɥ[�Z�p�ɼ�
            End If

            If reader("PL_HolidayOVerHours") Is DBNull.Value Then
                ai.PL_HolidayOVerHours = 0
            Else
                ai.PL_HolidayOVerHours = reader("PL_HolidayOVerHours")   '     ' * real                     /�`����[�Z�p�ɼ�
            End If

            If reader("PL_TimeHours") Is DBNull.Value Then
                ai.PL_TimeHours = 0
            Else
                ai.PL_TimeHours = reader("PL_TimeHours").ToString   '       ' * real                  /�p�ɤu��
            End If

            If reader("PL_CompensateSum") Is DBNull.Value Then
                ai.PL_CompensateSum = 0
            Else
                ai.PL_CompensateSum = reader("PL_CompensateSum")  '      ' * real                  /���ɪ��B
            End If

            ''---------------------------------------------------------------------------------------------------

            If reader("PL_SubtractSum") Is DBNull.Value Then
                ai.PL_SubtractSum = 0
            Else
                ai.PL_SubtractSum = reader("PL_SubtractSum")  '          '* real                /�������B
            End If

            If reader("PL_TimePay") Is DBNull.Value Then
                ai.PL_TimePay = 0
            Else
                ai.PL_TimePay = reader("PL_TimePay").ToString   '          '* real                /�p�ɤu��
            End If

            If reader("PL_PiecePay") Is DBNull.Value Then
                ai.PL_PiecePay = 0
            Else
                ai.PL_PiecePay = reader("PL_PiecePay").ToString   '         '* real                /�p��u��
            End If

            If reader("PL_MeritedPay") Is DBNull.Value Then
                ai.PL_MeritedPay = 0
            Else
                ai.PL_MeritedPay = reader("PL_MeritedPay").ToString   '       '* real                /���o�u��
            End If

            If reader("PL_Remark") Is DBNull.Value Then
                ai.PL_Remark = Nothing
            Else
                ai.PL_Remark = reader("PL_Remark").ToString   '         ' * nvarchar(MAX)       /�ƪ`
            End If
            ''----------------------------------------------------------------
            If reader("PL_Check") Is DBNull.Value Then
                ai.PL_Check = Nothing
            Else
                ai.PL_Check = reader("PL_Check").ToString   '    '* bit                   /�f��
            End If

            If reader("PL_CheckUserID") Is DBNull.Value Then
                ai.PL_CheckUserID = Nothing
            Else
                ai.PL_CheckUserID = reader("PL_CheckUserID").ToString    'nvarchar(20)          /�f�ֽs��
            End If

            If reader("PL_CheckDate") Is DBNull.Value Then
                ai.PL_CheckDate = Nothing
            Else
                ai.PL_CheckDate = reader("PL_CheckDate").ToString      ' * datetime              /�f�֤��
            End If

            If reader("PL_AddUserID") Is DBNull.Value Then
                ai.PL_AddUserID = Nothing
            Else
                ai.PL_AddUserID = reader("PL_AddUserID").ToString      '  * nvarchar(20)          /�K�[�H(�ާ@�H)�s��
            End If

            If reader("PL_AddDate") Is DBNull.Value Then
                ai.PL_AddDate = Nothing
            Else
                ai.PL_AddDate = reader("PL_AddDate").ToString       '* datetime             /�K�[���
            End If
            ''---------------------------------------------------------------------------------------------------------
            If reader("PL_ModifyUserID") Is DBNull.Value Then
                ai.PL_ModifyUserID = Nothing
            Else
                ai.PL_ModifyUserID = reader("PL_ModifyUserID").ToString       '* nvarchar             /�ק�H
            End If

            If reader("PL_ModifyDate") Is DBNull.Value Then
                ai.PL_ModifyDate = Nothing
            Else
                ai.PL_ModifyDate = reader("PL_ModifyDate").ToString       ' * datetime             /�ק���
            End If


            If reader("PL_AddUserName") Is DBNull.Value Then
                ai.PL_AddUserName = Nothing
            Else
                ai.PL_AddUserName = reader("PL_AddUserName").ToString       '  �O���K�[�H���W
            End If

            If reader("PL_CheckUserName") Is DBNull.Value Then
                ai.PL_CheckUserName = Nothing
            Else
                ai.PL_CheckUserName = reader("PL_CheckUserName").ToString     '   �f�֤H���W
            End If

            If reader("PL_ModifyUserName") Is DBNull.Value Then
                ai.PL_ModifyUserName = Nothing
            Else
                ai.PL_ModifyUserName = reader("PL_ModifyUserName").ToString     '   �ק�H���W
            End If

            If reader("PL_DepName") Is DBNull.Value Then
                ai.PL_DepName = Nothing
            Else
                ai.PL_DepName = reader("PL_DepName").ToString     '   �����W
            End If

            If reader("FacID") Is DBNull.Value Then
                ai.FacID = Nothing
            Else
                ai.FacID = reader("FacID").ToString     '   �t�O
            End If

            If reader("PL_FacName") Is DBNull.Value Then
                ai.PL_FacName = Nothing
            Else
                ai.PL_FacName = reader("PL_FacName").ToString     '   �t�O�W
            End If

            Return ai

        End Function
        ''' <summary>
        ''' �ӤH�p���~�����` �W�[
        ''' </summary>
        ''' <param name="obj"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ProductionPiecePayPersonnel_Add(ByVal obj As ProductionPiecePayPersonnelInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbcomm As DbCommand = db.GetStoredProcCommand("ProductionPiecePayPersonnel_Add")

                db.AddInParameter(dbcomm, "@Per_NO", DbType.String, obj.Per_NO)
                db.AddInParameter(dbcomm, "@Per_Name", DbType.String, obj.Per_Name)
                db.AddInParameter(dbcomm, "@PL_YYMM", DbType.String, obj.PL_YYMM) ' 
                db.AddInParameter(dbcomm, "@DepID", DbType.String, obj.DepID)
                db.AddInParameter(dbcomm, "@Per_DayPrice", DbType.Double, obj.Per_DayPrice) ' 

                db.AddInParameter(dbcomm, "@PL_OnDutyDays", DbType.Double, obj.PL_OnDutyDays)
                db.AddInParameter(dbcomm, "@PL_UsualOverHours", DbType.Double, obj.PL_UsualOverHours) ' - 
                db.AddInParameter(dbcomm, "@PL_HolidayOVerHours", DbType.Double, obj.PL_HolidayOVerHours) ' --
                db.AddInParameter(dbcomm, "@PL_TimeHours", DbType.Double, obj.PL_TimeHours) '  --
                db.AddInParameter(dbcomm, "@PL_CompensateSum", DbType.Double, obj.PL_CompensateSum) '

                db.AddInParameter(dbcomm, "@PL_SubtractSum", DbType.Double, obj.PL_SubtractSum)
                db.AddInParameter(dbcomm, "@PL_TimePay", DbType.Double, obj.PL_TimePay) ' - 
                db.AddInParameter(dbcomm, "@PL_PiecePay", DbType.Double, obj.PL_PiecePay) ' --
                db.AddInParameter(dbcomm, "@PL_MeritedPay", DbType.Double, obj.PL_MeritedPay) '  --
                db.AddInParameter(dbcomm, "@PL_Remark", DbType.String, obj.PL_Remark)

                db.AddInParameter(dbcomm, "@PL_AddUserID", DbType.String, obj.PL_AddUserID)
                db.AddInParameter(dbcomm, "@PL_AddDate", DbType.String, obj.PL_AddDate)
                db.AddInParameter(dbcomm, "@FacID", DbType.String, obj.FacID) '

                db.AddInParameter(dbcomm, "@PL_Check", DbType.String, obj.PL_Check) '@PL_Check

                db.AddInParameter(dbcomm, "@Per_Class", DbType.String, obj.Per_Class) '


                db.ExecuteNonQuery(dbcomm)
                ProductionPiecePayPersonnel_Add = True

            Catch ex As Exception
                MsgBox(ex.Message)
                ProductionPiecePayPersonnel_Add = False
            End Try
        End Function

        ''' <summary>
        ''' �ӤH�p���~�����` ��s
        ''' </summary>
        ''' <param name="obj"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ProductionPiecePayPersonnel_Update(ByVal obj As ProductionPiecePayPersonnelInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbcomm As DbCommand = db.GetStoredProcCommand("ProductionPiecePayPersonnel_Update")

                db.AddInParameter(dbcomm, "@AutoID", DbType.String, obj.AutoID) '
                db.AddInParameter(dbcomm, "@Per_NO", DbType.String, obj.Per_NO) '
                db.AddInParameter(dbcomm, "@Per_Name", DbType.String, obj.Per_Name)
                db.AddInParameter(dbcomm, "@PL_YYMM", DbType.String, obj.PL_YYMM)
                db.AddInParameter(dbcomm, "@DepID", DbType.String, obj.DepID)
                db.AddInParameter(dbcomm, "@Per_DayPrice", DbType.Double, obj.Per_DayPrice)

                db.AddInParameter(dbcomm, "@PL_OnDutyDays", DbType.Double, obj.PL_OnDutyDays)
                db.AddInParameter(dbcomm, "@PL_UsualOverHours", DbType.Double, obj.PL_UsualOverHours)
                db.AddInParameter(dbcomm, "@PL_HolidayOVerHours", DbType.Double, obj.PL_HolidayOVerHours)
                db.AddInParameter(dbcomm, "@PL_TimeHours", DbType.Double, obj.PL_TimeHours)
                db.AddInParameter(dbcomm, "@PL_CompensateSum", DbType.Double, obj.PL_CompensateSum)

                db.AddInParameter(dbcomm, "@PL_SubtractSum", DbType.Double, obj.PL_SubtractSum)
                db.AddInParameter(dbcomm, "@PL_TimePay", DbType.Double, obj.PL_TimePay)
                db.AddInParameter(dbcomm, "@PL_PiecePay", DbType.Double, obj.PL_PiecePay)
                db.AddInParameter(dbcomm, "@PL_MeritedPay", DbType.Double, obj.PL_MeritedPay)
                db.AddInParameter(dbcomm, "@PL_Remark", DbType.String, obj.PL_Remark)

                db.AddInParameter(dbcomm, "@PL_ModifyUserID", DbType.String, obj.PL_ModifyUserID)
                db.AddInParameter(dbcomm, "@PL_ModifyDate", DbType.String, obj.PL_ModifyDate)
                db.AddInParameter(dbcomm, "@FacID", DbType.String, obj.FacID)

                db.AddInParameter(dbcomm, "@Per_Class", DbType.String, obj.Per_Class) '

                db.ExecuteNonQuery(dbcomm)
                ProductionPiecePayPersonnel_Update = True

            Catch ex As Exception
                MsgBox(ex.Message)
                ProductionPiecePayPersonnel_Update = False
            End Try
        End Function
        ''' <summary>
        ''' �ӤH�p���~�����` �f�֧�s
        ''' </summary>
        ''' <param name="obj"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ProductionPiecePayPersonnel_Updatecheck(ByVal obj As ProductionPiecePayPersonnelInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbcomm As DbCommand = db.GetStoredProcCommand("ProductionPiecePayPersonnel_Updatecheck")

                db.AddInParameter(dbcomm, "@AutoID", DbType.String, obj.AutoID) '
                db.AddInParameter(dbcomm, "@PL_Check", DbType.String, obj.PL_Check) '
                db.AddInParameter(dbcomm, "@PL_CheckUserID", DbType.String, obj.PL_CheckUserID)
                db.AddInParameter(dbcomm, "@PL_CheckDate", DbType.String, obj.PL_CheckDate)

                db.ExecuteNonQuery(dbcomm)
                ProductionPiecePayPersonnel_Updatecheck = True

            Catch ex As Exception
                MsgBox(ex.Message)
                ProductionPiecePayPersonnel_Updatecheck = False
            End Try
        End Function

        ''' <summary>
        ''' �ӤH�p���~�����` �R��
        ''' </summary>
        ''' <param name="AutoID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>

        Public Function ProductionPiecePayPersonnel_Delete(ByVal AutoID As String) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbcomm As DbCommand = db.GetStoredProcCommand("ProductionPiecePayPersonnel_Delete")

                db.AddInParameter(dbcomm, "@AutoID", DbType.String, AutoID)

                db.ExecuteNonQuery(dbcomm)
                ProductionPiecePayPersonnel_Delete = True
            Catch ex As Exception
                MsgBox(ex.Message)
                ProductionPiecePayPersonnel_Delete = False
            End Try
        End Function


        ''' <summary>
        ''' �d�߲έp�X���w���u�Y�@����p���`�B
        ''' </summary>
        ''' <param name="Per_NO"></param>
        ''' <param name="PP_DateStart"></param>
        ''' <param name="PP_DateEnd"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>

        Public Function ProductionSumPiecePersonnelView(ByVal Per_NO As String, ByVal PP_DateStart As String, ByVal PP_DateEnd As String, ByVal DepID As String, ByVal FacID As String) As ProductionPiecePayPersonnelInfo
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("ProductionSumPiecePersonnelView")

            db.AddInParameter(dbComm, "@Per_NO", DbType.String, Per_NO) '
            db.AddInParameter(dbComm, "@PP_DateStart", DbType.String, PP_DateStart)
            db.AddInParameter(dbComm, "@PP_DateEnd", DbType.String, PP_DateEnd)

            db.AddInParameter(dbComm, "@DepID", DbType.String, DepID)
            db.AddInParameter(dbComm, "@FacID", DbType.String, FacID)


            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    Return FillProductionSumPiecePersonnelView(reader)
                End While
                Return Nothing
            End Using
        End Function

        Public Function FillProductionSumPiecePersonnelView(ByVal reader As IDataReader) As ProductionPiecePayPersonnelInfo
            Dim ai1 As New ProductionPiecePayPersonnelInfo
            If reader("PPGtotal_P") Is DBNull.Value Then
                ai1.PPGtotal_P = 0
            Else
                ai1.PPGtotal_P = reader("PPGtotal_P")
            End If
            Return ai1
        End Function

        ''' <summary>
        '''  �d�߲έp�X���w���u�Y�@����p�� �u��
        ''' </summary>
        ''' <param name="Per_NO"></param>
        ''' <param name="PT_DateStart"></param>
        ''' <param name="PT_DateEnd"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ProductionSumTimePersonnelView(ByVal Per_NO As String, ByVal PT_DateStart As String, ByVal PT_DateEnd As String, ByVal DepID As String, ByVal FacID As String) As ProductionPiecePayPersonnelInfo
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("ProductionSumTimePersonnelView")

            db.AddInParameter(dbComm, "@Per_NO", DbType.String, Per_NO) '
            db.AddInParameter(dbComm, "@PT_DateStart", DbType.String, PT_DateStart)
            db.AddInParameter(dbComm, "@PT_DateEnd", DbType.String, PT_DateEnd)

            db.AddInParameter(dbComm, "@DepID", DbType.String, DepID)
            db.AddInParameter(dbComm, "@FacID", DbType.String, FacID)

            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    Return FillProductionSumTimePersonnelView(reader)
                End While
                Return Nothing
            End Using
        End Function

        Public Function FillProductionSumTimePersonnelView(ByVal reader As IDataReader) As ProductionPiecePayPersonnelInfo
            Dim ai1 As New ProductionPiecePayPersonnelInfo
            If reader("PPGtotal_T") Is DBNull.Value Then
                ai1.PPGtotal_T = 0
            Else
                ai1.PPGtotal_T = reader("PPGtotal_T")
            End If

            If reader("PT_Total_Sum") Is DBNull.Value Then
                ai1.PT_Total_Sum = 0
            Else
                ai1.PT_Total_Sum = reader("PT_Total_Sum")
            End If

            Return ai1
        End Function
        ''' <summary>
        ''' �C�X�ӤH�p�ɡA�p��A�b�Ҧ������u�@�������C��A�Φb�ӤH�O���~�ʾɤJ���A�n�� "����">
        ''' </summary>
        ''' <param name="Per_NO"></param>
        ''' <param name="PT_DateStart"></param>
        ''' <param name="PT_DateEnd"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ProductionSumPieceTimePersonnelView(ByVal Per_NO As String, ByVal PT_DateStart As String, ByVal PT_DateEnd As String) As List(Of ProductionPiecePayPersonnelInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("ProductionSumPieceTimePersonnelView")

            db.AddInParameter(dbComm, "@Per_NO", DbType.String, Per_NO) '
            db.AddInParameter(dbComm, "@PT_DateStart", DbType.String, PT_DateStart)
            db.AddInParameter(dbComm, "@PT_DateEnd", DbType.String, PT_DateEnd)

            Dim FeatureList As New List(Of ProductionPiecePayPersonnelInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillProductionSumPieceTimePersonnelView(reader))
                End While
                Return FeatureList
            End Using
        End Function

        Public Function FillProductionSumPieceTimePersonnelView(ByVal reader As IDataReader) As ProductionPiecePayPersonnelInfo
            Dim ai1 As New ProductionPiecePayPersonnelInfo
            If reader("DepID") Is DBNull.Value Then
                ai1.DepID = Nothing
            Else
                ai1.DepID = reader("DepID")
            End If

            If reader("FacID") Is DBNull.Value Then
                ai1.FacID = Nothing
            Else
                ai1.FacID = reader("FacID")
            End If
            Return ai1
        End Function


        Public Function ProductionSumPieceTimePersonnelDepViewKQClass(ByVal FacID As String, ByVal DepID As String, ByVal PT_DateStart As String, ByVal PT_DateEnd As String, ByVal KQClass As String, ByVal KQMonth As String) As List(Of ProductionPiecePayPersonnelInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("ProductionSumPieceTimePersonnelDepViewKQClass")

            db.AddInParameter(dbComm, "@FacID", DbType.String, FacID) '
            db.AddInParameter(dbComm, "@DepID", DbType.String, DepID) '
            db.AddInParameter(dbComm, "@PT_DateStart", DbType.String, PT_DateStart)
            db.AddInParameter(dbComm, "@PT_DateEnd", DbType.String, PT_DateEnd)
            db.AddInParameter(dbComm, "@KQClass", DbType.String, KQClass) '
            db.AddInParameter(dbComm, "@KQMonth", DbType.String, KQMonth) '

            Dim FeatureList As New List(Of ProductionPiecePayPersonnelInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillProductionSumPieceTimePersonnelDepView(reader))
                End While
                Return FeatureList
            End Using
        End Function



        ''' <summary>
        ''' �b�ӤH�p�ɡA�p���~���ɤJ ���t�O�����ɤJ�� �n�d�ߥX,���t�O�����@�~���H���W��C��
        ''' </summary>
        ''' <param name="FacID"></param>
        ''' <param name="DepID"></param>
        ''' <param name="PT_DateStart"></param>
        ''' <param name="PT_DateEnd"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ProductionSumPieceTimePersonnelDepView(ByVal FacID As String, ByVal DepID As String, ByVal PT_DateStart As String, ByVal PT_DateEnd As String) As List(Of ProductionPiecePayPersonnelInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("ProductionSumPieceTimePersonnelDepView")

            db.AddInParameter(dbComm, "@FacID", DbType.String, FacID) '
            db.AddInParameter(dbComm, "@DepID", DbType.String, DepID) '
            db.AddInParameter(dbComm, "@PT_DateStart", DbType.String, PT_DateStart)
            db.AddInParameter(dbComm, "@PT_DateEnd", DbType.String, PT_DateEnd)

            Dim FeatureList As New List(Of ProductionPiecePayPersonnelInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillProductionSumPieceTimePersonnelDepView(reader))
                End While
                Return FeatureList
            End Using
        End Function

        Public Function FillProductionSumPieceTimePersonnelDepView(ByVal reader As IDataReader) As ProductionPiecePayPersonnelInfo
            Dim ai1 As New ProductionPiecePayPersonnelInfo
            If reader("Per_NO") Is DBNull.Value Then
                ai1.Per_NO = Nothing
            Else
                ai1.Per_NO = reader("Per_NO")
            End If
            Return ai1
        End Function
        ''' <summary>
        ''' �ӤH�p�p�A�ӤH�p��A�էO�p�ɡA�էO�p�� �έp���Y�H�A�Y�� �����ⶵ�Φh�����ާ@�H���M��
        ''' </summary>
        ''' <param name="Per_NO"></param>
        ''' <param name="FacID"></param>
        ''' <param name="DepID"></param>
        ''' <param name="DateStart"></param>
        ''' <param name="DateEnd"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ProductionSumPieceTimePerWGView(ByVal Per_NO As String, ByVal FacID As String, ByVal DepID As String, ByVal DateStart As String, ByVal DateEnd As String) As List(Of ProductionPiecePayPersonnelInfo)
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("ProductionSumPieceTimePerWGView")

            db.AddInParameter(dbComm, "@Per_NO", DbType.String, Per_NO)
            db.AddInParameter(dbComm, "@FacID", DbType.String, FacID) '
            db.AddInParameter(dbComm, "@DepID", DbType.String, DepID) '
            db.AddInParameter(dbComm, "@DateStart", DbType.String, DateStart)
            db.AddInParameter(dbComm, "@DateEnd", DbType.String, DateEnd)

            Dim FeatureList As New List(Of ProductionPiecePayPersonnelInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillProductionSumPieceTimePerWGView(reader))
                End While
                Return FeatureList
            End Using
        End Function

        Public Function FillProductionSumPieceTimePerWGView(ByVal reader As IDataReader) As ProductionPiecePayPersonnelInfo
            Dim ai1 As New ProductionPiecePayPersonnelInfo
            If reader("Per_NO") Is DBNull.Value Then
                ai1.Per_NO = Nothing
            Else
                ai1.Per_NO = reader("Per_NO")
            End If

            If reader("WPDate") Is DBNull.Value Then
                ai1.WPdate = Nothing
            Else
                ai1.WPdate = reader("WPDate")
            End If

            Return ai1
        End Function


        'Public Function ProductionSumPieceTimePersonnelView(ByVal Per_NO As String, ByVal PL_YYMM As String) As ProductionPiecePayPersonnelInfo
        '    Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
        '    Dim dbComm As DbCommand = db.GetStoredProcCommand("ProductionSumPieceTimePersonnelView")

        '    db.AddInParameter(dbComm, "@Per_NO", DbType.String, Per_NO) '
        '    db.AddInParameter(dbComm, "@PL_YYMM", DbType.String, PL_YYMM)

        '    Using reader As IDataReader = db.ExecuteReader(dbComm)
        '        While reader.Read
        '            Return FillProductionSumPieceTimePersonnelView(reader)
        '        End While
        '        Return Nothing
        '    End Using
        'End Function

        'Public Function FillProductionSumPieceTimePersonnelView(ByVal reader As IDataReader) As ProductionPiecePayPersonnelInfo
        '    Dim ai1 As New ProductionPiecePayPersonnelInfo
        '    If reader("PL_MeritedPaySum") Is DBNull.Value Then
        '        ai1.PL_MeritedPaySum = 0
        '    Else
        '        ai1.PL_MeritedPaySum = reader("PL_MeritedPaySum")
        '    End If
        '    Return ai1
        'End Function


        Public Function ProductionPiecePayPersonnel_GetList1(ByVal AutoID As String, ByVal Per_NO As String, ByVal Per_Name As String, ByVal PL_YYMM As String, ByVal DepID As String, ByVal PL_Check As String, _
                                                    ByVal PL_CheckUserID As String, ByVal PL_AddUserID As String, ByVal PL_ModifyUserID As String, ByVal PL_DateStart As String, ByVal PL_DateEnd As String, ByVal Print_Action As String, ByVal FacID As String, ByVal Per_Class As String, ByVal Per_Resign As String) As List(Of ProductionPiecePayPersonnelInfo)

            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("ProductionPiecePayPersonnel_GetList1")

            db.AddInParameter(dbComm, "@AutoID", DbType.String, AutoID) ' 
            db.AddInParameter(dbComm, "@Per_NO", DbType.String, Per_NO) '
            db.AddInParameter(dbComm, "@Per_Name", DbType.String, Per_Name) '
            db.AddInParameter(dbComm, "@PL_YYMM", DbType.String, PL_YYMM) '-- /�����s��

            db.AddInParameter(dbComm, "@DepID", DbType.String, DepID) '-- /�t�O
            db.AddInParameter(dbComm, "@PL_Check", DbType.String, PL_Check) '--/�إߤ��
            db.AddInParameter(dbComm, "@PL_CheckUserID", DbType.String, PL_CheckUserID) ' --/�ާ@�H
            db.AddInParameter(dbComm, "@PL_AddUserID", DbType.String, PL_AddUserID) '  --> < =
            db.AddInParameter(dbComm, "@PL_ModifyUserID", DbType.String, PL_ModifyUserID)

            db.AddInParameter(dbComm, "@PL_DateStart", DbType.String, PL_DateStart) '  --> < =
            db.AddInParameter(dbComm, "@PL_DateEnd", DbType.String, PL_DateEnd)
            db.AddInParameter(dbComm, "@FacID", DbType.String, FacID)

            'Per_Class
            db.AddInParameter(dbComm, "@Per_Class", DbType.String, Per_Class)

            db.AddInParameter(dbComm, "@Per_Resign", DbType.String, Per_Resign)

            Dim FeatureList As New List(Of ProductionPiecePayPersonnelInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillProductionPiecePayPersonnel(reader, PL_DateStart, PL_DateEnd, Print_Action))
                End While
                Return FeatureList
            End Using

        End Function


    End Class
End Namespace