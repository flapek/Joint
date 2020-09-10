#!/bin/bash
case "$TRAVIS_BRANCH" in
  "master")
  echo Start publishing NuGet packages.
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