import React, { useState } from "react";
import "./dashboard.css";
import { useNavigate } from "react-router-dom";
import ManageUsers from "./ManageUsers";
import ManageCars from "./ManageCars";

// Placeholder components (replace with full modules if needed)

const Reservations = () => <p>Manage user reservations and availability.</p>;
const Analytics = () => (
  <p>View analytics reports on usage, revenue, and key metrics.</p>
);
const Promotions = () => <p>Configure discounts and promotional offers.</p>;
const Feedback = () => <p>Manage user feedback and dispute resolution.</p>;

const AdminDashboard = () => {
  const [activeTab, setActiveTab] = useState("users");
  const navigate = useNavigate();

  const handleLogout = () => {
    navigate("/");
  };

  const renderContent = () => {
    switch (activeTab) {
      case "users":
        return <ManageUsers />;
      case "cars":
        return <ManageCars />;
      case "reservations":
        return <Reservations />;
      case "analytics":
        return <Analytics />;
      case "promotions":
        return <Promotions />;
      case "feedback":
        return <Feedback />;
      default:
        return null;
    }
  };

  return (
    <div className="dashboard-container">
      <div className="sidebar">
        <div>
          <h2>Admin Panel</h2>
          <div className="nav-buttons">
            <button
              className="nav-button"
              onClick={() => setActiveTab("users")}
            >
              Manage Users
            </button>
            <button className="nav-button" onClick={() => setActiveTab("cars")}>
              Vehicle Fleet
            </button>
            <button
              className="nav-button"
              onClick={() => setActiveTab("reservations")}
            >
              Reservations
            </button>
            <button
              className="nav-button"
              onClick={() => setActiveTab("analytics")}
            >
              Reports & Analytics
            </button>
            <button
              className="nav-button"
              onClick={() => setActiveTab("promotions")}
            >
              Pricing & Promotions
            </button>
            <button
              className="nav-button"
              onClick={() => setActiveTab("feedback")}
            >
              Feedback & Disputes
            </button>
          </div>
        </div>
        <button className="nav-button" onClick={handleLogout}>
          Logout
        </button>
      </div>

      <div className="content">
        <h1>{activeTab.toUpperCase().replace(/_/g, " ")}</h1>
        {renderContent()}
      </div>
    </div>
  );
};

export default AdminDashboard;
