#include <stdio.h>
#include <unistd.h>
#include <stdbool.h>
#include <stdlib.h>
#include <string.h>
#include <pthread.h>
#define clear() printf("\033[H\033[J")
pthread_t ptidTracker;
void clearStdin(){
	int c;
	while ((c = getchar()) != '\n' && c != EOF);
}//end clearStdin
void* trackAndKill() {
	FILE *fp;
	int pids[50];
	char popenCommand[256];
	int i = 0;
	pids[i] = (int) getpid();
	int pid;
	char watchOutput[50][256];
	strcpy(watchOutput[i],"  PID   CMD                                          STARTED");
	char saveOutput[256];
	int j;
	int z;
	char line[100];
	int loopCount = 0;
	int stop;
	bool found = false;
	while (loopCount < 5) {
		for (z = 0; z <= i; z++) {
			sprintf(popenCommand,"/bin/ps --ppid %d -o pid,cmd,lstart",pids[z]);
			fp = popen(popenCommand, "r");
			while (fgets(line, sizeof(line), fp) != NULL) {
				if(strstr(line, "ps --ppid") == NULL && strstr(line, "PID CMD") == NULL && strstr(line, "defunct") == NULL) {					
					strcpy(saveOutput,line);
					pid = atoi(strtok(line, " "));
					for(j = 0; j <= i; j++) {
       					if(pids[j] == pid)
							found = true;
    					}//end for
					if (!found) {
						i++;
						strcpy(watchOutput[i],saveOutput);
						pids[i] = pid;
					}//end if
					found = false;
				}//end if
			}//end while
		}//end for
		loopCount++;
		if (loopCount == 5) {
			printf("\nContinue (y/n)? ");
			stop = getchar();
			getchar();
			if (stop == 'y')
				loopCount = 0;
		}//end if
	}//end while
	printf("\nChild Processes:\n");
	for (z = 0; z <= i; z++) {
		printf("%s\n",watchOutput[z]);
	}//end for
	char killCommand[100];
	for(z = 1; z <= i; z++) {
		sprintf(killCommand,"kill -9 %d >/dev/null 2>&1",pids[z]);
		printf("Killing: %d\n",pids[z]);
		system(killCommand);
	}//end for
	sleep(5);
	pthread_exit(NULL);
}//end trackAndKill
int main() {
	bool go = true;
	int input;
	bool loop = true;
	char command[256];
	int confirm;
	while (go) {
		clear();
		input = 0;
		printf("ezEmu: now with more ELF on the shelf\n\n");
		printf("We are running as PID: %d\n\n",getpid());
		printf("\t[1] sh via the system() function (T1059.004)\n\t[2] Python via the popen() function (T1059.006)\n\t[3] Perl via the system() function (T1059)\n\n");
		printf("Select an execution procedure (or 0 to exit): ");
		scanf("%d", &input);
		getchar();
		loop = true;
		switch (input) {
			case 1:
				while (loop) {
					clear();
					printf("sh via system() execution\n\n");
					printf("Enter a command to execute (or quit): ");
					fgets(command, 256, stdin);
					if (strcmp(command,"quit\n") != 0) {
						int len = strlen(command);
						if(command[len-1]=='\n')
   							command[len-1]='\0';
						printf("\nAre you sure you want to execute %s with sh (y/n)? ", command);
						confirm = getchar();
						getchar();
						if (confirm == 'y') {
							clear();
							pthread_t ptidTracker;
							printf("\nCommand Output:\n\n");
							pthread_create(&ptidTracker, NULL, &trackAndKill, NULL);
							system(command);
							printf("\n\n");
							pthread_join(ptidTracker, NULL);
						}//end if
					}//end if
					else {
						loop = false;
						strcpy(command, "");
						break;
					}//end else
				}//end while
				break;
			case 2:
				while (loop) {
					clear();
					printf("Python via the popen() execution\n\n");
					printf("Enter a command to execute (or quit): ");
					fgets(command, 256, stdin);
					if (strcmp(command,"quit\n") != 0) {
						int len = strlen(command);
						if(command[len-1]=='\n')
   							command[len-1]='\0';
						printf("\nAre you sure you want to execute %s with Python (y/n)? ", command);
						confirm = getchar();
						getchar();
						if (confirm == 'y') {
							clear();
							pthread_t ptidTracker;
							printf("\nCommand Output:\n\n");
							pthread_create(&ptidTracker, NULL, &trackAndKill, NULL);
							char pyCommand[256];
							sprintf(pyCommand,"python -c \"import os;os.system(\'%s\')\"",command);
							FILE *fp;
							char output[1035];
							fp = popen(pyCommand, "r");
							while (fgets(output, sizeof(output), fp) != NULL) {
								printf("%s", output);
							}//end while
							pclose(fp);
							printf("\n\n");
							pthread_join(ptidTracker, NULL);
						}//end if
					}//end if
					else {
						loop = false;
						strcpy(command, "");
						break;
					}//end else
				}//end while
				break;
			case 3:
				while (loop) {
					clear();
					printf("Perl via the system() execution\n\n");
					printf("Warning: Do not concatenate (&) commands\nEnter a single command to execute (or quit): ");
					fgets(command, 256, stdin);
					if (strcmp(command,"quit\n") != 0) {
						int len = strlen(command);
						if(command[len-1]=='\n')
   							command[len-1]='\0';
						printf("\nAre you sure you want to execute %s with Perl (y/n)? ", command);
						confirm = getchar();
						getchar();
						if (confirm == 'y') {
							clear();
							pthread_t ptidTracker;
							printf("\nCommand Output:\n\n");
							pthread_create(&ptidTracker, NULL, &trackAndKill, NULL);
							char plCommand[256];
      							sprintf(plCommand,"perl -e \"system(\"%s\")\"",command);
							system(plCommand);
							printf("\n\n");
							pthread_join(ptidTracker, NULL);
						}//end if
					}//end if
					else {
						loop = false;
						strcpy(command, "");
						break;
					}//end else
				}//end while
				break;
			case 0:
				go = false;
				clear();
				printf("\nThanks for playing\n\n");
				sleep(3);
				break;
			default:
				printf("Bad input, try again.\n");
				clearStdin();
				sleep(1);
				break;
		}//end switch
	}//end while	
	return 0;
}//end main
