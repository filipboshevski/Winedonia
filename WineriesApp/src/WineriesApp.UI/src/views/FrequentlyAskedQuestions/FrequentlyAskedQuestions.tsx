import React from 'react'
import Header from '../../components/Header/Header'
import Content from '../../components/Content/Content'
import './FrequentlyAskedQuestions.scss'

function FrequentlyAskedQuestions() {

    let content = [
        'Дали може да оставам коментар и рејтинг без да се регистирам ?',
        'Да. Во принцип Winedonia функционира така што може да оставите коментар и рејтинг анонимно.',
        'Дали оставањето на коментар и рејтинг влијае на винаријата/виното ?',
        'Да. Ве молиме да бидете дискретни и да напишете коментар кој соодвествува со вашето вистинско мислење бидејќи ова влијае врз крајниот рејтинг на винаријата/виното.',
        'Што ја прави Winedonia уникатна ?',
        'Нашата веб страна се разликува од другите така што тука може да оставите коментар, може да пребарувате винарии според градовите (регионите), според рејтинг или според име на некоја винарија.',
        'Како да стапам во контакт со вашиот тим за поддршка ?',
        'Најдобар начин да стапите во контакт со нас е да ни испратите ваши контакт информации преку нашата Контакт страница или пак да ни испратите email порака на contact@wineries.mk.'
    ]

    return (
        <div className='faq-container'>
            <div className="faq-header">
                <Header title={'Често поставувани прашања'} hasRating={undefined} rating={undefined}/>
            </div>
            <Content content={content} isCustomStyled={'faq-container-content'}/>
        </div>
    )
}

export default FrequentlyAskedQuestions