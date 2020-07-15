import apiFetch from './fetch.js';

async function appointmentsFetchAsync(doctorId) {
  const url = doctorId ?
    `appointments?doctorId=${doctorId}`:
    "appointments"

  return await apiFetch(url)
      .then(res => res.json())
      .catch(console.log);
}

async function doctorsFetchAsync() {
  return await apiFetch("doctors")
      .then(res => res.json())
      .catch(console.log);
}

export { appointmentsFetchAsync, doctorsFetchAsync }