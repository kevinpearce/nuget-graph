# nuget-graph

## Setup

1. Steps to install from local build

```shell
dotnet pack
dotnet tool install --global --add-source ./nupkg cli-poc
```

1. Steps required to update install

```shell
dotnet pack
dotnet tool update --global --add-source ./nupkg cli-poc
```

1. Steps required to uninstall

```shell
dotnet tool uninstall cli-poc --global
```