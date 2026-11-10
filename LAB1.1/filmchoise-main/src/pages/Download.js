import { useState } from 'react';
import { useLocation, Link } from 'react-router-dom';
import { getFactory } from '../factories/LanguageFactory';

function Download() {
  const location = useLocation();
  const movieTitle = location.state?.movieTitle || "Выберите фильм";
  
  const [lang, setLang] = useState('ru');

  // Главная магия паттерна: берем фабрику и создаем СРАЗУ и аудио, и субтитры
  const factory = getFactory(lang);
  const audio = factory.createAudio();
  const subtitles = factory.createSubtitles();

  return (
    <div style={{ padding: '20px', background: '#f9f9f9', borderRadius: '8px', marginTop: '20px' }}>
      <h1>Оформление заказа</h1>
      <h2 style={{ color: '#d32f2f' }}>Фильм: {movieTitle}</h2>
      
      <div style={{ margin: '20px 0' }}>
        <label style={{ marginRight: '10px', fontWeight: 'bold' }}>Выберите язык:</label>
        <select 
          value={lang} 
          onChange={(e) => setLang(e.target.value)}
          style={{ padding: '5px', fontSize: '16px' }}
        >
          <option value="ru">Русский</option>
          <option value="en">English</option>
          <option value="es">Español</option>
        </select>
      </div>

      <div style={{ background: '#fff', padding: '15px', borderRadius: '5px', border: '1px solid #ccc' }}>
        <p><strong>Характеристики поставки:</strong></p>
        <p>{audio.play()}</p>
        <p>{subtitles.show()}</p>
      </div>

      <button 
        style={{ marginTop: '20px', padding: '10px 20px', background: '#1976d2', color: '#fff', border: 'none', borderRadius: '5px', cursor: 'pointer' }}
        onClick={() => alert(`Заказ на фильм "${movieTitle}" успешно оформлен!`)}
      >
        Оформить и скачать
      </button>

      <br /><br />
      <Link to="/">← Вернуться к списку фильмов</Link>
    </div>
  );
}

export default Download;