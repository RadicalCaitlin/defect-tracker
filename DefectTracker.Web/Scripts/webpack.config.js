"use strict";

var webpack = require("webpack");
var extractTextPlugin = require("extract-text-webpack-plugin");

module.exports = {
    mode: 'development',
    entry: {
        "main": "./js/main.js",
        "defectChart": "./js/DefectChart/main.js"
    },
    output: {
        filename: "../../wwwroot/dist/[name].bundle.js"
    },
    module: {
        rules: [
            {
                test: /\.scss$/,
                loader: extractTextPlugin.extract(["css-loader", "postcss-loader", "sass-loader"]),
            }
        ]
    },
    plugins: [
        new extractTextPlugin({
            filename: "../../wwwroot/dist/[name].bundle.css",
            allChunks: true
        }),
        new webpack.LoaderOptionsPlugin({
            debug: true
        }),
    ],
    externals: {
        $: 'jQuery',
        jquery: 'jQuery',
        'jquery.validation': 'jquery.validation'
    }
};
