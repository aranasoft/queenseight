queensEight.provider "q8ValidSolutionsApi", ->
  @$get = ($resource) ->
    $resource '/api/v1/solutions/valid', {}, {}
  return

queensEight.provider "q8PendingSolutionsApi", ->
  @$get = ($resource) ->
    $resource '/api/v1/solutions/pending', null,
      requestSolution: {method: 'POST'}
  return
