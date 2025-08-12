import React, { useEffect, useState } from "react";

const Bookings = () => {
  const [reservations, setReservations] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
    const fetchReservations = async () => {
      try {
        const token = localStorage.getItem("token");

        const response = await fetch("https://localhost:7294/api/Reservation", {
          headers: {
            "Content-Type": "application/json",
            Authorization: token ? `Bearer ${token}` : undefined,
          },
        });

        if (!response.ok) {
          const message = await response.text();
          throw new Error(message || "Failed to fetch reservations");
        }

        const data = await response.json();
        setReservations(data);
      } catch (err) {
        console.error("Error fetching reservations:", err);
        setError("Could not load reservations.");
      } finally {
        setLoading(false);
      }
    };

    fetchReservations();
  }, []);

  if (loading) return <p>Loading your reservations...</p>;
  if (error) return <p style={{ color: "red" }}>{error}</p>;
  if (reservations.length === 0) return <p>No reservations found.</p>;

  return (
    <div style={{ padding: "20px" }}>
      <h2>My Reservations</h2>
      {reservations.map((res) => (
        <div
          key={res.reservationId}
          style={{
            border: "1px solid #ccc",
            borderRadius: "8px",
            padding: "15px",
            marginBottom: "15px",
            backgroundColor: "#f9f9f9",
          }}
        >
          <p>
            <strong>Vehicle:</strong> {res.vehicleModel || "N/A"}
          </p>
          <p>
            <strong>Pickup Date:</strong>{" "}
            {res.pickupDate
              ? new Date(res.pickupDate).toLocaleDateString()
              : "N/A"}
          </p>
          <p>
            <strong>Drop-off Date:</strong>{" "}
            {res.dropOffDate
              ? new Date(res.dropOffDate).toLocaleDateString()
              : "N/A"}
          </p>
          <p>
            <strong>Total Price:</strong> ₹
            {res.totalPrice?.toFixed(2) || "0.00"}
          </p>
          <p>
            <strong>Status:</strong>{" "}
            {res.isCancelled ? (
              <span style={{ color: "red" }}>Cancelled</span>
            ) : (
              <span style={{ color: "green" }}>Confirmed</span>
            )}
          </p>
        </div>
      ))}
    </div>
  );
};

export default Bookings;
