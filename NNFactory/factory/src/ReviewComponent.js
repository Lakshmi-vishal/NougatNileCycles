// ReviewCard.js
import React from 'react';
import './ReviewCard.css'; // Assume you have some basic CSS for styling

const ReviewComponent = ({ review }) => {
    return (
        <div className="reviewCard">
            <p className="reviewComments">{review.comments}</p>
            <p className="reviewerName">- {review.reviewerName}</p>
        </div>
    );
};

export default ReviewComponent;
