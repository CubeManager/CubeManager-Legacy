/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./src/**/*.{html,js}"],
  theme: {
    extend: {},
  },
  daisyui: {
    themes: ["dark", "light", "aqua"],
  },
  plugins: [require('@tailwindcss/typography'), require("@tailwindcss/forms"), require("daisyui")],
}
