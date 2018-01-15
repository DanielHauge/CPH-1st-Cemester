package main

import (
	"net"
	"fmt"
	"bufio"
)

func main() {

	// connect to this socket
	conn, _ := net.Dial("tcp", "127.0.0.1:8081")
	for {
		// read in input from stdin
		text:= "Randomint: "
		// send to socket
		fmt.Fprintf(conn, text + "\n")
		// listen for reply
		message, _ := bufio.NewReader(conn).ReadString('\n')
		fmt.Print("Message from server: "+message)
	}
}