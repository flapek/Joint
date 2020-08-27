#!/bin/bash
dotnet pack -c Release -o Packages --version-suffix 0.0.$TRAVIS_BUILD_NUMBER