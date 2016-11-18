#!/bin/bash

# ---------------------------------------------
# do not edit or modify
# ---------------------------------------------
file="unix_path.dat" 	#the file where you keep your string name
name=$(cat "$file")     #the output of 'cat $file' is assigned to the $name variable
# echo $name

var="$name"
var="${var#"${var%%[![:space:]]*}"}"   # remove leading whitespace characters
var="${var%"${var##*[![:space:]]}"}"   # remove trailing whitespace characters

echo "$var"
cd "$var"

# ----------------------------------------------

# ----------------------------------------------
# Edit commands below as per requirements 
# ----------------------------------------------
git status
git add .
git commit -m "A test."
git push origin master