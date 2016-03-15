Imports System.Text
Imports RLI___TP2___Elevator.AsyncSocket.AsynchronousSocket
Imports RLI___TP2___Elevator.AsyncSocket.ClientSocket
Imports RLI___TP2___Elevator.AsyncSocket.ServerSocket

Public Class Elevator
    Public Shared ServerName As String = "localhost"
    Private serverIsRunning As Boolean = False
    Private clientIsRunning As Boolean = False
    Public server As Server



    
    Private Sub FC2_response(request As Byte()) 'Read discrete input
        Dim datagram As Byte() = New Byte(10) {}

        Dim t_id_0 As Integer = 0   'mettre en globale
        Dim t_id_1 As Integer = 0   'mettre en globale

        Dim i As Integer = 0

        Do While i <= 6
            datagram(i) = request(i)
            i += 1
        Loop

        datagram(7) = &H2   'MODBUS function code

        datagram(8) = &H1   'Byte count
        'datagram(9) = Convert.ToByte(int aSensor) 'Bit values
        datagram(9) = &H0   'Bit values

        For Each content As Byte In datagram
            Debug.Write("Response " + content.ToString)
        Next
    End Sub

    Private Sub FC2_exception(request As Byte()) 'Read discrete input
        Dim datagram As Byte() = New Byte(9) {}

        Dim t_id_0 As Integer = 0   'mettre en globale
        Dim t_id_1 As Integer = 0   'mettre en globale

        Dim i As Integer = 0

        Do While i <= 6
            datagram(i) = request(i)
            i += 1
        Loop

        datagram(7) = &H82   'MODBUS function code

        datagram(8) = &H1   'Exception code 0x01 or 0x02

        For Each content As Byte In datagram
            Debug.Write("Response " + content.ToString)
        Next
    End Sub


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
        Dim Msg As String = Encoding.ASCII.GetString(e.ReceivedBytes)
        'MessageBox.Show("Server says :" + Msg, "I am Client")
        Select Case Msg
            Case "UpdateSensor"
                SendCurrentSensor()
            Case "UP"
                CoilUP.CheckState = CheckState.Checked
                CoilDown.CheckState = CheckState.Unchecked
            Case "DOWN"
                CoilDown.CheckState = CheckState.Checked
                CoilUP.CheckState = CheckState.Unchecked
        End Select
        'BE CAREFUL!! 
        'If you want to change the properties of CoilUP/CoilDown/LedSensor... here, you must use safe functions. 
        'Functions for CoilUP and CoilDown are given (see SetCoilDown and SetCoilUP)
    End Sub


    Private Sub SendCoilsToServer()
        If CoilUP.Checked Then
            SendMessageToServer(Encoding.ASCII.GetBytes("UP"))
        End If
        If CoilDown.Checked Then
            SendMessageToServer(Encoding.ASCII.GetBytes("DOWN"))
        End If
    End Sub


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

    Private Sub SendCurrentSensor()
        If clientIsRunning Then
            Me.SendMessageToServer(Encoding.ASCII.GetBytes(currentSensor.ToString))
        End If
    End Sub





    

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
