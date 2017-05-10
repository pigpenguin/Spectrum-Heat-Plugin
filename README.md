Heat Info Plugin
===

This is a plugin for the [Spectrum](https://github.com/Ciastex/Spectrum) modification framework for [Distance](http://survivethedistance.com/). It displays the cars heat and it's speed.

Installation
---

Get the .dll file from the releases page and drop it into spectrums plugins folder. Running distance will cause the plugin to create it's default settings file.

Settings
---

+ units: the units to display the cars velocity. Acceptable values mph and kph
+ display: where to render the information. Acceptable values hud, which will cause the information to render where stunt info is rendered, and watermark, which will cause the text to render where the version number is.
+ activation: when to display the information. Acceptable values always, warning, and toggle. Always will always render the information, warning will only render when above a certain heat threshold, and toggle will use a hotkey to toggle the information on and off.
+ warningThreshold: when to trigger the information if using warning mode. Acceptable values any number between 0 and 1.
+ toggleHotkey: what hotkey to use when using toggle mode.
