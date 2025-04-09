import React, { useState } from 'react';
import { updateBook, deleteBook, toggleFavoriteBook } from '../services/bookService';
import 'bootstrap/dist/css/bootstrap.min.css';

const BookList = ({ books, onBookDeleted }) => {
  const [editingBook, setEditingBook] = useState(null);
  const [formData, setFormData] = useState({
    title: '',
    description: '',
    author: '',
    year: '',
    image: null,
    categories: '',
  });
  const [error, setError] = useState(null);

  const handleEdit = (book) => {
    setEditingBook(book);
    setFormData({
      title: book.title,
      description: book.description || '',
      author: book.author,
      year: book.year,
      image: null,
      categories: book.categories.join(','),
    });
  };

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
  };

  const handleFileChange = (e) => {
    setFormData({ ...formData, image: e.target.files[0] });
  };

  const handleUpdate = async (e) => {
    e.preventDefault();
    try {
      await updateBook(
        editingBook.id,
        formData.title,
        formData.description,
        formData.author,
        formData.year,
        formData.image,
        formData.categories
      );
      setEditingBook(null);
      setError(null);
      onBookDeleted(); // Refresca la lista
    } catch (error) {
      setError('Error al actualizar el libro');
      console.error('ERROR', error);
    }
  };

  const handleDelete = async (id) => {
    try {
      await deleteBook(id);
      setError(null);
      onBookDeleted();
    } catch (error) {
      setError('Error al eliminar el libro');
      console.error('ERROR', error);
    }
  };

  const handleToggleFavorite = async (id) => {
    try {
      await toggleFavoriteBook(id);
      setError(null);
      onBookDeleted(); // Refresca la lista para reflejar el cambio en isFavorite
    } catch (error) {
      setError('Error al cambiar el estado de favorito');
      console.error('ERROR', error);
    }
  };

  return (
    <div>
      <h2 className="mb-4">Lista de Libros</h2>
      {error && <div className="alert alert-danger">{error}</div>}
      <div className="row">
        {books.map((book) => (
          <div key={book.id} className="col-md-4 mb-4">
            <div className="card book-card">
              <img
                src={`data:image/jpeg;base64,${book.imageData}`}
                className="card-img-top"
                alt={book.title}
              />
              <div className="card-body">
                <h5 className="card-title">
                  {book.title}{' '}
                  {book.isFavorite && <span className="text-warning">★</span>}
                </h5>
                <p className="card-text">
                  <strong>{book.author}</strong> - {book.year}
                </p>
                {book.description && <p className="card-text">{book.description}</p>}
                <p className="card-text">Categorías: {book.categories.join(', ')}</p>
                <button
                  className={`btn ${book.isFavorite ? 'btn-outline-secondary' : 'btn-outline-success'} me-2`}
                  onClick={() => handleToggleFavorite(book.id)}
                >
                  {book.isFavorite ? 'Quitar de favoritos' : 'Añadir a favoritos'}
                </button>
                <button
                  className="btn btn-warning me-2"
                  onClick={() => handleEdit(book)}
                >
                  Editar
                </button>
                <button
                  className="btn btn-danger"
                  onClick={() => handleDelete(book.id)}
                >
                  Eliminar
                </button>
              </div>
            </div>
          </div>
        ))}
      </div>

      {editingBook && (
        <div className="modal-overlay">
          <div className="modal-content">
            <h3>Editar Libro</h3>
            {error && <div className="alert alert-danger">{error}</div>}
            <form onSubmit={handleUpdate} encType="multipart/form-data">
              <div className="mb-3">
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
              <div className="mb-3">
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
              <div className="mb-3">
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
              <div className="mb-3">
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
              <div className="mb-3">
                <input
                  type="file"
                  name="image"
                  className="form-control"
                  accept="image/*"
                  onChange={handleFileChange}
                />
              </div>
              <button type="submit" className="btn btn-primary me-2">
                Actualizar
              </button>
              <button
                type="button"
                className="btn btn-secondary"
                onClick={() => setEditingBook(null)}
              >
                Cancelar
              </button>
            </form>
          </div>
        </div>
      )}
    </div>
  );
};

export default BookList;