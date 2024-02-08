import { NavLink, useLocation } from 'react-router-dom';
import logo from '../../assets/logo-nav.png';
import star from '../../assets/Star.png';
import mainBackground from '../../assets/1.png';
import sideBackground from '../../assets/2.png';
import './Header.scss';
import menu from '../../assets/menu_2.png'
import menu_close from '../../assets/close.png'
import { useState } from 'react';

type Props = {
    title: string | undefined,
    hasRating: boolean | undefined,
    rating: number | undefined
}

function Header({ title, hasRating, rating }: Props) {
    const location = useLocation();

    let [isMenuIcon, setMenuIcon] = useState(true);

    const menuIcon = () => {
        setMenuIcon(!Boolean(isMenuIcon));
    }


    return (
        <div className='header-parent'>
            { !isMenuIcon &&  
                <div className="header-navbar-custom"> 
                    <div className="header-navbar-pages-responsive">
                        <NavLink to='/wineries' className={({ isActive }) => [ isActive ? 'header-navbar-page-active' : '', 'header-navbar-page' ].join(" ")}>Винарии</NavLink>
                        <NavLink to='/wines' className={({ isActive }) => [ isActive ? 'header-navbar-page-active' : '', 'header-navbar-page' ].join(" ")}>Вина</NavLink>
                        <NavLink to='/locations' className={({ isActive }) => [ isActive ? 'header-navbar-page-active' : '', 'header-navbar-page' ].join(" ")}>Локации</NavLink>
                        <NavLink to='/aboutus' className={({ isActive }) => [ isActive ? 'header-navbar-page-active' : '', 'header-navbar-page' ].join(" ")}>За нас</NavLink>
                        <NavLink to='/contact' className={({ isActive }) => [ isActive ? 'header-navbar-page-active' : '', 'header-navbar-page' ].join(" ")}>Контакт</NavLink>
                    </div>
                </div> }

            <div className="header">
                {
                    location.pathname == '/' ?
                    <img src={mainBackground} className='header-background' alt='background'/> :
                    <img src={sideBackground} className='header-background' alt='background'/>
                }
                <div className="header-navbar">
                    <div className="header-navbar-logo">
                        <NavLink to='/'><img src={logo} className='header-navbar-logo-img' alt='logo'/></NavLink>
                    </div>
                    <div className="header-navbar-menu" onClick={menuIcon}>
                        { isMenuIcon ? <img src={menu} alt="menu-icon" className="header-navbar-menu-icon"/>
                                    : <img src={menu_close} alt="menu-icon" className="header-navbar-menu-icon"/> }
                    </div>
                    <div className="header-navbar-pages">
                        <NavLink to='/wineries' className={({ isActive }) => [ isActive ? 'header-navbar-page-active' : '', 'header-navbar-page' ].join(" ")}>Винарии</NavLink>
                        <NavLink to='/wines' className={({ isActive }) => [ isActive ? 'header-navbar-page-active' : '', 'header-navbar-page' ].join(" ")}>Вина</NavLink>
                        <NavLink to='/locations' className={({ isActive }) => [ isActive ? 'header-navbar-page-active' : '', 'header-navbar-page' ].join(" ")}>Локации</NavLink>
                        <NavLink to='/aboutus' className={({ isActive }) => [ isActive ? 'header-navbar-page-active' : '', 'header-navbar-page' ].join(" ")}>За нас</NavLink>
                        <NavLink to='/contact' className={({ isActive }) => [ isActive ? 'header-navbar-page-active' : '', 'header-navbar-page' ].join(" ")}>Контакт</NavLink>
                    </div>
                </div>
                <div className={['header-title', location.pathname == '/' ? 'header-title-center' : 'header-side-page' ].join(" ")}>
                    {
                        Boolean(hasRating) ? 
                        <span className='header-title-rating'>
                            <img src={star} className='header-title-rating-star' alt='star' />
                            <span className='header-title-rating-text'>{rating}</span>
                        </span> :
                        null
                    }
                    <span className={'header-title-text'}>{title}</span>
                </div>
            </div>
        </div>
    )
}

export default Header