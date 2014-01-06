module.exports = (lineman) =>
  loadNpmTasks: ["grunt-bower-task"]

  prependTasks:
    common: ["bower:install"]

  bower:
    install:
      options:
        copy: false	

  concat:
    js:
      src: ["<%= files.template.generated %>", "<%= files.coffee.generated %>", "<%= files.js.app %>"]
      dest: "<%= files.js.concatenated %>"
      options:
        banner: "<%= meta.banner %>"
    jsVendor:
      src: ["<%= files.js.vendor %>"]
      dest: "<%= files.js.concatenatedVendor %>"

  uglify:
    jsVendor:
      files:
        "<%= files.js.minifiedVendor %>": "<%= files.js.concatenatedVendor %>"
  pages:
    dev:
      context:
        jsVendor: "js/vendor.js"
    dist:
      context:
        jsVendor: "js/vendor.js"
  watch:
    less:
      files: [
        "<%= files.less.vendor %>"
        "<%= files.less.app %>"
        "<%= files.less.watch %>"
      ]
      tasks: ["less", "concat:css"]
  webfonts:
    files:
      "vendor/components/font-awesome/font/": "vendor/components/font-awesome/font/**/*.*"
  ###
  server:
    apiProxy:
      enabled: true
      host: 'localhost'
      port: 3000
  ###
