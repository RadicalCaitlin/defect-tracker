const
    MiniCssExtractPlugin = require("mini-css-extract-plugin"),
    fs = require('fs'),
    path = require('path'),
    webpack = require('webpack');

const appPath = path.join(__dirname, 'src');

const appfiles = fs.readdirSync(appPath);
const entry = {};

appfiles.forEach(file => {
    const name = path.resolve(appPath, file);
    if (file !== 'Shared') {
        if (fs.statSync(name).isDirectory()) {
            entry[file] = name;
        }
    }
});

module.exports = (env, args) => {
    const mode = args.mode;
    const isDevBuild = mode !== 'production';

    console.debug("Mode: ", mode);

    const config = {
        mode,
        entry,

        devtool: isDevBuild ? "source-map" : false,

        output: {
            path: path.resolve(__dirname, '../wwwroot/dist')
        },

        resolve: {
            extensions: [".ts", ".tsx", ".js", ".json"]
        },

        module: {
            rules: [
                {
                    test: /\.ts(x?)$/,
                    use: [
                        {
                            loader: "awesome-typescript-loader",
                            options: {
                                reportFiles: [
                                    "src/**/*"
                                ]
                            }
                        }
                    ]
                },
                {
                    test: /\.s?css$/,
                    use: /* isDevBuild
                        ? ["style-loader", "css-loader", "sass-loader"]
                        :*/ [{
                        loader: MiniCssExtractPlugin.loader
                    },
                        "css-loader", "sass-loader"
                    ]
                },
                {
                    test: /\.(woff(2)?|ttf|eot|svg)(\?v=\d+\.\d+\.\d+)?$/,
                    use: [
                        {
                            loader: 'file-loader',
                            options: {
                                name: '[name].[ext]',
                                outputPath: '../dist/fonts/'
                            }
                        }
                    ]
                },
                {
                    test: /\.(png|jpg|gif)$/,
                    use: [
                        {
                            loader: 'file-loader',
                            options: {
                                name: '[path][name].[ext]',
                                context: path.resolve(__dirname, "src/"),
                                outputPath: '/',
                                publicPath: '../',
                                useRelativePaths: true
                            }
                        }
                    ]
                }
            ]
        },
        plugins: [
            new MiniCssExtractPlugin("site.css"),
            new webpack.ProvidePlugin({
                $: 'jquery',
                jQuery: 'jquery',
                'window.jQuery': 'jquery'
            })
        ]
    }

    if (!isDevBuild) {
        config.module.rules.push({
            enforce: "pre",
            test: /\.js$/,
            loader: "source-map-loader"
        });
    }

    return config;
};