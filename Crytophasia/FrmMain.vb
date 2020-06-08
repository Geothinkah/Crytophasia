Imports System.IO

Public Class FrmMain
    Public User() As String
    Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        User = Split(Application.UserAppDataPath, "\")

            ReadTSV()
        Dim cnt As Int16
        cnt = CShort(DataGridView1.Rows.Count)
        Text = User(2) & " - Crytophasia" & "        ---> " & cnt & " words are in the Crytonary! <---"
    End Sub




    Public Sub ReadTSV()
        DataPath = "C:\Users\" & User(2) & "\OneDrive\Cryptophasia\Cryptophasia.tsv"
        Dim SplitLine() As String
        If File.Exists(DataPath) = True Then
            'Open the StreamReader
            Dim objReader As New StreamReader(DataPath, System.Text.Encoding.Default)
            Do While objReader.Peek() <> -1                     'Peek to see if there is another line of data to process
                Dim TextLine As String = objReader.ReadLine()   'Read the next line of data
                SplitLine = Split(TextLine, ControlChars.Tab)   'Separate the line into the SplitLine array
                Dim unused = DataGridView1.Rows.Add(SplitLine)  'Fill in the columns via the array
            Loop
            'Close the StreamReader
            objReader.Close()
        End If
    End Sub



#Region "***** Export the TSV file. *****"
    ''' <summary>
    ''' Build the csv file as Tab separated records.
    ''' </summary>
    Private Sub ExportTSV()
        Dim tsv As String = String.Empty
        '                                                                   Convert the grid rows to csv strings
        For Each row As DataGridViewRow In DataGridView1.Rows               'Process a grid row
            For Each cell As DataGridViewCell In row.Cells                  'Build a tsv record from the grid row
                tsv = tsv & CType(cell.Value, String) & ControlChars.Tab    'Add each column's value and a tab
            Next
            tsv = tsv.TrimEnd(ControlChars.Tab) & vbCrLf                    'Trim the last tab Add a new line
        Next
        If tsv.Length < 4 Then ' Check to see if there is any data in the datagridview
            File.WriteAllText(DataPath, tsv) 'Empty File
        Else
            tsv = tsv.Substring(0, tsv.Length - 4) 'Remove the extra lines at the end of the csv file
            File.WriteAllText(DataPath, tsv)
        End If
    End Sub

    Private Sub FrmMain_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        ExportTSV()
    End Sub

    'Private Sub RbLarry_CheckedChanged(sender As Object, e As EventArgs) Handles RbLarry.CheckedChanged
    '    DataPath = "C:\Users\Larry\OneDrive\Cryptophasia\Cryptophasia.tsv"
    'End Sub

    'Private Sub RbDavid_CheckedChanged(sender As Object, e As EventArgs) Handles RbDavid.CheckedChanged
    '    DataPath = "C:\Users\David\OneDrive\Cryptophasia\Cryptophasia.tsv"
    'End Sub

    'Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    '    If RbDavid.Checked Or RbLarry.Checked Then
    '        ReadTSV()
    '    End If
    'End Sub


#End Region


End Class
