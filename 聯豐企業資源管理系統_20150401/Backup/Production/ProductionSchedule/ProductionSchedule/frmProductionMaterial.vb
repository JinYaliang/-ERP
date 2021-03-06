Imports LFERP.Library.ProductProcess
Imports LFERP.Library.Product
Imports LFERP.Library.ProductionKaiLiao
Imports LFERP.Library.Production.ProductionFieldDaySummary
Imports LFERP.Library.ProductionMaterial


Public Class frmProductionMaterial

    Dim ds As New DataSet
    Dim pc As New ProcessMainControl

    Dim pkc As New ProductionKaiLiaoControl

    Sub LoadProductNo()

        Dim mc As New ProductController
        PM_M_Code.Properties.DisplayMember = "PM_M_Code"
        PM_M_Code.Properties.ValueMember = "PM_M_Code"
        PM_M_Code.Properties.DataSource = mc.Product_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

    End Sub
    Private Sub frmProductionMaterial_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CreateTable()
        LoadProductNo()
        DateEdit1.Text = Format(Now, "yyyy/MM/dd")
        DateEdit2.Text = Format(Now, "yyyy/MM/dd")

        DateEdit1.Properties.ShowPopupShadow = True

        cbType.EditValue = ""
        PM_M_Code.EditValue = ""
        gluType.EditValue = ""

    End Sub

    Sub CreateTable()

        ds.Tables.Clear()

        With ds.Tables.Add("ProductType")
            .Columns.Add("PM_Type", GetType(String))
        End With
        gluType.Properties.DisplayMember = "PM_Type"
        gluType.Properties.ValueMember = "PM_Type"
        gluType.Properties.DataSource = ds.Tables("ProductType")

    End Sub

    Private Sub PM_M_Code_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PM_M_Code.EditValueChanged
        On Error Resume Next

        Dim pcc As New ProcessMainControl
        Dim pci As List(Of ProcessMainInfo)

        ds.Tables("ProductType").Clear()

        pci = pcc.ProcessMain_GetList1(Nothing, PM_M_Code.EditValue, cbType.EditValue, Nothing)
        If pci.Count = 0 Then Exit Sub

        Dim i As Integer
        For i = 0 To pci.Count - 1
            Dim row As DataRow
            row = ds.Tables("ProductType").NewRow

            row("PM_Type") = pci(i).Type3ID

            ds.Tables("ProductType").Rows.Add(row)
        Next
    End Sub

    '導出相應工藝信息的詳細開料狀況
    '@ 2012/1/6 添加判斷為空時，相應控件獲得焦點
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If cbType.EditValue = "" Then
            MsgBox("工藝類型不能為空!", 64, "提示")
            cbType.Focus()
            Exit Sub
        End If
        If PM_M_Code.EditValue = "" Then
            MsgBox("產品編號不能為空!", 64, "提示")
            PM_M_Code.Focus()
            Exit Sub
        End If
        If gluType.EditValue = "" Then
            MsgBox("類型不能為空!", 64, "提示")
            gluType.Focus()
            Exit Sub
        End If

        Dim ltc As New CollectionToDataSet
        Dim ltc1 As New CollectionToDataSet
        Dim ltc2 As New CollectionToDataSet
        Dim ltc3 As New CollectionToDataSet


        ds.Tables.Clear()

        Dim pdi As New ProductionFieldDaySummaryInfo
        Dim pdc As New ProductionFieldDaySummaryControl  '每天記錄信息
        Dim pmc As New ProductionMaterialControl
        Dim pkc As New ProductionKaiLiaoControl

        pdi.Str1 = DateEdit1.Text
        pdi.Str2 = DateEdit2.Text

        pdc.Temp3_Add(pdi)

        If pkc.KaiLiaoManagement_GetList(cbType.EditValue, PM_M_Code.EditValue, gluType.EditValue, Nothing, Nothing, "原材料", DateEdit1.Text, DateEdit2.Text, Nothing).Count = 0 Then
            MsgBox("當前無此開料單領出記錄!")
            Exit Sub
        End If

        ltc.CollToDataSet(ds, "KaiLiaoManagement", pkc.KaiLiaoManagement_GetList(cbType.EditValue, PM_M_Code.EditValue, gluType.EditValue, Nothing, Nothing, "原材料", DateEdit1.Text, DateEdit2.Text, Nothing))
        ltc1.CollToDataSet(ds, "Temp3", pdc.Temp3_GetList(Nothing, Nothing))
        ltc2.CollToDataSet(ds, "ProductionMaterial", pmc.ProductionMaterial_GetList(Nothing, Nothing, Nothing, Nothing))
        ltc3.CollToDataSet(ds, "ProductionKaiLiao", pkc.ProductionKaiLiao_GetList(Nothing, cbType.EditValue, Nothing, Nothing, Nothing, PM_M_Code.EditValue, gluType.EditValue, DateEdit1.Text, DateEdit2.Text, True, Nothing))


        PreviewRPT(ds, "rptKaiLiaoManagement", "物料開料記錄一覽表", True, True)

        ltc = Nothing
        ltc1 = Nothing
        ltc2 = Nothing
        ltc3 = Nothing

        Me.Close()
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    '@ 2012/1/6 添加，當控件內容發生改變，且PM_M_Code控件內容不為空時，加載相應的內容到gluType控件
    '此過程調用以下過程：
    'PM_M_Code_EditValueChanged()
    Private Sub cbType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbType.SelectedIndexChanged
        If PM_M_Code.Text <> "" Then
            PM_M_Code_EditValueChanged(Nothing, Nothing)
        End If
    End Sub
End Class