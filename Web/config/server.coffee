module.exports =
  drawRoutes: (app) =>
    ###
    app.get '/api/greeting/:message', (req, res) ->
      res.json
        message: "OK, "+req.params.message
    ###
