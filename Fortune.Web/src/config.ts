const config = {
    apiBaseUrl: import.meta.env.VITE_API_BASE_URL || "https://localhost:7109",
    debug: import.meta.env.VITE_DEBUG === "true"
  };
  
  
  export default config;
  