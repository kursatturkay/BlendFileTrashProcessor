# Unity Blend File Auto Cleaner (Unity 6000+)

This script automatically removes Blender backup files (.blend1, .blend2, etc.) from your Unity project as soon as they are imported. It keeps your project clean by moving these unnecessary backup files to trash automatically.

## Features

- **Automatic Cleanup**: Immediately moves .blend1, .blend2, and other Blender backup files to trash when added to project
- **Startup Check**: Cleans existing blend backup files when Unity starts
- **Non-Invasive**: Preserves original .blend files, only removes numbered backups
- **No Manual Intervention**: Works automatically in the background

## Installation

1. Save the script as `BlendFileTrashProcessor.cs`
2. Place it in your project's `Editor` folder (create if it doesn't exist)
  - Example path: `Assets/Editor/BlendFileTrashProcessor.cs`
3. Unity will automatically compile and activate the script

## How it Works

The script uses Unity's `AssetPostprocessor` to monitor new file imports. When a Blender backup file (ending with .blend followed by numbers) is detected, it's automatically moved to trash.

### What Gets Removed:
- .blend1
- .blend2
- .blend3
- .blend4
- .blend5
- Any .blend file with numbers at the end

### What Stays:
- .blend (original Blender files remain untouched)

## Requirements

- Unity 6000 or later
- Editor-only script (place in Editor folder)

## License

Free to use for any purpose.
