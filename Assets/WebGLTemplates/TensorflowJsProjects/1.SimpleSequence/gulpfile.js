var gulp = require('gulp'),
    ts = require('gulp-typescript'),
    watch = require('gulp-watch'),
    replace = require('gulp-replace');

var concat = require('gulp-concat');

var tsProject = ts.createProject('tsconfig.json');

gulp.task('default', function() {

  return watch('js/**.ts*', { ignoreInitial: false }, function () {
    gulp.src("js/**.ts*")
      .pipe(tsProject())
      .pipe(replace(/import .*/g, ''))
      .pipe(replace(/export .*/g, ''))
      .pipe(gulp.dest('../../../../Builds/WebGL/js/')
      .on('end', function() {
        console.log('Compiling done!');
      }));
  });
});
