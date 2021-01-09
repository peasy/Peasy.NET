
# Building Package

Update Peasy.nuspec and Peasy.csproj version info, release notes, etc.

From Peasy Project Dir:

 `dotnet pack -c=Release`

# Testing Package Locally

From Testing Project Dir (consuming client dir):

`dotnet add package peasy --source=/PathToLocalPeasyProject/bin/Release/Peasy.VERSION_NUMBER.nupkg`

# Deploying Package

`nuget SetApiKey Your-API-Key`

From bin/release dir:

 `nuget push MyPackage.nupkg` (will push both .nupkg and .snupkg)


