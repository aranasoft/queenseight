module.exports = (lineman) =>
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
  server:
    apiProxy:
      enabled: true
      host: 'localhost'
      port: 3000
  ###
