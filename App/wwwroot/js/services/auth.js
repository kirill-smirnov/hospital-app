import apiFetch from './fetch.js';

class AuthService {
  constructor() {
    this.user = null;
  }

  async loginAsync(data) {
    const that = this;
    return await apiFetch("token", {
      method:"POST",
      body: JSON.stringify(data),
      headers: {
        "Accept": "application/json",
        "Content-Type": "application/json"
      }
    }).then(res => res.json())
      .then(data => {
        //TODO: add cookie
        if (data && data.access_token)
          that.user = data;
        return data;
      })
      .catch(console.log);
  }

  logout() {
    //TODO: remove cookie
  }

  getCurrentUser() {
    //TODO: cookie
    return this.user;
  }
}

const authService = new AuthService();
export default authService;