Imports System.Data.Common
Namespace LFERP.Library.ProductionSumPiecePersonnel


    Public Class ProductionSumPiecePersonnelControl
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="PP_AutoID">�u������AutoID</param>
        ''' <param name="PP_NO"></param>
        ''' <param name="Per_NO"></param>
        ''' <param name="G_NO"></param>
        ''' <param name="DepID"></param>
        ''' <param name="FacID"></param>
        ''' <param name="Pro_Type"></param>
        ''' <param name="PM_M_Code"></param>
        ''' <param name="PM_Type"></param>
        ''' <param name="PS_NO"></param>
        ''' <param name="PS_NameS"></param>
        ''' <param name="PP_DateStart"></param>
        ''' <param name="PP_Action"></param>
        ''' <param name="PP_DateEnd"></param>
        ''' <param name="Print_Action">�ѥ��L�ϥ� </param>
        ''' <returns></returns>
        ''' <remarks></remarks>


        Public Function ProductionSumPiecePersonnel_GetList(ByVal PP_AutoID As String, ByVal PP_NO As String, ByVal Per_NO As String, ByVal G_NO As String, ByVal DepID As String, ByVal FacID As String, ByVal Pro_Type As String, _
                                                            ByVal PM_M_Code As String, ByVal PM_Type As String, ByVal PS_NO As String, ByVal PS_NameS As String, ByVal PP_DateStart As String, _
                                                            ByVal PP_Action As String, ByVal PP_DateEnd As String, ByVal Print_Action As String) As List(Of ProductionSumPiecePersonnelInfo)

            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("ProductionSumPiecePersonnel_GetList")

            db.AddInParameter(dbComm, "@PP_AutoID", DbType.String, PP_AutoID) ' --/�u���s��

            db.AddInParameter(dbComm, "@PP_NO", DbType.String, PP_NO) ' --/�p��渹
            db.AddInParameter(dbComm, "@Per_NO", DbType.String, Per_NO) '--/���u�s��
            db.AddInParameter(dbComm, "@G_NO", DbType.String, G_NO) ' --/�էO�s��
            db.AddInParameter(dbComm, "@DepID", DbType.String, DepID) '-- /�����s��
            db.AddInParameter(dbComm, "@FacID", DbType.String, FacID) '-- /�t�O

            db.AddInParameter(dbComm, "@Pro_Type", DbType.String, Pro_Type) '--/�u������
            db.AddInParameter(dbComm, "@PM_M_Code", DbType.String, PM_M_Code) ' -���~�s��
            db.AddInParameter(dbComm, "@PM_Type", DbType.String, PM_Type) ' /�t��W��
            db.AddInParameter(dbComm, "@PS_NO", DbType.String, PS_NO) '�j�u�ǦW��
            db.AddInParameter(dbComm, "@PS_NameS", DbType.String, PS_NameS) '�p�u�ǦW��

            db.AddInParameter(dbComm, "@PP_DateStart", DbType.String, PP_DateStart) '/�p����
            db.AddInParameter(dbComm, "@PP_Action", DbType.String, PP_Action) '-/�ާ@�H
            db.AddInParameter(dbComm, "@PP_DateEnd", DbType.String, PP_DateEnd) ' --> < =


            Dim FeatureList As New List(Of ProductionSumPiecePersonnelInfo)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    FeatureList.Add(FillProductionSumPiecePersonnel(reader, PP_DateStart, PP_DateEnd, Print_Action))
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
        Public Function FillProductionSumPiecePersonnel(ByVal reader As IDataReader, ByVal FPP_DateStart As String, ByVal FPP_DateEnd As String, ByVal FPrint_Action As String) As ProductionSumPiecePersonnelInfo
            Dim ai As New ProductionSumPiecePersonnelInfo
            ''�ǻ������L
            ai.PP_DateEnd = FPP_DateEnd
            ai.PP_DateStart = FPP_DateStart
            ai.Print_Action = FPrint_Action

            If reader("Per_Class") Is DBNull.Value Then
                ai.Per_Class = Nothing
            Else
                ai.Per_Class = reader("Per_Class").ToString
            End If


            If reader("PP_NO") Is DBNull.Value Then ' /�p��渹
                ai.PP_NO = Nothing
            Else
                ai.PP_NO = reader("PP_NO").ToString
            End If

            If reader("Per_NO") Is DBNull.Value Then ' /���u�s��
                ai.Per_NO = Nothing
            Else
                ai.Per_NO = reader("Per_NO").ToString
            End If

            If reader("G_NO") Is DBNull.Value Then ' /�էO�s��
                ai.G_NO = Nothing
            Else
                ai.G_NO = reader("G_NO").ToString
            End If

            If reader("DepID") Is DBNull.Value Then ' /�����s��
                ai.DepID = Nothing
            Else
                ai.DepID = reader("DepID").ToString
            End If

            If reader("FacID") Is DBNull.Value Then ' /�t�O
                ai.FacID = Nothing
            Else
                ai.FacID = reader("FacID").ToString
            End If

            ''-------------------------
            If reader("Pro_Type") Is DBNull.Value Then ' /�u������   ''�������O��
                ai.Pro_Type = Nothing
            Else
                ai.Pro_Type = reader("Pro_Type").ToString
            End If

            If reader("PM_M_Code") Is DBNull.Value Then ' /���~�s��''�������O��
                ai.PM_M_Code = Nothing
            Else
                ai.PM_M_Code = reader("PM_M_Code").ToString
            End If

            If reader("PM_Type") Is DBNull.Value Then ' /�t��W��''�������O��
                ai.PM_Type = Nothing
            Else
                ai.PM_Type = reader("PM_Type").ToString
            End If

            If reader("PS_NO") Is DBNull.Value Then ' /�j�u�ǦW�� ''�������O��
                ai.PS_NO = Nothing
            Else
                ai.PS_NO = reader("PS_NO").ToString
            End If



            If reader("PS_NameS") Is DBNull.Value Then ' /�p�u�ǦW��''�������O��
                ai.PS_NameS = Nothing
            Else
                ai.PS_NameS = reader("PS_NameS").ToString
            End If
            ''-------------------------------------------------------

            If reader("PP_factor") Is DBNull.Value Then ' /�ӥ]�t��
                ai.PP_factor = Nothing
            Else
                ai.PP_factor = reader("PP_factor").ToString
            End If

            If reader("PP_factor") Is DBNull.Value Then ' /�ӥ]�t��
                ai.PP_factor = Nothing
            Else
                ai.PP_factor = reader("PP_factor")
            End If

            If reader("PP_Qty") Is DBNull.Value Then ' /�ƶq
                ai.PP_Qty = Nothing
            Else
                ai.PP_Qty = reader("PP_Qty")
            End If

            If reader("PP_Date") Is DBNull.Value Then ' /�p����
                ai.PP_Date = Nothing
            Else
                ai.PP_Date = reader("PP_Date")
            End If

            If reader("PP_AddDate") Is DBNull.Value Then ' /�O�����
                ai.PP_AddDate = Nothing
            Else
                ai.PP_AddDate = reader("PP_AddDate")
            End If

            If reader("PP_Action") Is DBNull.Value Then ' /�ާ@�H
                ai.PP_Action = Nothing
            Else
                ai.PP_Action = reader("PP_Action")
            End If

            If reader("PP_Remark") Is DBNull.Value Then ' /�ƪ`
                ai.PP_Remark = Nothing
            Else
                ai.PP_Remark = reader("PP_Remark").ToString
            End If
            '1''--------------------------�~��r�q
            If reader("PP_ActionName") Is DBNull.Value Then ' /�ާ@���W
                ai.PP_ActionName = Nothing
            Else
                ai.PP_ActionName = reader("PP_ActionName").ToString
            End If

            If reader("PP_Per_Name") Is DBNull.Value Then ' /���u�W�m�W
                ai.PP_Per_Name = Nothing
            Else
                ai.PP_Per_Name = reader("PP_Per_Name").ToString
            End If

            If reader("PP_DepName") Is DBNull.Value Then ' /�����W
                ai.PP_DepName = Nothing
            Else
                ai.PP_DepName = reader("PP_DepName").ToString
            End If

            If reader("PP_FacName") Is DBNull.Value Then ' /�t�O�W
                ai.PP_FacName = Nothing
            Else
                ai.PP_FacName = reader("PP_FacName").ToString
            End If

            If reader("PP_G_Name") Is DBNull.Value Then ' /�էO�W
                ai.PP_G_Name = Nothing
            Else
                ai.PP_G_Name = reader("PP_G_Name").ToString
            End If

            If reader("PS_Name") Is DBNull.Value Then ' /�j�u�ǦW��
                ai.PS_Name = Nothing
            Else
                ai.PS_Name = reader("PS_Name").ToString
            End If


            If reader("PP_AutoID") Is DBNull.Value Then ' �u������AUTOID
                ai.PP_AutoID = Nothing
            Else
                ai.PP_AutoID = reader("PP_AutoID").ToString
            End If

            If reader("PP_Price") Is DBNull.Value Then ' �u��
                ai.PP_Price = Nothing
            Else
                ai.PP_Price = reader("PP_Price")
            End If




            Return ai

        End Function
        ''' <summary>
        ''' �W�[�O��
        ''' </summary>
        ''' <param name="obj"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ProductionSumPiecePersonnel_Add(ByVal obj As ProductionSumPiecePersonnelInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbcomm As DbCommand = db.GetStoredProcCommand("ProductionSumPiecePersonnel_Add")


                db.AddInParameter(dbcomm, "@PP_AutoID", DbType.String, obj.PP_AutoID) '--/�u���y�{�����s��
                db.AddInParameter(dbcomm, "@PP_Price", DbType.Double, obj.PP_Price) '--/�u��

                db.AddInParameter(dbcomm, "@PP_NO", DbType.String, obj.PP_NO) '--/�p��渹
                db.AddInParameter(dbcomm, "@Per_NO", DbType.String, obj.Per_NO) '-- /���u�s��
                db.AddInParameter(dbcomm, "@G_NO", DbType.String, obj.G_NO) ' --/�էO�s��
                db.AddInParameter(dbcomm, "@DepID", DbType.String, obj.DepID) ' --/�����s��
                db.AddInParameter(dbcomm, "@FacID", DbType.String, obj.FacID) ' --/�t�O

                db.AddInParameter(dbcomm, "@Pro_Type", DbType.String, obj.Pro_Type)  '/�u������
                db.AddInParameter(dbcomm, "@PM_M_Code", DbType.String, obj.PM_M_Code) ' - /���~�s��
                db.AddInParameter(dbcomm, "@PM_Type", DbType.String, obj.PM_Type) ' -- /�t��W��    
                db.AddInParameter(dbcomm, "@PS_NO", DbType.String, obj.PS_NO) '  -- /�j�u�ǦW��
                db.AddInParameter(dbcomm, "@PS_NameS", DbType.String, obj.PS_NameS) '--/�ާ@�H

                db.AddInParameter(dbcomm, "@PP_factor", DbType.Double, obj.PP_factor) '--/�ӥ]�t��
                db.AddInParameter(dbcomm, "@PP_Qty", DbType.Int32, obj.PP_Qty) ' - /�ƶq
                db.AddInParameter(dbcomm, "@PP_Date", DbType.String, obj.PP_Date) ' -- /�p����    
                db.AddInParameter(dbcomm, "@PP_AddDate", DbType.String, obj.PP_AddDate) '  -- /�O�����
                db.AddInParameter(dbcomm, "@PP_Action", DbType.String, obj.PP_Action) '--/�ާ@�H

                db.AddInParameter(dbcomm, "@PP_Remark", DbType.String, obj.PP_Remark) '--/�ƪ`


                db.ExecuteNonQuery(dbcomm)
                ProductionSumPiecePersonnel_Add = True

            Catch ex As Exception
                MsgBox(ex.Message)
                ProductionSumPiecePersonnel_Add = False
            End Try
        End Function
        ''' <summary>
        ''' ��s�O��
        ''' </summary>
        ''' <param name="obj"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ProductionSumPiecePersonnel_Update(ByVal obj As ProductionSumPiecePersonnelInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbcomm As DbCommand = db.GetStoredProcCommand("ProductionSumPiecePersonnel_Update")

                db.AddInParameter(dbcomm, "@PP_AutoID", DbType.String, obj.PP_AutoID) '--/�u���y�{�����s��
                db.AddInParameter(dbcomm, "@PP_Price", DbType.Double, obj.PP_Price) '--/�u��

                db.AddInParameter(dbcomm, "@PP_NO", DbType.String, obj.PP_NO) '--/�p��渹
                db.AddInParameter(dbcomm, "@Per_NO", DbType.String, obj.Per_NO) '-- /���u�s��
                db.AddInParameter(dbcomm, "@G_NO", DbType.String, obj.G_NO) ' --/�էO�s��
                db.AddInParameter(dbcomm, "@DepID", DbType.String, obj.DepID) ' --/�����s��
                db.AddInParameter(dbcomm, "@FacID", DbType.String, obj.FacID) ' --/�t�O

                db.AddInParameter(dbcomm, "@Pro_Type", DbType.String, obj.Pro_Type)  '/�u������
                db.AddInParameter(dbcomm, "@PM_M_Code", DbType.String, obj.PM_M_Code) ' - /���~�s��
                db.AddInParameter(dbcomm, "@PM_Type", DbType.String, obj.PM_Type) ' -- /�t��W��    
                db.AddInParameter(dbcomm, "@PS_NO", DbType.String, obj.PS_NO) '  -- /�j�u�ǦW��
                db.AddInParameter(dbcomm, "@PS_NameS", DbType.String, obj.PS_NameS) '--/�ާ@�H

                db.AddInParameter(dbcomm, "@PP_factor", DbType.Double, obj.PP_factor) '--/�ӥ]�t��
                db.AddInParameter(dbcomm, "@PP_Qty", DbType.Int32, obj.PP_Qty) ' - /�ƶq
                db.AddInParameter(dbcomm, "@PP_Date", DbType.String, obj.PP_Date) ' -- /�p����    
                db.AddInParameter(dbcomm, "@PP_AddDate", DbType.String, obj.PP_AddDate) '  -- /�O�����
                db.AddInParameter(dbcomm, "@PP_Action", DbType.String, obj.PP_Action) '--/�ާ@�H

                db.AddInParameter(dbcomm, "@PP_Remark", DbType.String, obj.PP_Remark) '--/�ƪ`


                db.ExecuteNonQuery(dbcomm)
                ProductionSumPiecePersonnel_Update = True

            Catch ex As Exception
                MsgBox(ex.Message)
                ProductionSumPiecePersonnel_Update = False
            End Try
        End Function
        ''' <summary>
        ''' �R���ӤH�p��W��
        ''' </summary>
        ''' <param name="PP_NO"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ProductionSumPiecePersonnel_Delete(ByVal PP_NO As String) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbcomm As DbCommand = db.GetStoredProcCommand("ProductionSumPiecePersonnel_Delete")

                db.AddInParameter(dbcomm, "@PP_NO", DbType.String, PP_NO)

                db.ExecuteNonQuery(dbcomm)
                ProductionSumPiecePersonnel_Delete = True
            Catch ex As Exception
                MsgBox(ex.Message)
                ProductionSumPiecePersonnel_Delete = False
            End Try
        End Function


        ''' <summary>
        ''' ����禬�渹 FillProductionSumPiecePersonnel1 �@�_��W�Ϧ�
        ''' </summary>
        ''' <param name="Ndate"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>

        Public Function ProductionSumPiecePersonnel_GetNO(ByVal Ndate As String) As ProductionSumPiecePersonnelInfo
            Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
            Dim dbComm As DbCommand = db.GetStoredProcCommand("ProductionSumPiecePersonnel_GetNO")
            db.AddInParameter(dbComm, "@NDate", DbType.String, Ndate)
            Using reader As IDataReader = db.ExecuteReader(dbComm)
                While reader.Read
                    Return FillProductionSumPiecePersonnel1(reader)
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
        Public Function FillProductionSumPiecePersonnel1(ByVal reader As IDataReader) As ProductionSumPiecePersonnelInfo
            Dim ai1 As New ProductionSumPiecePersonnelInfo
            If reader("PP_NO") Is DBNull.Value Then ai1.PP_NO = Nothing Else ai1.PP_NO = reader("PP_NO").ToString '
            Return ai1

        End Function

        ''' <summary>
        ''' ���L�ɵL�ƾڮɡA�եΦ����
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NothingNew() As List(Of ProductionSumPiecePersonnelInfo)
            Dim pi As New ProductionSumPiecePersonnelInfo
            Dim FeatureList As New List(Of ProductionSumPiecePersonnelInfo)
            FeatureList.Add(NothingFillProductionSumPiecePersonnel())
            Return FeatureList
        End Function

        Public Function NothingFillProductionSumPiecePersonnel() As ProductionSumPiecePersonnelInfo
            Dim ai1 As New ProductionSumPiecePersonnelInfo
            ai1.PP_NO = Nothing '            *  nvarchar(50)                /�p��渹
            ai1.Per_NO = Nothing '           *  nvarchar(50)                /���u�s��
            ai1.G_NO = Nothing '              *  nvarchar(50)                /�էO�s��
            ai1.DepID = Nothing '             *  nvarchar(50)                /�����s��
            ai1.FacID = Nothing '             *  nvarchar(50)                /�t�O

            ai1.Pro_Type = Nothing '          *  nvarchar(50)                /�u������
            ai1.PM_M_Code = Nothing '         *  nvarchar(50)                /���~�s��
            ai1.PM_Type = Nothing '          *  nvarchar(50)                /�t��W��

            ai1.PS_NameS = Nothing '          *  nvarchar(50)                /�p�u�ǦW��

            ai1.PP_factor = 0 '         *  float                       /�ӥ]�t��
            ai1.PP_Qty = 0 '            *  int                         /�ƶq 
            ai1.PP_Date = Nothing '           *  datetime                    /�p����
            ai1.PP_AddDate = Nothing '        *  datetime                 /�O�����
            ai1.PP_Action = Nothing  '        *  nvarchar(50)                /�ާ@�H

            ai1.PP_Remark = Nothing '         *  nvarchar(MAX)               /�ƪ`

            '�~��r�q
            ai1.PP_ActionName = Nothing ' �ާ@���W (SystemUser)
            ai1.PP_Per_Name = Nothing '    ���u�W�m�W(ProductionPiecePersonnel)
            ai1.PP_DepName = Nothing '    �����W
            ai1.PP_FacName = Nothing '    �t�O�W
            ai1.PP_G_Name = Nothing '      �էO�W(ProductionPieceWorkGroup)    ���Ҥ���

            ai1.PS_Name = Nothing                    '    /�j�u�ǦW��  ProcessSub
            ai1.PS_NO = Nothing                      '    �j�u�ǽs��   ProductionPieceProcess

            ai1.PP_AutoID = Nothing ''�u�����y�{
            ai1.PP_Price = 0  ''�u��

            ai1.PP_DateEnd = Nothing
            ai1.PP_DateStart = Nothing

            ai1.Print_Action = Nothing ''�ѥ��L�� ��
            Return ai1

        End Function



    End Class
End Namespace