import React, { useState, useEffect } from 'react';
import './Contact.css'; // Ensure this is correctly pointing to your CSS file
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import Box from '@material-ui/core/Box';
const ContactComponent = () => {
    const linkStyle = {
        color: 'inherit',
        textDecoration: 'none',
        padding: '8px 16px',
        display: 'inline-block',
    };
    const [locations, setLocations] = useState([]);
    const [locationDetail, setLocationDetail] = useState(null);

    useEffect(() => {
        fetch('http://localhost:5200/api/Locations')
            .then(response => response.json())
            .then(data => setLocations(data))
            .catch(error => console.error('Failed to fetch locations:', error));
    }, []);

    const fetchLocationDetail = (locationId) => {
        fetch(`http://localhost:5200/api/Locations/${locationId}`)
            .then(response => response.json())
            .then(data => setLocationDetail(data))
            .catch(error => console.error(`Failed to fetch details for location ${locationId}:`, error));
    };
  
    return (
        <div className="contactContainer">
            <AppBar position="static">
                <Toolbar>

                    <Box className="container">
                        <Box className="lögo">
                            <img src="1cycle.png" width="120px" height="120px" />
                        </Box>
                        <Box className="nav">
                            <ul style={{ listStyleType: 'none', margin: 0, padding: 0, display: 'flex' }}>
                                <li><a href="/" style={linkStyle}>Home</a></li>
                                <li><a href="/feature" style={linkStyle}>What Clients Say</a></li>
                                <li><a href="/products" style={linkStyle}>Products</a></li>
                                <li><a href="/contact" style={linkStyle}>Visit us</a></li>
                            </ul>
                        </Box>
                    </Box>
                </Toolbar>
            </AppBar>
            <div className="locationsContainer">
                <section className="factory">
                    <div className="waviy">
                        <span style={{ '--i': 1 }}>F</span>
                        <span style={{ '--i': 2 }}>A</span>
                        <span style={{ '--i': 3 }}>C</span>
                        <span style={{ '--i': 4 }}>T</span>
                        <span style={{ '--i': 5 }}>O</span>
                        <span style={{ '--i': 6 }}>R</span>
                        <span style={{ '--i': 7 }}>Y</span>
                        <span style={{ '--i': 8 }}></span> 
                        <span style={{ '--i': 8 }}></span> 
                        <span style={{ '--i': 9 }}>O</span>
                        <span style={{ '--i': 10 }}>U</span>
                        <span style={{ '--i': 11 }}>T</span>
                        <span style={{ '--i': 12 }}>L</span>
                        <span style={{ '--i': 13 }}>E</span>
                        <span style={{ '--i': 14 }}>T</span>
                        <span style={{ '--i': 15 }}></span> 
                        <span style={{ '--i': 15 }}></span> 
                        <span style={{ '--i': 16 }}>S</span>
                        <span style={{ '--i': 17 }}>A</span>
                        <span style={{ '--i': 18 }}>L</span>
                        <span style={{ '--i': 19 }}>E</span>
                    </div>
                </section>
                <ul>
                    {locations.map(location => (
                        <li key={location.locationId} className="locationItem">
                            <span className="locationName">{location.name}</span>
                            <button onClick={() => fetchLocationDetail(location.locationId)}>View Details</button>
                        </li>
                    ))}
                </ul>
            </div>
            {locationDetail && (
                <div className="locationDetail">
                    <h3>Product Availability</h3>
                    <p>Name: {locationDetail.name}</p>
                    <p>Cost Rate: {locationDetail.costRate}</p>
                    <p>Availability: {locationDetail.availability}</p>
                    <p>Modified Date: {locationDetail.modifiedDate}</p>
                </div>
            )}
        </div>
    );
};

export default ContactComponent;
