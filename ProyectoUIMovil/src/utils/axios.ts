import axios from "axios";


const API_BASE_URL = "'http://localhost:5000/ProyectoAPI_FabioDiscua_CristopherFlores'" // Cambia esto por la URL de tu API (En caso de que lo hagas de manera local, como en este caso)  

const instance = axios.create({
  baseURL: API_BASE_URL,
  timeout: 10000,
  headers: {
    "Content-Type": "application/json",
  },
});

// Interceptor para agregar tokens (Sigue en desarrollo)
instance.interceptors.request.use(
  async (config) => {
    // const token = await AsyncStorage.getItem("token");
    // if (token) config.headers.Authorization = `Bearer ${token}`;
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

// Interceptor para manejar errores de respuesta
instance.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response) {
      console.warn("API Error:", error.response.data);
    } else {
      console.warn("Error sin respuesta del servidor:", error.message);
    }
    return Promise.reject(error);
  }
);

export default instance;
