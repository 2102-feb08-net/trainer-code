const path = require('path');

module.exports = {
  // mode: 'production',
  mode: 'development',
  entry: './out/index.js',
  output: {
    filename: 'main.js',
    path: path.resolve(__dirname, 'dist')
  }
};
