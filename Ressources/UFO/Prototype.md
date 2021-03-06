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
The hypothesis is that, the closest geographical location to the requester will be the fastest. ea. Distance.

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

### Evaluation of results
resume of the results: (averages)
- Execution 1 (on windows 10 - Csharp app run on visual studio)
>- Denmark -> Australia -> Denmark (__275 ms__)
>- Denmark -> United States -> Denmark (__599ms__)
>- Denmark -> United Kingdom -> Denmark (__696ms__)
- Execution 2 (on ubuntu 16.04 - Csharp app run with mono)
>- London -> Australia -> London (__186,6ms__)
>- London -> United States -> London (__258,2ms__)
>- London -> Unites Kingdom -> London (__491ms__)
- Execution 3 (With https://tools.pingdom.com/)
>- Australia -> Australia -> Australia (__668ms__)
>- Australia -> United States -> Australia (__417ms__)
>- Australia -> United Kingdom -> Australia (__320,8ms__)
>- United States -> Australia -> United States (__338,8ms__)
>- United States -> United States -> United States (__150,6ms__)
>- United States -> United Kingdom -> United States (__349,4ms__)
>- Sweden -> Australia -> Sweden (__49,4ms__)
>- Sweden -> United States -> Sweden (__194,2ms__)
>- Sweden -> United Kingdom -> Sweden (__407ms__)

These results can be rearranged to be more interesting by listing them by server and taking the average of the server from all these locations.

I would like to highlight a few results in later discussion. I will flag some results to reference them later.

- Australia Server:
>- From Denmark 275ms
>- From London 186,6ms
>- From Australia 668ms **__(¤Flag 1)__**
>- From United States 338,8ms
>- From Sweden 49,4ms ----- **__(¤Flag 2)__**

Average responsetime: __393,36ms__
- United States Server:
>- From Denmark 599ms
>- From London 258,2ms
>- From Australia 417ms
>- From United States 150,6ms 
>- From Sweden 194,2ms

Average responsetime: __323,8__
- United Kingdom Server:
>- From Denmark 696ms
>- From London 491 ----- **__(¤Flag 3)__**
>- From Australia 320,8ms
>- From United States 349,4ms
>- From Sweden 407ms ----- 

Average responsetime: __452,84ms__

### Conclusion

On the outlook of things, it looks like united states servers is overall faster. But as the hypothesis stated, the closer geographicly the server and client is, the faster the responsetime would be. This does not seem to be true in this case. To further prove my point, i'd like to consider flag1. The request is comming from melbourne in australia where the server is also located. But this has a wooping 668 miliseconds delay, where's a request from sweden only takes on average 49,4ms (see flag2). Another examples consider flag 3, the server in united kingdom is taking a long time to respond back to a client in london even if they are very close geographicly. the response is even slower to sweden and denmark than to australia and united states, which is absurd if geographic distance was the deciding factor in responsetime. In addition to this, there were some very strange results from execution 1 and 3(mainly from sweden server), in execution 1 the results from the australian server was very low 80% of the time, only 1 results kinda skewed the average result. it was mainly below 80ms. This trend kinda continued with the execution 3 results from sweden with an average responsetime of 49,4ms. This seems very strange if the responsetime was dependent on geographical distance.

There must be something going on which i have not factored into the prototype evaluation. more specificly how http packets travel the world. To further experiment and test newer prototypes, we would need to look into Tracerouting, and how packets jumps from place to place before finaly arriving at destination. New hypothesis would be that it is still dependent on geographical location, but not so much distance, but rather how many places the packet needs to jump to before arriving and how far that packet travels. This can explained differently: if a server is in Australia and requester is also in australia, but the packet needs to jump to the US, then to europe, then to asia then back to australia(requester) and then take the same tour back again. It makes sense it takes a longer time than this scenario: Server in australia - requester in sweden. And the packet jumps directly from sweden to australia and back again with 2 jumps in total.

But for this case, we have found the answer to which is fastest overall. Ofcause it does depend on the client, what is requesting and from where. But the United States Server is the fastest at serving a static website to about 4/5 requesters, and overall serving website faster. http://192.81.216.124:8080 : united states is the clear winner in this case. (Provided all servers are setup equaly)
