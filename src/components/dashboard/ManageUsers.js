import React, { useEffect, useState } from "react";
import "./ManageUsers.css";

const ManageUsers = () => {
  const [users, setUsers] = useState([]);
  const [loading, setLoading] = useState(true);

  const token = localStorage.getItem("token"); // Ensure admin token is stored here

  useEffect(() => {
    const fetchUsers = async () => {
      try {
        const res = await fetch("https://localhost:7294/api/Admin/users", {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        });

        if (!res.ok) throw new Error("Failed to load users");

        const data = await res.json();
        setUsers(data);
      } catch (err) {
        console.error("Error loading users:", err);
      } finally {
        setLoading(false);
      }
    };

    fetchUsers();
  }, [token]);

  const toggleStatus = async (userId, currentStatus) => {
    const newStatus = currentStatus === "active" ? "inactive" : "active";

    try {
      const res = await fetch(
        `https://localhost:7294/api/Admin/users/${userId}/status`,
        {
          method: "PUT",
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
          },
          body: JSON.stringify({ status: newStatus }),
        }
      );

      if (!res.ok) throw new Error("Failed to update status");

      // Update UI after successful response
      setUsers((prev) =>
        prev.map((user) =>
          user.userId === userId ? { ...user, status: newStatus } : user
        )
      );
    } catch (err) {
      console.error("Status toggle failed:", err);
      alert("Failed to update user status.");
    }
  };

  if (loading) return <p>Loading users...</p>;

  return (
    <div className="manage-users-container">
      <h2>Manage User Accounts</h2>
      <table className="admin-table">
        <thead>
          <tr>
            <th>Name</th>
            <th>Email</th>
            <th>Status</th>
            <th>Action</th>
          </tr>
        </thead>
        <tbody>
          {users.map((u) => (
            <tr key={u.userId}>
              <td>
                {u.firstName} {u.lastName}
              </td>
              <td>{u.email}</td>
              <td
                className={
                  u.status === "active" ? "status-active" : "status-inactive"
                }
              >
                {u.status}
              </td>
              <td>
                <button
                  className="action-button"
                  onClick={() => toggleStatus(u.userId, u.status)}
                >
                  {u.status === "active" ? "Deactivate" : "Activate"}
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default ManageUsers;
