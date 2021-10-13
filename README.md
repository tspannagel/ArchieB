# Introduction 
ArchieB maps your CPU and memory usage to your Logitech Keyboard using the official logitech sdk

# Configuration
To Setup ArchieB to your pc, enter the keys which have to be illuminated in the archieB.conf file under cpuKeys. You have to specify NumberOfThreads+1 Keys (one for each Thread plus Total)

e.g. if you have a 4 core processor with hyperthreading, you have to define
(4*2)+1 = 9 Keys.
The last key will be used to display the CPU Average (total)
