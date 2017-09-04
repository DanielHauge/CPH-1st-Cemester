All Bash Commands https://github.com/datsoftlyngby/soft2017fall-lsd-teaching-material/blob/master/lecture_notes/01-Bash.ipynb

Create a new project directory. 

 - mkdir project

Create a data directory within the project directory.

 - mkdir data

Download the file http://stat-computing.org/dataexpo/2009/2005.csv.bz2 into the ~/project/data directory with curl.

wget http://stat-computing.org/dataexpo/2009/2005.csv.bz2

- Uncompress the downloaded file.

bzip2 -d filename.csv.bz2.

- Rename that file to flightdelays_2005.csv.

mv filename wantedname

- Count the number of lines in that file.

cat filename.csv | wc -l

- Count the number of words in that file.

cat filename.csv | wc -w

tail -100 ./flightdelays_2005.csv | wc -c
sed 's/[^,]//g' flightdelays_2005.csv | w
c -c
207077313
Counts the comma's... a way of finding words


- Count the number of characters in the last 100 lines of flightdelays_2005.csv using the  | pipe operator.
cat filename.csv wc -m    : pipeline.

- Create an executable Bash script data_analysis.sh, which performs the actions above.
touch data_analysis


- Find all flights leaving from JFK that were delayed more that half an hour due to bad weather in 2005 (flightdelays_2005.csv).
- How many such delayed flights were there?
- What was the longest delay due to weather?
- Hint you might need to use the sort and/or the uniq commands.
- Explain what the provision script of the Vagrantfile of this project does.


apt-cache search tree //Tree is a command to search for things with the commandline
"kill [id]" can used ask a program to terminate, a additional command -9 like "kill -9 [id]" can be used to force terminate a program

