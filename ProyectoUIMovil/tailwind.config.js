/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./App.{js,jsx,ts,tsx}",
    "./src/**/*.{js,jsx,ts,tsx}", // por si luego tienes componentes ahí
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
