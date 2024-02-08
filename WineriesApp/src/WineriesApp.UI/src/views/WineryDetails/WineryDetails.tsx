import './WineryDetails.scss';
import Header from '../../components/Header/Header';
import Content from '../../components/Content/Content';
import { MapContainer, Marker, Popup, TileLayer } from 'react-leaflet';
import 'leaflet/dist/leaflet.css';
import Leaflet from 'leaflet';
import { Link } from 'react-router-dom';
import ratingIcon from '../../assets/Star.png';
import { useLocation } from 'react-router-dom';
import { useEffect, useState } from 'react';
import { WineryService } from '../../services/WineryService';
import { WineryDetails } from '../../models/WineryDetails';
import { UrlHelper } from '../../helpers/UrlHelper';
import AddReview from '../../components/AddReview/AddReview';
import { ReviewService } from '../../services/ReviewService';
import { ReviewEntityType } from '../../enums/ReviewEntityType';
import { ReviewInfo } from '../../models/ReviewInfo';
import Review from '../../components/Review/Review';

Leaflet.Icon.Default.mergeOptions({
    iconRetinaUrl: require('leaflet/dist/images/marker-icon-2x.png'),
    iconUrl: require('leaflet/dist/images/marker-icon.png'),
    shadowUrl: require('leaflet/dist/images/marker-shadow.png')
});

function WineryDetailsPage() {

    const wineryService = new WineryService();
    const reviewService = new ReviewService();

    const location = useLocation();
    const { wineryId } = location.state;

    const [details, setDetails] = useState<WineryDetails | null>(null);
    const [reviews, setReviews] = useState<ReviewInfo[]>([]);
    const [marker, setMarker] = useState<Leaflet.Marker<any>|null>(null);

    const [showAddReview, setShowAddReview] = useState(false);
    
    const getData = async () => {
        if (!Boolean(wineryId)) {
            return;
        }

        const details = await wineryService.getWineryDetails(wineryId);
        const reviews = await reviewService.getReviews(ReviewEntityType.Winery, wineryId);

        if (Boolean(details)) {
            setDetails(details);
        }

        if (Boolean(reviews)) {
            setReviews(reviews ?? []);
        }
    }

    const submitReview = async (rating: number, comment: string) => {
        await reviewService.addReview(rating, comment, ReviewEntityType.Winery, details?.id ?? '');
        setShowAddReview(!showAddReview);
        getData();
    }

    useEffect(() => {
        getData();
    }, []);

    useEffect(() => {
        if (marker) {
            marker.openPopup();
        }
    }, [marker]);

    return (
        <div>
            <div className="winery-details-header">
                <Header title={details?.name} hasRating={true} rating={details?.rating ?? 5.0} />
            </div>
            <div className="winery-details-container">
                <div className="winery-details-container-forwinery">
                    <p className='winery-details-container-forwinery-paragraph'>За винаријата</p>
                </div>
                <div className="winery-details-container-content">
                    <Content content={details?.description ?? []} isCustomStyled={'winery-details-container-content-description'}/>
                    <div className="winery-details-container-content-imageurl">
                        <img src={details?.imageUrl} alt="winery-photo" className="winery-details-container-content-imageurl-image"/>
                    </div>
                </div>
                <div className="winery-details-container-location-and-contact">
                    <div className="winery-details-container-location">
                        <p className='winery-details-container-location-paragraph'>Локација</p>
                        <div className="winery-details-container-location-map">
                            { Boolean(details) && <MapContainer center={[details?.latitude ?? 0, details?.longitude ?? 0]} zoom={14} scrollWheelZoom={false}>
                                <TileLayer attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors' url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"/>
                                <Marker ref={setMarker} position={[details?.latitude ?? 0, details?.longitude ?? 0]}>
                                    <Popup className='winery-details-container-location-map-popup'>
                                        <span className='winery-details-container-location-map-popup-name'>{details?.name}</span> <br/>
                                        <span className='winery-details-container-location-map-popup-rating'>
                                        <img src={ratingIcon} alt="rating-icon" className='winery-details-container-location-map-popup-rating-img'/>
                                            {details?.rating}
                                        </span>
                                        <br/>
                                        <span className="winery-details-container-location-map-popup-address">
                                            {details?.address}
                                        </span>
                                        <br />
                                        <span className="winery-details-container-location-map-popup-phone">
                                            {details?.contact}
                                        </span>
                                        <br />
                                        <span className="winery-details-container-location-map-popup-website">
                                            <a href={UrlHelper.getUrl(details?.url ?? null)} target='_blank'>{UrlHelper.getUrl(details?.url ?? null)}</a>
                                        </span>
                                    </Popup>
                                </Marker>
                            </MapContainer> }
                            { !Boolean(details) && <span>Се вчитува...</span> }
                        </div>
                    </div>

                    <div className={`winery-details-container-contact ${showAddReview ? 'winery-details-container-contact-add-review-active' : ''}`}>
                        <p className='winery-details-container-contact-paragraph'>Контакт</p>
                        <p className='winery-details-container-contact-info'>Доколку сте заинтересирани и сакате да дознаете повеќе за винаријата може да ја посетете нивната страница или да ги исконтактирате на нивниот телефонски број.</p>
                        <div className="winery-details-container-contact-info-website-and-tel">
                            <div className="winery-details-container-contact-info-website-div">
                                <p className="winery-details-container-contact-website-title">Website</p>
                                <p className="winery-details-container-contact-website-url"><a target='_blank' title={UrlHelper.getUrl(details?.url ?? null)} href={UrlHelper.getUrl(details?.url ?? null)} className="winery-details-container-contact-website-url-link">{UrlHelper.getUrl(details?.url ?? null)}</a></p>
                            </div>
                            <div className="winery-details-container-contact-info-tel-div">
                                <p className="winery-details-container-contact-tel-title">Телефон</p>
                                <p className="winery-details-container-contact-tel-number">{details?.contact}</p>
                            </div>
                        </div>
                        <div className="winery-details-container-contact-info-comment-and-rating">
                            { !showAddReview && <div className="winery-details-container-contact-info-rating-div">
                                <button onClick={() => setShowAddReview(!showAddReview)} className="winery-details-container-contact-info-rating-div-button">Оцени винарија</button>
                            </div> }
                            { showAddReview && <AddReview toggleCallback={() => setShowAddReview(false)} addReviewCallback={submitReview} classList={undefined} />}
                        </div>
                    </div>
                </div>
                { reviews.length > 0 && <div className="wine-details-container-forwine">
                    <p className='wine-details-container-forwine-paragraph'>Рејтинзи</p>
                </div> }
                {
                    reviews.length > 0 && reviews.map((r: ReviewInfo, idx) => <Review key={idx} data={r}/>)
                }
            </div>
        </div>
    )
}

export default WineryDetailsPage