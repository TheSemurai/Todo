import React, { useState } from "react";
import NavTodoBar from "../../components/nav-bar/nav-todo-bar";
import { useNavigate } from "react-router-dom";
import AuthService from "../../services/auth.service";

export default function LoginPage() {
  const authService = new AuthService();
  const navigate = useNavigate();
  const [loginData, setLoginData] = useState({
    email: "",
    password: "",
  });

  function handleEmailChange(event) {
    setLoginData((login) => ({ ...login, email: event.target.value }));
  }

  function handlePasswordChange(event) {
    setLoginData((login) => ({ ...login, password: event.target.value }));
  }

  function login() {
    const request = authService.login(loginData);

    request
      .then(() => {
        navigate("/");
      })
      .catch((error) => console.log(error));
  }

  function singup() {
    navigate("/singup");
  }

  return (
    <>
      <NavTodoBar />
      <div className="login">
        <div className="login-card">
          <h2>Login</h2>
          <div className="login-data">
            <input
              className="inpt-text"
              type="text"
              value={loginData.email}
              onChange={handleEmailChange}
              placeholder="Enter email.... example: email@email.com"
              maxLength={256}
            />
            <input
              className="inpt-text"
              type="password"
              value={loginData.password}
              onChange={handlePasswordChange}
              placeholder="Enter password...."
            />
          </div>
          <div className="login-button">
            <button className="btn btn-blue btn-login ml-20px" onClick={login}>
              Login
            </button>
            <button
              className="btn btn-gray btn-singUp  ml-20px"
              onClick={singup}
            >
              Sing up
            </button>
          </div>
        </div>
      </div>
    </>
  );
}
