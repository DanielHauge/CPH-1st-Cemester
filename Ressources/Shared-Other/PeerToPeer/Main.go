package main

import (
	"log"
	"net/http"
	"os"
	"sync"

)

// Args= 1: host, 2: Username, 3: Port
var chainrequest = false
var initialized = false
var debugging = false

var wg sync.WaitGroup

// go get github.com/shurcooL/github_flavored_markdown

func main() {

	Name = os.Args[2]
	wg.Add(1)
	DiscussionQueue = make(chan Discussion, 10)
	log.Println("Initializing P2P Server")
	go server()
	if os.Args[1]!="127.0.0.1"{
		chainrequest = true
		go introduceMyself(os.Args[1]);
		log.Println("Connecting to peers")
	} else {
			log.Println("I'm the first peer on the network, therefor will not introduce myself")
			BlockChain = NewBlockChain()
			log.Println("I'm finished! Mining first block")
			initialized = true
			wg.Done()
	}




	log.Println("Initiazling API")
	router := NewRouter()
	log.Fatal(http.ListenAndServe(":8080", router))
	log.Println("DONE [x]")



}

