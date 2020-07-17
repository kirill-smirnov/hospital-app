import authService from './services/auth.js';

export default function allowEditingAppointment(appointment) {
  const user = authService.getCurrentUser();
  switch(user.role) {
    case "Patient":
      return user.id === appointment.patient;
    case "ReceptionStaff":
      return true;
    case "Doctor":
      return user.id === appointment.doctor;
  }
}