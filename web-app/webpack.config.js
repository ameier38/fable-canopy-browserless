const path = require("path")
const webpack = require("webpack");

module.exports = (env, argv) => {
    const mode = argv.mode

    const htmlPlugin = new HtmlWebpackPlugin({
        filename: 'index.html',
        template: './src/App/index.html'
    })

    return {
        mode: mode,
        entry: './src/App/App.fsproj',
        output: {
            path: path.join(__dirname, "dist"),
            filename: "main.js",
        },
        devServer: {
            contentBase: path.join(__dirname, "dist"),
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
            new webpack.HotModuleReplacementPlugin()
        ] : [
            htmlPlugin
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
