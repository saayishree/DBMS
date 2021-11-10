Imports MySql.Data.MySqlClient
Public Class RESERVATION
    Dim connection As New CONNECTION()

    REM-add reservation function
    Function addReservation(ByVal roomNumber As Integer, ByVal clientID As Integer, ByVal dateIn As Date, ByVal dateOut As Date) As Boolean
        Dim command As New MySqlCommand("INSERT INTO 'reservations'('clientID','room_Number','dateIN','dtaeOUT') VALUES (@cid,@rn,@dtin,@dtot)", connection.getConnection())
        '@cid,@rn,@dtin,@dtot
        command.Parameters.Add("@cid", MySqlDbType.Int32).Value = clientID
        command.Parameters.Add("@rn", MySqlDbType.Int32).Value = roomNumber
        command.Parameters.Add("@dtin", MySqlDbType.date).Value = dateIn
        command.Parameters.Add("@dtot", MySqlDbType.date).Value = dateOut

        connection.openConnection()
        If command.ExecuteNonQuery() > 0 Then
            'set the room to reserved
            Dim setRoomToReservedCommand As New MySqlCommand("UPDATE 'rooms' SET 'reserved'='Yes' WHERE 'number'=@num", connection.getConnection())
            setRoomToReservedCommand.Parameters.Add("@num", MySqlDbType.Int32).Value = roomNumber
            setRoomToReservedCommand.ExecuteNonQuery()
            connection.closeConnection()
            Return True

        Else
            connection.closeConnection()
            Return False

        End If
    End Function

    REM-edit reservation function
    Function editReservation(ByVal reservId As Integer, ByVal roomNumber As Integer, ByVal clientID As Integer, ByVal dateIn As Date, ByVal dateOut As Date) As Boolean
        Dim command As New MySqlCommand("UPDATE 'reservations' SET 'client_ID'=@cid,'room_number'=@rn,'dateIN'=@dtin,'dateOUT'=@dtot WHERE 'reservID'=@rvid", connection.getConnection())
        '@cid,@rn,@dtin,@dtot,@rvid
        command.Parameters.Add("@rvid", MySqlDbType.Int32).Value = reservId

        command.Parameters.Add("@cid", MySqlDbType.Int32).Value = clientID
        command.Parameters.Add("@rn", MySqlDbType.Int32).Value = roomNumber
        command.Parameters.Add("@dtin", MySqlDbType.date).Value = dateIn
        command.Parameters.Add("@dtot", MySqlDbType.date).Value = dateOut

        connection.openConnection()
        If command.ExecuteNonQuery() > 0 Then
            connection.closeConnection()
            Return True

        Else
            connection.closeConnection()
            Return False

        End If
    End Function


    REM-REMOVE RESERVATION FUNCTION
    Function removeReservation(ByVal reservId As Integer, ByVal roomNumber As Integer) As Boolean
        Dim command As New MySqlCommand("DELETE FROM  'reservations'  WHERE 'reservID'=@rvid", connection.getConnection())
        '@cid,@rn,@dtin,@dtot,@rvid
        command.Parameters.Add("@rvid", MySqlDbType.Int32).Value = reservId


        connection.openConnection()
        If command.ExecuteNonQuery() > 0 Then
            'set the room to not reserved
            Dim setRoomToNotReservedcommand As New MySqlCommand("UPDATE 'rooms' SET 'reserved'='No' WHERE 'number'=@num", connection.getConnection())
            '@cid,@rn,@dtin,@dtot,@rvid
            setRoomToNotReservedcommand.Parameters.Add("@num", MySqlDbType.Int32).Value = roomNumber
            setRoomToNotReservedcommand.ExecuteNonQuery()

            connection.closeConnection()
            Return True

        Else
            connection.closeConnection()
            Return False

        End If
    End Function

    REM-get all reservation function
    Function getAllReservations() As DataTable
        Dim adapter As New MySqlDataAdapter()
        Dim command As New MySqlCommand()
        Dim table As New DataTable()
        Dim selectQuery As String = "SELECT * FROM 'reservations' "

        command.CommandText = selectQuery
        command.Connection = CONNECTION.getconnection()

        adapter.SelectCommand = command
        adapter.Fill(table)

        Return table
    End Function
End Class
