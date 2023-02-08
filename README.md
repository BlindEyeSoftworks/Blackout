# Blackout
A small headless utility program for powering off all DDC/CI capable monitors connected to the system.

### Usage:
In order to use this program all targetted monitors must support DDC/CI and DDC/CI must be enabled via their OSD.

<p align="center">
<img width="607" src="https://github.com/BlindEyeSoftworks/Blackout/blob/main/assets/osd.jpg" alt="Acer OSD">
</p>

Once environment compatiblity has been verified and configured simply run the utility for effects to take place. Blackout does not need to be
invoked with command line arguments and will also automatically terminate once the operation has been completed.

### Note:
Blackout does not contain any error handling mechanisms and will fail loudly if an exception is raised. For silent operation
you may implement your own logic for accepting and handling a silent parameter for invocation. When building Blackout you
must ensure that the compiler is generating the assembly with debug information to avoid an AV false positive flag.

### Minimum Requirements:
- Windows Vista or Windows Server 2008
- .NET Framework 4.6
- DDC/CI capable monitor(s)
