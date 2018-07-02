Public Module WS_Movement
    Public Sub On_SMSG_SET_REST_START(ByRef Packet As PacketClass)
        Console.WriteLine("[{0}][World] You're now inside the world.", Format(TimeOfDay, "HH:mm:ss"))
        'that's it... 
        'needs moving by packets and x-y-z positions in space controlable by console 
    End Sub
End Module
