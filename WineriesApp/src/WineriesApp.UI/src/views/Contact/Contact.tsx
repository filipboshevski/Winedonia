import React from 'react'
import './Contact.scss'
import Header from '../../components/Header/Header'
import './Contact.scss'

function Contact() {
  return (
    <div>
        <div className="contact-header">
            <Header title={"Контакт"} hasRating={undefined} rating={undefined} />
        </div>

        <div className="contact-container">
            <div className="contact-container-paragraph">
                <p>Би сакале да слушнеме од Вас, ве молиме оставете ни порака доколку имате нешто да споделите.</p>
            </div>

            <div className="contact-container-formgroup">
                <div className="contact-container-formgroup-part">
                    <div className="contact-container-formgroup-part-inside">
                        <label>Име</label>
                        <input type="text"/>
                    </div>
                    <div className="contact-container-formgroup-part-inside">
                        <label>Презиме</label>
                        <input type="text"/>
                    </div>
                </div>
                <div className="contact-container-formgroup-email">
                    <label>Е-маил адреса</label>
                    <input type="text"/>
                </div>
                <div className="contact-container-formgroup-msg">
                    <label>Порака</label>
                    <textarea></textarea>
                </div>
                <div className="contact-container-formgroup-submit">
                    <button type="submit">Испрати</button>
                </div>
            </div>
        </div>
    </div>
  )
}

export default Contact