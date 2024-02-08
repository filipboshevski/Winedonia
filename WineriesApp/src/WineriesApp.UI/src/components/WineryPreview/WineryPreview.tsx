import React from 'react'
import star from '../../assets/Star.png';
import './WineryPreview.scss';
import { Link } from 'react-router-dom';
import { HashLink } from 'react-router-hash-link';

type Props = {
    id: string,
    img: string,
    rating: number,
    name: string,
    description: string
};

function WineryPreview({ id, img, rating, name, description }: Props) {
  return (
      <div className='winery-preview'>
          <HashLink to="/winery#" state={{ wineryId: id }}>
            <img src={img} alt={name} className='winery-preview-image' />
          </HashLink>
          <div className='winery-preview-info'>
              <div className='winery-preview-info-rating'>
                  <img className='winery-preview-info-rating-star' src={star} alt='star' />
                  <span className='winery-preview-info-rating-number'>{rating}</span>
              </div>
              <div className='winery-preview-info-name'>{name}</div>
          </div>
          <div title={description} className='winery-preview-description'>{description}</div>
      </div>
  )
}

export default WineryPreview