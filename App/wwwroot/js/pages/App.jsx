import React from 'react';

import Header from '../components/Header.jsx';
import Main from '../components/Main.jsx';
import Footer from '../components/Footer.jsx';

import {BrowserRouter} from 'react-router-dom';

class App extends React.Component {
  render() {
    return (
      <BrowserRouter>
        <Header />
        <Main />
        <Footer />
      </BrowserRouter>
    );
  }
}

export default App;