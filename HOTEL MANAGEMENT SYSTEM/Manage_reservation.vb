Private Sub ButtonAdd_Click(sender As Object, e As EventArgs) Handles ButtonAdd.Click

    Try

        Dim clientId As Integer = Convert.ToInt32(TextBoxClientID.Text)
        Dim roomNumber As Integer = Convert.ToInt32(ComboBoxRoomNumber.SelectedValue.ToString())
        Dim dateIn As Date = DateTimePickerIN.Value
        Dim dateOut As Date = DateTimePickerOUT.Value

        If DateTime.Compare(dateIn.Date, DateTime.Now.Date) < 0 Then

            MessageBox.Show("The Date In Must be = Or > to Today Date", "Invalid Date IN", MessageBoxButtons.OK, MessageBoxIcon.Error)

        ElseIf DateTime.Compare(dateOut.Date, dateIn.Date) < 0 Then

            MessageBox.Show("The Date Out Must be = Or > to The Date In", "Invalid Date OUT", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Else

            If reservation.addReservation(roomNumber, clientId, dateIn, dateOut) Then

                MessageBox.Show("Reservation Added Successfully", "Add Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information)
                DataGridView1.DataSource = reservation.getAllReservations()
                ' we need to refresh the combobox to show only the not reserved rooms
                ComboBoxType.DataSource = room.getAllRoomsType()

            Else

                MessageBox.Show("Reservation NOT Added", "Add Reservation", MessageBoxButtons.OK, MessageBoxIcon.Error)

            End If

        End If


    Catch ex As Exception

        MessageBox.Show(ex.Message, "Add Reservation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

    End Try

End Sub

REM- Edit The Selected Reservation Button

Private Sub ButtonEdit_Click(sender As Object, e As EventArgs) Handles ButtonEdit.Click

    Try

        Dim reservationId As Integer = Convert.ToInt32(TextBoxReservationID.Text)
        Dim clientId As Integer = Convert.ToInt32(TextBoxClientID.Text)
        Dim roomNumber As Integer = Convert.ToInt32(DataGridView1.CurrentRow.Cells(2).Value.ToString())
        Dim dateIn As Date = DateTimePickerIN.Value
        Dim dateOut As Date = DateTimePickerOUT.Value

        If DateTime.Compare(dateIn.Date, DateTime.Now.Date) < 0 Then

            MessageBox.Show("The Date In Must be = Or > to Today Date", "Invalid Date IN", MessageBoxButtons.OK, MessageBoxIcon.Error)

        ElseIf DateTime.Compare(dateOut.Date, dateIn.Date) < 0 Then

            MessageBox.Show("The Date Out Must be = Or > to The Date In", "Invalid Date OUT", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Else

            If reservation.editReservation(reservationId, roomNumber, clientId, dateIn, dateOut) Then

                MessageBox.Show("Reservation Updated Successfully", "Edit Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information)
                DataGridView1.DataSource = reservation.getAllReservations()

            Else

                MessageBox.Show("Reservation NOT Updated", "Edit Reservation", MessageBoxButtons.OK, MessageBoxIcon.Error)

            End If

        End If

    Catch ex As Exception

        MessageBox.Show(ex.Message, "Edit Reservation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

    End Try

End Sub

REM- Remove The Selected Reservation Button

Private Sub ButtonRemove_Click(sender As Object, e As EventArgs) Handles ButtonRemove.Click

    Try

        Dim reservationId As Integer = Convert.ToInt32(TextBoxReservationID.Text)
        Dim roomNumber As Integer = Convert.ToInt32(DataGridView1.CurrentRow.Cells(2).Value.ToString())

        If reservation.removeReservation(reservationId, roomNumber) Then

            MessageBox.Show("Reservation Deleted Successfully", "Remove Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information)
            DataGridView1.DataSource = reservation.getAllReservations()

        Else

            MessageBox.Show("Reservation NOT Deleted", "Remove Reservation", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End If

    Catch ex As Exception

        MessageBox.Show(ex.Message, "Remove Reservation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

    End Try

End Sub
