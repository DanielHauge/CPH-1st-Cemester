# -*- mode: ruby -*-
# vi: set ft=ruby :

# All Vagrant configuration is done below. The "2" in Vagrant.configure
# configures the configuration version (we support older styles for
# backwards compatibility). Please don't change it unless you know what
# you're doing.
Vagrant.configure("2") do |config|

  config.vm.box = "bento/ubuntu-16.04"
  config.vm.network "forwarded_port", guest: 9999, host: 9999, host_ip: "127.0.0.1"
  config.vm.network "private_network", ip: "192.168.33.10"
  
  config.vm.define "JenkinsLocal", primary: true do |dockermachine|
	dockermachine.vm.hostname = "JenkinsLocal"
	dockermachine.vm.provision "shell", inline: <<-SHELL
		
		wget -q -O - https://pkg.jenkins.io/debian/jenkins-ci.org.key | sudo apt-key add -
		sudo sh -c 'echo deb http://pkg.jenkins.io/debian-stable binary/ > /etc/apt/sources.list.d/jenkins.list'
		sudo apt-get update
		sudo apt-get install -y jenkins
		sudo apt-get install -y openjdk-8-jdk

		sudo apt-get update
		sudo apt-key adv --keyserver hkp://p80.pool.sks-keyservers.net:80 --recv-keys 58118E89F3A912897C070ADBF76221572C52609D
		echo "deb https://apt.dockerproject.org/repo ubuntu-xenial main" | sudo tee /etc/apt/sources.list.d/docker.list
		sudo apt-get update
		apt-cache policy docker-engine
		sudo apt-get install -y docker-engine

		sudo usermod -aG docker $(whoami)
		sudo usermod -aG docker jenkins
		sudo systemctl daemon-reload
		sudo systemctl restart docker
		sudo service jenkins restart

		sudo chmod 700 ~/.ssh
		sudo echo "ssh-rsa AAAAB3NzaC1yc2EAAAADAQABAAACAQDoqHjzR547kAzN2HsRxwuiipeLpAbyM9ZN8JMDg0DzzxsExqKZJC2txil//kKqsYRLEbK3ZoqXIaIYRkYmMYoFt7CQk34ZFY0WEA66lu+sKCFfCtk0pweRJCxkNY5nqb6aI/R7ON6aqtAtXlbTp4HGwwllVsuA7Ap4x5EMjo+RuIUdkhsKo6rTl+ty55i86mMTdxdY5pOsCZDLDqEV6zkyRfkYU7pxnfJLL0zFreEFtbH3JAN9d++RuVoKK5HKPlHpn0VjdmMs8xCrS45GmIQybC8P0t+//jLk9JBd48jWUL269JuthOUDZKaZD+XdULrvoIg4fLkPr74miKxfOA9TiSKMDTiInt9mnYVV26ce+JKg7tWFZ3Gnb+6WUBdwkS2IhGmw/NcpaMuSq2Ru7JXHJGO9EcQhjTg5ALX45Cv++mYzOZgis5EYzaZHBVw+sOphq2K7DosKc1Cv0XK1dvT1cfs2/0EQ+hb7IerP5NNV3Y+lrDPdo1AK+LPd3wX73ZCNhs3crJOJcApzPKlaOdGhG3Y+20wOGRAZYG8k4MPiCnW2vomhrgLY7fXjd1l+Pf4G+MLw2yHUPu3ILRFNaCGc83UJdP+TamkFU7yKisZXmjrNK2Is7GJxtpRJtEHKg6YZc/MJ6UwfNST3pW8SRxUTh5PEQlcTrj6XYRKc90RK2w== Animcuil@gmail.com"> ~/.ssh/authorized_keys
		sudo echo "ssh-rsa AAAAB3NzaC1yc2EAAAADAQABAAACAQDSKf3TBR0jDrqGQNMeZVJGs/oOTnO7ILVX3T56kumpmYxIzaMFrGENx/cxKcY2sE599LMvYPI2oYidqoGq/NJg2X0IdnC5B3XMJcx0vSPtrDYYGzwWcG6uEvdqG8+jjFR+oXGspGc8ys3sUTJe9sGEVIUPNuwZvsXiFpHgVIbnZNmW+yrzS4+3ZUu/KZSEaqc/T5FvJbEdjUtjeGNWy5vbokCsj/HGjvTAJVgLYh6bgzStjljIlmhMNa1k23sxIpD/QImvOZeNq2oXySWW3wCxTDfMZlaMvZPFD2EvuFuD6rku9qUuL/+12WdYZViDufXE+WOKqQHMe/XA1vY8A9rVI+7BAlxcAo9O49xnFAScY69d41k0DQwXotX73RM1Q70Mq/Wdz0d0aKePhiQmuGSCUaG6miPkF2kfTpYrp9Iqg6VYpEgw1SpOjAAx+M1vXnGSkpjRLPe2nu+fR4ouipXQFQHFDSpRUfZOKP+7fQOWB25iyciYWssekdAJfnlJ176kPN6zFMdqVp5bgxP4taloHQV4evrenzNFrz2iq/oZkrmkLMVG+300IBA8T1DPAoBuUtRlMde9D9xqOCoCdz8MqsjpbwWmT0P+uGbTR0SFBWiC2dylXqXATUsCZBjYOBjhAmtVCGYYtW9/BobfnGcjhKHOGY6jZP6TDI6TCFBVdQ== Animcuil@gmail.com"> ~/.ssh/authorized_keys
		sudo chmod 600 ~/.ssh/authorized_keys


		echo "********** Initial Admin Password **********"
		sudo cat /var/lib/jenkins/secrets/initialAdminPassword
		echo "********************************************"
		
		
	SHELL
end

end
