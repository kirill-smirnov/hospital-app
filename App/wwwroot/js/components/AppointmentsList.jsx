import React from 'react';

import { appointmentsFetchAsync } from '../services/dataFetch';

import Appointment from './Appointment.jsx';

class AppointmentsList extends React.Component {
  constructor(props) {
    super(props);
    this.state = {};
  }

  async componentDidMount() {
    await appointmentsFetchAsync()
      .then((data) => {
        this.setState({ appointments: data });
      })
  }

  render() {
    let {appointments} = this.state;

    if (appointments)
      return appointments.map((appt, index) => {
        return <Appointment key={index} data={appt} />;
      });

    return (
        <p>Empty</p>
      );
  }
}

export default AppointmentsList;