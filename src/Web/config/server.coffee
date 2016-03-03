
newSolution = {"positions":[{"row":0,"column":5,"isValid":true},{"row":5,"column":6,"isValid":true},{"row":1,"column":3,"isValid":true},{"row":2,"column":1,"isValid":true},{"row":3,"column":7,"isValid":true},{"row":4,"column":4,"isValid":true},{"row":6,"column":0,"isValid":true},{"row":7,"column":2,isValid:true}],"$$hashKey":"005",hash:"513213744566072"}
allSolutions = [newSolution]

module.exports =

  drawRoutes: (app) =>
    app.get '/signalr/hubs', (req, res) ->
      res.type 'js'
      res.sendfile '/config/devHub.js',
        root: process.cwd()

    app.get '/api/v1/solutions/valid', (req, res) ->
      res.json allSolutions

    app.post '/api/v1/solutions/valid', (req, res) ->
      res.status 200
         .send 'got it'

    app.get '/api/v1/solutions/pending', (req, res) ->
      res.json allSolutions

    app.post '/api/v1/solutions/pending', (req, res) ->
      res.status 200
         .send 'got it'

