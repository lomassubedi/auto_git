#!/bin/bash

# ---------------------------------------------
# do not edit or modify
# ---------------------------------------------
file="unix_path.dat" 	#the file where you keep your string name
name=$(cat "$file")     #the output of 'cat $file' is assigned to the $name variable
# echo $name
cd "$name"
# ----------------------------------------------

# ----------------------------------------------
# Edit commands below as per requirements 
# ----------------------------------------------
git status
git add .
git commit -m "XML almost ready."
git push origin xml_revert