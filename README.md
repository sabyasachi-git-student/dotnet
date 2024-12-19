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

Extended Description

The WYSIWYG Configurator Application is a powerful tool designed to make UI design and configuration simpler and more interactive. It provides users with a drag-and-drop interface for adding visual components like Rectangles, Ellipses, and TextBlocks to a design area and offers a dynamic property editor for customizing their attributes. The application has been built with scalability and user-friendliness in mind, ensuring an intuitive experience for developers and designers alike.

Key Features
Drag-and-Drop Functionality

Drag elements (Rectangle, Ellipse, TextBlock) from the Toolbox and drop them into the Design Area.
Smooth mouse interactions and precise position handling.
Dynamic Property Editor

Adjust properties such as:
Dimensions: Width and Height.
Colors: Change the fill color for Rectangles and Ellipses or the text color for TextBlocks.
Opacity: Modify transparency for all elements.
Text and Font Size: Update content and size for TextBlocks.
Real-time updates reflect changes immediately on the selected elements.
Selection Overlay

A visual overlay highlights the selected element in the Design Area, making it clear which object is being edited.
The overlay dynamically resizes based on the dimensions of the selected object.
Error Handling

User-friendly error messages for invalid inputs (e.g., incorrect color values, out-of-range opacity).
Extensibility

Modular design enables:
Adding new UI components to the Toolbox.
Extending the Property Editor with additional customizable attributes.
Exporting designs to external formats (future improvement).
Improved User Interaction

Snapping and alignment support for better element arrangement.
Context-sensitive property display in the Property Editor.
Technical Highlights
Clean and Modular Codebase

Classes: MainWindow, ToolboxContainer, DesignAreaContainer, and PropertyContainer ensure code maintainability and scalability.
Rich Interaction Handling

Implements events like MouseLeftButtonDown, DragEnter, and Drop for seamless interactions.
Robust Exception Handling

Prevents crashes from invalid inputs.
Usage Scenarios
UI Design Prototyping: Quickly mock up UI layouts with dynamic property adjustment.
Teaching and Learning Tool: Learn WPF concepts such as drag-and-drop, property binding, and real-time updates.
Extensibility Projects: Serve as a base for more advanced WYSIWYG editors with export and import features.
Future Improvements
Enhanced Element Support

Add support for new components like Buttons, Images, and custom shapes.
Export Options

Enable export of the designed layout as XML or JSON.
Alignment Tools

Add snapping and alignment guides for precision.
Undo/Redo Functionality

Allow users to revert or redo changes during the design process.
Conclusion
This project provides a foundation for further extensibility and customization. Developers can leverage its clean architecture to implement additional features, making it a versatile tool for UI design and teaching.
