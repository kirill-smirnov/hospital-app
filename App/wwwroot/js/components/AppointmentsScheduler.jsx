import React from 'react';

import 'devextreme/dist/css/dx.common.css';
import 'devextreme/dist/css/dx.light.css';
import Scheduler from 'devextreme-react/Scheduler';

import DoctorSelect from './DoctorSelect.jsx';
import authService from '../services/auth.js';
import allowEditingAppointment from '../permissions.js';
import * as dataService from '../services/data';

function serialize(data, appointments) {
  return {
    id: data.id,
    start: data.start,
    end: data.end,
    commentary: data.description,
    patient: data.patient.id,
    doctor: data.doctor.id,
  }
}

const AppointmentTooltipRender = (props) => {
    console.log(props);
    return <div></div>
  }

class AppointmentsScheduler extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      doctors: [],
      selectedDoctor: ''
    };
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

  onSelectChange(e) {
    const doctorId = e.target.value;

    dataService.appointmentsFetchAsync(doctorId)
      .then((data) => {
        this.setState({ appointments: data, selectedDoctor: doctorId });
      })
  }

  onAppointmentEditing(e) {
    let data = serialize(e.appointmentData, this.state.appointments);
    const allowedToEdit = allowEditingAppointment(data);
    if (!allowedToEdit)
      e.cancel = true;

    return allowedToEdit;
  }

  onAppointmentFormOpening(e) {
    this.onAppointmentEditing(e);
  }

  onAppointmentDeleting(e) {
    //TODO: Notify
    console.log("Not allowed to delete");
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

  render() {
    return (
      <div>
        <DoctorSelect 
          value={this.state.selectedDoctor}
          data={this.state.doctors}
          onChange={this.onSelectChange.bind(this)} />
        <Scheduler
          dataSource={this.state.appointments}
          onAppointmentFormOpening={this.onAppointmentFormOpening.bind(this)}
          onAppointmentDeleting={this.onAppointmentDeleting.bind(this)}
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