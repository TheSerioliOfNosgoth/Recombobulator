# Recombobulator
A tool for analysing the contents of room and object files from Legacy of Kain: Soul Reaver.

# Building the Alpha-Retail hybrid map.

Note: You must obtain any required builds of Soul Reaver yourself, as I am unable to provide those.

* From the PC version of Soul Reaver, copy bigfile.dat and textures.big to a new folder for the project.
* Use Soul Spiral (available from www.thelostworlds.net) to extract the bigfiles from both February builds and the April builds of Soul Reaver.
* Copy kain2/areas/city/city12.drm and kain2/areas/city/city12.crm from the Feb 04 build to the corresponding location in the Feb 16 build.
* Copy kain2/areas/adda/adda1.drm and kain2/areas/adda1/adda1.crm from the April 14 build to the corresponding location in the Feb 16 build.
* Launch Recombobulator and File->New Project, then browse for the folder above.
* Wait for Recombobulator to extract all the files from the PC version of the game.
* Select Scripted Imports->Import All Deleted Areas..., then browse for where the Feb 16 build was extracted.
* Wait for the import process to complete.
* Select File->Compile and wait for Recombobulator to rebuild the new bigfile.dat and textures.big for the PC version.
* Look in the folder you originally created for the project, and copy the bigfile.dat and textures.big from the output folder back to PC version of Soul Reaver's install directory.
