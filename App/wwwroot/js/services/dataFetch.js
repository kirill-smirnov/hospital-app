async function appointmentsFetchAsync(doctorId) {
  const url = doctorId ?
    `api/appointments?doctorId=${doctorId}`:
    'api/appointments'

  return await fetch(url)
      .then(res => res.json())
      .catch(console.log);
}

async function doctorsFetchAsync() {
  return await fetch('api/doctors')
      .then(res => res.json())
      .catch(console.log);
}

export { appointmentsFetchAsync, doctorsFetchAsync }