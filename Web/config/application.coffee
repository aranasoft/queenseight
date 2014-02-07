module.exports = (lineman) =>
  concat_sourcemap:
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
  jade:
    pagesDev:
      options:
        data:
          jsVendor: "js/vendor.js"
    pagesDist:
      options:
        data:
          jsVendor: "js/vendor.js"
  watch:
    less:
      files: [
        "<%= files.less.vendor %>"
        "<%= files.less.app %>"
        "<%= files.less.watch %>"
      ]
  webfonts:
    files:
      "vendor/components/font-awesome/fonts/": "vendor/components/font-awesome/fonts/**/*.*"
  ###
  server:
    apiProxy:
      enabled: true
      host: 'localhost'
      port: 3000
  ###
