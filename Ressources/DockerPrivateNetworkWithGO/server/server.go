package main

import (
	"fmt"
	"net"
	"bufio"
	"math/rand"
	"strconv"
)

func main() {

	fmt.Println("Launching server...")

	// listen on all interfaces
	ln, _ := net.Listen("tcp", ":8081")

	// accept connection on port
	conn, _ := ln.Accept()

	// run loop forever (or until ctrl-c)
	for {
		// will listen for message to process ending in newline (\n)
		message, _ := bufio.NewReader(conn).ReadString('\n')
		// output message received
		fmt.Print("Message Received:", string(message))


		randomint := rand.Int()
		ri := strconv.Itoa(randomint)
		// sample process for string received
		newmessage := ri
		// send new string back to client
		fmt.Print(" [x] Sending message: \n"+newmessage+"\n")
		conn.Write([]byte(newmessage + "\n"))
	}
}
