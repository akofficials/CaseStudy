import React, { Component, useState } from "react";
import { useNavigate } from "react-router-dom";
import "./AdminLogin.css";

const AdminLogin = () => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const navigate = useNavigate();

  const handleLogin = (e) => {
    e.preventDefault();
    if (username === "admin" && password === "admin123") {
      navigate("/admin-dashboard");
    } else {
      alert("Invalid credentials!");
    }
  };

  return (
    <div className="landing-container">
      <h1 className="title">Admin Login</h1>
      <form className="login-form" onSubmit={handleLogin}>
        <input
          type="text"
          placeholder="Username"
          className="input-field"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
          required
        />
        <input
          type="password"
          placeholder="Password"
          className="input-field"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          required
        />
        <button className="btn user-btn" type="submit">
          LOGIN
        </button>
      </form>
    </div>
  );
};

export default AdminLogin;
