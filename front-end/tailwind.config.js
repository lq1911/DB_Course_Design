module.exports = {
  content: [
    "./src/**/*.{vue,js,ts,jsx,tsx}",
    "./public/index.html",
  ],
  theme: {
    extend: {
      fontFamily: {
        'yahei': ['"Microsoft YaHei"', 'sans-serif'],
        'hei': ['"SimHei"', 'sans-serif'],
      },
    },
  },
  plugins: [],
}