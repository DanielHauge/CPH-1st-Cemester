#!/bin/bash

# add this line to hosts too get rid of unknown host messages
# echo '127.0.1.1 ubuntu-xenial' | sudo tee --append /etc/hosts

# install Jenkins to the VM
wget -q -O - https://pkg.jenkins.io/debian/jenkins-ci.org.key | sudo apt-key add -
sudo sh -c 'echo deb http://pkg.jenkins.io/debian-stable binary/ > /etc/apt/sources.list.d/jenkins.list'
sudo apt-get update
sudo apt-get install -y jenkins
# we need the JDK to build our code later
sudo apt-get install -y openjdk-8-jdk

# install docker to the VM so that we can build docker images
sudo apt-get update
sudo apt-key adv --keyserver hkp://p80.pool.sks-keyservers.net:80 --recv-keys 58118E89F3A912897C070ADBF76221572C52609D
echo "deb https://apt.dockerproject.org/repo ubuntu-xenial main" | sudo tee /etc/apt/sources.list.d/docker.list
sudo apt-get update
apt-cache policy docker-engine
sudo apt-get install -y docker-engine

# add the users to the docker group, so that we can run the docker commands
# without sudo. Be carefull with that on a real machine as docker commands can
# execute root level commands when building the images. Should be okay on a VM.
sudo usermod -aG docker $(whoami)
sudo usermod -aG docker jenkins
sudo systemctl daemon-reload
sudo systemctl restart docker
sudo service jenkins restart

sudo chmod 700 ~/.ssh
sudo echo "ssh-rsa AAAAB3NzaC1yc2EAAAADAQABAAACAQDoqHjzR547kAzN2HsRxwuiipeLpAbyM9ZN8JMDg0DzzxsExqKZJC2txil//kKqsYRLEbK3ZoqXIaIYRkYmMYoFt7CQk34ZFY0WEA66lu+sKCFfCtk0pweRJCxkNY5nqb6aI/R7ON6aqtAtXlbTp4HGwwllVsuA7Ap4x5EMjo+RuIUdkhsKo6rTl+ty55i86mMTdxdY5pOsCZDLDqEV6zkyRfkYU7pxnfJLL0zFreEFtbH3JAN9d++RuVoKK5HKPlHpn0VjdmMs8xCrS45GmIQybC8P0t+//jLk9JBd48jWUL269JuthOUDZKaZD+XdULrvoIg4fLkPr74miKxfOA9TiSKMDTiInt9mnYVV26ce+JKg7tWFZ3Gnb+6WUBdwkS2IhGmw/NcpaMuSq2Ru7JXHJGO9EcQhjTg5ALX45Cv++mYzOZgis5EYzaZHBVw+sOphq2K7DosKc1Cv0XK1dvT1cfs2/0EQ+hb7IerP5NNV3Y+lrDPdo1AK+LPd3wX73ZCNhs3crJOJcApzPKlaOdGhG3Y+20wOGRAZYG8k4MPiCnW2vomhrgLY7fXjd1l+Pf4G+MLw2yHUPu3ILRFNaCGc83UJdP+TamkFU7yKisZXmjrNK2Is7GJxtpRJtEHKg6YZc/MJ6UwfNST3pW8SRxUTh5PEQlcTrj6XYRKc90RK2w== Animcuil@gmail.com"> ~/.ssh/authorized_keys
sudo chmod 600 ~/.ssh/authorized_keys

# Your need this password to complete the initial Jenkins setup on the VM
echo "********** Initial Admin Password **********"
sudo cat /var/lib/jenkins/secrets/initialAdminPassword
echo "********************************************"
