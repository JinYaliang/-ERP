Namespace LFERP.Library.Purchase.SharePurchase
    '���B�s�������ʼҶ��w�s��,����Ƶ��p����
    Public Class SharePurchaseController
        ''' <summary>
        ''' �f�֩ΰh�f��, �ק�������ʳ渹�������ƽs�X������ơG
        ''' ������   �����=���ʼ�-�禬��+�h�f��
        ''' </summary>
        ''' <param name="objFile1"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function UpdatePurchase_NoSenQty(ByVal objFile1 As SharePurchaseInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("UpdatePurchase_NoSenQty")
                db.AddInParameter(dbComm, "@PM_NO", DbType.String, objFile1.PM_NO)
                db.AddInParameter(dbComm, "@M_Code", DbType.String, objFile1.M_Code)
                db.ExecuteNonQuery(dbComm)
                UpdatePurchase_NoSenQty = True
            Catch ex As Exception
                MsgBox(ex.Message)
                UpdatePurchase_NoSenQty = False
            End Try
        End Function



        ''' <summary>
        ''' �վ�Y�ܮw�Y���Ʈw�s�G(�ƶq�i�H���t��)
        ''' �Y�Y�ܮw�L������,�h�K�[�O��  �ܮw=@WH_ID,  ���ƽs�X=@M_Code,  �ƶq=@WI_Qty
        ''' �Y�Y�ܮw��������,�h�ק�ƶq   �ƶq=��ƶq+@WI_Qty   where   �ܮw=@WH_ID and  ���ƽs�X=@M_Code
        ''' </summary>
        ''' <param name="objFile1"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function UpdateWareInventory_WIQty(ByVal objFile1 As SharePurchaseInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("UpdateWareInventory_WIQty")
                db.AddInParameter(dbComm, "@WI_Qty", DbType.Double, objFile1.WI_Qty)
                db.AddInParameter(dbComm, "@M_Code", DbType.String, objFile1.M_Code)
                db.AddInParameter(dbComm, "@WH_ID", DbType.String, objFile1.WH_ID)
                db.ExecuteNonQuery(dbComm)
                UpdateWareInventory_WIQty = True
            Catch ex As Exception
                MsgBox(ex.Message)
                UpdateWareInventory_WIQty = False
            End Try
        End Function
        ''' <summary>
        ''' �վ�Y�ܮw�Y���Ʈw�s�G(�ƶq���i���t��)
        ''' �Y�Y�ܮw�L������,�h�K�[�O��  �ܮw=@WH_ID,  ���ƽs�X=@M_Code,  �ƶq=@WI_Qty
        ''' �Y�Y�ܮw��������,�h�ק�ƶq   �ƶq=@WI_Qty   where   �ܮw=@WH_ID and  ���ƽs�X=@M_Code
        ''' </summary>
        ''' <param name="objinfo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function UpdateWareInventory_WIQty2(ByVal objinfo As SharePurchaseInfo) As Boolean
            Try
                Dim db As New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnStr)
                Dim dbComm As DbCommand = db.GetStoredProcCommand("UpdateWareInventory_WIQty2")
                db.AddInParameter(dbComm, "@WI_Qty", DbType.Double, objinfo.WI_Qty)
                db.AddInParameter(dbComm, "@M_Code", DbType.String, objinfo.M_Code)
                db.AddInParameter(dbComm, "@WH_ID", DbType.String, objinfo.WH_ID)
                db.ExecuteNonQuery(dbComm)

                UpdateWareInventory_WIQty2 = True

            Catch ex As Exception
                MsgBox(ex.Message)
                UpdateWareInventory_WIQty2 = False
            End Try
        End Function


    End Class
End Namespace