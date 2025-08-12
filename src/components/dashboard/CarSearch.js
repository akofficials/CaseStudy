import React, { useState } from "react";
import "./carsearch.css";

const CarSearch = () => {
  const [model, setModel] = useState("");
  const [pickupLocation, setPickupLocation] = useState("");
  const [pickupDate, setPickupDate] = useState("");
  const [dropoffDate, setDropoffDate] = useState("");
  const [results, setResults] = useState([]);
  const [loading, setLoading] = useState(false);

  const handleSearch = async () => {
    if (!pickupDate || !dropoffDate || !pickupLocation) {
      alert("Please enter pickup/drop-off dates and pickup location.");
      return;
    }

    const url = `https://localhost:7294/api/Vehicle/search?model=${encodeURIComponent(
      model
    )}&location=${encodeURIComponent(
      pickupLocation
    )}&pickup=${pickupDate}&dropoff=${dropoffDate}`;

    try {
      setLoading(true);
      const response = await fetch(url);

      if (!response.ok) {
        const error = await response.text();
        alert("Search failed: " + error);
        return;
      }

      const data = await response.json();
      setResults(data);

      if (data.length === 0) {
        alert("No vehicles found for the selected filters.");
      }
    } catch (err) {
      console.error("Search error:", err);
      alert("An error occurred while searching.");
    } finally {
      setLoading(false);
    }
  };

  const handleReserve = (car) => {
    localStorage.setItem("selectedCar", JSON.stringify(car));
    localStorage.setItem("pickupDate", pickupDate);
    localStorage.setItem("dropoffDate", dropoffDate); // ✅ corrected here
    window.location.href = "/booking";
  };

  return (
    <div className="car-search-container">
      <div className="search-form">
        <select value={model} onChange={(e) => setModel(e.target.value)}>
          <option value="">Select Model</option>
          <option value="Camry">Camry</option>
          <option value="Civic">Civic</option>
          <option value="Fusion">Fusion</option>
          <option value="Elantra">Elantra</option>
          <option value="Malibu">Malibu</option>
        </select>

        <select
          value={pickupLocation}
          onChange={(e) => setPickupLocation(e.target.value)}
        >
          <option value="">Pickup Location</option>
          <option value="Chennai">Chennai</option>
          <option value="Coimbatore">Coimbatore</option>
          <option value="Tiruchirappalli">Tiruchirappalli</option>
          <option value="Madurai">Madurai</option>
          <option value="Kanniyakumari">Kanniyakumari</option>
          <option value="Vellore">Vellore</option>
          <option value="Salem">Salem</option>
        </select>

        <input
          type="date"
          value={pickupDate}
          onChange={(e) => setPickupDate(e.target.value)}
        />

        <input
          type="date"
          value={dropoffDate}
          onChange={(e) => setDropoffDate(e.target.value)}
        />

        <button onClick={handleSearch} disabled={loading}>
          {loading ? "Searching..." : "Search"}
        </button>
      </div>

      <div className="car-results">
        {results.map((car) => (
          <div key={car.vehicleId} className="car-card">
            <img src={car.imageUrl} alt={car.model} />
            <h3>
              {car.make} {car.model}
            </h3>
            <p>
              {car.year} | {car.location}
            </p>
            <p>₹{car.pricePerDay} / day</p>
            <button onClick={() => handleReserve(car)}>Reserve</button>
          </div>
        ))}
      </div>
    </div>
  );
};

export default CarSearch;
