module.exports = (lineman) =>
  js:
    vendor: [
      "vendor/components/jquery/jquery.js"
      "vendor/components/bootstrap/docs/assets/js/bootstrap.js"
      "vendor/components/underscore/underscore.js"
      "vendor/components/angular/angular.js"
      "vendor/js/**/*.js"
    ]
    concatenatedVendor: "generated/js/vendor.js"
    minifiedVendor: "dist/js/vendor.js"

  less:
    app: [
      "app/css/app.less"
    ]
    watch: [
      "app/css/**/*.less"
    ]

  webfonts:
    root: "font"