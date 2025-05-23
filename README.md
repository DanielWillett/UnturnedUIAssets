# UnturnedUIAssets
Collection of all the icons used by the vanilla UI in Unturned.

## Legal
I do not own rights to these icons, they are the intellectual property of Smartly Dressed Games. I've [received permission](https://github.com/DanielWillett/UnturnedUIAssets/issues/1#issue-3029522441) by SDG to host this repository and continue to update it. Content in this repository is subject to their [End User License Agreement](https://store.steampowered.com/eula/304930_eula_1).

## Plugin UI

The main point of this is the images in `UI/uGUI/DarkTheme/` which should be used for Unity UI's going for the Unturned style.<br/>
There is also a unity bundle at `UI/uGUI/DarkTheme/uGUI Assets.unitypackage` with vanilla UI prefabs.

# Using the vanilla UI components

Download this file: [uGUI Assets.unitypackage](https://github.com/DanielWillett/UnturnedUIAssets/blob/main/UI/uGUI/DarkTheme/uGUI%20Assets.unitypackage) and import it into a v2021.3.29f1 Unity Project.

Use the right click menu to add components under the Add -> UI -> uGUI section.

![image](https://github.com/DanielWillett/UnturnedUIAssets/assets/12886600/e5045cf5-78dd-4bd1-bb95-d1bc2b4cb473)


## Disableables
Disableables contain a State object which has an Activation hook on it. You can set the visibility of the state object and it will set the `interactable` property of the actual object (button, scroll handle, etc).

## Toggles
Use the plugin-controlled toggles if a plugin needs to know about the toggle, instead of if it's purely used for animations.<br/>
To listen for changes, listen for a button click on the root element.<br/>
To apply the changes or set an initial value, set the visibility of the `ToggleState` object. This is called the `CheckedState` on the disableable variant.

## Sliders
Vertical and horizontal sliders are available.<br/>
There is unfortunately no way to get the value of a slider from the server, but you can enable/disable them if desired by using the disableable variants.

## Scroll Views
Scroll views let you create a scroll box. There is a disableable variant available.<br/>
These are commonly used with a layout group (vertical or horizontal).<br/>

## Labels

### Label Contrast
The correct label to use depends on the context.<br/>
Usually, default labels are used when on a button or when they have a solid background. This is called `ETextContrastContext.Default` for vanilla UIs (or when shadowStyle is not set at all).<br/>
Shadow labels are used when the label may be harder to see. This is called `ETextContrastContext.InconspicuousBackdrop` for vanilla UIs.<br/>
Outline labels are used when the label is in front of the viewport or needs more contrast (in front of the map, for example). This is called `ETextContrastContext.ColorfulBackdrop` for vanilla UIs.<br/>
Tooltip labels are used for item tooltips and have the contrast `ETextContrastContext.Tooltip`.

### Font Sizes `ESleekFontSize`
* Default: 12
* Tiny: 8
* Small: 10
* Medium: 14
* Large: 20
* Title: 50

### Text Colors `ESleekTint` (default settings, dark theme)
RGB values are 0-1, not 0-255
* Background: `RGB(0.9, 0.9, 0.9)` (note that even though this is almost white, the box uses a dark background so it just slightly lightens it)
* Foreground: `RGB(0.9, 0.9, 0.9)` (same here)
* Font: `RGB(0.9, 0.9, 0.9)`
* Rich Text Default: `RGB(0.7, 0.7, 0.7)`
* Bad: `RGB(0.75, 0.12, 0.12)`

Rich Text Default should be used when you're going to be using rich text sometimes, like an item description. All labels in this pack default to `RGB(0.9, 0.9, 0.9)`

## Buttons
Right-clickable buttons are available which use a second button to listen for right clicks.<br/>
Listen for the root component for left clicks, and the `RightClickListener` button for right clicks.<br/>
There is a disableable right-clickable variant available.<br/>
For non-right-clickable buttons, both clicks trigger the on clicked event.

## Input Fields
Input trigger the on text committed event when someone presses enter or leaves the control. There is a disableable input field available as well.<br/>
Set the `Placeholder` text to change the placeholder (this also works at run-time).<br/>
Set the root element's text to set the actual value of the input field at run-time, **not the `Text` element**.

## Boxes
Boxes are just panels that have the classic box background from unturned. You can put text, images, etc on them.

## Vanilla Canvases
The canvases are available to mimic Unturned's scaling. They auto-scale with your UI scale setting in-game.

## Images
There are no differences between the Unity `Image` component and the Glazier image, so I did not include a custom image prefab.
