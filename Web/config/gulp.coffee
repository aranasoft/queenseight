pkg = require './../package.json'

output =
  css:      'css/app.css'
  jsApp:    'js/app.js'
  jsVendor: 'js/vendor.js'
files =
  coffee:   [
    'app/js/app.coffee'
    'app/js/**/*.coffee'
  ]
  img:      'app/img/**/*.*'
  static:   'app/static/**/*.*'
  webfonts: [
    'vendor/webfonts/**/*.*'
    'vendor/components/font-awesome/fonts/**/*.*'
  ]
  jade:
    app:    'app/pages/**/*.jade'
    templates: 'app/templates/**/*.jade'
    watch:  [
      'app/pages/**/*.jade'
      'app/templates/**/*.jade'
    ]
  js:
    app:    ['app/js/**/*.js']
    vendor: [
      'vendor/components/jquery/jquery.js'
      'vendor/components/underscore/underscore-min.js'
      'vendor/components/angular/angular.js'
      'vendor/js/**/*.js'
    ]
  less:
    app:    'app/css/app.less'
    watch:  [
      'app/css/**'
      'vendor/components/bootstrap/less/**'
    ]

module.exports =
  output: output
  files: files
  htmlmin:
    removeComments: true
    removeCommentsFromCDATA: true
    collapseWhitespace: true
    collapseBooleanAttributes: false
    removeAttributeQuotes: false
    removeRedundantAttributes: true
    removeEmptyAttributes: false
    removeOptionalTags: false
    removeEmptyElements: false
  html2js:
    moduleName: 'QueensEight'
    prefix: "templates/"
  coffeelint:
    max_line_length:
      level: 'ignore'
  jshint:
  # enforcing options
    curly: true
    eqeqeq: true
    latedef: true
    newcap: true
    noarg: true
  # relaxing options
    boss: true
    eqnull: true
    sub: true
  # environment/globals
    browser: true
  jade:
    pretty: true
    data:
      js: output.jsApp
      jsVendor: output.jsVendor
      css: output.css
      pkg: pkg
  server:
    web:
      port: 3000
      base: 'generated'
    livereload: true
    open: true
