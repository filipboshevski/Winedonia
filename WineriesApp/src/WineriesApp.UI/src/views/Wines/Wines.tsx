import React, { useEffect, useState } from 'react'
import Header from '../../components/Header/Header'
import './Wines.scss'
import union_icon from '../../assets/union-1.png';
import union_down_icon from '../../assets/union-black-2.png';
import SearchBar from '../../components/SearchBar/SearchBar'
import bottle from '../../assets/wine_bottle.png'
import { WineService } from '../../services/WineService';
import { WinesSearchInfo } from '../../models/WinesSearchInfo';
import { WineType, WineTypeDescriptions } from '../../enums/WineType';
import { HashLink } from 'react-router-hash-link';
import { SelectChangeEvent } from '@mui/material';

function Wines() {
    const wineService = new WineService();

    const [ratingsExpanded, setRatingsExpanded] = useState(true);
    const [wineTypesExpanded, setWineTypesExpanded] = useState(true);

    const [wines, setWines] = useState<WinesSearchInfo[]>([]);
    const [timeoutId, setTimeoutId] = useState<NodeJS.Timeout | null>(null);

    const [searchTerm, setSearchTerm] = useState('');
    const [filterTypes, setFilterTypes] = useState<string[]>([]);
    const [filterRatings, setFilterRatings] = useState<string[]>([]);

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
        const wines = await wineService.filterWines();

        if (Boolean(wines) && wines != null) {
            setWines(wines);
        }
    }

    const getWines = () => {
        if (!Boolean(wines) || wines.length == 0) return null;
        let wineContainers = [];

        for (let i = 0; i < wines.length; i += (screenSize.width > 900 ? 4 : 2)) {
            let items = wines.slice(i, i + (screenSize.width > 900 ? 4 : 2));
            let mappedWines = items?.map((w: WinesSearchInfo, idx: number) => <div key={idx} className="wines-container-filtered-content-item">
                <HashLink to="/wine#" state={{ wineId: w.id }}><img src={w.imageUrl} alt="wine bottle" className="wines-container-filtered-content-item-img"/></HashLink>
                <div className="wines-container-filtered-content-item-paragraph">
                    <span>{w.name}</span>
                </div>
            </div>);

            let div = <div key={i} className='wines-container-filtered-content-container'>
                {mappedWines}
            </div>;

            wineContainers.push(div);
        }

        return wineContainers;
    }

    const filterWines = async (searchTerm: string | null = null, ratings: number[] = [], types: number[] = []) => {
        if (timeoutId) {
            clearTimeout(timeoutId);
        }

        const newTimeoutId = setTimeout(async () => {
            const result = await wineService.filterWines(searchTerm, ratings, types);

            if (Boolean(result)) {
                setWines(result ?? []);
            }
        }, 300);

        setTimeoutId(newTimeoutId);
    }

    const onTypesChange = (e: SelectChangeEvent<string>) => {
        let newTypes: string[] = [];

        if (filterTypes.includes(e.target.value)) {
            newTypes = filterTypes.filter(t => t != e.target.value);
        }
        else {
            newTypes = filterTypes;
            newTypes.push(e.target.value);
        }

        setFilterTypes(newTypes);

        filterWines(searchTerm, filterRatings.map(r => Number(r)), newTypes.map(r => Number(r)));
    };

    const onRatingsChange = (e: SelectChangeEvent<string>) => {
        let newRatings: string[] = [];

        if (filterRatings.includes(e.target.value)) {
            newRatings = filterRatings.filter(t => t != e.target.value);
        }
        else {
            newRatings = filterRatings;
            newRatings.push(e.target.value);
        }

        setFilterRatings(newRatings);

        filterWines(searchTerm, newRatings.map(r => Number(r)), filterTypes.map(r => Number(r)));
    }

    const onSearchTermChange = async (change: string) => {
        setSearchTerm(change);
        filterWines(change, filterRatings.map(r => Number(r)), filterTypes.map(r => Number(r)));
    }

    useEffect(() => {
        getData();
    }, []);
    
    return (
        <div>
            <div className="wines-header">
                <Header title={"Вина"} hasRating={undefined} rating={undefined} />
            </div>
            <div className="wines-container">
                <div className="wines-container-input">
                    <SearchBar
                        searchTerm={searchTerm}
                        locations={[]}
                        placeholder={'Име на вино'} 
                        hasFilter={undefined} 
                        hasButton={undefined} 
                        locationsFilterVal={[]} 
                        locationFilterCallback={undefined} 
                        submitCallback={undefined} 
                        inputCallback={onSearchTermChange} 
                        classList={'wines-container-searchinput'} 
                        hasRatingsFilter={false} 
                        ratingsFilterVal={[]} 
                        ratingsFilterCallback={undefined}/>
                    <div className="wines-container-filter">
                        <div className="wines-container-filter-rating-box" onClick={() => setRatingsExpanded(!ratingsExpanded)}>
                            <p className='wines-container-filter-rating-box-paragraph'>Рејтинг</p>
                            { ratingsExpanded && <img src={union_icon} className='wines-container-filter-rating-box-img' alt="icon"/> }
                            { !ratingsExpanded && <img src={union_down_icon} className='wines-container-filter-rating-box-img' alt="icon"/> }
                        </div>
                        { ratingsExpanded && <div className="wines-container-filter-rating-checkbox">
                            <div className="wines-container-filter-rating-checkbox-item">
                                <input checked={Boolean(filterRatings) && filterRatings.includes("5")} onChange={e => onRatingsChange(e)} type="checkbox" value="5" id="5"/>
                                <label className="wines-container-filter-rating-checkbox-item-label" htmlFor="5">5.0</label>
                            </div>
                            <div className="wines-container-filter-rating-checkbox-item">
                                <input checked={Boolean(filterRatings) && filterRatings.includes("4")} onChange={e => onRatingsChange(e)} type="checkbox" value="4" id="4"/>
                                <label className="wines-container-filter-rating-checkbox-item-label" htmlFor="4">4.0</label>
                            </div>
                            <div className="wines-container-filter-rating-checkbox-item">
                                <input checked={Boolean(filterRatings) && filterRatings.includes("3")} onChange={e => onRatingsChange(e)} type="checkbox" value="3" id="3"/>
                                <label className="wines-container-filter-rating-checkbox-item-label" htmlFor="3">3.0</label>
                            </div>
                            <div className="wines-container-filter-rating-checkbox-item">
                                <input checked={Boolean(filterRatings) && filterRatings.includes("2")} onChange={e => onRatingsChange(e)} type="checkbox" value="2" id="2"/>
                                <label className="wines-container-filter-rating-checkbox-item-label" htmlFor="2">2.0</label>
                            </div>
                            <div className="wines-container-filter-rating-checkbox-item">
                                <input checked={Boolean(filterRatings) && filterRatings.includes("1")} onChange={e => onRatingsChange(e)} type="checkbox" value="1" id="1"/>
                                <label className="wines-container-filter-rating-checkbox-item-label" htmlFor="1">1.0</label>
                            </div>
                        </div> }
                        <div className="wines-container-filter-rating-box" onClick={() => setWineTypesExpanded(!wineTypesExpanded)}>
                            <p className='wines-container-filter-rating-box-paragraph'>Тип на вино</p>
                            { wineTypesExpanded && <img src={union_icon} className='wines-container-filter-rating-box-img' alt="icon"/> }
                            { !wineTypesExpanded && <img src={union_down_icon} className='wines-container-filter-rating-box-img' alt="icon"/> }
                        </div>
                        { wineTypesExpanded && <div className="wines-container-filter-rating-checkbox">
                            <div className="wines-container-filter-rating-checkbox-item">
                                <input checked={Boolean(filterTypes) && filterTypes.includes(WineType.Red.toString())} onChange={e => onTypesChange(e)} type="checkbox" value={WineType.Red} id="red"/>
                                <label className="wines-container-filter-rating-checkbox-item-label" htmlFor="red">Црвено</label>
                            </div>
                            <div className="wines-container-filter-rating-checkbox-item">
                                <input checked={Boolean(filterTypes) && filterTypes.includes(WineType.White.toString())} onChange={e => onTypesChange(e)} type="checkbox" value={WineType.White} id="white"/>
                                <label className="wines-container-filter-rating-checkbox-item-label" htmlFor="white">Бело</label>
                            </div>
                            <div className="wines-container-filter-rating-checkbox-item">
                                <input checked={Boolean(filterTypes) && filterTypes.includes(WineType.Rose.toString())} onChange={e => onTypesChange(e)} type="checkbox" value={WineType.Rose} id="rose"/>
                                <label className="wines-container-filter-rating-checkbox-item-label" htmlFor="rose">Розе</label>
                            </div>
                            <div className="wines-container-filter-rating-checkbox-item">
                                <input checked={Boolean(filterTypes) && filterTypes.includes(WineType.Sparkling.toString())} onChange={e => onTypesChange(e)} type="checkbox" value={WineType.Sparkling} id="penlivo"/>
                                <label className="wines-container-filter-rating-checkbox-item-label" htmlFor="penlivo">Пенливо</label>
                            </div>
                        </div> }
                    </div>
                </div>
                <div className="wines-container-filtered-content">
                    { getWines() }
                    { wines.length == 0 && <span>Нема записи</span> }
                </div>
            </div>
        </div>
    )
}

export default Wines