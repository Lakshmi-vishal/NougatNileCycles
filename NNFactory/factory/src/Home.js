// Home.js

import React from 'react';
import { AppBar, Toolbar, Typography, Button, Box, Grid,Container } from '@material-ui/core';
import './Home.css';

export default function Home() {
    const linkStyle = {
        color: 'inherit', // Maintain inherited color
        textDecoration: 'none', // Remove underline
        padding: '8px 16px', // Example padding similar to Material-UI Button
        display: 'inline-block', // Display links inline
    };
    return (
        <div className="home">
            <AppBar position="static">
                <Toolbar>
                  
                    <Box className="container">
                        <Box className="lögo">
                            <img src="1cycle.png" width="120px" height="120px" />
                        </Box>
                        <Box className="nav">
                            <ul style={{ listStyleType: 'none', margin: 0, padding: 0, display: 'flex' }}>
                                <li><a href="/" style={linkStyle}>Home</a></li>
                                <li><a href="/feature" style={linkStyle}>Feature</a></li>
                                <li><a href="/products" style={linkStyle}>Products</a></li>
                                <li><a href="/contact" style={linkStyle}>Contact us</a></li>
                            </ul>
                        </Box>
                    </Box>
                </Toolbar>
            </AppBar>
            <div className="content">
               
                <Container maxWidth="lg">
                    <Grid container spacing={3} alignItems="center" className="content-grid">
                        <Grid item xs={12} md={6}>
                            <Typography variant="h4" className="headline">
                                "Ride the Excellence with Nougat Nile – Where Precision Meets Passion"
                            </Typography>
                            <Typography variant="body1" className="subheading">

At Nougat Nile, our dedication lies in engineering the finest bicycle components and accessories that not only meet but exceed the expectations of cycling enthusiasts and professionals alike. Our team, composed of seasoned designers and engineers, brings decades of industry experience to the table, crafting each product with precision and a deep understanding of the cyclist's needs. From robust frames to precision-engineered gear systems, our expansive range caters to the dynamic demands of the sport. Whether it's for the adrenaline rush of mountain biking or the rigorous demands of road racing, Nougat Nile's products are designed to enhance performance, comfort, and style. Embrace the journey with us, and let Nougat Nile be the silent ally in your pursuit of cycling excellence. </Typography>
                            <Button variant="contained" color="primary" className="cta-button">
                                BOKA HÄR
                            </Button>

                        </Grid>
                        <Grid item xs={12} md={6}>
                            <Box className="image-container">
                                <img src="cycle.png" alt="Swedish cycle" className="responsive-image" />
                            </Box>
                        </Grid>
                    </Grid>
                </Container>
               
            </div>
        </div>
    );
}
