import React from 'react';

import 'devextreme/dist/css/dx.common.css';
import 'devextreme/dist/css/dx.light.css';
import Scheduler from 'devextreme-react/Scheduler';

import DoctorSelect from './DoctorSelect.jsx';
import { appointmentsFetchAsync, doctorsFetchAsync } from '../services/dataFetch';

class AppointmentsScheduler extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      doctors: [],
      selectedDoctor: ''
    };
  }

  onSelectChange(e) {
    const doctorId = e.target.value;

    appointmentsFetchAsync(doctorId)
      .then((data) => {
        this.setState({ appointments: data });
      })
      .then(() => this.setState({ selectedDoctor: doctorId }));
  }

  async componentDidMount() {
    await appointmentsFetchAsync()
      .then((data) => {
        this.setState({ appointments: data });
      });

    await doctorsFetchAsync()
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