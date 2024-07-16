import axios from 'axios';
import { STORAGE_KEY } from '../constants/user';

const axiosInstance = axios.create({
  baseURL: '/api',
  timeout: 0,
  headers: {
    'Content-Type': 'application/json',
  },
});

axiosInstance.interceptors.request.use(
  async (config) => {
    if (config.url === '/Authentication/login') {
      return config;
    }
    const token = localStorage.getItem(STORAGE_KEY);
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => Promise.reject(error),
);

axiosInstance.interceptors.response.use(
  (response) => response,
  async (error) => {
    if (error.response && error.response.status === 401) {
      // const { dispatch } = store;
      // dispatch({ type: LOGOUT });
      // window.location.href = PAGES.signIn;
    }
    return Promise.reject(error);
  },
);

export default axiosInstance;
