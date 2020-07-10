import React from 'react';

import AppointmentsList from './AppointmentsList.jsx';
import AppointmentsScheduler from './AppointmentsScheduler.jsx';

class Main extends React.Component {
  
  render() {
    return (
      <div>
        <AppointmentsScheduler />
        <AppointmentsList />
      </div>
    );
  }
}

export default Main;