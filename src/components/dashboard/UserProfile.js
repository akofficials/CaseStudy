import React, { useEffect, useState } from "react";
import "./userProfile.css";

const UserProfile = () => {
  const [user, setUser] = useState(null);
  const [isEditing, setIsEditing] = useState(false);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchProfile = async () => {
      const token = localStorage.getItem("token");
      if (!token) return;

      try {
        const res = await fetch("https://localhost:7294/api/User/me", {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        });

        if (!res.ok) {
          throw new Error("Failed to fetch user");
        }

        const data = await res.json();
        setUser(data);
      } catch (err) {
        console.error("Error loading user:", err);
      } finally {
        setLoading(false);
      }
    };

    fetchProfile();
  }, []);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setUser((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleSave = async () => {
    const token = localStorage.getItem("token");

    try {
      const res = await fetch("https://localhost:7294/api/User/update", {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify({
          firstName: user.firstName,
          lastName: user.lastName,
          phoneNumber: user.phoneNumber,
        }),
      });

      if (!res.ok) {
        const errText = await res.text();
        alert("Failed to update: " + errText);
        return;
      }

      alert("Profile updated successfully!");
      setIsEditing(false);
    } catch (err) {
      console.error("Update error:", err);
      alert("An error occurred while updating.");
    }
  };

  if (loading) return <p>Loading profile...</p>;
  if (!user) return <p>No user data found.</p>;

  return (
    <div className="profile-container">
      <h2>My Profile</h2>
      <div className="profile-section">
        <label>First Name</label>
        <input
          name="firstName"
          value={user.firstName}
          onChange={handleChange}
          disabled={!isEditing}
        />

        <label>Last Name</label>
        <input
          name="lastName"
          value={user.lastName}
          onChange={handleChange}
          disabled={!isEditing}
        />

        <label>Email</label>
        <input name="email" value={user.email} disabled />

        <label>Phone</label>
        <input
          name="phoneNumber"
          value={user.phoneNumber}
          onChange={handleChange}
          disabled={!isEditing}
        />
      </div>

      <div className="profile-buttons">
        {isEditing ? (
          <button onClick={handleSave} className="save-btn">
            Save
          </button>
        ) : (
          <button onClick={() => setIsEditing(true)} className="edit-btn">
            Edit
          </button>
        )}
      </div>
    </div>
  );
};

export default UserProfile;
