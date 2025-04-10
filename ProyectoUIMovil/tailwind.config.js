/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./App.js",
    "./src/**/*.{js,jsx}", // por si luego tienes componentes ahí
  ],
  theme: {
    extend: {
      fontFamily: {
        poppins: ['Poppins-Regular'],
        poppinsBold: ['Poppins-Bold'],
      },
    },
  },
  plugins: [],
};
