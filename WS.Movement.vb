Public Module WS_Movement
    Public Sub On_SMSG_SET_REST_START(ByRef Packet As PacketClass)
        Console.WriteLine("[{0}][World] You're now inside the world.", Format(TimeOfDay, "HH:mm:ss"))
    End Sub
End Module
