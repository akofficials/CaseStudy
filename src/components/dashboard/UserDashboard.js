import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import CarSearch from "./CarSearch";
import UserProfile from "./UserProfile";
import Bookings from "./Bookings";
import Reviews from "./Reviews";
import "./dashboard.css";

const UserDashboard = () => {
  const [activeTab, setActiveTab] = useState("carSearch");
  const navigate = useNavigate();

  const handleLogout = () => {
    navigate("/");
  };

  const renderTab = () => {
    switch (activeTab) {
      case "carSearch":
        return <CarSearch />;
      case "profile":
        return <UserProfile />;
      case "bookings":
        return <Bookings />;
      case "reviews":
        return <Reviews />;
      default:
        return null;
    }
  };

  return (
    <div className="dashboard-container">
      <div className="sidebar">
        <div>
          <h2>User Dashboard</h2>
          <div className="nav-buttons">
            <button
              className="nav-button"
              onClick={() => setActiveTab("carSearch")}
            >
              Search & Reserve
            </button>
            <button
              className="nav-button"
              onClick={() => setActiveTab("profile")}
            >
              My Profile
            </button>
            <button
              className="nav-button"
              onClick={() => setActiveTab("bookings")}
            >
              Bookings
            </button>
            <button
              className="nav-button"
              onClick={() => setActiveTab("reviews")}
            >
              Reviews
            </button>
          </div>
        </div>
        <button className="nav-button" onClick={handleLogout}>
          Logout
        </button>
      </div>
      <div className="content">{renderTab()}</div>
    </div>
  );
};

export default UserDashboard;
