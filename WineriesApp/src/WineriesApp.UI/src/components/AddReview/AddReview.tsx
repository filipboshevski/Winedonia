import { useState } from 'react';
import './AddReview.scss';

type Props = {
  addReviewCallback: (rating: number, comment: string) => void,
  classList: string | undefined,
  toggleCallback: () => void
}

function AddReview({ addReviewCallback, classList, toggleCallback }: Props) {
    const [comment, setComment] = useState('');
    const [rating, setRating] = useState(5.0);

    const onSubmit = () => {
      addReviewCallback(rating, comment);
    };

    return (
      <div className={`add-review ${Boolean(classList) ? classList : ''}`}>
          <div className='add-review-rating'>
              <label htmlFor='add-review-select'>Рејтинг</label>
              <select onChange={e => setRating(Number(e.target.value))} name="review" id="add-review-select">
                <option value={5.0}>5.0</option>
                <option value={4.0}>4.0</option>
                <option value={3.0}>3.0</option>
                <option value={2.0}>2.0</option>
                <option value={1.0}>1.0</option>
              </select>
          </div>
          <div className='add-review-comment'>
            <label htmlFor='add-review-select'>Коментар</label>
            <textarea id='add-review-comment' onChange={e => setComment(e.target.value)} className='add-review-comment-textarea'></textarea>
          </div>
          <div className='add-review-buttons'>
            <button onClick={() => onSubmit()} className='search-bar-filter-submit-btn add-review-submit'>Додај рејтинг</button>
            <button onClick={() => toggleCallback()} style={{ marginLeft: '10px' }} className='search-bar-filter-submit-btn add-review-submit'>Назад</button>
          </div>
      </div>
    )
}

export default AddReview