# Recombobulator
A tool for analysing the contents of room and object files from Legacy of Kain: Soul Reaver.

# Building a custom map.

Recombobulator allows new rooms and and objects from alternative versions of the game on PSX to be added to the PC version of Soul Reaver.

- Copy bigfile.dat and textures.big from Soul Reaver's install directory into a new folder.
- Open Recombobulator and choose File->New Project... and then select the folder with the above files. This will unpack them into a repository.
- File->Open Project... can be used instead after the first time.
- Select File->Open File... to view an object or room (*.pcm) file before adding it.
- Select File->Add To Project... to add the currently open object or room file.
- If a previously added room uses the same textures, you can select those from the Texture Set dropdown to avoid adding duplicate copies.

Repeat until you have all the new files you want, then select File->Compile Project... to build the new bigfile.dat and textures.big

Once finished, the files can be found in the output folder inside the project directory. Copy those back to the Soul Reaver install folder.

Some rooms contain objects that aren't in the retail version. If the room won't load, check the intros and object lists for ones it doesn't have, then try adding them.

Deleting files manually will corrupt the project. Recombobulator doesn't yet have a delete feature, so start over if you make mistakes.

# Building the Alpha-Retail hybrid map.

Recombobulator can also build a hybrid map that combines the retail build with the cut areas from the alpha builds.

Note: You must obtain any required builds of Soul Reaver yourself, as I am unable to provide those.

- From the PC version of Soul Reaver, copy bigfile.dat and textures.big to a new folder for the project.
- Use Soul Spiral (available from www.thelostworlds.net) to extract the bigfiles from both February builds and the April builds of Soul Reaver.
- Copy kain2/areas/city/city12.drm and kain2/areas/city/city12.crm from the Feb 04 build to the corresponding location in the Feb 16 build.
- Copy kain2/areas/adda/adda1.drm and kain2/areas/adda1/adda1.crm from the April 14 build to the corresponding location in the Feb 16 build.
- Launch Recombobulator and File->New Project, then browse for the folder above.
- Wait for Recombobulator to extract all the files from the PC version of the game.
- Select Scripted Imports->Import All Cut Areas..., then browse for where the Feb 16 build was extracted.
- Wait for the import process to complete.
- Select File->Compile Project... and wait for Recombobulator to rebuild the new bigfile.dat and textures.big for the PC version.
- Look in the folder you originally created for the project, and copy the bigfile.dat and textures.big from the output folder back to PC version of Soul Reaver's install directory.
