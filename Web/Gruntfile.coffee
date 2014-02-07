timer = require "grunt-timer"
fs = require 'fs'
path = require 'path'
server = require './config/server'
urlrouter = require 'urlrouter'

module.exports = (grunt) ->
  timer.init grunt

  # Project configuration.
  grunt.initConfig
    pkg: grunt.file.readJSON 'package.json'

    files:
      coffee:
        app: "app/js/**/*.coffee"
        generated: "generated/js/app.coffee.js"
  
      css:
        vendor: "vendor/css/**/*.css"
        app: "app/css/**/*.css"
        concatenated: "generated/css/app.css"
        minified: "dist/css/app.css"
        minifiedWebRelative: "css/app.css"
        
      img:
        root: "img"

      jade:
        templates: "app/templates/**/*.jade"
        generatedTemplate: "generated/template/jade.js"
        pages: "**/*.jade"
        pageRoot: "app/pages/"

      js:
        app: "app/js/**/*.js"
        vendor: [
          "vendor/components/jquery/jquery.js"
          "vendor/components/underscore/underscore.js"
          "vendor/components/angular/angular.js"
          "vendor/js/**/*.js"
        ]
        concatenatedVendor: "generated/js/vendor.js"
        minifiedVendor: "dist/js/vendor.js"
        minifiedVendorWebRelative: "js/vendor.js"
        concatenated: "generated/js/app.js"
        minified: "dist/js/app.js"
        minifiedWebRelative: "js/app.js"        
        
      less:
        app: "app/css/app.less"
        vendor: "vendor/css/**/*.less"
        generatedApp: "generated/css/app.less.css"
        generatedVendor: "generated/css/vendor.less.css"
        watch: "app/css/**/*.less"

      webfonts:
        root: "fonts"

    bower:
      install:
        options:
          copy: false

    coffee:
      compile:
        files:
          "<%= files.coffee.generated %>": "<%= files.coffee.app %>"
          
    coffeelint:
      app: [
        "<%= files.coffee.app %>"
      ]

    concat:
      css:
        src: [
          "<%= files.less.generatedVendor %>"
          "<%= files.css.vendor %>"
          "<%= files.less.generatedApp %>"
          "<%= files.css.app %>"
        ]
        dest: "<%= files.css.concatenated %>"


      js:
        src: [
          "<%= files.coffee.generated %>"
          "<%= files.js.app %>"
        ]
        dest: "<%= files.js.concatenated %>"

      jsVendor:
        src: ["<%= files.js.vendor %>"]
        dest: "<%= files.js.concatenatedVendor %>"

    connect:
      server:
        options:
          port: 8000
          base: 'generated'
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

    copy:
      imagesDev:
        files: [{
          expand: true
          cwd: "app/img/"
          src: "**"
          dest: "generated/<%= files.img.root %>/"
        }
        {
          expand: true
          cwd: "vendor/img/"
          src: "**"
          dest: "generated/<%= files.img.root %>/"
        }]
      imagesDist:
        files: [{
          expand: true
          cwd: "app/img/"
          src: "**"
          dest: "dist/<%= files.img.root %>/"
        }
        {
          expand: true
          cwd: "vendor/img/"
          src: "**"
          dest: "dist/<%= files.img.root %>/"
        }]
      staticDev:
        files: [
          expand: true
          cwd: "app/static"
          src: "**"
          dest: 'generated'
        ]
      staticDist:
        files: [
          expand: true
          cwd: "app/static"
          src: "**"
          dest: 'dist'
        ]
      webfontsDev:
        files: [{
          expand: true
          cwd: "vendor/webfonts/"
          src: "**"
          dest: "generated/<%= files.webfonts.root %>/"
        }
        {
          expand: true
          cwd: "vendor/components/font-awesome/fonts/"
          src: "**"
          dest: "generated/<%= files.webfonts.root %>/"
        }]

      webfontsDist:
        files: [{
          expand: true
          cwd: "vendor/webfonts/"
          src: "**"
          dest: "dist/<%= files.webfonts.root %>/"
        }
        {
          expand: true
          cwd: "vendor/components/font-awesome/fonts/"
          src: "**"
          dest: "dist/<%= files.webfonts.root %>/"
        }]
        
    cssmin:
      compress:
        files:
          "<%= files.css.minified %>": "<%= files.css.concatenated %>"

    jade:
      templates:
        options:
          client: true
        files:
          "<%= files.jade.generatedTemplate %>": "<%= files.jade.templates %>"
      pagesDev:
        options:
          pretty: true
          data:
            js: "<%= files.js.minifiedWebRelative %>" 
            jsVendor: "<%= files.js.minifiedVendorWebRelative %>"
            css: "<%= files.css.minifiedWebRelative %>"
            pkg: "<%= pkg %>"
        files: [{
          expand: true
          src: "<%= files.jade.pages %>"
          cwd: "<%= files.jade.pageRoot %>"
          dest: "generated/"
          ext: ".html"
        }]
      pagesDist:
        options:
          data:
            js: "<%= minifiedWebRelative %>"
            jsVendor: "<%= minifiedVendorWebRelative %>"
            css: "<%= files.css.minifiedWebRelative %>"
            pkg: "<%= pkg %>"
        files: [{
          expand: true
          src: "<%= files.jade.pages %>"
          cwd: "<%= files.jade.pageRoot %>"
          dest: "dist/"
          ext: ".html"
        }]

    jshint:
      files: ["<%= files.js.app %>"]
      options:
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
        
    less:
      options:
        paths: ["app/css", "vendor/css"]
      compile:
        files:
          "<%= files.less.generatedVendor %>": "<%= files.less.vendor %>"
          "<%= files.less.generatedApp %>": "<%= files.less.app %>"
          
    uglify:
      options:
        banner: '/*! <%= pkg.name %> <%= grunt.template.today("yyyy-mm-dd") %> */\n'
      js:
        files:
          "<%= files.js.minified %>": "<%= files.js.concatenated %>"
      jsVendor:
        files:
          "<%= files.js.minifiedVendor %>": "<%= files.js.concatenatedVendor %>"

    clean:
      bower:
        src: bowerDirectory grunt
      js:
        src: "<%= files.js.concatenated %>"

      css:
        src: "<%= files.css.concatenated %>"

      dist:
        src: ["dist", "generated"]

    watch:
      coffee:
        files: "<%= files.coffee.app %>"
        tasks: ["coffeelint", "coffee", "concat:js"]

      css:
        files: ["<%= files.css.vendor %>", "<%= files.css.app %>"]
        tasks: ["concat:css"]

      images:
        files: ["app/img/**/*.*", "vendor/img/**/*.*"]
        tasks: ["copy:imagesDev"]
        
      jadePages:
        files: ["<%= files.jade.pageRoot %>/<%= files.jade.pages %>","<%= files.jade.templates %>"]
        tasks: ["jade:pagesDev"]

      jadeTemplates:
        files: "<%= files.jade.templates %>"
        tasks: ["jade:templates", "concat:js"]

      js:
        files: ["<%= files.js.vendor %>", "<%= files.js.app %>"]
        tasks: ["concat:js"]

      less:
        files: [
          "<%= files.less.vendor %>"
          "<%= files.less.watch %>"
        ]
        tasks: ["less", "concat:css"]
  
      lint:
        files: "<%= files.js.app %>"
        tasks: ["jshint"]
  
      webfonts:
        files: ["vendor/webfonts/**/*.*", "vendor/components/font-awesome/fonts/**/*.*"]
        tasks: ["copy:webfontsDev"]
        
      livereload:
        options:
          livereload: true
        files: "dist/**/*.*"
        
  grunt.loadNpmTasks 'grunt-bower-task'
  grunt.loadNpmTasks 'grunt-coffeelint'
  grunt.loadNpmTasks 'grunt-contrib-clean'
  grunt.loadNpmTasks 'grunt-contrib-coffee'
  grunt.loadNpmTasks 'grunt-contrib-concat'
  grunt.loadNpmTasks 'grunt-contrib-connect'
  grunt.loadNpmTasks 'grunt-contrib-copy'
  grunt.loadNpmTasks 'grunt-contrib-cssmin'
  grunt.loadNpmTasks 'grunt-contrib-jshint'
  grunt.loadNpmTasks 'grunt-contrib-less'
  grunt.loadNpmTasks 'grunt-contrib-jade'
  grunt.loadNpmTasks 'grunt-contrib-uglify'
  grunt.loadNpmTasks 'grunt-contrib-watch'

  grunt.registerTask 'default', [
    'common'
    'dev'
  ]
  grunt.registerTask 'common', [
    'bower'
    'coffeelint'
    'jshint'
    'coffee'
    'less'
    'concat'
    'copy:staticDev'
    'copy:imagesDev'
    'copy:webfontsDev'
    'jade:templates'
    'jade:pagesDev'
  ]
  grunt.registerTask 'dev', [
    'connect'
    'watch'
  ]
  grunt.registerTask 'dist', [
    'uglify'
    'cssmin'
    'copy:staticDist'
    'copy:imagesDist'
    'copy:webfontsDist'
    'jade:pagesDist'
  ]

bowerDirectory = (grunt) ->
  bowerrc = path.join(process.cwd(), ".bowerrc")
  bowerConfig = grunt.file.readJSON(bowerrc) unless !fs.existsSync(bowerrc)
  bowerConfig?.directory || "vendor/components"