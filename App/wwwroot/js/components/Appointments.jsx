import React from 'react';

import Appointment from './Appointment.jsx';

class Appointments extends React.Component {
  constructor(props) {
    super(props);
    this.state = {};
  }

  async componentDidMount() {
    await fetch('https://localhost:5001/api/appointments')
      .then(res => res.json())
      .then((data) => {
        this.setState({ appointments: data });
      })
      .catch(console.log);
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

export default Appointments;