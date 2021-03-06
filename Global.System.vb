Imports System.Runtime.InteropServices

Public Module Global_System
    Public Class ConsoleColorClass
        Public Enum ForegroundColors
            Black = 0
            Blue = 1
            Green = 2
            Cyan = Blue Or Green
            Red = 4
            Magenta = Blue Or Red
            Yellow = Green Or Red
            White = Blue Or Green Or Red
            Gray = 8
            LightBlue = Gray Or Blue
            LightGreen = Gray Or Green
            LightCyan = Gray Or Cyan
            LightRed = Gray Or Red
            LightMagenta = Gray Or Magenta
            LightYellow = Gray Or Yellow
            BrightWhite = Gray Or White
        End Enum
        Public Enum BackgroundColors
            Black = 0
            Blue = 16
            Green = 32
            Cyan = Blue Or Green
            Red = 64
            Magenta = Blue Or Red
            Yellow = Green Or Red
            White = Blue Or Green Or Red
            Gray = 128
            LightBlue = Gray Or Blue
            LightGreen = Gray Or Green
            LightCyan = Gray Or Cyan
            LightRed = Gray Or Red
            LightMagenta = Gray Or Magenta
            LightYellow = Gray Or Yellow
            BrightWhite = Gray Or White
        End Enum
        Public Enum Attributes
            None = &H0
            GridHorizontal = &H400
            GridLVertical = &H800
            GridRVertical = &H1000
            ReverseVideo = &H4000
            Underscore = &H8000
        End Enum

        Private Const STD_OUTPUT_HANDLE As Integer = -11
        Private Shared InvalidHandleValue As New IntPtr(-1)

        Public Sub New()
            ' This class can not be instantiated.
        End Sub

        ' Our wrapper implementations.
        Public Sub SetConsoleColor()
            SetConsoleColor(ForegroundColors.White, BackgroundColors.Black)
        End Sub
        Public Sub SetConsoleColor(ByVal foreground As ForegroundColors)
            SetConsoleColor(foreground, BackgroundColors.Black, Attributes.None)
        End Sub
        Public Sub SetConsoleColor(ByVal foreground As ForegroundColors, ByVal background As BackgroundColors)
            SetConsoleColor(foreground, background, Attributes.None)
        End Sub
        Public Sub SetConsoleColor(ByVal foreground As ForegroundColors, ByVal background As BackgroundColors, ByVal attribute As Attributes)
            Dim handle As IntPtr = GetStdHandle(STD_OUTPUT_HANDLE)
            If handle.Equals(InvalidHandleValue) Then
                Throw New System.ComponentModel.Win32Exception
            End If
            ' We have to convert the integer flag values into a Unsigned Short (UInt16) to pass to the 
            ' SetConsoleTextAttribute API call.
            Dim value As UInt16 = System.Convert.ToUInt16(foreground Or background Or attribute)
            If Not SetConsoleTextAttribute(handle, value) Then
                Throw New System.ComponentModel.Win32Exception
            End If
        End Sub

        ' DLLImport's (Win32 functions)
        <DllImport("Kernel32.dll", SetLastError:=True)> Private Shared Function GetStdHandle(ByVal stdHandle As Integer) As IntPtr
        End Function
        <DllImport("Kernel32.dll", SetLastError:=True)> Private Shared Function SetConsoleTextAttribute(ByVal consoleOutput As IntPtr, ByVal Attributes As UInt16) As Boolean
        End Function
        <DllImport("Kernel32.dll", SetLastError:=True)> Public Shared Function SetConsoleTitle(ByVal ConsoleTitle As String) As Boolean
        End Function
    End Class

    Public Function BytesToHex(ByRef bBytes() As Byte) As String
        Return BitConverter.ToString(bBytes).Replace("-", "")
    End Function
End Module
