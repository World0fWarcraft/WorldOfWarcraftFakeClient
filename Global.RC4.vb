Public Class RC4
    'http://www.skullsecurity.org/wiki/index.php/Crypto_and_Hashing

    Public Shared Function Init(ByVal base() As Byte) As Byte()
        Dim val As Integer = 0
        Dim position As Integer = 0
        Dim temp As Byte

        Dim key(255 + 2) As Byte

        For i As Integer = 0 To 256 - 1
            key(i) = i
        Next

        key(256) = 0
        key(257) = 0

        For i As Integer = 1 To 64
            val = val + key((i * 4) - 4) + base(position Mod base.Length)
            val = val And &HFF
            position += 1
            temp = key((i * 4) - 4)
            key((i * 4) - 4) = key(val And &HFF)
            key(val And &HFF) = temp

            val = val + key((i * 4) - 3) + base(position Mod base.Length)
            val = val And &HFF
            position += 1
            temp = key((i * 4) - 3)
            key((i * 4) - 3) = key(val And &HFF)
            key(val And &HFF) = temp

            val = val + key((i * 4) - 2) + base(position Mod base.Length)
            val = val And &HFF
            position += 1
            temp = key((i * 4) - 2)
            key((i * 4) - 2) = key(val And &HFF)
            key(val And &HFF) = temp

            val = val + key((i * 4) - 1) + base(position Mod base.Length)
            val = val And &HFF
            position += 1
            temp = key((i * 4) - 1)
            key((i * 4) - 1) = key(val And &HFF)
            key(val And &HFF) = temp
        Next

        Return key
    End Function
    Public Shared Sub Crypt(ByRef data As Byte(), ByVal key As Byte())
        Dim temp As Byte
        For i As Integer = 0 To data.Length - 1
            key(256) = (CType(key(256), Integer) + 1) And &HFF
            key(257) = (CType(key(257), Integer) + CType(key(key(256)), Integer)) And &HFF

            temp = key(key(257) And &HFF)
            key(key(257) And &HFF) = key(key(256) And &HFF)
            key(key(256) And &HFF) = temp

            data(i) = (data(i) Xor key((CType(key(key(257)), Integer) + CType(key(key(256)), Integer)) And &HFF))
        Next
    End Sub
End Class
