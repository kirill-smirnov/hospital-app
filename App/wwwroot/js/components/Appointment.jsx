import React from 'react';

class Appointment extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      data: props.data
    }
  }

  render() {
    let data = this.state.data;
    return (
      <div>
        <p> Id: {data.id} </p>
        <p> Patient id : {data.patientId} </p>
        <p> Doctor id: {data.doctorId} </p>
        <p> Start time: {data.start} </p>
        <p> End time: {data.end} </p>
        <br />
      </div>
    )
  }
}

export default Appointment;