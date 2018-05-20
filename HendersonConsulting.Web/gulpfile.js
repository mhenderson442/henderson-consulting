/// <binding AfterBuild='default, less' />
var gulp = require('gulp');
var less = require('gulp-less');
var path = require('path');

var wwwroot = {
    js: ['scripts/**/*.js', 'scripts/**/*.ts', 'scripts/**/*.map']
}

gulp.task('default', function () {
    // place code for your default task here

    return gulp.src('./Scripts/**/*.js')
        .pipe(gulp.dest('./wwwroot/js'));
});

gulp.task('less', function () {
    return gulp.src('./Less/**/*.less')
        .pipe(less({
            paths: [path.join(__dirname, 'less', 'includes')]
        }))
        .pipe(gulp.dest('./wwwroot/css'));
});