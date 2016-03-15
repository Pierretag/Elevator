Imports RLI___TP2___Elevator.AsyncSocket.AsynchronousSocket
Imports RLI___TP2___Elevator.AsyncSocket.ClientSocket
Imports RLI___TP2___Elevator.AsyncSocket.ServerSocket
Imports System.Text

Public Class Server

    Dim _socket As AsynchronousServer
    Dim currentSensor As E_Sensor
    Dim oldSensor As E_Sensor
    Dim Order As Boolean = False
    Public floorAsked As Integer
    Private floorAsked2 As E_Floor
    Private FloorCalled As List(Of E_Floor) = New List(Of E_Floor)
    Dim isOnFloor As Boolean
    Private Enum E_Sensor
        sensor0
        sensor1
        sensor2
        sensor3
        sensor4
        NoSensor
    End Enum

    Public Sub setFloorAsked(ByVal i As Integer)
        Me.floorAsked = i
    End Sub


    Private Sub FC2_request() 'Read discrete input
        Dim datagram As Byte() = New Byte(12) {}

        Dim t_id_0 As Integer = 0   'mettre en globale
        Dim t_id_1 As Integer = 0   'mettre en globale

        datagram(0) = Convert.ToByte(t_id_0)    'Transaction identifier
        datagram(1) = Convert.ToByte(t_id_1)    'Transaction identifier

        datagram(2) = &H0   'Protocol identifier
        datagram(3) = &H0   'Protocol identifier

        datagram(4) = &H0   'Length field
        datagram(5) = &H6   'Length field

        datagram(6) = &H0   'Unit identifier

        datagram(7) = &H2   'MODBUS function code

        datagram(8) = &H0   'Reference number
        datagram(9) = &H0   'Reference number

        datagram(10) = &H0  'Bit count
        datagram(11) = &H8  'Bit count

        For Each content As Byte In datagram
            Debug.Write("Request " + content.ToString)
        Next

        If t_id_1.Equals(Convert.ToInt32(11111111)) Then
            t_id_0 += 1
            t_id_1 = 0
        Else
            t_id_1 += 1
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

    Private Sub Server_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._socket = New AsynchronousServer()
        Me._socket.AttachReceiveCallBack(AddressOf ReceivedDataFromClient)
        TryCast(_socket, AsynchronousServer).RunServer()
        ' If _socket IsNot Nothing Then
        '_socket.Close()
        ' End If

    End Sub

    Private Sub CallFloor3_Click(sender As Object, e As EventArgs) Handles CallFloor3.Click
        'Me.SendMessageToClient(Encoding.ASCII.GetBytes("Call Floor 3"))
        Me.AddFloorToList(E_Floor.floor3)
    End Sub

    Private Sub CallFloor2_Click(sender As Object, e As EventArgs) Handles CallFloor2.Click
        'Me.SendMessageToClient(Encoding.ASCII.GetBytes("Call Floor 2"))
        Me.AddFloorToList(E_Floor.floor2)
    End Sub

    Private Sub CallFloor1_Click(sender As Object, e As EventArgs) Handles CallFloor1.Click
        'Me.SendMessageToClient(Encoding.ASCII.GetBytes("Call Floor 1"))
        Me.AddFloorToList(E_Floor.floor1)
    End Sub

    Private Sub CallFloor0_Click(sender As Object, e As EventArgs) Handles CallFloor0.Click
        'Me.SendMessageToClient(Encoding.ASCII.GetBytes("Call Floor 0"))
        Me.AddFloorToList(E_Floor.floor0)
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
        ' CheckCoils(Msg)
        'BE CAREFUL!! 
        'If you want to change the properties of CoilUP/CoilDown/LedSensor... here, you must use safe functions. 
        'Functions for CoilUP and CoilDown are given (see SetCoilDown and SetCoilUP)
    End Sub

    Private Sub CheckCoils(ByVal Msg As String)
        Select Case Msg
            Case "UP"

                Me.CoilUP.CheckState = CheckState.Checked
                Me.CoilDown.CheckState = CheckState.Unchecked
            Case "DOWN"
                Me.CoilDown.CheckState = CheckState.Checked
                Me.CoilUP.CheckState = CheckState.Unchecked
        End Select

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
            Case "NoSensor"
                currentSensor = E_Sensor.NoSensor
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

    Private Enum E_Floor
        floor0
        floor1
        floor2
        floor3
    End Enum

    Private Sub AddFloorToList(ByVal floor As E_Floor)

        FloorCalled.Add(floor)
    End Sub

    Private Function selectFloorFromList()
        Dim floorSelected As E_Floor
        'C'est une file, on récupère la donnée du premier élement'
        floorSelected = FloorCalled(0) 'On copie la donnee'
        FloorCalled.RemoveAt(0) 'On supprime l'appel de l'étage traité'

        'FloorCalled.Add(floor)
        Return floorSelected


    End Function


    Private Sub ChooseFloor(ByVal floorChosen As E_Floor)
        Select Case floorChosen
            Case E_Floor.floor0
                If Me.currentSensor = E_Sensor.sensor1 Or Me.currentSensor = E_Sensor.sensor2 Or Me.currentSensor = E_Sensor.sensor3 Then
                    Me.CoilUP.CheckState = CheckState.Unchecked
                    Me.CoilDown.CheckState = CheckState.Checked
                End If
                If Me.currentSensor = E_Sensor.sensor0 Then
                    Me.CoilUP.CheckState = CheckState.Unchecked
                    Me.CoilDown.CheckState = CheckState.Unchecked
                    Me.isOnFloor = True
                    Me.setFloorAsked(4)
                End If
            Case E_Floor.floor1

                If Me.currentSensor = E_Sensor.sensor1 Or Me.currentSensor = E_Sensor.sensor0 Then
                    Me.CoilDown.CheckState = CheckState.Unchecked
                    Me.CoilUP.CheckState = CheckState.Checked
                End If
                If Me.currentSensor = E_Sensor.sensor3 Or Me.currentSensor = E_Sensor.sensor4 Then
                    Me.CoilUP.CheckState = CheckState.Unchecked
                    Me.CoilDown.CheckState = CheckState.Checked
                End If
                If Me.currentSensor = E_Sensor.sensor2 Then
                    Me.CoilUP.CheckState = CheckState.Unchecked
                    Me.CoilDown.CheckState = CheckState.Unchecked
                    Me.setFloorAsked(4)
                End If
            Case E_Floor.floor2
                If Me.currentSensor = E_Sensor.sensor1 Or Me.currentSensor = E_Sensor.sensor0 Or Me.currentSensor = E_Sensor.sensor2 Then
                    Me.CoilDown.CheckState = CheckState.Unchecked
                    Me.CoilUP.CheckState = CheckState.Checked
                End If
                If Me.currentSensor = E_Sensor.sensor4 Then
                    Me.CoilUP.CheckState = CheckState.Unchecked
                    Me.CoilDown.CheckState = CheckState.Checked
                End If
                If Me.currentSensor = E_Sensor.sensor3 Then
                    Me.CoilUP.CheckState = CheckState.Unchecked
                    Me.CoilDown.CheckState = CheckState.Unchecked
                    Me.setFloorAsked(4)
                End If
            Case E_Floor.floor3

                If Me.currentSensor = E_Sensor.sensor0 Or Me.currentSensor = E_Sensor.sensor1 Or Me.currentSensor = E_Sensor.sensor2 Or Me.currentSensor = E_Sensor.sensor3 Then
                    Me.CoilDown.CheckState = CheckState.Unchecked
                    Me.CoilUP.CheckState = CheckState.Checked
                End If
                If Me.currentSensor = E_Sensor.sensor4 Then
                    Me.CoilUP.CheckState = CheckState.Unchecked
                    Me.CoilDown.CheckState = CheckState.Unchecked
                    Me.setFloorAsked(4)
                End If
            Case 4 'Aucun étage n'est appelé'
                Me.isOnFloor = True
        End Select


        'On gère le booleen isOnFloor'
        If Me.currentSensor = E_Sensor.NoSensor Then
            Me.isOnFloor = True
        Else : Me.isOnFloor = False

        End If
    End Sub



    Private Sub UpdateTimer_Tick(sender As Object, e As EventArgs) Handles UpdateTimer.Tick
        If Order = False Then
            SendMessageToClient(Encoding.ASCII.GetBytes("UpdateSensor"))
            Order = True
        Else
            SendCoils()
            Order = False
        End If

        If FloorCalled.Count <> 0 And CoilDown.CheckState = CheckState.Unchecked And CoilUP.CheckState = CheckState.Unchecked Then

            floorAsked2 = Me.selectFloorFromList()
        End If

        ChooseFloor(floorAsked2)

    End Sub

    Private Sub SendCoils()
        If CoilDown.CheckState = CheckState.Checked Then
            SendMessageToClient(Encoding.ASCII.GetBytes("DOWN"))
        ElseIf CoilUP.CheckState = CheckState.Checked Then
            SendMessageToClient(Encoding.ASCII.GetBytes("UP"))
        Else : SendMessageToClient(Encoding.ASCII.GetBytes("NO"))
        End If

    End Sub
End Class