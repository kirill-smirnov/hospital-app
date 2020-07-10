async function appointmentsFetchAsync() {
  return await fetch('api/appointments')
      .then(res => res.json())
      .catch(console.log);
}


export { appointmentsFetchAsync }