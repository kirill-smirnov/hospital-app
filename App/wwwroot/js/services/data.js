import apiFetch from './fetch.js';

export async function appointmentsFetchAsync(doctorId) {
  const url = doctorId ?
    `appointments?doctorId=${doctorId}`:
    "appointments"

  return await apiFetch(url)
      .then(res => res.json())
      .catch(console.log);
}

export async function appointmentCreateAsync(appointment) {
  return await apiFetch("appointments", {
    method:"POST",
      body: JSON.stringify(appointment)
  }).then(res => res.json())
    .catch(console.log);
}

export async function appointmentUpdateAsync(appointment) {
  return await apiFetch("appointments/"+appointment.id, {
    method:"PUT",
      body: JSON.stringify(appointment)
  }).then(res => res.json())
    .catch(console.log);
}

export async function appointmentDeleteAsync(appointment) {
  return await apiFetch("appointments/"+appointment.id, {
    method:"DELETE"
  }).then(res => res.json())
    .catch(console.log);
}

export async function doctorsFetchAsync() {
  return await apiFetch("doctors")
      .then(res => res.json())
      .catch(console.log);
}