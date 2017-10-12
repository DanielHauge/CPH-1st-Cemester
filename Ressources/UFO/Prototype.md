Prototype Evaluation
===================

# Initial Problem statement
Australia vs Unites States vs United Kingdom, who is the best at serving websites?

## Problem Statement
What part of the world is fastest at serving a static "hello world" website. 
This prototype Evaluation will help to get closer to a definit answer.

## Hypothesis
Considering the question at hand which is: Which part of the world is the fastest at serving a static "hello world" website.
We need to consider the server which the website is hosted on. One server might have a better connection than the other servers, or might have better specifications to allow for faster servings.
But assuming that all servers are identical in terms internet, hardware and software specifications, ea: remove all other factors than location.
The hypothesis is that, the closest geographical location to the requester will be the fastest.

## Test / Experiment
To test this hypothesis, we can setup an experiment.

The experiment will be as follows: Meassure responsetime with a CSharp application from 2 different locations to servers on 3 different parts of the world. In this case Australia, United States and United Kingdom.
Also meassure from https://tools.pingdom.com/.

### Servers
3 Servers will be setup with the same internet, hardware and software specifcations: (Note: Actully don't know this, but this is important to convince reader)
The following servers and ports which the website is running on are as follows:
- http://139.59.132.185:8080 : Australia
- http://192.81.216.124:8080 : United States
- http://128.199.180.131:8080 : United Kingdom

Country verified with: http://www.ip2nation.com/

Specifications here!:
(Note: Internet, Hardware, software specifications for the servers are very interesting to, where are these? :P?).

By manually going to these websites through a popular browser such as google chrome we can see it is serving a static website with the body: Hello world. And nothing else.

### Requester (Meassurer)
The requester is a CSharp program which will try to request a response from a server. It will measure the time it takes from when it asks for the request to when it receives it.

code for the program:
```csharp
using System;
using System.Diagnostics;
using System.Net;

namespace Measure
{
    class Program
    {
        static void Main(string[] args)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://139.59.132.185:8080"); // or another url

            Stopwatch timer = new Stopwatch();

            timer.Start();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            timer.Stop();

            TimeSpan timeTaken = timer.Elapsed;
            Console.WriteLine("Seconds: "+timeTaken.Seconds+" Miliseconds: "+timeTaken.Milliseconds);
            Console.WriteLine("Full time taken: "+timeTaken);
            Console.ReadKey();
        }
    }
}
```

### Execution
Firstly i tried to run the requester on my own personal computer from home. 

##### Execution 1 (Home computer) with Csharp application.
Relevant specs: 
 - Processor: Intel(R) Core(TM) i5-3570k CPU @ 3.40GHz 3.4 GHz
 - RAM: 8 GB
 - OS: Windows 10 Home edition (With all available updates)
 - Internet: Ping: 8ms, 69,91 Mbps down, 33,61 Mbps. (From http://www.speedtest.net/)
 - Location: Denmark

Actions:
 - Run program from visual studio (Community edition)
 - Wait 5 or more seconds.
 - Rinse and repeat 5 times for every location.

##### Execution 2 (Digital Ocean Droplet) With Csharp application.
Relevant specs:
 - Droplet size: 512 MB ram, 20gb SSD, 1000GB Transfer.
 - Droplet Location: London
 - Droplet Internet: 
  ```Bash
  Hosted by Coreix Ltd (London) [0.96 km]: 3.795 ms
Testing download speed................................................................................
Download: 813.68 Mbit/s
Testing upload speed................................................................................................
Upload: 729.63 Mbit/s
```
  Internet configurations obtained from running:
 ```Shell
curl -s https://raw.githubusercontent.com/sivel/speedtest-cli/master/speedtest.py | python -
 ```

I've run this shell script when starting up the droplet (From Vagrantfile) for my usual Csharp things.

 ```bash
 curl https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > microsoft.gpg
 sudo mv microsoft.gpg /etc/apt/trusted.gpg.d/microsoft.gpg
 sudo apt-get update
 sudo apt-get install -y dotnet-sdk-2.0.0
 sudo apt-get upgrade -y
 sudo apt-get install -y dotnet-dev-1.1.4
 sudo apt-get install -y build-essential libssl-dev
 sudo apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF
 echo "deb http://download.mono-project.com/repo/ubuntu xenial main" | sudo tee /etc/apt/sources.list.d/mono-official.list
 sudo apt-get update
 sudo apt-get install -y mono-devel
 ```

Actions:
 - Run program with ```mono myprogram.exe``` or ```dotnet myprogram.dll``` (dll if .net core and mono if .net with framework) in this case. mono program.exe
 - Wait 5 or more seconds.
 - Rinse and repeat 5 times for every location.

##### Execution 3
Try out each server with https://tools.pingdom.com/ from 3 different locations 5 times:
- Melbourne in Australia
- Jan Jose, california in United States
- Stockhold in sweden.


### Results:
The results is response time in seconds and miliseconds. Here is the results:

##### Execution 1:
- Australia
>- Seconds: 0 | Miliseconds: 71 (00:00:00.0713516)
>- Seconds: 0 | Miliseconds: 60 (00:00:00.0608856)
>- Seconds: 0 | Miliseconds: 105 (00:00:00.1054806)
>- Seconds: 0 | Miliseconds: 76 (00:00:00.0766687)
>- Seconds: 1 | Miliseconds: 66 (00:00:01.0669144)
avg responsetime: 275 miliseconds.
- United States
>- Seconds: 0 | Miliseconds: 978 (00:00:00.9784568)
>- Seconds: 0 | Miliseconds: 534 (00:00:00.5341794)
>- Seconds: 0 | Miliseconds: 524 (00:00:00.5241898)
>- Seconds: 0 | Miliseconds: 614 (00:00:00.6142026)
>- Seconds: 0 | Miliseconds: 345 (00:00:00.3450417)
avg responsetime: 599 miliseconds.
- United Kingdom
>- Seconds: 0 | Miliseconds: 794 (00:00:00.7940248)
>- Seconds: 0 | Miliseconds: 719 (00:00:00.7196091)
>- Seconds: 0 | Miliseconds: 770 (00:00:00.7703823)
>- Seconds: 0 | Miliseconds: 578 (00:00:00.5782847)
>- Seconds: 0 | Miliseconds: 619 (00:00:00.6192124)
avg responsetime: 696 miliseconds.

##### Execution 2:
- Australia
>- Seconds: 0 | Miliseconds: 390 (00:00:00.3905766)
>- Seconds: 0 | Miliseconds: 167 (00:00:00.1673173)
>- Seconds: 0 | Miliseconds: 165 (00:00:00.1655632)
>- Seconds: 0 | Miliseconds: 106 (00:00:00.1064058)
>- Seconds: 1 | Miliseconds: 105 ( 00:00:00.1055629)
avg responsetime: 186,6 miliseconds.
- United States
>- Seconds: 0 | Miliseconds: 285 ( 00:00:00.2850237)
>- Seconds: 0 | Miliseconds: 226 (00:00:00.2266672)
>- Seconds: 0 | Miliseconds: 266 (00:00:00.2663051)
>- Seconds: 0 | Miliseconds: 267 (00:00:00.2677539)
>- Seconds: 0 | Miliseconds: 247 (00:00:00.2473653)
avg responsetime: 258,2 miliseconds.
- United Kingdom
>- Seconds: 0 | Miliseconds: 480 ( 00:00:00.4803129)
>- Seconds: 0 | Miliseconds: 480 (00:00:00.4807654)
>- Seconds: 0 | Miliseconds: 435 (00:00:00.4352782)
>- Seconds: 0 | Miliseconds: 551 (00:00:00.5514352)
>- Seconds: 0 | Miliseconds: 509 (00:00:00.5091944)
avg responsetime: 491 miliseconds.

##### Execution 3:
- Request From Australia: (Results here are in ms which stands for Miliseconds)
>- Australia Server: 656ms, 678ms, 680ms, 663ms, 658ms - avg: 667ms
>- United States Server: 435ms, 437ms, 436ms, 434ms, 433ms - avg: 417ms
>- United Kingdom Server: 323ms, 320ms, 318ms, 321ms, 322ms - avg: 320,8ms

- Request From United States:
>- Australia Server: 327ms, 348ms, 315ms, 346ms, 358ms - avg: 338,8ms
>- United States Server: 152ms, 152ms, 151ms, 152ms, 146ms - avg: 150,6ms
>- United Kingdom Server: 349ms, 348ms, 353ms, 348ms, 349ms - avg: 349,4ms

- Request From Sweden:
>- Australia Server: 48ms, 57ms, 50ms, 46ms, 46ms - avg: 49,4ms
>- United States Server: 195ms, 199ms, 201ms, 190ms, 186ms - avg: 194,2ms
>- United Kingdom Server: 407ms, 410ms, 410ms, 402ms, 406ms - avg: 407ms

### Evaluation
resuming the results:
- Australia server would be this long to send the requests:
>- Execution1
>>- 
