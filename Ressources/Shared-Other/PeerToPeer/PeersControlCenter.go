package main

import (
	"net"
	"sync"
	"log"
	"os"
	"encoding/json"
)

const PORT = ":2017"

var (

	PeerIPs = make(map[string]string)   /// Username:IP
	Connections = make(map[string]net.Conn) /// Username:TCPConnection
	Name string /// Username of this process (Node)
	testing bool = true /// This users setting for testing
	mutex = new(sync.Mutex)  /// This users multiplexer

)


func server(){
	if testing {log.Println("server")}
	tcpAddr, err := net.ResolveTCPAddr("tcp4", PORT)
	if err != nil{ panic(err); log.Println(err.Error())}

	listener, err := net.ListenTCP("tcp", tcpAddr)
	if err != nil{ panic(err); log.Println(err.Error())}

	for {
		conn, err := listener.Accept()
		if err != nil{
			continue
		}
		if testing {log.Println("Accepted Connection")}
		go receive(conn)
	}
}



//introduces peer to the chat
func introduceMyself(IP string){

	if testing {log.Println("introduceMyself")}
	conn:=createConnection(IP)
	enc:= json.NewEncoder(conn)
	introMessage:= createMessage("CONNECT", Name , getMyIP(), "", make([]string, 0), make([]string, 0))
	enc.Encode(introMessage)
	GiveMeChainMessage:= createMessage("GIVEMECHAIN", Name, getMyIP(), "Please and thank you", make([]string, 0), make([]string, 0))
	enc.Encode(GiveMeChainMessage)
	go receive(conn)

}


//adds a peer to everyone list
func addPeer(msg Message){
	mutex.Lock()
	PeerIPs[msg.Username]=msg.IP
	conn:=createConnection(msg.IP)
	Connections[msg.Username]=conn
	mutex.Unlock()
	log.Println(msg.Username+" just joined")
}


func connectToPeers(msg Message) {
	for index, ip := range msg.IPs {
		conn:=createConnection(ip)
		mutex.Lock()
		PeerIPs[msg.Usernames[index]]=ip
		Connections[msg.Usernames[index]]=conn
		mutex.Unlock()
	}
	addMessage := createMessage("ADD", Name, getMyIP(), "", make([]string, 0), make([]string, 0))
	addMessage.send_all()
}


//creates a new connection, given the IP address, and returns it
func createConnection(IP string) (conn net.Conn){
	service:= IP+PORT
	tcpAddr, err := net.ResolveTCPAddr("tcp", service)
	if err != nil {log.Println(err.Error())}
	conn, err = net.DialTCP("tcp", nil, tcpAddr)
	if err != nil {log.Println(err.Error())}
	return
}

func ReplyWithChain(user string) {
	wg.Wait()
	ReplyMessage := createMessage("CHAINREPLY", Name, getMyIP(), "here you go", make([]string, 0), make([]string, 0))
	mem := MemoryBlockChain{}
	mem.AllBlocks = BlockChain.BlockChainToMemory()
	ReplyMessage.Block = mem.Serialize()
	ReplyMessage.sendPrivate(user)
}


func disconnect(msg Message){
	mutex.Lock()
	delete(PeerIPs, msg.Username)
	delete(Connections, msg.Username)
	mutex.Unlock()
	log.Println(msg.Username + " left")
}



func handleConnect(msg Message, conn net.Conn) bool{
	if testing {log.Println("handleConnect")}

	Users, IPS := getFromMap(PeerIPs)
	Users = append(Users, Name)
	IPS = append(IPS, getMyIP())
	response := createMessage("LIST", "", "", "", Users, IPS)
	log.Println(Users)
	if alreadyAUser(msg.Username){
		response.MSG="Username already taken, choose another one that is not in the list"
		response.send_all()
		return false
	}
	mutex.Lock()
	PeerIPs[msg.Username]=msg.IP
	Connections[msg.Username]=conn
	mutex.Unlock()
	log.Println(Connections)
	response.sendPrivate(msg.Username)
	return true

}


//checks to see if a userName is already in the list
func alreadyAUser(user string) bool {
	for userName,_:= range PeerIPs {
		if userName == user {return true}
	}
	return false
}




func getMyIP() string {
	name, err := os.Hostname()
	if err != nil {log.Println(err.Error())}
	addr, err := net.ResolveIPAddr("ip", name)
	if err != nil {log.Println(err.Error())}
	return addr.String()

}


func getFromMap(mappa map[string]string) ([]string, []string){
	var keys []string
	var values []string
	for key,value := range mappa{
		keys = append(keys,key)
		values = append(values,value)
	}
	return keys,values
}