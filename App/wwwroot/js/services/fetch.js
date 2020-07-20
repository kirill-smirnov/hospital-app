import authService from './auth.js';

const urlBase = "https://localhost:5001/api/"

function getDefaultHeaders() {
  const token = authService.getCurrentUser() ?
    authService.getToken() : "";

  return {
    "Accept": "application/json",
    "Content-Type": "application/json",
    "Authorization": "Bearer " + token
  }
}

const apiFetch = (url, params = {}) => {
  params.headers = {...getDefaultHeaders(), ...params.headers};
  return fetch(urlBase + url, params);
}

export default apiFetch;