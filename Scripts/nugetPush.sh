#!/bin/bash
if $TRAVIS_BRANCH = master then
    dotnet nuget push Packages/SCL.*.nupkg --api-key $TRAVIS_NUGET_API_KEY --source https://api.nuget.org/v3/index.json
fi