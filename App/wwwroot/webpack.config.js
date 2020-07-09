const path = require('path');

module.exports = {
  entry: './js/App.jsx',
  output: {
    filename: 'bundle.js',
    globalObject: 'this',
    path: path.resolve(__dirname, 'dist'),
    publicPath: '/dist/'
  },
  mode: 'development',
  module: {
    rules: [
      {
        test: /\.jsx?$/,
        exclude: /node_modules/,
        loader: 'babel-loader',
      },
    ],
  }
}