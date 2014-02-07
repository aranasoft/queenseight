send = require 'send'

module.exports =
  drawRoutes: (app) =>
    app.get '/signalr/hubs', (req, res) ->
      res.setHeader 'Content-Type', 'application/javascript'
      file = send req, './server/testHub.js'
      file.pipe res