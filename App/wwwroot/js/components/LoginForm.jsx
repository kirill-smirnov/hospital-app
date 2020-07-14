import React from 'react';
import { withStore } from '@spyna/react-store';
import authService from '../services/auth.js';

class LoginForm extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      username: '',
      password: '',
    };
  }

  handleChange(e) {
    const target = e.target;
    const value = target.value;
    const name = target.name;

    this.setState({
      [name]: value
    });
  }

  handleSubmit(e) {
    e.preventDefault();
    const {username, password} = this.state;
    authService.loginAsync({
      username: username,
      password:password,
      role: 'Doctor' })
      .then(user => this.props.store.set('user', user))

    this.setState({
      username: '',
      password: '',
    });
  }

  render() {
    return (
      <form onSubmit={this.handleSubmit.bind(this)}>
        <div className="form-group">
          <label htmlFor="username">Your username:</label>
          <input 
            className="form-control"
            type="text"
            value={this.state.username}
            name="username"
            onChange={this.handleChange.bind(this)} />
        </div>
        <div className="form-group">
          <label htmlFor="password">Your password:</label>
          <input 
            className="form-control"
            type="password"
            value={this.state.password}
            name="password"
            onChange={this.handleChange.bind(this)} />
        </div>
        <input 
          className="btn btn-primary"
          type="submit" 
          value="Send" />
      </form>
    )
  }
}

export default withStore(LoginForm);