<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Server
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.CallFloor3 = New System.Windows.Forms.Button()
        Me.CallFloor2 = New System.Windows.Forms.Button()
        Me.CallFloor1 = New System.Windows.Forms.Button()
        Me.CallFloor0 = New System.Windows.Forms.Button()
        Me.PanelSensors = New System.Windows.Forms.Panel()
        Me.LabelLedSensor4 = New System.Windows.Forms.Label()
        Me.LedSensor4 = New System.Windows.Forms.Panel()
        Me.LabelLedSensor3 = New System.Windows.Forms.Label()
        Me.LabelLedSensor2 = New System.Windows.Forms.Label()
        Me.LabelLedSensor1 = New System.Windows.Forms.Label()
        Me.LabelLedSensor0 = New System.Windows.Forms.Label()
        Me.LedSensor3 = New System.Windows.Forms.Panel()
        Me.LedSensor2 = New System.Windows.Forms.Panel()
        Me.LedSensor1 = New System.Windows.Forms.Panel()
        Me.LedSensor0 = New System.Windows.Forms.Panel()
        Me.LabelSensors = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.CoilDown = New System.Windows.Forms.CheckBox()
        Me.CoilUP = New System.Windows.Forms.CheckBox()
        Me.LabelCoils = New System.Windows.Forms.Label()
        Me.PanelSensors.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'CallFloor3
        '
        Me.CallFloor3.Location = New System.Drawing.Point(183, 48)
        Me.CallFloor3.Name = "CallFloor3"
        Me.CallFloor3.Size = New System.Drawing.Size(75, 23)
        Me.CallFloor3.TabIndex = 0
        Me.CallFloor3.Text = "Call Floor 3"
        Me.CallFloor3.UseVisualStyleBackColor = True
        '
        'CallFloor2
        '
        Me.CallFloor2.Location = New System.Drawing.Point(183, 77)
        Me.CallFloor2.Name = "CallFloor2"
        Me.CallFloor2.Size = New System.Drawing.Size(75, 23)
        Me.CallFloor2.TabIndex = 1
        Me.CallFloor2.Text = "Call Floor 2"
        Me.CallFloor2.UseVisualStyleBackColor = True
        '
        'CallFloor1
        '
        Me.CallFloor1.Location = New System.Drawing.Point(183, 106)
        Me.CallFloor1.Name = "CallFloor1"
        Me.CallFloor1.Size = New System.Drawing.Size(75, 23)
        Me.CallFloor1.TabIndex = 2
        Me.CallFloor1.Text = "Call Floor 1"
        Me.CallFloor1.UseVisualStyleBackColor = True
        '
        'CallFloor0
        '
        Me.CallFloor0.Location = New System.Drawing.Point(183, 135)
        Me.CallFloor0.Name = "CallFloor0"
        Me.CallFloor0.Size = New System.Drawing.Size(75, 23)
        Me.CallFloor0.TabIndex = 3
        Me.CallFloor0.Text = "Call Floor 0"
        Me.CallFloor0.UseVisualStyleBackColor = True
        '
        'PanelSensors
        '
        Me.PanelSensors.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PanelSensors.Controls.Add(Me.LabelLedSensor4)
        Me.PanelSensors.Controls.Add(Me.LedSensor4)
        Me.PanelSensors.Controls.Add(Me.LabelLedSensor3)
        Me.PanelSensors.Controls.Add(Me.LabelLedSensor2)
        Me.PanelSensors.Controls.Add(Me.LabelLedSensor1)
        Me.PanelSensors.Controls.Add(Me.LabelLedSensor0)
        Me.PanelSensors.Controls.Add(Me.LedSensor3)
        Me.PanelSensors.Controls.Add(Me.LedSensor2)
        Me.PanelSensors.Controls.Add(Me.LedSensor1)
        Me.PanelSensors.Controls.Add(Me.LedSensor0)
        Me.PanelSensors.Controls.Add(Me.LabelSensors)
        Me.PanelSensors.Location = New System.Drawing.Point(12, 48)
        Me.PanelSensors.Name = "PanelSensors"
        Me.PanelSensors.Size = New System.Drawing.Size(155, 77)
        Me.PanelSensors.TabIndex = 20
        '
        'LabelLedSensor4
        '
        Me.LabelLedSensor4.AutoSize = True
        Me.LabelLedSensor4.Location = New System.Drawing.Point(121, 33)
        Me.LabelLedSensor4.Name = "LabelLedSensor4"
        Me.LabelLedSensor4.Size = New System.Drawing.Size(13, 13)
        Me.LabelLedSensor4.TabIndex = 13
        Me.LabelLedSensor4.Text = "4"
        '
        'LedSensor4
        '
        Me.LedSensor4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LedSensor4.Location = New System.Drawing.Point(118, 49)
        Me.LedSensor4.Name = "LedSensor4"
        Me.LedSensor4.Size = New System.Drawing.Size(20, 20)
        Me.LedSensor4.TabIndex = 12
        '
        'LabelLedSensor3
        '
        Me.LabelLedSensor3.AutoSize = True
        Me.LabelLedSensor3.Location = New System.Drawing.Point(96, 33)
        Me.LabelLedSensor3.Name = "LabelLedSensor3"
        Me.LabelLedSensor3.Size = New System.Drawing.Size(13, 13)
        Me.LabelLedSensor3.TabIndex = 11
        Me.LabelLedSensor3.Text = "3"
        '
        'LabelLedSensor2
        '
        Me.LabelLedSensor2.AutoSize = True
        Me.LabelLedSensor2.Location = New System.Drawing.Point(70, 33)
        Me.LabelLedSensor2.Name = "LabelLedSensor2"
        Me.LabelLedSensor2.Size = New System.Drawing.Size(13, 13)
        Me.LabelLedSensor2.TabIndex = 10
        Me.LabelLedSensor2.Text = "2"
        '
        'LabelLedSensor1
        '
        Me.LabelLedSensor1.AutoSize = True
        Me.LabelLedSensor1.Location = New System.Drawing.Point(44, 33)
        Me.LabelLedSensor1.Name = "LabelLedSensor1"
        Me.LabelLedSensor1.Size = New System.Drawing.Size(13, 13)
        Me.LabelLedSensor1.TabIndex = 9
        Me.LabelLedSensor1.Text = "1"
        '
        'LabelLedSensor0
        '
        Me.LabelLedSensor0.AutoSize = True
        Me.LabelLedSensor0.Location = New System.Drawing.Point(18, 33)
        Me.LabelLedSensor0.Name = "LabelLedSensor0"
        Me.LabelLedSensor0.Size = New System.Drawing.Size(13, 13)
        Me.LabelLedSensor0.TabIndex = 8
        Me.LabelLedSensor0.Text = "0"
        '
        'LedSensor3
        '
        Me.LedSensor3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LedSensor3.Location = New System.Drawing.Point(93, 49)
        Me.LedSensor3.Name = "LedSensor3"
        Me.LedSensor3.Size = New System.Drawing.Size(20, 20)
        Me.LedSensor3.TabIndex = 7
        '
        'LedSensor2
        '
        Me.LedSensor2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LedSensor2.Location = New System.Drawing.Point(67, 49)
        Me.LedSensor2.Name = "LedSensor2"
        Me.LedSensor2.Size = New System.Drawing.Size(20, 20)
        Me.LedSensor2.TabIndex = 6
        '
        'LedSensor1
        '
        Me.LedSensor1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LedSensor1.Location = New System.Drawing.Point(41, 49)
        Me.LedSensor1.Name = "LedSensor1"
        Me.LedSensor1.Size = New System.Drawing.Size(20, 20)
        Me.LedSensor1.TabIndex = 5
        '
        'LedSensor0
        '
        Me.LedSensor0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LedSensor0.Location = New System.Drawing.Point(15, 49)
        Me.LedSensor0.Name = "LedSensor0"
        Me.LedSensor0.Size = New System.Drawing.Size(20, 20)
        Me.LedSensor0.TabIndex = 4
        '
        'LabelSensors
        '
        Me.LabelSensors.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LabelSensors.AutoSize = True
        Me.LabelSensors.Font = New System.Drawing.Font("Elephant", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelSensors.Location = New System.Drawing.Point(3, -2)
        Me.LabelSensors.Name = "LabelSensors"
        Me.LabelSensors.Size = New System.Drawing.Size(145, 21)
        Me.LabelSensors.TabIndex = 3
        Me.LabelSensors.Text = "Sensors/Inputs"
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.CoilDown)
        Me.Panel1.Controls.Add(Me.CoilUP)
        Me.Panel1.Controls.Add(Me.LabelCoils)
        Me.Panel1.Location = New System.Drawing.Point(12, 150)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(155, 77)
        Me.Panel1.TabIndex = 21
        '
        'CoilDown
        '
        Me.CoilDown.AutoSize = True
        Me.CoilDown.Location = New System.Drawing.Point(49, 48)
        Me.CoilDown.Name = "CoilDown"
        Me.CoilDown.Size = New System.Drawing.Size(54, 17)
        Me.CoilDown.TabIndex = 5
        Me.CoilDown.Text = "Down"
        Me.CoilDown.UseVisualStyleBackColor = True
        '
        'CoilUP
        '
        Me.CoilUP.AutoSize = True
        Me.CoilUP.Location = New System.Drawing.Point(49, 24)
        Me.CoilUP.Name = "CoilUP"
        Me.CoilUP.Size = New System.Drawing.Size(41, 17)
        Me.CoilUP.TabIndex = 4
        Me.CoilUP.Text = "UP"
        Me.CoilUP.UseVisualStyleBackColor = True
        '
        'LabelCoils
        '
        Me.LabelCoils.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LabelCoils.AutoSize = True
        Me.LabelCoils.Font = New System.Drawing.Font("Elephant", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelCoils.Location = New System.Drawing.Point(8, 0)
        Me.LabelCoils.Name = "LabelCoils"
        Me.LabelCoils.Size = New System.Drawing.Size(134, 21)
        Me.LabelCoils.TabIndex = 3
        Me.LabelCoils.Text = "Coils/Outputs"
        '
        'Server
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.PanelSensors)
        Me.Controls.Add(Me.CallFloor0)
        Me.Controls.Add(Me.CallFloor1)
        Me.Controls.Add(Me.CallFloor2)
        Me.Controls.Add(Me.CallFloor3)
        Me.Name = "Server"
        Me.Text = "Server"
        Me.PanelSensors.ResumeLayout(False)
        Me.PanelSensors.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CallFloor3 As System.Windows.Forms.Button
    Friend WithEvents CallFloor2 As System.Windows.Forms.Button
    Friend WithEvents CallFloor1 As System.Windows.Forms.Button
    Friend WithEvents CallFloor0 As System.Windows.Forms.Button
    Friend WithEvents PanelSensors As System.Windows.Forms.Panel
    Friend WithEvents LabelLedSensor4 As System.Windows.Forms.Label
    Friend WithEvents LedSensor4 As System.Windows.Forms.Panel
    Friend WithEvents LabelLedSensor3 As System.Windows.Forms.Label
    Friend WithEvents LabelLedSensor2 As System.Windows.Forms.Label
    Friend WithEvents LabelLedSensor1 As System.Windows.Forms.Label
    Friend WithEvents LabelLedSensor0 As System.Windows.Forms.Label
    Friend WithEvents LedSensor3 As System.Windows.Forms.Panel
    Friend WithEvents LedSensor2 As System.Windows.Forms.Panel
    Friend WithEvents LedSensor1 As System.Windows.Forms.Panel
    Friend WithEvents LedSensor0 As System.Windows.Forms.Panel
    Friend WithEvents LabelSensors As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Public WithEvents CoilDown As System.Windows.Forms.CheckBox
    Friend WithEvents CoilUP As System.Windows.Forms.CheckBox
    Friend WithEvents LabelCoils As System.Windows.Forms.Label

End Class
