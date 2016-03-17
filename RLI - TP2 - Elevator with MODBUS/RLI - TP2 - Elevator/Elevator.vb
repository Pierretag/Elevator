Imports System.Text
Imports RLI___TP2___Elevator.AsyncSocket.AsynchronousSocket
Imports RLI___TP2___Elevator.AsyncSocket.ClientSocket
Imports RLI___TP2___Elevator.AsyncSocket.ServerSocket

Public Class Elevator
    Public Shared ServerName As String = "localhost"
    Private serverIsRunning As Boolean = False
    Private clientIsRunning As Boolean = False
    Public server As Server

    Private Function FC1_response(request As Byte()) As Byte() 'Read coils
        Dim datagram As Byte() = New Byte(10) {}

        Dim i As Integer = 0

        Do While i <= 6
            datagram(i) = request(i)
            i += 1
        Loop

        datagram(7) = &H1   'MODBUS function code

        datagram(8) = &H1   'Byte count

        If Not CoilUP.Checked And Not CoilDown.Checked Then
            datagram(9) = &H0
        ElseIf CoilUP.Checked And Not CoilDown.Checked Then
            datagram(9) = &H1
        ElseIf Not CoilUP.Checked And CoilDown.Checked Then
            datagram(9) = &H2
        ElseIf CoilUP.Checked And CoilDown.Checked Then
            datagram(9) = &H3
        End If 'Bit values

        For Each content As Byte In datagram
            'Debug.Write("Response " + content.ToString)
        Next
        Return datagram
    End Function

    Private Function FC1_exception(request As Byte()) As Byte() 'Read coils
        Dim datagram As Byte() = New Byte(9) {}

        Dim i As Integer = 0

        Do While i <= 6
            datagram(i) = request(i)
            i += 1
        Loop

        datagram(7) = &H81   'MODBUS function code

        datagram(8) = &H1   'Exception code 0x01 or 0x02

        For Each content As Byte In datagram
            ' Debug.Write("Response " + content.ToString)
        Next
        Return datagram
    End Function

    Private Function FC2_response(request As Byte()) As Byte() 'Read discrete input
        Dim datagram As Byte() = New Byte(10) {}

        Dim i As Integer = 0

        Do While i <= 6
            datagram(i) = request(i)
            i += 1
        Loop

        datagram(7) = &H2   'MODBUS function code

        datagram(8) = &H1   'Byte count

        Select Case currentSensor
            Case E_Sensor.sensor0
                datagram(9) = &H1   'Bit values
            Case E_Sensor.sensor1
                datagram(9) = &H2   'Bit values
            Case E_Sensor.sensor2
                datagram(9) = &H4   'Bit values
            Case E_Sensor.sensor3
                datagram(9) = &H8   'Bit values
            Case E_Sensor.sensor4
                datagram(9) = &H10   'Bit values
        End Select

        For Each content As Byte In datagram
            ' Debug.Write("Response " + content.ToString)
        Next
        Return datagram
    End Function

    Private Function FC2_exception(request As Byte()) As Byte() 'Read discrete input
        Dim datagram As Byte() = New Byte(9) {}

        Dim i As Integer = 0

        Do While i <= 6
            datagram(i) = request(i)
            i += 1
        Loop

        datagram(7) = &H82   'MODBUS function code

        datagram(8) = &H1   'Exception code 0x01 or 0x02

        For Each content As Byte In datagram
            'Debug.Write("Response " + content.ToString)
        Next
        Return datagram
    End Function

    Private Function FC5_response(request As Byte()) As Byte() 'Write coils
        Dim datagram As Byte() = New Byte(12) {}

        Dim i As Integer = 0

        Do While i <= 6
            datagram(i) = request(i)
            i += 1
        Loop

        datagram(7) = &H5   'MODBUS function code

        datagram(8) = &H0   'Reference number
        datagram(9) = &H1   'Reference number

        datagram(10) = request(10)
        datagram(11) = request(11)

        'If datagram(10) = 0 And datagram(11) = 1 Then
        If datagram(10) = &HF Then
            SetCoilUP(True)
            SetCoilDown(False)
            'MessageBox.Show("UP")

            'CoilUP.CheckState = CheckState.Checked
            'CoilDown.CheckState = CheckState.Unchecked
            'ElseIf datagram(10) = 1 And datagram(11) = 0 Then
        ElseIf datagram(10) = &HF0 Then
            SetCoilUP(False)
            SetCoilDown(True)
            '  MessageBox.Show("DOWN")

            'CoilDown.CheckState = CheckState.Checked
            'CoilUP.CheckState = CheckState.Unchecked
            'ElseIf datagram(10) = 0 And datagram(11) = 0 Then
        ElseIf datagram(10) = &H0 Then
            SetCoilUP(False)
            SetCoilDown(False)
            'MessageBox.Show("NO")
        End If

        For Each content As Byte In datagram
            'Debug.Write("Response " + content.ToString)
        Next
        Return datagram
    End Function

    Private Function FC5_exception(request As Byte()) As Byte() 'Write coils
        Dim datagram As Byte() = New Byte(9) {}

        Dim i As Integer = 0

        Do While i <= 6
            datagram(i) = request(i)
            i += 1
        Loop

        datagram(7) = &H85   'MODBUS function code

        datagram(8) = &H1   'Exception code 0x01 or 0x02 or 0x03

        For Each content As Byte In datagram
            'Debug.Write("Response " + content.ToString)
        Next
        Return datagram
    End Function

    Private Function FC15_response(request As Byte()) As Byte() 'Force multiple coils
        Dim datagram As Byte() = New Byte(12) {}

        Dim i As Integer = 0

        Do While i <= 6
            datagram(i) = request(i)
            i += 1
        Loop

        datagram(7) = &HF   'MODBUS function code

        datagram(8) = &H0   'Reference number
        datagram(9) = &H0   'Reference number

        datagram(10) = &H0  'Bit count
        datagram(11) = &H10 'Bit count

        For Each content As Byte In datagram
            ' Debug.Write("Response " + content.ToString)
        Next
        Return datagram
    End Function

    Private Function FC15_exception(request As Byte()) As Byte() 'Force multiple coils
        Dim datagram As Byte() = New Byte(9) {}

        Dim i As Integer = 0

        Do While i <= 6
            datagram(i) = request(i)
            i += 1
        Loop

        datagram(7) = &H8F   'MODBUS function code

        datagram(8) = &H1   'Exception code 0x01 or 0x02

        For Each content As Byte In datagram
            'Debug.Write("Response " + content.ToString)
        Next
        Return datagram
    End Function

    Private Sub ConnectToServer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConnectToServer.Click
        If Not clientIsRunning Then
            Me.clientIsRunning = True
            Dim serverNameForm As AsyncSocket.ServerNameForm = New AsyncSocket.ServerNameForm
            serverNameForm.ShowDialog()
            Me.ConnectToServer.ForeColor = System.Drawing.Color.Green
            Me.ConnectToServer.Text = "Disconnect From the Server"

            Try
                Me._socket = New AsynchronousClient()
                _socket.AttachReceiveCallBack(AddressOf ReceivedDataFromServer)
                TryCast(_socket, AsynchronousClient).ConnectToServer(ServerName)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Me.clientIsRunning = False
                Me.ConnectToServer.ForeColor = System.Drawing.Color.Red
                Me.ConnectToServer.Text = "Connect To the Server"
            End Try
        Else
            If _socket IsNot Nothing Then
                _socket.Close()
            End If
            Me.clientIsRunning = False
            Me.ConnectToServer.ForeColor = System.Drawing.Color.Red
            Me.ConnectToServer.Text = "Connect To the Server"
        End If
    End Sub

    Private Sub LauchServer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LauchServer.Click
        If Not serverIsRunning Then
            Me.server = New Server()
            Me.server.Show()


            Me.serverIsRunning = True
            Me.LauchServer.ForeColor = System.Drawing.Color.Green
            Me.LauchServer.Text = "Stop the Server"
        Else

            Me.server.Hide()
            Me.serverIsRunning = False
            Me.LauchServer.ForeColor = System.Drawing.Color.Red
            Me.LauchServer.Text = "Launch the Server"
        End If
    End Sub

    Public Sub New()
        ' Cet appel est requis par le Concepteur Windows Form.
        InitializeComponent()

        ' Ajoutez une initialisation quelconque après l'appel InitializeComponent().
    End Sub

    Private Sub Ascenseur_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        If _socket IsNot Nothing Then
            _socket.Close()
        End If
    End Sub

    Public Sub SendMessageToServer(ByVal msg As Byte())
        If _socket IsNot Nothing Then
            If TryCast(_socket, AsynchronousClient) IsNot Nothing Then
                Me._socket.SendMessage(msg)
            End If
        End If
    End Sub

    ' This delegate enables asynchronous calls for setting
    ' the property on a Checkbox control.
    Delegate Sub SetCoilUPCallback(ByVal [val] As Boolean)
    Public Sub SetCoilUP(ByVal [val] As Boolean)
        ' InvokeRequired required compares the thread ID of the
        ' calling thread to the thread ID of the creating thread.
        ' If these threads are different, it returns true.
        If Me.CoilUP.InvokeRequired Then
            Dim d As New SetCoilUPCallback(AddressOf SetCoilUP)
            Me.Invoke(d, New Object() {[val]})
        Else
            Me.CoilUP.Checked = [val]
        End If
    End Sub

    ' This delegate enables asynchronous calls for setting
    ' the property on a Checkbox control.
    Delegate Sub SetCoilDownCallback(ByVal [val] As Boolean)
    Public Sub SetCoilDown(ByVal [val] As Boolean)
        ' InvokeRequired required compares the thread ID of the
        ' calling thread to the thread ID of the creating thread.
        ' If these threads are different, it returns true.
        If Me.CoilDown.InvokeRequired Then
            Dim d As New SetCoilDownCallback(AddressOf SetCoilDown)
            Me.Invoke(d, New Object() {[val]})
        Else
            Me.CoilDown.Checked = [val]
        End If
    End Sub


    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' YOUR JOB START HERE. You don't have to modify another file!
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub ReceivedDataFromServer(ByVal sender As Object, ByVal e As AsyncEventArgs)
        'Add some stuff to interpret messages (and remove the next line!)
        'Bytes are in e.ReceivedBytes and you can encore the bytes to string using Encoding.ASCII.GetString(e.ReceivedBytes)
        Dim Msg As Byte() = e.ReceivedBytes
        Dim rsp As Byte()
        Try
            Select Case Msg(7)  'Check MODBUS function code
                Case &H1    'Read coils
                    rsp = FC1_response(Msg)
                Case &H2    'Read discrete inputs
                    rsp = FC2_response(Msg)
                Case &H5    'Write coil
                    rsp = FC5_response(Msg)
                Case &H15   'Force multiple coils
                    rsp = FC15_response(Msg)
                Case Else
                    rsp = FC1_exception(Msg)
            End Select
        Catch exception As IndexOutOfRangeException
            Debug.WriteLine("BUG")
        End Try
        If clientIsRunning Then
            Me.SendMessageToServer(rsp)
        Else
            Debug.WriteLine("Client is not running")
        End If

        'BE CAREFUL!! 
        'If you want to change the properties of CoilUP/CoilDown/LedSensor... here, you must use safe functions. 
        'Functions for CoilUP and CoilDown are given (see SetCoilDown and SetCoilUP)
    End Sub

    ' ++++ dans la réponse serveur
    'Private Sub SendCoilsToServer()
    '    If CoilUP.Checked Then
    '       SendMessageToServer(Encoding.ASCII.GetBytes("UP"))
    '  End If
    ' If CoilDown.Checked Then
    '    SendMessageToServer(Encoding.ASCII.GetBytes("DOWN"))
    'End If
    'End Sub


    Private Sub ButtonCallFloor2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCallFloor2.Click
        ' Me.AddFloorToList(E_Floor.floor2)

        If serverIsRunning Then
            'Me.SendMessageToClient(Encoding.ASCII.GetBytes("Coucou client !"))
        End If
        If clientIsRunning Then
            Me.SendMessageToServer(Encoding.ASCII.GetBytes("Coucou server !"))
        End If
    End Sub


    Private Sub CoilTimer_Tick(sender As Object, e As EventArgs) Handles CoilTimer.Tick

        If (Me.ElevatorPhys.Top > Me.Panel2.Top And Me.CoilUP.Checked = True) Then

            Me.ElevatorPhys.Location = New Point(Me.ElevatorPhys.Location.X, Me.ElevatorPhys.Location.Y - 1)

        End If
        If (Me.ElevatorPhys.Bottom < Me.Panel2.Bottom And Me.CoilDown.Checked = True) Then
            Me.ElevatorPhys.Location = New Point(Me.ElevatorPhys.Location.X, Me.ElevatorPhys.Location.Y + 1)

        End If

        BlinkLedSensor()
        '  If isOnFloor Then
        'ClearLedSensor()
        'End If
    End Sub


    Private Enum E_Sensor
        sensor0
        sensor1
        sensor2
        sensor3
        sensor4
    End Enum

    Dim currentSensor As E_Sensor
    Dim oldSensor As E_Sensor

    Private Sub CheckSensor()

        'Pour la montée'
        If CoilUP.CheckState = CheckState.Checked Then

            Select Case Me.ElevatorPhys.Location.Y
                Case Me.PositionSensor0.Location.Y
                    currentSensor = E_Sensor.sensor0
                Case Me.PositionSensor1.Location.Y
                    currentSensor = E_Sensor.sensor1
                Case Me.PositionSensor2.Location.Y
                    currentSensor = E_Sensor.sensor2
                Case Me.PositionSensor3.Location.Y
                    currentSensor = E_Sensor.sensor3
                Case Me.PositionSensor4.Location.Y
                    currentSensor = E_Sensor.sensor4
            End Select

        End If
        'Pour la descente'
        If CoilDown.CheckState = CheckState.Checked Then

            Select Case Me.ElevatorPhys.Location.Y + 140
                Case Me.PositionSensor0.Location.Y
                    currentSensor = E_Sensor.sensor0
                Case Me.PositionSensor1.Location.Y
                    currentSensor = E_Sensor.sensor1
                Case Me.PositionSensor2.Location.Y
                    currentSensor = E_Sensor.sensor2
                Case Me.PositionSensor3.Location.Y
                    currentSensor = E_Sensor.sensor3
                Case Me.PositionSensor4.Location.Y
                    currentSensor = E_Sensor.sensor4
            End Select

        End If


    End Sub

    Private Sub BlinkLedSensor()
        CheckSensor()

        ClearLedSensor()
        Select Case currentSensor
            Case E_Sensor.sensor0
                LedSensor0.BackColor = Color.Green
            Case E_Sensor.sensor1
                LedSensor1.BackColor = Color.Green
            Case E_Sensor.sensor2
                LedSensor2.BackColor = Color.Green
            Case E_Sensor.sensor3
                LedSensor3.BackColor = Color.Green
            Case E_Sensor.sensor4
                LedSensor4.BackColor = Color.Green
        End Select
        'SendCurrentSensor()

        oldSensor = currentSensor
    End Sub

    Private Sub ClearLedSensor()
        Select Case oldSensor
            Case E_Sensor.sensor0
                LedSensor0.BackColor = Color.Transparent
            Case E_Sensor.sensor1
                LedSensor1.BackColor = Color.Transparent
            Case E_Sensor.sensor2
                LedSensor2.BackColor = Color.Transparent
            Case E_Sensor.sensor3
                LedSensor3.BackColor = Color.Transparent
            Case E_Sensor.sensor4
                LedSensor4.BackColor = Color.Transparent
        End Select

    End Sub

    ' ---- Ajout directement dans la fonction de traitement des requetes
    'Private Sub SendCurrentSensor()
    '    If clientIsRunning Then
    '        Me.SendMessageToServer(Encoding.ASCII.GetBytes(currentSensor.ToString))
    '    End If
    'End Sub







    Private Sub ButtonCallFloor0_Click(sender As Object, e As EventArgs) Handles ButtonCallFloor0.Click
        ' Me.AddFloorToList(E_Floor.floor0)
    End Sub

    Private Sub CoilDown_CheckStateChanged(sender As Object, e As EventArgs) Handles CoilDown.CheckStateChanged
        Me.CoilUP.CheckState = CheckState.Unchecked
    End Sub

    Private Sub CoilUP_CheckStateChanged_1(sender As Object, e As EventArgs) Handles CoilUP.CheckStateChanged
        Me.CoilDown.CheckState = CheckState.Unchecked
    End Sub

    Private Sub ButtonCallFloor3_Click(sender As Object, e As EventArgs) Handles ButtonCallFloor3.Click
        '  Me.AddFloorToList(E_Floor.floor3)
    End Sub

    Private Sub ButtonCallFloor1_Click(sender As Object, e As EventArgs) Handles ButtonCallFloor1.Click
        ' Me.AddFloorToList(E_Floor.floor1)
    End Sub



End Class
