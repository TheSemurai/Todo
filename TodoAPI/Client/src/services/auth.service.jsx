import React from "react";
import HttpService from "./http.service";

class AuthService extends React.Component {
  constructor(mainUrl = "/User", loginUrl = "/LogIn", singupUrl = "/SingUp") {
    super();
    this.mainUrl = mainUrl;
    this.loginUrl = loginUrl;
    this.singupUrl = singupUrl;
    this.httpService = new HttpService();
  }

  setToken = (repsonse) =>
    localStorage.setItem("token", `Bearer ${repsonse.data.token}`);

  clearPersonalData = () => {
    localStorage.removeItem("token");
  };

  login(data) {
    return this.httpService
      .post(this.mainUrl + this.loginUrl, data)
      .then((repsonse) => this.setToken(repsonse));
  }

  singup(data) {
    return this.httpService
      .post(this.mainUrl + this.singupUrl, data)
      .then((repsonse) => this.setToken(repsonse));
  }

  logout() {
    this.clearPersonalData();
  }

  isUserLoggined() {
    return localStorage.getItem("token") === null;
  }
}

export default AuthService;
