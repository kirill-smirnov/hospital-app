import React from 'react';
import {Route} from 'react-router-dom';
import { withStore } from '@spyna/react-store';


// import PrivateRoute from './PrivateRoute.jsx';
import AppointmentsScheduler from './AppointmentsScheduler.jsx';
import LoginForm from './LoginForm.jsx';


const Main = props => {
  const user = props.store.get('user');
  return (
    <div className="container-md">
      {user ? <AppointmentsScheduler /> : <LoginForm /> }
    </div>
  );
}

export default withStore(Main);