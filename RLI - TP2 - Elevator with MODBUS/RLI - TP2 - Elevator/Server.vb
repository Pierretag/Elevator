Imports RLI___TP2___Elevator.AsyncSocket.AsynchronousSocket
Imports RLI___TP2___Elevator.AsyncSocket.ClientSocket
Imports RLI___TP2___Elevator.AsyncSocket.ServerSocket
Imports System.Text

Public Class Server

    Dim _socket As AsynchronousServer
    Dim currentSensor As E_Sensor
    Dim oldSensor As E_Sensor
    Private Enum E_Sensor
        sensor0
        sensor1
        sensor2
        sensor3
        sensor4
    End Enum


    Private Sub Server_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._socket = New AsynchronousServer()
        Me._socket.AttachReceiveCallBack(AddressOf ReceivedDataFromClient)
        TryCast(_socket, AsynchronousServer).RunServer()
        ' If _socket IsNot Nothing Then
        '_socket.Close()
        ' End If

    End Sub

    Private Sub CallFloor3_Click(sender As Object, e As EventArgs) Handles CallFloor3.Click
        Me.SendMessageToClient(Encoding.ASCII.GetBytes("Call Floor 3"))
    End Sub

    Private Sub CallFloor2_Click(sender As Object, e As EventArgs) Handles CallFloor2.Click
        Me.SendMessageToClient(Encoding.ASCII.GetBytes("Call Floor 2"))
    End Sub

    Private Sub CallFloor1_Click(sender As Object, e As EventArgs) Handles CallFloor1.Click
        Me.SendMessageToClient(Encoding.ASCII.GetBytes("Call Floor 1"))
    End Sub

    Private Sub CallFloor0_Click(sender As Object, e As EventArgs) Handles CallFloor0.Click
        Me.SendMessageToClient(Encoding.ASCII.GetBytes("Call Floor 0"))
    End Sub



    Public Sub SendMessageToClient(ByVal msg As Byte())
        If _socket IsNot Nothing Then
            If TryCast(_socket, AsynchronousServer) IsNot Nothing Then
                Me._socket.SendMessage(msg)
            End If
        End If
    End Sub


    Private Sub ReceivedDataFromClient(ByVal sender As Object, ByVal e As AsyncEventArgs)
        'Add some stuff to interpret messages (and remove the next line!)
        'Bytes are in e.ReceivedBytes and you can encore the bytes to string using Encoding.ASCII.GetString(e.ReceivedBytes)
        Dim Msg As String = Encoding.ASCII.GetString(e.ReceivedBytes)
        BlinkLedSensor(Msg)

        'BE CAREFUL!! 
        'If you want to change the properties of CoilUP/CoilDown/LedSensor... here, you must use safe functions. 
        'Functions for CoilUP and CoilDown are given (see SetCoilDown and SetCoilUP)
    End Sub

    

    Private Sub CheckSensor(ByVal Msg As String)
        Select Case Msg
            Case "sensor0"
                currentSensor = E_Sensor.sensor0
            Case "sensor1"
                currentSensor = E_Sensor.sensor1
            Case "sensor2"
                currentSensor = E_Sensor.sensor2
            Case "sensor3"
                currentSensor = E_Sensor.sensor3
            Case "sensor4"
                currentSensor = E_Sensor.sensor4
        End Select
    End Sub

    Private Sub BlinkLedSensor(ByVal Msg As String)
        CheckSensor(Msg)
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


    

End Class