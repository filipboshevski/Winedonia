import React from 'react'
import './Review.scss'
import ratingIcon from '../../assets/Star.png'
import guest from '../../assets/userIcon.png'
import { ReviewInfo } from '../../models/ReviewInfo'

type Props = {
    data: ReviewInfo
}

function Review({ data }: Props) {
  return (
    <div className="review">
        <div className="review-card">
            <div className="review-card-rating">
                <img src={ratingIcon} alt="rating-icon" className="review-card-rating-icon"/>
                &nbsp;
                <span className="review-card-rating-paragraph">{data.rating}</span>
                &nbsp;&nbsp;&nbsp;
                <span className="review-card-rating-paragraph-from">од</span>
                &nbsp;&nbsp;&nbsp;
                <img src={guest} alt="rating-icon" className="review-card-guest-icon"/>
                &nbsp;
                <span className="review-card-rating-paragraph-guest">Гостин</span>
                &nbsp;&nbsp;
                <span className="review-card-rating-paragraph-from" style={{ fontWeight: 600 }}>{new Date(data.date).toLocaleString()}</span>
            </div>
            <div className="review-card-comment">
                <span className="review-card-comment-paragraph">{data.comment}</span>
            </div>  
        </div>
    </div>
  )
}

export default Review