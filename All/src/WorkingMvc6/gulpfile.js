var gulp = require('gulp');
var project = require('./project.json');

gulp.task('default', function () {
    console.log(project.version);
    console.dir(process.env);
});