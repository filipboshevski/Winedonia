import React from 'react'
import Header from '../../components/Header/Header'
import './Locations.scss'
import sk from '../../assets/Skopje.png';
import ka from '../../assets/Kavadarci.png'
import dk from '../../assets/Demirkapija.png'
import oh from '../../assets/Ohrid.png'
import ve from '../../assets/Veles.png'
import ne from '../../assets/Negotino.png'
import ge from '../../assets/Gevgelija.png'
import sht from '../../assets/Stip.png'
import { Link } from 'react-router-dom';

function Locations() {
  return (
    <div>
        <div className="locations-header">
            <Header title={"Најпопуларни локации"} hasRating={undefined} rating={undefined} />
        </div>
        <div className='locations-rows-container'>
            <div className="locations-container">
                <Link to="/wineries" state={{ locationName: 'Skopje' }}>
                    <div className="locations-container-card">
                        <img src={sk} alt="Skopje"/>
                        <p>Скопје</p>
                    </div>
                </Link>
                
                <Link to="/wineries" state={{ locationName: 'Kavadarci' }}>
                    <div className="locations-container-card">
                        <img src={ka} alt="Kavadarci"/>
                        <p>Кавадарци</p>
                    </div>
                </Link>

                <Link to="/wineries" state={{ locationName: 'Demir Kapija' }}>
                    <div className="locations-container-card">
                        <img src={dk} alt="Demir Kapija"/>
                        <p>Демир Капија</p>
                    </div>
                </Link>

                <Link to="/wineries" state={{ locationName: 'Ohrid' }}>
                    <div className="locations-container-card">
                        <img src={oh} alt="Ohrid"/>
                        <p>Охрид</p>
                    </div>
                </Link>
            </div>
            <div className="locations-container">
                <Link to="/wineries" state={{ locationName: 'Veles' }}>
                    <div className="locations-container-card">
                        <img src={ve} alt="Veles"/>
                        <p>Велес</p>
                    </div>
                </Link>

                <Link to="/wineries" state={{ locationName: 'Negotino' }}>
                    <div className="locations-container-card">
                        <img src={ne} alt="Negotino"/>
                        <p>Неготино</p>
                    </div>
                </Link>

                <Link to="/wineries" state={{ locationName: 'Gevgelija' }}>
                    <div className="locations-container-card">
                        <img src={ge} alt="Gevgelija"/>
                        <p>Гевгелија</p>
                    </div>
                </Link>

                <Link to="/wineries" state={{ locationName: 'Stip' }}>
                    <div className="locations-container-card">
                        <img src={sht} alt="Shtip"/>
                        <p>Штип</p>
                    </div>
                </Link>
            </div>
        </div>
    </div>
  )
}

export default Locations