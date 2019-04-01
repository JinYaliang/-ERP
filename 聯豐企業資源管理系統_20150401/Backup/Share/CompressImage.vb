Imports System.IO
Imports System.IO.File
Imports System.Drawing
Imports System.Drawing.Imaging
Public Class CompressImage
    Dim fs As IO.FileStream
    Const Int_SmallLSize As Integer = 1024 '����v�j�p
    Const Int_SmallHSize As Integer = 768
    Public Function GetMinPic(ByVal MaxPic As System.Drawing.Image) As System.Drawing.Image '��������Y ���O���ծĪG���O�ܦn
        Dim MinWidth As Integer, MinHeight As Integer

        If MaxPic.Height > Int_SmallHSize Or MaxPic.Width > Int_SmallLSize Then
            If MaxPic.Height > MaxPic.Width Then
                MinWidth = MaxPic.Width / (MaxPic.Height / Int_SmallHSize)
                MinHeight = Int_SmallHSize
            Else
                MinWidth = Int_SmallLSize
                MinHeight = MaxPic.Height / (MaxPic.Width / Int_SmallLSize)
            End If
            Return MaxPic.GetThumbnailImage(CInt(MinWidth), CInt(MinHeight), Nothing, New System.IntPtr())
        Else
            Return MaxPic
        End If

    End Function
    Public Sub GetJPEG(ByVal picturebox_image As System.Drawing.Image, ByVal picturebox As PictureBox, ByVal i As Integer) '�s�X�Ѽ����Y,�ĪG���n
        '���Y�e�u�P�_�Ϥ��j�p,�Ϥ��L�j���Y��v�ĥ�65�A�p�h�ĥ�100
        'Dim ofd As New OpenFileDialog
        'ofd.Filter = "�Ϥ����(*.jpg,*.gif,*.bmp)|*.jpg;*.gif;*.bmp"
        'ofd.ShowDialog()
        'ofd.OpenFile.Length/1024 =*MB   

        Dim codecs() As Imaging.ImageCodecInfo = Imaging.ImageCodecInfo.GetImageEncoders()
        Dim ici As Imaging.ImageCodecInfo = Nothing
        Dim codec As Imaging.ImageCodecInfo

        Dim compressPic As New Bitmap(picturebox_image)
        Dim ep As Imaging.EncoderParameters = New Imaging.EncoderParameters()
        For Each codec In codecs
            If (codec.MimeType = "image/jpeg") Then
                ici = codec
            End If
        Next
        ep.Param(0) = New Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, i)
        ' ���B�Ѽ�i�N�����Y�v�A��ĳ�]�m65-85����,�d��0-100,100�N��Ϥ���q���u�A�����Y�v�̧C
        picturebox_image.Save(Application.StartupPath & "\TempFile\" & "ok.jpg", ici, ep)
        compressPic.Dispose()
        picturebox.ImageLocation = Application.StartupPath & "\TempFile\" & "ok.jpg"
    End Sub
End Class
