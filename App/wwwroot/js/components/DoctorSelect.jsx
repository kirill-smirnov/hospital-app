import React from 'react';

const DoctorSelect = (props) => {
  const {value, onChange, data} = props;
  return (
    <select value={value} onChange={onChange}>
      <option value={''}>All</option>
      {data.map((doc, index) => 
        <option key={index} value={doc.id}> {doc.name} </option>
      )}
   </select>
  )
}

export default DoctorSelect;