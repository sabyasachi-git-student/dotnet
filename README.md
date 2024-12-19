# dotnet
# WYSIWYG Configurator Application

## Description
The WYSIWYG Configurator is a desktop application built using WPF (.NET Core). 
It allows users to drag-and-drop UI components (Rectangle, Ellipse, TextBlock) 
into a design area and dynamically adjust their properties (Width, Height, Color, Opacity, etc.) 
through an interactive property editor.

## Features
- Drag-and-drop functionality for UI components (Rectangle, Ellipse, TextBlock).
- Dynamic property editing:
  - Change dimensions (Width, Height).
  - Modify color and opacity.
  - Edit text and font size for TextBlock.
- Visual selection overlay to highlight selected elements.
- Clean and user-friendly UI.

## Technologies Used
- **C# (.NET Core 6)**: Backend logic and WPF UI design.
- **WPF (Windows Presentation Foundation)**: User interface framework.
- **Visual Studio 2022**: Development environment.

## How to Run the Project
1. **Prerequisites**:
   - Install [Visual Studio 2022](https://visualstudio.microsoft.com/).
   - Install .NET Core SDK (version 6.0 or higher).

2. **Clone the Repository**:
   ```bash   
   git clone https://github.com/YourUsername/WYSIWYGConfigurator.git
   cd WYSIWYGConfigurator
3.Build and Run:

Open the solution WYSIWYGConfigurator.sln in Visual Studio.
Set the build configuration to Release.
Press F5 or click "Start" to run the project.

4.Generate the Executable:
Go to Build > Publish to create an EXE file.
The executable will be available in the bin/Release folder.
