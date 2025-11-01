import axios from 'axios';

const setAuthToken = token => {
  if (token) {
    // Apply to every request with Bearer prefix
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
  } else {
    // Delete auth token
    delete axios.defaults.headers.common['Authorization'];
  }
};

export default setAuthToken;
