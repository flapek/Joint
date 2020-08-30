#!/bin/bash
echo Executing after success scripts on branch $TRAVIS_BRANCH
echo Triggering Nuget package build

cd src/Joint.Logging/src
dotnet pack -c release -o Packages /p:PackageVersion=0.0.$TRAVIS_BUILD_NUMBER --no-restore

echo Uploading Joint package to Nuget using branch $TRAVIS_BRANCH

dotnet nuget push Packages/*.nupkg -k $TRAVIS_NUGET_API_KEY -s https://api.nuget.org/v3/index.json
