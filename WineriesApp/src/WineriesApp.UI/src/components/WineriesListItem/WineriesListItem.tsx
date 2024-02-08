import { MouseEventHandler, useEffect } from 'react'
import './WineriesListItem.scss';
import { Link } from 'react-router-dom';
import locationIcon from '../../assets/union_black.png'
import ratingIcon from '../../assets/Star.png'
import { WinerySearchInfo } from '../../models/WinerySearchInfo';

type Props = {
  callback: ((data: WinerySearchInfo) => void) | undefined,
  data: WinerySearchInfo
}

function WineriesListItem({ callback, data }: Props) {

  const onMouseOver = () => {
    if (Boolean(callback) && callback != null) {
      callback(data);
    }
  }

  return (
    <div onMouseEnter={() => onMouseOver()} className="wineries-list-container">
        <div className="wineries-list-container-card">
            <div className="wineries-list-container-card-left">
                <img src={locationIcon} alt="location-icon" className='wineries-list-container-card-left-location'/>
                <img src={ratingIcon} alt="rating-icon" className='wineries-list-container-card-left-ratingicon'/>
                <span className='wineries-list-container-card-left-rating'>{data.rating}</span>
                <span title={data.name} className='wineries-list-container-card-left-winery'>{data.name}</span>
            </div>
            <div className="wineries-list-container-card-right">
                <Link to="/winery" state={{ wineryId: data.id }}><button className="wineries-list-container-card-right-button">Видете повеќе</button></Link>
            </div>
        </div>
    </div>
  )
}

export default WineriesListItem