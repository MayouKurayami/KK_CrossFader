## FOR DEVELOPERS  

### Preprocessor Directives  
- **DEBUG_FIX**: Define this to enable the workaround that prevents the non-VR game from crashing when using the modified mono.dll for debugging in dnSpy.  

### Build Configurations
The three build configurations you'd see in Visual Studio are:  
- **Debug(debug-fix)**: The default VS debug configuration with DEBUG_FIX defined. Use this if you have dnSpy debugging enabled.
- **Release(debug-fix)**: The default VS release configuration with DEBUG_FIX defined. Use this if you have dnSpy debugging enabled.
- **Release(no-debug-fix)**: The default VS release configuration with DEBUG_FIX undefined. This configuration is recommended for the general release for users.
