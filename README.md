# Progress Bars with Different Styles In C# Console Programs

The project implements some progress bar base on C# console program.

## Usage

Directly refer the following code file to use the progress bar:  
*  `FollowerProgressBar.cs`
*  `NormProgressBar.cs`

### Style 1: Follower Progress Bar
In this style there are two enumeration value to choose different sub-styles. They are:   
* Head
* Rear

### Style 2: Norm Progress Bar
In this style there are three enumeration values to choose different sub-styles. They are:   
* CharacterFill
* CharacterReplace
* ColorBlock

## Configuration

You can change the progress bar by setting different properties as follows:

```csharp
// Set the shape for the progress bar.
var pb = new NormProgressBar(NormProgressBarStyle.CharacterReplace);
pb.ReplacedProgressBackgroundChar = '=';
pb.FilledProgressBodyChar = 'O';
pb.FilledProgressHeaderChar = 'O';

// Set the color for the progress bar.
var pb = new NormProgressBar(NormProgressBarStyle.ColorBlock);
pb.Background = ConsoleColor.DarkYellow;
pb.BlockColor = ConsoleColor.Magenta; 
```

## Examples

### FollowerProgressBar.Head

![Ex1][Ex1]

![Ex2][Ex2]

### FollowerProgressBar.Rear

![Ex3][Ex3]

![Ex4][Ex4]

### NormProgressBar.CharacterFill

![Ex5][Ex5]

![Ex6][Ex6]

### NormProgressBar.CharacterReplace


![Ex7][Ex7]

![Ex8][Ex8]

### NormProgressBar.ColorBlock

![Ex9][Ex9]

![Ex10][Ex10]


[Ex1]: ./images/follower_head_01.png
[Ex2]: ./images/follower_head_02.png
[Ex3]: ./images/follower_rear_01.png
[Ex4]: ./images/follower_rear_02.png
[Ex5]: ./images/norm_charfill_01.png
[Ex6]: ./images/norm_charfill_02.png
[Ex7]: ./images/norm_charreplace_01.png
[Ex8]: ./images/norm_charreplace_02.png
[Ex9]: ./images/norm_colorblock_01.png
[Ex10]: ./images/norm_colorblock_02.png
