"use strict";

var webpack = require("webpack");
var extractTextPlugin = require("extract-text-webpack-plugin");

module.exports = {
    mode: 'development',
    entry: {
        "main": "./app.js",
        "profile": "./App/Profile/main.js",
        "edit-profile": "./App/EditProfile/main.js",
        "edit-song": "./App/EditSong/main.js",
        "messages": "./App/Messages/main.js",
        "connect": "./App/Connect/main.js",
        "sessions": "./App/Sessions/main.js",
        "upload-song": "./App/Profile/upload-song.js",
        "auth": "./App/Auth/main.js"
    },
    output: {
        filename: "../../Content/dist/[name].bundle.js"
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
            filename: "../../Content/dist/[name].bundle.css",
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
