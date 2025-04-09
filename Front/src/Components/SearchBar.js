import React, { useState } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';

const SearchBar = ({ onSearch }) => {
  const [searchTerm, setSearchTerm] = useState('');
  const [searchType, setSearchType] = useState('title');

  const handleSearchChange = (e) => {
    const term = e.target.value;
    setSearchTerm(term);
    if (term.trim()) {
      onSearch({ term: term.trim(), type: searchType });
    } else {
      onSearch({ term: '', type: searchType }); 
    }
  };

  const handleTypeChange = (e) => {
    const newType = e.target.value;
    setSearchType(newType);
    if (searchTerm.trim()) {
      onSearch({ term: searchTerm.trim(), type: newType }); 
    }
  };

  return (
    <div className="card p-3 mb-4">
      <div className="d-flex align-items-center gap-2">
        <select
          value={searchType}
          onChange={handleTypeChange}
          className="form-select w-auto"
        >
          <option value="title">Título</option>
          <option value="author">Autor</option>
        </select>
        
        <input
          type="text"
          className="form-control"
          placeholder={`Buscar por ${searchType === 'title' ? 'título' : 'autor'}...`}
          value={searchTerm}
          onChange={handleSearchChange}
        />
      </div>
    </div>
  );
};

export default SearchBar;