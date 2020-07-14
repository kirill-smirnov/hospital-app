const urlBase = "https://localhost:5001/api/"

const apiFetch = (url, ...params) => fetch(urlBase + url, ...params);

export default apiFetch;