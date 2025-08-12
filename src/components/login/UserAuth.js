import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import "./AdminLogin.css";

const UserAuth = () => {
  const [isLogin, setIsLogin] = useState(true);
  const [isForgotPassword, setIsForgotPassword] = useState(false);
  const [form, setForm] = useState({
    firstName: "",
    lastName: "",
    email: "",
    password: "",
    phoneNumber: "",
  });

  const navigate = useNavigate();

  const toggleForm = () => {
    setIsLogin(!isLogin);
    setIsForgotPassword(false);
    setForm({
      firstName: "",
      lastName: "",
      email: "",
      password: "",
      phoneNumber: "",
    });
  };

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleLogin = async (e) => {
    e.preventDefault();

    try {
      const response = await fetch("https://localhost:7294/api/User/login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          email: form.email,
          password: form.password,
        }),
      });

      if (!response.ok) {
        const errorText = await response.text();
        alert(`Login failed: ${errorText}`);
        return;
      }

      const data = await response.json();
      localStorage.setItem("token", data.token);
      alert("Login successful!");
      navigate("/user-dashboard");
    } catch (err) {
      console.error("Login error:", err);
      alert("Login error. See console.");
    }
  };

  const handleSignUp = async (e) => {
    e.preventDefault();

    try {
      const response = await fetch("https://localhost:7294/api/User/register", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          firstName: form.firstName,
          lastName: form.lastName,
          email: form.email,
          password: form.password,
          phoneNumber: form.phoneNumber,
        }),
      });

      if (!response.ok) {
        const error = await response.text();
        alert(`Registration failed: ${error}`);
        return;
      }

      alert("Registration successful! Please log in.");
      setIsLogin(true);
    } catch (err) {
      console.error(err);
      alert("Error during registration. Check console.");
    }
  };

  const handleForgotPassword = (e) => {
    e.preventDefault();
    alert("If this email exists, a reset link will be sent.");
    setIsForgotPassword(false);
  };

  return (
    <div className="landing-container">
      <h1 className="title">
        {isForgotPassword
          ? "Reset Password"
          : isLogin
          ? "User Login"
          : "User Sign Up"}
      </h1>

      <form
        className="login-form"
        onSubmit={
          isForgotPassword
            ? handleForgotPassword
            : isLogin
            ? handleLogin
            : handleSignUp
        }
      >
        {!isLogin && !isForgotPassword && (
          <>
            <input
              className="input-field"
              type="text"
              name="firstName"
              placeholder="First Name"
              value={form.firstName}
              onChange={handleChange}
              required
            />
            <input
              className="input-field"
              type="text"
              name="lastName"
              placeholder="Last Name"
              value={form.lastName}
              onChange={handleChange}
              required
            />
            <input
              className="input-field"
              type="text"
              name="phoneNumber"
              placeholder="Phone Number"
              value={form.phoneNumber}
              onChange={handleChange}
              required
            />
          </>
        )}

        <input
          className="input-field"
          type="email"
          name="email"
          placeholder="Email"
          value={form.email}
          onChange={handleChange}
          required
        />

        {!isForgotPassword && (
          <input
            className="input-field"
            type="password"
            name="password"
            placeholder="Password"
            value={form.password}
            onChange={handleChange}
            required
          />
        )}

        <button className="btn user-btn" type="submit">
          {isForgotPassword ? "SEND RESET LINK" : isLogin ? "LOGIN" : "SIGN UP"}
        </button>

        {isLogin && !isForgotPassword && (
          <button
            type="button"
            className="btn admin-btn"
            onClick={() => setIsForgotPassword(true)}
          >
            Forgot Password?
          </button>
        )}

        {!isForgotPassword && (
          <button type="button" className="btn admin-btn" onClick={toggleForm}>
            {isLogin
              ? "Don't have an account? Sign Up"
              : "Already have an account? Login"}
          </button>
        )}

        {isForgotPassword && (
          <button
            type="button"
            className="btn admin-btn"
            onClick={() => setIsForgotPassword(false)}
          >
            Back to Login
          </button>
        )}
      </form>
    </div>
  );
};

export default UserAuth;
