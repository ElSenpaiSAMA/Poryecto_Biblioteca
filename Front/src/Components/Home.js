import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap-icons/font/bootstrap-icons.css';
import '../styles/Home.css'; 
import { Link } from 'react-router-dom';  

const Home = () => {
  return (
    <div className="home-container min-vh-100 d-flex align-items-center">
      <div className="container py-5">
        <div className="animated-card bg-white p-5 rounded-4 shadow-lg mx-auto" style={{ maxWidth: '800px' }}>
          <div className="text-center mb-4">
            <i className="bi bi-book-fill display-1 text-primary mb-3"></i>
            <h1 className="gradient-text display-4 fw-bold mb-3">
              Biblioteca Proyecto Back
            </h1>
            <div className="d-grid gap-3 d-md-flex justify-content-md-center">
            <Link to="/books" className="btn btn-primary btn-lg px-4 me-md-2 hover-effect">
              <i className="bi bi-search me-2"></i>Ver Libros
            </Link>
            </div>
          </div>
          
          <div className="features-grid row text-center mt-5">      
            <div className="col-md-6.5 feature-item">
              <i className="bi bi-collection-fill fs-1 text-danger mb-3"></i>
              <h3>Todos los libros que quieras</h3>
            </div>
            <div className="col-md-12 feature-item">
              <i className="bi bi-bookmark-heart-fill fs-1 text-warning mb-3"></i>
              <h3>Favoritos</h3>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Home;