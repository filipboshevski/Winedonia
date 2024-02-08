import { useEffect, useState } from 'react'
import Header from '../../components/Header/Header'
import SearchBar from '../../components/SearchBar/SearchBar'
import { SelectChangeEvent } from '@mui/material';
import './Main.scss';
import WineryPreview from '../../components/WineryPreview/WineryPreview';
import slika from '../../assets/Slika-primer.png';
import { MunicipalityService } from '../../services/MunicipalityService';
import { Municipality } from '../../models/Municipality';
import { useNavigate } from 'react-router-dom';
import { WineryPreviewInfo } from '../../models/WineryPreviewInfo';
import { WineryService } from '../../services/WineryService';

function Main() {
    const getCurrentDimension = () => {
        return {
              width: window.innerWidth,
              height: window.innerHeight
        }
    }

    const navigate = useNavigate();
    const municipalityService = new MunicipalityService();
    const wineryService = new WineryService();

    const [screenSize, setScreenSize] = useState(getCurrentDimension());

    const [locations, setLocations] = useState<string[]>([]);
    const [searchTerm, setSearchTerm] = useState('');
    const [municipalities, setMunicipalities] = useState<Municipality[]>([]);
    const [topWineries, setTopWineries] = useState<WineryPreviewInfo[]>([]);

    useEffect(() => {
        const updateDimension = () => {
          setScreenSize(getCurrentDimension())
        }
        window.addEventListener('resize', updateDimension);
        
        return(() => {
            window.removeEventListener('resize', updateDimension);
        })
      }, [screenSize])

    const getData = async () => {
        const municipalities = await municipalityService.getMunicipalities();
        const topWineries = await wineryService.getTopWineries();

        if (Boolean(municipalities) && municipalities != null) {
            setMunicipalities(municipalities);
        }

        if (Boolean(topWineries) && topWineries != null) {
            setTopWineries(topWineries);
        }
    }

    const onLocationsChange = (e: SelectChangeEvent<string[]>) => {
        let change = typeof(e.target.value) == 'string' ? [e.target.value] : e.target.value;
        setLocations(change);
    }

    const onSearchTermChange = async (change: string) => {
        setSearchTerm(change);
    }

    const onSubmit = () => {
        navigate('/wineries', { state: { searchTerm: searchTerm, locations: locations } });
    }

    const getWineries = () => {
        if (!Boolean(topWineries) || topWineries.length == 0) return null;
        let wineryContainers = [];

        for (let i = 0; i < topWineries.length; i += (screenSize.width < 801 ? 1 : 3)) {
            let items = topWineries.slice(i, i + (screenSize.width < 801 ? 1 : 3));
            let wineries = items.map((w: WineryPreviewInfo, idx) => <WineryPreview key={idx} id={w.id ?? ''} img={w.imageUrl ?? ''} rating={w.rating ?? 5} name={w.name ?? ''} description={w.description?.join() ?? ''} />);

            let div = <div key={i} className='main-top-wineries-links-container'>
                {wineries}
            </div>;

            wineryContainers.push(div);
        }

        return wineryContainers;
    }

    useEffect(() => {
        getData();
    }, []);

    return (
        <div className='main'>
            <Header title={"Најдобрите винарии во Македонија"} hasRating={undefined} rating={undefined} />
            <div style={{ justifyContent: 'center', display: 'flex' }}>
                <SearchBar
                    searchTerm={searchTerm}
                    locations={municipalities}
                    placeholder={'Каде би сакале да одите?'}
                    hasFilter={screenSize.width > 400}
                    hasButton={true}
                    locationsFilterVal={locations}
                    locationFilterCallback={onLocationsChange}
                    submitCallback={onSubmit}
                    inputCallback={onSearchTermChange}
                    classList={'main-search-bar'} 
                    hasRatingsFilter={false} 
                    ratingsFilterVal={[]} 
                    ratingsFilterCallback={undefined} />
            </div>
            <div className='main-top-wineries'>
                <div className='main-top-wineries-title'>Топ 12 винарии</div>
                <div className='main-top-wineries-links'>
                    { getWineries() }
                </div>
            </div>
        </div>
    )
}

export default Main