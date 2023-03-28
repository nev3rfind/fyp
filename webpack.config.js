const HtmlWebpackPlugin = require('html-webpack-plugin');
const path = require('path');
const { VueLoaderPlugin } = require('vue-loader');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');


module.exports = {
    mode: 'development',
    devtool: 'eval',
    entry: [
        './src/js/app.js',
    ],
    output: {
        clean: true,
        path: path.resolve(__dirname, 'dist'),
        publicPath: '/dist',
        filename: '[name].bundle.[contenthash].js',
    },
    resolve: {
        // point bundler to the vue template compiler
        alias: {
            'vue$': 'vue/dist/vue.esm-bundler.js',
        },
        // allow imports to omit file exensions, 
        // e.g. "import foo from 'foobar'" instead of "import foo from 'foobar.js'"
        extensions: ['.js', '.vue'],
    },
    devServer: {
        historyApiFallback: {
            index: '/dist/index.html',
        },
        proxy: [
            {
                context: '/api/**',
                target: 'http://localhost:64373', // use port from IISExpress
            },
        ],
        open: true
    },
    module: {
        rules: [
            {
                test: /\.vue$/,
                use: 'vue-loader',
            },
            {
                test: /\.css$/,
                use: [
                    'style-loader',
                    'css-loader',
                ],
            },
            {
                test: /\.js$/,
                exclude: /node_modules/,
                use: {
                    loader: 'babel-loader',
                    options: {
                        presets: ['@babel/preset-env'],
                    },
                },
            },
            {
                test: /\.(sa|sc|c)ss$/,
                use: [
                    MiniCssExtractPlugin.loader,
                    'css-loader',
                    'sass-loader'
                ],
            },
            {
                test: /\.svg$/,
                use: [
                    {
                        loader: 'url-loader',
                        options: {
                            limit: 10000, // file size limit
                        },
                    },
                ],
            }


        ],
    },
    plugins: [
        new VueLoaderPlugin(),
        new HtmlWebpackPlugin({
            template: 'src/index.html',
            inject: true,
            // favicon: 'src/images/favicon.ico',
            publicPath: '/dist'
        }),
        new MiniCssExtractPlugin()
    ],
};