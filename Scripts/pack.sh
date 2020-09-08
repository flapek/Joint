#!/bin/bash
if [ "$TRAVIS_COMMIT_MESSAGE" = "ignore" ]; then
  case "$TRAVIS_BRANCH" in
    "master")
    for dir in src/*/
    do
        dir=${dir%*/}
        echo Publishing NuGet package:  ${dir##*/}
        exec ./$dir/Scripts/dotnet-pack.sh &
        wait
    done
  
    echo Finished publishing NuGet packages.
    ;;
  esac
fi