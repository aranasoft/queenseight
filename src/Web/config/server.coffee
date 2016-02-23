module.exports =
  drawRoutes: (app) =>
    app.get '/signalr/hubs', (req, res) ->
      res.type 'js'
      res.sendfile './config/fakeHub.js'