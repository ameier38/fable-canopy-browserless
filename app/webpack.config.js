const path = require("path")
const HtmlWebpackPlugin = require("html-webpack-plugin")
const webpack = require("webpack");

module.exports = (env, argv) => {
    const mode = argv.mode

    const htmlPlugin = new HtmlWebpackPlugin({
        filename: 'index.html',
        template: path.resolve('./src/Client/index.html')
    })

    const environmentPlugin = new webpack.EnvironmentPlugin([
        'SERVER_SCHEME',
        'SERVER_HOST',
        'SERVER_PORT'
    ])

    return {
        mode: mode,
        entry: path.resolve('./src/App/App.fsproj'),
        output: {
            path: path.resolve(__dirname, "dist"),
            filename: "main.js",
        },
        devServer: {
            contentBase: path.resolve(__dirname, "dist"),
            port: 3000,
            hot: true,
            inline: true,
            // NB: required so that webpack will go to index.html on not found
            historyApiFallback: true
        },
        // NB: so webpack works with docker
        watchOptions: {
            poll: true
        },
        plugins: mode === 'development' ? [
            htmlPlugin,
            environmentPlugin,
            new webpack.HotModuleReplacementPlugin()
        ] : [
            htmlPlugin,
            environmentPlugin
        ],
        module: {
            rules: [
                { 
                    test: /\.fs(x|proj)?$/,
                    use: "fable-loader"
                },
                {
                    test: /\.(png|jpe?g|gif|svg)$/i,
                    use: "file-loader"
                }
            ]
        }
    }
} 
