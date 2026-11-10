function MovieCard({ title, year, image, onClick }) {
  return (
    <div 
      onClick={onClick} 
      style={{ 
        width: '200px', 
        margin: '10px', 
        borderRadius: '8px', 
        overflow: 'hidden', 
        boxShadow: '0 4px 8px rgba(0,0,0,0.2)',
        cursor: 'pointer',
        transition: 'transform 0.2s'
      }}
      onMouseEnter={(e) => e.currentTarget.style.transform = 'scale(1.05)'}
      onMouseLeave={(e) => e.currentTarget.style.transform = 'scale(1)'}
    >
      <img src={image} alt={title} style={{ width: '100%', height: '300px', objectFit: 'cover' }} />
      <div style={{ padding: '10px', background: '#fff', color: '#000' }}>
        <h3 style={{ margin: '0 0 5px 0', fontSize: '18px' }}>{title}</h3>
        <p style={{ margin: 0, color: '#666' }}>{year} год</p>
      </div>
    </div>
  );
}

export default MovieCard;