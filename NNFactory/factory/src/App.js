// src/App.js
import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Home from './Home';

import ProductComponent from './ProductComponent';

function App() {
    return (
        <Router>
         
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/products" element={<ProductComponent />} />
                
            </Routes>
        </Router>
    );
}

export default App;
