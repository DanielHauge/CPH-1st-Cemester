package main

import (
	"net"
	"encoding/json"
	"log"
)


func receive(conn net.Conn){

	defer conn.Close()
	dec := json.NewDecoder(conn)
	msg := new(Message)



	for {
		InboundTCP.Inc()
		if err := dec.Decode(msg); err != nil { return }

		if testing {log.Print("\nReceieved Message: \n Kind: "+msg.Kind+ "    MSG: "+ msg.MSG+"    Username: "+ msg.Username+"    IP: "+ msg.IP+ "     Number of Peers: ", len(msg.Usernames), "     Number of Connections: ",len(msg.IPs), "\n\n")}

		switch msg.Kind{

		case "CONNECT":
			if !handleConnect(*msg, conn){return}

		case "YELL":
			log.Println("i just yelled something: " + msg.MSG)

		case "END":
			EndDiscussion()
			ENDOutboundTCP.Inc()

		case "HEALTH":

		case "ABORT":
			if DiscussionInSession && DiscussionSpeaker == msg.Username{
				AbourtDiscussion()

			} else {
				ABORTOutboundTCP.Inc()
			}


		case "JOIN-PROPOSITION":
			EvaluateProposition(Deserialize(msg.Block), msg.Username)

		case "JOIN-PROPOSITION-ANSWER":
			CountVotes(msg.Username, msg.MSG)

		case "GIVEMECHAIN":
			ReplyWithChain(msg.Username)

		case "CHAINREPLY":
			log.Println("OOOOH I GOT REPLY!")
			if chainrequest{
				log.Println("OOHH I TRY TO FILL IN!")
				FillInChain(msg.Block, msg.MSG)

				chainrequest = false
				wg.Done()
			}else {log.Println("ohh i wasn't set for filling!")}

		case "SESSION-HEAD-COUNT":
			HeadCount(msg.Username, msg.MSG, msg.Usernames)

		case "SESSION-PROPOSAL":
			ShareMyFellowPeeps(msg.Username, msg.Usernames, msg.MSG)
			DiscussionInSession = true
			PromInSession.Set(1)
			INVOutboundTCP.Inc()

		case "DISCUSSION-TO-QUEUE":
			DiscussionQueue <- createDiscussion(msg.Username, msg.MSG)

		case "DISCONNECT":
			disconnect(*msg)
			return

		case "LIST":
			connectToPeers(*msg)
			return

		case "ADD":
			addPeer(*msg)

		}
	}

}
