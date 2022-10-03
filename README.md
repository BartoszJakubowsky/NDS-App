# NDS-App-v2

This app supposed to be help in my actual / previous job but it quickly changed to be my pet project to practice coding in C#

App is still in develop mode, over time i can see what to change, what is garbage, what may be work better and what I should continue

General purpeuse of this app supposed to be help in repeatable tasks in my job. Tasks gone away, app stayed :D so now I it's my coding training

The "help" part:
- Renaming large number of files
- Creating large number of directories
- Creating / copying large number of files

Target right now is to finish app in those 3 purposes in the way that final user won't have bigger problems in way of using it. In the meantime I add additional features, last would be for example save of command history in json file.

There was many problems i came trough, such as 

- unnessesary calling parent constructor (base) just because I learn something new and I wanted to implement it :D

- console changing colors and console.readline() - it's great example how not to write code and why working at 2am causes problems :) It's not suitable for further development and next time when I will be forced to add something new in this area I'm going to rewrite hole thing to be more flexible and susceptible to change (edit:it's finally happening!);

- JSON -> oh, this took me away some years from my futher life :d 
Code is quite simple but I came across with strange bug or mistakenly I overwrite somehow some variable wich caused unwroting any string into json file. Finally it works fine and it's ready to cooperate as auto-hint with CLI when it's will be ready...

- subfolders  move/rename -> in my job i sometimes needed to copy only folders without files, i'm still strugling with it due to lack of time to do code it correctly -> code first moves the "main" folder and then looks for subfolders... so error is occuring :/ I'll finish it soon.

- lot of smaller errors such as wrong path, wrong string name, mostly with move / copy file. Now it works fine.

How does it wokr in summary? 

In "menu" you type a command with thing you want to do (right now you can aslo write a digit but not for long).

Avaible commands right now are help, move, copy, in the future there will be among others a settings command.

CLI parse characters into string array and sends it to the right class.
Main command are typed with prefix "-",  for example "-move", then you type home path and target path (in move and copy class right now this part of array is assigned to specific path due to lack of posibility to paste paths and to speed up develop process)

After main command and writing boths paths, you can write additional commands, starting with prefix "--". 
There's avaible:
-- create (to create target string/file),
-- subFolders (to copy folders with or without subfolders),
-- whatToCopy -> to type files or folders (to copy files folders :D),
-- extension -> to type extension (to copy only certain files)
-- whatrename (in -rename)

App is being develop after hours so sometimes when I wrote something late at night, next day I wonder how someone could wrote something so bad :(
I guess my only hope is to keep coding:)
