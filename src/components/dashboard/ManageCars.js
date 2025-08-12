import React, { useState } from "react";
import "./ManageCars.css"; // Make sure this file exists and is linked

const ManageCars = () => {
  const [cars, setCars] = useState([
    {
      id: 1,
      make: "Toyota",
      model: "Camry",
      year: 2020,
      price: 45,
      available: true,
    },
    {
      id: 2,
      make: "Honda",
      model: "Civic",
      year: 2019,
      price: 40,
      available: false,
    },
  ]);

  const toggleAvailability = (id) => {
    setCars((prev) =>
      prev.map((car) =>
        car.id === id ? { ...car, available: !car.available } : car
      )
    );
  };

  const handleDelete = (id) => {
    const confirmDelete = window.confirm(
      "Are you sure you want to delete this car?"
    );
    if (confirmDelete) {
      setCars((prev) => prev.filter((car) => car.id !== id));
    }
  };

  const handleEdit = (id) => {
    alert(`Edit functionality coming soon for car ID: ${id}`);
  };

  const handleAddCar = () => {
    alert("Add Car functionality coming soon.");
  };

  return (
    <div className="manage-cars-container">
      <h2>Manage Cars</h2>
      <button className="add-car-button" onClick={handleAddCar}>
        Add New Car
      </button>
      <table className="car-table">
        <thead>
          <tr>
            <th>Make</th>
            <th>Model</th>
            <th>Year</th>
            <th>Price ($/day)</th>
            <th>Availability</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {cars.map((car) => (
            <tr key={car.id}>
              <td>{car.make}</td>
              <td>{car.model}</td>
              <td>{car.year}</td>
              <td>{car.price}</td>
              <td>
                <span
                  className={
                    car.available ? "status-active" : "status-inactive"
                  }
                >
                  {car.available ? "Available" : "Unavailable"}
                </span>
              </td>
              <td>
                <div className="action-buttons">
                  <button
                    className="action-button edit-button"
                    onClick={() => handleEdit(car.id)}
                  >
                    Edit
                  </button>
                  <button
                    className="action-button delete-button"
                    onClick={() => handleDelete(car.id)}
                  >
                    Delete
                  </button>
                  <button
                    className="action-button toggle-button"
                    onClick={() => toggleAvailability(car.id)}
                  >
                    {car.available ? "Deactivate" : "Activate"}
                  </button>
                </div>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default ManageCars;
