import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import AdminLogin from "./components/login/AdminLogin";
import LandingPage from "./components/landingPage/landingPage";
import UserAuth from "./components/login/UserAuth";
import AdminDashboard from "./components/dashboard/AdminDashboard";
import UserDashboard from "./components/dashboard/UserDashboard";
import Booking from "./components/dashboard/Bookings";

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<LandingPage />} />
        <Route path="/admin-login" element={<AdminLogin />} />
        <Route path="/user-auth" element={<UserAuth />} />
        <Route path="/admin-dashboard" element={<AdminDashboard />} />
        <Route path="/user-dashboard" element={<UserDashboard />} />
        <Route path="/booking" element={<Booking />} />
      </Routes>
    </Router>
  );
}

export default App;
