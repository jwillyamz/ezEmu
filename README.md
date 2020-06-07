# ezEmu

ezEmu enables users to test adversary behaviors via various execution techniques. Sort of like an "*offensive framework for blue teamers*", ezEmu does not have any networking/C2 capabilities and rather focuses on creating local test telemetry.

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
- *Microsoft Word Macro (T1059.005)*

**Note:** You need to enable some macro related trust center settings for the Word stuffz to work - https://support.office.com/en-us/article/enable-or-disable-macros-in-office-files-12b036fd-d140-4e74-b45e-16fed1a7e5c6

## Usage/Demo

ezEmu is an interactive terminal application and works much better if you run from cmd.exe

![ezEmu Demo](ezEmu.gif)

Compile with reference to a few local DLL dependencies

(ex: `csc /r:Microsoft.Office.Interop.Word.dll,Microsoft.Vbe.Interop.dll,System.Management.Automation.dll parent.cs`)

## Feedback/Contribute

This started as just simple personal research/putzing and is definitely not intended to be "clean code" (this is very much Jamie-code™️). That said, I am happy to accept issues and further suggestions!

**TODO:** Log output file (*perhaps*), more CTI + learning >> more execution techniques (**always**)

____


### Notice 

Copyright 2020 The MITRE Corporation

Approved for Public Release; Distribution Unlimited. Case Number 20-1357.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

   http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

The author's affiliation with The MITRE Corporation is provided for identification purposes only, and is not intended to convey or imply MITRE's concurrence with, or support for, the positions, opinions or view points expressed by the author.
