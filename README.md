# ezEmu

ezEmu enables users to test adversary behaviors via various execution techniques. Sort of like a "*offensive framework for blue teamers*", ezEmu does not have any networking/C2 capabilities and rather focuses on creating local test telemetry.

ezEmu is compiled as `parent.exe` to simplify process trees, and will track (and also kill) child processes to enable easy searches in logs/dashboards.

Current execution techniques include:

- *Cmd.exe (T1059.003)*
- *PowerShell (T1059.001)*
- *Unmanaged PowerShell (T1059.001)*
- *CreateProcess() API (T1106)*
- *WinExec() API (T1106)*
- *ShellExecute (T1106)*
- *Windows Management Instrumentation (T1047)*
- *Windows Script Host (T1059.005)*
- *Windows Fiber*
- *WMIC XSL Script/Squiblytwo (T1220)*

## Usage/Demo

ezEmu is an interactive terminal application and works much better if you run from cmd.exe

![ezEmu Demo](ezEmu.gif)

Compile with reference to a local PowerShell DLL 
(ex: `csc /reference:system.management.automation.dll parent.cs`)

## Feedback/Contribute

This started just simple personal research/putzing and is definitely not intended to be "clean code" (this is very much Jamie-code™️). That said, I am happy to accept issues and further suggestions!

**TODO:** Log output file, more CTI + learning >> more execution techniques 

____



<!--©2020 The MITRE Corporation. ALL RIGHTS RESERVED. Approved for public release. Distribution unlimited 20--1357. The author's affiliation with The MITRE Corporation is provided for identification purposes only, and is not intended to convey or imply MITRE's concurrence with, or support for, the positions, opinions or view points expressed by the author.-->
