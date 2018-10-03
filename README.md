# Argus GitHub Scrapper

This is an exercise for Argus Backend Automation.

### Installing

1. You need to download to you windows 7/10 Docker ToolBox from: https://docs.docker.com/toolbox/toolbox_install_windows/
Just click on the link. You can also download from this link: https://download.docker.com/win/stable/DockerToolbox.exe.
Make sure you select VirtualBox and Kitematic in the install wizard.
It takes soome time so be patiannt. 
2. Go to the installed Docker Quickstart Terminal you installed
3. To create a MongoDB server you need to run the followwing commands:
* docker volume create --name=mongodata
* docker run -d -p 27017:27017 -v mongodata:/data/db mongo
4. Now create a selenium server on a new docekr with the following command: 
* docker run -d -p 4444:4444 -v /dev/shm:/dev/shm selenium/standalone-chrome:3.14.0-europium

5. Start Kitematic (Alpha) app the docker toolbox installed, and search for the 2 docker instances you created. Write down the IP and the port of the MongoDB server and the Selenium server (It should be 192.168.99.100:27017 for docker and 192.168.99.100:4444 for selenium)
6. Download the repo to a folder of you choice

## Running the tests

1. Run GitHubScraper.exe located in \GitHubScraper\GitHubScraper\bin\Release
2. Enter the MongoDB and Slenium server ip and port as you will see in the command prompt. 

## See the results the test
1. Install Robo 3T in order to connect to the MongoDB server. You can download it from https://robomongo.org/download
2. Connect to the MongoDB server with the IP you were given (It should be 192.168.99.100:27017).
3. Navigate to test database-> GitRepo collection and there you will see all the scraped data.
4. In order to see the timing resilts of the search and the loading of the second page, go the command and see in there.


Enjoy,
Omer Shamir
