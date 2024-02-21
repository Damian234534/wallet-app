import axios from 'axios';

const restApiClient = axios.create({
  baseURL: 'http://localhost:5000/api',
});

export default restApiClient;