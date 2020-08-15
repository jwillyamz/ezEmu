using System;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using Microsoft.Office.Interop.Word;

public struct PROCESS_INFORMATION {
	public IntPtr hProcess;
	public IntPtr hThread;
	public uint dwProcessId;
	public uint dwThreadId;
}//end struct
public struct STARTUPINFO {
	public uint cb;
	public string lpReserved;
	public string lpDesktop;
	public string lpTitle;
	public uint dwX;
	public uint dwY;
	public uint dwXSize;
	public uint dwYSize;
	public uint dwXCountChars;
	public uint dwYCountChars;
	public uint dwFillAttribute;
	public uint dwFlags;
	public short wShowWindow;
	public short cbReserved2;
	public IntPtr lpReserved2;
	public IntPtr hStdInput;
	public IntPtr hStdOutput;
	public IntPtr hStdError;
}//end struct
public struct SECURITY_ATTRIBUTES {
	public int length;
	public IntPtr lpSecurityDescriptor;
	public bool bInheritHandle;
}//end struct
class Parent {
	static void Main() {
		bool exit = false;
		while (exit == false) {
			Console.Clear();
			Console.WriteLine("\n===== y emu hard? =====\n");
			Console.Write("\t[1] cmd.exe /c (T1059.003)\n\t[2] powershell - c (T1059.001)\n\t[3] Unmanaged PowerShell aka PS w/o PowerShell.exe (T1059.001)\n\t[4] CreateProcess() API (T1106)\n\t[5] WinExec() API (T1106)\n\t[6] ShellExecute (T1106)\n\t[7] Windows Management Instrumentation (T1047)\n\t[8] VBScript (T1059.005)\n\t[9] Windows Fiber (research-based)\n\t[10] WMIC XSL Script/Squiblytwo (T1220)\n\t[11] Microsoft Word VBA Macro (T1059.005)\n\t[12] Python (T1059.006)\n\nSelect an execution procedure (or exit): ");
			string exec = Console.ReadLine().ToLower();
			switch (exec) {
				case "1":
					bool cmd = true;
					while (cmd) {
						Console.Clear();
						Console.WriteLine("\n===== cmd.exe /c execution =====\n");
						Console.Write("cmd.exe /c [magic]? <<< please provide magic (or back): ");
						string command = Console.ReadLine().ToLower();
						if (command == "back") {
							Console.Clear();
							Console.WriteLine("===== \"So the pie isn't perfect? Cut it into wedges. Stay in control, and never panic.\" --Martha Stewart =====");
							Thread.Sleep(3000);
							cmd = false;
						}//end if
						else {
							Console.Write("\nAre you sure you want to execute:\n\tcmd.exe /c " + command + "\n\n[y/n/q]? ");
							string confirm = Console.ReadLine().ToLower();
							switch (confirm) {
								case "y":
									Console.Clear();
									Console.WriteLine("Executing cmd.exe /c " + command + "\n");
									cliExec("cmd", command);
									Console.Write("\nPress enter to continue ");
									Console.ReadLine();
									break;
								case "n":
									break;
								case "q":
									Console.Clear();
									Console.WriteLine("===== No one likes a quiter... =====");
									Thread.Sleep(3000);
									cmd = false;
									break;
								default:
									Console.WriteLine("\nMight want to rethink that last one...");
									Thread.Sleep(3000);
									break;
							}//end swtich
						}//end else
					}//end while
					break;
				case "2":
					bool ps = true;
					while (ps) {
						Console.Clear();
						Console.WriteLine("\n===== powershell.exe -c execution =====\n");
						Console.Write("powershell.exe -c [sauce]? <<< please provide sauce (or back): ");
						string command = Console.ReadLine().ToLower();
						if (command == "back") {
							Console.Clear();
							Console.WriteLine("===== \"People say nothing is impossible, but I do nothing every day.\" --Winnie the Pooh =====");
							Thread.Sleep(3000);
							ps = false;
						}//end if
						else {
							Console.Write("\nAre you sure you want to execute:\n\tpowershell.exe -c " + command + "\n\n[y/n/q]? ");
							string confirm = Console.ReadLine().ToLower();
							switch (confirm) {
								case "y":
									Console.Clear();
									Console.WriteLine("Executing powershell.exe -c " + command + "\n");
									cliExec("powershell", command);
									Console.Write("\nPress enter to continue ");
									Console.ReadLine();
									break;
								case "n":
									break;
								case "q":
									Console.Clear();
									Console.WriteLine("===== There is no try, only quit... =====");
									Thread.Sleep(3000);
									ps = false;
									break;
								default:
									Console.WriteLine("\nDon't be weird...");
									Thread.Sleep(3000);
									break;
							}//end swtich
						}//end else
					}//end while
					break;
				case "3":
					bool noPs = true;
					while (noPs) {
						Console.Clear();
						Console.WriteLine("\n===== Unmanaged PowerShell execution =====\n");
						Console.Write("\"powershell.exe -c\" [oomph]? <<< but not really,\n\twarning: commands that include CLIs with no args such as just \"cmd\" or \"powershell\" may hang\n\tplease provice oomph (or back): ");
						string command = Console.ReadLine().ToLower();
						if (command == "back") {
							Console.Clear();
							Console.WriteLine("===== \"They say stay in the lines, but there's always something better on the other side.\" --John Mayer =====");
							Thread.Sleep(3000);
							noPs = false;
						}//end if
						else {
							Console.Write("\nAre you sure you want to execute \"" + command + "\" using Unmanaged PowerShell\n\n[y/n/q]? ");
							string confirm = Console.ReadLine().ToLower();
							switch (confirm) {
								case "y":
									Console.Clear();
									Process currentProcess = Process.GetCurrentProcess();
									Console.WriteLine("Executing \"" + command + "\" using Unmanaged PowerShell\n");
									//thank you https://github.com/Ben0xA/AwesomerShell
									Runspace rs = RunspaceFactory.CreateRunspace();
									rs.Open();
									PowerShell power = PowerShell.Create();
									power.Runspace = rs;
									power.AddScript(command);
									Collection<PSObject> output = power.Invoke();
									Console.WriteLine("PS \"" + command + "\"" + " executed within " + currentProcess.Id + " at " + DateTime.Now.ToString("HH:mm:ss tt") + "\n");
									Console.WriteLine("\n==== Output/Error(s) =====\n");
									if (output != null) {
										foreach (PSObject rtnItem in output) {
											Console.WriteLine(rtnItem.ToString());
										}//end foreach
									}//end if
									trackANDkill((int) currentProcess.Id);
									rs.Close();
									Console.Write("\nPress enter to continue ");
									Console.ReadLine();
									break;
								case "n":
									break;
								case "q":
									Console.Clear();
									Console.WriteLine("===== Quiting is not giving up... =====");
									Thread.Sleep(3000);
									noPs = false;
									break;
								default:
									Console.WriteLine("\nThat's a paddlin...");
									Thread.Sleep(3000);
									break;
							}//end swtich
						}//end else
					}//end while
					break;
				case "4":
					bool cp = true;
					while (cp) {
						Console.Clear();
						Console.WriteLine("\n===== CreateProcess() API execution =====\n");
						Console.Write("API needs an application with full path and args (ex: C:\\Windows\\System32\\cmd.exe /c calc)\n\tplease oblige (or back): ");
						string command = Console.ReadLine().ToLower();
						if (command == "back") {
							Console.Clear();
							Console.WriteLine("===== \"There's no such thing as perfect...Even with your imperfections, you can do anything.\" --Bathtub Barracuda =====");
							Thread.Sleep(3000);
							cp = false;
						}//end if
						else {
							string[] full = command.Split(' ');
							string app = "";
							string param = "";
							int count = 0;
							foreach (string i in full) {
								if (count == 0)
									app = i;
								else if (count == 1)
									param = i;
								else
									param += " " + i;
								count++;
							}//end foreach
							Console.Write("\nAre you sure you want to execute:\n\t" + app + " with parameters \"" + param + "\" using CreateProcess()\n\n[y/n/q]? ");
							string confirm = Console.ReadLine().ToLower();
							switch (confirm) {
								case "y":
									Console.Clear();
									STARTUPINFO si = new STARTUPINFO();
									PROCESS_INFORMATION pi = new PROCESS_INFORMATION();
									Console.WriteLine("Executing " + app + " with params \"" + param + "\" using CreateProcess()\n");
									CreateProcess(app, param, IntPtr.Zero, IntPtr.Zero, false, 0, IntPtr.Zero, null, ref si, out pi);
									Console.WriteLine(app + " started as PID " + pi.dwProcessId + " at " + DateTime.Now.ToString("HH:mm:ss tt") + "\n");
									Console.WriteLine("\n==== Output/Error(s) =====\n");
									try {
										trackANDkill((int) pi.dwProcessId);
									}//end try
									catch {
										Console.WriteLine("\t Process died too fast to fully index");
									}//end catch
									Console.Write("\nPress enter to continue ");
									Console.ReadLine();
									break;
								case "n":
									break;
								case "q":
									Console.Clear();
									Console.WriteLine("===== Jimmy Buffett would be so disappointed... =====");
									Thread.Sleep(3000);
									cp = false;
									break;
								default:
									Console.WriteLine("\nY tho...");
									Thread.Sleep(3000);
									break;
							}//end swtich
						}//end else
					}//end while
					break;
				case "5":
					bool winexec = true;
					while (winexec) {
						Console.Clear();
						Console.WriteLine("\n===== WinExec() API execution =====\n");
						Console.Write("API takes ANY command (exe + parameters), please give us direction/meaning/purpose (or back): ");
						string command = Console.ReadLine().ToLower();
						if (command == "back") {
							Console.Clear();
							Console.WriteLine("===== \"He who has a why to live can bear almost any how.\" --Friedrich Nietzsche =====");
							Thread.Sleep(3000);
							winexec = false;
						}//end if
						else {
							Console.Write("\nAre you sure you want to execute " + command + " using WinExec()\n\n[y/n/q]? ");
							string confirm = Console.ReadLine().ToLower();
							switch (confirm) {
								case "y":
									Console.Clear();
									Process currentProcess = Process.GetCurrentProcess();
									Console.WriteLine("Executing " + command + " using WinExec() at " + DateTime.Now.ToString("HH:mm:ss tt") + "\n");
									Console.WriteLine("\n==== Output/Error(s) =====\n");
									WinExec(command, 1);
									Thread.Sleep(2000);
									Console.WriteLine();
									trackANDkill((int) currentProcess.Id);
									Console.Write("\nPress enter to continue ");
									Console.ReadLine();
									break;
								case "n":
									break;
								case "q":
									Console.Clear();
									Console.WriteLine("===== At least you're not too legit... =====");
									Thread.Sleep(3000);
									winexec = false;
									break;
								default:
									Console.WriteLine("\nNow that's just rude...");
									Thread.Sleep(3000);
									break;
							}//end swtich
						}//end else
					}//end while
					break;
				case "6":
					bool se = true;
					while (se) {
						Console.Clear();
						Console.WriteLine("\n===== ShellExecute execution =====\n");
						Console.Write("ShellExecute needs an application (an exe somewhere) and args\n\tplease oblige (or back): ");
						string command = Console.ReadLine().ToLower();
						if (command == "back") {
							Console.Clear();
							Console.WriteLine("===== \"They misunderestimated me.\" --George W. Bush =====");
							Thread.Sleep(3000);
							se = false;
						}//end if
						else {
							string[] full = command.Split(' ');
							string app = "";
							string param = "";
							int count = 0;
							foreach (string i in full) {
								if (count == 0)
									app = i;
								else if (count == 1)
									param = i;
								else
									param += " " + i;
								count++;
							}//end foreach
							Console.Write("\nAre you sure you want to execute " + app + " with params \"" + param + "\" using ShellExecute\n\n[y/n/q]? ");
							string confirm = Console.ReadLine().ToLower();
							switch (confirm) {
								case "y":
									Console.Clear();
									Process process = new Process();
									Console.WriteLine("Executing " + app + " with params \"" + param + "\" using ShellExecute\n");
									process.StartInfo.FileName = app;
									process.StartInfo.Arguments = param;
									process.StartInfo.RedirectStandardOutput = false;
									process.StartInfo.RedirectStandardError = false;
									process.StartInfo.UseShellExecute = true;
									process.Start();
									Console.WriteLine(process.ProcessName + " started at " + process.StartTime + " as PID " + process.Id);
									trackANDkill((int) process.Id);
									Console.Write("\nPress enter to continue ");
									Console.ReadLine();
									break;
								case "n":
									break;
								case "q":
									Console.Clear();
									Console.WriteLine("===== Boo... =====");
									Thread.Sleep(3000);
									se = false;
									break;
								default:
									Console.WriteLine("\nI thought we were friends...");
									Thread.Sleep(3000);
									break;
							}//end swtich
						}//end else
					}//end while
					break;
				case "7":
					bool wmi = true;
					while (wmi) {
						Console.Clear();
						Console.WriteLine("\n===== WMI execution =====\n");
						Console.Write("WMI needs an application (an exe somewhere) and args\n\tplease oblige (or back): ");
						string command = Console.ReadLine().ToLower();
						if (command == "back") {
							Console.Clear();
							Console.WriteLine("===== \"I'm not Mother Teresa, but I'm not Charles Manson, either.\" --Iron Mike Tyson =====");
							Thread.Sleep(3000);
							wmi = false;
						}//end if
						else {
							Console.Write("\nAre you sure you want to execute " + command + " using WMI\n\n[y/n/q]? ");
							string confirm = Console.ReadLine().ToLower();
							switch (confirm) {
								case "y":
									Console.Clear();
									Console.WriteLine("Executing " + command + " using WMI\n");
									//thank you https://github.com/GhostPack/SharpWMI
									ManagementScope scope = new ManagementScope("root\\cimv2");
									var wmiProcess = new ManagementClass(scope, new ManagementPath("Win32_Process"), new ObjectGetOptions());
									ManagementBaseObject inParams = wmiProcess.GetMethodParameters("Create");
									System.Management.PropertyDataCollection properties = inParams.Properties;
									inParams["CommandLine"] = command;
									ManagementBaseObject outParams = wmiProcess.InvokeMethod("Create", inParams, null);
									Console.WriteLine(command + " executed at " + DateTime.Now.ToString("HH:mm:ss tt") + " as PID " + outParams["processId"]);
									Console.WriteLine("\n==== Output/Error(s) =====\n");
									Console.WriteLine(outParams["returnValue"]);
									UInt32 pid = (UInt32) outParams["processId"];
									trackANDkill((int) pid);
									Console.Write("\nPress enter to continue ");
									Console.ReadLine();
									break;
								case "n":
									break;
								case "q":
									Console.Clear();
									Console.WriteLine("===== Acronymns right... =====");
									Thread.Sleep(3000);
									ps = false;
									break;
								default:
									Console.WriteLine("\nMaybe try that again, but better...");
									Thread.Sleep(3000);
									break;
							}//end swtich
						}//end else
					}//end while
					break;
				case "8":
					bool wscript = true;
					while (wscript) {
						Console.Clear();
						Console.WriteLine("\n===== VBScript execution =====\n");
						Console.Write("I'll build a vbs file for you (you're welcome),\n\tbut I WILL NOT sanitize input (so play nice unless you know what you're doing)\n\tI need a full command and args (or back): ");
						string command = Console.ReadLine().ToLower();
						if (command == "back") {
							Console.Clear();
							Console.WriteLine("===== \"Automation may be a good thing, but don’t forget that it began with Frankenstein.\" --Anonymous =====");
							Thread.Sleep(3000);
							wscript = false;
						}//end if
						else {
							Console.Write("\nAre you sure you want to execute:\n\t" + command + " with the wscript shell\n\n[y/n/q]? ");
							string confirm = Console.ReadLine().ToLower();
							switch (confirm) {
								case "y":			
									Console.Clear();
									Console.WriteLine("Executing " + command + " using the wscript shell\n");
									cliExec("wscript", command);
									Console.Write("\nPress enter to continue ");
									Console.ReadLine();
									File.Delete(Directory.GetCurrentDirectory() + "\\parent.vbs");
									break;
								case "n":
									break;
								case "q":
									Console.Clear();
									Console.WriteLine("===== Like a bad habit... =====");
									Thread.Sleep(3000);
									wscript = false;
									break;
								default:
									Console.WriteLine("\nEveryone likes a mystery...");
									Thread.Sleep(3000);
									break;
							}//end swtich
						}//end else
					}//end while
					break;
				case "9":
					bool fiber = true;
					while (fiber) {
						Console.Clear();
						Console.WriteLine("\n===== Windows Fiber execution =====\n");
						Console.Write("Fibers are like threads but \"invisible\" in terms of scheduling to the kernel\n\tscheduling is implemented in userland, you're welcome\n\tI need a full command and args (or back): ");
						string command = Console.ReadLine().ToLower();
						if (command == "back") {
							Console.Clear();
							Console.WriteLine("===== \"Men don't pay attention to small things.\" --Katherine Johnson =====");
							Thread.Sleep(3000);
							fiber = false;
						}//end if
						else {
							Console.Write("\nAre you sure you want to execute:\n\t" + command + " from a Windows fiber\n\n[y/n/q]? ");
							string confirm = Console.ReadLine().ToLower();
							switch (confirm) {
								case "y":			
									Console.Clear();
									Console.WriteLine("Executing " + command +  " from a Windows fiber\n");
									Thread t = new Thread(ThreadProc);
									t.Start(command);
									Console.Write("\nPress enter to continue ");
									Console.ReadLine();
									break;
								case "n":
									break;
								case "q":
									Console.Clear();
									Console.WriteLine("===== Threads are easier anyways... =====");
									Thread.Sleep(3000);
									fiber = false;
									break;
								default:
									Console.WriteLine("\nThis is already complex enough...");
									Thread.Sleep(3000);
									break;
							}//end swtich
						}//end else
					}//end while
					break;
				case "10":
					bool wxsl = true;
					while (wxsl) {
						Console.Clear();
						Console.WriteLine("\n===== WMIC XSL Script Processing (Squiblytwo) =====\n");
						Console.Write("I'll build a xsl file for you (you're welcome),\n\tbut I WILL NOT sanitize input (so play nice unless you know what you're doing)\n\tI need a full command and args (or back): ");
						string command = Console.ReadLine().ToLower();
						if (command == "back") {
							Console.Clear();
							Console.WriteLine("===== \"...If it weren't for those meddling kids.\" --Too Many Scooby-Doo Villains =====");
							Thread.Sleep(3000);
							wxsl = false;
						}//end if
						else {
							Console.Write("\nAre you sure you want to execute:\n\t" + command + " through a wmic xsl script\n\n[y/n/q]? ");
							string confirm = Console.ReadLine().ToLower();
							switch (confirm) {
								case "y":			
									Console.Clear();
									Console.WriteLine("Executing " + command + "  through a wmic xsl script\n");
									cliExec("wxsl", command);
									Console.Write("\nPress enter to continue ");
									Console.ReadLine();
									File.Delete(Directory.GetCurrentDirectory() + "\\parent.xsl");
									break;
								case "n":
									break;
								case "q":
									Console.Clear();
									Console.WriteLine("===== WMIC is weird... =====");
									Thread.Sleep(3000);
									wxsl = false;
									break;
								default:
									Console.WriteLine("\nDon't try to wiggle out of this one...");
									Thread.Sleep(3000);
									break;
							}//end swtich
						}//end else
					}//end while
					break;
				case "11":
					bool wordz = true;
					while (wordz) {
						Console.Clear();
						Console.WriteLine("\n===== Microsoft Word VBA Macro =====\n");
						Console.Write("I'll build a doc file for you (you're welcome),\n\tbut I WILL NOT sanitize input (so play nice unless you know what you're doing)\n\tI need a full command and args (or back): ");
						string command = Console.ReadLine().ToLower();
						if (command == "back") {
							Console.Clear();
							Console.WriteLine("===== \"Words are but pictures of our thoughts.\" --John Dryden =====");
							Thread.Sleep(3000);
							wordz = false;
						}//end if
						else {
							Console.Write("\nAre you sure you want to execute:\n\t" + command + " inside a Word macro\n\n[y/n/q]? ");
							string confirm = Console.ReadLine().ToLower();
							switch (confirm) {
								case "y":			
									Console.Clear();
									Console.WriteLine("Executing " + command + " inside a Word macro at " + DateTime.Now.ToString("HH:mm:ss tt") + "\n");
									Console.WriteLine();
									//thank you https://github.com/enigma0x3/Generate-Macro
									Microsoft.Office.Interop.Word.Application winword = new Microsoft.Office.Interop.Word.Application();
									object missing = System.Reflection.Missing.Value;
									string VBCode = "Sub AutoOpen()\nEZ\nEnd Sub\nPublic Function EZ() As Variant\nPID = Shell(\"" + command + "\",4)\nEnd Function";
									try {
										Microsoft.Office.Interop.Word.Document document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);
										string path = Directory.GetCurrentDirectory() + "\\parent.docm"; 
										document.SaveAs2(path);
										Microsoft.Vbe.Interop.VBProject Project = document.VBProject;
										Microsoft.Vbe.Interop.VBComponent Module = Project.VBComponents.Add(Microsoft.Vbe.Interop.vbext_ComponentType.vbext_ct_StdModule);
										Microsoft.Vbe.Interop.CodeModule Code = Module.CodeModule;
										Code.AddFromString(VBCode);
										Process currentProcess = Process.GetCurrentProcess();
										Process process = new Process();
										process.StartInfo.FileName = "winword.exe";
										process.StartInfo.Arguments = path;
										process.StartInfo.RedirectStandardOutput = false;
										process.StartInfo.RedirectStandardError = false;
										process.StartInfo.UseShellExecute = false;
										process.Start();
										Console.WriteLine(process.ProcessName + " started at " + process.StartTime + " as PID " + process.Id);
										//find real winword.exe
										Thread.Sleep(1000);
										Process[] winWordTwo = Process.GetProcessesByName("winword");
										foreach (Process winPid in winWordTwo) {
											trackANDkill((int) winPid.Id);
										}//end foreach
										Console.Write("\nPress enter to continue ");
										Console.ReadLine();
										File.Delete(path);
									}//end try
									catch (Exception ex) {
										Console.Write(ex + "\n\nAlso double check that you have enabled macros and automagic code access (aka trust access to the VBA project object model) for this jazz to work\n\nPeep https://support.office.com/en-us/article/enable-or-disable-macros-in-office-files-12b036fd-d140-4e74-b45e-16fed1a7e5c6 for the good word\n");
									}//end catch
									break;
								case "n":
									break;
								case "q":
									Console.Clear();
									Console.WriteLine("===== This was harder to do than it seems... =====");
									Thread.Sleep(3000);
									wordz = false;
									break;
								default:
									Console.WriteLine("\nThere's a time to be different, but not now...");
									Thread.Sleep(3000);
									break;
							}//end swtich
						}//end else
					}//end while
					break;
				case "exit":
					Console.Clear();
					Console.WriteLine("\n===== stay classy =====\n");
					Thread.Sleep(3000);
					System.Environment.Exit(1);
					break;
				default:
					Console.Clear();
					Console.WriteLine("\n===== try to play nice... =====\n");
					break;
				case "12":
					bool python = true;
					while (python) {
						Console.Clear();
						Console.WriteLine("\n===== python.exe execution =====\n");
						Console.Write("please provide the cmd.exe command you want to execute via python.exe (or back): ");
						string command = Console.ReadLine().ToLower();
						if (command == "back") {
							Console.Clear();
							Console.WriteLine("===== \"Snakes are only cool if they are eating their own tail.\" --Yours Truly =====");
							Thread.Sleep(3000);
							python = false;
						}//end if
						else {
							Console.Write("\nAre you sure you want to execute:\n\t " + command + " via python.exe\n\n[y/n/q]? ");
							string confirm = Console.ReadLine().ToLower();
							switch (confirm) {
								case "y":
									Console.Clear();
									Console.WriteLine("Executing " + command + " via python.exe\n");
									command = "\"import os;os.system(\'" + command + "\')\"";
									Console.Write(command);
									cliExec("python", command);
									Console.Write("\nPress enter to continue ");
									Console.ReadLine();
									break;
								case "n":
									break;
								case "q":
									Console.Clear();
									Console.WriteLine("===== I'm not a big fan of python either... =====");
									Thread.Sleep(3000);
									python = false;
									break;
								default:
									Console.WriteLine("\nNot everyone can color inside the lines...");
									Thread.Sleep(3000);
									break;
							}//end swtich
						}//end else
					}//end while
					break;
			}//end switch
		}//end while
	}//end Main
	public static void cliExec(string cli, string args) {
		Process process = new Process();
		if (cli == "cmd") {
			process.StartInfo.FileName = "cmd.exe";
			process.StartInfo.Arguments = "/c " + args;
		}//end if
		else if (cli == "powershell") {
			process.StartInfo.FileName = "powershell.exe";
			process.StartInfo.Arguments = "-c " + args;
		}//end else if
		else if (cli == "wscript") {
			process.StartInfo.FileName = "cscript.exe";
			process.StartInfo.Arguments = " parent.vbs";
			string path = Directory.GetCurrentDirectory() + "\\parent.vbs";
			if (!File.Exists(path)) {
				using (StreamWriter sw = File.CreateText(path)) {
					sw.WriteLine("Dim objShell,oExec,input:Set objShell = wscript.createobject(\"wscript.shell\"):Set oExec = objShell.Exec(\"" + args + "\"):x = oExec.StdOut.ReadLine:Wscript.Echo x");
				}//end using
			}//end if
		}//end else if
		else if (cli == "wxsl") {
			process.StartInfo.FileName = "wmic.exe";
			process.StartInfo.Arguments = " process list /FORMAT:parent.xsl";
			string path = Directory.GetCurrentDirectory() + "\\parent.xsl";
			if (!File.Exists(path)) {
				using (StreamWriter sw = File.CreateText(path)) {
					sw.WriteLine("<?xml version='1.0'?><stylesheet xmlns=\"http://www.w3.org/1999/XSL/Transform\" xmlns:ms=\"urn:schemas-microsoft-com:xslt\" xmlns:user=\"placeholder\" version=\"1.0\"> <output method=\"text\"/><ms:script implements-prefix=\"user\" language=\"JScript\"><![CDATA[	var r = new ActiveXObject(\"WScript.Shell\").Run(\"" + args + "\");	]]> </ms:script></stylesheet>");
				}//end using
			}//end if
		}//end else if
		else if (cli == "python") {
			process.StartInfo.FileName = "python.exe";
			process.StartInfo.Arguments = "-c " + args;
		}//end else if
		process.StartInfo.RedirectStandardOutput = true;
		process.StartInfo.RedirectStandardError = true;
		process.StartInfo.UseShellExecute = false;
		process.Start();
		Console.WriteLine(process.ProcessName + " started at " + process.StartTime + " as PID " + process.Id);
		trackANDkill((int) process.Id);
		Console.WriteLine("\n===== Output =====\n\n" + process.StandardOutput.ReadToEnd());
		StreamReader error = process.StandardError;
		Console.WriteLine("\n==== Error(s) =====\n\n" + error.ReadLine());
	}//end cliExec
	public static void trackANDkill(int pid) {
		Process parent = Process.GetProcessById(pid);
		Console.Write("\n===== Observed Child Processes =====\n\n");
		List<int> knownPids = new List<int>();
		knownPids.Add(pid);
		int loops = 0;
		bool go = true;
		while (!parent.HasExited && go) {
			List<int> tmp = new List<int>(knownPids);
			foreach (int proc in tmp) {
				ManagementObjectSearcher mos = new ManagementObjectSearcher(String.Format("Select * From Win32_Process Where ParentProcessID={0}", proc));
				foreach (ManagementObject mo in mos.Get()) {
					Int32 childPid = Convert.ToInt32(mo["ProcessID"]);
					if (!knownPids.Contains(childPid)) {
						knownPids.Add(childPid);
						try {
							Process child = Process.GetProcessById(childPid);
							Console.WriteLine("\t" + child.ProcessName + " started at " + child.StartTime + " as PID " + child.Id);
						}//end try
						catch {
							Console.WriteLine("\t Child process died too fast to index, but the PID was " + childPid);
						}//end catch
					}//end if
				}//end foreach
			}//end while
			loops++;
			if ((loops == 100) || (loops % 300 == 0)) {
				DialogResult dialog = MessageBox.Show("Kill processes and move on?","Had Enough?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
				if (dialog == DialogResult.Yes) {
					foreach (int target in knownPids) {
						try {
							Process target2Kill = Process.GetProcessById(target);
							Process currentProcess = Process.GetCurrentProcess();
							if (target2Kill.Id == currentProcess.Id)
								go = false;
							else {
								target2Kill.Kill();
							}//end else
						}//end try
						catch {
							Console.WriteLine("\nCould not kill child with PID " + target + ", maybe it is already dead\n\tor an idle process?");
						}//end catch
					}//end foreach
				}//end if
			}//end if
		}//end while
	}//end trackANDkill
	public static void ThreadProc(object data) {
		//a lot of this may seem redundant, but needs to be implemented within the fiber
		//or the scheduler gets weird...
		IntPtr fiberAddr = ConvertThreadToFiber();
		SwitchToFiber(fiberAddr);
		string command = (string) data;
		string[] full = command.Split(' ');
		string app = "";
		string param = "";
		int count = 0;
		foreach (string i in full) {
			if (count == 0)
				app = i;
			else if (count == 1)
				param = i;
			else
				param += " " + i;
			count++;
		}//end foreach
		Process process = new Process();
		process.StartInfo.FileName = app;
		process.StartInfo.Arguments = param;
		process.StartInfo.RedirectStandardOutput = true;
		process.StartInfo.RedirectStandardError = true;
		process.StartInfo.UseShellExecute = false;
		process.Start();
		Console.WriteLine(process.ProcessName + " started at " + process.StartTime + " as PID " + process.Id);
		trackANDkill(process.Id);
		Console.WriteLine("\n===== Output =====\n\n" + process.StandardOutput.ReadToEnd());
		DeleteFiber(fiberAddr);
    }//end ThreadProc
	[DllImport("kernel32.dll")]
	static extern bool CreateProcess(string lpApplicationName, string lpCommandLine, IntPtr lpProcessAttributes, IntPtr lpThreadAttributes,
		bool bInheritHandles, uint dwCreationFlags, IntPtr lpEnvironment,
		string lpCurrentDirectory, ref STARTUPINFO lpStartupInfo,out PROCESS_INFORMATION lpProcessInformation);
	[DllImport("kernel32.dll")]
	static extern int WinExec(string lpCmdLine, int uCmdShow);
	[DllImport("kernel32.dll")]
	static extern IntPtr ConvertThreadToFiber();
	[DllImport("kernel32.dll")]
	static extern IntPtr SwitchToFiber(IntPtr lpFiber);
	[DllImport("kernel32.dll")]
	static extern IntPtr DeleteFiber(IntPtr lpFiber);
}//end class