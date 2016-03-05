pkg = require './../package.json'

output =
  css:      'css/app.css'
  jsApp:    'js/app.js'
  jsVendor: 'js/vendor.js'
files =
  coffee:   [
    'app/js/app.coffee'
    'app/js/solutionData.coffee'
    'app/js/resources.coffee'
    'app/js/solutionHub.coffee'
    'app/js/solutionService.coffee'
    'app/js/**/*.coffee'
  ]
  img:      'app/img/**/*.*'
  static:   'app/static/**/*.*'
  webfonts: [
    'vendor/webfonts/**/*.*'
    'vendor/components/font-awesome/fonts/**/*.*'
  ]
  lodash:
    app:    'app/pages/**/*.html'
    templates: 'app/templates/**/*.html'
    watch:  [
      'app/pages/**/*.html'
      'app/templates/**/*.html'
    ]
  js:
    app:    ['app/js/**/*.js']
    vendor: [
      'vendor/components/jquery/jquery.js'
      'vendor/components/underscore/underscore.js'
      'vendor/components/angular/angular.js'
      'vendor/components/angular-resource/angular-resource.js'
      'vendor/components/toastr/toastr.js'
      'vendor/components/orbweaver-message/orbweaver-message.js'
      'vendor/js/**/*.js'
    ]
  less:
    app:    ['vendor/components/toastr/toastr.less','app/css/app.less']
    watch:  [
      'app/css/**'
      'vendor/components/bootstrap/less/**'
      'vendor/components/toastr/toastr.less'
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
  lodash:
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
