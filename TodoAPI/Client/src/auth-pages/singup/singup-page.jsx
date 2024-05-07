import React, { useState } from "react";
import NavTodoBar from "../../components/nav-bar/nav-todo-bar";
import { useNavigate } from "react-router-dom";
import AuthService from "../../services/auth.service";

export default function SingUpPage() {
  const authService = new AuthService();
  const navigate = useNavigate();
  const [data, setData] = useState({
    email: "",
    password: "",
    username: "",
    gender: 0,
    name: "",
    lastname: "",
  });

  const Gender = {
    Male: 0,
    Female: 1,
    Other: 2,
  };

  function handleEmailChange(event) {
    setData((login) => ({ ...login, email: event.target.value }));
  }

  function handlePasswordChange(event) {
    setData((login) => ({ ...login, password: event.target.value }));
  }

  function handleUsernameChange(event) {
    setData((login) => ({ ...login, username: event.target.value }));
  }

  function handleNameChange(event) {
    setData((login) => ({ ...login, name: event.target.value }));
  }

  function handleLastnameChange(event) {
    setData((login) => ({ ...login, lastname: event.target.value }));
  }

  function handleGenderChange(event) {
    setData((login) => ({ ...login, gender: parseInt(event.target.value) }));
  }

  function backToLogin() {
    navigate(-1);
  }

  function singup() {
    const request = authService.singup(data);

    request
      .then(() => {
        navigate("/");
      })
      .catch((error) => console.log(error));
  }

  return (
    <>
      <NavTodoBar />
      <div className="singup-page">
        <div className="singup-card">
          <h2>SingUp</h2>
          <div className="singup-fields">
            <div className="singup-field">
              <p>email:</p>
              <input
                className="inpt-text"
                type="text"
                value={data.email}
                onChange={handleEmailChange}
                placeholder="Enter email.... example: email@email.com"
                maxLength={256}
              />
            </div>
            <div className="singup-field">
              <p>password:</p>
              <input
                className="inpt-text"
                type="text"
                value={data.password}
                onChange={handlePasswordChange}
                placeholder="Enter password...."
              />
            </div>
            <div className="singup-field">
              <p>username:</p>
              <input
                className="inpt-text"
                type="text"
                value={data.username}
                onChange={handleUsernameChange}
                placeholder="Enter username..."
                maxLength={256}
              />
            </div>
            <div className="singup-field">
              <p>choose gender:</p>
              <select
                className="gender-selection"
                value={data.gender}
                onChange={handleGenderChange}
              >
                <option value={Gender.Male}>Male</option>
                <option value={Gender.Female}>Female</option>
                <option value={Gender.Other}>Other</option>
              </select>
            </div>
            <div className="singup-field">
              <p>name:</p>
              <input
                className="inpt-text"
                type="text"
                value={data.name}
                onChange={handleNameChange}
                placeholder="Enter name..."
                maxLength={25}
              />
            </div>
            <div className="singup-field">
              <p>lastname:</p>
              <input
                className="inpt-text"
                type="text"
                value={data.lastname}
                onChange={handleLastnameChange}
                placeholder="Enter lastname...."
                maxLength={25}
              />
            </div>
          </div>
          <div className="singup-buttons">
            <button className="btn btn-green btn-main ml-20px" onClick={singup}>
              Sing-up
            </button>
            <button
              className="btn btn-blue btn-second btn-back-to-login ml-20px"
              onClick={backToLogin}
            >
              back to login
            </button>
          </div>
        </div>
      </div>
    </>
  );
}
