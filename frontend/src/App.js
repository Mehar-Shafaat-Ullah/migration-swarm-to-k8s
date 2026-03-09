import React, { useEffect, useState } from 'react';
import './App.css';

function App() {
  const [data, setData] = useState("Checking connection...");
  const [userName, setUserName] = useState(""); // State for input field
  const [loginStatus, setLoginStatus] = useState("Ready"); // State for DB result
  const BACKEND_URL = process.env.REACT_APP_API_URL || "http://dotnet-api:5000";

  // Keep this to check connection on page load
  useEffect(() => {
    fetch('/api/status')
      .then(response => response.json())
      .then(json => setData(json.message))
      .catch(err => setData("Backend not connected yet."));
  }, []);

  // New function to Save to SQL Server
  const handleLogin = () => {
    if (!userName) return;

    fetch('/api/status/login', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(userName) // Sends the name string as JSON
    })
    .then(res => res.json())
    .then(json => {
      setLoginStatus(json.message);
      setUserName(""); // Clear input on success
    })
    .catch(err => setLoginStatus("Failed to save to DB."));
  };

  return (
    <div className="App">
      <header className="App-header">
        <h1>SwarmSync Project</h1>
        
        {/* Connection Status Section */}
        <div style={{ marginBottom: '20px', padding: '10px', border: '1px solid white' }}>
          <p>System Status: <strong>{data}</strong></p>
        </div>

        {/* Database Login Section */}
        <div style={{ padding: '20px', border: '2px solid #61dafb', borderRadius: '10px' }}>
          <h3>SQL Server Login</h3>
          <input 
            type="text" 
            placeholder="Enter your name" 
            value={userName} 
            onChange={(e) => setUserName(e.target.value)} 
            style={{ padding: '10px', borderRadius: '5px', border: 'none' }}
          />
          <button 
            onClick={handleLogin} 
            style={{ padding: '10px 20px', marginLeft: '10px', cursor: 'pointer', backgroundColor: '#61dafb', border: 'none', borderRadius: '5px' }}
          >
            Save to DB
          </button>
          <p style={{ fontSize: '0.9rem', marginTop: '10px' }}>Result: {loginStatus}</p>
        </div>
      </header>
    </div>
  );
}

export default App;
