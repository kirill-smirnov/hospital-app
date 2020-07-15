import React from 'react';
import { createStore } from '@spyna/react-store';

const AppWithStore = createStore(App, {});

import App from './pages/App.jsx';  

ReactDOM.render(
  <AppWithStore />,
  document.getElementById("content")
);