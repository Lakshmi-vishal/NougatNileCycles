import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Home from './Home';
import ProductComponent from './ProductComponent';
import MerryComponent from './MerryComponent'; 
import Contact from './Contact';
function App() {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/products" element={<ProductComponent />} />
                <Route path="/feature" element={<MerryComponent />} /> 
                <Route path="/contact" element={<Contact />} /> 
                
            </Routes>
        </Router>
    );
}

export default App;
