import React, { useState, useEffect, useRef } from 'react';
import ReviewCard from './ReviewComponent'; 
import './MerryComponent.css'; 
import ArrowBackIcon from '@material-ui/icons/ArrowBack';
import { useNavigate } from 'react-router-dom'; 

const MerryComponent = () => {
    const videoRef = useRef(null);
    const [dynamicText, setDynamicText] = useState('');
    const [reviews, setReviews] = useState([]);
    const productNames = ['Bike Chains', 'Pedals', 'Brake Pads', 'Handlebars', 'Saddles'];
    const navigate = useNavigate();

    useEffect(() => {
        fetch('http://localhost:5200/api/ProductReviews')
            .then(response => response.json())
            .then(data => setReviews(data))
            .catch(error => console.error("Failed to fetch product reviews:", error));
    }, []);

    useEffect(() => {
        const intervalId = setInterval(() => {
            let currentIndex = productNames.findIndex(name => name === dynamicText);
            currentIndex = (currentIndex + 1) % productNames.length;
            setDynamicText(productNames[currentIndex]);
        }, 400);
        return () => clearInterval(intervalId);
    }, [dynamicText, productNames]);

    useEffect(() => {
        const video = videoRef.current;
        if (video) {
            video.addEventListener('mouseover', () => video.play());
            video.addEventListener('mouseout', () => video.pause());

            return () => {
                video.removeEventListener('mouseover', () => video.play());
                video.removeEventListener('mouseout', () => video.pause());
            };
        }
    }, []);

    return (
        <div className="componentContainer">
            <ArrowBackIcon onClick={() => navigate(-1)} style={{ cursor: 'pointer', margin: '10px' }} />
            <div className="productHighlight">
                <h1 className="productTyping">{dynamicText}</h1>
            </div>
            <div className="mediaContainer">
                <div className="videoContainer">
                    <video ref={videoRef} src="EB.mp4" type="video/mp4" muted loop></video>
                </div>
            </div>
            <h4 style={{ textAlign: 'center', margin: '20px 0' }}>What Our Clients Feel About Our Product</h4>
            <div className="reviewsContainer">
                {reviews.map(review => (
                    <ReviewCard key={review.productReviewId} review={review} />
                ))}
            </div>
        </div>
    );
};

export default MerryComponent;
