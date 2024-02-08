import React from 'react'
import Content from '../../components/Content/Content'
import Header from '../../components/Header/Header'
import './Privacy.scss'
import { HashLink } from 'react-router-hash-link';

function Privacy() {

    let content = [
        'Во Winedonia вашата приватност ни е најголем приоритет. Оваа Политика за приватност ги опишува видовите лични информации што се примаат и собираат и како се користат.',
        'Можеме да ги користиме информациите што ги собираме од вас кога ја посетувате нашата веб страна, сурфате на веб страната или користите одредени други функции на страницата на следниве начини:',
        '- За да го персонализирате вашето искуство и да ни овозможите да го испорачаме типот на содржини и понуди на винарии и вина за кои сте најмногу заинтересирани.',
        '- За да ја подобриме нашата веб страница за подобро да ви служи.',
        '- За брзо и ефикасно процесирање на вашите трансакции.',
        'Со користење на нашата страница, вие се согласувате со нашата политика за приватност.',
        'Го задржуваме правото да ја ажурираме или менуваме оваа Политика за приватност во секое време без претходно известување. Ве молиме периодично да ја прегледувате оваа политика.'
    ]

    return (
        <div className='privacy-container'>
            <div className="privacy-header">
                <Header title={'Приватност'} hasRating={undefined} rating={undefined}/>
            </div>
            <Content content={content} isCustomStyled={undefined}/>
            <p className='privacy-container-paragraph'>Доколку имате било какви прашања поврзани со Политиката на приватност, ве молиме да не исконтактирате <HashLink to="/contact#" className='privacy-container-paragraph-link'>тука</HashLink>.</p>
        </div>
    )
}

export default Privacy