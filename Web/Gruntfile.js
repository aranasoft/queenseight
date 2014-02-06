var timer = require("grunt-timer");

/*global module:false*/
module.exports = function(grunt) {
  require('./config/lineman').config.grunt.run(grunt);
  timer.init(grunt);
};
