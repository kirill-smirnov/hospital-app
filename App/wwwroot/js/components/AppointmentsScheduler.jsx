import React from 'react';

import 'devextreme/dist/css/dx.common.css';
import 'devextreme/dist/css/dx.light.css';
import Scheduler from 'devextreme-react/Scheduler';

import DoctorSelect from './DoctorSelect.jsx';
import authService from '../services/auth.js';
import * as dataService from '../services/data';

function serialize(data, appointments) {
  const user = authService.getCurrentUser();
  return {
    id: data.id,
    start: data.start,
    end: data.end,
    commentary: data.description,
    patient: appointments.find(appt => appt.patient.name === data.patient.name).id,
    doctor: user.id
  }
}

class AppointmentsScheduler extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      doctors: [],
      selectedDoctor: '',
      permissions: {
        allowAdding: true,
        allowDeleting: true,
        allowResizing: true,
        allowDragging: true,
        allowUpdating: true
      }
    };
  }

  onSelectChange(e) {
    const doctorId = e.target.value;

    dataService.appointmentsFetchAsync(doctorId)
      .then((data) => {
        this.setState({ appointments: data, selectedDoctor: doctorId });
      })
      //.then(() => this.setState({ selectedDoctor: doctorId }));
  }

  onAppointmentAdded(e) {
    let data = serialize(e.appointmentData, this.state.appointments);
    dataService.appointmentCreateAsync(data);
  }

  onAppointmentUpdated(e) {
    let data = serialize(e.appointmentData, this.state.appointments);
    dataService.appointmentUpdateAsync(data);
  }

  onAppointmentDeleted(e) {
    let data = serialize(e.appointmentData, this.state.appointments);
    dataService.appointmentDeleteAsync(data);
  }

  async componentDidMount() {
    await dataService.appointmentsFetchAsync()
      .then((data) => {
        this.setState({ appointments: data });
      });

    await dataService.doctorsFetchAsync()
    .then(doctors => {
      this.setState({
        doctors
      });
    });
  }

  render() {
    return (
      <div>
        <DoctorSelect 
          value={this.state.selectedDoctor}
          data={this.state.doctors}
          onChange={this.onSelectChange.bind(this)} />
        <Scheduler
          dataSource={this.state.appointments}
          onAppointmentAdded={this.onAppointmentAdded.bind(this)}
          onAppointmentUpdated={this.onAppointmentUpdated.bind(this)}
          onAppointmentDeleted={this.onAppointmentDeleted.bind(this)}
          views={['day', 'workWeek', 'month']}
          defaultCurrentView="month"
          height={300}
          width={700}
          textExpr="patient.name"
          startDateExpr="start"
          endDateExpr="end" />
      </div>
    );
  }
}

export default AppointmentsScheduler;