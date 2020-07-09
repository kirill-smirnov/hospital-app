import React from 'react';

import Appointment from './Appointment.jsx';

class Appointments extends React.Component {
  constructor(props) {
    super(props);
    this.state = {};
  }

  componentDidMount() {
    fetch('https://localhost:5001/api/appointments')
      .then(res => res.json())
      .then((data) => {
        this.setState({ appointments: data });
      })
      .catch(console.log);
  }

  render() {
    let appts = this.state.appointments;

    if (appts)
      return appts.map((appt, index) => {
        return <Appointment key={index} data={appt} />;
      });
    else
      return (
        <p>Empty</p>
      );
  }
}

export default Appointments;