# Building Assembly

From Peasy Project Dir:

`dotnet build --configuration Release`

# Building Package

Update Peasy.nuspec and Peasy.csproj version info, release notes, etc.

From Peasy Project Dir:

 `dotnet pack -c=Release`

NOTE: This seems to cache packages by version number.  Be sure to modify the version number in Peasy.nuspec and the `VersionPrefix` in Peasy.csproj to see updated changes in the Peasy assembly.

# Testing Package Locally

From Testing Project Dir (consuming client dir):

`dotnet add package peasy --source=/PathToLocalPeasyProject/bin/Release`

# Deploying Package

`nuget SetApiKey Your-API-Key`

From bin/release dir:

 `nuget push MyPackage.nupkg` (will push both .nupkg and .snupkg)


