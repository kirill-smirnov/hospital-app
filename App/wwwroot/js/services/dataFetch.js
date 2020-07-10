async function appointmentsFetchAsync() {
  return await fetch('https://localhost:5001/api/appointments')
      .then(res => res.json())
      .catch(console.log);
}


export { appointmentsFetchAsync }