import React from 'react';
import { withStore } from '@spyna/react-store';

import authService from '../services/auth.js';

class Header extends React.Component {
  constructor(props) {
    super(props);
  }

  render() {
    const user = this.props.store.get('user');
    if (user)
      return <span> Hello, {user.username} </span>
    else
      return <span>Log in, please</span>
  }
}

export default withStore(Header);