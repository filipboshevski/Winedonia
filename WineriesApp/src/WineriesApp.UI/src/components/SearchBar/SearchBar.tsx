import { ChangeEvent, MouseEventHandler, ReactNode, useEffect, useState } from 'react';
import searchIcon from '../../assets/icons8-search-120.png';
import locationIcon from '../../assets/Union.png';
import filterStarIcon from '../../assets/filterStar.png';
import { FormControl, InputLabel, MenuItem, Select, SelectChangeEvent, Theme, useTheme } from '@mui/material';
import './SearchBar.scss';
import { Municipality } from '../../models/Municipality';

type Props = {
    searchTerm: string | null,
    locations: Municipality[],
    placeholder: string | undefined,
    hasFilter: boolean | undefined,
    hasRatingsFilter: boolean | undefined,
    hasButton: boolean | undefined,
    locationsFilterVal: string[],
    ratingsFilterVal: string[],
    locationFilterCallback: ((event: SelectChangeEvent<string[]>) => void) | undefined,
    ratingsFilterCallback: ((event: SelectChangeEvent<string[]>) => void) | undefined,
    submitCallback: (() => void) | undefined,
    inputCallback: ((change: string) => void) | undefined,
    classList: string
}

const ITEM_HEIGHT = 48;
const ITEM_PADDING_TOP = 8;
const MenuProps = {
  PaperProps: {
    style: {
      maxHeight: ITEM_HEIGHT * 4.5 + ITEM_PADDING_TOP,
      width: 250,
    },
  },
};

function getStyles(location: string, filterVal: string[], theme: Theme) {
    return {
      fontWeight:
        filterVal.indexOf(location) === -1
          ? theme.typography.fontWeightRegular
          : theme.typography.fontWeightMedium,
          fontSize: 13
    };
}

function SearchBar({
    searchTerm,
    locations,
    placeholder,
    hasFilter,
    hasRatingsFilter,
    hasButton,
    locationsFilterVal,
    ratingsFilterVal,
    locationFilterCallback,
    ratingsFilterCallback,
    submitCallback,
    inputCallback,
    classList = ""
} : Props) {
    const [isLocationsPlaceholderVisible, setLocationsPlaceholderVisible] = useState(false);
    const [isRatingsPlaceholderVisible, setRatingsPlaceholderVisible] = useState(false);
    const theme = useTheme();

    useEffect(() => {
        setLocationsPlaceholderVisible(locationsFilterVal.length <= 0);
      }, [locationsFilterVal]);

    useEffect(() => {
        setRatingsPlaceholderVisible(ratingsFilterVal.length <= 0);
    }, [ratingsFilterVal]);
    
    const onInputChange = (e: ChangeEvent<HTMLInputElement>) => {
        let change = Boolean(e.target?.value) ? e.target?.value : "";
        
        if (Boolean(inputCallback) && inputCallback != null) {
            inputCallback(change);
        }
    }

    const onLocationSelectChange = (e: SelectChangeEvent<string[]>) => {
        if (Boolean(locationFilterCallback) && locationFilterCallback != null) {
            locationFilterCallback(e);
        }

        setLocationsPlaceholderVisible(e.target.value.length == 0);
    }

    const onRatingsSelectChange = (e: SelectChangeEvent<string[]>) => {
        if (Boolean(ratingsFilterCallback) && ratingsFilterCallback != null) {
            ratingsFilterCallback(e);
        }

        setRatingsPlaceholderVisible(e.target.value.length == 0);
    }

    const onKeyDown = (e: React.KeyboardEvent<HTMLInputElement>) => {
        if (e.key == 'Enter' && Boolean(submitCallback) && submitCallback != null) {
            submitCallback();
        }
    }

    return (
        <div className={['search-bar', classList].join(" ")}>
            <div className="search-bar-textbox">
                <img className='search-bar-textbox-icon' src={searchIcon} alt='icon' />
                <input onKeyDown={onKeyDown} maxLength={50} className='search-bar-textbox-input' onChange={onInputChange} type='text' value={searchTerm ?? ''} placeholder={placeholder} />
            </div>
            <div className='search-bar-optionals-container'>
                { hasFilter && <div className='search-bar-filter'>
                    <div className='search-bar-filter-separator'></div>
                    <div className='search-bar-filter-input'>
                        <img src={locationIcon} alt='location' className='search-bar-filter-input-icon' />
                        <FormControl id="search-form-control" variant='standard' sx={{ m: 1, width: 300 }}>
                            { isLocationsPlaceholderVisible && <span className='search-form-control-placeholder'>Локација</span> }
                            <Select
                                id="search-multiple-locations"
                                multiple
                                value={locationsFilterVal}
                                onChange={onLocationSelectChange}
                                sx={{fontSize:'small'}}
                                disableUnderline={true}
                                >
                                {locations.map((location) => (
                                    <MenuItem
                                        key={location.id}
                                        value={location.id}
                                        style={getStyles(location.name ?? '', locationsFilterVal, theme)}>
                                        {location.name}
                                    </MenuItem>
                                ))}
                            </Select>
                        </FormControl>
                    </div>
                </div>
                }
                { hasRatingsFilter && <div className='search-bar-filter'>
                    <div className='search-bar-filter-separator'></div>
                    <div className='search-bar-filter-input'>
                        <img src={filterStarIcon} alt='location' className='search-bar-filter-star-icon' />
                        <FormControl id="search-form-control" variant='standard' sx={{ m: 1, width: 300 }}>
                            { isRatingsPlaceholderVisible && <span className='search-form-control-placeholder'>Рејтинг</span> }
                            <Select
                                id="search-multiple-ratings"
                                multiple
                                value={ratingsFilterVal}
                                onChange={onRatingsSelectChange}
                                sx={{fontSize:'small'}}
                                disableUnderline={true}
                                >
                                {[1.0, 2.0, 3.0, 4.0, 5.0].map((rating, idx) => (
                                    <MenuItem
                                        key={idx}
                                        value={rating.toString()}
                                        style={getStyles(rating.toString(), ratingsFilterVal, theme)}
                                        
                                        >
                                        {rating}
                                    </MenuItem>
                                ))}
                            </Select>
                        </FormControl>
                    </div>
                </div>
                }
                { hasButton && <div className='search-bar-filter-submit'>
                    <button className='search-bar-filter-submit-btn' onClick={submitCallback}>Пребарајте</button>
                </div> }
            </div>
        </div>
    )
}

export default SearchBar