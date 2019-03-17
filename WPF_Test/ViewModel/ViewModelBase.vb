﻿Imports System.ComponentModel
Imports System.Runtime.CompilerServices

Namespace ViewModel
    Public MustInherit Class ViewModelBase
        Implements INotifyPropertyChanged, IDisposable

        Private Shared ReadOnly HostProcesses As New List(Of String)({"XDesProc", "devenv", "WDExpress"})

        ''' <summary>
        ''' Gibt zurück ob sich die ausführung des Codes aktuell in der Entwicklungszeit oder in der Laufzeit befindet.
        ''' Für eine Logausgabe muss das LogAction Propertie genutzt werden.
        ''' </summary>
        ''' <returns>Wird Code des ViewModels vom XAML Designer ausgeführt wird True zurückgegeben.</returns>
        Public ReadOnly Property IsInDesignMode As Boolean
            Get
                Dim ret = HostProcesses.Contains(Process.GetCurrentProcess().ProcessName)
                Return ret
            End Get
        End Property


#Region "INotifyPropertyChanged"

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged


        ''' <summary>
        ''' Prozedur wirft den INotifyPropertyChanged Event welcher in der WPF benötigt wird um die UI zu verständingen
        ''' das eine Änderung an einem Property stattgefunden hat.
        ''' </summary>
        ''' <param name="prop">Das Propertie welches sich geändert hat. Ist Optional da als Parameter "CallerMemberName" verwendet wird. Wird Nothing übergeben werden alle PRoperties des Views aktualisiert!!</param>
        Protected Overridable Sub RaisePropertyChanged(<CallerMemberName> Optional ByVal prop As String = "")
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(prop))
        End Sub

#End Region


#Region "IDisposable Support"
        Private _disposedValue As Boolean

        ' IDisposable
        Public Overridable Sub Dispose(disposing As Boolean)
            If Not _disposedValue Then
                If disposing Then

                End If

            End If
            _disposedValue = True
        End Sub

        ' Dieser Code wird von Visual Basic hinzugefügt, um das Dispose-Muster richtig zu implementieren.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in Dispose(disposing As Boolean) weiter oben ein.

            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class
End Namespace