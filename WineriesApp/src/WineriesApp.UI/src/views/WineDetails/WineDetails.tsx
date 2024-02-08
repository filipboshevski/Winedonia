import { useEffect, useState } from 'react'
import Header from '../../components/Header/Header'
import './WineDetails.scss'
import Content from '../../components/Content/Content'
import wineImg from '../../assets/Cabernet.png'
import { WineryPreviewInfo } from '../../models/WineryPreviewInfo'
import WineryPreview from '../../components/WineryPreview/WineryPreview'
import { WineService } from '../../services/WineService'
import Review from '../../components/Review/Review'
import { WineDetails } from '../../models/WineDetails'
import { useLocation } from 'react-router-dom'
import AddReview from '../../components/AddReview/AddReview'
import { ReviewService } from '../../services/ReviewService'
import { ReviewEntityType } from '../../enums/ReviewEntityType'
import { ReviewInfo } from '../../models/ReviewInfo'

function WineDetailsPage() {

    const location = useLocation();
    const { wineId } = location.state;
    const wineService = new WineService();

    const [details, setDetails] = useState<WineDetails | null>(null);
    const [reviews, setReviews] = useState<ReviewInfo[]>([]);
    const [showAddReview, setShowAddReview] = useState(false);

    const reviewService = new ReviewService();

    const getCurrentDimension = () => {
        return {
              width: window.innerWidth,
              height: window.innerHeight
        }
    }

    const [screenSize, setScreenSize] = useState(getCurrentDimension());

    useEffect(() => {
        const updateDimension = () => {
          setScreenSize(getCurrentDimension())
        }
        window.addEventListener('resize', updateDimension);
        
        return(() => {
            window.removeEventListener('resize', updateDimension);
        })
      }, [screenSize]);

    const getData = async () => {
        if (!Boolean(wineId)) {
            return;
        }

        const details = await wineService.getWineryDetails(wineId);
        const result = await reviewService.getReviews(ReviewEntityType.Wine, wineId);

        if (Boolean(details) && details != null) {
            setDetails(details);
        }
        
        if (Boolean(result) && result != null) {
            setReviews(result);
        }
    }

    const getWinesPerRow = (): number => {
        if (screenSize.width >= 1300) {
            return 3;
        }

        if (screenSize.width < 1300 && screenSize.width >= 900) {
            return 2;
        }

        if (screenSize.width < 900) {
            return 1;
        }

        return 3;
    }

    const getWineries = () => {
        if (!Boolean(details) || details == null || details.wineries.length == 0) return null;
        let wineryContainers = [];

        for (let i = 0; i < details.wineries.length; i += getWinesPerRow()) {
            let items = details.wineries.slice(i, i + getWinesPerRow());
            let wineries = items.map((w: WineryPreviewInfo, idx) => <WineryPreview key={idx} id={w.id ?? ''} img={w.imageUrl ?? ''} rating={w.rating ?? 5} name={w.name ?? ''} description={w.description?.join() ?? ''} />);

            let div = <div key={i} className='wine-details-container-wineries-links-container'>
                {wineries}
            </div>;

            wineryContainers.push(div);
        }

        return wineryContainers;
    }

    const submitReview = async (rating: number, comment: string) => {
        await reviewService.addReview(rating, comment, ReviewEntityType.Wine, details?.id ?? '');
        setShowAddReview(!showAddReview);
        getData();
    }

    useEffect(() => {
        getData();
    }, []);

    return (
        <div className="wine-details-wrapper">
            <div className="wine-details-header">
                <Header title={details?.name} hasRating={true} rating={details?.rating}/>
            </div>
            <div className="wine-details-container">
                <div className="wine-details-container-forwine">
                    <p className='wine-details-container-forwine-paragraph'>За виното</p>
                </div>
                <div className="wine-details-container-content">
                        <div className='wine-details-content-container'>
                            <Content content={details?.description ?? []} isCustomStyled={'wine-details-container-content-description'}/>
                            { !showAddReview && <div className="winery-details-container-contact-info-rating-div">
                                <button onClick={() => setShowAddReview(!showAddReview)} className="winery-details-container-contact-info-rating-div-button">Оцени вино</button>
                            </div> }
                            { showAddReview && <AddReview toggleCallback={() => setShowAddReview(false)} addReviewCallback={submitReview} classList={'wine-details-container-content-add-review'} />}
                        </div>
                        <div className="wine-details-container-content-imageurl">
                            <img src={details?.imageUrl} alt="wine-photo" className="wine-details-container-content-imageurl-image"/>
                        </div>
                </div>
                <div className="wine-details-container-forwineries">
                    <p className='wine-details-container-forwine-paragraph'>Винарии кои ја имаат оваа врста на вино</p>
                </div>
                <div className="wine-details-container-wineries">
                    { getWineries() }
                </div>
                { reviews.length > 0 && <div className="wine-details-container-forrating">
                    <p className='wine-details-container-forrating-paragraph'>Рејтинзи</p>
                </div> }
                {
                    reviews.length > 0 && reviews.map((r: ReviewInfo, idx) => <Review key={idx} data={r}/>)
                }
            </div>
        </div>
    )
}

export default WineDetailsPage