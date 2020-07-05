**DISCLAIMER**: This project is an effort to decompile and fix an existing plugin that was not written by me.  
The original plugin can be found on http://uppervolta-3d.net/illu/upload.php

# CrossFader
> Replaces fades between animations in Koikatsu with animated transitions


## Prerequisites  
- Afterschool DLC  
- Darkness DLC
- BepInEx 5  


## Installation
Download [**CrossFader.zip** from the latest release](https://github.com/MayouKurayami/KK_CrossFader/releases) then extract it into your game directory (where the game exe and BepInEx folder are located). Replace old files if asked. <br>
*CrossFader.dll* should end up in BepInEx\plugins directory.

## Configurations  
>Press F1 when not in VR to access the plugin settings at the upper right of the screen.  

![](https://github.com/MayouKurayami/KK_CrossFader/blob/master/images/Crossfader_settings.png)  

- **Enabled** - Choose if and when to enable the plugin. For instance, choose *VR Only* if you only want it enabled in official VR. **(Default: On)**  

- **Debugger Crash Workaround (DebugFix)** - When not in official VR, disables crossfade in 3P intercourse to prevent potential game crash. See [*Notes & Limitations*](https://github.com/MayouKurayami/KK_CrossFader#notes--limitations) for more details. **(Default: True)**



## Notes & Limitations
- Since the vanilla game has no fades at all between animations in VR, this plugin is extremely useful to prevent sudden and jerky transitions when in VR.  

- Animation fades are disabled by default during sex in 3P mode with two females when *NOT in Official VR* as a workaround to prevent the game from crashing when running with the modified mono.dll for dnSpy debugging **(Official VR is not affected by this workaround)**. <br>
If you'd like to disable this workaround outside of VR, disable the **Debugger Crash Workaround** option. <br>
Please note that this is generally not recommended, as if your game starts crashing after changing this config you'd have to manually change it back to *true* in *BepInEx\config\bero.crossfader.cfg*



## Credits
All credit of the plugin up to version 0.1 goes to the unknown developer who made this plugin.  
