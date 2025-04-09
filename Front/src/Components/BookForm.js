import React, { useState } from 'react';
import { createBook } from '../services/bookService'; 
import 'bootstrap/dist/css/bootstrap.min.css';

const BookForm = ({ onBookAdded }) => {
  const [formData, setFormData] = useState({
    title: '',
    description: '',
    author: '',
    year: '',
    image: null,
    categories: '',
  });
  const [error, setError] = useState(null);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
  };

  const handleFileChange = (e) => {
    setFormData({ ...formData, image: e.target.files[0] });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await createBook(
        formData.title,
        formData.description,
        formData.author,
        formData.year,
        formData.image,
        formData.categories
      );
      setFormData({ title: '', description: '', author: '', year: '', image: null, categories: '' });
      setError(null);
      onBookAdded();
    } catch (error) {
      setError('Error al añadir el libro. Por favor, intenta de nuevo.');
      console.error('Error al añadir libro:', error);
    }
  };

  return (
    <div className="card p-4 form-card">
      <h2 className="mb-0">Agregar Nuevo Libro</h2>
      {error && <div className="alert alert-danger">{error}</div>}
      <form onSubmit={handleSubmit} encType="multipart/form-data">
        <div className="row">
          <div className="col-md-6 mb-3">
            <input
              type="text"
              name="title"
              className="form-control"
              placeholder="Título"
              value={formData.title}
              onChange={handleInputChange}
              required
            />
          </div>
          <div className="col-md-6 mb-3">
            <input
              type="text"
              name="author"
              className="form-control"
              placeholder="Autor"
              value={formData.author}
              onChange={handleInputChange}
              required
            />
          </div>
        </div>
        <div className="mb-3">
          <input
            type="text"
            name="description"
            className="form-control"
            placeholder="Descripción (opcional)"
            value={formData.description}
            onChange={handleInputChange}
          />
        </div>
        <div className="row">
          <div className="col-md-6 mb-3">
            <input
              type="number"
              name="year"
              className="form-control"
              placeholder="Año"
              value={formData.year}
              onChange={handleInputChange}
              required
            />
          </div>
          <div className="col-md-6 mb-3">
            <input
              type="text"
              name="categories"
              className="form-control"
              placeholder="Categorías (separadas por comas)"
              value={formData.categories}
              onChange={handleInputChange}
              required
            />
          </div>
        </div>
        <div className="mb-3">
          <input
            type="file"
            name="image"
            className="form-control"
            accept="image/*"
            onChange={handleFileChange}
            required
          />
        </div>
        <button type="submit" className="btn btn-success w-100">
          Agregar Libro
        </button>
      </form>
    </div>
  );
};

export default BookForm;