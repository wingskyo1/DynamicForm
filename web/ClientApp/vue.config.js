module.exports = {
    outputDir: 'dist',
    publicPath: '/'
    //required to fix sockjs-node SSL error
    //https://stackoverflow.com/questions/50877800/how-can-i-fix-these-sockjs-ssl-errors
    // devServer: {
    //     public: 'https://localhost:5001'
    // }
};