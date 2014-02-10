gulp = require 'gulp'
gutil = require 'gulp-util'
fs = require 'fs'
path = require 'path'

bower = require 'gulp-bower'
clean = require 'gulp-clean'
coffee = require 'gulp-coffee'
coffeelint = require 'gulp-coffeelint'
concat = require 'gulp-concat'
jade = require 'gulp-jade'
jslint = require 'gulp-jshint'
jslintReporter = require 'jshint-stylish'
less = require 'gulp-less'
cssmin = require 'gulp-minify-css'
connect = require 'gulp-connect'
uglify = require 'gulp-uglify'

plumber = require 'gulp-plumber'

es = require 'event-stream'
pkg = require './package.json'
server = require './config/server'
urlrouter = require 'urlrouter'

output =
  css:      'css/app.css'
  jsApp:    'js/app.js'
  jsVendor: 'js/vendor.js'

files =
  coffee:   'app/js/**/*.coffee'
  img:      'app/img/**/*.*'
  static:   'app/static/**/*.*'
  webfonts: [
    'vendor/webfonts/**/*.*'
    'vendor/components/font-awesome/fonts/**/*.*'
  ]
  jade:     'app/pages/**/*.jade'
  js:
    app:    ['app/js/**/*.js']
    vendor: [
      'vendor/components/jquery/jquery.min.js'
      'vendor/components/underscore/underscore-min.js'
      'vendor/components/angular/angular.min.js'
      'vendor/js/**/*.js'
    ]
  less:
    app:    'app/css/app.less'
    watch:  [
      'app/css/**'
      'vendor/components/bootstrap/less/**'
    ]

config =
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
    port: 8000
    base: 'generated'
    livereload: true
    open: true
    middleware: (connect, options) ->
      middlewares = [];
      if (!Array.isArray(options.base))
        options.base = [options.base]
  
      directory = options.directory || options.base[options.base.length - 1]
      options.base.forEach (base) ->
        # Serve static files.
        middlewares.push(connect.static(base))
  
      middlewares.push urlrouter(server.drawRoutes)
      # Make directory browse-able.
      middlewares.push connect.directory(directory)
      middlewares

gulp.task 'default', ['lint','build']

gulp.task 'run', ['lint','build','server','watch']

gulp.task 'build', [
    'install'
    'js'
    'css'
    'jade'
    'copy'
  ]

gulp.task 'install', () ->
  bower()

gulp.task 'lint', ['coffeelint','jslint']

gulp.task 'coffeelint', () ->
  gulp.src(files.coffee)
    .pipe(coffeelint())
    .pipe(coffeelint.reporter())

gulp.task 'jslint', () ->
  gulp.src(files.js.app)
    .pipe(jslint(config.jshint))
    .pipe(jslint.reporter(jslintReporter))
  
gulp.task 'jade', () ->
  gulp.src(files.jade)
    .pipe(jade(config.jade))
    .pipe(gulp.dest('./generated'))
    .pipe(gulp.dest('./dist'))

gulp.task 'jsApp', () ->
  es.concat(
      gulp.src(files.coffee).pipe(coffee()),
      gulp.src(files.js.app)
    ).pipe(concat(output.jsApp))
    .pipe(gulp.dest('./generated'))
    .pipe(uglify())
    .pipe(gulp.dest('./dist'))
  
gulp.task 'jsVendor', ['install'], () ->
  gulp.src(files.js.vendor)
    .pipe(concat(output.jsVendor))
    .pipe(gulp.dest('./generated'))
    .pipe(gulp.dest('./dist'))
  
gulp.task 'js', ['jsApp','jsVendor']
  
gulp.task 'css', ['install'], () ->
  gulp.src(files.less.app)
    .pipe(plumber())
    .pipe(less())
    .pipe(concat(output.css))
    .pipe(gulp.dest('./generated'))
    .pipe(cssmin())
    .pipe(gulp.dest('./dist'))
  
gulp.task 'clean', () ->
  gulp.src(['./dist','./generated', bowerDirectory()])
    .pipe(clean())

gulp.task 'copy', ['install'], () ->
  es.concat(
    gulp.src(files.img)
      .pipe(gulp.dest('./generated/img'))
      .pipe(gulp.dest('./dist/img')),
    gulp.src(files.static)
      .pipe(gulp.dest('./generated/'))
      .pipe(gulp.dest('./dist/')),
    gulp.src(files.webfonts)
      .pipe(gulp.dest('./generated/fonts'))
      .pipe(gulp.dest('./dist/fonts'))
  )

gulp.task 'watch', () ->
  gulp.watch files.coffee,      ['coffeelint','jsApp']
  gulp.watch files.js.app,      ['jslint','jsApp']
  gulp.watch files.js.vendor,   ['jsVendor']
  gulp.watch files.jade.pages,  ['jade']
  gulp.watch files.less.watch,  ['css']
  gulp.watch [files.img, files.webfonts, files.static],  ['copy']
  
gulp.task 'server', ['build'], connect.server(config.server)

bowerDirectory = () ->
  bowerpath = path.join(process.cwd(), ".bowerrc")
  bowerrc = fs.readFileSync(bowerpath) unless !fs.existsSync bowerpath 
  bowerConfig = JSON.parse(bowerrc) if bowerrc?
  bowerConfig?.directory || "vendor/components"
 