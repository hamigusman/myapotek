Public Class frmBarangObat
    Dim DtDataview As New DataView
    Dim objDataTable As New DataTable
    Dim bMgr As BindingManagerBase
    Dim BarangObatBindSource As New BindingSource

    Private Sub frmBarangObat_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If MsgBox("Anda yakin ingin keluar ?", MsgBoxStyle.Information Or MsgBoxStyle.YesNo, "Konfirmasi") = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Public Sub ListViewDesign()
        ListView1.View = View.Details
        ListView1.GridLines = True
        ListView1.FullRowSelect = True

        'menambahkan header kolom
        ListView1.Columns.Add("", 0, HorizontalAlignment.Center)
        ListView1.Columns.Add("Nama BarangObat", 200, HorizontalAlignment.Center)
        ListView1.Columns.Add("Alamat", 200, HorizontalAlignment.Center)
        ListView1.Columns.Add("Kontak", 200, HorizontalAlignment.Center)
        ListView1.Columns.Add("Wilayah", 200, HorizontalAlignment.Center)
        ListView1.Columns.Add("Area", 200, HorizontalAlignment.Center)
        ListView1.Columns.Add("Sub Area", 200, HorizontalAlignment.Center)
        ListView1.Columns.Add("Komisi", 200, HorizontalAlignment.Center)
    End Sub

    Public Sub FillListview()
        Try
            Dim DataControl As New AccessData.DataControl
            Dim myData As DataSet = DataControl.GetDataSet("Select * from tbl_BarangObat")
            objDataTable = myData.Tables("data")
            DtDataview.Table = objDataTable
            ListView1.Items.Clear()
            For i = 0 To (objDataTable.Rows.Count - 1)
                With objDataTable.Rows(i)
                    Dim lSingleItem As ListViewItem
                    lSingleItem = ListView1.Items.Add(.Item("id_BarangObat").ToString)
                    lSingleItem.SubItems.Add(.Item("nama_BarangObat").ToString)
                    lSingleItem.SubItems.Add(.Item("alamat").ToString)
                    lSingleItem.SubItems.Add(.Item("no_telp").ToString)
                    lSingleItem.SubItems.Add(.Item("wilayah").ToString)
                    lSingleItem.SubItems.Add(.Item("area").ToString)
                    lSingleItem.SubItems.Add(.Item("sub_area").ToString)
                    lSingleItem.SubItems.Add(.Item("komisi").ToString)
                End With
            Next i
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub frmBarangObat_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ListViewDesign()
        FillListview()
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Try
            Dim DataControl As New AccessData.DataControl
            Dim myData As DataSet = DataControl.GetDataSet("Select * from tbl_BarangObat where nama_BarangObat like '" & txtSearch.Text & "%'")
            objDataTable = myData.Tables("data")
            DtDataview.Table = objDataTable
            ListView1.Items.Clear()
            For i = 0 To (objDataTable.Rows.Count - 1)
                With objDataTable.Rows(i)
                    Dim lSingleItem As ListViewItem
                    lSingleItem = ListView1.Items.Add(.Item("id_BarangObat").ToString)
                    lSingleItem.SubItems.Add(.Item("nama_BarangObat").ToString)
                    lSingleItem.SubItems.Add(.Item("alamat").ToString)
                    lSingleItem.SubItems.Add(.Item("no_telp").ToString)
                    lSingleItem.SubItems.Add(.Item("wilayah").ToString)
                    lSingleItem.SubItems.Add(.Item("area").ToString)
                    lSingleItem.SubItems.Add(.Item("sub_area").ToString)
                    lSingleItem.SubItems.Add(.Item("komisi").ToString)
                End With
            Next i
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick
        Dim x As Integer = CInt(ListView1.SelectedItems(0).Index)
        frmMstBarangObat.Data = CInt(ListView1.Items(x).SubItems(0).Text)
        frmMstBarangObat.Opt = 2
        frmMstBarangObat.ShowDialog()
    End Sub

    'Private Sub ListView1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.Click
    '    Dim x As Integer = CInt(ListView1.SelectedItems(0).Index)
    '    MsgBox(ListView1.Items(x).SubItems(0).Text)
    'End Sub

    Private Sub ListView1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ListView1.MouseUp
        If e.Button = MouseButtons.Right Then
            Me.ContextMenuStrip = ContextMenuStrip1

        End If
    End Sub

    Private Sub HapusToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles HapusToolStripMenuItem.Click
        Dim x As Integer = CInt(ListView1.SelectedItems(0).Index)
        'MsgBox(ListView1.Items(x).SubItems(0).Text)


        If MsgBox("Anda yakin ingin menghapus data : " & ListView1.Items(x).SubItems(1).Text & "?", MsgBoxStyle.Information Or MsgBoxStyle.YesNo, "Konfirmasi") = MsgBoxResult.Yes Then
            Try
                Dim BarangObat As New BarangObat
                Dim AccessBarang As New AccessData.AccessBarang
                BarangObat.IdObat = CInt(ListView1.Items(x).SubItems(0).Text)
                AccessBarang.BarangObatDelete(BarangObat.IdObat)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
        FillListview()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        frmMstBarangObat.Opt = 1
        frmMstBarangObat.ShowDialog()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If ListView1.SelectedItems.Count > 0 Then
            Dim x As Integer = CInt(ListView1.SelectedItems(0).Index)
            frmMstBarangObat.Data = CInt(ListView1.Items(x).SubItems(0).Text)
            frmMstBarangObat.Opt = 2
            frmMstBarangObat.ShowDialog()
        Else
            MsgBox("Silahkan pilih data yang akan diubah")
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If ListView1.SelectedItems.Count > 0 Then
            Dim x As Integer = CInt(ListView1.SelectedItems(0).Index)
            'MsgBox(ListView1.Items(x).SubItems(0).Text)
            If MsgBox("Anda yakin ingin menghapus data :  " & ListView1.Items(x).SubItems(1).Text & "?", MsgBoxStyle.Information Or MsgBoxStyle.YesNo, "Konfirmasi") = MsgBoxResult.Yes Then
                Try
                    Dim BarangObat As New BarangObat
                    'Dim AccessBarangObat As New AccessData.AccessBarangObat
                    'BarangObat.IDBarangObat = CInt(ListView1.Items(x).SubItems(0).Text)
                    'AccessBarangObat.BarangObatDelete(BarangObat.IDBarangObat)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End If
            FillListview()
        Else
            MsgBox("Silahkan pilih data yang akan dihapus")
        End If
    End Sub

    Private Sub EditToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditToolStripMenuItem.Click
        Dim x As Integer = CInt(ListView1.SelectedItems(0).Index)
        frmMstBarangObat.Data = CInt(ListView1.Items(x).SubItems(0).Text)
        frmMstBarangObat.Opt = 2
        frmMstSales.ShowDialog()
    End Sub

    Private Sub RefreshDataToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshDataToolStripMenuItem.Click
        FillListview()

    End Sub
End Class