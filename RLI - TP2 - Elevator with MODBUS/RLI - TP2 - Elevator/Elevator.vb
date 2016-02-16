Imports System.Text
Imports RLI___TP2___Elevator.AsyncSocket.AsynchronousSocket
Imports RLI___TP2___Elevator.AsyncSocket.ClientSocket
Imports RLI___TP2___Elevator.AsyncSocket.ServerSocket

Public Class Elevator
    Public Shared ServerName As String = "localhost"
    Private serverIsRunning As Boolean = False
    Private clientIsRunning As Boolean = False
    Public floorAsked As Integer
    Dim isOnFloor As Boolean


    Public Sub setFloorAsked(ByVal i As Integer)
        Me.floorAsked = i
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
            Me._socket = New AsynchronousServer()
            Me._socket.AttachReceiveCallBack(AddressOf ReceivedDataFromClient)
            TryCast(_socket, AsynchronousServer).RunServer()

            Me.serverIsRunning = True
            Me.LauchServer.ForeColor = System.Drawing.Color.Green
            Me.LauchServer.Text = "Stop the Server"
        Else
            If _socket IsNot Nothing Then
                _socket.Close()
            End If
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

    Public Sub SendMessageToClient(ByVal msg As Byte())
        If _socket IsNot Nothing Then
            If TryCast(_socket, AsynchronousServer) IsNot Nothing Then
                Me._socket.SendMessage(msg)
            End If
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
        MessageBox.Show("Server says :" + Encoding.ASCII.GetString(e.ReceivedBytes), "I am Client")

        'BE CAREFUL!! 
        'If you want to change the properties of CoilUP/CoilDown/LedSensor... here, you must use safe functions. 
        'Functions for CoilUP and CoilDown are given (see SetCoilDown and SetCoilUP)
    End Sub

    Private Sub ReceivedDataFromClient(ByVal sender As Object, ByVal e As AsyncEventArgs)
        'Add some stuff to interpret messages (and remove the next line!)
        'Bytes are in e.ReceivedBytes and you can encore the bytes to string using Encoding.ASCII.GetString(e.ReceivedBytes)
        MessageBox.Show("Client says :" + Encoding.ASCII.GetString(e.ReceivedBytes), "I am Server")

        'BE CAREFUL!! 
        'If you want to change the properties of CoilUP/CoilDown/LedSensor... here, you must use safe functions. 
        'Functions for CoilUP and CoilDown are given (see SetCoilDown and SetCoilUP)
    End Sub



    Private Sub ButtonCallFloor2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCallFloor2.Click
        Me.setFloorAsked(2)

        If serverIsRunning Then
            Me.SendMessageToClient(Encoding.ASCII.GetBytes("Coucou client !"))
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

        Me.ChooseFloor(Me.floorAsked)
        If isOnFloor = False Then
            BlinkLedSensor()
        Else
            ClearLedSensor()
        End If
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
    End Sub

    Private Sub BlinkLedSensor()
        CheckSensor()
        If oldSensor <> currentSensor Then
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
        End If
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

    Private Sub ChooseFloor(ByVal floorChosen As Integer)
        Select Case floorChosen
            Case 0
                Me.isOnFloor = False
                If Me.ElevatorPhys.Location.Y < Me.PositionSensor1.Location.Y + 3 Then
                    Me.CoilUP.CheckState = CheckState.Unchecked
                    Me.CoilDown.CheckState = CheckState.Checked
                End If
                If Me.ElevatorPhys.Location.Y = Me.PositionSensor1.Location.Y + 3 Then
                    Me.CoilUP.CheckState = CheckState.Unchecked
                    Me.CoilDown.CheckState = CheckState.Unchecked
                    Me.isOnFloor = True
                    Me.setFloorAsked(4)
                End If
            Case 1
                Me.isOnFloor = False
                If Me.ElevatorPhys.Location.Y > Me.PositionSensor2.Location.Y + 3 Then
                    Me.CoilDown.CheckState = CheckState.Unchecked
                    Me.CoilUP.CheckState = CheckState.Checked
                End If
                If Me.ElevatorPhys.Location.Y < Me.PositionSensor2.Location.Y + 3 Then
                    Me.CoilUP.CheckState = CheckState.Unchecked
                    Me.CoilDown.CheckState = CheckState.Checked
                End If
                If Me.ElevatorPhys.Location.Y = Me.PositionSensor2.Location.Y + 3 Then
                    Me.CoilUP.CheckState = CheckState.Unchecked
                    Me.CoilDown.CheckState = CheckState.Unchecked
                    Me.isOnFloor = True
                    Me.setFloorAsked(4)
                End If
            Case 2
                Me.isOnFloor = False
                If Me.ElevatorPhys.Location.Y > Me.PositionSensor3.Location.Y + 3 Then
                    Me.CoilDown.CheckState = CheckState.Unchecked
                    Me.CoilUP.CheckState = CheckState.Checked
                End If
                If Me.ElevatorPhys.Location.Y < Me.PositionSensor3.Location.Y + 3 Then
                    Me.CoilUP.CheckState = CheckState.Unchecked
                    Me.CoilDown.CheckState = CheckState.Checked
                End If
                If Me.ElevatorPhys.Location.Y = Me.PositionSensor3.Location.Y + 3 Then
                    Me.CoilUP.CheckState = CheckState.Unchecked
                    Me.CoilDown.CheckState = CheckState.Unchecked
                    Me.isOnFloor = True
                    Me.setFloorAsked(4)
                End If
            Case 3
                Me.isOnFloor = False
                If Me.ElevatorPhys.Location.Y > Me.PositionSensor4.Location.Y + 3 Then
                    Me.CoilDown.CheckState = CheckState.Unchecked
                    Me.CoilUP.CheckState = CheckState.Checked
                End If
                If Me.ElevatorPhys.Location.Y = Me.PositionSensor4.Location.Y + 3 Then
                    Me.CoilUP.CheckState = CheckState.Unchecked
                    Me.CoilDown.CheckState = CheckState.Unchecked
                    Me.isOnFloor = True
                    Me.setFloorAsked(4)
                End If
            Case 4 'Aucun étage n'est appelé'
                Me.isOnFloor = True
        End Select

    End Sub


    Private Sub ButtonCallFloor0_Click(sender As Object, e As EventArgs) Handles ButtonCallFloor0.Click
        Me.setFloorAsked(0)
    End Sub






    Private Sub CoilDown_CheckStateChanged(sender As Object, e As EventArgs) Handles CoilDown.CheckStateChanged
        Me.CoilUP.CheckState = CheckState.Unchecked
    End Sub

    Private Sub CoilUP_CheckStateChanged_1(sender As Object, e As EventArgs) Handles CoilUP.CheckStateChanged
        Me.CoilDown.CheckState = CheckState.Unchecked
    End Sub

    Private Sub ButtonCallFloor3_Click(sender As Object, e As EventArgs) Handles ButtonCallFloor3.Click
        Me.setFloorAsked(3)
    End Sub

    Private Sub ButtonCallFloor1_Click(sender As Object, e As EventArgs) Handles ButtonCallFloor1.Click
        Me.setFloorAsked(1)
    End Sub

    'TEST broooooo

End Class
