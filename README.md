# SmartBrush

Custom brush which automatically selects a tilemap with the same name as the picked tile.

Tested on Unity 2018.3.5f1

## Installation
1. This package requires the scripting runtime version to be set to `.NET 4.x Equivalent`, you can change this setting in `Project Settings > Player > Other Settings > Scripting Runtime Version`. The `.NET 4.x Equivalent` scripting runtime version is the default in Unity 2018.3 and higher.

2. Clone this repository and use the Package Manager `Window > Package Manager` to add the package.json located in the root of this repository.

## Usage

SmartBrush tries to find a tilemap in the `GetCurrentStageHandle` with the same name as the selected tile.

> For example: Selecting a tile with the name "Platform" will automatically switch the active tilemap to "Platform" if gameobject with a tilemap component by that name exists.

1. Select SmartBrush in the Tile Palette brush dropdown
2. Pick a tile from the tile palette
3. Paint!

## Possible improvements

-   Add support for mapping tiles to specific tilemap names so for example, ["Platform", "Ground"] would both switch the active tilemap to "Platform".
