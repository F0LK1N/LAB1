import { useNavigate } from 'react-router-dom';
import MovieCard from '../Components/MovieCard';

function Home() {
  const navigate = useNavigate(); 

  const movies = [
    { id: 1, title: 'Интерстеллар', year: 2014, image: 'https://image.tmdb.org/t/p/w500/gEU2QniE6E77NI6lCU6MxlNBvIx.jpg' },
    { id: 2, title: 'Начало', year: 2010, image: 'https://image.tmdb.org/t/p/w500/oYuLEt3zVCKq57qu2F8dT7NIa6f.jpg' },
    { id: 3, title: 'Джон Уик', year: 2014, image: 'https://image.tmdb.org/t/p/w500/fZPSd91yGE9fCcCe6OoQr6E3Bev.jpg' },
    { id: 4, title: 'Побег из Шоушенка', year: 1994, image: 'https://image.tmdb.org/t/p/w500/9cqNxx0GxF0bflZmeSMuL5tnGzr.jpg' }
  ];

  
  const handleMovieClick = (movieTitle) => {
    navigate('/download', { state: { movieTitle } });
  };

  return (
    <div>
      <h1>Кинопрокат</h1>
      <p>Выберите киноленту для оформления заказа:</p>
      
      <div style={{ display: 'flex', flexWrap: 'wrap', marginTop: '20px' }}>
        {movies.map((movie) => (
          <MovieCard 
            key={movie.id} 
            title={movie.title} 
            year={movie.year} 
            image={movie.image} 
            onClick={() => handleMovieClick(movie.title)} ы
          />
        ))}
      </div>
    </div>
  );
}

export default Home;