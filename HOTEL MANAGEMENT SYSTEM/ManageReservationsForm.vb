Public Class ManageReservationsForm
    Dim room As New ROOMS()

    Dim reservation As New RESERVATION()

    Private Sub ManageReservationForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'populate the datagridview with all the reservation 
        DataGridView1.DataSource = reservation.getAllReservations()


        'populate the combobox type with all room's type
        ComboBoxType.DataSource = room.getAllRoomsType()
        ComboBoxType.DisplayMember = "label"
        ComboBoxType.ValueMember = "id"

        'populate the combobox room number with all the not reserved room numbers
        Dim roomType As Integer = Convert.ToInt32(ComboBoxType.SelectedValue.ToString())
        ComboBoxRoomNumber.DataSource = room.getRoomsByType(roomType)
        ComboBoxRoomNumber.DisplayMember = "number"
        ComboBoxRoomNumber.ValueMember = "number"




    End Sub

    Private Sub ComboBoxType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxType
            Try
            'Display room numbers depending on the selected type
            Dim roomType As Integer = Convert.ToInt32(ComboBoxType.SelectedValue.ToString())
            ComboBoxRoomNumber.DataSource = room.getRoomsByType(roomType)
            ComboBoxRoomNumber.DisplayMember = "number"
            ComboBoxRoomNumber.ValueMember = "number"

        Catch ex As Exception

        End Try

    End Sub

    Private Sub ButtonAdd_Click(Sender As Object, e As EventArgs) Handles ButtonAdd.Click

        Dim clientId As Integer = Convert.ToInt32(TextBoxClientID.Text)
        Dim roomNumber As Integer = Convert.ToInt32(ComboBoxroomNumber.SelectedValue.ToString())
        Dim dateIn As Date = DateTimePickerIN.Value
        Dim dateOut As Date = DateTimePickerOUT.Value
        If DateTime.Compare(dateIn.Date, DateTime.Now.Date) < 0 Then

            MessageBox.Show("The Date In Must be = Or > to Today Date", "Invalid Date IN", MessageBoxButtons.OK, MessageBoxIcon.Error)

        ElseIf DateTime.Compare(dateOut.Date, dateIn.Date) < 0 Then

            MessageBox.Show("The Date Out Must be = Or > to The Date In", "Invalid Date OUT", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Else
            If reservation.addReservation(roomNumber, clientId, dateIn, dateOut) Then

                MessageBox.Show("Resservation Added Successfully", "Add Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information)
                DateGridView1.DataSource = reservation.getAllReservations()
                'we need to refresh the combobox to show only the not reserved rooms
                ComboBoxType.DataSource = room.getAllRoomType()



            Else
                MessageBox.Show("Reservation Not Added", "Add Reservation", MessageBoxButtons.OK, MessageBoxIcon.Error)

            End If

        End If


    End Sub

    Private Sub ButtonEdit_Click(Sender As Object, e As EventArgs) Handles ButtonEdit.Click
        Try
            Dim ReservationId As Integer = Convert.ToInt32(TextBoxReservationID.Text)
            Dim clientId As Integer = Convert.ToInt32(TextBoxClientID.Text)
            Dim roomNumber As Integer = Convert.ToInt32(DataGridView1.CurrentRowCells(1).Value.ToString())
            Dim dateIn As Date = DateTimePickerIN.Value
            Dim dateOut As Date = DateTimePickerOUT.Value
            If DateTime.Compare(dateIn.Date, DateTime.Now.Date) < 0 Then

                MessageBox.Show("The Date In Must be = Or > to Today Date", "Invalid Date IN", MessageBoxButtons.OK, MessageBoxIcon.Error)

            ElseIf DateTime.Compare(dateOut.Date, dateIn.Date) < 0 Then

                MessageBox.Show("The Date Out Must be = Or > to The Date In", "Invalid Date OUT", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Else
                If reservation.editReservation(roomNumber, clientId, dateIn, dateOut) Then

                    MessageBox.Show("Resservation Updated Successfully", "Edit Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    DateGridView1.DataSource = reservation.getAllReservations()



                Else
                    MessageBox.Show("Reservation Not Updated", "Edit Reservation", MessageBoxButtons.OK, MessageBoxIcon.Error)

                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Edit Reservation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)


        End Try

    End Sub

    Private Sub ButtonRemove_Click(Sender As Object, e As EventArgs) Handles ButtonRemove.Click

        Try
            Dim reservationId As Integer = Convert.ToInt32(TextBoxReservationID.Text)
            Dim roomNumber As Integer = Convert.ToInt32(DataGridView1.CurrentRowCells(2).Value.ToString())


            If reservation.removeReservation(reservationId, roomNumber) Then

                MessageBox.Show("Resservation Deleted Successfully", "Remove Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information)
                DateGridView1.DataSource = reservation.getAllReservations()



            Else
                MessageBox.Show("Reservation Not Deleted", "Remove Reservation", MessageBoxButtons.OK, MessageBoxIcon.Error)

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Remove Reservation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try


    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        'display datagridview selected row data into the input fields
        TextBoxReservation.ID.Text = DataGridView1.CurrentRow.Cells(0).Value.ToString()
        TextBoxClientID.Text = DataGridView1.CurrentRow.Cells(1).Value.ToString()
        'To display the selected room number we need first to select the room type
        Dim roomNumber As Integer = Convert.ToInt32(DataGridView1.CurrentRow.Cells(2).Value.ToString())

        ComboBoxType.SelectedValue = room.getRoomType(roomNumber)
        ComboBoxRoomNumber.SelectedValue = DataGridView1.CurrentRow.Cells(2).Value.ToString()
        DataTimePickerIN.Value = DataGridView1.CurrentRow.Cells(3).Value
        DataTimePickerOUT.Value = DataGridView1.CurrentRow.Cells(4).Value

    End Sub
End Class

'we need to display room numbers depending on the selected type
'we have to add foreign keys for the table rooms(rooms and rooms_type)
'->ALTER TABLE rooms ADD CONSTRAINT fk_type_id FOREIGN KEY(type)REFERENCES rooms_type(id )on DELETE CASCADE on UPDATE CASCADE

'we have to add foreign keys for the table reservations(reservations and rooms)
'->ALTER TABLE reservations ADD CONSTRAINT fk_room_number FOREIGN KEY (room_Number)REFERENCES rooms(number)on DELETE CASCADE on UPDATE CASCADE
'we have to add foreign keys for the table reservations(reservations and clients)
'->ALTER TABLE reservations ADD CONSTRAINT fk_client_id FOREIGN KEY(client_ID)REFERENCES clients(id) on DELETE CACADE and UPDATE CASCADE


'some fixes we need to do
'only display the not reserved room in the combobox

'fix the date in and date out[Done]
'the date in must be =or > today date[Done]
'the date out must be =  or > date in[Done]

'when we add a reservation we need to set this room to reserved[Done]
'when we remove a reservation we need to set this room to not reserved[Done]

'the error come from the room number[Fixed]
'because the room will not show up in the combobox(the room is already reserved and we only show the not reserved one)
'to fix date we will get the room number from the datagridview


