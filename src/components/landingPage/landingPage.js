import React, { Component } from "react";
import { useNavigate } from "react-router-dom";
import "./LandingPage.css";

const LandingPage = () => {
  const navigate = useNavigate();

  const handleUserClick = () => {
    navigate("/user-auth");
  };

  const handleAdminClick = () => {
    navigate("/admin-login");
  };

  return (
    <div className="landing-container">
      <h1 className="title">CAR RENTAL SYSTEM</h1>
      <p className="subtitle">Choose your access level to continue</p>
      <div className="button-group">
        <button className="btn user-btn" onClick={handleUserClick}>
          USER
        </button>
        <button className="btn admin-btn" onClick={handleAdminClick}>
          ADMIN
        </button>
      </div>
    </div>
  );
};

export default LandingPage;
