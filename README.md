Heat Info Plugin
===

This is a plugin for the [Spectrum](https://github.com/Ciastex/Spectrum) modification framework for [Distance](http://survivethedistance.com/). It displays the cars heat and it's speed.

Installation
---

Get the .dll file from the releases page and drop it into spectrums plugins folder. Running distance will cause the plugin to create it's default settings file.

Settings
---
<dl>
<dt>units</dt>
<dd>the units to display the cars velocity. Acceptable values automatic, mph, and kph. Automatic is the default setting and will pick whichever speed matches your distance settings. This is probably the value you want to keep it on but you do you.</dd>

<dt>display</dt> <dd>where to render the information. Acceptable values hud, which will cause the information to render where stunt info is rendered, and watermark, which will cause the text to render where the version number is.</dd>

<dt>activation</dt> <dd>when to display the information. Acceptable values always, warning, and toggle. Always will always render the information, warning will only render when above a certain heat threshold, and toggle will use a hotkey to toggle the information on and off.</dd>

<dt>warningThreshold</dt> <dd>when to trigger the information if using warning mode. Acceptable values any number between 0 and 1.</dd>

<dt>toggleHotkey</dt> <dd>what hotkey to use when using toggle mode.</dd>
</dl>
