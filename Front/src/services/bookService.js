import axios from 'axios';

const API_URL = 'http://localhost:5107/api/books';

export const getAllBooks = async () => {
  const response = await axios.get(API_URL);
  return response.data;
};

export const createBook = async (title, description, author, year, image, categories) => {
  const formData = new FormData();
  formData.append('title', title);
  formData.append('description', description || '');
  formData.append('author', author);
  formData.append('year', year);
  formData.append('image', image);
  formData.append('categories', categories);

  const response = await axios.post(API_URL, formData, {
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  });
  return response.data;
};

export const updateBook = async (id, title, description, author, year, image, categories) => {
  const formData = new FormData();
  formData.append('title', title);
  formData.append('description', description || '');
  formData.append('author', author);
  formData.append('year', year);
  if (image) formData.append('image', image);
  formData.append('categories', categories);

  await axios.put(`${API_URL}/${id}`, formData, {
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  });
};

export const deleteBook = async (id) => {
  await axios.delete(`${API_URL}/${id}`);
};

export const toggleFavoriteBook = async (id) => {
  await axios.post(`${API_URL}/${id}/favorite`);
};