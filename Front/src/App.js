import React, { useEffect, useState } from 'react';
import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom';
import BookList from './Components/BookList'; 
import BookForm from './Components/BookForm'; 
import SearchBar from './Components/SearchBar'; 
import Home from './Components/Home'; 
import { getAllBooks } from './services/bookService'; 
import './styles/App.css';

function App() {
  return (
    <Router>
      <div className="app-container">
        <header className="app-header">
          <h1>Biblioteca Proyecto Back</h1>
          <br></br>
          <Link to="/" className="btn btn-primary px-2 py-1 me-0 hover-effect" style={{backgroundColor:"#528fd1"}} >
                  <i className="bi bi-house me-1"></i>Inicio
          </Link>
        </header>
        <main className="container">
          <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/books" element={<BookManagement />} />
          </Routes>
        </main>
        <footer className="app-footer">
        </footer>
      </div>
    </Router>
  );
}

const BookManagement = () => {
  const [books, setBooks] = useState([]);
  const [filteredBooks, setFilteredBooks] = useState([]);

  useEffect(() => {
    fetchBooks();
  }, []);

  const fetchBooks = async () => {
    try {
      const data = await getAllBooks();
      setBooks(data);
      setFilteredBooks(data); 
    } catch (error) {
      console.error('Error al obtener libros:', error);
    }
  };

  const handleSearch = (searchData) => {
    const { term, type } = searchData;
    if (!term) {
      setFilteredBooks(books); 
      return;
    }
    const filtered = books.filter((book) => {
      const searchValue = type === 'title' ? book.title : book.author;
      return searchValue.toLowerCase().includes(term.toLowerCase());
    });
    setFilteredBooks(filtered);
  };

  return (
    <>
      <section className="search-section">
        <SearchBar onSearch={handleSearch} />
      </section>
      <section className="form-section">
        <BookForm onBookAdded={fetchBooks} />
      </section>
      <section className="list-section">
        <BookList books={filteredBooks} onBookDeleted={fetchBooks} />
      </section>
    </>
  );
};

export default App;