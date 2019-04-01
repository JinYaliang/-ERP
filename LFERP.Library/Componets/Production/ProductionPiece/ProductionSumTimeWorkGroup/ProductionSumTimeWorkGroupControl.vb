Imports System.Data.Common

Namespace LFERP.Library.ProductionSumTimeWorkGroup


    Public Class ProductionSumTimeWorkGroupControl

        ''' <summary>
        ''' �d�߲էO�p�ɲέp
        ''' </summary>
        ''' <param name="GT_NO"></param>
        ''' <param name="Per_NO"></param>
        ''' <param name="G_NO"></param>
        ''' <param name="DepID"></param>
        ''' <param name="FacID"></param>
        ''' <param name="GT_DateStart "></param>
        ''' <param name="GT_Action"></param>
        ''' <param name="GT_DateEnd "></param>
        ''' <returns></returns>
        ''' <remarks></remarks>

        Public Function ProductionSumTimeWorkGroup_GetList(ByVal GT_NO As String, ByVal Per_NO As String, ByVal G_NO As String, ByVal DepID As String, ByVal FacID As String, ByVal GT_DateStart As String, _
                                                    ByVal GT_Action As String, ByVal GT_DateEnd As String, ByVal Model As String, ByVal Print_Action As String) As List(Of ProductionSumTimeWorkGroupInfo)

            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("ProductionSumTimeWorkGroup_GetList")

            db.AddInParameter(dbComm, "@GT_NO", DbType.String, GT_NO) ' 
            db.AddInParameter(dbComm, "@Per_NO", DbType.String, Per_NO) '
            db.AddInParameter(dbComm, "@G_NO", DbType.String, G_NO) '
            db.AddInParameter(dbComm, "@DepID", DbType.String, DepID) '-- /�����s��

            db.AddInParameter(dbComm, "@FacID", DbType.String, FacID) '-- /�t�O
            db.AddInParameter(dbComm, "@GT_Datestart", DbType.String, GT_DateStart) '--/�إߤ��
            db.AddInParameter(dbComm, "@GT_Action", DbType.String, GT_Action) ' --/�ާ@�H
            db.AddInParameter(dbComm, "@GT_DateEnd", DbType.String, GT_DateEnd) '  --> < = 
            db.AddInParameter(dbComm, "@Model", DbType.String, Model)

            Dim FeatureList As New List(Of ProductionSumTimeWorkGroupInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillProductionSumTimeWorkGroup(reader, GT_DateStart, GT_DateEnd, Print_Action))
                End While
                Return FeatureList
            End Using

        End Function
        ''' <summary>
        ''' ����էO�p�ɲέp�ɫ��w�O��
        ''' </summary>
        ''' <param name="reader"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FillProductionSumTimeWorkGroup(ByVal reader As IDataReader, ByVal FGT_Datestart As String, ByVal FGT_DateEnd As String, ByVal FPrint_Action As String) As ProductionSumTimeWorkGroupInfo

            On Error Resume Next

            Dim ai As New ProductionSumTimeWorkGroupInfo

            ''�u�Φb���L��
            ai.GT_DateStart = FGT_Datestart
            ai.GT_DateEnd = FGT_DateEnd
            ai.Print_Action = FPrint_Action


            If reader("GT_NO") Is DBNull.Value Then
                ai.GT_NO = Nothing
            Else
                ai.GT_NO = reader("GT_NO").ToString  '      GT_NO             *  nvarchar(50)                /�p�ɳ渹
            End If

            If reader("Per_NO") Is DBNull.Value Then
                ai.Per_NO = Nothing
            Else
                ai.Per_NO = reader("Per_NO").ToString  '              *  nvarchar(50)                  /���u�u��
            End If

            If reader("G_NO") Is DBNull.Value Then
                ai.G_NO = Nothing
            Else
                ai.G_NO = reader("G_NO").ToString  ''G_NO                *  nvarchar(50)                  /�էO�s��
            End If

            If reader("DepID") Is DBNull.Value Then
                ai.DepID = Nothing
            Else
                ai.DepID = reader("DepID").ToString  ' 'DPT_ID              *  nvarchar(50)                  /�����s��
            End If

            If reader("FacID") Is DBNull.Value Then
                ai.FacID = Nothing
            Else
                ai.FacID = reader("FacID").ToString  ''DPT_PID             *  nvarchar(50)                  /�t�O
            End If

            ''---------------------------------------------------------------------

            If reader("GT_BeginTime") Is DBNull.Value Then
                ai.GT_BeginTime = Nothing
            Else
                ai.GT_BeginTime = reader("GT_BeginTime").ToString  '  GT_BeginTime      *  nvarchar(50)                /�p�ɶ}�l�ɶ�
            End If

            If reader("GT_EndTime") Is DBNull.Value Then
                ai.GT_EndTime = Nothing
            Else
                ai.GT_EndTime = reader("GT_EndTime").ToString  ' 'GT_EndTime        *  nvarchar(50)                /�p�ɵ����ɶ�
            End If

            If reader("GT_Total") Is DBNull.Value Then
                ai.GT_Total = Nothing
            Else
                ai.GT_Total = reader("GT_Total").ToString  ''GT_Total          *  float                       /�`�p�ɶ�    
            End If

            If reader("GT_Date") Is DBNull.Value Then
                ai.GT_Date = Nothing
            Else
                ai.GT_Date = CStr(reader("GT_Date"))  'GT_Date           *  datetime                    /�إߤ��
            End If

            If reader("GT_Action") Is DBNull.Value Then
                ai.GT_Action = Nothing
            Else
                ai.GT_Action = CStr(reader("GT_Action"))  'GT_Action         *  nvarchar(50)                /�ާ@�H
            End If

            If reader("GT_Remark") Is DBNull.Value Then
                ai.GT_Remark = Nothing
            Else
                ai.GT_Remark = CStr(reader("GT_Remark"))   'GT_Remark         *  nvarchar(MAX)               /�ƪ`
            End If

            ''�~��r�q
            If reader("GT_ActionName") Is DBNull.Value Then 'GT_ActionName(�ާ@���W)
                ai.GT_ActionName = Nothing
            Else
                ai.GT_ActionName = reader("GT_ActionName")
            End If

            If reader("GT_Per_Name") Is DBNull.Value Then 'GT_Per_Name(���u�W�m�W(ProductionPiecePersonnel))
                ai.GT_Per_Name = Nothing
            Else
                ai.GT_Per_Name = reader("GT_Per_Name").ToString
            End If

            If reader("GT_DepName") Is DBNull.Value Then 'GT_DepName(�����W)
                ai.GT_DepName = Nothing
            Else
                ai.GT_DepName = reader("GT_DepName").ToString
            End If

            If reader("GT_FacName") Is DBNull.Value Then        'GT_FacName(�t�O�W)
                ai.GT_FacName = Nothing
            Else
                ai.GT_FacName = reader("GT_FacName")
            End If

            If reader("GT_G_Name") Is DBNull.Value Then        'GT_G_Name(�էO�W(ProductionPieceWorkGroup))
                ai.GT_G_Name = Nothing
            Else
                ai.GT_G_Name = reader("GT_G_Name")
            End If

            '-----------------------------------------------------------------------------------------

            If reader("SampID") Is DBNull.Value Then
                ai.SampID = Nothing
            Else
                ai.SampID = reader("SampID").ToString
            End If

            If reader("SampPrice") Is DBNull.Value Then
                ai.SampPrice = 0
            Else
                ai.SampPrice = reader("SampPrice")
            End If

            If reader("SampName") Is DBNull.Value Then
                ai.SampName = Nothing
            Else
                ai.SampName = reader("SampName").ToString
            End If


            Return ai

        End Function

        ''' <summary>
        ''' �W�[�էO�p�ɲέp�ɰO��
        ''' </summary>
        ''' <param name="obj"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ProductionSumTimeWorkGroup_Add(ByVal obj As ProductionSumTimeWorkGroupInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbcomm As DbCommand = db.GetStoredProcCommand("ProductionSumTimeWorkGroup_Add")

                db.AddInParameter(dbcomm, "@GT_NO", DbType.String, obj.GT_NO) '--/�p�ɳ渹
                db.AddInParameter(dbcomm, "@Per_NO", DbType.String, obj.Per_NO) '-- /���u�s��
                db.AddInParameter(dbcomm, "@G_NO", DbType.String, obj.G_NO) ' --/�էO�s��
                db.AddInParameter(dbcomm, "@DepID", DbType.String, obj.DepID) ' --/�����s��

                db.AddInParameter(dbcomm, "@FacID", DbType.String, obj.FacID) ' --/�t�O
                db.AddInParameter(dbcomm, "@GT_BeginTime", DbType.String, obj.GT_BeginTime)  '/�p�ɶ}�l�ɶ�
                db.AddInParameter(dbcomm, "@GT_EndTime", DbType.String, obj.GT_EndTime) ' - /�p�ɵ����ɶ�
                db.AddInParameter(dbcomm, "@GT_Total", DbType.Double, obj.GT_Total) ' -- /�`�p�ɶ�    

                db.AddInParameter(dbcomm, "@GT_Date", DbType.String, obj.GT_Date) '  -- /�έp���
                db.AddInParameter(dbcomm, "@GT_Action", DbType.String, obj.GT_Action) '--/�ާ@�H
                db.AddInParameter(dbcomm, "@GT_Remark", DbType.String, obj.GT_Remark) '--/�ƪ`

                db.AddInParameter(dbcomm, "@SampID", DbType.String, obj.SampID)
                db.AddInParameter(dbcomm, "@SampPrice", DbType.Double, obj.SampPrice)

                db.ExecuteNonQuery(dbcomm)
                ProductionSumTimeWorkGroup_Add = True

            Catch ex As Exception
                MsgBox(ex.Message)
                ProductionSumTimeWorkGroup_Add = False
            End Try
        End Function
        ''' <summary>
        ''' ��s�էO�p�ɲέp�ɰO��
        ''' </summary>
        ''' <param name="obj"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ProductionSumTimeWorkGroup_Update(ByVal obj As ProductionSumTimeWorkGroupInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbcomm As DbCommand = db.GetStoredProcCommand("ProductionSumTimeWorkGroup_Update")

                db.AddInParameter(dbcomm, "@GT_NO", DbType.String, obj.GT_NO) '--/�p�ɳ渹
                db.AddInParameter(dbcomm, "@Per_NO", DbType.String, obj.Per_NO) '-- /���u�s��
                db.AddInParameter(dbcomm, "@G_NO", DbType.String, obj.G_NO) ' --/�էO�s��
                db.AddInParameter(dbcomm, "@DepID", DbType.String, obj.DepID) ' --/�����s��

                db.AddInParameter(dbcomm, "@FacID", DbType.String, obj.FacID) ' --/�t�O
                db.AddInParameter(dbcomm, "@GT_BeginTime", DbType.String, obj.GT_BeginTime)  '/�p�ɶ}�l�ɶ�
                db.AddInParameter(dbcomm, "@GT_EndTime", DbType.String, obj.GT_EndTime) ' - /�p�ɵ����ɶ�
                db.AddInParameter(dbcomm, "@GT_Total", DbType.Double, obj.GT_Total) ' -- /�`�p�ɶ�    

                db.AddInParameter(dbcomm, "@GT_Date", DbType.String, obj.GT_Date) '  -- /�έp���
                db.AddInParameter(dbcomm, "@GT_Action", DbType.String, obj.GT_Action) '--/�ާ@�H
                db.AddInParameter(dbcomm, "@GT_Remark", DbType.String, obj.GT_Remark) '--/�ƪ`

                db.AddInParameter(dbcomm, "@SampID", DbType.String, obj.SampID)
                db.AddInParameter(dbcomm, "@SampPrice", DbType.Double, obj.SampPrice)

                db.ExecuteNonQuery(dbcomm)
                ProductionSumTimeWorkGroup_Update = True

            Catch ex As Exception
                MsgBox(ex.Message)
                ProductionSumTimeWorkGroup_Update = False
            End Try
        End Function

        ''' <summary>
        ''' �R���էO�p�ɲέp�ɰO��
        ''' </summary>
        ''' <param name="GT_NO"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>

        Public Function ProductionSumTimeWorkGroup_Delete(ByVal GT_NO As String) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbcomm As DbCommand = db.GetStoredProcCommand("ProductionSumTimeWorkGroup_Delete")

                db.AddInParameter(dbcomm, "@GT_NO", DbType.String, GT_NO)

                db.ExecuteNonQuery(dbcomm)
                ProductionSumTimeWorkGroup_Delete = True
            Catch ex As Exception
                MsgBox(ex.Message)
                ProductionSumTimeWorkGroup_Delete = False
            End Try
        End Function


        ''' <summary>
        ''' ����禬�渹 FillProductionSumTimeWorkGroup1 �@�_��W�Ϧ�
        ''' </summary>
        ''' <param name="Ndate"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>

        Public Function ProductionSumTimeWorkGroup_GetNO(ByVal Ndate As String) As ProductionSumTimeWorkGroupInfo
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("ProductionSumTimeWorkGroup_GetNO")
            db.AddInParameter(dbComm, "@NDate", DbType.String, Ndate)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    Return FillProductionSumTimeWorkGroup1(reader)
                End While
                Return Nothing
            End Using
        End Function
        ''' <summary>
        ''' ��^TOP1�էO�渹(�]�n���J���ƾڤ��h��W�w�@���)�H�����t��
        ''' </summary>
        ''' <param name="reader"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FillProductionSumTimeWorkGroup1(ByVal reader As IDataReader) As ProductionSumTimeWorkGroupInfo
            Dim ai1 As New ProductionSumTimeWorkGroupInfo
            If reader("GT_NO") Is DBNull.Value Then ai1.GT_NO = Nothing Else ai1.GT_NO = reader("GT_NO").ToString '
            Return ai1

        End Function


        ''------------------
        ''' <summary>
        ''' ���L�ɵL�ƾڮɡA�եΦ����
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NothingNew() As List(Of ProductionSumTimeWorkGroupInfo)
            Dim pi As New ProductionSumTimeWorkGroupInfo
            Dim FeatureList As New List(Of ProductionSumTimeWorkGroupInfo)
            FeatureList.Add(NothingFillProductionSumTimeWorkGroup())
            Return FeatureList
        End Function

        Public Function NothingFillProductionSumTimeWorkGroup() As ProductionSumTimeWorkGroupInfo
            Dim ai1 As New ProductionSumTimeWorkGroupInfo
            ai1.GT_NO = Nothing '             *  nvarchar(50)                /�p�ɳ渹
            ai1.Per_NO = Nothing '            *  nvarchar(50)                /���u�s��
            ai1.G_NO = Nothing '              *  nvarchar(50)                /�էO�s��
            ai1.DepID = Nothing '             *  nvarchar(50)                /�����s��
            ai1.FacID = Nothing '             *  nvarchar(50)                /�t�O

            ai1.GT_BeginTime = Nothing '       *  nvarchar(50)                /�p�ɶ}�l�ɶ�
            ai1.GT_EndTime = Nothing '         *  nvarchar(50)                /�p�ɵ����ɶ�
            ai1.GT_Total = 0 '         *  float                       /�`�p�ɶ�    
            ai1.GT_Date = Nothing '           *  datetime                    /�إߤ��
            ai1.GT_Action = Nothing '        *  nvarchar(50)                /�ާ@�H

            ai1.GT_Remark = Nothing '         *  nvarchar(MAX)               /�ƪ`

            '�~��r�q
            ai1.GT_ActionName = Nothing '  �ާ@���W 
            ai1.GT_Per_Name = Nothing '    ���u�W�m�W(ProductionPiecePersonnel)
            ai1.GT_DepName = Nothing '     �����W
            ai1.GT_FacName = Nothing '     �t�O�W
            ai1.GT_G_Name = Nothing '      �էO�W(ProductionPieceWorkGroup)

            ai1.GT_DateStart = Nothing  ''���L��
            ai1.GT_DateEnd = Nothing
            ai1.Print_Action = Nothing
            Return ai1

        End Function


        ''---2013-5-15��s����p��----------------------------------------------

        Public Function ProductionSumTimeWorkGroupSamp_Update(ByVal GT_NO As String, ByVal SampID As String, ByVal SampPrice As Double) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbcomm As DbCommand = db.GetStoredProcCommand("ProductionSumTimeWorkGroupSamp_Update")

                db.AddInParameter(dbcomm, "@GT_NO", DbType.String, GT_NO)
                db.AddInParameter(dbcomm, "@SampID", DbType.String, SampID)
                db.AddInParameter(dbcomm, "@SampPrice", DbType.Double, SampPrice)

                db.ExecuteNonQuery(dbcomm)
                ProductionSumTimeWorkGroupSamp_Update = True

            Catch ex As Exception
                MsgBox(ex.Message)
                ProductionSumTimeWorkGroupSamp_Update = False
            End Try
        End Function


    End Class

End Namespace