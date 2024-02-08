import React from 'react'
import Header from '../../components/Header/Header'
import Content from '../../components/Content/Content'
import { HashLink } from 'react-router-hash-link'
import './TermsAndConditions.scss'

function TermsAndConditions() {

    let content = [
        'Добредојдовте во Winedonia. Овие правила и услови ги прикажуваат правилата и прописите за користење на нашата веб страница.',
        'Со пристапување на оваа веб страна се согласувате дека целосно ги прифаќате овие правила и услови. Не продолжувајте да ја користите веб страницата на Winedonia доколку не ги прифаќате сите правила и услови наведени на оваа страница.',
        'Во никој случај Winedonia нема да биде одговорна за какви било посебни, директни, индиректни, последователни или случајни штети или какви било штети, без разлика дали се работи за договор, небрежност или друг деликт, што произлегува од или во врска со користење на веб страницата или содржината на веб страницата.',
        'Winedonia го задржува правото да ги ревидира овие услови во секое време без најава. Со користење на оваа веб страна, се согласувате да бидете обврзани со тогашната актуелна верзија на овие услови.'
    ]

    return (
        <div className='tad-container'>
            <div className="tad-header">
                <Header title={'Политика за користење'} hasRating={undefined} rating={undefined}/>
            </div>
            <Content content={content} isCustomStyled={undefined}/>
            <p className='tad-container-paragraph'>Доколку имате било какви прашања поврзани со Политиката на приватност, ве молиме да не исконтактирате <HashLink to="/contact#" className='tad-container-paragraph-link'>тука</HashLink>.</p>
        </div>
    )
}

export default TermsAndConditions