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

# echo "$var"
# cd "$var"

# ----------------------------------------------

# ----------------------------------------------
# Edit commands below as per requirements 
# ----------------------------------------------
cd "$var" && git status
# git add .
# git commit -m "A test."
# git push origin master

read -rsp $'Press any key to continue...\n' -n 1 key