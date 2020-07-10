import React from 'react';

import 'devextreme/dist/css/dx.common.css';
import 'devextreme/dist/css/dx.light.css';
import Scheduler from 'devextreme-react/Scheduler';

import { appointmentsFetchAsync } from '../services/dataFetch';

class AppointmentsScheduler extends React.Component {
  constructor(props) {
    super(props);
    this.state = {}
  }

  async componentDidMount() {
    await appointmentsFetchAsync()
      .then((data) => {
        this.setState({ appointments: data });
      })
  }

  render() {
    return (
      <Scheduler
        dataSource={this.state.appointments}
        textExpr="id"
        startDateExpr="start"
        endDateExpr="end" />
  );
  }
}

export default AppointmentsScheduler;