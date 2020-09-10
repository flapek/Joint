#!/bin/bash
echo "TRAVIS_COMMIT:$TRAVIS_COMMIT"
echo "TRAVIS_COMMIT_MESSAGE:$TRAVIS_COMMIT_MESSAGE,,"

if [ "$TRAVIS_COMMIT_MESSAGE" = "pack" ]; then
  echo Start publishing NuGet packages.
  case "$TRAVIS_BRANCH" in
    "master")
    for dir in src/*/
    do
        dir=${dir%*/}
        echo Publishing NuGet package:  ${dir##*/}
        exec ./$dir/Scripts/dotnet-pack.sh &
        wait
    done
    ;;
  esac
  echo Finished publishing NuGet packages.
fi